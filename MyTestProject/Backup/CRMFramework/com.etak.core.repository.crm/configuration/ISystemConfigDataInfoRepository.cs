using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.configuration
{
    /// <summary>
    /// The respository interface for <typeparamref name="TSystemConfigDataInfo"/> entity
    /// </summary>
    /// <typeparam name="TSystemConfigDataInfo">The entity managed by the interface, is or extends SystemConfigDataInfo</typeparam>
    public interface ISystemConfigDataInfoRepository<TSystemConfigDataInfo> : IRepository<TSystemConfigDataInfo, String>
        where TSystemConfigDataInfo : SystemConfigDataInfo
    {
        /// <summary>
        /// Gets all entities of  TSystemConfigDataInfo
        /// </summary>
        /// <returns></returns>
        IEnumerable<TSystemConfigDataInfo> GetAllSystemConfigDateInfo();

        /// <summary>
        /// Gets all entities of TSystemConfigDataInfo with a given item
        /// </summary>
        /// <param name="item">the item of the TSystemConfigDataInfo to retrieve</param>
        /// <returns>entities of TSystemConfigDataInfo with a given item</returns>
        IEnumerable<TSystemConfigDataInfo> GetSystemConfigDateInfoByItem(string item);

        /// <summary>
        /// Gets all entities of TSystemConfigDataInfo with a given name
        /// </summary>
        /// <param name="name">the name of the TSystemConfigDataInfo to retrieve</param>
        /// <returns>entities of TSystemConfigDataInfo with a given name</returns>
        IEnumerable<TSystemConfigDataInfo> GetSystemConfigDateInfoByName(string name);

    }
}
