using System;
using System.Linq;
using System.Collections.Generic;
using com.etak.core.customer.message.GetInvoicesByCustomerIdAndLegalInvoiceNumber;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.customer.message.GetActiveProductsOfCustomer;
using com.etak.core.customer.microservices;
using com.etak.core.model.operation;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.manager;
using com.etak.core.model;

namespace com.etak.core.bizops.revenue.GetActiveProductsOfCustomer
{
    /// <summary>
    /// Operation for GetActiveProducsOfCustomer
    /// </summary>
    public class GetActiveProducsOfCustomerBizOp 
        : AbstractBusinessOperation<GetActiveProductsOfCustomerRequestDTO, GetActiveProductsOfCustomerResponseDTO, GetActiveProductsOfCustomerRequestInternal, GetActiveProductsOfCustomerResponseInternal>
    {       
        /// <summary>
        /// We don't need to map anything because all the properties are managed by the framework
        /// </summary>
        /// <param name="dtoRequest">the dto request</param>
        /// <param name="coreInput">the core request</param>
        protected override void MapNotAutomappedInboundProperties(GetActiveProductsOfCustomerRequestDTO dtoRequest, ref GetActiveProductsOfCustomerRequestInternal coreInput)
        {         
        }

        /// <summary>
        /// We don't need to map anything because all the properties are managed by the framework
        /// </summary>
        /// <param name="coreOutput">the core response</param>
        /// <param name="dtoOutput">the dto response</param>
        protected override void MapNotAutomappedOutboundProperties(GetActiveProductsOfCustomerResponseInternal coreOutput, ref GetActiveProductsOfCustomerResponseDTO dtoOutput)
        {            
            if (coreOutput.ResultType == ResultTypes.Ok)
            {
                dtoOutput.CustomerProductAssignments = new List<CustomerProductAssignmentDTO>();
                foreach (CustomerProductAssignment customerProduct in coreOutput.CustomerProductAssignments)
                {
                    (dtoOutput.CustomerProductAssignments as List<CustomerProductAssignmentDTO>).Add(customerProduct.ToDto());                    
                }
            }
        }

        /// <summary>
        /// Code that will be stored in the operation log for the operation
        /// </summary>
        public override string OperationCode
        { 
            get { return OperationCodes.QueryActiveProductsOfCustomerOperation; } 
        }

        /// <summary>
        /// Unique identifier of the operation
        /// </summary>
        public override string OperationDiscriminator 
        {
            get { return OperationDiscriminators.QueryActiveProductsOfCustomerOperation; } 
        }

        /// <summary>
        /// Business logic for GetActiveProducsOfCustomerBizOp
        /// </summary>
        /// <param name="request"></param>
        /// <param name="runningOperation"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        protected override GetActiveProductsOfCustomerResponseInternal ProcessBusinessLogic(GetActiveProductsOfCustomerRequestInternal request, BusinessOperationExecution runningOperation, operation.RequestInvokationEnvironment invoker)
        {
            #region Customer information validation

            if (request.Customer == null)
                throw new BusinessLogicErrorException("Can not find customer information.", BizOpsErrors.CustomerNotFound);

            // 20: logical deleted
            if (request.Customer.StatusID.HasValue && request.Customer.StatusID.Value == 20)
                throw new BusinessLogicErrorException("Customer was logical deleted.", BizOpsErrors.CustomerNotFound);

            PropertyInfo propertyInfo = request.Customer.PropertyInfo.FirstOrDefault(x => x.PendingStatus == (int)CustomerStatus.Active);

            if (propertyInfo == null)
                throw new BusinessLogicErrorException(string.Format("Can not find an active customer with CustomerId '{0}'.", request.Customer.CustomerID.Value), BizOpsErrors.CustomerIsNotActive);

            #endregion

            #region Invoke micro service to get all active products for customer

            IMicroService<GetActiveProductsOfCustomerRequest, GetActiveProductsOfCustomerResponse> microService = MicroServiceManager.GetMicroService<GetActiveProductsOfCustomerRequest, GetActiveProductsOfCustomerResponse>();

            GetActiveProductsOfCustomerRequest activeProductsOfCustomerRequest = new GetActiveProductsOfCustomerRequest()
            {
                Customer = request.Customer
            };

            GetActiveProductsOfCustomerResponse activeProductsOfCustomerResponse = microService.Process(activeProductsOfCustomerRequest, invoker);

            #endregion

            #region Gets configuration of this operation

            ActiveProductsConfiguration configuration = GetOperationConfigForDealer<ActiveProductsConfiguration>(request.MVNO);

            #endregion

            #region Gets really active products according to its charge type id

            // Gets specific active products
            IEnumerable<CustomerProductAssignment> specificActiveProducts = activeProductsOfCustomerResponse.CustomerProductAssignments
                .Where(p => p.PurchasedProduct.Id.In(configuration.SpecificProductsForActiveProducts.ToArray()));

            // Gets recurring active products
            IEnumerable<CustomerProductAssignment> recurringActiveProducts = (from product in activeProductsOfCustomerResponse.CustomerProductAssignments
                                                                              where product.PurchasedProduct.Id.NotIn(configuration.SpecificProductsForActiveProducts.ToArray())
                                                                              from charge in product.ProductChargePurchased.Charges
                                                                              where charge is ChargeRecurring
                                                                              select product);

            // Gets the first day of current month
            DateTime now = DateTime.Now;
            DateTime theFirstDayOfThisMonth = new DateTime(now.Year, now.Month, 1);

            #region Get Last Invoice to check billcycle

            var getInvoices =
                MicroServiceManager
                    .GetMicroService
                    <GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest,
                        GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse>();
            DateTime startDateBr = DateTime.Now;
            DateTime? endDateBr = DateTime.Now;

                var getInvoiceReq = new GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest()
                {
                    CustomerId = request.Customer.CustomerID.Value,
                    LegalInvoiceNumber = null,
                };
                var getLastInvoiceResp = getInvoices.Process(getInvoiceReq, null);

                if (getLastInvoiceResp.ResultType == ResultTypes.Ok && getLastInvoiceResp.Invoices.Any())
                {
                    startDateBr = getLastInvoiceResp.Invoices.OrderByDescending(x => x.StartDate).FirstOrDefault().StartDate;
                    endDateBr = getLastInvoiceResp.Invoices.OrderByDescending(x => x.StartDate).FirstOrDefault().EndDate;
                }
  
            #endregion

            // Gets non-recurring active products
            IEnumerable<CustomerProductAssignment> nonrecurringActiveProducts = (from product in activeProductsOfCustomerResponse.CustomerProductAssignments
                                                                                 where product.StartDate >= startDateBr && product.StartDate <= endDateBr
                                                                                 && product.PurchasedProduct.Id.NotIn(configuration.SpecificProductsForActiveProducts.ToArray())
                                                                                 from charge in product.ProductChargePurchased.Charges
                                                                                 where charge is ChargeNonRecurring
                                                                                 select product);
            //Get the datatransfer permission products
            IEnumerable<CustomerProductAssignment> dataTransferPermissionActiveProducts = (from product in activeProductsOfCustomerResponse.CustomerProductAssignments
                                                                                 where product.PurchasedProduct.Id.In(configuration.DataTransferPermissions.ToArray())
                                                                                 select product);

            #endregion

            #region Sets internal response to output

            GetActiveProductsOfCustomerResponseInternal response = new GetActiveProductsOfCustomerResponseInternal()
            {
                CustomerProductAssignments = specificActiveProducts.Union(recurringActiveProducts).Union(nonrecurringActiveProducts).Union(dataTransferPermissionActiveProducts).Distinct(),
                Subscription = request.Customer.ResourceMBInfo.FirstOrDefault(x => x.StatusID == (int)ResourceStatus.Active),
                ErrorCode = activeProductsOfCustomerResponse.ErrorCode,
                Message = string.IsNullOrEmpty(activeProductsOfCustomerResponse.Message) ? "OK" : activeProductsOfCustomerResponse.Message,
                ResultType = activeProductsOfCustomerResponse.ResultType
            };

            #endregion

            return response;
        }

    }

}
