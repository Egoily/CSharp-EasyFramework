using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using com.etak.core.customer.message.GetCustomerChargeByCustomerIdAndInvoice;
using com.etak.core.customer.message.GetInvoicesByCustomerIdAndLegalInvoiceNumber;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using log4net;

namespace com.etak.core.bizops.revenue.QueryCustomerUnbilledCharges
{
    /// <summary>
    /// BizOp : Query Customer Unbilled Charges
    /// </summary>
    public class QueryCustomerUnbilledChargesBizOp : AbstractBusinessOperation<QueryCustomerUnbilledChargesRequestDTO, QueryCustomerUnbilledChargesResponseDTO, QueryCustomerUnbilledChargesRequestInternal, QueryCustomerUnbilledChargesResponseInternal>
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Map not automapped inbound properties, which is CustomerCharges
        /// </summary>
        /// <param name="dtoRequest">the dto request</param>
        /// <param name="coreInput">the core input</param>
        protected override void MapNotAutomappedInboundProperties(QueryCustomerUnbilledChargesRequestDTO dtoRequest, ref QueryCustomerUnbilledChargesRequestInternal coreInput)
        {
            //Check if the customer is null
            if (coreInput.Customer == null)
                throw new DataValidationErrorException(string.Format("The Customer ID {0} doesn't exists.", dtoRequest.CustomerId), BizOpsErrors.CustomerNotFound);
            //checking if this customerid doesn't belong to lowi
            if(coreInput.Customer.DealerID != coreInput.MVNO.DealerID)
                throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);
            
            #region checkAuthorization
            var microServiceCheckAuthorization =
                MicroServiceManager.GetMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
            var checkAuthorizationRequest = new CheckAuthorizationRequest { UserInfo = coreInput.User, DealerId = coreInput.Customer.DealerID != null ? coreInput.Customer.DealerID.Value : 0 };
            var checkAuthorizationResponse = microServiceCheckAuthorization.Process(checkAuthorizationRequest, null);
            if (!checkAuthorizationResponse.IsAuthorized)
                throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);
            #endregion

            #region Get Invoices by CustomerId and LegalInvoiceNumber
            var getInvoicesByCustomerIdAndLegalInvoiceNumberMS = MicroServiceManager
                .GetMicroService
                <GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest,
                    GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse>();
            var getInvoicesByCustomerIdAndLegalInvoiceNumberReq = new GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest
                ()
            {
                CustomerId = dtoRequest.CustomerId,
                LegalInvoiceNumber = null
            };
            var getInvoicesByCustomerIdAndLegalInvoiceNumberRes =
                getInvoicesByCustomerIdAndLegalInvoiceNumberMS.Process(getInvoicesByCustomerIdAndLegalInvoiceNumberReq, null);
            #endregion

            if (getInvoicesByCustomerIdAndLegalInvoiceNumberRes == null || getInvoicesByCustomerIdAndLegalInvoiceNumberRes.Invoices == null)
            {
                throw new DataValidationErrorException("Customer Invoices is null", BizOpsErrors.InvoicesIsNull);
            }

            #region Get CustomerCharges by CustomerId and LegalInvoiceNumber
            //Fill the core input
            coreInput.CustomerCharges = new List<CustomerCharge>();
            foreach (var invoice in getInvoicesByCustomerIdAndLegalInvoiceNumberRes.Invoices)
            {
                var getCustomerChargeByCustomerIdAndInvoiceMS =
                    MicroServiceManager
                        .GetMicroService
                        <GetCustomerChargeByCustomerIdAndInvoiceRequest, GetCustomerChargeByCustomerIdAndInvoiceResponse
                            >();
                var getCustomerChargeByCustomerIdAndInvoiceReq = new GetCustomerChargeByCustomerIdAndInvoiceRequest()
                {
                    CustomerId = dtoRequest.CustomerId,
                    Invoice = invoice
                };
                var getCustomerChargeByCustomerIdAndInvoiceRes = getCustomerChargeByCustomerIdAndInvoiceMS.Process(getCustomerChargeByCustomerIdAndInvoiceReq, null);
                coreInput.CustomerCharges.AddRange(getCustomerChargeByCustomerIdAndInvoiceRes.CustomerCharges.ToList());
            }
            #endregion
        }

        /// <summary>
        /// Convert the core CustomerCharges into CustomerChargesDTO
        /// </summary>
        /// <param name="coreOutput">the core output</param>
        /// <param name="dtoOutput">the dto output</param>
        protected override void MapNotAutomappedOutboundProperties(QueryCustomerUnbilledChargesResponseInternal coreOutput, ref QueryCustomerUnbilledChargesResponseDTO dtoOutput)
        {
            dtoOutput.CustomerCharges = new List<CustomerChargeDTO>();
            if (coreOutput.CustomerCharges!=null)
            {
                foreach (var customerCharge in coreOutput.CustomerCharges)
                {
                    dtoOutput.CustomerCharges.Add(customerCharge.ToDto());
                }
            }
        }

        /// <summary>
        /// Code that will be stored in the operation log for the operation
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.QueryCustomerUnbilledChargesOperation; }
        }

        /// <summary>
        /// Unique identifier of the operation
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.QueryCustomerUnbilledChargesOperation; }
        }

        /// <summary>
        /// Filter the customer charges that are not needed to be shown
        /// </summary>
        /// <param name="request">the core input</param>
        /// <param name="runningOperation">The trace of the operation</param>
        /// <param name="invoker">the environment of the invokation</param>
        /// <returns>QueryCustomerUnbilledChargesResponseInternal with the customer charges that have been filtered</returns>
        protected override QueryCustomerUnbilledChargesResponseInternal ProcessBusinessLogic(QueryCustomerUnbilledChargesRequestInternal request, BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            var listCustomerCharge = new List<CustomerCharge>();

            if (request.CustomerCharges != null)
            {
                foreach (var customerCharge in request.CustomerCharges)
                {
                    // Do not show products whose promotion are set to invisible
                    if (customerCharge.Product != null && customerCharge.Product.PurchasedProduct != null &&
                        customerCharge.Product.PurchasedProduct.AssociatedPrmotionPlan != null &&
                        customerCharge.Product.PurchasedProduct.AssociatedPrmotionPlan.APIVisible == 0)
                    {
                        //Do nothing
                    }
                    // If Charge is informative, don't show it
                    else if (customerCharge.ChargeDefinition != null &&
                             customerCharge.ChargeDefinition.InformationalOnly == InformationalTypes.Y)
                    {
                        //Do nothing
                    }
                    else
                    {
                        listCustomerCharge.Add(customerCharge);
                    }
                }
            }
            return new QueryCustomerUnbilledChargesResponseInternal()
            {
                CustomerCharges = listCustomerCharge,
                Subscription = request.Customer.ResourceMBInfo.FirstOrDefault(x => x.StatusID == (int)ResourceStatus.Active),
                ErrorCode = 0,
                Message = "Query Success",
                ResultType = ResultTypes.Ok
            };
        }
    }
}
