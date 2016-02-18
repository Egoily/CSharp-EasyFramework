using System;
using System.Collections.Generic;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.repository.crm.revenueManagement
{
    /// <summary>
    /// Repository for BillRun entity
    /// </summary>
    /// <typeparam name="TTaxDefinition">The type of the entity that is or extends BillRun</typeparam>
    public interface ITaxDefinitionRepository<TTaxDefinition> : IRepository<TTaxDefinition, Int32> where TTaxDefinition : TaxDefinition
    {
        /// <summary>
        /// Gets all the tax definitions with the given category
        /// </summary>
        /// <param name="taxCategory">the category to filter</param>
        /// <returns>the matching results</returns>
        IEnumerable<TTaxDefinition> GetDefinitionsForCategory(Int32 taxCategory);


        /// <summary>
        /// Searchs all the charge definition that have a zip code in that range. 
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        IEnumerable<TTaxDefinition> GetDefinitionsByZipCodeLike(String zipCode);
    }
}
