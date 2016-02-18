using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class ImportMasterInfo
    {
        private string _TransactionID;        
        private int? _OrderID = null;        
        private string _AlgorithmName;        
        private string _ManufacturerID;        
        private int? _LanguageID = null;        
        private string _INIPassword_DES;        
        private string _INIPassword_MD5;        
        private int? _PackageID = null;        
        private decimal? _CreditLimit = null;        
        private int? _ActivateType = null;        
        private int? _UserID = null;        
        private DateTime? _StartDateTime = null;        
        private int? _DealerID = null;        
        private int? _DetailsCount = null;        
        private DateTime? _ImportDate = null;
        //add 20120220
        private int _SimType = 0;

        #region Attribute
        public virtual string TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }

        public virtual int? OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }

        public virtual string AlgorithmName
        {
            get { return _AlgorithmName; }
            set { _AlgorithmName = value; }
        }

        public virtual string ManufacturerID
        {
            get { return _ManufacturerID; }
            set { _ManufacturerID = value; }
        }

        public virtual int? LanguageID
        {
            get { return _LanguageID; }
            set { _LanguageID = value; }
        }

        public virtual string INIPassword_DES
        {
            get { return _INIPassword_DES; }
            set { _INIPassword_DES = value; }
        }

        public virtual string INIPassword_MD5
        {
            get { return _INIPassword_MD5; }
            set { _INIPassword_MD5 = value; }
        }

        public virtual int? PackageID
        {
            get { return _PackageID; }
            set { _PackageID = value; }
        }

        public virtual decimal? CreditLimit
        {
            get { return _CreditLimit; }
            set { _CreditLimit = value; }
        }

        public virtual int? ActivateType
        {
            get { return _ActivateType; }
            set { _ActivateType = value; }
        }

        public virtual int? UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public virtual DateTime? StartDateTime
        {
            get { return _StartDateTime; }
            set { _StartDateTime = value; }
        }

        public virtual int? DealerID
        {
            get { return _DealerID; }
            set { _DealerID = value; }
        }

        public virtual int? DetailsCount
        {
            get { return _DetailsCount; }
            set { _DetailsCount = value; }
        }

        public virtual DateTime? ImportDate
        {
            get { return _ImportDate; }
            set { _ImportDate = value; }
        }

        public virtual int SimType
        {
            get { return _SimType; }
            set { _SimType = value; }
        }

        // Add by wood, 2013-08-06
        public virtual int? AlgoID { get; set; }
        public int ManufacturerEncryptionType { get; set; }
        #endregion
        
    }
}
