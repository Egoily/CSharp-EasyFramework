using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class TransferBalanceInfo
    {
        public virtual int TransferID{ get; set; }
        public virtual string FromMsisdn { get; set; }
        public virtual string ToMsisdn { get; set; }
        public virtual int FromCustomerID { get; set; }
        public virtual int ToCustomerID { get; set; }
        public virtual int MvnoID { get; set; }
        public virtual int DealerID { get; set; }
        public virtual string PinCode { get; set; }
        public virtual int ConfirmFlag { get; set; }
        public virtual decimal TransferAmount { get; set; }
        public virtual DateTime ExpireDate { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual long? ApplyLogID { get; set; }
        public virtual long? ConfirmLogID { get; set; }
        public virtual string Description { get; set; }
        public virtual int UserID { get; set; }
        
    }
}
