using System;
using System.Collections.Generic;
using com.etak.core.customer.message.CreateTroubleTicketInfo;
using com.etak.core.customer.message.GetTTQuestionInfoByTypeSubTypeAndMvnoid;
using com.etak.core.model;
using com.etak.core.model.dto;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.repository.crm.configuration;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using Newtonsoft.Json;
using NHibernate.Util;
using NSubstitute;
using NUnit.Framework;
using com.etak.core.bizops.fullfilment.CreateTroubleTicket;
using com.etak.core.repository;
using com.etak.core.repository.crm;


namespace com.etak.core.bizops.UnitTests.fullfilment.CreateTroubleTicket
{
    [TestFixture()]
    public class CreateTroubleTicketBizOpUnitTests : AbstractSinglePhaseOrderProcessorTest<CreateTroubleTicketBizOp, CreateTroubleTicketRequestDTO, CreateTroubleTicketResponseDTO, CreateTroubleTicketRequestInternal,CreateTroubleTicketResponseInternal,CreateTroubleTicketOrder>
    {
        private IMicroService<CreateTroubleTicketInfoRequest, CreateTroubleTicketInfoResponse> mockMicroServiceCreateTroubleTicket;

        private IMicroService<GetTTQuestionInfoByTypeSubTypeAndMvnoidRequest, GetTTQuestionInfoByTypeSubTypeAndMvnoidResponse> mockMicroServiceGetTTQuestion;

        private ISequenceProvider mockRepositoryGetSequence;

        [TestFixtureSetUp()]
        public void Initialize() {
            FakeSessionFactorySingleton.Init();
            mockMicroServiceCreateTroubleTicket = MockMicroServiceManager.GetMockedMicroService<CreateTroubleTicketInfoRequest, CreateTroubleTicketInfoResponse>();
            mockMicroServiceGetTTQuestion = MockMicroServiceManager.GetMockedMicroService<GetTTQuestionInfoByTypeSubTypeAndMvnoidRequest, GetTTQuestionInfoByTypeSubTypeAndMvnoidResponse>();
            mockRepositoryGetSequence = Substitute.For<ISequenceProvider>();
            RepositoryManager.RemapInterfaceToConstant<ISequenceProvider, ISequenceProvider>(mockRepositoryGetSequence);

        }
        [Test]
        public void CreateTroubleTicketBizOp_GivenTroubleTicketInfo_ReturnOkTroubleTicketInfo()
        {
            #region populate data for get TT trouble ticket

            var actualGetTTQuestionRequest = Arg.Is<GetTTQuestionInfoByTypeSubTypeAndMvnoidRequest>(x=> x.SubType == "test");
            var actualGetTTQuestionResponse = new GetTTQuestionInfoByTypeSubTypeAndMvnoidResponse();
            var actualTroubleTicketQuestionInfo = CreateDefaultObject.Create<TroubleTicketQuestionInfo>();
            actualTroubleTicketQuestionInfo.TTSUBTYPE = "test";
            actualGetTTQuestionResponse.TroubleTicketQuestionInfos = new List<TroubleTicketQuestionInfo>() { actualTroubleTicketQuestionInfo };
            mockMicroServiceGetTTQuestion.Process(actualGetTTQuestionRequest, null).Returns(actualGetTTQuestionResponse);


            mockRepositoryGetSequence.GetNextSequence(Arg.Any<String>()).Returns(1);

            #endregion

            #region mock create Trouble Ticket
            
            var actualCreateTTRequest = Arg.Is<CreateTroubleTicketInfoRequest>(tt => tt.TroubleTicketInfo.TTCODE == "test");
            //var actualCreateTTRequest = Arg.Any<CreateTroubleTicketInfoRequest>();
            var actualCreateTTResponse = CreateDefaultObject.Create<CreateTroubleTicketInfoResponse>();
            actualCreateTTResponse.TroubleTicketInfo.TTCODE = "test";
            mockMicroServiceCreateTroubleTicket.Process(actualCreateTTRequest, null).Returns(actualCreateTTResponse);
            
            #endregion

            
            var RequestDTO = new CreateTroubleTicketRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "12345678",
                priority = 1,
                subtype = "test",
                type = "test",
                troubleTicketDescription = "test trouble ticket"
            };

            MockAbstractSinglePhaseOrderProcessor(RequestDTO);
            var response = CallBizOp(RequestDTO);
            Assert.IsTrue(response.resultType == ResultTypes.Ok);
            Assert.IsNotNull(response.TroubleTicket);
        }
        [Test]
        public void CreateTroubleTicketBizOp_GivenTroubleTicketInfo_ReturnExceptionNullTroubleTicketInfo()
        {
            #region populate data for get TT trouble ticket

            var actualGetTTQuestionRequest = Arg.Is<GetTTQuestionInfoByTypeSubTypeAndMvnoidRequest>(x => x.SubType == "test");
            var actualGetTTQuestionResponse = new GetTTQuestionInfoByTypeSubTypeAndMvnoidResponse();
            var actualTroubleTicketQuestionInfo = CreateDefaultObject.Create<TroubleTicketQuestionInfo>();
            actualTroubleTicketQuestionInfo.TTSUBTYPE = "test";
            actualGetTTQuestionResponse.TroubleTicketQuestionInfos = new List<TroubleTicketQuestionInfo>() { actualTroubleTicketQuestionInfo };
            mockMicroServiceGetTTQuestion.Process(actualGetTTQuestionRequest, null).Returns(actualGetTTQuestionResponse);

            mockRepositoryGetSequence.GetNextSequence(Arg.Any<String>()).Returns(2);

            #endregion

            #region mock create Trouble Ticket

            var actualCreateTTRequest = Arg.Is<CreateTroubleTicketInfoRequest>(tt => tt.TroubleTicketInfo.TTCODE == "test");
            var actualCreateTTResponse = new CreateTroubleTicketInfoResponse();
            mockMicroServiceCreateTroubleTicket.Process(actualCreateTTRequest, null).Returns(actualCreateTTResponse);

            #endregion


            var RequestDTO = new CreateTroubleTicketRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "12345679",
                priority = 1,
                subtype = "test",
                type = "test",
                troubleTicketDescription = "test trouble ticket"
            };

            MockAbstractSinglePhaseOrderProcessor(RequestDTO);
            var response = CallBizOp(RequestDTO);
            Assert.IsTrue(response.resultType != ResultTypes.Ok);
            Assert.IsNull(response.TroubleTicket);
        }

        [Test]
        public void CreateTroubleTicketBizOp_GivenTroubleTicketInfo_ReturnNOKTroubleTicketInfo() {
            #region populate data for get TT trouble ticket

            var actualGetTTQuestionRequest = Arg.Is<GetTTQuestionInfoByTypeSubTypeAndMvnoidRequest>(x => x.SubType == "test");
            var actualGetTTQuestionResponse = new GetTTQuestionInfoByTypeSubTypeAndMvnoidResponse();
            var actualTroubleTicketQuestionInfo = CreateDefaultObject.Create<TroubleTicketQuestionInfo>();
            actualTroubleTicketQuestionInfo.TTSUBTYPE = "test";
            actualGetTTQuestionResponse.TroubleTicketQuestionInfos = new List<TroubleTicketQuestionInfo>() { actualTroubleTicketQuestionInfo };
            mockMicroServiceGetTTQuestion.Process(actualGetTTQuestionRequest, null).Returns(actualGetTTQuestionResponse);


            mockRepositoryGetSequence.GetNextSequence(Arg.Any<String>()).Returns(3);

            #endregion

            #region mock create Trouble Ticket

            var actualCreateTTRequest = Arg.Is<CreateTroubleTicketInfoRequest>(tt => tt.TroubleTicketInfo.TTCODE == "test");
            //var actualCreateTTRequest = Arg.Any<CreateTroubleTicketInfoRequest>();
            //var actualCreateTTResponse = CreateDefaultObject.Create<CreateTroubleTicketInfoResponse>();
            //actualCreateTTResponse.TroubleTicketInfo.TTCODE = "test";
            mockMicroServiceCreateTroubleTicket.Process(actualCreateTTRequest, null).Returns(x => { throw new Exception("Errror"); });

            #endregion


            var RequestDTO = new CreateTroubleTicketRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "12345677",
                priority = 1,
                subtype = "test",
                type = "test",
                troubleTicketDescription = "test trouble ticket"
            };

            MockAbstractSinglePhaseOrderProcessor(RequestDTO);
            var response = CallBizOp(RequestDTO);
            Assert.IsTrue(response.resultType == ResultTypes.UnknownError);
            
        }
    }
}
