using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.numberInfo;
using com.etak.core.model.operation.contract.product;
using com.etak.core.model.operation.contract.simcard;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.operation.implementation;
using com.etak.core.operation.util;
using com.etak.core.repository;
using com.etak.core.repository.crm;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.operation;
using com.etak.core.repository.crm.resource;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.crm.subscription;
using com.etak.core.repository.crm.subscription.catalog;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;

namespace com.etak.core.test.utilities.abstracts
{
    /// <summary>
    /// Used to create nunit BizOp
    /// </summary>
    /// <typeparam name="IBusinessOperation"></typeparam>
    /// <typeparam name="TDTOInternalInput"></typeparam>
    /// <typeparam name="TDTOInternalOutput"></typeparam>
    /// <typeparam name="TInternalInput"></typeparam>
    /// <typeparam name="TInternalOutput"></typeparam>
    public abstract class AbstractBusinessOperationTest<IBusinessOperation, TDTOInternalInput, TDTOInternalOutput, TInternalInput, TInternalOutput>
        where IBusinessOperation :
            AbstractBusinessOperation<TDTOInternalInput, TDTOInternalOutput, TInternalInput, TInternalOutput>, new()
        where TDTOInternalInput : RequestBaseDTO, new()
        where TDTOInternalOutput : ResponseBaseDTO, new()
        where TInternalInput : RequestBase, new()
        where TInternalOutput : ResponseBase, new()
    {
        /// <summary>
        /// Fake invoker to set in the BizOp.ProcessFromCustomerModel input parameter
        /// </summary>
        protected readonly RequestInvokationEnvironment FakeInvoker = Helpers.FakeInvoker.FakeInvokationEnvironment();

        /// <summary>
        /// Mock all the respositores used in AbstractBusinesOperation with the given RequestBaseDTO 
        /// </summary>
        /// <param name="requestDto"></param>
        public void MockAbsctractBusinessOperation(RequestBaseDTO requestDto)
        {
            var mockedBusinessOperation = MockRepositoryManager.GetMockedRepository<IBusinessOperationRepository<BusinessOperation>>();
            mockedBusinessOperation.GetById(Arg.Any<Int32>()).Returns((BusinessOperation) null);

            #region Mock AuthenticationHelper.Authenticate(request.user, request.password);
            var mockedRepoLoginInfo = MockRepositoryManager.GetMockedRepository<ILoginInfoRepository<LoginInfo>>();
            var actualLogInfos = new List<LoginInfo>();
            var actualLoginInfo = CreateDefaultObject.Create<LoginInfo>();
            actualLoginInfo.Password = MD5Utility.ComputeHash(requestDto.password);
            actualLoginInfo.Status = 0;
            actualLogInfos.Add(actualLoginInfo);
            mockedRepoLoginInfo.GetByUserId(Int32.Parse(requestDto.user.Trim())).Returns(actualLogInfos);
            #endregion

            #region Mock repositories used in AbstractBusinessOperation.GetDealerInfoByVmoRepo()
            var mockedRepoMVNOPropertiesInfo = MockRepositoryManager.GetMockedRepository<IMVNOPropertiesRepository<MVNOPropertiesInfo>>();
            var actualMVNOPropertiesInfo = CreateDefaultObject.Create<MVNOPropertiesInfo>();
            actualMVNOPropertiesInfo.DealerInfo = CreateDefaultObject.Create<DealerInfo>();
            var actualMVNOPropertiesInfos = new List<MVNOPropertiesInfo>();
            actualMVNOPropertiesInfos.Add(actualMVNOPropertiesInfo);
            mockedRepoMVNOPropertiesInfo.GetByVMNOId(requestDto.vmno).Returns(actualMVNOPropertiesInfos);
            #endregion

            #region Mock IBusinessOperationRepository used in AbstractBusinessOperation.Process() and  AbstractBusinessOperation.ProcessFromCustomerModel()
            var mockedRepoBusinessOperationExecution = MockRepositoryManager.GetMockedRepository<IBusinessOperationExecutionRepository<BusinessOperationExecution>>();
            mockedRepoBusinessOperationExecution.Create(CreateDefaultObject.Create<BusinessOperationExecution>());
            #endregion

            #region Mock all repositries used in AbstractBusinessOperation.AutoMapInboundFields()
            TInternalInput coreInput = new TInternalInput();

            ICustomerBasedRequest custRequest = coreInput as ICustomerBasedRequest;
            if (custRequest != null)
            {
                ICustomerIdBasedDTORequest custDtoRequest = requestDto as ICustomerIdBasedDTORequest;
                IDocumentIdBasedDTORequest custDocumentRequest = requestDto as IDocumentIdBasedDTORequest;
                if (custDtoRequest != null)
                {
                    var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
                    var actualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
                    actualCustomerInfo.CustomerID = custDtoRequest.CustomerId;
                    mockedRepoCustomerInfo.GetById(custDtoRequest.CustomerId).Returns(actualCustomerInfo);
                }
                else if (custDocumentRequest != null)
                {
                    var mockedRepoPropertyInfo = MockRepositoryManager.GetMockedRepository<IPropertyInfoRepository<PropertyInfo>>();
                    var actualPropertyInfos = new List<PropertyInfo>();
                    var actualPropertyInfo = CreateDefaultObject.Create<PropertyInfo>();
                    actualPropertyInfo.CustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
                    actualPropertyInfos.Add(actualPropertyInfo);
                    mockedRepoPropertyInfo.GetByDocumentId(custDocumentRequest.DocumentType,custDocumentRequest.DocumentNumber).Returns(actualPropertyInfos);
                }
                else
                {
                    throw new Exception("Request core model is ICustomerBasedRequest but the DTO did not have any" +
                                        "Customer information in the request in DTO model, this is a development error");
                }
            }

            IMultiCustomerRequestBased multiCustomerRequestBased = coreInput as IMultiCustomerRequestBased;
            if (multiCustomerRequestBased != null)
            {
                IExternalCustomerIdBasedDTORequest custExtIdRequest = requestDto as IExternalCustomerIdBasedDTORequest;
                if (custExtIdRequest != null)
                {
                    var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
                    var actualCustomerInfos = new List<CustomerInfo>();
                    var actualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
                    actualCustomerInfos.Add(actualCustomerInfo);
                    mockedRepoCustomerInfo.GetByExternalId(custExtIdRequest.ExternalCustomerId).Returns(actualCustomerInfos);
                }
            }

            ISubscriptionLastActiveBasedRequest subsRequest = coreInput as ISubscriptionLastActiveBasedRequest;
            if (subsRequest != null)
            {
                IMsisdnBasedDTORequest msdinBasedDTORequest = requestDto as IMsisdnBasedDTORequest;
                if (msdinBasedDTORequest != null)
                {
                    var mockedRepoResourceMBInfo = MockRepositoryManager.GetMockedRepository<IResourceMBRepository<ResourceMBInfo>>();
                    var actualResourceMBInfos = new List<ResourceMBInfo>();
                    var actualResourveMB = CreateDefaultObject.Create<ResourceMBInfo>();
                    actualResourveMB.Resource = msdinBasedDTORequest.MSISDN;
                    actualResourveMB.CustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
                    actualResourveMB.OperatorInfo = CreateDefaultObject.Create<DealerInfo>();
                    actualResourveMB.ResourceDIDInfo = CreateDefaultObject.Create<ResourceDIDInfo>();
                    actualResourceMBInfos.Add(actualResourveMB);
                    mockedRepoResourceMBInfo.GetByMSISDNAndStatusNotInAndActiveDates(msdinBasedDTORequest.MSISDN, Arg.Any<Int32[]>()).Returns(actualResourceMBInfos);
                }
                else
                {
                    throw new Exception("Request core model is ISubscriptionLastActiveBasedRequest but the DTO did not have any" +
                                         "msisdn information in the request in DTO model, this is a development error");
                }
            }

            IMultiSubscriptionBasedRequest multiSubsRequest = coreInput as IMultiSubscriptionBasedRequest;
            if (multiSubsRequest != null)
            {
                IMsisdnBasedDTORequest msdinBasedDTORequest = requestDto as IMsisdnBasedDTORequest;
                if (msdinBasedDTORequest != null)
                {
                    var mockedRepoResourceMBInfo = MockRepositoryManager.GetMockedRepository<IResourceMBRepository<ResourceMBInfo>>();
                    var actualResourceMBInfos = new List<ResourceMBInfo>();
                    var actualResourveMB = CreateDefaultObject.Create<ResourceMBInfo>();
                    actualResourveMB.Resource = msdinBasedDTORequest.MSISDN;
                    actualResourveMB.CustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
                    actualResourveMB.OperatorInfo = CreateDefaultObject.Create<DealerInfo>();
                    actualResourveMB.ResourceDIDInfo = CreateDefaultObject.Create<ResourceDIDInfo>();
                    actualResourceMBInfos.Add(actualResourveMB);
                    mockedRepoResourceMBInfo.GetByMSISDNAndStatusNotInAndActiveDates(msdinBasedDTORequest.MSISDN, Arg.Any<Int32[]>()).Returns(actualResourceMBInfos);
                }
                else
                {
                    throw new Exception("Request core model is IMultiSubscriptionBasedRequest but the DTO did not have any" +
                                         "msisdn information in the request in DTO model, this is a development error");
                }
            }

            ISimCardBasedRequest simRequest = coreInput as ISimCardBasedRequest;
            if (simRequest != null)
            {
                IICCIDBasedDTORequest iccidBasedDTORequest = requestDto as IICCIDBasedDTORequest;
                if (iccidBasedDTORequest != null)
                {
                    var mockedRepoSIMCardInfo = MockRepositoryManager.GetMockedRepository<ISIMCardInfoRepository<SIMCardInfo>>();
                    var actualSimCardInfo = CreateDefaultObject.Create<SIMCardInfo>();
                    actualSimCardInfo.ICCID = iccidBasedDTORequest.ICCID;
                    actualSimCardInfo.Dealer =  CreateDefaultObject.Create<DealerInfo>();
                    mockedRepoSIMCardInfo.GetById(iccidBasedDTORequest.ICCID).Returns(actualSimCardInfo);
                }
                else
                {
                    throw new Exception("Request core model is ISimCardBasedRequest but the DTO did not have any" +
                                      "sim information in the request in DTO model, this is a development error");
                }
            }

            INumberInfoBasedRequest numInfoBasedRequest = coreInput as INumberInfoBasedRequest;
            if (numInfoBasedRequest != null)
            {
                IMsisdnBasedDTORequest msdinBasedDTORequest = requestDto as IMsisdnBasedDTORequest;
                if (msdinBasedDTORequest != null)
                {
                    //INumberInfoBasedRequest
                    var mockedRepoNumberInfo =
                        MockRepositoryManager.GetMockedRepository<INumberInfoRepository<NumberInfo>>();
                    var actualNumberInfo = CreateDefaultObject.Create<NumberInfo>();
                    actualNumberInfo.NumberProperty = CreateDefaultObject.Create<NumberPropertyInfo>();
                    actualNumberInfo.Resource = msdinBasedDTORequest.MSISDN;
                    mockedRepoNumberInfo.GetById(msdinBasedDTORequest.MSISDN).Returns(actualNumberInfo);
                }
                else
                {
                    throw new Exception("Request core model is INumberInfoBasedRequest but the DTO did not have any" +
                                       "msisdn information in the request in DTO model, this is a development error");
                }
            }

            IJointCustomerBasedRequest jointCustomer = coreInput as IJointCustomerBasedRequest;
            if (jointCustomer != null)
            {
                IJointCustomerIdDTOBasedRequest jointCustIdDtoRequest = requestDto as IJointCustomerIdDTOBasedRequest;
                if (jointCustIdDtoRequest != null)
                {
                    var mockedRepoCustomerInfo =
                        MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
                    var actualSourceCustomer = CreateDefaultObject.Create<CustomerInfo>();
                    actualSourceCustomer.CustomerID = jointCustIdDtoRequest.SourceCustomerId;
                    mockedRepoCustomerInfo.GetById(jointCustIdDtoRequest.SourceCustomerId).Returns(actualSourceCustomer);

                    var actualDestinationCustomer = CreateDefaultObject.Create<CustomerInfo>();
                    actualDestinationCustomer.CustomerID = jointCustIdDtoRequest.DestinationCustomerId;
                    mockedRepoCustomerInfo.GetById(jointCustIdDtoRequest.DestinationCustomerId).Returns(actualDestinationCustomer);
                }
                else
                {
                    throw new Exception("Request core model is IJointCustomerBasedRequest but the DTO did not have any" +
                                          "customerId information in the request in DTO model, this is a development error");
                }

            }

            IJointSubscriptionBasedRequest jointSubscription = coreInput as IJointSubscriptionBasedRequest;
            if (jointSubscription != null)
            {
                IJointMsisdnDTOBasedRequest jointMsisdnDtoRequest = requestDto as IJointMsisdnDTOBasedRequest;
                if (jointMsisdnDtoRequest != null)
                {
                    var mockedRepoResourceMBInfo = MockRepositoryManager.GetMockedRepository<IResourceMBRepository<ResourceMBInfo>>();
                    var actualSourceSubscription = CreateDefaultObject.Create<ResourceMBInfo>();
                    actualSourceSubscription.Resource = jointMsisdnDtoRequest.SourceMSISDN;
                    actualSourceSubscription.CustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
                    actualSourceSubscription.OperatorInfo = CreateDefaultObject.Create<DealerInfo>();
                    actualSourceSubscription.ResourceDIDInfo = CreateDefaultObject.Create<ResourceDIDInfo>();
                    var actualSourceSubscriptionList = new List<ResourceMBInfo>();
                    actualSourceSubscriptionList.Add(actualSourceSubscription);

                    mockedRepoResourceMBInfo.GetByMSISDNAndStatusNotInAndActiveDates(jointMsisdnDtoRequest.SourceMSISDN, Arg.Any<Int32[]>()).Returns(actualSourceSubscriptionList);

                    var actualDestinationSubscription = CreateDefaultObject.Create<ResourceMBInfo>();
                    actualDestinationSubscription.Resource = jointMsisdnDtoRequest.DestinationMSISDN;
                    actualDestinationSubscription.CustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
                    actualDestinationSubscription.OperatorInfo = CreateDefaultObject.Create<DealerInfo>();
                    actualDestinationSubscription.ResourceDIDInfo = CreateDefaultObject.Create<ResourceDIDInfo>();
                    var actualDestinationSubscriptionList = new List<ResourceMBInfo>();
                    actualDestinationSubscriptionList.Add(actualDestinationSubscription);
                    mockedRepoResourceMBInfo.GetByMSISDNAndStatusNotInAndActiveDates(jointMsisdnDtoRequest.DestinationMSISDN,Arg.Any<Int32[]>()).Returns(actualDestinationSubscriptionList);
                }
                else
                {
                    throw new Exception("Request core model is IJointSubscriptionBasedRequest but the DTO did not have any" +
                                            "customerId information in the request in DTO model, this is a development error");

                }
            }

            IAccountBasedRequest accountBasedRequest = coreInput as IAccountBasedRequest;
            if (accountBasedRequest != null)
            {
                 IAccountIdBasedDTORequest accountBasedDtoRequest = requestDto as IAccountIdBasedDTORequest;
                if (accountBasedDtoRequest != null)
                {
                    var mockedRepoAccount = MockRepositoryManager.GetMockedRepository<IAccountRepository<Account>>();
                    var actualAccount = CreateDefaultObject.Create<Account>();
                    actualAccount.Balance = CreateDefaultObject.Create<BalanceForAccount>();
                    actualAccount.BillingCycle = CreateDefaultObject.Create<BillCycle>();
                    actualAccount.Balance = CreateDefaultObject.Create<BalanceForAccount>();
                    actualAccount.CurrentAsignedCustomer = CreateDefaultObject.Create<CustomerInfo>();
                    actualAccount.Description = CreateDefaultObject.Create<MultiLingualDescription>();
                    actualAccount.LastBillRun = CreateDefaultObject.Create<BillRun>();
                    actualAccount.Name = CreateDefaultObject.Create<MultiLingualDescription>();
                    
                    mockedRepoAccount.GetById(accountBasedDtoRequest.AccountId).Returns(actualAccount);

                }
                else
                {
                    throw new Exception("Request core model is IAccountBasedRequest but the DTO did not have any" +
                                        "accountId information in the request in DTO model, this is a development error");
                }

            }
            #endregion


            #region Mock ProductOffering group as it could be executed in ToDto method
            var mockedProductOfferingRepo = MockRepositoryManager.GetMockedRepository<IProductOfferingRepository<ProductOffering>>();
            mockedProductOfferingRepo.GetByGroupId(Arg.Any<int>()).ReturnsForAnyArgs(new List<ProductOffering>());
            #endregion


        }

        /// <summary>
        /// Utility to call MockMicroServiceManager
        /// </summary>
        /// <typeparam name="TInput">The type of the input request extends TInput</typeparam>
        /// <typeparam name="TOutput">The type of the output response extends TOutput</typeparam>
        /// <returns>Mocked microservice</returns>
        public IMicroService<TInput, TOutput> MockMicroService< TInput, TOutput>()
            where TInput : RequestBase
            where TOutput : ResponseBase
        {
            var mockedMicroService = MockMicroServiceManager.GetMockedMicroService< TInput, TOutput>();
            return mockedMicroService;
        }

        /// <summary>
        /// Call the BizOp
        /// </summary>
        /// <param name="request"></param>
        /// <returns>BizOp response</returns>
        public virtual TDTOInternalOutput CallBizOp(TDTOInternalInput request)
        {
            var response = new TDTOInternalOutput();
            using (var conn = RepositoryManager.GetNewConnection())
            {
                IBusinessOperation bizop = new IBusinessOperation();

                response = bizop.ProcessFromCustomerModel(new NullValidator<TDTOInternalInput>(),new SameTypeConverter<TDTOInternalInput>(),
                    new SameTypeConverter<TDTOInternalOutput>(),request, FakeInvoker);
                
            }
            RepositoryManager.CloseConnection();
            return response;
        }
    }
}
