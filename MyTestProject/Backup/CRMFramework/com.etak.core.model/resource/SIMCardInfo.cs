using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class SIMCardInfo
    {
        public SIMCardInfo()
        {
            IsProvision = false;
        }

        public virtual DealerInfo Dealer { get; set; }
        public virtual string ICCID { get; set; }
        public virtual string IMSI1 { get; set; }
        public virtual string IMSI2 { get; set; }
        public virtual string IMSI3 { get; set; }
        public virtual string IMSI4 { get; set; }
        public virtual string IMSI5 { get; set; }
        public virtual string IMSI6 { get; set; }
        public virtual string IMSI7 { get; set; }
        public virtual string IMSI8 { get; set; }
        public virtual string IMSI9 { get; set; }
        public virtual string IMSI10 { get; set; }
        public virtual string IMSI11 { get; set; }
        public virtual string IMSI12 { get; set; }
        public virtual string IMSI13 { get; set; }
        public virtual string IMSI14 { get; set; }
        public virtual string IMSI15 { get; set; }
        public virtual string MSISDN { get; set; }
        public virtual string PIN1 { get; set; }
        public virtual string PIN2 { get; set; }
        public virtual string PUK1 { get; set; }
        public virtual string PUK2 { get; set; }
        public virtual string KI { get; set; }
        public virtual string OPC { get; set; }
        public virtual string KIC_0F { get; set; }
        public virtual string KID_0F { get; set; }
        public virtual string KIK_0F { get; set; }
        public virtual string ADM1 { get; set; }
        public virtual string ADM2 { get; set; }
        public virtual string ACC { get; set; }
        public virtual int? Status { get; set; }
        public virtual string AlgorithmName { get; set; }
        public virtual string ManufacturerID { get; set; }
        public virtual int? SIMType { get; set; }
        public virtual int? AlgoID { get; set; }
        public virtual int? ActivateType { get; set; }
        public virtual DateTime? ManufactureDate { get; set; }
        public virtual DateTime? ChangeStatusDate { get; set; }
        public virtual int? AssignStatusId { get; set; }
        public virtual string KIC2 { get; set; }
        public virtual string KID2 { get; set; }
        public virtual string KIK2 { get; set; }

        public virtual int EKI_Index { get; set; }

        public virtual bool IsProvision { get; set; }
        /// <summary>
        /// Temporay IMSI
        /// </summary>
        public virtual string TEMPORARY_IMSI { get; set; }
        /// <summary>
        /// Temporary MSISDN
        /// </summary>
        public virtual string TEMPORARY_MSISDN { get; set; }
        
        public virtual int? ManufacturerEncryptionType { get; set; }
    }
}
