using System;
using com.etak.core.microservices.messages.GetTaxAuthority;
using com.etak.core.microservices.microservices;
using com.etak.core.test.utilities.abstracts;
using NUnit.Framework;

namespace com.etak.core.microservices.UniTests
{
    [TestFixture()]
    public class GetTaxAuthorityMSUnitTests : AbstractMicroServiceTest<GetTaxAuthorityMS, GetTaxAuthorityRequest, GetTaxAuthorityResponse>
    {
        // This MS is a Dummy one (no logic), will be implemented at CRM Level


        [TestFixtureSetUp()]
        public void InitializeSetup()
        {
        }

        [Test]
        public void Process_RepoThrowException_ShouldThrowException()
        {

            var request = new GetTaxAuthorityRequest()
            {
                
            };

            Assert.Throws<NotImplementedException>(() => CallMicroservice(request));
        }

    }
}
