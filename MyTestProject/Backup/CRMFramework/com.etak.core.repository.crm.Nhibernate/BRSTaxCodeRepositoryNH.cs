using System;
using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for BRSTaxCode entity inheritance tree
    /// </summary>
    /// <typeparam name="TBRSTaxCode">the type of entity managed, is or extends BRSTaxCode</typeparam>
    public class BRSTaxCodeRepositoryNH<TBRSTaxCode> : 
        NHibernateRepository<TBRSTaxCode, Int32>, IBRSTaxCodeRepository<TBRSTaxCode> where TBRSTaxCode : BRSTaxCode
    {
         
    }
}
