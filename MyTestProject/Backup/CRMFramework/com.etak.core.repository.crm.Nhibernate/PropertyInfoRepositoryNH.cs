using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity PropertyInfo 
    /// </summary>
    /// <typeparam name="TPropertyInfo">Entity managed by the repository, is or extends PropertyInfo</typeparam>
    public class PropertyInfoRepositoryNH<TPropertyInfo> : 
        NHibernateRepository<TPropertyInfo, Int32>,                       
                      IPropertyInfoRepository<TPropertyInfo> where TPropertyInfo : PropertyInfo 
    {
        /// <summary>
        /// Gets the customer properties that have a given identufication number
        /// </summary>
        /// <param name="idType">the type of the document</param>
        /// <param name="idNumber">the document number</param>
        /// <returns>the list of customer properties that have this id</returns>
        public  IEnumerable<TPropertyInfo> GetByDocumentId(int idType, string idNumber)
        {
            return GetQuery().Where(x => x.IDType == idType && x.IDNumber == idNumber).
                TransformUsing(global::NHibernate.Transform.Transformers.DistinctRootEntity).
                Future();
        }
      
    }
}
