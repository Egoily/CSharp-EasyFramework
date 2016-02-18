using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.crm.rules;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity BussinessRule 
    /// </summary>
    /// <typeparam name="TBussinessRule">Entity managed by the repository, is or extends BussinessRule</typeparam>
    public class CrmBusinessRuleInfoRepositoryNH<TBussinessRule>
                    : NHibernateRepository<TBussinessRule, Int32>,                         //Extends and gets basic CRUD operations
                      ICrmBussinessRuleInfoRepository<TBussinessRule> where TBussinessRule : BussinessRule //Implementes 
    {
        private const String CacheRegion = CacheRegions.UserDealerCacheRegion;

        /// <summary>
        /// Gets an entity by it's unique ID
        /// </summary>
        /// <param name="id">Id of the entity to retrieve</param>
        /// <returns>The updated entity</returns>
        public TBussinessRule GetById(long id)
        {
            return (GetQuery().Where(x => x.Id == id).
                                Cacheable().CacheRegion(CacheRegion).
                                Future().FirstOrDefault());
        }

        /// <summary>
        /// This should not be required, the class types are managed by the Repository, not 
        /// </summary>
        /// <param name="classType">the class type of the TCrmBusinessRuleInfo</param>
        /// <returns>the list of mathing TCrmBusinessRuleInfo</returns>
        public IEnumerable<TBussinessRule> GetByClassType(string classType)
        {
            return (GetQuery().Where(x => x.ClassType == classType).
                                Cacheable().CacheRegion(CacheRegion).
                                Future());
        }
    }
}
