using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.subscription
{
    /// <summary>
    /// Interface for ResourceMB Repository
    /// </summary>
    /// <typeparam name="TResourceMBInfo">The entity managed by the repository, is or extends ResourceMBInfo</typeparam>
    public interface IResourceMBRepository<TResourceMBInfo> : IRepository<TResourceMBInfo, Int32> where TResourceMBInfo : ResourceMBInfo
    {
        /// <summary>
        /// Gets all TResourceMBInfo with a given MSISDN, Dates are in a active range and the status 
        /// is not equal to the <paramref name="p"/> parameter
        /// </summary>
        /// <param name="MSISDN">the msisdn to look up for</param>
        /// <param name="p">the status in which the TResourceMBInfo can't be</param>
        /// <returns>the list of resources that fullfill the conditions</returns>
        IEnumerable<TResourceMBInfo> GetByMSISDNAndNotStatusAndActiveDates(string MSISDN, int p);

        /// <summary>
        /// Gets a list of TResourceMBInfo based on a list of Id
        /// </summary>
        /// <param name="IdEnum">the list of the Ids to look for</param>
        /// <returns>the list of resources that fullfill the conditions</returns>
        IEnumerable<TResourceMBInfo> GetByIdList(IEnumerable<int> IdEnum);

        /// <summary>
        /// Gets all TResourceMBInfo with a given MSISDN, Dates are in a active range and the status 
        /// is not in to the <paramref name="status"/> parameter
        /// </summary>
        /// <param name="msisdn">the msisdn to look up for</param>
        /// <param name="status">the lis of status in which the TResourceMBInfo can't be</param>
        /// <returns>the list of resources that fullfill the conditions</returns>
        IEnumerable<TResourceMBInfo> GetByMSISDNAndStatusNotInAndActiveDates(string msisdn, IEnumerable<Int32> status);

        /// <summary>
        /// Gets the list of Subscriptions with a given ICCid
        /// </summary>
        /// <param name="iccardId">the Iccid to look for</param>
        /// <returns>the list of resources that fullfill the conditions</returns>
        IEnumerable<TResourceMBInfo> GetMSISDNByICC(string iccardId);

        /// <summary>
        /// Gets all Subscriptions of the given customerId
        /// </summary>
        /// <param name="customerId">the customer Id that owns the subscriptions</param>
        /// <returns>the list of resources that fullfill the conditions</returns>
        IEnumerable<TResourceMBInfo> GetMSISDNByCustomerID(int customerId);

        /// <summary>
        /// Gets the list of Subscriptions with a given IMSI
        /// </summary>
        /// <param name="imsi">the imsi to look for</param>
        /// <returns>the list of resources that fullfill the conditions</returns>
        IEnumerable<TResourceMBInfo> GetMSISDNByIMSI(string imsi);

        /// <summary>
        /// Get the Last Subscription in base of a Dealer Id, MSISDN and a list of non valid status
        /// </summary>
        /// <param name="dealer">The DealerInfo that owns the subscription</param>
        /// <param name="msisdn">The Resource of the subscription</param>
        /// <param name="status">A list of non valid status</param>
        /// <returns>The Subscription with the oldest CreateDate that match the criteria</returns>
        IEnumerable<TResourceMBInfo> GetLastByDealerIdAndMSISDNAndStatusNotIn(DealerInfo dealer, string msisdn, IEnumerable<Int32> status);

        /// <summary>
        /// Get the Subscriptions related with the given MSISDN and a list of non Valid status
        /// </summary>
        /// <param name="msisdn">The Resource of the subscription</param>
        /// <param name="status">A list of non valid status</param>
        /// <returns></returns>
        IEnumerable<TResourceMBInfo> GetByMSISDNAndStatusNotIn(string msisdn, IEnumerable<Int32> status);

    }
}
