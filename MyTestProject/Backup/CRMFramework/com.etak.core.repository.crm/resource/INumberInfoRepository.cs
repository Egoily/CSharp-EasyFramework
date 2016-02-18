using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.resource
{
    /// <summary>
    /// Respository for <typeparamref name="TNumberInfo"/> entity
    /// </summary>
    /// <typeparam name="TNumberInfo">The type of the managed entity, is or extends NumberInfo</typeparam>
   
    public interface INumberInfoRepository<TNumberInfo> : IRepository<TNumberInfo, String> where TNumberInfo : NumberInfo
    {
        /// <summary>
        /// Gets n (maxElements) TNumberInfo of any mvnos of the category and the status.
        /// </summary>
        /// <param name="mvnoIds">The possible mvnos to which the number may belong</param>
        /// <param name="categoryId">The category of the number</param>
        /// <param name="status">all the possible status of the numbers to retrieve</param>
        /// <param name="maxElements">the maximun number of numbers to retrieve</param>
        /// <returns>the list of matching numbers</returns>
        IEnumerable<NumberInfo> GetByCategoryAndVmnoAndStatusIdIn(IEnumerable<int> mvnoIds, Int32 categoryId, IEnumerable<Int32> status, Int32 maxElements);
    }
}