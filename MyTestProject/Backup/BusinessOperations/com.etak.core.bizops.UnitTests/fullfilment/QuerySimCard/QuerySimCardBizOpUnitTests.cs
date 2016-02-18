using System;
using com.etak.core.bizops.fullfilment.QuerySimCard;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.operation.dtoConverters;
using com.etak.core.repository.crm;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;


namespace com.etak.core.bizops.UnitTests.fullfilment.QuerySimCard
{
    [TestFixture()]
    public class QuerySimCardBizOpUnitTests : AbstractBusinessOperationTest<QuerySimCardBizOp, QuerySimCardRequestDTO, QuerySimCardResponseDTO, QuerySimCardRequestInternal, QuerySimCardResponseInternal>
    {
        private IMicroService <CheckAuthorizationRequest, CheckAuthorizationResponse> mockMicroServiceCheckAuthorization;

        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            mockMicroServiceCheckAuthorization = MockMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
        }

        [Test()]
        public void QuerySimCardBizOp_ExistingIccid_ReturnSimCardInfo()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var expectedQuerySimCardResponseDTO = new QuerySimCardResponseDTO()
            {
                SimCard = CreateDefaultObject.Create<SIMCardInfo>().ToDto()
            };
            expectedQuerySimCardResponseDTO.SimCard.ICCID = "100";
            expectedQuerySimCardResponseDTO.SimCard.DealerID = 1;

            var querySimCardRequestDTO = new QuerySimCardRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                ICCID = expectedQuerySimCardResponseDTO.SimCard.ICCID
            };

            MockAbsctractBusinessOperation(querySimCardRequestDTO);

            var actualQuerySimCardResponseDTO = CallBizOp(querySimCardRequestDTO);
            AssertExt.ObjectPropertiesAreEqual(expectedQuerySimCardResponseDTO.SimCard, actualQuerySimCardResponseDTO.SimCard);
        }

        [Test()]
        public void QuerySimCardBizOp_ErrorIccid_ReturnException()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var querySimCardRequestDTO = new QuerySimCardRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                ICCID = "1"
            };

            MockAbsctractBusinessOperation(querySimCardRequestDTO);

            var simCardInfomockRepo = MockRepositoryManager.GetMockedRepository<ISIMCardInfoRepository<SIMCardInfo>>();
            simCardInfomockRepo.GetById(Arg.Any<string>()).Returns(x => { throw new Exception("Error"); });

            var queryCustomerByExternalIdBizOpResponseDTO = CallBizOp(querySimCardRequestDTO);
            Assert.IsTrue(queryCustomerByExternalIdBizOpResponseDTO.resultType == ResultTypes.UnknownError);
        }

        [Test()]
        public void QuerySimCardBizOp_WrongIccid_ReturnNullSimCardInfo()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var expectedQuerySimCardResponseDTO = new QuerySimCardResponseDTO()
            {
                SimCard = CreateDefaultObject.Create<SIMCardInfo>().ToDto()
            };
            expectedQuerySimCardResponseDTO.SimCard.ICCID = null;
            expectedQuerySimCardResponseDTO.SimCard.DealerID = 1;

            var querySimCardRequestDTO = new QuerySimCardRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                ICCID = "2"
            };

            MockAbsctractBusinessOperation(querySimCardRequestDTO);

            var simCardInfomockRepo = MockRepositoryManager.GetMockedRepository<ISIMCardInfoRepository<SIMCardInfo>>();
            simCardInfomockRepo.GetById(Arg.Any<string>()).Returns((SIMCardInfo)null);

            var queryCustomerByExternalIdBizOpResponseDTO = CallBizOp(querySimCardRequestDTO);
            Assert.IsNull(queryCustomerByExternalIdBizOpResponseDTO.SimCard);
            Assert.IsTrue(queryCustomerByExternalIdBizOpResponseDTO.resultType == ResultTypes.BussinessLogicError);
        }

        [Test()]
        public void QuerySimcardBizOp_NOK_authorizationfailed()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = false };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var requestDto = new QuerySimCardRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                ICCID = "1000000000"
            };

            MockAbsctractBusinessOperation(requestDto);

            var res = CallBizOp(requestDto);
            Assert.AreEqual(res.resultType, ResultTypes.AuthorizationError);
            Assert.AreEqual(res.errorCode, BizOpsErrors.AuthorizeErrorUser);
        }
    }
}