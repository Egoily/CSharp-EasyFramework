using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.revenue.QueryInvoicesByCustomerId;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;

namespace com.etak.core.bizops.revenue.QueryInvoicesByMSISDN
{
    /// <summary>
    /// Gets last N invoices of a customer by customer id
    /// </summary>
    public class QueryInvoicesByMSISDNBizOp : 
        AbstractBusinessOperation<QueryInvoicesByMSISDNRequestDTO, QueryInvoicesByMSISDNResponseDTO,
                                    QueryInvoicesByMSISDNRequestInternal, QueryInvoicesByMSISDNResponseInternal>
    {
        /// <summary>
        /// Map not AutoMap inbound properties from QueryInvoicesByMSISDNRequestDTO to QueryInvoicesByMSISDNRequest
        /// </summary>
        /// <param name="dtoRequest">QueryInvoicesByMSISDNRequestDTO</param>
        /// <param name="coreInput">QueryInvoicesByMSISDNRequest</param>
        protected override void MapNotAutomappedInboundProperties(QueryInvoicesByMSISDNRequestDTO dtoRequest, ref QueryInvoicesByMSISDNRequestInternal coreInput)
        {
            coreInput.NumberOfInvoices = dtoRequest.NumberOfInvoices;
            if (coreInput.Subscription != null)
            {
            #region checkAuthorization
            var microServiceCheckAuthorization =
                MicroServiceManager.GetMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
            var checkAuthorizationRequest = new CheckAuthorizationRequest { UserInfo = coreInput.User, DealerId = coreInput.Subscription.OperatorInfo.DealerID != null ? coreInput.Subscription.OperatorInfo.DealerID.Value : 0 };
            var checkAuthorizationResponse = microServiceCheckAuthorization.Process(checkAuthorizationRequest, null);
            if (!checkAuthorizationResponse.IsAuthorized)
                throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);
            #endregion
            }			
        }

        /// <summary>
        /// Map not AutoMap outbound properties from QueryInvoicesByMSISDNResponse to QueryInvoicesByMSISDNResponseDTO
        /// </summary>
        /// <param name="coreOutput">QueryInvoicesByMSISDNResponse</param>
        /// <param name="dtoOutput">QueryInvoicesByMSISDNResponseDTO</param>
        protected override void MapNotAutomappedOutboundProperties(QueryInvoicesByMSISDNResponseInternal coreOutput, ref QueryInvoicesByMSISDNResponseDTO dtoOutput)
        {
            if (coreOutput.Invoices != null)
            {
                List<InvoiceDTO> invoiceDtos = new List<InvoiceDTO>();
                foreach (var keyValuePair in coreOutput.Invoices)
                {
                    var invoiceDTO = new InvoiceDTO
                    {
                        Amount = keyValuePair.Value.InformationalAmount ?? 0,
                        InvoiceId = keyValuePair.Key.Id,
                    };
                    if (keyValuePair.Key.GeneratingBillRun != null &&
                        keyValuePair.Key.GeneratingBillRun.BillingCycle != null)
                    {
                        invoiceDTO.BillingCycle = keyValuePair.Key.GeneratingBillRun.BillingCycle.Id;
                    }
                    invoiceDtos.Add(invoiceDTO);
                }

                dtoOutput.Invoices = invoiceDtos;
            }
        }

        /// <summary>
        /// Operation code of QueryInvoicesByMSISDN
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.QueryInvoicesByMSISDN; }
        }

        /// <summary>
        /// Operation discriminator of QueryInvoicesByMSISDN
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.QueryInvoicesByMSISDN; }
        }

        /// <summary>
        /// Business logic of QueryInvoicesByCustomerId Operation
        /// </summary>
        /// <param name="request">QueryInvoicesByMSISDNRequest</param>
        /// <param name="runningOperation"></param>
        /// <param name="invoker"></param>
        /// <returns>QueryInvoicesByMSISDNResponse as a internal output</returns>
        protected override QueryInvoicesByMSISDNResponseInternal ProcessBusinessLogic(QueryInvoicesByMSISDNRequestInternal request, BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            #region validateRequest
            if (request.Subscription == null)
                throw new BusinessLogicErrorException("Can't get the invoices, subscription not found",
                    BizOpsErrors.SubscriptionNotFound);

            if (request.Subscription.CustomerInfo == null)
                throw new BusinessLogicErrorException("Can't get the invoices, subscription does not have a customer",
                    BizOpsErrors.SubcriptionWithoutCustomer);

            CustomerInfo customerOfSubscription = request.Subscription.CustomerInfo;
            
            if (customerOfSubscription.CustomerID == null)
                throw new BusinessLogicErrorException("Can't get the invoices, customer not found",
                    BizOpsErrors.CustomerByIdNotFound);
            #endregion validateRequest

            QueryInvoicesByCustomerIdRequestInternal bizOpReq = new QueryInvoicesByCustomerIdRequestInternal
            {
                Channel = request.Channel,
                Customer = customerOfSubscription,
                MVNO = request.MVNO,
                NumberOfInvoices = request.NumberOfInvoices,
                User = request.User
            };

            var bizOp =
                BusinessOperationManager
                    .GetCoreBusinessOperation
                    <QueryInvoicesByCustomerIdRequestInternal, QueryInvoicesByCustomerIdResponseInternal>(
                        (int) request.MVNO.DealerID);

            
            var bizOpResp = bizOp.Process(bizOpReq, invoker);

            return new QueryInvoicesByMSISDNResponseInternal
            {
                ErrorCode = 0,
                Customer = customerOfSubscription,
                Invoices = bizOpResp.Invoices.ToList(),
                Message = String.Empty,
                ResultType = ResultTypes.Ok,
            };
        }
    }
}
