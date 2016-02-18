using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.assurance.ApplyChargeAndSchedule;
using com.etak.core.bizops.revenue.AddNonRecurringChargeToCustomer;
using com.etak.core.customer.message.GetInvoicesByCustomerIdAndLegalInvoiceNumber;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;
using com.etak.core.operation;
using com.etak.core.repository.crm.customer;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;
using com.etak.core.operation.contract;
using com.etak.core.operation.manager;
using com.etak.core.product.message.GetChargeById;

namespace com.etak.core.bizops.UnitTests.revenue.AddNonRecurringChargeToCustomer
{
    [TestFixture()]
    public class AddNonRecurringChargeToCustomerBizOpUnitTests : AbstractSinglePhaseOrderProcessorTest<AddNonRecurringChargeToCustomerBizOp, AddNonRecurringChargeToCustomerRequestDTO, AddNonRecurringChargeToCustomerResponseDTO, AddNonRecurringChargeToCustomerRequestInternal, AddNonRecurringChargeToCustomerResponseInternal, AddNonRecurringChargeToCustomerOrder>
    {
        private DateTime date = DateTime.Now;
        private IMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse> mockMicroServiceCheckAuthorization;
        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            mockMicroServiceCheckAuthorization = MockMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
        }

        [Test()]
        public void AddNonRecurringChargeToCustomerBizOp_CorrectRequestGiven_ReturnCustomerCharge()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
               actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var getChargeByIdMS = MockMicroService<GetChargeByIdRequest, GetChargeByIdResponse>();
            var getChargeByIdMSRequest = Arg.Is<GetChargeByIdRequest>(x => x.ChargeId == 1000);
            var getChargeByIdMSResponse = new GetChargeByIdResponse()
            {
                Charge = CreateDefaultObject.Create<ChargeAggregate>()
            };
            getChargeByIdMS.Process(getChargeByIdMSRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(getChargeByIdMSResponse);

            var getInvoicesByCustomerIdAndLegalInvoiceNumberMSMock =
                MockMicroService
                    <GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest,
                        GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse>();

            var getInvoicesByCustomerIdAndLegalInvoiceNumberMSRequest =
                Arg.Is<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest>(x => x.CustomerId == 1000 && x.LegalInvoiceNumber == null);
            var getInvoicesByCustomerIdAndLegalInvoiceNumberMSResponse = new GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse
                ()
            {
                Invoices = new List<Invoice> {CreateDefaultObject.Create<Invoice>()}
            };

            getInvoicesByCustomerIdAndLegalInvoiceNumberMSMock.Process(
                getInvoicesByCustomerIdAndLegalInvoiceNumberMSRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(getInvoicesByCustomerIdAndLegalInvoiceNumberMSResponse);

            var addNonRecurringChargeToCustomerRequestDTO = new AddNonRecurringChargeToCustomerRequestDTO()
            {
                CustomerId = 1000,
                user = "00",
                password = "123456",
                vmno = "970100",
                Amount = 10,
                AccountId = 1000,
                ChargeCatalogId = 1000,
                ChargeDate = date,
            };
            var mockedApplyChargeAndScheduleBizOp = Substitute.For<ICoreBusinessOperation<ApplyChargeAndScheduleRequest, ApplyChargeAndScheduleResponse>>();
            mockedApplyChargeAndScheduleBizOp.Process(Arg.Any<ApplyChargeAndScheduleRequest>(), null)
                .Returns(new ApplyChargeAndScheduleResponse()
                {
                    ResultType = ResultTypes.Ok,
                    ChargeAdde = CreateDefaultObject.Create<CustomerCharge>(),
                });

            BusinessOperationManager.RebindCoreInterfaceToConstant(1, mockedApplyChargeAndScheduleBizOp);

            MockAbstractSinglePhaseOrderProcessor(addNonRecurringChargeToCustomerRequestDTO);

            var mockCustomerRepo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            var mockedActualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            mockedActualCustomerInfo.PropertyInfo = new List<PropertyInfo>()
            {
                CreateDefaultObject.Create<PropertyInfo>()
            };
            mockedActualCustomerInfo.PropertyInfo.First().PendingStatus = (int)PendingStatus.Active;
            mockedActualCustomerInfo.CustomerID = 1000;
            mockedActualCustomerInfo.ResourceMBInfo.Clear();
            mockedActualCustomerInfo.ResourceMBInfo.Add(CreateDefaultObject.Create<ResourceMBInfo>());
            mockedActualCustomerInfo.ResourceMBInfo.First().CustomerInfo.CustomerID = 1000;
            mockCustomerRepo.GetById(mockedActualCustomerInfo.CustomerID.Value).Returns(mockedActualCustomerInfo);


            var addNonRecurringChargeToCustomerResponseDTO = CallBizOp(addNonRecurringChargeToCustomerRequestDTO);
            var expectedaddNonRecurringChargeToCustomer = new AddNonRecurringChargeToCustomerResponseDTO()
            {
                CustomerCharge = CreateDefaultObject.Create<CustomerChargeDTO>(),
                Subscription = new SubscriptionDTO { CustomerId = 1000}
            };

            addNonRecurringChargeToCustomerResponseDTO.CustomerCharge.ChargeId = 1;
            addNonRecurringChargeToCustomerResponseDTO.CustomerCharge.ProductPurchaseId = 1;
            addNonRecurringChargeToCustomerResponseDTO.CustomerCharge.ReferenceCode = "ReferenceCode1";

            Assert.AreEqual(addNonRecurringChargeToCustomerResponseDTO.CustomerCharge.ChargeId, expectedaddNonRecurringChargeToCustomer.CustomerCharge.ChargeId);
            Assert.AreEqual(addNonRecurringChargeToCustomerResponseDTO.Subscription.CustomerId, expectedaddNonRecurringChargeToCustomer.Subscription.CustomerId);

            Assert.IsTrue(expectedaddNonRecurringChargeToCustomer.resultType == ResultTypes.Ok);


        }

        [Test()]
        public void AddNonRecurringChargeToCustomerBizOp_CorrectRequestGivenButNullCharge_ThrowErrorException()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
               actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var getChargeByIdMS = MockMicroService<GetChargeByIdRequest, GetChargeByIdResponse>();
            var getChargeByIdMSRequest = Arg.Is<GetChargeByIdRequest>(x => x.ChargeId == 1000);
            var getChargeByIdMSResponse = new GetChargeByIdResponse();

            getChargeByIdMS.Process(getChargeByIdMSRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(getChargeByIdMSResponse);


            var actualAddNonRecurringChargeToCustomerRequestDTO = new AddNonRecurringChargeToCustomerRequestDTO()
            {
                CustomerId = 3000,
                user = "00",
                password = "123456",
                vmno = "970100",
                Amount = 30,
                AccountId = 3000,
                ChargeCatalogId = 3000,
                ChargeDate = date,
            };

            MockAbstractSinglePhaseOrderProcessor(actualAddNonRecurringChargeToCustomerRequestDTO);

            var ActualAddNonRecurringChargeToCustomerResponseDTO = CallBizOp(actualAddNonRecurringChargeToCustomerRequestDTO);

            Assert.IsTrue(ActualAddNonRecurringChargeToCustomerResponseDTO.resultType == ResultTypes.DataValidationError);
            Assert.IsNull(ActualAddNonRecurringChargeToCustomerResponseDTO.CustomerCharge);
        }
        [Test()]
        public void AddNonRecurringChargeToCustomerBizOp_NOK_authorizationfailed()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = false };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var requestDto = new AddNonRecurringChargeToCustomerRequestDTO()
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
