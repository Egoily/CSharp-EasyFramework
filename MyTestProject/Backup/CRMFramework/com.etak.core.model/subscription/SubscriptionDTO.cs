using System;

namespace com.etak.core.model.subscription
{
    /// <summary>
    /// DTO class for Subscription entity
    /// </summary>
    public class SubscriptionDTO
    {
        /// <summary>
        /// Unique id of the subscription
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// MSISDN of the subcription (Unique identifier of the numberInfo used)
        /// </summary>
        public String MSISDN { get; set; }

        /// <summary>
        /// imsi of the subcription
        /// </summary>
        public String IMSI { get; set; }

        /// <summary>
        /// Id of the customer owning the subscription
        /// </summary>
        public Int32 CustomerId { get; set; }
        
        /// <summary>
        /// Id of the fiscal unit where the subscription is registered
        /// </summary>
        public Int32 OperatorInfo { get; set; }

        /// <summary>
        /// Identifier of the simcard that in which this subscription is installed
        /// </summary>
        public  String ICCId { get; set; }

        /// <summary>
        /// Current status of the subscription
        /// </summary>
        public virtual int Status { get; set; }

        /// <summary>
        /// Date in which the service was used for first time
        /// </summary>
        public DateTime? FirstUsed { get; set; }
        
        /// <summary>
        /// Date in which the service was used for last time
        /// </summary>
        public DateTime? LastUsed { get; set; }

        /// <summary>
        /// The starting date for the validity period of the subscription
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// The ending date for the validity period of the subscription
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Date in which the subscription was created.
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Date of the last satus change of the subscription
        /// </summary>
        public DateTime? ChangeStatusDate { get; set; }

        public DateTime? LastConsumeDate { get; set; }
        public DateTime? FrozenDate { get; set; }

        /// <summary>
        /// The targed date in which the subscription will become inactive.
        /// </summary>
        public DateTime? ActiveDeadlineDate { get; set; }

        /// <summary>
        /// Id of the user that created the subscription
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Tokenized string of all the ODB Barrings (Operator determined barrings) of the subscription
        /// </summary>
        public string ODBMask { get; set; }

        /// <summary>
        /// Tokenized string of all the TS (Tele services) of the subscription
        /// </summary>
        public string TeleServiceList { get; set; }

        /// <summary>
        /// Tokenized string of all the BS (Bearer services) of the subscription
        /// </summary>
        public string BearerServiceList { get; set; }

        public virtual string Remarks { get; set; }
        public virtual string MsIsdnAlertInd { get; set; }
       
        public virtual bool UssdAllowed { get; set; }
        public virtual int? CBSubsoption { get; set; }
        public virtual string CBPassword { get; set; }
        public virtual int? CBWrongAttempts { get; set; }
        public virtual int? Calculation { get; set; }

       
        
        /// <summary>
        /// PUK recover number of the subscription
        /// </summary>
        public virtual string PUK { get; set; }

        /// <summary>
        /// Forward to number rule in the HLR
        /// </summary>
        public virtual string FTNRule { get; set; }
       
        public virtual int? PINInvalidTimes { get; set; }
        public virtual int? PINInvalidTimesTotal { get; set; }
        public virtual bool DeleteFlag { get; set; }
        public virtual int? OCPPlmnTemplateId { get; set; }
        public virtual int? ProvisionId { get; set; }
        public virtual int? NAM { get; set; }
        public virtual int? MainNumberStatus { get; set; }
        public virtual int? MainNumberVoiceMailStatus { get; set; }
        public virtual string MobileType { get; set; }
        public virtual string PortedNO { get; set; }
        public virtual string TempNO { get; set; }
        public virtual bool WelcomeSMS { get; set; }
    }
}
