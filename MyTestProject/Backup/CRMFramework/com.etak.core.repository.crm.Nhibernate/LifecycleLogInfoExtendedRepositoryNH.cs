using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Respository for <typeparamref name="TLifecycleLogInfoExtended"/> entity
    /// </summary>
    /// <typeparam name="TLifecycleLogInfoExtended">The type of the managed entity, is or extends LifecycleLogInfoExtended</typeparam>
    public class LifecycleLogInfoExtendedRepositoryNH<TLifecycleLogInfoExtended>
        : NHibernateRepository<TLifecycleLogInfoExtended, long>,
       ILifecycleLogInfoExtendedRepository<TLifecycleLogInfoExtended> where TLifecycleLogInfoExtended : LifecycleLogInfoExtended
    {


   
    }
}
