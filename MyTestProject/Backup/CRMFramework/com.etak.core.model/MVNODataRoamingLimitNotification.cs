using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class MVNODataRoamingLimitNotification
    {
        private int? _ID = null;
        private int? _NotificationType = null;
        private int? _StatusId;
        private int? _LimitUnit;
        private decimal? _ThresholdLimit;
        private int? _TemplateId;
        private int? _SMSType;
        private int? _ThresholdSubType;
        private DateTime? _UpdateDate;
        virtual public int? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        virtual public int? TemplateId
        {
            get { return _TemplateId; }
            set { _TemplateId = value; }
        }


        virtual public int? SMSType
        {
            get { return _SMSType; }
            set { _SMSType = value; }
        }

        virtual public int? ThresholdSubType
        {
            get { return _ThresholdSubType; }
            set { _ThresholdSubType = value; }
        }

        virtual public DealerInfo DealerInfo { get; set; }
        virtual public int? NotificationType
        {
            get { return _NotificationType; }
            set { _NotificationType = value; }
        }

        virtual public int? StatusId
        {
            get { return _StatusId; }
            set { _StatusId = value; }
        }

        virtual public int? LimitUnit
        {
            get { return _LimitUnit; }
            set { _LimitUnit = value; }
        }

        virtual public decimal? ThresholdLimit
        {
            get { return _ThresholdLimit; }
            set { _ThresholdLimit = value; }
        }

        virtual public DateTime? UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }
    }
}
