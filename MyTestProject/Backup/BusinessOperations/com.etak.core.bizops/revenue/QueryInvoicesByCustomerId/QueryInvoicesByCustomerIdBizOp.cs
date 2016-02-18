using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.customer.message.GetLastNInvoicesByCustomerIdAndInvoiceStatuses;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;

namespace com.etak.core.bizops.revenue.QueryInvoicesByCustomerId
{
    /// <summary>
    /// Gets last N invoices of a customer by customer id
    /// </summary>
    public class QueryInvoicesByCustomerIdBizOp : 
        AbstractBusinessOperation<QueryInvoicesByCustomerIdRequestDTO, QueryInvoicesByCustomerIdResponseDTO,
                                    QueryInvoicesByCustomerIdRequestInternal, QueryInvoicesByCustomerIdResponseInternal>
    {
        /// <summary>
        /// Map not automapped inbound properties, which is Number Of Invoices
        /// </summary>
        /// <param name="dtoRequest">The DTO Request</param>
        /// <param name="coreInput">The Core Input</param>
        protected override void MapNotAutomappedInboundProperties(QueryInvoicesByCustomerIdRequestDTO dtoRequest, ref QueryInvoicesByCustomerIdRequestInternal coreInput)
        {
            coreInput.NumberOfInvoices = dtoRequest.NumberOfInvoices;
            if (coreInput.Customer != null)
            {
            #region checkAuthorization
            var microServiceCheckAuthorization =
                MicroServiceManager.GetMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
            var checkAuthorizationRequest = new CheckAuthorizationRequest { UserInfo = coreInput.User, DealerId = coreInput.Customer.DealerID != null ? coreInput.Customer.DealerID.Value : 0 };
            var checkAuthorizationResponse = microServiceCheckAuthorization.Process(checkAuthorizationRequest, null);
            if (!checkAuthorizationResponse.IsAuthorized)
                throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);
            #endregion
            }			
        }

        /// <summary>
        /// Convert the core Invoice into InvoiceDTO 
        /// </summary>
        /// <param name="coreOutput">The Core Output</param>
        /// <param name="dtoOutput">The DTO Output</param>
        protected override void MapNotAutomappedOutboundProperties(QueryInvoicesByCustomerIdResponseInternal coreOutput, ref QueryInvoicesByCustomerIdResponseDTO dtoOutput)
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
        /// Code that will be stored in the operation log for the operation
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.QueryInvoicesByCustomerId; }
        }

        /// <summary>
        /// Unique identifier of the operation
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.QueryInvoicesByCustomerId; }
        }

        /// <summary>
        /// Returning the response that has already been in core request/QueryInvoicesByCustomerIdRequestInternal
        /// </summary>
        /// <param name="request">The Core Input</param>
        /// <param name="runningOperation">The trace of the operation</param>
        /// <param name="invoker">The environment of the invokation</param>
        /// <returns>QueryInvoicesByCustomerIdResponse</returns>
        protected override QueryInvoicesByCustomerIdResponseInternal ProcessBusinessLogic(QueryInvoicesByCustomerIdRequestInternal request, BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            //Validate customer is correct
            if (request.Customer == null)
                throw new BusinessLogicErrorException("Can't get the invoices, customer not found", BizOpsErrors.CustomerByIdNotFound);
            
             if (request.Customer.CustomerID == null)
                throw new BusinessLogicErrorException("Can't get the invoices, customer not found", BizOpsErrors.CustomerByIdNotFound);

             var conf = GetOperationConfigForDealer<QueryInvoicesByCustomerIdConfiguration>(request.MVNO);

             if (conf == null)
                 throw new InternalErrorException("The operation QueryInvoicesByCustomerId needs operation configuration", BizOpsErrors.MissingOperationConfiguration);

            //Check the NumberOfInvoices and the NumberOfInvoices Configuration
             if (!request.NumberOfInvoices.HasValue && !conf.NumberOfInvoices.HasValue)
                 throw new BusinessLogicErrorException("There is no NumberOfInvoices Configuration", BizOpsErrors.NoNumberOfInvoicesConfiguration);
             else if (!request.NumberOfInvoices.HasValue && conf.NumberOfInvoices.HasValue) request.NumberOfInvoices = (int)conf.NumberOfInvoices;

            //Invoke get last N Invoices micro service
            GetLastNInvoicesByCustomerIdAndInvoiceStatusesRequest reqMS = new GetLastNInvoicesByCustomerIdAndInvoiceStatusesRequest
            {
                CustomerId = request.Customer.CustomerID.Value,
                InvoiceStatuses = new List<InvoiceStatus?> { InvoiceStatus.Ready },
                NumberOfInvoices = (int)request.NumberOfInvoices
            };
            
            IMicroService<GetLastNInvoicesByCustomerIdAndInvoiceStatusesRequest, GetLastNInvoicesByCustomerIdAndInvoiceStatusesResponse> ms =
                        MicroServiceManager.GetMicroService<GetLastNInvoicesByCustomerIdAndInvoiceStatusesRequest,GetLastNInvoicesByCustomerIdAndInvoiceStatusesResponse>();

            var respMS = ms.Process(reqMS, invoker);
            IList<KeyValuePair<Invoice, CustomerCharge>> invoices = new List<KeyValuePair<Invoice, CustomerCharge>>();
            if (respMS.Invoices != null)
            {
                foreach (var invoice in respMS.Invoices)
                {
                    KeyValuePair<Invoice, CustomerCharge> keyValue = new KeyValuePair<Invoice, CustomerCharge>
                        (
                            invoice,
                            invoice.Charges.FirstOrDefault(x => x.ChargeDefinition.Id == conf.AggregateTotalInvoiceChargeId)
                        );
                    invoices.Add(keyValue);
                }
            }

            return new QueryInvoicesByCustomerIdResponseInternal
            {
                ErrorCode = 0,
                Invoices = invoices,
                Message = String.Empty,
                Subscription = request.Customer.ResourceMBInfo.FirstOrDefault(),
                ResultType = ResultTypes.Ok,
            };
        }
    }
}
