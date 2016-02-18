using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.NHibernate;
using NHibernate;
using NHibernate.Collection;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity CustomerInfo 
    /// </summary>
    /// <typeparam name="TCustomerInfo">Entity managed by the repository, is or extends CustomerInfo</typeparam>
    public class CustomerInfoRepositoryNH<TCustomerInfo>
        : NHibernateRepository<TCustomerInfo, Int32>,
       ICustomerInfoRepository<TCustomerInfo> where TCustomerInfo : CustomerInfo
    {
        /// <summary>
        /// Gets the csutomer with the associations pre loaded from the db
        /// </summary>
        /// <param name="customerId">the Id of the customer to test</param>
        /// <returns>an enumerable with 0 or 1 customers</returns>
        public IEnumerable<TCustomerInfo> LoadCustomerAsociations(int customerId)
        {

            IEnumerable<TCustomerInfo> customers =

           GetQuery().
                Fetch(x => x.ProductsInfo).Eager.
                Where(x => x.CustomerID == customerId).
                TransformUsing(global::NHibernate.Transform.Transformers.DistinctRootEntity).
                Future();

            GetQuery().
                Fetch(x => x.Promotions).Eager.
                Where(x => x.CustomerID == customerId).
                Future();

            GetQuery().
                Fetch(x => x.PromotionGroups).Eager.
                Where(x => x.CustomerID == customerId).
                Future();

            GetQuery().
                Fetch(x => x.PropertyInfo).Eager.
                Where(x => x.CustomerID == customerId).
                Future();

            GetQuery().
                Fetch(x => x.ServicesInfo).Eager.
                Where(x => x.CustomerID == customerId).
                Future();

            GetQuery().
                Fetch(x => x.ResourceMBInfo).Eager.
                Where(x => x.CustomerID == customerId)
                .Future();

            return (customers);
        }

        /// <summary>
        /// Gets all customers with a given parent
        /// </summary>
        /// <param name="customerId">the paret of the customer</param>
        /// <returns>the list of customers</returns>
        public IEnumerable<TCustomerInfo> GetByParentId(Int32 customerId)
        {
            var ret = GetQuery().Where(ee => ee.ParentID == customerId && (ee.StatusID != 20 || ee.StatusID == null)).Future();

            GetQuery().Fetch(x => x.ProductsInfo).Eager.Where(x => x.ParentID == customerId).Future();

            return ret;
        }

        /// <summary>
        /// Load a customer and it's promotions by a given customerId
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IEnumerable<TCustomerInfo> LoadCustomerAndPromotionsByCustomerId(int customerId)
        {
            return (GetQuery().
                 Fetch(x => x.Promotions).Eager.
                 Where(x => x.CustomerID == customerId).
                 TransformUsing(global::NHibernate.Transform.Transformers.DistinctRootEntity).
                 Future());
        }

        /// <summary>
        /// Gets the only customer info with the provided customer id that ara not in a given state
        /// </summary>
        /// <param name="customerId">the Id of the customer to look up</param>
        /// <param name="statusId">the status Id of the customer to exclude</param>
        /// <returns>the 1 or 0 customers in an enumerable</returns>
        public IEnumerable<TCustomerInfo> LoadCustomerAllInfoByCustomerIdExcludeStatusID(int customerId, int statusId)
        {
            IQueryOver<TCustomerInfo, TCustomerInfo> rootQuery = GetQuery().Where(x => x.CustomerID == customerId
                && (x.StatusID == null || x.StatusID.Value != statusId));

            IQueryOver<TCustomerInfo, TCustomerInfo> queryOverProductInfoList = rootQuery.Clone();
            IQueryOver<TCustomerInfo, TCustomerInfo> queryOverPropertyInfoList = rootQuery.Clone();
            IQueryOver<TCustomerInfo, TCustomerInfo> queryOverResourceMBInfoList = rootQuery.Clone();
            IQueryOver<TCustomerInfo, TCustomerInfo> queryOverCrmCustomersPromotionInfoList = rootQuery.Clone();
            IQueryOver<TCustomerInfo, TCustomerInfo> queryOverCrmCustomersPromotionGroupList = rootQuery.Clone();
            IQueryOver<TCustomerInfo, TCustomerInfo> queryOverServicesInfoList = rootQuery.Clone();

            IQueryOver<TCustomerInfo, TCustomerInfo> queryOverRemarksInfoList = rootQuery.Clone();
            IQueryOver<TCustomerInfo, TCustomerInfo> queryOverMVNOCustomerPropertyInfoList = rootQuery.Clone();
            IQueryOver<TCustomerInfo, TCustomerInfo> queryOverBankInfoList = rootQuery.Clone();
            IQueryOver<TCustomerInfo, TCustomerInfo> queryOverCreditCheckInfoList = rootQuery.Clone();
            IQueryOver<TCustomerInfo, TCustomerInfo> queryOverMappingInfoList = rootQuery.Clone();
            IQueryOver<TCustomerInfo, TCustomerInfo> queryOverWholeSaleInfoList = rootQuery.Clone();
            IQueryOver<TCustomerInfo, TCustomerInfo> queryOverResourceIpInfoList = rootQuery.Clone();
            IQueryOver<TCustomerInfo, TCustomerInfo> queryOverResourceCSInfoList = rootQuery.Clone();
            IQueryOver<TCustomerInfo, TCustomerInfo> queryOverResourceTCInfoList = rootQuery.Clone();
            IQueryOver<TCustomerInfo, TCustomerInfo> queryOverPRSRatePlanInfoList = rootQuery.Clone();
            IQueryOver<TCustomerInfo, TCustomerInfo> queryOverResourceWIInfoList = rootQuery.Clone();
            IQueryOver<TCustomerInfo, TCustomerInfo> queryOverProvisionResourceMBInfoList = rootQuery.Clone();
            IQueryOver<TCustomerInfo, TCustomerInfo> queryOverFlex2nrNumberInfoList = rootQuery.Clone();
            IQueryOver<TCustomerInfo, TCustomerInfo> queryOverPortingTraceInfoList = rootQuery.Clone();
            IQueryOver<TCustomerInfo, TCustomerInfo> queryOverCustomerCreditCardList = rootQuery.Clone();

            IEnumerable<TCustomerInfo> customers = rootQuery.Future();
            queryOverProductInfoList.Fetch(x => x.ProductsInfo).Eager.Future();
            queryOverPropertyInfoList.Fetch(x => x.PropertyInfo).Eager.Future();
            queryOverResourceMBInfoList.Fetch(x => x.ResourceMBInfo).Eager.Future();
            queryOverCrmCustomersPromotionInfoList.Fetch(x => x.Promotions).Eager.Future();
            queryOverCrmCustomersPromotionGroupList.Fetch(x => x.PromotionGroups).Eager.Future();
            queryOverServicesInfoList.Fetch(x => x.ServicesInfo).Eager.Future();

            //queryOverRemarksInfoList.Fetch(x => x.RemarksInfo).Eager.Future();
            queryOverMVNOCustomerPropertyInfoList.Fetch(x => x.MVNOCustomerPropertyInfo).Eager.Future();
            queryOverBankInfoList.Fetch(x => x.BankInfo).Eager.Future();
            //queryOverCreditCheckInfoList.Fetch(x => x.CreditCheckInfos).Eager.Future();
            queryOverMappingInfoList.Fetch(x => x.MappingInfo).Eager.Future();
            //queryOverWholeSaleInfoList.Fetch(x => x.WholeSaleInfo).Eager.Future();
            //queryOverResourceIpInfoList.Fetch(x => x.ResourceIpInfo).Eager.Future();
            //queryOverResourceCSInfoList.Fetch(x => x.ResourceCSInfo).Eager.Future();
            //queryOverResourceTCInfoList.Fetch(x => x.ResourceTCInfo).Eager.Future();
            //queryOverPRSRatePlanInfoList.Fetch(x => x.PRSRatePlanInfo).Eager.Future();
            //queryOverResourceWIInfoList.Fetch(x => x.ResourceWIInfo).Eager.Future();
            queryOverProvisionResourceMBInfoList.Fetch(x => x.ProvisionResourceMBInfo).Eager.Future();
            //queryOverFlex2nrNumberInfoList.Fetch(x => x.Flex2nrNumberInfo).Eager.Future();
            //queryOverPortingTraceInfoList.Fetch(x => x.CustomerPortingTraceInfos).Eager.Future();
            queryOverCustomerCreditCardList.Fetch(x => x.CustomerCreditCards).Eager.Future();


            foreach (TCustomerInfo c in customers)
            {
                if (c.ProductsInfo != null && c.ProductsInfo.Count>0)
                _session.GetSessionImplementation().InitializeCollection(c.ProductsInfo as IPersistentCollection, false);
                if (c.PropertyInfo != null && c.PropertyInfo.Count > 0)
                _session.GetSessionImplementation().InitializeCollection(c.PropertyInfo as IPersistentCollection, false);
                if (c.ResourceMBInfo != null && c.ResourceMBInfo.Count > 0)
                _session.GetSessionImplementation().InitializeCollection(c.ResourceMBInfo as IPersistentCollection, false);
                if (c.Promotions != null && c.Promotions.Count > 0)
                _session.GetSessionImplementation().InitializeCollection(c.Promotions as IPersistentCollection, false);
                if (c.PromotionGroups != null && c.PromotionGroups.Count > 0)
                _session.GetSessionImplementation().InitializeCollection(c.PromotionGroups as IPersistentCollection, false);
                if (c.ServicesInfo != null && c.ServicesInfo.Count > 0)
                _session.GetSessionImplementation().InitializeCollection(c.ServicesInfo as IPersistentCollection, false);

                //if (c.RemarksInfo != null && c.RemarksInfo.Count > 0)
                //_session.GetSessionImplementation().InitializeCollection(c.RemarksInfo as IPersistentCollection, false);
                if (c.MVNOCustomerPropertyInfo != null && c.MVNOCustomerPropertyInfo.Count > 0)
                _session.GetSessionImplementation().InitializeCollection(c.MVNOCustomerPropertyInfo as IPersistentCollection, false);
                if (c.BankInfo != null && c.BankInfo.Count > 0)
                _session.GetSessionImplementation().InitializeCollection(c.BankInfo as IPersistentCollection, false);
                //if (c.CreditCheckInfos != null && c.CreditCheckInfos.Count > 0)
                //_session.GetSessionImplementation().InitializeCollection(c.CreditCheckInfos as IPersistentCollection, false);
                if (c.MappingInfo != null && c.MappingInfo.Count > 0)
                _session.GetSessionImplementation().InitializeCollection(c.MappingInfo as IPersistentCollection, false);
                //if (c.WholeSaleInfo != null && c.WholeSaleInfo.Count > 0)
                //_session.GetSessionImplementation().InitializeCollection(c.WholeSaleInfo as IPersistentCollection, false);
                //if (c.ResourceIpInfo != null && c.ResourceIpInfo.Count > 0)
                //_session.GetSessionImplementation().InitializeCollection(c.ResourceIpInfo as IPersistentCollection, false);
                //if (c.ResourceCSInfo != null && c.ResourceCSInfo.Count > 0)
                //_session.GetSessionImplementation().InitializeCollection(c.ResourceCSInfo as IPersistentCollection, false);
                //if (c.ResourceTCInfo != null && c.ResourceTCInfo.Count > 0)
                //_session.GetSessionImplementation().InitializeCollection(c.ResourceTCInfo as IPersistentCollection, false);
                //if (c.PRSRatePlanInfo != null && c.PRSRatePlanInfo.Count > 0)
                //_session.GetSessionImplementation().InitializeCollection(c.PRSRatePlanInfo as IPersistentCollection, false);
                //if (c.ResourceWIInfo != null && c.ResourceWIInfo.Count > 0)
                //_session.GetSessionImplementation().InitializeCollection(c.ResourceWIInfo as IPersistentCollection, false);
                if (c.ProvisionResourceMBInfo != null && c.ProvisionResourceMBInfo.Count > 0)
                _session.GetSessionImplementation().InitializeCollection(c.ProvisionResourceMBInfo as IPersistentCollection, false);
                //if (c.Flex2nrNumberInfo != null && c.Flex2nrNumberInfo.Count > 0)
                //_session.GetSessionImplementation().InitializeCollection(c.Flex2nrNumberInfo as IPersistentCollection, false);
                //if (c.CustomerPortingTraceInfos != null && c.CustomerPortingTraceInfos.Count > 0)
                //_session.GetSessionImplementation().InitializeCollection(c.CustomerPortingTraceInfos as IPersistentCollection, false);
                if (c.CustomerCreditCards != null && c.CustomerCreditCards.Count > 0)
                _session.GetSessionImplementation().InitializeCollection(c.CustomerCreditCards as IPersistentCollection, false);


            }
            return (customers);
        }


        /// <summary>
        /// Gets all customers with a given external id, and filtering by dealer id
        /// </summary>
        /// <param name="mvnoId">the dealer owner of the configuration</param>
        /// <param name="externalId">the external id of the customer</param>
        /// <returns>all customers with the given external id</returns>
        public IEnumerable<TCustomerInfo> GetByDealerIdAndExternalId(Int32 mvnoId, string externalId)
        {
            PropertyInfo propAlias = null;

            return GetQuery().Where(x=> x.DealerID == mvnoId)               
                .JoinAlias(j=> j.PropertyInfo, () => propAlias)
                .Where(y => propAlias.ExternalId==externalId).
                Future();
        }


        /// <summary>
        /// Gets all customers with a given external id
        /// </summary>
        /// <param name="externalId">the external id of the customer</param>
        /// <returns>all customers with the given external id</returns>
        public IEnumerable<TCustomerInfo> GetByExternalId (string externalId)
        {
            PropertyInfo propAlias = null;

            return GetQuery()
               .JoinAlias(j => j.PropertyInfo, () => propAlias)
               .Where(() => propAlias.ExternalId == externalId).
               Future();
           
        }
    }
}
