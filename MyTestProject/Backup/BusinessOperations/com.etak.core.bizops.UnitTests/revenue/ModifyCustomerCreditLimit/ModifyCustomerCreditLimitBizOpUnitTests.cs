using System.Collections.Generic;
using com.etak.core.bizops.revenue.ModifyCustomerCreditLimit;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.subscription;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.repository.crm.customer;
using com.etak.core.service.messages.UpdateServicesInfoWithCustomCreditLimit;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NHibernate.Util;
using NSubstitute;
using NUnit.Framework;
using System.Linq;

namespace com.etak.core.bizops.UnitTests.revenue.ModifyCustomerCreditLimit
{
    [TestFixture]
    public class ModifyCustomerCreditLimitBizOpUnitTests : AbstractSinglePhaseOrderProcessorTest<ModifyCustomerCreditLimitBizOp, ModifyCustomerCreditLimitRequestDTO, ModifyCustomerCreditLimitResponseDTO, ModifyCustomerCreditLimitRequestInternal, ModifyCustomerCreditLimitResponseInternal, ModifyCustomerCreditLimitOrder>
    {
        private IMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse> mockMicroServiceCheckAuthorization;
        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            mockMicroServiceCheckAuthorization = MockMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
        }

        [Test()]
        public void ModifyCustomerCreditLimitBizOp_CorrectRequestGiven_CustomerCreditLimitModified()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion
            //Mock MS UpdateServicesInfoWithCustomCreditLimit
            var updateServicesInfoWithCustomCreditLimitMSMock =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <UpdateServicesInfoWithCustomCreditLimitRequest, UpdateServicesInfoWithCustomCreditLimitResponse>();
            var updateServicesInfoWithCustomCreditLimitRequest =
                Arg.Is<UpdateServicesInfoWithCustomCreditLimitRequest>(x => x.NewCreditLimit == 100);

            var servicesInfoReturned = CreateDefaultObject.Create<ServicesInfo>();
            servicesInfoReturned.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();
            servicesInfoReturned.CREDITLIMITBASEBUNDLEID = servicesInfoReturned.BundleDefinition.BundleID.Value;
            servicesInfoReturned.CreditLimit = 100;

            var updateServicesInfoWithCustomCreditLimitResponse = new UpdateServicesInfoWithCustomCreditLimitResponse()
            {
                ServicesInfo = servicesInfoReturned
            };

            updateServicesInfoWithCustomCreditLimitMSMock.Process(updateServicesInfoWithCustomCreditLimitRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(updateServicesInfoWithCustomCreditLimitResponse);

            var modifyCustomerCreditLimitRequestDTO = new ModifyCustomerCreditLimitRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 100,
                NewCreditLimit = 100
            };

            MockAbstractSinglePhaseOrderProcessor(modifyCustomerCreditLimitRequestDTO);

            //Remock customerinfo

            var mockCustomerRepo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            var mockedActualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            mockedActualCustomerInfo.PropertyInfo = new List<PropertyInfo>()
            {
                CreateDefaultObject.Create<PropertyInfo>()
            };
            mockedActualCustomerInfo.PropertyInfo.First().PendingStatus = (int)PendingStatus.Active;
            mockedActualCustomerInfo.CustomerID = modifyCustomerCreditLimitRequestDTO.CustomerId;
            mockedActualCustomerInfo.ResourceMBInfo.Clear();
            mockedActualCustomerInfo.ResourceMBInfo.Add(CreateDefaultObject.Create<ResourceMBInfo>());
            mockedActualCustomerInfo.ResourceMBInfo.First().CustomerInfo.CustomerID = modifyCustomerCreditLimitRequestDTO.CustomerId;
            mockedActualCustomerInfo.ServicesInfo = new List<ServicesInfo>()
            {
                servicesInfoReturned
            };
            mockCustomerRepo.GetById(mockedActualCustomerInfo.CustomerID.Value).Returns(mockedActualCustomerInfo);

            var expectedModifyCustomerCreditLimitResponseDTO = new ModifyCustomerCreditLimitResponseDTO()
            {
                Subscription = new SubscriptionDTO { CustomerId = modifyCustomerCreditLimitRequestDTO.CustomerId }
            };

            var actualModifyCustomerCreditLimitResponseDTO = CallBizOp(modifyCustomerCreditLimitRequestDTO);

            Assert.AreEqual(ResultTypes.Ok, actualModifyCustomerCreditLimitResponseDTO.resultType);
            Assert.AreEqual(expectedModifyCustomerCreditLimitResponseDTO.Subscription.CustomerId,actualModifyCustomerCreditLimitResponseDTO.Subscription.CustomerId);
        }

        [Test()]
        public void ModifyCustomerCreditLimitBizOp_NewCreditLimitIsLessThanZero_ShouldThrowDataValidationException()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion
            //Mock MS UpdateServicesInfoWithCustomCreditLimit
            var updateServicesInfoWithCustomCreditLimitMSMock =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <UpdateServicesInfoWithCustomCreditLimitRequest, UpdateServicesInfoWithCustomCreditLimitResponse>();
            var updateServicesInfoWithCustomCreditLimitRequest =
                Arg.Is<UpdateServicesInfoWithCustomCreditLimitRequest>(x => x.NewCreditLimit == -1100);

            var servicesInfoReturned = CreateDefaultObject.Create<ServicesInfo>();
            servicesInfoReturned.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();
            servicesInfoReturned.CREDITLIMITBASEBUNDLEID = servicesInfoReturned.BundleDefinition.BundleID.Value;

            var updateServicesInfoWithCustomCreditLimitResponse = new UpdateServicesInfoWithCustomCreditLimitResponse()
            {
                ServicesInfo = servicesInfoReturned
            };

            updateServicesInfoWithCustomCreditLimitMSMock.Process(updateServicesInfoWithCustomCreditLimitRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(updateServicesInfoWithCustomCreditLimitResponse);

            var modifyCustomerCreditLimitRequestDTO = new ModifyCustomerCreditLimitRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 100,
                NewCreditLimit = -1100
            };

            MockAbstractSinglePhaseOrderProcessor(modifyCustomerCreditLimitRequestDTO);

            //Remock Repo PropertyInfo
            var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            CustomerInfo customerInfo = CreateDefaultObject.Create<CustomerInfo>();
            customerInfo.ServicesInfo = new List<ServicesInfo>()
            {
                servicesInfoReturned
            };
            mockedRepoCustomerInfo.GetById(Arg.Any<int>()).Returns(customerInfo);

            var actualModifyCustomerCreditLimitResponseDTO = CallBizOp(modifyCustomerCreditLimitRequestDTO);

            Assert.AreEqual(ResultTypes.DataValidationError, actualModifyCustomerCreditLimitResponseDTO.resultType);
        }

        [Test()]
        public void ModifyCustomerCreditLimitBizOp_CustomerHasNoServicesInfo_ShouldThrowBusinessLogicException()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion
            //Mock MS UpdateServicesInfoWithCustomCreditLimit
            var updateServicesInfoWithCustomCreditLimitMSMock =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <UpdateServicesInfoWithCustomCreditLimitRequest, UpdateServicesInfoWithCustomCreditLimitResponse>();
            var updateServicesInfoWithCustomCreditLimitRequest =
                Arg.Is<UpdateServicesInfoWithCustomCreditLimitRequest>(x => x.NewCreditLimit == 100);

            var servicesInfoReturned = CreateDefaultObject.Create<ServicesInfo>();
            servicesInfoReturned.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();
            servicesInfoReturned.CREDITLIMITBASEBUNDLEID = servicesInfoReturned.BundleDefinition.BundleID.Value;

            var updateServicesInfoWithCustomCreditLimitResponse = new UpdateServicesInfoWithCustomCreditLimitResponse()
            {
                ServicesInfo = servicesInfoReturned
            };

            updateServicesInfoWithCustomCreditLimitMSMock.Process(updateServicesInfoWithCustomCreditLimitRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(updateServicesInfoWithCustomCreditLimitResponse);

            var modifyCustomerCreditLimitRequestDTO = new ModifyCustomerCreditLimitRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 100,
            };

            MockAbstractSinglePhaseOrderProcessor(modifyCustomerCreditLimitRequestDTO);

            //Remock Repo PropertyInfo
            var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            CustomerInfo customerInfo = CreateDefaultObject.Create<CustomerInfo>();
            customerInfo.ServicesInfo = new List<ServicesInfo>();
            mockedRepoCustomerInfo.GetById(Arg.Any<int>()).Returns(customerInfo);

            var actualModifyCustomerCreditLimitResponseDTO = CallBizOp(modifyCustomerCreditLimitRequestDTO);

            Assert.AreEqual(ResultTypes.BussinessLogicError, actualModifyCustomerCreditLimitResponseDTO.resultType);
        }

        [Test()]
        public void ModifyCustomerCreditLimitBizOp_CustomerHasNoServicesInfoToBeUpdated_ShouldThrowDataValidationException()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion
            //Mock MS UpdateServicesInfoWithCustomCreditLimit
            var updateServicesInfoWithCustomCreditLimitMSMock =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <UpdateServicesInfoWithCustomCreditLimitRequest, UpdateServicesInfoWithCustomCreditLimitResponse>();
            var updateServicesInfoWithCustomCreditLimitRequest =
                Arg.Is<UpdateServicesInfoWithCustomCreditLimitRequest>(x => x.NewCreditLimit == 100);

            var servicesInfoReturned = CreateDefaultObject.Create<ServicesInfo>();
            servicesInfoReturned.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();
            servicesInfoReturned.CREDITLIMITBASEBUNDLEID = servicesInfoReturned.BundleDefinition.BundleID.Value;

            var updateServicesInfoWithCustomCreditLimitResponse = new UpdateServicesInfoWithCustomCreditLimitResponse()
            {
                ServicesInfo = servicesInfoReturned
            };

            updateServicesInfoWithCustomCreditLimitMSMock.Process(updateServicesInfoWithCustomCreditLimitRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(updateServicesInfoWithCustomCreditLimitResponse);

            var modifyCustomerCreditLimitRequestDTO = new ModifyCustomerCreditLimitRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 100,
            };

            MockAbstractSinglePhaseOrderProcessor(modifyCustomerCreditLimitRequestDTO);

            //Remock Repo PropertyInfo
            var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            CustomerInfo customerInfo = CreateDefaultObject.Create<CustomerInfo>();
            servicesInfoReturned.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();
            servicesInfoReturned.CREDITLIMITBASEBUNDLEID = servicesInfoReturned.BundleDefinition.BundleID.Value + 1;
            customerInfo.ServicesInfo = new List<ServicesInfo>()
            {
                servicesInfoReturned
            };

            mockedRepoCustomerInfo.GetById(Arg.Any<int>()).Returns(customerInfo);

            var actualModifyCustomerCreditLimitResponseDTO = CallBizOp(modifyCustomerCreditLimitRequestDTO);

            Assert.AreEqual(ResultTypes.BussinessLogicError, actualModifyCustomerCreditLimitResponseDTO.resultType);
        }

        [Test()]
        public void ModifyCustomerCreditLimitBizOp_NOK_authorizationfailed()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = false };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var requestDto = new ModifyCustomerCreditLimitRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1000000000
            };

            MockAbsctractBusinessOperation(requestDto);

            var res = CallBizOp(requestDto);
            Assert.AreEqual(res.resultType, ResultTypes.AuthorizationError);
            Assert.AreEqual(res.errorCode, BizOpsErrors.AuthorizeErrorUser);
        }
    }
}