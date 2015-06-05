using System;
using System.Collections.Generic;
using com.etak.core.microservices.messages.GetTaxDefinitonsByCategory;
using com.etak.core.microservices.microservices;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.microservices.unittest
{
    [TestFixture]
    public class GetTaxDefinitionByCategoryMSUnitTests : AbstractMicroServiceTest<GetTaxDefinitonsByCategoryMS, GetTaxDefinitonsByCategoryRequest, GetTaxDefinitonsByCategoryResponse>
    {
        private ITaxDefinitionRepository<TaxDefinition> mockedRepository;

        [TestFixtureSetUp()]
        public void InitializeSetup()
        {
            mockedRepository = MockRepository<ITaxDefinitionRepository<TaxDefinition>>();
            mockedRepository.GetDefinitionsForCategory(100).Returns(new List<TaxDefinition>() { CreateDefaultObject.Create<TaxDefinition>() });
            mockedRepository.GetDefinitionsForCategory(200).Returns(x => { throw new Exception("Error"); });
            mockedRepository.GetDefinitionsForCategory(300).Returns(new List<TaxDefinition>());
        }

        [TestCase(100)]
        public void GetTaxDefinitionByCategoryMSOk(int testParameter)
        {
            var expectedObject = new List<TaxDefinition>() { CreateDefaultObject.Create<TaxDefinition>() };
            var request = new GetTaxDefinitonsByCategoryRequest()
            {
                TaxCategory = testParameter,
            };

            var response = CallMicroservice(request);

            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            AssertExt.ObjectPropertiesAreEqual(expectedObject, response.TaxDefinitions);
        }

        [TestCase(200)]
        public void GetTaxDefinitionByCategoryMSNOk(int testParameter)
        {

            var request = new GetTaxDefinitonsByCategoryRequest()
            {
                TaxCategory = testParameter,
            };

            Assert.Throws<Exception>(() => CallMicroservice(request));
        }

        [TestCase(300)]
        public void GetDealerInfoByIdMSNOkNull(int testParameter)
        {
            var request = new GetTaxDefinitonsByCategoryRequest()
            {
                TaxCategory = testParameter,
            };

            var response = CallMicroservice(request);

            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            AssertExt.IsEmpty(response.TaxDefinitions);
        }
    }
}
