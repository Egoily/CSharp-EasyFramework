using System;
using System.Runtime.Serialization;

namespace com.etak.eventsystem.model.dto
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class CustomerProperty : LoadeableEntity
    {
        [DataMember()]
        public virtual int? CustomerID
        {
            get;
            set;
        }

        [DataMember()]
        public virtual int? PropertyID
        {
            get;
            set;
        }

        [DataMember()]
        public virtual int? CustomerTypeID
        {
            get;
            set;
        }

        [DataMember()]
        public virtual int? LanguageID
        {
            get;
            set;
        }

        [DataMember()]
        public virtual int? PaymentMethodID
        {
            get;
            set;
        }

        [DataMember()]
        public virtual int? BillingMethodID
        {
            get;
            set;
        }

        [DataMember()]
        public virtual bool ParentBilling
        {
            get;
            set;
        }

        [DataMember()]
        public virtual int? TrafficTypeID
        {
            get;
            set;
        }

        [DataMember()]
        public virtual int? TaxPlanID
        {
            get;
            set;
        }

        [DataMember()]
        public virtual bool InvoiceDetails
        {
            get;
            set;
        }

        [DataMember()]
        public virtual string Email
        {
            get;
            set;
        }

        [DataMember()]
        public virtual int? InvoiceDueDate
        {
            get;
            set;
        }

        [DataMember()]
        public virtual int? CountryCode
        {
            get;
            set;
        }

        [DataMember()]
        public virtual string CPSCode
        {
            get;
            set;
        }

        [DataMember()]
        public int? BillingEntity
        {
            get;
            set;
        }

        [DataMember()]
        public virtual int? WithDrawPeriod
        {
            get;
            set;
        }

        [DataMember()]
        public virtual string UserName
        {
            get;
            set;
        }

        [DataMember()]
        public virtual string PasswordDES
        {
            get;
            set;
        }

        [DataMember()]
        public virtual string PasswordMD5
        {
            get;
            set;
        }

        [DataMember()]
        public virtual DateTime? Birthday
        {
            get;
            set;
        }

        [DataMember()]
        public virtual DateTime? CreateDate
        {
            get;
            set;
        }

        [DataMember()]
        public virtual int? UserID
        {
            get;
            set;
        }

        [DataMember()]
        public virtual int? IDType
        {
            get;
            set;
        }

        [DataMember()]
        public virtual string IDNumber
        {
            get;
            set;
        }

        [DataMember()]
        public virtual int? CreditScore
        {
            get;
            set;
        }

        [DataMember()]
        public virtual decimal? OriginalDepositAmount
        {
            get;
            set;
        }

        [DataMember()]
        public virtual decimal? CurrentDepositAmount
        {
            get;
            set;
        }

        [DataMember()]
        public virtual DateTime? DepositDate
        {
            get;
            set;
        }

        [DataMember()]
        public virtual int? PendingStatus
        {
            get;
            set;
        }

        [DataMember()]
        public virtual int? LoyaltyPoint
        {
            get;
            set;
        }

        [DataMember()]
        public virtual bool AcceptNews
        {
            get;
            set;
        }

        [DataMember()]
        public virtual DateTime? LastLoyaltyDate
        {
            get;
            set;
        }

        [DataMember()]
        public virtual bool FF
        {
            get;
            set;
        }

        [DataMember()]
        public virtual int? BillingScenarioID
        {
            get;
            set;
        }

        [DataMember()]
        public virtual int? ContractPeriod
        {
            get;
            set;
        }

        [DataMember()]
        public virtual int? CreditTransferType
        {
            get;
            set;
        }

        [DataMember()]
        public virtual string VATNO
        {
            get;
            set;
        }

        [DataMember()]
        public virtual int? DepositStatus
        {
            get;
            set;
        }

        [DataMember()]
        public virtual decimal? CurrentDepositCredit
        {
            get;
            set;
        }

        [DataMember()]
        public virtual int? AutoTopupStatus
        {
            get;
            set;
        }

        [DataMember()]
        public virtual decimal? AutoTopupAmount
        {
            get;
            set;
        }

        [DataMember()]
        public virtual string ActionCode
        {
            get;
            set;
        }

        [DataMember()]
        public virtual string DMCEndUserId
        {
            get;
            set;
        }

        [DataMember()]
        public virtual DateTime? DocumentValidateTime
        {
            get;
            set;
        }

        [DataMember()]
        public virtual int? DocumentValidateStatus
        {
            get;
            set;
        }

        [DataMember()]
        public virtual string DocumentRejectReason
        {
            get;
            set;
        }

        [DataMember()]
        public int? LoginType
        {
            get;
            set;
        }

        [DataMember()]
        public DateTime? DateUpdated
        {
            get;
            set;
        }

        [DataMember()]
        public DateTime? IDExpiryDate
        {
            get;
            set;
        }

        [DataMember()]
        public string MailType
        {
            get;
            set;
        }

        [DataMember()]
        public string SubscriberType
        {
            get;
            set;
        }

        [DataMember()]
        public int? ContractNo
        {
            get;
            set;
        }

        [DataMember()]
        public int? ReferrerCustomerID
        {
            get;
            set;
        }

        [DataMember()]
        public int CustomerRole
        {
            get;
            set;
        }

        [DataMember()]
        public decimal LowBalanceQuantity { get; set; }

        [DataMember()]
        public long ServiceSwitch { get; set; }

        [DataMember()]
        public decimal Cashdeposit
        {
            get;
            set;
        }

        [DataMember()]
        public virtual decimal Roamingdeposit
        {
            get;
            set;
        }

         [DataMember()]
        public virtual String ExternalId { get; set; }

    }
}
