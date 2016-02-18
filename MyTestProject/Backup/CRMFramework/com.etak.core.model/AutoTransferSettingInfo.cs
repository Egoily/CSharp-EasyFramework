using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class AutoTransferSettingInfo
    {
        private int? _ID = null;
        public int? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private int? _MVNOID = null;
        public int? MVNOID
        {
            get { return _MVNOID; }
            set { _MVNOID = value; }
        }

        private int? _DEALERID = null;
        public int? DEALERID
        {
            get { return _DEALERID; }
            set { _DEALERID = value; }
        }

        private int? _CUSTOMERID = null;
        public int? CUSTOMERID
        {
            get { return _CUSTOMERID; }
            set { _CUSTOMERID = value; }
        }

        private string _MSISDNFROM;
        public string MSISDNFROM
        {
            get { return _MSISDNFROM; }
            set { _MSISDNFROM = value; }
        }

        private string _MSISDNTO;
        public string MSISDNTO
        {
            get { return _MSISDNTO; }
            set { _MSISDNTO = value; }
        }

        private decimal? _AMOUNT = null;
        public decimal? AMOUNT
        {
            get { return _AMOUNT; }
            set { _AMOUNT = value; }
        }

        private decimal? _AMOUNTWITHOUTTAX = null;
        public decimal? AMOUNTWITHOUTTAX
        {
            get { return _AMOUNTWITHOUTTAX; }
            set { _AMOUNTWITHOUTTAX = value; }
        }

        private int? _TAXCODE = null;
        public int? TAXCODE
        {
            get { return _TAXCODE; }
            set { _TAXCODE = value; }
        }

        private int? _PRIORITY = null;
        public int? PRIORITY
        {
            get { return _PRIORITY; }
            set { _PRIORITY = value; }
        }

        private int? _STATUS = null;
        public int? STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        private int? _USERID = null;
        public int? USERID
        {
            get { return _USERID; }
            set { _USERID = value; }
        }

        private DateTime? _CREATEDATE = null;
        public DateTime? CREATEDATE
        {
            get { return _CREATEDATE; }
            set { _CREATEDATE = value; }
        }

        private DateTime? _UPDATEDATE = null;
        public DateTime? UPDATEDATE
        {
            get { return _UPDATEDATE; }
            set { _UPDATEDATE = value; }
        }
    }
}
