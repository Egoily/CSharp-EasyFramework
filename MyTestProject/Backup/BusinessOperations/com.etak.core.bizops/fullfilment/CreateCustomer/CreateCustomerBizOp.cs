using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using com.etak.core.customer.message.CreateAddresses;
using com.etak.core.customer.message.CreateCustomerInfo;
using com.etak.core.dealer.messages.GetDealerInfoById;
using com.etak.core.GSMSubscription.message.GetCustomerDataRoamingLimitNotificationByCustomerId;
using com.etak.core.GSMSubscription.messages.CreateCustomerDastaRoamingLimit;
using com.etak.core.GSMSubscription.messages.CreateCustomerDastaRoamingLimitNotification;
using com.etak.core.GSMSubscription.messages.GetCustomerDataRoamingLimitsByCustomerID;
using com.etak.core.microservices.messages.GetLanguageTypeByCode;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using log4net;
using PropertyInfo = com.etak.core.model.PropertyInfo;
using com.etak.core.dealer.messages.GetMVNOConfigActionInfosByIdAndName;

namespace com.etak.core.bizops.fullfilment.CreateCustomer
{
    /// <summary>
    /// Bizop to register customer
    /// </summary>
    public class CreateCustomerBizOp : AbstractSinglePhaseOrderProcessor<CreateCustomerRequestDTO,CreateCustomerResponseDTO,CreateCustomerRequestInternal,CreateCustomerResponseInternal,CreateCustomerOrder>
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Mapping not Automapped Create Customer Properties
        /// </summary>
        /// <param name="request">CreateCustomerRequestDTO</param>
        /// <param name="coreInput">CreateCustomerRequestInternal</param>
        protected override void MapNotAutomappedOrderInboundProperties(CreateCustomerRequestDTO request, ref CreateCustomerRequestInternal coreInput)
        {
            ValidateRequestDto(request);
            if (request.LanguageId.HasValue)
            {
                var getLanguageMs =
                    MicroServiceManager
                        .GetMicroService<GetLanguageTypeInfoByCodeRequest, GetLanguageTypeInfoByCodeResponse>();
                var getLanguageRequest = new GetLanguageTypeInfoByCodeRequest()
                {
                    LanguadeId = (int) request.LanguageId
                };
                var getLanguageResponse = getLanguageMs.Process(getLanguageRequest, null);
                if (!getLanguageResponse.LanguageTypeInfos.IsNullOrEmpty())
                {
                    var languageInfos =
                        getLanguageResponse.LanguageTypeInfos.FirstOrDefault(
                            lang => lang.LanguageID == getLanguageRequest.LanguadeId);
                    if (languageInfos != null && languageInfos.LanguageID != null)
                        coreInput.LanguageId = (int) languageInfos.LanguageID;
                }
            }

            
            coreInput.ExternalCustomerID = request.ExternalCustomerID;
            coreInput.BillingDate = request.BillingDate;
            coreInput.PendingStatus = request.PendingStatus;
            // check nullable value, if they has value then assign it to core input
            if (request.BusinessType.HasValue) coreInput.BusinessType = (int)request.BusinessType;
            if (request.RegistrationType.HasValue) coreInput.RegistrationType = (int)request.RegistrationType;
            if (request.PaymentType.HasValue) coreInput.PaymentType = (int)request.PaymentType;
            if (request.InvoiceDetails.HasValue) coreInput.InvoiceDetails = (bool)request.InvoiceDetails;
            if (request.InvoiceDueDate.HasValue) coreInput.InvoiceDueDate = (int) request.InvoiceDueDate;
            if (request.PendingStatus.HasValue) coreInput.PendingStatus = (int) request.PendingStatus;
            if (request.BillingDate.HasValue) coreInput.BillingDate = (int) request.BillingDate;
            coreInput.ParentBilling = request.ParentBilling;
            coreInput.WithDrawPeriod = request.WithDrawPeriod;

            coreInput.CustomerToBeCreated = request.CustomerToBeCreatedDto.ToCore();
        }
        /// <summary>
        /// Mapping Internal response to DTO output
        /// </summary>
        /// <param name="source">CreateCustomerResponseInternal</param>
        /// <param name="DTOOutput">CreateCustomerResponseDTO</param>
        protected override void MapNotAutomappedOrderOutboundProperties(CreateCustomerResponseInternal source, ref CreateCustomerResponseDTO DTOOutput)
        {
            if (source.Customer != null)
            {
                DTOOutput.Customer = source.Customer.ToDto();
            }
            DTOOutput.resultType = source.ResultType;
        }

        /// <summary>
        /// logic of Create Customer 
        /// </summary>
        /// <param name="order">CreateCustomerOrder</param>
        /// <param name="request">CreateCustomerRequestInternal</param>
        /// <returns>Internal response as CreateCustomerResponseInternal</returns>

        public override CreateCustomerResponseInternal ProcessRequest(CreateCustomerOrder order, CreateCustomerRequestInternal request)
        {
            
            //get configuration for this operation
            Log.InfoFormat("Get register customer configuration for MVNO ({0}).", request.MVNO.DealerID);
            CreateCustomerConfiguration config = GetOperationConfigForDealer<CreateCustomerConfiguration>(request.MVNO);
            GetCreateCustomerCofiguration(config,ref request);
            
            CustomerInfo newCustomerInfo = request.CustomerToBeCreated;
            
            //customer basic info
            newCustomerInfo.DealerID = request.MVNO.DealerID;
            newCustomerInfo.UserID = request.User.UserID;
            if(!request.RegistrationType.HasValue) newCustomerInfo.RegistrationType = config.RegistrationType;
            newCustomerInfo.CreateDate = DateTime.Now;
            newCustomerInfo.ActivedDate = DateTime.Now;

            newCustomerInfo.StatusID = null;
            //if postpaid then must have a billing date
            if (request.PaymentType != null && (int) request.PaymentType == (int) PaymentType.Postpayment)
            {
                //if request has billingdate
                if (request.BillingDate.HasValue)
                    newCustomerInfo.BillingDate = request.BillingDate;
                // get from value from config or get billing date by function
                else
                {
                    newCustomerInfo.BillingDate = config.BillCycleId.HasValue ? config.BillCycleId : GetBillingDate();
                }
            }
            else
            {
                newCustomerInfo.BillingDate = null;
            }

            PropertyInfo propertyInfo = newCustomerInfo.PropertyInfo[0];

            propertyInfo.PendingStatus = request.PendingStatus;
            propertyInfo.LanguageID = request.LanguageId;
            propertyInfo.CustomerTypeID = request.BusinessType;
            propertyInfo.PaymentMethodID = request.PaymentType ;
            propertyInfo.UserID = request.User.UserID;
            propertyInfo.UserName = request.User.UserName;
            if (request.InvoiceDetails != null) propertyInfo.InvoiceDetails = (bool) request.InvoiceDetails;
            propertyInfo.InvoiceDueDate = request.InvoiceDueDate;
            propertyInfo.ParentBilling = request.ParentBilling;
            propertyInfo.WithDrawPeriod = request.WithDrawPeriod;
            propertyInfo.ExternalId = request.ExternalCustomerID;

            newCustomerInfo.PropertyInfo[0] = propertyInfo;

            if (newCustomerInfo.Addresses.Any())
            {
                var createAddressResponse = CreateAddresses(newCustomerInfo.Addresses);
                if(createAddressResponse.ResultType != ResultTypes.Ok)
                    throw new BusinessLogicErrorException("Failed when create address",BizOpsErrors.CreateAddressFailed);
            }

            foreach (var item in newCustomerInfo.BankInfo)
            {
                item.CreateDate = DateTime.Now;
                item.UserID = request.User.UserID;
                item.CustomerInfo = newCustomerInfo;
                item.StartDate = DateTime.Now;
            }

            newCustomerInfo = buildMappingInfo(newCustomerInfo, true, config);

            #region create DataRoamingLimit and DataRoamingLimitNotification info
            if (newCustomerInfo.DataRoamingLimits.IsNullOrEmpty())
            {
                newCustomerInfo.DataRoamingLimits = new List<CustomerDataRoamingLimit>();
                CustomerDataRoamingLimit tt = new CustomerDataRoamingLimit()
                    {
                        ContinueBySMS = true,
                        ContinueSUM = 0,
                        DataRoamingConsumptionCounter = 0,
                        DataRoamingLimit =(decimal)DataRoamingLimitType.MvnoDefault,
                        Customer = newCustomerInfo
                    };
                newCustomerInfo.DataRoamingLimits.Add(tt);
                Log.InfoFormat("Create DataRoamingLimit for customer:{0}.", newCustomerInfo.CustomerID.Value);
            }

            if (newCustomerInfo.DataRoamingLimitNotifications.IsNullOrEmpty())
            {

                IEnumerable<com.etak.core.model.MVNODataRoamingLimitNotification> MvnoDataRoamingNotifications =
                    request.MVNO.MVNODataRoamingLimitNotificationList;

                
                if (MvnoDataRoamingNotifications != null && MvnoDataRoamingNotifications.Count() > 0)
                {
                    newCustomerInfo.DataRoamingLimitNotifications = new List<CustomerDataRoamingLimitNotification>();
                    var createCustomerDataRoamingLimitNotification = MicroServiceManager.GetMicroService<CreateCustomerDastaRoamingLimitNotificationRequest, CreateCustomerDastaRoamingLimitNotificationResponse>();
                    foreach (var item in MvnoDataRoamingNotifications)
                    {
                        newCustomerInfo.DataRoamingLimitNotifications.Add(new CustomerDataRoamingLimitNotification()
                            {
                                ISSent = 0,
                                MVNODataRoamingLimitNotification = item,
                                NotificationType = item.NotificationType,
                                StatusID = item.NotificationType == (int)SMSTemplateRoamingDataNotificationType.DailyAccumulatedConsumption ? 0 : item.StatusId,
                                Customer = newCustomerInfo
                            }
                        );
                    }
                    Log.InfoFormat("Create DataRoamingLimitNotification for customer:{0}.", newCustomerInfo.CustomerID.Value);
                }
            }
            #endregion create DataRoamingLimit and DataRoamingLimitNotification info

            #region create customer

            //validate request
            ValidateRequest(request);

            var createCustomerMs =
                MicroServiceManager.GetMicroService<CreateCustomerInfoRequest, CreateCustomerInfoResponse>();
            var createCustomerInfoRequest = new CreateCustomerInfoRequest()
            {
                CustomerInfo = newCustomerInfo
            };

            var createCustomerInfoResponse = createCustomerMs.Process(createCustomerInfoRequest, null);

            if (createCustomerInfoResponse.CustomerInfo == null || createCustomerInfoResponse.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException("Cannot create Customer", BizOpsErrors.CustomerIsNull);

            #endregion create customer

            var response = new CreateCustomerResponseInternal()
            {
                Customer = createCustomerInfoResponse.CustomerInfo,
                Subscription = createCustomerInfoResponse.CustomerInfo.ResourceMBInfo.FirstOrDefault(x => x.StatusID == (int)ResourceStatus.Active),
                ResultType =  createCustomerInfoResponse.ResultType
            };
            return response;
        }

        /// <summary>
        /// Operation Code for Create Customer Operation
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.CreateCustomerOperation; }
        }

        /// <summary>
        /// Operation Discriminator for Create Customer Operation
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.CreateCustomerOperation; }
        }



        #region private method
        /// <summary>
        /// check if some of properties are part of configuration than check it, if is null then use config to fill it as default value 
        /// </summary>
        /// <param name="config">CreateCustomerConfiguration</param>
        /// <param name="request">CreateCustomerRequestInternal</param>
        private void GetCreateCustomerCofiguration(CreateCustomerConfiguration config, ref CreateCustomerRequestInternal request)
        {
          
            if (!request.BusinessType.HasValue) request.BusinessType = (int)config.BusinessType;
            if (!request.RegistrationType.HasValue) request.RegistrationType = (int)config.RegistrationType;
            if (!request.PaymentType.HasValue) request.PaymentType = (int)config.PaymentType;
            if (!request.InvoiceDetails.HasValue) request.InvoiceDetails = (bool)config.InvoiceDetails;
            if (!request.InvoiceDueDate.HasValue) request.InvoiceDueDate = (int)config.InvoiceDueDate;
            if (!request.PendingStatus.HasValue) request.PendingStatus = (int)config.PendingStatus;
            if (!request.LanguageId.HasValue) request.LanguageId = (int) config.LanguageID;


        }
        /// <summary>
        /// validate Create customer request Dto
        /// </summary>
        /// <param name="request">CreateCustomerRequestDTO</param>
        private void ValidateRequestDto(CreateCustomerRequestDTO request)
        {
            if(request.CustomerToBeCreatedDto == null)
                throw new BusinessLogicErrorException("CustomerDTO cannot be null",BizOpsErrors.CustomerIsNull);
           
        }
        /// <summary>
        /// Validate Internal request
        /// </summary>
        /// <param name="request"></param>
        private void ValidateRequest(CreateCustomerRequestInternal request)
        {
            if (request.CustomerToBeCreated == null)
                throw new BusinessLogicErrorException("CustomerInfo cannot be null", BizOpsErrors.CustomerIsNull);
            if(!request.BusinessType.HasValue)
                throw new BusinessLogicErrorException("Invalid request Business Type cannot be null", BizOpsErrors.BussinesTypeIsNull);
            if(!request.InvoiceDetails.HasValue)
                throw new BusinessLogicErrorException("Invalid request Invoice Details cannot be null", BizOpsErrors.InvoiceDetailIsNull);
            if (!request.PaymentType.HasValue)
                throw new BusinessLogicErrorException("Invalid request Payment Type cannot be null", BizOpsErrors.PaymentTypeIsNull);
            if (!request.RegistrationType.HasValue)
                throw new BusinessLogicErrorException("Invalid request Registration Type cannot be null", BizOpsErrors.RegistrationTypeIsNull);
            if (request.PaymentType == (int) PaymentType.Postpayment)
            {
                if (request.InvoiceDueDate == 0 || request.InvoiceDueDate > 31 || request.InvoiceDueDate < 1)
                {
                    throw new BusinessLogicErrorException("Incorrect value for InvoiceDueDate", BizOpsErrors.InvoiceDueDateIsNotValid);
                }

            }
           if(request.PendingStatus == 0)
                throw new BusinessLogicErrorException("Cannot create customer whitin invalid status",BizOpsErrors.CustomerHaveInvalidStatus);
        }

        /// <summary>
        /// Get billing date base on date of creating customer
        /// </summary>
        /// <returns>number as billing periode </returns>
        private int GetBillingDate()
        {
            var day = DateTime.Today.Day;
            if (day < (int)BillingPeriode.FirstPeriode)
                return (int)BillingPeriode.FirstPeriode;
            if (day >= (int) BillingPeriode.FirstPeriode && day < (int) BillingPeriode.SecondPeriode)
                return (int) BillingPeriode.SecondPeriode;
            if (day >= (int) BillingPeriode.SecondPeriode && day < (int) BillingPeriode.ThirdPeriode)
                return (int) BillingPeriode.ThirdPeriode;
            return (int) BillingPeriode.DefaultPeriode;
        }

        /// <summary>
        /// Create customer address
        /// </summary>
        /// <param name="customerAddresses"></param>
        /// <returns>CreateAddressesResponse</returns>
        private CreateAddressesResponse CreateAddresses(IEnumerable<CustomerAddress> customerAddresses)
        {
            var enumerable = customerAddresses as IList<CustomerAddress> ?? customerAddresses.ToList();
            if (enumerable.IsNullOrEmpty()) return null;

            IMicroService<CreateAddressesRequest, CreateAddressesResponse> CreateAddressMS = MicroServiceManager.GetMicroService<CreateAddressesRequest, CreateAddressesResponse>();


            IList<CustomerAddress> insertedAddressList = new List<CustomerAddress>();
            //create new address (if not exist)
            foreach (var customerAddress in enumerable)
            {
                var actualAddress = customerAddress;
                var toBeInsert = true;
                foreach (var insertedAddress in insertedAddressList)
                {
                    if (insertedAddress.Address.CompareInformation(actualAddress.Address))
                    {
                        actualAddress.Address.CreateDate = DateTime.Now;
                        toBeInsert = false;
                        break;
                    }
                }
                if (!toBeInsert) continue;
                actualAddress.Address.CreateDate = DateTime.Now;
                insertedAddressList.Add(actualAddress);
                   
            }
          
            var createAddressRequest = new CreateAddressesRequest()
            {
                CustomerAddresses = insertedAddressList
            };

            return CreateAddressMS.Process(createAddressRequest, null);

        }

        /// <summary>
        /// mapping value of customer 
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <param name="isRegistration"></param>
        /// <param name="config"></param>
        /// <returns>CustomerInfo</returns>
        private CustomerInfo buildMappingInfo(CustomerInfo customerInfo, bool isRegistration, CreateCustomerConfiguration config)
        {
            //step1: check the information
            if (customerInfo.Equals(null) || !customerInfo.DealerID.HasValue)
                return customerInfo;

            //step2: get dealaer information
            Log.InfoFormat("Calling GetDealerInfoByIdMS to get the DealerInfo with DealerId ({0}).", customerInfo.DealerID.Value);
            var getDealerInfoMs =
                MicroServiceManager.GetMicroService<GetDealerInfoByIdRequest, GetDealerInfoByIdResponse>();
            var getDealerInfoRequest = new GetDealerInfoByIdRequest()
            {
                DealerId = customerInfo.DealerID.Value
            };
            var getDealerInfoResponse = getDealerInfoMs.Process(getDealerInfoRequest, null);
            var dealerInfo = getDealerInfoResponse.DealerInfo;
            if(dealerInfo == null)
                return customerInfo;

            //step3: set information
            if (isRegistration)
            {
                customerInfo.MappingInfo.Clear();

                MappingInfo mappingInfo = new MappingInfo();
                mappingInfo.FiscalUnitId = dealerInfo.FiscalUnitID;
                mappingInfo.ReSellerId = dealerInfo.ResellerID;
                mappingInfo.AgentId = dealerInfo.AgentID;
                mappingInfo.SubAgentId = dealerInfo.SubagentID;
                mappingInfo.OrgId = config.MappingInfoOrgId;
                mappingInfo.OldId = config.MappingInfoOldId;
                mappingInfo.Stat1 = config.MappingInfoStatus;
                mappingInfo.Stat2 = config.MappingInfoStatus;
                mappingInfo.Stat3 = config.MappingInfoStatus;
                mappingInfo.Stat4 = config.MappingInfoStatus;
                mappingInfo.Stat5 = config.MappingInfoStatus;
                mappingInfo.Stat6 = config.MappingInfoStatus;
                mappingInfo.Stat7 = config.MappingInfoStatus;
                mappingInfo.CustomerInfo = customerInfo;

                customerInfo.MappingInfo.Add(mappingInfo);
            }
            else
            {
                if (customerInfo.MappingInfo == null) return customerInfo;
                foreach (MappingInfo item in customerInfo.MappingInfo)
                {
                    item.FiscalUnitId = dealerInfo.FiscalUnitID;
                    item.ReSellerId = dealerInfo.ResellerID;
                    item.AgentId = dealerInfo.AgentID;
                    item.SubAgentId = dealerInfo.SubagentID;
                }
            }

            return customerInfo;
        }

        #endregion private method
    }

    /// <summary>
    /// enumration of billing periode
    /// </summary>
    public enum BillingPeriode
    {
        /// <summary>
        /// default peride
        /// </summary>
        DefaultPeriode =1,
        /// <summary>
        /// FirstPeriode
        /// </summary>
        FirstPeriode = 8,
        /// <summary>
        /// Second periode
        /// </summary>
        SecondPeriode = 15,
        /// <summary>
        /// Third Periode
        /// </summary>
        ThirdPeriode = 22

    }
    /// <summary>
    /// DataRoaming Limit for customer
    /// </summary>
    public enum DataRoamingLimitType
    {
        /// <summary>
        /// not provide this service 
        /// </summary>
        Disabled = 0,
        /// <summary>
        /// Infinite  dataRoaming
        /// </summary>
        Infinite = -1,
        /// <summary>
        /// use the mvno default limit
        /// </summary>
        MvnoDefault = -2
    }
}
