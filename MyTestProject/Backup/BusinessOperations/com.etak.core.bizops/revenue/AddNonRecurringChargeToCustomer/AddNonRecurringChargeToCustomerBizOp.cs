using System;
using System.Linq;
using com.etak.core.bizops.assurance.ApplyChargeAndSchedule;
using com.etak.core.customer.message.GetActiveCustomerAccountAssociationByDate;
using com.etak.core.customer.message.GetInvoicesByCustomerIdAndLegalInvoiceNumber;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.product.message.GetChargeById;

namespace com.etak.core.bizops.revenue.AddNonRecurringChargeToCustomer
{
    /// <summary>
    /// Create AddNonRecurringChargeToCustomerBizOp
    /// </summary>
    public class AddNonRecurringChargeToCustomerBizOp : AbstractSinglePhaseOrderProcessor<AddNonRecurringChargeToCustomerRequestDTO, AddNonRecurringChargeToCustomerResponseDTO, AddNonRecurringChargeToCustomerRequestInternal, AddNonRecurringChargeToCustomerResponseInternal, AddNonRecurringChargeToCustomerOrder>
    {
        /// <summary>
        /// Calling All Microservices that involve in AddnonRecurringChargeToCustomer and set to Internal Request Entity
        /// </summary>
        /// <param name="requestDTO">AddNonRecurringChargeToCustomerRequestDTO</param>
        /// <param name="coreInput">AddNonRecurringChargeToCustomerRequestInternal</param>
        protected override void MapNotAutomappedOrderInboundProperties(AddNonRecurringChargeToCustomerRequestDTO requestDTO, ref AddNonRecurringChargeToCustomerRequestInternal coreInput)
        {
            #region checkAuthorization
            var microServiceCheckAuthorization =
                MicroServiceManager.GetMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
            var checkAuthorizationRequest = new CheckAuthorizationRequest { UserInfo = coreInput.User, DealerId = coreInput.Customer.DealerID != null ? coreInput.Customer.DealerID.Value : 0 };
            var checkAuthorizationResponse = microServiceCheckAuthorization.Process(checkAuthorizationRequest, null);
            if (!checkAuthorizationResponse.IsAuthorized)
                throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);
            #endregion

            IMicroService<GetChargeByIdRequest, GetChargeByIdResponse> getChargeByIdMS =
                MicroServiceManager.GetMicroService<GetChargeByIdRequest, GetChargeByIdResponse>();
            var getChargeByIdMSReq = new GetChargeByIdRequest()
            {
                ChargeId = requestDTO.ChargeCatalogId
            };
            var chargeTemp = getChargeByIdMS.Process(getChargeByIdMSReq, null);

            if (chargeTemp == null)
            {
                throw new DataValidationErrorException(String.Format("Charge data cannot be found for catalog id: {0}", requestDTO.ChargeCatalogId), BizOpsErrors.ChargeNotFound);
            }
            coreInput.ChargeInfo = chargeTemp.Charge;

            var getInvoicesByCustomerIdAndLegalInvoiceNumberMS
                        =
                        MicroServiceManager
                            .GetMicroService
                            <GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest,
                                GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse>();

            
            
            var getInvoicesByCustomerIdAndLegalInvoiceNumberMDReq = new GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest
                ()
            {
                CustomerId = requestDTO.CustomerId,
                LegalInvoiceNumber = null
            };
            var invoiceTemp = getInvoicesByCustomerIdAndLegalInvoiceNumberMS.Process(
                getInvoicesByCustomerIdAndLegalInvoiceNumberMDReq, null);
            if (invoiceTemp.Invoices != null)
            {
                coreInput.InvoiceInfo = invoiceTemp.Invoices.FirstOrDefault();
            }
            else
            {
                throw new DataValidationErrorException(String.Format("Invoice data cannot be found with {0}", requestDTO.CustomerId), BizOpsErrors.InvoiceNotFound);
            }


            DateTime chargeDate;
            if (requestDTO.ChargeDate == null)
            {
                chargeDate = DateTime.Now;
            }
            else
            {
                chargeDate = (DateTime)requestDTO.ChargeDate;
            }

            #region Checking data exist
            if (coreInput.Customer == null)
            {
                throw new DataValidationErrorException(string.Format("It doesn't exist information for the CustomerID:{0}", requestDTO.CustomerId), BizOpsErrors.CustomerInfoByCustomerIdNotFound);
            }

            if (coreInput.Account == null)
            {
                throw new DataValidationErrorException(string.Format("Cannot find Account with AccountId {0}", requestDTO.AccountId), BizOpsErrors.AccountInfoNotFound);
            }
            if (requestDTO.ChargeDate < coreInput.InvoiceInfo.StartDate)
            {
                throw new DataValidationErrorException(string.Format("Cannot add a charge with datetime less than the Invoice start date ({0})", coreInput.InvoiceInfo.StartDate), BizOpsErrors.InvalidChargeDate);
            } 
            #endregion
            coreInput.Amount = requestDTO.Amount;
            coreInput.ChargeDate = chargeDate;

        }
        /// <summary>
        /// Convert internal request to DTO
        /// </summary>
        /// <param name="source">AddNonRecurringChargeToCustomerResponseInternal</param>
        /// <param name="coreOutput">AddNonRecurringChargeToCustomerResponseDTO</param>
        protected override void MapNotAutomappedOrderOutboundProperties(AddNonRecurringChargeToCustomerResponseInternal source, ref AddNonRecurringChargeToCustomerResponseDTO coreOutput)
        {
            if (source.CustomerCharge != null)
            {
                coreOutput.CustomerCharge = source.CustomerCharge.ToDto();
            }
            else
            {
                coreOutput.CustomerCharge = null;
            }
        }
        /// <summary>
        /// AddNonRecurringCharge to customer that calling ApplyChargeAndScheduleBizop
        /// </summary>
        /// <param name="order">AddNonRecurringChargeToCustomerOrder</param>
        /// <param name="Internalrequest">AddNonRecurringChargeToCustomerRequestInternal</param>
        /// <returns>AddNonRecurringChargeToCustomerResponseInternal</returns>
        public override AddNonRecurringChargeToCustomerResponseInternal ProcessRequest(AddNonRecurringChargeToCustomerOrder order, AddNonRecurringChargeToCustomerRequestInternal Internalrequest)
        {
            var response = new AddNonRecurringChargeToCustomerResponseInternal();
            var applyChargeAndScheduleBizOp =
                BusinessOperationManager
                   .GetCoreBusinessOperation
                   <ApplyChargeAndScheduleRequest, ApplyChargeAndScheduleResponse>((int)Internalrequest.MVNO.DealerID);
            //Set value to be send as request to ApplyChargeAndScheduleRequest
            var requestOperation = new ApplyChargeAndScheduleRequest()
            {
                Account = Internalrequest.Account,
                ChargeToAdd = Internalrequest.ChargeInfo,
                CustomAmount = Internalrequest.Amount,
                Customer = Internalrequest.Customer,
                CustomerProductAssignment = null,
                CycleNumber = null,
                InvoiceOfCharge = Internalrequest.InvoiceInfo,
                Schedule = null,
                StartDate = Internalrequest.ChargeDate,
                User = Internalrequest.User,
                MVNO = Internalrequest.MVNO,
            };
            
            var responseOperation = applyChargeAndScheduleBizOp.Process(requestOperation,null);
            response.CustomerCharge = responseOperation.ChargeAdde;
            return new AddNonRecurringChargeToCustomerResponseInternal()
            {
                ErrorCode = responseOperation.ErrorCode,
                ResultType = ResultTypes.Ok,
                Message = String.Empty,
                CustomerCharge = responseOperation.ChargeAdde,
                Subscription = Internalrequest.Customer.ResourceMBInfo.FirstOrDefault(x => x.StatusID == (int)ResourceStatus.Active)      
            };
        }
        /// <summary>
        /// Operation Code of AddNonRecurringChargeToCustomer
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.AddNonRecurringChargeToCustomerOperation; }
        }
        /// <summary>
        /// Operation Discriminator of AddNonRecurringChargeToCustomer
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.AddNonRecurringChargeToCustomerOperation; }
        }
        /// <summary>
        /// validation customer info and ActiveCustomer
        /// </summary>
        /// <param name="request">GetActiveCustomerAccountAssociationByDateRequest</param>
        private void validationOfRequestParameters(GetActiveCustomerAccountAssociationByDateRequest request)
        {
            if (request.CustomerInfo == null)
            {
                throw new DataValidationErrorException("The CustomerDefinition is mandatory!", BizOpsErrors.CustomerDefinitionNotFound);
            }
            if (request.ActiveCustomerAccountAssociationDate == null)
            {
                throw new DataValidationErrorException("The DateActiveAccount is mandatory!", BizOpsErrors.DateActiveAccountNotFound);
            }
        }


    }
}
