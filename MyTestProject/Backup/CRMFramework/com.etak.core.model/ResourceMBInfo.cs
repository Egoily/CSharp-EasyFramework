using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    /// GSM subscription entity
    /// </summary>
    [DataContract]
    [Serializable]
    public class ResourceMBInfo
    {
        /// <summary>
        /// The customer which this subscription is asociated with
        /// </summary>
        public virtual CustomerInfo CustomerInfo { get; set; }

        /// <summary>
        /// The operator in which this subscription is registered
        /// </summary>
        public virtual DealerInfo OperatorInfo { get; set; }

        /// <summary>
        /// Unique Id of the subscription
        /// </summary>
        public virtual int? ResourceID { get; set; }

        /// <summary>
        /// MSISDN used for the subscription, Id of the NumberInfo
        /// </summary>
        public virtual string Resource { get; set; }


        public virtual string ICC { get; set; }
        public virtual string IMSI { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string MsIsdnAlertInd { get; set; }
        public virtual string ODBMask { get; set; }
        public virtual bool UssdAllowed { get; set; }
        public virtual int? CBSubsoption { get; set; }
        public virtual string CBPassword { get; set; }
        public virtual int? CBWrongAttempts { get; set; }
        public virtual int? Calculation { get; set; }
        public virtual int? StatusID { get; set; }
        public virtual DateTime? FirstUsed { get; set; }
        public virtual DateTime? LastUsed { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual int? UserID { get; set; }
        public virtual string PUK { get; set; }
        public virtual string TeleServiceList { get; set; }
        public virtual string BearerServiceList { get; set; }
        public virtual DateTime? ChangeStatusDate { get; set; }
        public virtual DateTime? LastConsumeDate { get; set; }
        public virtual DateTime? ActiveDeadlineDate { get; set; }
        public virtual int? PINInvalidTimes { get; set; }
        public virtual int? PINInvalidTimesTotal { get; set; }
        public virtual bool DeleteFlag { get; set; }
        public virtual int? OCPPlmnTemplateId { get; set; }
        public virtual int? ProvisionId { get; set; }
        public virtual int? NAM { get; set; }
        public virtual string FTNRule { get; set; }
        public virtual int? MainNumberStatus { get; set; }
        public virtual int? MainNumberVoiceMailStatus { get; set; }
        public virtual string MobileType { get; set; }
        public virtual string PortedNO { get; set; }
        public virtual string TempNO { get; set; }
        public virtual bool WelcomeSMS { get; set;}
        public virtual DateTime ? FrozenDate { get; set; }
        public virtual ResourceDIDInfo ResourceDIDInfo { get; set; }
        public virtual int? ActivationChannel { get; set; }
        public virtual int? BBRoamingStatus { get; set; }
        virtual public ResourceMBInfo Clone()
        {
            return this.MemberwiseClone() as ResourceMBInfo;
        }
        public ResourceMBInfo()
        {
            MobileType = string.Empty;
            PortedNO = string.Empty;
            TempNO = string.Empty;
            WelcomeSMS = false;
            CrmCustomersResourceMbPropertyInfo = new List<CrmCustomersResourceMbPropertyInfo>();

        }
        /// <summary>
        /// Resource Property
        /// </summary>
        public virtual IList<CrmCustomersResourceMbPropertyInfo> CrmCustomersResourceMbPropertyInfo { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            ResourceMBInfo cRes = obj as ResourceMBInfo;
            if (cRes == null)
                return false;

            return (cRes.ResourceID == this.ResourceID);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
