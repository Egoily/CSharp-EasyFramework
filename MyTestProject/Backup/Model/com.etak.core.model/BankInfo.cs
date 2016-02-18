using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    /// Bank account information
    /// </summary>
    [DataContract]
    [Serializable]
    public class BankInfo
    {
        
        private CustomerInfo _CustomerInfo;
            
        private int? _BankID = null;        
        private string _BankCode;        
        private string _BankName;        
        private string _BankNumber;        
        private string _Owner;        
        private string _City;        
        private int? _CountryID = null;        
        private string _IBAN;        
        private string _Swift;        
        private string _CVC;        
        private string _ABI;        
        private string _CAB;        
        private string _ValidDate;        
        private DateTime? _CreateDate = null;        
        private int? _UserID = null;        
        private string _AccountCode;



        #region FK object
        /// <summary>
        /// The customer to which this Bank information relates to
        /// </summary>
        public virtual CustomerInfo CustomerInfo
        {
            get { return _CustomerInfo; }
            set { _CustomerInfo = value; }
        }
        #endregion



        #region Attribute
        /// <summary>
        /// Unique Id of the entity
        /// </summary>
        public virtual int? BankID
        {
            get { return _BankID; }
            set { _BankID = value; }
        }

        public virtual string BankCode
        {
            get { return _BankCode; }
            set { _BankCode = value; }
        }

        /// <summary>
        /// The name of the bank
        /// </summary>
        public virtual string BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }

        /// <summary>
        /// The number of the office branch of the bank
        /// </summary>
        public virtual string BankNumber
        {
            get { return _BankNumber; }
            set { _BankNumber = value; }
        }

        /// <summary>
        /// The name of the owner of the account
        /// </summary>
        public virtual string Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }

        /// <summary>
        /// The city of the branch of the bank where this account is registered
        /// </summary>
        public virtual string City
        {
            get { return _City; }
            set { _City = value; }
        }

        /// <summary>
        /// The country of the branch of the bank where this account is registered
        /// </summary>
        public virtual int? CountryID
        {
            get { return _CountryID; }
            set { _CountryID = value; }
        }

        /// <summary>
        /// IBAN Code for the bank account
        /// </summary>
        public virtual string IBAN
        {
            get { return _IBAN; }
            set { _IBAN = value; }
        }

        /// <summary>
        /// Swift Code for the bank account
        /// </summary>
        public virtual string Swift
        {
            get { return _Swift; }
            set { _Swift = value; }
        }

        public virtual string CVC
        {
            get { return _CVC; }
            set { _CVC = value; }
        }

        public virtual string ABI
        {
            get { return _ABI; }
            set { _ABI = value; }
        }

        public virtual string CAB
        {
            get { return _CAB; }
            set { _CAB = value; }
        }

        public virtual string ValidDate
        {
            get { return _ValidDate; }
            set { _ValidDate = value; }
        }

        /// <summary>
        /// THe date in which this entity was created (not the account in the bank)
        /// </summary>
        public virtual DateTime? CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        /// <summary>
        /// The ETAK user that created this entity
        /// </summary>
        public virtual int? UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public virtual string AccountCode
        {
            get { return _AccountCode; }
            set { _AccountCode = value; }
        }

        /// <summary>
        /// The start date in which this bank account can be used
        /// </summary>
        public DateTime? StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// The enddate date in which this bank account can be used
        /// </summary>
        public DateTime? EndDate
        {
            get;
            set;
        }

        #endregion

        public virtual BankInfo Clone()
        {
            return this.MemberwiseClone() as BankInfo;
        }
    }
}
