using System;
using com.etak.core.model.provisioning;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for HLRRequestErrors entity inheritance tree
    /// </summary>
    /// <typeparam name="THLRRequestErrors">the type of entity managed, is or extends HLRRequestErrors</typeparam>
    public class HLRRequestErrorsRepositoryNH<THLRRequestErrors>
        : NHibernateRepository<THLRRequestErrors, Int64>,
        IHLRRequestErrorsRepository<THLRRequestErrors> where THLRRequestErrors : HLRRequestErrors
    {
    }
}
