using System;
using com.etak.core.model;
using com.etak.core.repository.crm.resource;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.resource
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity ImeiAssnHist 
    /// </summary>
    /// <typeparam name="TImeiAssnHist">Entity managed by the repository, is or extends ImeiAssnHist</typeparam>
    public class ImeiAssnHistRepositoryNH<TImeiAssnHist> : NHibernateRepository<TImeiAssnHist, ImeiAssnHistPKInfo>,
        IImeiAssnHistRepository<TImeiAssnHist> where TImeiAssnHist : ImeiAssnHist
    {
       
    }
}
