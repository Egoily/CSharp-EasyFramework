using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class DealerLoyaltyInfo
    {
        virtual public bool IsDelete { get; set; }
        virtual public int? LoyaltyID { get; set; }
        virtual public string LoyaltyName{ get; set; }
        virtual public int? PaymentTypeID{ get; set; }
        virtual public int? UnitValue{ get; set; }
        virtual public int? LoyaltyPoint{ get; set; }
        virtual public bool IsActive{ get; set; }
        virtual public int? UserID{ get; set; }
        virtual public DateTime? CreateDate{ get; set; }
        virtual public DateTime? StartDate{ get; set; }
        virtual public DateTime? EndDate{ get; set; }
        virtual public DealerInfo DealerInfo { get; set; }
    }
}
