using System;
using com.etak.core.microservices.messages.GetTaxDefinitionById;
using com.etak.core.microservices.microservices;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.microservices.UniTests
{
    [TestFixture()]
    public class GetTaxDefinitionByIdMSUnitTests : AbstractMicroServiceTest<GetTaxDefinitionByIdMS, GetTaxDefinitionByIdRequest, GetTaxDefinitionByIdResponse>
    {
        private ITaxDefinitionRepository<TaxDefinition> mockedRepository;

        private TaxDefinition expectedResult = CreateDefaultObject.Create<TaxDefinition>();

        [TestFixtureSetUp()]
        public void InitializeSetup()
        {
            mockedRepository = MockRepository<ITaxDefinitionRepository<TaxDefinition>>();
            mockedRepository.GetById(100).Returns(expectedResult);
            mockedRepository.GetById(200).Returns(x => { throw new Exception("Error"); });
            mockedRepository.GetById(300).Returns((TaxDefinition) null);
        }

        [TestCase(100)]
        public void Process_CorrectTaxDefinitionId_ShouldReturnTaxDefinition(int testParameter)
        {
            
            var request = new GetTaxDefinitionByIdRequest()
            {
                TaxDefinitionId = testParameter,
            };

            var response = CallMicroservice(request);

            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            AssertExt.ObjectPropertiesAreEqual(expectedResult, response.TaxDefinition);
        }

        [TestCase(200)]
        public void Process_RepoThrowException_ShouldThrowException(int testParameter)
        {

            var request = new GetTaxDefinitionByIdRequest()
            {
                TaxDefinitionId = testParameter,
            };

            Assert.Throws<Exception>(() => CallMicroservice(request));
        }

        [TestCase(300)]
        public void Process_TaxDefinitionIdHasNoTaxDefinition_ShouldReturnEmptyTaxDefinition(int testParameter)
        {
            var request = new GetTaxDefinitionByIdRequest()
            {
                TaxDefinitionId = testParameter,
            };

            var response = CallMicroservice(request);

            AssertExt.IsNull(response.TaxDefinition);
            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            
        }

    }
}
