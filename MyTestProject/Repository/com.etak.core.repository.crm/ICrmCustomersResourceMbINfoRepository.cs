using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    public interface ICrmCustomersResourceMbInfoRepository<T> : IRepository<T, int> where T : CrmCustomersResourceMbInfo
    {
        IEnumerable<T> LoadResourceMBAsociations(Int32 resourceKey);
    }
}
