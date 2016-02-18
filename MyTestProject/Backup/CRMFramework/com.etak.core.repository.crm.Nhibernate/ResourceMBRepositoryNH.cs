using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.subscription;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Implementation of IResourceMBRepository based on Nhibernate
    /// </summary>
    /// <typeparam name="TResourceMBInfo">The type of the entity for the repository is or extends ResourceMBInfo</typeparam>
    public class ResourceMBRepositoryNH<TResourceMBInfo>
        : NHibernateRepository< TResourceMBInfo, Int32>,
        IResourceMBRepository<TResourceMBInfo> where TResourceMBInfo : ResourceMBInfo
    {
        /// <summary>
        /// Gets all TResourceMBInfo with a given MSISDN, Dates are in a active range and the status 
        /// is not equal to the <paramref name="p"/> parameter
        /// </summary>
        /// <param name="MSISDN">the msisdn to look up for</param>
        /// <param name="p">the status in which the TResourceMBInfo can't be</param>
        /// <returns>the list of resources that fullfill the conditions</returns>
        public IEnumerable<TResourceMBInfo> GetByMSISDNAndNotStatusAndActiveDates(string MSISDN, int p)
        {
            return (GetByMSISDNAndStatusNotInAndActiveDates(MSISDN, new int[] { p }));
        }

        /// <summary>
        /// Gets a list of TResourceMBInfo based on a list of Id
        /// </summary>
        /// <param name="IdEnum">the list of the Ids to look for</param>
        /// <returns>the list of resources that fullfill the conditions</returns>
        public IEnumerable<TResourceMBInfo> GetByIdList(IEnumerable<int> IdEnum)
        {
            return (GetQuery().WhereRestrictionOn(x=> x.ResourceID).IsIn(IdEnum.ToArray()).Future());
        }

        /// <summary>
        /// Gets all TResourceMBInfo with a given MSISDN, Dates are in a active range and the status 
        /// is not in to the <paramref name="status"/> parameter
        /// </summary>
        /// <param name="msisdn">the msisdn to look up for</param>
        /// <param name="status">the lis of status in which the TResourceMBInfo can't be</param>
        /// <returns>the list of resources that fullfill the conditions</returns>
        public IEnumerable<TResourceMBInfo> GetByMSISDNAndStatusNotInAndActiveDates(string msisdn, IEnumerable<Int32> status)
        {
            return (GetQuery().Where(x => x.Resource == msisdn
                                         &&  x.StartDate < DateTime.Now
                                         && (x.EndDate == null || x.EndDate > DateTime.Now)).
                                         AndRestrictionOn(x=> x.StatusID).Not.
                                         IsIn(status.ToArray()).
                                         Future());
        }

        /// <summary>
        /// Gets the list of Subscriptions with a given ICCid
        /// </summary>
        /// <param name="iccardId">the Iccid to look for</param>
        /// <returns>the list of resources that fullfill the conditions</returns>
        public IEnumerable<TResourceMBInfo> GetMSISDNByICC(string iccardId)
        {
            return (GetQuery().Where(x => x.ICC == iccardId). Future());
        }

        /// <summary>
        /// Gets all Subscriptions of the given customerId
        /// </summary>
        /// <param name="customerId">the customer Id that owns the subscriptions</param>
        /// <returns>the list of resources that fullfill the conditions</returns>
        public IEnumerable<TResourceMBInfo> GetMSISDNByCustomerID(int customerId)
        {
            return (GetQuery().Where(x => x.CustomerInfo.CustomerID.Value == customerId).Future());
        }

        /// <summary>
        /// Gets the list of Subscriptions with a given IMSI
        /// </summary>
        /// <param name="imsi">the imsi to look for</param>
        /// <returns>the list of resources that fullfill the conditions</returns>
        public IEnumerable<TResourceMBInfo> GetMSISDNByIMSI(string imsi)
        {
            return (GetQuery().Where(x => x.IMSI == imsi).Future());
        }

        /// <summary>
        /// Get the Last Subscription in base of a Dealer Id, MSISDN and a list of non valid status
        /// </summary>
        /// <param name="dealer">The DealerInfo that owns the subscription</param>
        /// <param name="msisdn">The Resource of the subscription</param>
        /// <param name="status">A list of non valid status</param>
        /// <returns>The Subscription with the oldest CreateDate that match the criteria</returns>
        public IEnumerable<TResourceMBInfo> GetLastByDealerIdAndMSISDNAndStatusNotIn(DealerInfo dealer, string msisdn, IEnumerable<int> status)
        {
            
            return (GetQuery().Where(x => x.Resource == msisdn 
                                    && x.StartDate < DateTime.Now
                                    && x.OperatorInfo == dealer))
                                    .AndRestrictionOn(x => x.StatusID).Not.IsIn(status.ToArray())
                                    .OrderBy(x => x.CreateDate).Desc
                                    .Take(1)
                                    .Future();
        }


        /// <summary>
        /// Get the Subscriptions related with the given MSISDN and a list of non Valid status
        /// </summary>
        /// <param name="msisdn">The Resource of the subscription</param>
        /// <param name="status">A list of non valid status</param>
        /// <returns></returns>
        public IEnumerable<TResourceMBInfo> GetByMSISDNAndStatusNotIn(string msisdn, IEnumerable<int> status)
        {
            return (GetQuery().Where(x => x.Resource == msisdn).
                                         AndRestrictionOn(x=> x.StatusID).Not.IsIn(status.ToArray())
                                         .Future());
        }
    }
}
