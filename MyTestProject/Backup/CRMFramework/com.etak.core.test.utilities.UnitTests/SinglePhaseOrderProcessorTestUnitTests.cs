using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using com.etak.core.test.utilities.UnitTests.BizOpTest.SinglePhaseOrderProcessorTest;
using NUnit.Framework;

namespace com.etak.core.test.utilities.UnitTests
{
    [TestFixture()]
    class SinglePhaseOrderProcessorTestUnitTests : AbstractSinglePhaseOrderProcessorTest<SinglePhaseOrderProcessorTestBizOp, SinglePhaseOrderProcessorRequestDTO, SinglePhaseOrderProcessorResponseDTO, SinglePhaseOrderProcessorRequestInternal, SinglePhaseOrderProcessorResponseInternal, SinglePhaseOrderProcessorOrder>
    {
        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
        }

        [Test()]
        public void CallBizop_ExpectedSinglePhaseOrderProcessorRequestDTO_ShouldReturnResponseWithOkOrderCode()
        {

            var actualSinglePhaseOrderProcessorRequestDTO = new SinglePhaseOrderProcessorRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "1675003129",
            };

            MockAbstractSinglePhaseOrderProcessor(actualSinglePhaseOrderProcessorRequestDTO);

            var response = CallBizOp(actualSinglePhaseOrderProcessorRequestDTO);
            Assert.IsTrue(response.orderCode == 0);
        }
    }
}
