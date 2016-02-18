using System;

namespace com.etak.core.model.resource
{
    /// <summary>
    /// SimCard DTO Object
    /// </summary>
    public class SimCardDTO
    {
        /// <summary>
        /// The Dealer Id corresponding with the Simcard
        /// </summary>
        public virtual int? DealerID { get; set; }
        /// <summary>
        /// The ICCID
        /// </summary>
        public virtual string ICCID { get; set; }

        /// <summary>
        /// The IMSI1 of the Simcard
        /// </summary>
        public virtual string IMSI1 { get; set; }
        /// <summary>
        /// The IMSI2 of the Simcard
        /// </summary>
        public virtual string IMSI2 { get; set; }
        /// <summary>
        /// The IMSI3 of the Simcard
        /// </summary>
        public virtual string IMSI3 { get; set; }
        /// <summary>
        /// The IMSI4 of the Simcard
        /// </summary>
        public virtual string IMSI4 { get; set; }
        /// <summary>
        /// The IMSI5 of the Simcard
        /// </summary>
        public virtual string IMSI5 { get; set; }
        /// <summary>
        /// The IMSI6 of the Simcard
        /// </summary>
        public virtual string IMSI6 { get; set; }
        /// <summary>
        /// The IMSI7 of the Simcard
        /// </summary>
        public virtual string IMSI7 { get; set; }
        /// <summary>
        /// The IMSI8 of the Simcard
        /// </summary>
        public virtual string IMSI8 { get; set; }
        /// <summary>
        /// The IMSI9 of the Simcard
        /// </summary>
        public virtual string IMSI9 { get; set; }
        /// <summary>
        /// The IMSI10 of the Simcard
        /// </summary>
        public virtual string IMSI10 { get; set; }
        /// <summary>
        /// The IMSI11 of the Simcard
        /// </summary>
        public virtual string IMSI11 { get; set; }
        /// <summary>
        /// The IMSI12 of the Simcard
        /// </summary>
        public virtual string IMSI12 { get; set; }
        /// <summary>
        /// The IMSI13 of the Simcard
        /// </summary>
        public virtual string IMSI13 { get; set; }
        /// <summary>
        /// The IMSI14 of the Simcard
        /// </summary>
        public virtual string IMSI14 { get; set; }
        /// <summary>
        /// The IMSI15 of the Simcard
        /// </summary>
        public virtual string IMSI15 { get; set; }
        /// <summary>
        /// The Msisdn of the Simcard
        /// </summary>
        public virtual string MSISDN { get; set; }
        /// <summary>
        /// Pin 1 of the Simcard
        /// </summary>
        public virtual string PIN1 { get; set; }
        /// <summary>
        /// Pin 2 of the Simcard
        /// </summary>
        public virtual string PIN2 { get; set; }
        /// <summary>
        /// PUK1 of the Simcard
        /// </summary>
        public virtual string PUK1 { get; set; }
        /// <summary>
        /// PUK2 of the Simcard
        /// </summary>
        public virtual string PUK2 { get; set; }
        /// <summary>
        /// KI of the Simcard
        /// </summary>
        public virtual string KI { get; set; }
        /// <summary>
        /// OPC of the Simcard
        /// </summary>
        public virtual string OPC { get; set; }
        /// <summary>
        /// KIC_0F of the Simcard
        /// </summary>
        public virtual string KIC_0F { get; set; }
        /// <summary>
        /// KID_0F of the Simcard
        /// </summary>
        public virtual string KID_0F { get; set; }
        /// <summary>
        /// KIK_0F of the Simcard
        /// </summary>
        public virtual string KIK_0F { get; set; }
        /// <summary>
        /// ADM1
        /// </summary>
        public virtual string ADM1 { get; set; }
        /// <summary>
        /// ADM2
        /// </summary>
        public virtual string ADM2 { get; set; }
        /// <summary>
        /// ACC of the Simcard
        /// </summary>
        public virtual string ACC { get; set; }
        /// <summary>
        /// The current Status of the Simcard
        /// </summary>
        public virtual int? Status { get; set; }
        /// <summary>
        /// The Name of the Algorithm
        /// </summary>
        public virtual string AlgorithmName { get; set; }
        /// <summary>
        /// ID of the Manufacturer
        /// </summary>
        public virtual string ManufacturerID { get; set; }
        /// <summary>
        /// The type of the SIM
        /// </summary>
        public virtual int? SIMType { get; set; }
        /// <summary>
        /// AlgoID
        /// </summary>
        public virtual int? AlgoID { get; set; }

        /// <summary>
        /// Type of Activation
        /// </summary>
        public virtual int? ActivateType { get; set; }
        /// <summary>
        /// Manufacture date
        /// </summary>
        public virtual DateTime? ManufactureDate { get; set; }
        /// <summary>
        /// When was the last status change
        /// </summary>
        public virtual DateTime? ChangeStatusDate { get; set; }
        /// <summary>
        /// Assign Status ID
        /// </summary>
        public virtual int? AssignStatusId { get; set; }
        /// <summary>
        /// KIC2 
        /// </summary>
        public virtual string KIC2 { get; set; }
        /// <summary>
        /// KID2 of the Simcard
        /// </summary>
        public virtual string KID2 { get; set; }
        /// <summary>
        /// KIK2 of the Simcard
        /// </summary>
        public virtual string KIK2 { get; set; }
        /// <summary>
        /// EKI_Index of the Simcard
        /// </summary>
        public virtual int EKI_Index { get; set; }
        /// <summary>
        /// Is Provision flag
        /// </summary>
        public virtual bool IsProvision { get; set; }
        /// <summary>
        /// Temporaty IMSI of the Simcard
        /// </summary>
        public virtual string TEMPORARY_IMSI { get; set; }
        /// <summary>
        /// Temporary MSISDN of the Simcard
        /// </summary>
        public virtual string TEMPORARY_MSISDN { get; set; }
        /// <summary>
        /// The type of encryption
        /// </summary>
        public virtual int? ManufacturerEncryptionType { get; set; }
    }
}
