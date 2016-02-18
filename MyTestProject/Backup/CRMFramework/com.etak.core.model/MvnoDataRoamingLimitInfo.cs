using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class MvnoDataRoamingLimitInfo : ModelBase
    {
        virtual public int? ID { get; set; }
        virtual public int? ZONEID { get; set; }
        virtual public string APN { get; set; }
        virtual public decimal? PREPAYCONSUMPTIONLIMIT { get; set; }
        virtual public decimal? PREPAYCONSUMPTIONCONTINUELIMIT { get; set; }
        virtual public int? PREPAYLIMITUNIT { get; set; }
        virtual public decimal? POSTPAYCONSUMPTIONLIMIT { get; set; }
        virtual public decimal? POSTPAYCONSUMPTIONCONTINUELIMIT { get; set; }
        virtual public int? POSTPAYLIMITUNIT { get; set; }
        virtual public bool DEFAULTAPPLICATIONFLAG { get; set; }
        virtual public DateTime? UPDATEDATE { get; set; }
        virtual public DealerInfo DealerInfo { get; set; }
        // AnnaM: 20140409
        virtual public bool CONTINUEBYSMS
        {
            get;
            set;
        }
    }
}
