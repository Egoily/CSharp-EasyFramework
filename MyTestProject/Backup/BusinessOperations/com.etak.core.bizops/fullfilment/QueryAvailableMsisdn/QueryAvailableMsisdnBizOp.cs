using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using com.etak.core.dealer.messages.GetDealerInfosByFiscalUnitId;
using com.etak.core.dealer.messages.GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItem;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.model.resource;
using com.etak.core.operation;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.resource.msisdn.message.GetNumberByCategoryAndVmoAndStatusIdIn;
using log4net;

namespace com.etak.core.bizops.fullfilment.QueryAvailableMsisdn
{
    /// <summary>
    /// Operation to get available msisdn
    /// </summary>
    public class QueryAvailableMsisdnBizOp : AbstractBusinessOperation<QueryAvailableMsisdnRequestDTO, QueryAvailableMsisdnResponseDTO, QueryAvailableMsisdnRequestInternal, QueryAvailableMsisdnResponseInternal>
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get the NumberInfos to be map to internal/core input, by getting all the number info for the dealer with status = ResourceStatus.Init
        /// </summary>
        /// <param name="dtoRequest">the dto request</param>
        /// <param name="coreInput">the internal request</param>
        protected override void MapNotAutomappedInboundProperties(QueryAvailableMsisdnRequestDTO dtoRequest, ref QueryAvailableMsisdnRequestInternal coreInput)
        {
            //Get DealerInfos By FiscalUnitId
            if (coreInput.MVNO.DealerID != null)
            {
                #region Get Dealers By FiscalUnitId
                if (Log.IsDebugEnabled)
                    Log.InfoFormat("Calling GetDealerInfosByFiscalUnitIdMS to get the DealerInfo with FiscalUnitId {0}",
                        coreInput.MVNO.DealerID.Value);
                var getDealerInfosByFiscalUnitIdReq = new GetDealerInfosByFiscalUnitIdRequest()
                {
                    FiscalUnitId = coreInput.MVNO.DealerID.Value
                };
                var getDealerInfoByFiscalUnitIdMS =
                    MicroServiceManager
                        .GetMicroService<GetDealerInfosByFiscalUnitIdRequest, GetDealerInfosByFiscalUnitIdResponse>();
                var getDealerInfosByFiscalUnitIdRes =
                    getDealerInfoByFiscalUnitIdMS.Process(getDealerInfosByFiscalUnitIdReq, null);
                if (getDealerInfosByFiscalUnitIdRes == null)
                {
                    if (Log.IsDebugEnabled)
                        Log.InfoFormat("Cannot found DealerInfo with FiscalUnitId {0}", coreInput.MVNO.DealerID.Value);

                    throw new DataValidationErrorException(
                        string.Format("Cannot found DealerInfo with FiscalUnitId {0}", coreInput.MVNO.DealerID.Value),
                        BizOpsErrors.DealerInfoNotFound);
                }
                #endregion
                #region Get NumberInfo By vMNO, Category, Quantity, And StatusId

                var statusList = new List<int> { (int)ResourceStatus.Init };
                var dealerIdList = getDealerInfosByFiscalUnitIdRes.DealerInfos.Select(x => x.DealerID.Value);
                var idList = dealerIdList as IList<int> ?? dealerIdList.ToList();
                if (!idList.Any())
                {
                    throw new DataValidationErrorException("DealerIdList is null", BizOpsErrors.DealerIdIsNull);
                }
                var getMvnoConfigActionMS =
                    MicroServiceManager.GetMicroService<GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest,
                        GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResponse>();

                var GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemReq = new GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest()
                {
                    CategoryId = 0,
                    MvnoId = coreInput.MVNO.DealerID.Value,
                    Item = "Filter_Msisdns_Range",
                    StatusId = 1
                };

                var filterList = new List<string>();

                Log.Debug("Getting filter msisdns...");
                var GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResp = getMvnoConfigActionMS.Process(GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemReq, null);
                if (GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResp.ResultType == ResultTypes.Ok)
                {
                    if (GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResp.MvnoConfigActionInfos.Any())
                    {
                        var filterValueRange =
                            GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResp.MvnoConfigActionInfos.First().Value;
                        filterList = GetMsisdnFilterList(filterValueRange);
                        Log.Debug(string.Format("Filter msisdns:{0}", string.Join(",", filterList.ToList())));
                    }
                }
                Log.Debug("Get filter msisdns end");

                var getNumberByCategoryAndVmoAndStatusIdInReq = new GetNumberByCategoryAndVmoAndStatusIdInRequest()
                {
                    CategoryId = dtoRequest.CategoryId,
                    StatusId = statusList,
                    Vmo = idList,
                    MaxElements = dtoRequest.Quantity + filterList.Count //here, we take more element to avoid some number is filtered
                };

                var getNumberByCategoryAndVmoAndStatusIdInMs =
                    MicroServiceManager
                        .GetMicroService
                        <GetNumberByCategoryAndVmoAndStatusIdInRequest, GetNumberByCategoryAndVmoAndStatusIdInResponse>();
                if (Log.IsDebugEnabled)
                    Log.InfoFormat(
                        "Calling GetNumberByCategoryAndVmoAndStatusIdInMS to get the NumberInfo with CategoryId {0}, StatusId {1}, Vmo {2}, MaxElements {3}",
                        dtoRequest.CategoryId, ResourceStatus.Init, dealerIdList, getNumberByCategoryAndVmoAndStatusIdInReq.MaxElements);
                var getNumberByCategoryAndVmoAndStatusIdInRes =
                    getNumberByCategoryAndVmoAndStatusIdInMs.Process(getNumberByCategoryAndVmoAndStatusIdInReq, null);

                #endregion

                #region Fill the core input

                coreInput.NumberInfos = getNumberByCategoryAndVmoAndStatusIdInRes.NumberInfo.OrderBy(x=>x.Resource);

                var numberInfos = coreInput.NumberInfos as NumberInfo[] ?? coreInput.NumberInfos.ToArray();
                Log.Debug(string.Format("Query the {0} available msisdns:{1}", dtoRequest.Quantity + filterList.Count, string.Join(",", numberInfos.Select(x=>x.Resource).ToList())));

                if (filterList.Any())
                {
                    var list = numberInfos.Where(rs => !filterList.Contains(rs.Resource)).Take(dtoRequest.Quantity).ToList();
                    coreInput.NumberInfos = list;

                    Log.Debug(string.Format("Return {0} available msisdns:{1}", dtoRequest.Quantity, string.Join(",", coreInput.NumberInfos.Select(x => x.Resource).ToList())));
                }
                #endregion
            }
            else
            {
                throw new DataValidationErrorException(
                        string.Format("Dealer Id is null{0}", coreInput.MVNO.DealerID.Value),
                        BizOpsErrors.DealerIdIsNull);
            }

        }

        /// <summary>
        /// Map List of NumberInfo to MsisdnResourceDTO
        /// </summary>
        /// <param name="coreOutput">the internal output</param>
        /// <param name="dtoOutput">the dto output</param>
        protected override void MapNotAutomappedOutboundProperties(QueryAvailableMsisdnResponseInternal coreOutput, ref QueryAvailableMsisdnResponseDTO dtoOutput)
        {
            dtoOutput.AvailableMsisdns = new List<MSISDNResourceDTO>();
            if (coreOutput.NumberInfos != null)
            {
                foreach (var numberInfo in coreOutput.NumberInfos)
                {
                    dtoOutput.AvailableMsisdns.Add(numberInfo.ToDto());
                }
            }
        }

        /// <summary>
        /// Code that will be stored in the operation log for the operation
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.QueryAvailableMSISDNOperation; }
        }
        /// <summary>
        /// Unique identifier of the operation
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.QueryAvailableMSISDNOperation; }
        }

        /// <summary>
        /// Simple process only returning the response that has already been in core request/QueryAvailableMsisdnRequestInternal
        /// </summary>
        /// <param name="request">the request in core form</param>
        /// <param name="runningOperation">The trace of the operation</param>
        /// <param name="invoker">the environemnt of the invokation</param>
        /// <returns>QueryAvailableMsisdnResponseInternal with available msisdn</returns>
        protected override QueryAvailableMsisdnResponseInternal ProcessBusinessLogic(QueryAvailableMsisdnRequestInternal request, BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            return new QueryAvailableMsisdnResponseInternal()
            {
                NumberInfos = request.NumberInfos,
                ResultType = ResultTypes.Ok,
                Message = "Query Success",
                ErrorCode = 0
            };
        }

        private List<string> GetMsisdnFilterList(string filterValue)
        {
            var filterList = new List<string>();
            try
            {
                var ranges = filterValue.Split(
                    new char[]
                    {
                        ';',
                        ','
                    });
                if (ranges.Any())
                {
                    foreach (var range in ranges.Where(x => !string.IsNullOrWhiteSpace(x)))
                    {
                        var msisdns = range.Split('-');
                        if (msisdns.Length == 1)
                        {
                            if (!filterList.Contains(msisdns[0].ToString()))
                            {
                                filterList.Add(msisdns[0].ToString());
                            }
                        }
                        else if (msisdns.Length == 2)
                        {
                            var minRange = long.Parse(msisdns[0]);
                            var maxRange = long.Parse(msisdns[1]);
                            if (minRange > maxRange)
                            {
                                var temp = minRange;
                                minRange = maxRange;
                                maxRange = temp;

                            }

                            for (long i = minRange; i <= maxRange; i++)
                            {
                                if (!filterList.Contains(i.ToString()))
                                {
                                    filterList.Add(i.ToString());
                                }

                            }
                        }
                    }
                }
                return filterList;

            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Error at {0}:", MethodBase.GetCurrentMethod().Name), ex);
                return new List<string>();
            }

        }
    }
}
