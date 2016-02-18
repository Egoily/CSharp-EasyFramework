using System;
using System.Collections.Generic;
using com.etak.core.microservices.messages.GetTaxDefinitionsByZipCodeLike;
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
    [TestFixture]
    public class GetTaxDefinitionsByZipCodeLikeMSUnitTests : AbstractMicroServiceTest<GetTaxDefinitionsByZipCodeLikeMS, GetTaxDefinitionsByZipCodeLikeRequest, GetTaxDefinitionsByZipCodeLikeResponse>
    {
        private ITaxDefinitionRepository<TaxDefinition> mockedRepository;

        [TestFixtureSetUp()]
        public void InitializeSetup()
        {
            mockedRepository = MockRepository<ITaxDefinitionRepository<TaxDefinition>>();
            mockedRepository.GetDefinitionsByZipCodeLike("100").Returns(new List<TaxDefinition>() { CreateDefaultObject.Create<TaxDefinition>() });
            mockedRepository.GetDefinitionsByZipCodeLike("200").Returns(x => { throw new Exception("Error"); });
            mockedRepository.GetDefinitionsByZipCodeLike("300").Returns(new List<TaxDefinition>());
        }

        [TestCase(100)]
        public void Process_CorrectZipCode_ShouldReturnTaxDefinitions(int testParameter)
        {
            var expectedObject = new List<TaxDefinition>() { CreateDefaultObject.Create<TaxDefinition>() };
            var request = new GetTaxDefinitionsByZipCodeLikeRequest()
            {
                ZipCode = testParameter.ToString(),
            };

            var response = CallMicroservice(request);

            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            AssertExt.ObjectPropertiesAreEqual(expectedObject, response.TaxDefinitions);
        }

        [TestCase(200)]
        public void Process_RepoThrowException_ShouldThrowException(int testParameter)
        {

            var request = new GetTaxDefinitionsByZipCodeLikeRequest()
            {
                ZipCode = testParameter.ToString(),
            };

            Assert.Throws<Exception>(() => CallMicroservice(request));
        }

        [TestCase(300)]
        public void Process_ZipCodeHasNoTaxDefinitions_ShouldReturnEmptyTaxDefinitions(int testParameter)
        {
            var request = new GetTaxDefinitionsByZipCodeLikeRequest()
            {
                ZipCode = testParameter.ToString(),
            };

            var response = CallMicroservice(request);

            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            AssertExt.IsEmpty(response.TaxDefinitions);
        }
    }
}
