using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for CrmCustomersUnpaidFeeInfo entity inheritance tree
    /// </summary>
    /// <typeparam name="TCrmCustomersUnpaidFeeInfo">the type of entity managed, is or extends CrmCustomersUnpaidFeeInfo</typeparam>
    public class CrmCustomersUnpaidFeeInfoRepositoryNH<TCrmCustomersUnpaidFeeInfo> :
      NHibernateRepository<TCrmCustomersUnpaidFeeInfo, Int64>, ICrmCustomersUnpaidFeeInfoRepository<TCrmCustomersUnpaidFeeInfo>
     where TCrmCustomersUnpaidFeeInfo : CrmCustomersUnpaidFeeInfo 
    {
       
    }
}
