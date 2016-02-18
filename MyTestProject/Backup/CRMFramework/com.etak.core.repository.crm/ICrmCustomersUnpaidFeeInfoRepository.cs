using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{

    /// <summary>
    /// The respository interface for <typeparamref name="TCrmCustomersUnpaidFeeInfo"/> entity
    /// </summary>
    /// <typeparam name="TCrmCustomersUnpaidFeeInfo">The entity managed by the interface, is or extends CrmCustomersUnpaidFeeInfo</typeparam>
     public interface ICrmCustomersUnpaidFeeInfoRepository<TCrmCustomersUnpaidFeeInfo> : IRepository<TCrmCustomersUnpaidFeeInfo, long> where TCrmCustomersUnpaidFeeInfo : CrmCustomersUnpaidFeeInfo
    {
        
    }
}
