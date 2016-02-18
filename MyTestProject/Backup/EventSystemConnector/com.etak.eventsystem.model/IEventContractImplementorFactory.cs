
namespace com.etak.eventsystem.model
{
    public interface IEventContractImplementorFactory
    {
        EventSystemContract GetImplementation();
        void Destroy(EventSystemContract implementation);
    }

}
