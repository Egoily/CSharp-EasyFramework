using System;
using System.Collections.Generic;
using com.etak.core.microservices.messages.GetSucessfulOperationExecutionForCustomer;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.repository;
using com.etak.core.repository.crm.operation;

namespace com.etak.core.microservices.microservices
{
    /// <summary>
    /// Gets all the Ok operations for the customer, within a time range and a type of operation
    /// 
    /// </summary>
    public class GetSucessfulOperationExecutionForCustomerMS : IMicroService<GetSucessfulOperationExecutionForCustomerRequest, GetSucessfulOperationExecutionForCustomerResponse>
    {

        /// <summary>
        /// Main process for the microservice
        /// </summary>
        /// <param name="request">Request with an the input parameters</param>
        /// <param name="invoker">the request invokation environment</param>
        /// <returns>The list of matching operations</returns>
        public GetSucessfulOperationExecutionForCustomerResponse Process(GetSucessfulOperationExecutionForCustomerRequest request, operation.RequestInvokationEnvironment invoker)
        {
            if (request.Customer == null)
            {
                throw new BusinessLogicErrorException("Can't get the operations of a customer if customer is not provided", CoreMicroServicesErrorCodes.CustomerNotProvided);    
            }

            IBusinessOperationExecutionRepository<BusinessOperationExecution> bizOpRepo = RepositoryManager.GetRepository<IBusinessOperationExecutionRepository<BusinessOperationExecution>>();

            String operationDiscriminator = request.OperationDiscriminator;
            DateTime startDate = request.StartDate;
            DateTime endDate = request.EndDate;
            CustomerInfo customer = request.Customer;
            IEnumerable<BusinessOperationExecution> bizOps = bizOpRepo.GetCustomerOperationsBetweenDates(customer, operationDiscriminator, startDate, endDate, new[] {ResultTypes.Ok});

            return new GetSucessfulOperationExecutionForCustomerResponse
            {
                ErrorCode = 0,
                Message = String.Empty,
                ResultType = ResultTypes.Ok,
                Operations = bizOps
            };
        }
    }
}
