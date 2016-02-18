using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// The respository interface for <typeparamref name="TTransitionInfo"/> entity
    /// </summary>
    /// <typeparam name="TTransitionInfo">The entity managed by the interface, is or extends TTransitionInfo</typeparam>
    public interface ITransitionInfoRepository<TTransitionInfo> : IRepository<TTransitionInfo, Int32> where TTransitionInfo : TransitionInfo
    {
        /// <summary>
        /// Gets all the TTransitionInfo
        /// </summary>
        /// <returns>List with all the TTransitionInfo</returns>
        IEnumerable<TransitionInfo> GetAll();
    }
}
