using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.revenue.ChargeService.Proxy;
using com.etak.core.customer.message.GetAccountById;
using com.etak.core.customer.message.GetCustomerChargesScheduleById;
using com.etak.core.customer.message.GetCustomerInfoById;
using com.etak.core.customer.message.GetInvoiceById;
using com.etak.core.GSMSubscription.messages.GetCustomerProductAssignmentById;
using com.etak.core.microservices.messages.GetTaxDefinitionById;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.contract;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.product.message.GetChargeById;

namespace com.etak.core.bizops.revenue.ChargeService
{
    /// <summary>
    /// ChargeServiceBizOp
    /// </summary>
    public class ChargeServiceBizOp : AbstractBusinessOperation<ChargeServiceRequestDTO,ChargeServiceResponseDTO,ChargeServiceRequestInternal,ChargeServiceResponseInternal>
    {

        #region Public Dependency Injection by Property

        /// <summary>
        /// Service fro External API
        /// </summary>
        public IApplyRecurringChargeInterface _applyRecurringCharge { get; set; }

        #endregion

        #region DI Constructor

        /// <summary>
        /// Constructor parameterless
        /// </summary>
        public ChargeServiceBizOp()
        {
            _applyRecurringCharge = new IApplyRecurringCharge();
        }

        /// <summary>
        /// COsntructor with DI
        /// </summary>
        /// <param name="applyRecurringCharge"></param>
        public ChargeServiceBizOp(IApplyRecurringChargeInterface applyRecurringCharge)
        {
            _applyRecurringCharge = applyRecurringCharge;

        }

        #endregion

        #region Create MicroServices

        private IMicroService<GetChargeByIdRequest, GetChargeByIdResponse> _getChargeByIdMS =MicroServiceManager.GetMicroService<GetChargeByIdRequest, GetChargeByIdResponse>();
        private IMicroService<GetAccountByIdRequest, GetAccountByIdResponse> _getAccountByIdMs =MicroServiceManager.GetMicroService<GetAccountByIdRequest, GetAccountByIdResponse>();
        private IMicroService<GetCustomerInfoByIdRequest, GetCustomerInfoByIdResponse> _getCustomerInfoByIdMS = MicroServiceManager.GetMicroService<GetCustomerInfoByIdRequest, GetCustomerInfoByIdResponse>();
        private IMicroService<GetCustomerProductAssignmentByIdRequest, GetCustomerProductAssignmentByIdResponse> _getCustomerProductByIdMS = MicroServiceManager.GetMicroService<GetCustomerProductAssignmentByIdRequest, GetCustomerProductAssignmentByIdResponse>();
        private IMicroService<GetCustomerChargesScheduleByIdRequest, GetCustomerChargesScheduleByIdResponse> _getCustomerChargeScheduleByIdMS = MicroServiceManager.GetMicroService<GetCustomerChargesScheduleByIdRequest, GetCustomerChargesScheduleByIdResponse>();
        // TODO: awaiting Customer Version
        private IMicroService<GetInvoiceByIdRequest, GetInvoiceByIdResponse> _getInvoiceByIdMS = MicroServiceManager.GetMicroService<GetInvoiceByIdRequest, GetInvoiceByIdResponse>();
        

        // TODO: awaiting Framework version
        private IMicroService<GetTaxDefinitionByIdRequest, GetTaxDefinitionByIdResponse> _getTaxDefinitionByIdMS =MicroServiceManager.GetMicroService<GetTaxDefinitionByIdRequest, GetTaxDefinitionByIdResponse>();
            
        
        #endregion

        #region BizOP Implementation

        /// <summary>
        /// MapNotAutomappedInboundProperties
        /// </summary>
        /// <param name="dtoRequest"></param>
        /// <param name="coreInput"></param>
        protected override void MapNotAutomappedInboundProperties(ChargeServiceRequestDTO dtoRequest, ref ChargeServiceRequestInternal coreInput)
        {

            coreInput.CustomerChargeSchedule = _getCustomerChargeScheduleByIdMS.Process(new GetCustomerChargesScheduleByIdRequest() { customerChargeId = dtoRequest.CustomerChargeScheduleId }, null)
                .CustomerChargeSchedule;

            coreInput.Invoice = _getInvoiceByIdMS.Process(new GetInvoiceByIdRequest() { InvoiceId = (int) dtoRequest.Invoice.InvoiceId}, null).Invoice;
        }

        /// <summary>
        /// MapNotAutomappedOutboundProperties
        /// </summary>
        /// <param name="coreOutput"></param>
        /// <param name="dtoOutput"></param>
        protected override void MapNotAutomappedOutboundProperties(ChargeServiceResponseInternal coreOutput, ref ChargeServiceResponseDTO dtoOutput)
        {
            
        }

        /// <summary>
        /// OperationCode
        /// </summary>
        public override string OperationCode
        {
            get { return (OperationCodes.ChargeServiceOperation); }
        }

        /// <summary>
        /// OperationDiscriminator
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return (OperationDiscriminators.ChargeServiceOperation); }
        }
        

        /// <summary>
        /// ProcessBusinessLogic
        /// </summary>
        /// <param name="request"></param>
        /// <param name="runningOperation"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        protected override ChargeServiceResponseInternal ProcessBusinessLogic(ChargeServiceRequestInternal request, model.operation.BusinessOperationExecution runningOperation, operation.RequestInvokationEnvironment invoker)
        {
            #region Ini Var
            ChargeServiceResponseInternal response = new ChargeServiceResponseInternal();
            chargingRequestParameters param = null;
            if(!String.IsNullOrEmpty(request.Url) && request.Url.Trim().Length>0)
            {
                _applyRecurringCharge.Url = request.Url;
            }


            if (request.CustomerChargeSchedule.IsNull())
            {
                throw new DataValidationErrorException("CustomerChargeSchedule is null",BizOpsErrors.CustomerChargeScheduleIsNull);
            }

            param = new chargingRequestParameters()
            {
                accountId = request.CustomerChargeSchedule.ChargedAccount.Id,
                chargeId = request.CustomerChargeSchedule.ChargeDefinition.Id, //the charge to be applied
                customerChargeScheduleId = request.CustomerChargeSchedule.Id, //Indicates the schedule of recurring charge 
                customerId = request.CustomerChargeSchedule.Purchase.PurchasingCustomer.CustomerID.Value, //the customer to be charged
                customerProductId = request.CustomerChargeSchedule.Purchase.Id,
                cycleNumber = request.CustomerChargeSchedule.CurrentCyclenumber,
                periodNumber = request.CustomerChargeSchedule.NextPeriodNumber,
                startDate = request.datetimePurchase,
                startDateSpecified = true,
                chargeDate = request.datetimePurchase,
                chargeDateSpecified = true,
                endDateSpecified = false,
                priceEffectiveDateSpecified = false,
            };

            if (request.CustomerChargeSchedule.Purchase.EndDate != null)
            {
                param.endDate = request.CustomerChargeSchedule.Purchase.EndDate.Value;
                param.endDateSpecified = true;
            }
            if (request.datetimePriceEffective != null)
            {
                param.priceEffectiveDate = request.datetimePriceEffective;
                param.priceEffectiveDateSpecified = true;
            }

            #endregion


            #region Call to External API

            chargingApplyResponse result = _applyRecurringCharge.firstPeriodChargingApply(param);
            if (result.code == 0 && result.flag == 0)
            {
                // TODO: pending to fix in API
                result.nextChargeScheduleInfo.customerChargeScheduleId = param.customerChargeScheduleId;

                updateSchedule(request.CustomerChargeSchedule, result.nextChargeScheduleInfo);
                response.CustomerCharges = getList(result.customerChargedResultList, request.Invoice);
                response.Customer = request.CustomerChargeSchedule.Customer;
                response.Subscription = request.CustomerChargeSchedule.Customer.ResourceMBInfo.FirstOrDefault(x => x.StatusID == (int)ResourceStatus.Active);
            }
            else
            {
                int productID = 0;
                if (request.CustomerChargeSchedule.Purchase.PurchasedProduct != null)
                    productID = request.CustomerChargeSchedule.Purchase.PurchasedProduct.Id;

                throw new InternalErrorException("ApplyRecurringCharge API Exception. errorMsg:["
                                    + result.errorMsg + "], code:[" + result.code + "], flag:[" + result.flag + "], ProductID:[" + productID + "], Parameters:\n" + param.ToString(),result.code );
            }

            #endregion

            return response;
        }

        #endregion

        #region Private Methods

        private CustomerChargeSchedule updateSchedule(CustomerChargeSchedule customerChargeSchedule, nextChargeScheduleInfo rCustomerChargeSchedule)
        {
         
            // customerChargeSchedule.ChargeDefinition = _getChargeByIdMS.Process(new GetChargeByIdRequest() { ChargeId = rCustomerChargeSchedule.chargeId }, null).Charge;
                    
            //Charges,
            customerChargeSchedule.CreateDate = rCustomerChargeSchedule.createDate;
            customerChargeSchedule.CurrentCyclenumber = rCustomerChargeSchedule.currentCycleNumber;
            // customerChargeSchedule.Customer =_getCustomerInfoByIdMS.Process(new GetCustomerInfoByIdRequest() { CustomerId = rCustomerChargeSchedule.customerId }, null).CustomerInfo;
                    
            customerChargeSchedule.Id = rCustomerChargeSchedule.customerChargeScheduleId;
            customerChargeSchedule.NextChargeDate = rCustomerChargeSchedule.nextChargeDate;
            customerChargeSchedule.NextPeriodNumber = rCustomerChargeSchedule.nextPeriodNumber;

            if (rCustomerChargeSchedule.priceEffectiveDate == DateTime.MinValue)
                customerChargeSchedule.PriceEffectiveDate = null;
            else
                customerChargeSchedule.PriceEffectiveDate = rCustomerChargeSchedule.priceEffectiveDate;
            //CBO-198 Purchase is set to null. There was an error casting the Id to (int), causing that the microservice returns null as the Id was wrong. 
            //Anyway, as the purchased is created a few steps before, it's worthless to get it from the repo (again)
            //customerChargeSchedule.Purchase =_getCustomerProductByIdMS.Process(new GetCustomerProductAssignmentByIdRequest()  {  Id= rCustomerChargeSchedule.customerProductId }, null).CustomerProductAssignment;
                    
            customerChargeSchedule.Status = (ScheduleChargeStatus)rCustomerChargeSchedule.statusId;
            customerChargeSchedule.UpdateDate = rCustomerChargeSchedule.updateDate;
            return customerChargeSchedule;
            
        
        }

        private List<CustomerCharge> getList(customerChargeInfo[] rCustomersCharges, Invoice invoice)
        {
            
            List<CustomerCharge> customersCharges = new List<CustomerCharge>();
            for (int i = 0; i < rCustomersCharges.Length; ++i)
            {
                CustomerCharge item = new CustomerCharge()
                {
                    Amount = (decimal)rCustomersCharges[i].amount,
                    ChargeDefinition = _getChargeByIdMS.Process(new GetChargeByIdRequest() { ChargeId =rCustomersCharges[i].chargeId }, null).Charge,
                    ChargingAccount = _getAccountByIdMs.Process(new GetAccountByIdRequest() { AccountId = (int) rCustomersCharges[i].accountId}, null).Account,
                    ChargingDate = rCustomersCharges[i].chargeDate,
                    Currency = (ISO4217CurrencyCodes)Enum.Parse(typeof(ISO4217CurrencyCodes), rCustomersCharges[i].iso4217CurrencyCode),
                    Customer = _getCustomerInfoByIdMS.Process(new GetCustomerInfoByIdRequest() { CustomerId = rCustomersCharges[i].customerId }, null).CustomerInfo,
                    CycleNumber = rCustomersCharges[i].cycleNumber,
                    Id = rCustomersCharges[i].customerChargeId,
                    InformationalAmount = (decimal)rCustomersCharges[i].informationalAmount,
                    Invoice = invoice,
                    PeriodNumber = rCustomersCharges[i].periodNumber,
                    Product = _getCustomerProductByIdMS.Process(new GetCustomerProductAssignmentByIdRequest() { Id =(int) rCustomersCharges[i].customerProductId }, null).CustomerProductAssignment,
                    Schedule = _getCustomerChargeScheduleByIdMS.Process(new  GetCustomerChargesScheduleByIdRequest()  { customerChargeId = rCustomersCharges[i].customerChargeScheduleId },null ).CustomerChargeSchedule,
                            
                    // TODO: implement MS in framework
                    Tax = _getTaxDefinitionByIdMS.Process(new GetTaxDefinitionByIdRequest() { TaxDefinitionId = rCustomersCharges[i].taxId},null ).TaxDefinition
                        // repoTaxDefinition.GetById(rCustomersCharges[i].taxId)
                };
                customersCharges.Add(item);
            }

            return customersCharges;
            
        }

        #endregion

    }
}
