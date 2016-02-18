using System;
using System.Collections.Generic;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.NHibernate;
using NHibernate.Criterion;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity TaxDefinition 
    /// </summary>
    /// <typeparam name="TTaxDefinition">Entity managed by the repository, is or extends TaxDefinition</typeparam>
    public class TaxDefinitionRepositoryNH<TTaxDefinition> : 
        NHibernateRepository<TTaxDefinition, Int32>, 
        ITaxDefinitionRepository<TTaxDefinition> where TTaxDefinition : TaxDefinition
    {
        /// <summary>
        /// Gets all the tax definitions with the given category
        /// </summary>
        /// <param name="taxCategory">the category to filter</param>
        /// <returns>the matching results</returns>
        public IEnumerable<TTaxDefinition> GetDefinitionsForCategory(int taxCategory)
        {
            return GetQuery().Where(x => x.TaxCategory == taxCategory).Cacheable().CacheRegion(CacheRegions.CatalogCacheRegion).Future();
        }


        /// <summary>
        /// Searchs all the charge definition that have a zip code in that range. 
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public IEnumerable<TTaxDefinition> GetDefinitionsByZipCodeLike(String zipCode)
        {
            //ICriterion crit = Restrictions.On<TaxZipRanges>(x => x.ZipRangesHigh).IsLike(ZipCode+"%");
            ICriterion crit = Restrictions.And(Restrictions.Le("ZipRangesLow", zipCode),
                                               Restrictions.Ge("ZipRangesHigh", zipCode));
           
            return GetQuery().
                JoinQueryOver(x => x.ZipRanges).
                Where(crit)
                .Cacheable().CacheRegion(CacheRegions.CatalogCacheRegion)
                .Future();
            
        }
    }
}
