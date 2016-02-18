using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;

namespace com.etak.core.repository.crm.subscription
{
    /// <summary>
    /// Repository interface for CrmMobileNetworkInfo
    /// </summary>
    /// <typeparam name="TCrmMobileNetworkInfo">The entity managed by the repository, is or extends CrmMobileNetworkInfo</typeparam>
    public interface ICrmMobileNetworkInfoRepository<TCrmMobileNetworkInfo> : IRepository<TCrmMobileNetworkInfo, int>
        where TCrmMobileNetworkInfo: CrmMobileNetWorkInfo
    {

    }
}
