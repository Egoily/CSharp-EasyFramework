using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.resource;
using com.etak.core.repository.NHibernate;
using NHibernate.Criterion;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity NumberInfo 
    /// </summary>
    /// <typeparam name="TNumberInfo">Entity managed by the repository, is or extends NumberInfo</typeparam>
    public class NumberInfoRepositoryNH<TNumberInfo> :
        NHibernateRepository<TNumberInfo, Int64>,
        INumberInfoRepository<TNumberInfo> where TNumberInfo:NumberInfo
    {
        /// <summary>
        /// Gets n (maxElements) TNumberInfo of any mvnos of the category and the status.
        /// </summary>
        /// <param name="mvnoIds">The possible mvnos to which the number may belong</param>
        /// <param name="categoryId">The category of the number</param>
        /// <param name="status">all the possible status of the numbers to retrieve</param>
        /// <param name="maxElements">the maximun number of numbers to retrieve</param>
        /// <returns>the list of matching numbers</returns>
        public IEnumerable<NumberInfo> GetByCategoryAndVmnoAndStatusIdIn(IEnumerable<int> mvnoIds, int categoryId, IEnumerable<int> status, int maxElements)
        {
            NumberPropertyInfo numProp = null;
            DealerNumberInfo numDelaer = null;

            return 
                GetQuery().
                JoinAlias(x => x.NumberProperty, () => numProp).
                JoinAlias(x => x.NumberDealerSharing, () => numDelaer).
                Where(
                    n =>
                        numProp.StatusID.Value.IsIn(status.ToArray()) 
                        && n.CategoryID == categoryId 
                        && numDelaer.DealerID.IsIn(mvnoIds.ToArray()))
                .Take(maxElements).Future();
        }

        /// <summary>
        /// Stores the entity provided
        /// </summary>
        /// <param name="entity">the entity to persist</param>
        /// <returns>the created entity with the Id assigned</returns>
        public override TNumberInfo Create(TNumberInfo entity)
        {
            _session.Save(entity.NumberProperty);
            return (base.Create(entity));
            
        }
    }
}
