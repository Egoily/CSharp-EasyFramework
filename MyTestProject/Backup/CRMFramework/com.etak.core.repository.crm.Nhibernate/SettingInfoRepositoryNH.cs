using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.NHibernate;
using NHibernate;
using NHibernate.Collection;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity SettingInfo 
    /// </summary>
    /// <typeparam name="TSettingInfo">Entity managed by the repository, is or extends CrmCustomersPromotionOpeSettingInforationLogInfo</typeparam>
    public class SettingInfoRepositoryNH<TSettingInfo> : NHibernateRepository<TSettingInfo, Int32>, ISettingInfoRepository<TSettingInfo> where TSettingInfo : SettingInfo
    {
        /// <summary>
        /// Gets the sms configuration of a dealer
        /// </summary>
        /// <param name="dealerId">the dealer for which the sms configuration is</param>
        /// <returns>the list of sms settings</returns>
        public IEnumerable<TSettingInfo> GetByDealerId(int dealerId)
        {
            return GetQuery().Where(x => x.DealerId == dealerId).Future();
        }

        /// <summary>
        /// Gets the sms configuration of a dealer loading the details association
        /// </summary>
        /// <param name="dealerId">the dealer for which the sms configuration is</param>
        /// <returns>the list of sms settings</returns>
        public IEnumerable<TSettingInfo> GetSettingInfoWithDetailByDealerId(int dealerId)
        {


            IQueryOver<TSettingInfo, TSettingInfo> queryOver = GetQuery();
            queryOver.Where(x => x.DealerId == dealerId);

            IQueryOver<TSettingInfo, TSettingInfo> queryOverSettingDetailInfo = queryOver.Clone();
            IQueryOver<TSettingInfo, TSettingInfo> queryOverSmsTempletInfo = queryOver.Clone();

            IEnumerable<TSettingInfo> infos = queryOver.Future();

            queryOverSettingDetailInfo
             .Fetch(x => x.SettingDetailInfos).Eager
            .Future();

            queryOverSmsTempletInfo
              .Fetch(x => x.SmsTempletInfos).Eager
             .Future();


            foreach (TSettingInfo s in infos)
            {
                _session.GetSessionImplementation().InitializeCollection(s.SettingDetailInfos as IPersistentCollection, false);
                _session.GetSessionImplementation().InitializeCollection(s.SmsTempletInfos as IPersistentCollection, false);
            }
            return (infos);
        }

        /// <summary>
        /// Gets the sms configuration of a dealer and an event
        /// </summary>
        /// <param name="dealerId">the dealer for which the sms configuration is</param>
        /// <param name="eventId">the id of the event to which the sms configuration applies</param>
        /// <returns>the list of sms settings</returns>
        public IEnumerable<TSettingInfo> GetSettingInfoWithDetailByDealerIdAndEventID(int dealerId,int eventId)
        {


            IQueryOver<TSettingInfo, TSettingInfo> queryOver = GetQuery();
            queryOver.Where(x => x.DealerId == dealerId && x.EventInfo.EventId==eventId);

            IQueryOver<TSettingInfo, TSettingInfo> queryOverSettingDetailInfo = queryOver.Clone();
            IQueryOver<TSettingInfo, TSettingInfo> queryOverSmsTempletInfo = queryOver.Clone();

            IEnumerable<TSettingInfo> infos = queryOver.Future();

            queryOverSettingDetailInfo
             .Fetch(x => x.SettingDetailInfos).Eager
            .Future();

            queryOverSmsTempletInfo
              .Fetch(x => x.SmsTempletInfos).Eager
             .Future();


            foreach (TSettingInfo s in infos)
            {
                _session.GetSessionImplementation().InitializeCollection(s.SettingDetailInfos as IPersistentCollection, false);
                _session.GetSessionImplementation().InitializeCollection(s.SmsTempletInfos as IPersistentCollection, false);
            }
            return (infos);
        }
    }
}
