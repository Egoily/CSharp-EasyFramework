using System;
using com.etak.core.microservices.messages.CreateHLRRequestErrors;
using com.etak.core.microservices.microservices;
using com.etak.core.model.operation.messages;
using com.etak.core.model.provisioning;
using com.etak.core.repository.crm;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using Network3GPPModel;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.microservices.UniTests
{
    [TestFixture()]
    public class CreateHLRRequestErrorsMSUnitTest: AbstractMicroServiceTest<CreateHLRRequestErrorsMS, CreateHLRRequestErrorsRequest, CreateHLRRequestErrorsResponse>
    {
        private IHLRRequestErrorsRepository<HLRRequestErrors> _mockRepository;

        private readonly DateTime _dummyDate = new DateTime(2015, 04, 02);

        [TestFixtureSetUp()]
        public void InitailizeTest()
        {
            _mockRepository = MockRepository<IHLRRequestErrorsRepository<HLRRequestErrors>>();
        }
        [Test]
        public void CreateHLRRequestErrorsMS_CorrectRequestGiven_TestOk()
        {
            var expectedObj = CreateDummyHLRRequestErrors();
            
            _mockRepository.Create(expectedObj).Returns(expectedObj);
            
            var request = new CreateHLRRequestErrorsRequest()
            {
        
                HLRRequestErrorsObj = expectedObj
            };

            var response = CallMicroservice(request);
            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            AssertExt.ObjectPropertiesAreEqual(response.HLRRequestErrorsObj,expectedObj);
        }

        [Test]
        public void CreateHLRRequestErrorsMS_MockedErrorInput_ShouldThrownException()
        {
            var expectedObj = CreateDummyHLRRequestErrors();

            _mockRepository.Create(expectedObj).Returns(x => { throw new Exception("Error"); });

            var request = new CreateHLRRequestErrorsRequest()
            {
                HLRRequestErrorsObj = expectedObj
            };
            Assert.Throws<Exception>(() => CallMicroservice(request));
            
        }

        private HLRRequestErrors CreateDummyHLRRequestErrors()
        {
            var obj = CreateDefaultObject.Create<HLRRequestErrors>();
            obj.SEQID = 0;
            obj.QueueId = 0;
            obj.REPEAT = 0;
            obj.ResponseMessage = "test";
            obj.ERRORCODE = ((int)HLRErrorCodes.Ok).ToString();
            obj.CREATETIME = _dummyDate;
            obj.RequestMessage = "";
            obj.TypeName = null;
            return obj;
        }


    }
}
