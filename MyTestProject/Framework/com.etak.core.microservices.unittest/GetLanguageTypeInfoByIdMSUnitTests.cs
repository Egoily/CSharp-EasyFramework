using System;
using System.Collections.Generic;
using com.etak.core.microservices.messages.GetLanguageTypeByCode;
using com.etak.core.microservices.microservices;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.repository.crm;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.microservices.unittest
{
    [TestFixture]
    public class GetLanguageTypeInfoByCodeMSTest :
        AbstractMicroServiceTest
            <GetLanguageTypeInfoByCodeMS, GetLanguageTypeInfoByCodeRequest, GetLanguageTypeInfoByCodeResponse>
    {
        private ILanguageTypeInfoRepository<LanguageTypeInfo> mockedRepository;

        [TestFixtureSetUp()]
        public void InitializeSetup()
        {
            mockedRepository = MockRepository<ILanguageTypeInfoRepository<LanguageTypeInfo>>();
            mockedRepository.GetAllLanguageById(100).Returns(new List<LanguageTypeInfo>{CreateDefaultObject.Create<LanguageTypeInfo>()});
            mockedRepository.GetAllLanguageById(200).Returns(x => { throw new Exception("Error"); });
            mockedRepository.GetAllLanguageById(300).Returns(new List<LanguageTypeInfo>());
        }

        [TestCase(100)]
        public void GetLanguageTypeInfoByCodeMSOk(int testParameter)
        {
            var expectedObject = new List<LanguageTypeInfo>() {CreateDefaultObject.Create<LanguageTypeInfo>()};
            var request = new GetLanguageTypeInfoByCodeRequest()
            {
                LanguadeId = testParameter,
            };

            var response = CallMicroservice(request);

            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            AssertExt.ObjectPropertiesAreEqual(expectedObject, response.LanguageTypeInfos);
        }

        [TestCase(200)]
        public void GetLanguageTypeInfoByCodeMSNOk(int testParameter)
        {

            var request = new GetLanguageTypeInfoByCodeRequest()
            {
                LanguadeId = testParameter,
            };

            Assert.Throws<Exception>(() => CallMicroservice(request));
        }

        [TestCase(300)]
        public void GetLanguageTypeInfoByCodeMSNOkNull(int testParameter)
        {
            var request = new GetLanguageTypeInfoByCodeRequest()
            {
                LanguadeId = testParameter,
            };

            var response = CallMicroservice(request);

            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            AssertExt.IsEmpty(response.LanguageTypeInfos);
        }
    }
}


