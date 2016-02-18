using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class BundleInfo
    {
        virtual public int? BundleID {get;set;}
        virtual public int? DealerID { get; set; }
        virtual public int? ServiceTypeID { get; set; }
        virtual public int? SubserviceTypeID { get; set; }
        virtual public string BundleName { get; set; }
        virtual public int? RatePlanID { get; set; }
        virtual public decimal? CreditLimit { get; set; }
        virtual public decimal? SubscriptionFee { get; set; }
        virtual public int? SubscriptionCycleID { get; set; }
        virtual public int? CurrencyID { get; set; }
        virtual public int? PaymengTypeID { get; set; }
        virtual public DateTime? StartDate { get; set; }
        virtual public DateTime? EndDate { get; set; }
        virtual public decimal? MonthFee { get; set; }        

    }
}
