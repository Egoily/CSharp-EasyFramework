using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.repository.crm.revenueManagement
{
    /// <summary>
    /// Repository Interface for <typeparamref name="TPaymentInfo"/> entity
    /// </summary>
    /// <typeparam name="TPaymentInfo">The type of the entity managed is or extends PaymentInfo</typeparam>
    public interface IPaymentInfoRepository<TPaymentInfo> : IRepository<TPaymentInfo, Int64>
        where TPaymentInfo : PaymentInfo
    {

    }
}
