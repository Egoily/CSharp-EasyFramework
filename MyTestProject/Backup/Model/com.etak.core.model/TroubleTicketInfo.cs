using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class TroubleTicketInfo
    {
        public TroubleTicketInfo()
        { }

        public TroubleTicketInfo(int id, string customerName, string msisdn, string description, DateTime submitDate)
        {
            this._ID = id;
            this._CustomerName = customerName;
            this._MSISDN = msisdn;
            this._DESCRIPTION = description;
            this._REPORTTIME = submitDate;
        }

        public TroubleTicketInfo(int id,string department,string ttCode,string ticketNumber,string userName,DateTime reportTime,int classID,int priority,int status,DateTime etack)
        {
            _ID = id;
            _Department = department;
            _TTCODE = ttCode;
            _TICKETNUMBER = ticketNumber;
            _UserName = userName;
            _REPORTTIME = reportTime;
            _CLASSID = classID;
            _PRIORITY = priority;
            _STATUS = status;
            _ETACK = etack;
        }


        #region 
        private int? _ID = null;
        private string _TICKETNUMBER;
        private int? _CLASSID = null;
        private int? _PRIORITY = null;
        private int? _DEPTID = null;
        private int? _OPERATORID = null;
        private int? _HANDLEBY = null;
        private string _DESCRIPTION;
        private DateTime? _REPORTTIME = null;
        private DateTime? _UPDATETIME = null;
        private int? _STATUS = null;
        private int? _CUSTOMERID = null;
        private string _MSISDN;
        private string _DIALEDNO;
        private DateTime? _CALLTIME = null;
        private DateTime? _ETACK = new DateTime();
        private string _NETWORK;
        private string _CALLPLACE;
        private string _TTCODE;
        private int? _FUID = null;
        private string _CLIENTID;
        private string _SECONDNUMBER;

        public string SECONDNUMBER
        {
            get { return _SECONDNUMBER; }
            set { _SECONDNUMBER = value; }
        }
        private string _FLEXNUMBER;

        public string FLEXNUMBER
        {
            get { return _FLEXNUMBER; }
            set { _FLEXNUMBER = value; }
        }
        private string _INTERNALNO;

        public string INTERNALNO
        {
            get { return _INTERNALNO; }
            set { _INTERNALNO = value; }
        }
        private int? _CUSTTYPE = null;

        public int? CUSTTYPE
        {
            get { return _CUSTTYPE; }
            set { _CUSTTYPE = value; }
        }

        private string _EXTERNALCODE;

        public string EXTERNALCODE
        {
            get { return _EXTERNALCODE; }
            set { _EXTERNALCODE = value; }
        }

        private string _CustomerName;

        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }

        private string _Department;
        public string Department
        {
            get { return _Department; }
            set { _Department = value; }
        }

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        #endregion

        #region Attribute

        public int? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string TTCODE
        {
            get { return _TTCODE; }
            set { _TTCODE = value; }
        }

        public int? FUID
        {
            get { return _FUID; }
            set { _FUID = value; }
        }

        public string CLIENTID
        {
            get { return _CLIENTID; }
            set { _CLIENTID = value; }
        }

        public string TICKETNUMBER
        {
            get { return _TICKETNUMBER; }
            set { _TICKETNUMBER = value; }
        }

        public int? CLASSID
        {
            get { return _CLASSID; }
            set { _CLASSID = value; }
        }

        public int? PRIORITY
        {
            get { return _PRIORITY; }
            set { _PRIORITY = value; }
        }


        public int? DEPTID
        {
            get { return _DEPTID; }
            set { _DEPTID = value; }
        }
        public int? OPERATORID
        {
            get { return _OPERATORID; }
            set { _OPERATORID = value; }
        }

        public int? HANDLEBY
        {
            get { return _HANDLEBY; }
            set { _HANDLEBY = value; }
        }

        public string DESCRIPTION
        {
            get { return _DESCRIPTION; }
            set { _DESCRIPTION = value; }
        }

        public DateTime? REPORTTIME
        {
            get { return _REPORTTIME; }
            set { _REPORTTIME = value; }
        }

        public DateTime? ETACK
        {
            get { return _ETACK; }
            set { _ETACK = value; }
        }

        public DateTime? UPDATETIME
        {
            get { return _UPDATETIME; }
            set { _UPDATETIME = value; }
        }

        public int? STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public int? CUSTOMERID
        {
            get { return _CUSTOMERID; }
            set { _CUSTOMERID = value; }
        }

        public string MSISDN
        {
            get { return _MSISDN; }
            set { _MSISDN = value; }
        }

        public string DIALEDNO
        {
            get { return _DIALEDNO; }
            set { _DIALEDNO = value; }
        }

        public DateTime? CALLTIME
        {
            get { return _CALLTIME; }
            set { _CALLTIME = value; }
        }

        public string NETWORK
        {
            get { return _NETWORK; }
            set { _NETWORK = value; }
        }

        public string CALLPLACE
        {
            get { return _CALLPLACE; }
            set { _CALLPLACE = value; }
        }

        #endregion
    }
}
