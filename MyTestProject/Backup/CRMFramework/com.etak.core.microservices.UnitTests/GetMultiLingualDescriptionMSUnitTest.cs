using System;
using com.etak.core.microservices.messages.GetMultiLingualDescriptionById;
using com.etak.core.microservices.microservices;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.repository.crm;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.microservices.UniTests
{
    [TestFixture]
    public class GetMultiLingualDescriptionMSUnitTest : AbstractMicroServiceTest<GetMultiLingualDescriptionByIdMS, GetMultiLingualDescriptionByIdRequest, GetMultiLingualDescriptionByIdResponse>
    {
        private IMultiLingualDescriptionRepository<MultiLingualDescription> mockedRepo;

        [TestFixtureSetUp()]
        public void InitializeSetup()
        {
            mockedRepo = MockRepository<IMultiLingualDescriptionRepository<MultiLingualDescription>>();
            mockedRepo.GetById(100).Returns(CreateDefaultObject.Create<MultiLingualDescription>());
            mockedRepo.GetById(200).Returns(x => { throw new Exception("Error"); });
            mockedRepo.GetById(300).Returns((MultiLingualDescription)null);
        }

        [TestCase(100)]
        public void Process_CorrectMultiLingualDescriptionId_ShouldReturnMultiLingualDescription(int testParameter)
        {
            var expectedObject = CreateDefaultObject.Create<MultiLingualDescription>();
            var request = new GetMultiLingualDescriptionByIdRequest()
            {
                MultiLingualDescriptionId = testParameter,
            };

            var response = CallMicroservice(request);

            Assert.IsTrue(response.ResultType == ResultTypes.Ok);
            AssertExt.ObjectPropertiesAreEqual(expectedObject, response.MultiLingualDescription);

        }

        [TestCase(200)]
        public void Process_RepoThrowException_ShouldThrowException(int testParameter)
        {
            var request = new GetMultiLingualDescriptionByIdRequest()
            {
                MultiLingualDescriptionId = testParameter,
            };

            Assert.Throws<Exception>(() => CallMicroservice(request));
        }

        [TestCase(300)]
        public void Process_MultiLingualDescriptionIdHasNoMultiLingualDescription_ShouldReturnEmptyMultiLingualDescription(int testParameter)
        {
            var request = new GetMultiLingualDescriptionByIdRequest()
            {
                MultiLingualDescriptionId = testParameter,
            };

            var response = CallMicroservice(request);

            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            AssertExt.IsNull(response.MultiLingualDescription);
        }
    }
}
