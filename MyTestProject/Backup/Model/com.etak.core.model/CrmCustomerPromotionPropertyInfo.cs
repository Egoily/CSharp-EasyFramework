using System;

namespace com.etak.core.model
{
    [Serializable]
    public class CrmCustomerPromotionPropertyInfo
    {
        public long ID { get; set; }
        public CrmCustomersPromotionInfo Promotion { get; set; }
        public int IsSent { get; set; }
    }
}
