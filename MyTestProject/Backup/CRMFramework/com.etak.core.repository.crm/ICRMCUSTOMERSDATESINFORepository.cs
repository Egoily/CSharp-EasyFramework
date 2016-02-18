using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Interface for repository of entity CRMCUSTOMERSDATESPKINFO
    /// </summary>
    /// <typeparam name="TCRMCUSTOMERSDATESINFO">The type of the managed entity is or extends CRMCUSTOMERSDATESPKINFO</typeparam>
    public interface ICRMCUSTOMERSDATESINFORepository<TCRMCUSTOMERSDATESINFO> : IRepository<TCRMCUSTOMERSDATESINFO, CRMCUSTOMERSDATESPKINFO> where TCRMCUSTOMERSDATESINFO : CRMCUSTOMERSDATESINFO
    {
    }
}
