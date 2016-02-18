using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using com.etak.core.customer.microservices;
using com.etak.core.dealer.messages.GetDealerInfoMVNOByDealerId;
using com.etak.core.microservices.messages.CalculateNextPeriod;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.product.message.GetRmPromotionPlanInfosByIds;
using com.etak.core.promotion.messages.CreateCustomersPromotion;
using com.etak.core.promotion.messages.GetCustomersPromotionInfoByPromotionId;
using com.etak.core.promotion.microservices;
using log4net;

namespace com.etak.core.bizops.fullfilment.ApplyCustomerPromotion
{
	/// <summary>
	/// Business Operation to apply a specific Promotion to a Customer
	/// </summary>
	public class ApplyCustomerPromotionBizOp : AbstractSinglePhaseOrderProcessor<ApplyCustomerPromotionRequestDTO, ApplyCustomerPromotionResponseDTO, ApplyCustomerPromotionRequestInternal, ApplyCustomerPromotionResponseInternal, ApplyCustomerPromotionOrder>
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// Maps all the inboud properties of the request that are not mapped by the framework
		/// </summary>
		/// <param name="request">the request in ET DTO Form</param><param name="coreInput">the resquest partially mapped by the core and needs to be updated</param>
		protected override void MapNotAutomappedOrderInboundProperties(ApplyCustomerPromotionRequestDTO request, ref ApplyCustomerPromotionRequestInternal coreInput)
		{
			var getRmPromotionByPromotionIdMs = MicroServiceManager.GetMicroService<GetRmPromotionPlanInfosByIdsRequest, GetRmPromotionPlanInfosByIdsResponse>();
			var getRmPromReq = new GetRmPromotionPlanInfosByIdsRequest()
			{
				PromotionPlanIds = request.PromotionPlanIds.ToList(),
			};
			var getRmPromResp = getRmPromotionByPromotionIdMs.Process(getRmPromReq, null);
			if (getRmPromResp.ResultType != ResultTypes.Ok || !getRmPromResp.RmPromotionPlanInfos.Any())
				throw new BusinessLogicErrorException(string.Format("Error tryng to get the Promotion List for customer {0}", request.CustomerId), BizOpsErrors.ApplyCustomerPromotionCantGetPromotionsList);
			
			coreInput.PromotionPlans = getRmPromResp.RmPromotionPlanInfos.ToList();
			coreInput.FactorToCreditLimit = request.FactorToCreditLimit;
		}

		/// <summary>
		/// Maps all the outboud properties of the response that are not mapped by the framework
		/// </summary>
		/// <param name="source">the response of the core operation that needs to be mapped</param><param name="DTOOutput">the response of the operation in DTO format</param>
		protected override void MapNotAutomappedOrderOutboundProperties(ApplyCustomerPromotionResponseInternal source, ref ApplyCustomerPromotionResponseDTO DTOOutput)
		{
			
		}

		/// <summary>
		/// The implementation of IOrderProcessor
		/// </summary>
		/// <param name="order">The order to be processed</param><param name="request">The request to process</param>
		/// <returns>
		/// The result of the process
		/// </returns>
		public override ApplyCustomerPromotionResponseInternal ProcessRequest(ApplyCustomerPromotionOrder order, ApplyCustomerPromotionRequestInternal request)
		{
			#region MicroServices to be used
			var createPromotionMs = MicroServiceManager.GetMicroService<CreateCustomersPromotionRequest, CreateCustomersPromotionResponse>();
			var calculateNextPeriodMs = MicroServiceManager.GetMicroService<CalculateNextPeriodRequest, CalculateNextPeriodResponse>();
			#endregion

			CustomerInfo customerInfo = request.Customer;
			
			Log.Info("Check that promotions are valid...");
			#region Check that request is valid
			if (!request.PromotionPlans.Any())
				throw new BusinessLogicErrorException("Cannot apply promotions to a Customer without Promotions", BizOpsErrors.ApplyCustomerPromotionMustProvidePromotionPlanId);

			CheckPromotions(request.PromotionPlans, request.MVNO); 
			#endregion

			Log.Info("Get all the PromotionPlans from the promotions");
			List<RmPromotionPlanDetailInfo> promotionDetails = request.PromotionPlans.SelectMany(x => x.RmPromotionPlanDetailInfoList).ToList();
			var promotionPlan = new List<CrmCustomersPromotionInfo>();

			Log.Info("For each promotion detail, let's build the correspongind Customer Promotion.");
			foreach (var promoDetail in promotionDetails)
			{
				#region Prepare CrmCustomersPromotionInfo with basic information

				var customerPromotion = new CrmCustomersPromotionInfo()
				{
					PromotionDetail = promoDetail,
					Customer = customerInfo,
					CustomerId = customerInfo.CustomerID.Value,
					RenewAutomatically = promoDetail.RmPromotionPlanInfo.RenewAutomatically,
					Active = true,
					FirstUsed = null,
					RenewalCount = 0,
					StartDate = request.StartDate ?? DateTime.Now,
					WhiteList = promoDetail.WhiteList,
					Priority = null,
					ActiveWithoutCredit = null,
					CurrentLimit =
						request.FactorToCreditLimit == null
							? promoDetail.Limit
							: request.FactorToCreditLimit.Value*promoDetail.Limit,
				};
				#endregion

			

				if (promoDetail.Periodicity > 0)
				{
                    #region Call CalculateNextPeriodMS to get all the dates needed

                    //for calculate Logical Start Date for yearly monthly and weekly..
                    DateTime logicalStartDate = customerPromotion.StartDate.Value;
                    if (promoDetail.RmPromotionPlanInfo.DailyResetTime == 0)
                    {
                        //means natural.
                        switch (promoDetail.PeriodUnit)
                        {
                            case TimeUnits.Year: logicalStartDate = new DateTime(logicalStartDate.Year, 1, 1); break;
                            case TimeUnits.Month: logicalStartDate = new DateTime(logicalStartDate.Year, logicalStartDate.Month, 1); break;
                            case TimeUnits.Week: logicalStartDate = logicalStartDate.FirstDayOfWeek(); break;
                        }
                    }

                    var calculateNextPeriodReq = new CalculateNextPeriodRequest()
                    {
                        CurrentCycleNumber = promoDetail.Periodicity > 0 ? 1 : 0,
                        CycleRepeatCount = promoDetail.CycleRepeatCount,
                        EndPeriodNumber = promoDetail.EndPeriodNumber,
                        NextDate = logicalStartDate,
                        NextPeriodNumber = 0,
                        PeriodCount = promoDetail.PeriodCount,
                        PeriodUnit = promoDetail.PeriodUnit,
                        Periodicity = promoDetail.Periodicity,
                        StartPeriodNumber = promoDetail.StartPeriodNumber,

                    };
                    var calculateNextPeriodResp = calculateNextPeriodMs.Process(calculateNextPeriodReq, null);

                    #endregion

					#region Periodic promotion 
					customerPromotion.CurrentCycleNumber = calculateNextPeriodResp.CurrentCycleNumber;
					customerPromotion.NextRenewDate = calculateNextPeriodResp.NextDate.Date;
					customerPromotion.PreRenewalActionsDate = (customerPromotion.NextRenewDate != null)
						? customerPromotion.NextRenewDate.Value.AddMinutes(0 - promoDetail.PreRenewalActionsMinutesOffset)
						: (DateTime?) null;

					customerPromotion.NextPeriodNumber = calculateNextPeriodResp.NextPeriodNumber;
					customerPromotion.EndDate = customerPromotion.NextRenewDate.Value.AddSeconds(-1);
					#endregion
				}
				else
				{
                    customerPromotion.EndDate = request.EndDate ?? promoDetail.EndDate;
				}

				customerInfo.Promotions.Add(customerPromotion);
				promotionPlan.Add(customerPromotion);

				#region Create Promotion
				var createCustomerPromotionReq = new CreateCustomersPromotionRequest()
				{
					CrmCustomersPromotionInfo = customerPromotion
				};
				var createCustomerPromotionResp = createPromotionMs.Process(createCustomerPromotionReq, null);
				if (createCustomerPromotionResp.ResultType != ResultTypes.Ok)
					throw new BusinessLogicErrorException(string.Format("Cannot create the customer promotion for customer {0} with promotion plan id {1}",
							customerInfo.CustomerID.Value, promoDetail.PromotionPlanDetailId),
							BizOpsErrors.CustomerPromotionCreationError);
				#endregion
				
			}

			return new ApplyCustomerPromotionResponseInternal()
			{
				ResultType = ResultTypes.Ok,
				CrmCustomersPromotionInfos = promotionPlan,
				Customer = customerInfo,
                Subscription = customerInfo.ResourceMBInfo.FirstOrDefault(x => x.StatusID == (int)ResourceStatus.Active),
			};
		}


		#region OperationDiscriminator and OperationCode
		/// <summary>
		/// Unique Id of the operation
		/// </summary>
		public override string OperationDiscriminator
		{
			get { return OperationDiscriminators.ApplyCustomerPromotionOperation; }
		}

		/// <summary>
		/// Code that will be stored in the operation log for the operation
		/// </summary>
		public override string OperationCode
		{
			get { return OperationCodes.ApplyCustomerPromotionOperation; }
		} 
		#endregion

		/// <summary>
		/// Check that all the Promotions Passed are owned by the Dealer
		/// </summary>
		/// <param name="promotionList"></param>
		/// <param name="mvno"></param>
		private static void CheckPromotions(IList<RmPromotionPlanInfo> promotionList, DealerInfo mvno)
		{
			var getMvnoById = MicroServiceManager.GetMicroService<GetDealerInfoMVNOByDealerIdRequest, GetDealerInfoMVNOByDealerIdResponse>();
			var getMvnoByIdReq = new GetDealerInfoMVNOByDealerIdRequest();
			var getMvnoByIdResponse = new GetDealerInfoMVNOByDealerIdResponse();


			foreach (var promo in promotionList)
			{
				int promoDealerId = promo.DealerId;
				if (promoDealerId != mvno.FiscalUnitID)
				{
					getMvnoByIdReq.DealerId = promoDealerId;
					getMvnoByIdResponse = getMvnoById.Process(getMvnoByIdReq, null);
					promoDealerId = getMvnoByIdResponse.DealerInfo.DealerID.Value;
				}
				
				if (promoDealerId != mvno.FiscalUnitID)
					throw new BusinessLogicErrorException(string.Format("The promotion {0} doesn't belong to dealer {1}", promo.PromotionPlanId, mvno.DealerID ), BizOpsErrors.PromotionPlanHasNotSameDealerAsCustomer);

				if (promo.EndDate != null && promo.EndDate.Value < DateTime.Now)
					throw new BusinessLogicErrorException(string.Format("The promotion {0} has expired!", promo.PromotionPlanId), BizOpsErrors.PromotionExpired);
			}

		}
	}
}
