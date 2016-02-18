using System;
using System.Collections.Generic;
using com.etak.core.bizops.revenue.QueryTransferences;
using com.etak.core.GSMSubscription.messages.GetLastSubscriptionByMsisdn;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.operation.dtoConverters;
using com.etak.core.repository.crm.configuration;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.subscription;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using com.etak.core.microservices.messages.GetSucessfulOperationExecutionForCustomer;
using NHibernate.Mapping;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.revenue.QueryTransferences
{
    [TestFixture]
    public class QueryTransferencesBizOpUnitTests :
       AbstractBusinessOperationTest<QueryTransferencesBizOp, QueryTransferencesRequestDTO, QueryTransferencesResponseDTO, QueryTransferencesRequestInternal, QueryTransferencesResponseInternal>
    {
        
        private static IMicroService<GetLastSubscriptionByMsisdnRequest, GetLastSubscriptionByMsisdnResponse> _getLastSubscriptionMs;
        
        [TestFixtureSetUp]
        public static void Initialize()
        {
            FakeSessionFactorySingleton.Init();
        }

        [TestCase]
        public void QueryTransferencesBizOp_OK()
        {
            var date = new DateTime(2015, 05, 08);

            var getSucessfulOperationExecutionForCustomerMS = MockMicroService<GetSucessfulOperationExecutionForCustomerRequest, GetSucessfulOperationExecutionForCustomerResponse>();

            var request = Arg.Is<GetSucessfulOperationExecutionForCustomerRequest>(x => x.Customer.CustomerID == 1 && x.StartDate == date && x.EndDate == date);          

            var response = new GetSucessfulOperationExecutionForCustomerResponse()
            {
                Operations = new List<BusinessOperationExecution> 
                { 
                    CreateDefaultObject.Create<BusinessOperationExecution>() 
                }
            };

            getSucessfulOperationExecutionForCustomerMS.Process(request, Arg.Any<RequestInvokationEnvironment>()).Returns(response);
            var requestDTO = new QueryTransferencesRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1,
                StartDate = date,
                EndDate = date                
            };

            MockAbsctractBusinessOperation(requestDTO);
            
            SetupMocksExpectedBehaviour();

            var responseDTO = CallBizOp(requestDTO);

            var expectedTransfer = CreateDefaultObject.Create<TransferenceExecutionDTO>();
            expectedTransfer.Operation = CreateDefaultObject.Create<BusinessOperationExecution>().ToDto();
            expectedTransfer.DonorMsisdn = "Resource1";
            expectedTransfer.ReceiverMsisdn = "Resource1";
            var expectedResponseDTO = new QueryTransferencesResponseDTO()
            {
                Transferences = new List<TransferenceExecutionDTO>()
                {
                    expectedTransfer
                }                
            };

            Assert.AreEqual(responseDTO.resultType, ResultTypes.Ok);
            Assert.IsNotEmpty(responseDTO.Transferences);
            AssertExt.ObjectPropertiesAreEqual(responseDTO.Transferences, expectedResponseDTO.Transferences);
            
        }

        [TestCase]
        public void QueryTransferencesBizOp_CustomerAndMsisdnAreNotProvided()
        {
            var date = new DateTime(2015, 05, 08);

            var getSucessfulOperationExecutionForCustomerMS = MockMicroService<GetSucessfulOperationExecutionForCustomerRequest, GetSucessfulOperationExecutionForCustomerResponse>();

            var request = Arg.Is<GetSucessfulOperationExecutionForCustomerRequest>(x => x.Customer == null && x.StartDate == date && x.EndDate == date);

            var response = new GetSucessfulOperationExecutionForCustomerResponse()
            {
                Operations = null
            };

            getSucessfulOperationExecutionForCustomerMS.Process(request, Arg.Any<RequestInvokationEnvironment>()).Returns(response);
            var requestDTO = new QueryTransferencesRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                StartDate = date,
                EndDate = date
            };

            MockAbsctractBusinessOperation(requestDTO);

            SetupMocksExpectedBehaviour();

            

            var responseDTO = CallBizOp(requestDTO);

            Assert.AreEqual(responseDTO.resultType, ResultTypes.BussinessLogicError);
            Assert.IsNotEmpty(responseDTO.messages);
            Assert.IsNull(responseDTO.Transferences);
        }

        [TestCase]
        public void QueryTransferencesBizOp_CustomerIsNotProvidedButMsisdnIsProvided()
        {
            var date = new DateTime(2015, 05, 08);

            var getSucessfulOperationExecutionForCustomerMS = MockMicroService<GetSucessfulOperationExecutionForCustomerRequest, GetSucessfulOperationExecutionForCustomerResponse>();

            var request = Arg.Is<GetSucessfulOperationExecutionForCustomerRequest>(x => x.Customer.CustomerID == 1 && x.StartDate == date && x.EndDate == date);

            var response = new GetSucessfulOperationExecutionForCustomerResponse()
            {
                Operations = new List<BusinessOperationExecution> 
                { 
                    CreateDefaultObject.Create<BusinessOperationExecution>() 
                }
            };

            getSucessfulOperationExecutionForCustomerMS.Process(request, Arg.Any<RequestInvokationEnvironment>()).Returns(response);
            var requestDTO = new QueryTransferencesRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                StartDate = date,
                EndDate = date,
                MSISDN = "600000000"
            };

            MockAbsctractBusinessOperation(requestDTO);

            SetupMocksExpectedBehaviour();
            
            var responseDTO = CallBizOp(requestDTO);

            var expectedTransfer = CreateDefaultObject.Create<TransferenceExecutionDTO>();
            expectedTransfer.Operation = CreateDefaultObject.Create<BusinessOperationExecution>().ToDto();
            expectedTransfer.DonorMsisdn = "Resource1";
            expectedTransfer.ReceiverMsisdn = "Resource1";
            var expectedResponseDTO = new QueryTransferencesResponseDTO()
            {
                Transferences = new List<TransferenceExecutionDTO>()
                {
                    expectedTransfer
                }
            };

            Assert.AreEqual(responseDTO.resultType, ResultTypes.Ok);
            Assert.IsNotEmpty(responseDTO.Transferences);
            AssertExt.ObjectPropertiesAreEqual(responseDTO.Transferences, expectedResponseDTO.Transferences);
        }

        [TestCase]
        public void QueryTransferencesBizOp_StartDateIsNotProvided()
        {
            var date = new DateTime(2015, 05, 08);

            var getSucessfulOperationExecutionForCustomerMS = MockMicroService<GetSucessfulOperationExecutionForCustomerRequest, GetSucessfulOperationExecutionForCustomerResponse>();

            var request = Arg.Is<GetSucessfulOperationExecutionForCustomerRequest>(x => x.Customer.CustomerID == 1 && x.StartDate == DateTime.MinValue && x.EndDate == date);

            var response = new GetSucessfulOperationExecutionForCustomerResponse()
            {
                Operations = null
            };

            getSucessfulOperationExecutionForCustomerMS.Process(request, Arg.Any<RequestInvokationEnvironment>()).Returns(response);
            var requestDTO = new QueryTransferencesRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1,
                StartDate = DateTime.MinValue,
                EndDate = date
            };

            MockAbsctractBusinessOperation(requestDTO);

            SetupMocksExpectedBehaviour();

            var responseDTO = CallBizOp(requestDTO);

            Assert.AreEqual(responseDTO.resultType, ResultTypes.BussinessLogicError);
            Assert.IsNotEmpty(responseDTO.messages);
            Assert.IsNull(responseDTO.Transferences);            
        }

        [TestCase]
        public void QueryTransferencesBizOp_EndDateIsNotProvided()
        {
            var date = new DateTime(2015, 05, 08);

            var getSucessfulOperationExecutionForCustomerMS = MockMicroService<GetSucessfulOperationExecutionForCustomerRequest, GetSucessfulOperationExecutionForCustomerResponse>();

            var request = Arg.Is<GetSucessfulOperationExecutionForCustomerRequest>(x => x.Customer.CustomerID == 1 && x.StartDate == date && x.EndDate == DateTime.MinValue);

            var response = new GetSucessfulOperationExecutionForCustomerResponse()
            {
                Operations = null
            };

            getSucessfulOperationExecutionForCustomerMS.Process(request, Arg.Any<RequestInvokationEnvironment>()).Returns(response);
            var requestDTO = new QueryTransferencesRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1,
                StartDate = date,
                EndDate = DateTime.MinValue
            };

            MockAbsctractBusinessOperation(requestDTO);

            SetupMocksExpectedBehaviour();

            var responseDTO = CallBizOp(requestDTO);

            Assert.AreEqual(responseDTO.resultType, ResultTypes.BussinessLogicError);
            Assert.IsNotEmpty(responseDTO.messages);
            Assert.IsNull(responseDTO.Transferences);
            
        }

        [TestCase]
        public void QueryTransferencesBizOp_EmptyOK()
        {
            var date = new DateTime(2015, 05, 08);

            var getSucessfulOperationExecutionForCustomerMS = MockMicroService<GetSucessfulOperationExecutionForCustomerRequest, GetSucessfulOperationExecutionForCustomerResponse>();

            var request = Arg.Is<GetSucessfulOperationExecutionForCustomerRequest>(x => x.Customer.CustomerID == 1 && x.StartDate == date && x.EndDate == date);

            var response = new GetSucessfulOperationExecutionForCustomerResponse()
            {
                Operations = new List<BusinessOperationExecution>()      
            };

            getSucessfulOperationExecutionForCustomerMS.Process(request, Arg.Any<RequestInvokationEnvironment>()).Returns(response);
            var requestDTO = new QueryTransferencesRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1,
                StartDate = date,
                EndDate = date
            };

            MockAbsctractBusinessOperation(requestDTO);

            SetupMocksExpectedBehaviour();

            var responseDTO = CallBizOp(requestDTO);

            var expectedResponseDTO = new QueryTransferencesResponseDTO()
            {
                Transferences = new List<TransferenceExecutionDTO>()
            };

            Assert.AreEqual(responseDTO.resultType, ResultTypes.Ok);        
            Assert.IsEmpty(responseDTO.Transferences);
            AssertExt.ObjectPropertiesAreEqual(responseDTO.Transferences, expectedResponseDTO.Transferences);
        }


        private void SetupMocksExpectedBehaviour()
        {
            #region Mock getLastSubscriptionMs
            _getLastSubscriptionMs = MockMicroServiceManager.GetMockedMicroService<GetLastSubscriptionByMsisdnRequest, GetLastSubscriptionByMsisdnResponse>();
            //The default response is ok with one resource
            //_getLastSubscriptionMs.Process(Arg.Is<GetLastSubscriptionByMsisdnRequest>(x => x.Msisdn == "100"), Arg.Any<RequestInvokationEnvironment>().Returns()
            var expectedResponse = new GetLastSubscriptionByMsisdnResponse()
            {
                ResultType = ResultTypes.Ok,
                ResourceMBInfo = new List<ResourceMBInfo>()
                {
                    CreateDefaultObject.Create<ResourceMBInfo>(),
                }
            };
            _getLastSubscriptionMs.Process(Arg.Any<GetLastSubscriptionByMsisdnRequest>(), Arg.Any<RequestInvokationEnvironment>()).Returns(expectedResponse);

            //If the Msisdn is null, should return an empty list
            var emptyResponse = new GetLastSubscriptionByMsisdnResponse()
            {
                ResultType = ResultTypes.Ok,
                ResourceMBInfo = new List<ResourceMBInfo>(),
            };
            _getLastSubscriptionMs.Process(Arg.Is<GetLastSubscriptionByMsisdnRequest>(x => string.IsNullOrEmpty(x.Msisdn)), Arg.Any<RequestInvokationEnvironment>()).Returns(emptyResponse);
            #endregion

            #region Mock Repositories
            //If the customer is not set, should return a null customer
            var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            mockedRepoCustomerInfo.GetById(Arg.Any<int>()).Returns(CreateDefaultObject.Create<CustomerInfo>());
            CustomerInfo customerInfo = null;
            mockedRepoCustomerInfo.GetById(0).Returns(customerInfo);


            //If MSISDN is null, should return an empty list
            var mockedGetByMsisdnRepo = MockRepositoryManager.GetMockedRepository<IResourceMBRepository<ResourceMBInfo>>();
            IList<ResourceMBInfo> expectedList = new List<ResourceMBInfo>()
            {
                CreateDefaultObject.Create<ResourceMBInfo>()
            };
            mockedGetByMsisdnRepo.GetByMSISDNAndStatusNotInAndActiveDates(Arg.Any<String>(), Arg.Any<IEnumerable<Int32>>()).Returns(expectedList);

            IList<ResourceMBInfo> emptyList = new List<ResourceMBInfo>();
            mockedGetByMsisdnRepo.GetByMSISDNAndStatusNotInAndActiveDates(null, Arg.Any<IEnumerable<Int32>>()).Returns(emptyList);
            
            #endregion
        }


    }
}
