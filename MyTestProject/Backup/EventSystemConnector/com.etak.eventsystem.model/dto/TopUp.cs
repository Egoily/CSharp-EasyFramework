using System;
using System.Runtime.Serialization;

namespace com.etak.eventsystem.model.dto
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]   
    public enum TopUpChannels
    {
        [EnumMember()]
        IVR,
        [EnumMember()]
        USSD,
        [EnumMember()]
        API,
        [EnumMember()]
        CRM,
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class TopUp : LoadeableEntity
    {
        [DataMember(EmitDefaultValue = false)]
        public virtual Int64 ID { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual int? DealerID { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual Int64? OperationCode { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual String RefrenceCode { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual String Msisdn { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual int? TopupType { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual String Pin { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual int? Status { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual DateTime? StatusDate { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual decimal? Amount { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual int? CurrencyID { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual int? TaxCode { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual long? PreOperationCode { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual decimal? PreBalance { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual String OperationCause { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual String Reamrk { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual bool Used { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual DateTime? UsedTime { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual String PaymentCardNumber { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual String ExpirationDate { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual decimal? AmountWithTax { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual int? UserID { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual String VcEncrypt { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual int? CustomerID { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual String UserName { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual String BonusConfigIds { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual String BonusConfigName { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual decimal? TopupAmount { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual decimal? BonusAmount { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual decimal? BonusAmountWithTAX { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual decimal? NormalAmount { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual decimal? NormalAmountWithTAX { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual string BonusPromotionIds { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual String BonusPromotionName { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual decimal? TopupBonusVAT { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual decimal? TopupBonus { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual decimal? BonusForNormal { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual decimal? BonusForNormalVAT { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual decimal? BonusForPromoted { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual decimal? BonusForPromotedVAT { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual int? TopupAccount { get; set; }
        //Added by Benny 2014-07-04
        [DataMember(EmitDefaultValue = false)]
        public virtual string VoucherCode { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public int PaymentTypeID { get; set; }
        

    }
}
