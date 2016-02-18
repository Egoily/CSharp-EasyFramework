using System;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// NHibernate repository for inheritance tree of entity PaymentInfo
    /// </summary>
    /// <typeparam name="TPaymentInfo">Entity managed by the repository, is or extends PaymentInfo</typeparam>
    public class PaymentInfoRepositoryNH<TPaymentInfo> : NHibernateRepository<TPaymentInfo, Int64>, IPaymentInfoRepository<TPaymentInfo>
        where TPaymentInfo : PaymentInfo
    {

    }
}
