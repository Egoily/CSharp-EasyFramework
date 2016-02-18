using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;

namespace com.etak.core.repository.crm.subscription
{
    /// <summary>
    /// Repository interface for CrmMobileMultipleImsiInfo
    /// </summary>
    /// <typeparam name="TCrmMobileMultipleImsiInfo">The entity managed by the repository, is or extends CrmMobileMultipleImsiInfo</typeparam>
    public interface ICrmMobileMultipleImsiInfoRepository<TCrmMobileMultipleImsiInfo> : IRepository<TCrmMobileMultipleImsiInfo, int>
        where TCrmMobileMultipleImsiInfo: CrmMobileMultipleImsiInfo
    {

    }
}
