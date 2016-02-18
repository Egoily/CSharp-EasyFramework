
namespace com.etak.simpleeventsystem.model
{
    public interface ISimpleEventContractImplementorFactory
    {
        SimpleEventSystemContract GetImplementation();
        void Destroy(SimpleEventSystemContract implementation);
    }

}
