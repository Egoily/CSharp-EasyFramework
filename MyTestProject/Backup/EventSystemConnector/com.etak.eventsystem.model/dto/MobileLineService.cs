using System;
using System.Runtime.Serialization;

namespace com.etak.eventsystem.model.dto
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class MobileLineService : LoadeableEntity
    {
        [DataMember(EmitDefaultValue = false)]
        public virtual int? CustomerID{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual int? ResourceID{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual string Resource{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual string ICC{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual string IMSI{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual string Remarks { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual string MsIsdnAlertInd { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual string ODBMask { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual bool UssdAllowed { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual int? CBSubsoption{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual string CBPassword{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual int? CBWrongAttempts{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual int? Calculation{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual int? StatusID{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual DateTime? FirstUsed{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual DateTime? LastUsed{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual DateTime? StartDate{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual DateTime? EndDate{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual DateTime? CreateDate{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual int? UserID{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual string PUK{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual string TeleServiceList{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual string BearerServiceList{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual DateTime? ChangeStatusDate{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual DateTime? LastConsumeDate{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual DateTime? ActiveDeadlineDate{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual int? PINInvalidTimes{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual int? PINInvalidTimesTotal{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual bool DeleteFlag{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual string OperatorCode{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual int? OCPPlmnTemplateId{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual int? ProvisionId{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual int? NAM{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual string FTNRule{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public int? MainNumberStatus{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public int? MainNumberVoiceMailStatus{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string MobileType{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string PortedNO{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string TempNO{ get; set; }
        [DataMember(EmitDefaultValue = false)]
        public bool WelcomeSMS{ get; set; }
    }
}