using com.etak.core.model.operation;

namespace com.etak.core.operation.UnitTests.operations
{
    public class FakeOrder : Order
    {
        public override string Discriminator
        {
            get { return "FakeOr";  }
        }
    }
}
