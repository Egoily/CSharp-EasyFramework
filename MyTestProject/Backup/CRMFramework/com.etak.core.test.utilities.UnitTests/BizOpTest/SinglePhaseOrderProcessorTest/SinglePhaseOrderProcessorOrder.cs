using com.etak.core.model.operation;

namespace com.etak.core.test.utilities.UnitTests.BizOpTest.SinglePhaseOrderProcessorTest
{
    public class SinglePhaseOrderProcessorOrder : Order
    {
        public override string Discriminator
        {
            get { return "TBD"; }
        }
    }
}
