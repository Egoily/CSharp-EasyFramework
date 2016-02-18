using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for CRMCUSTOMERSDATESINFO entity inheritance tree
    /// </summary>
    /// <typeparam name="TCRMCUSTOMERSDATESINFO">the type of entity managed, is or extends CRMCUSTOMERSDATESINFO</typeparam>
    public class CRMCUSTOMERSDATESINFORepositoryNH<TCRMCUSTOMERSDATESINFO> : 
        NHibernateRepository<TCRMCUSTOMERSDATESINFO, CRMCUSTOMERSDATESPKINFO>, ICRMCUSTOMERSDATESINFORepository<TCRMCUSTOMERSDATESINFO> where TCRMCUSTOMERSDATESINFO : CRMCUSTOMERSDATESINFO
    {
    }
}
