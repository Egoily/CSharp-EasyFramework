using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class BlockMsisdnInfo
    {
        private long _id;
        private int? _dealerID;
        private int? _customerID;
        private string _msisdn;
        private string _topuptype;
        private int? _topuptimes;
        private decimal? _topupamount;
        private string _timeunit;
        private int? _createuserid;
        private DateTime? _createdate;
        //2011-9-15 winson add for new requirement
        private int? _statusID;
        private int? _ruletype;
        private decimal? _amount;
        private int? _issend;
        //2011-9-15 end by  winson

        public long ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public int? DealerID
        {
            get { return _dealerID; }
            set { _dealerID = value; }
        }

        public int? CustomerID
        {
            get { return _customerID; }
            set { _customerID = value; }
        }

        public string MSISDN
        {
            get { return _msisdn; }
            set { _msisdn = value; }
        }

        public string TopUpType
        {
            get { return _topuptype; }
            set { _topuptype = value; }
        }

        public int? TopUpTimes
        {
            get { return _topuptimes; }
            set { _topuptimes = value; }
        }

        public decimal? TopUpAmount
        {
            get { return _topupamount; }
            set { _topupamount = value; }
        }

        public string TimeUnit
        {
            get { return _timeunit; }
            set { _timeunit = value; }
        }

        /// <summary>
        /// Id of the user that created the entity
        /// </summary>
        public int? CreateUserID
        {
            get { return _createuserid; }
            set { _createuserid = value; }
        }

        public DateTime? CreateDate
        {
            get { return _createdate; }
            set { _createdate = value; }
        }

        //2011-9-15 winson add for new requirement   

        public int? StatusID
        {
            get { return _statusID; }
            set { _statusID = value; }
        }

        public int? Ruletype
        {
            get { return _ruletype; }
            set { _ruletype = value; }
        }

        public decimal? Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public int? Issend
        {
            get { return _issend; }
            set { _issend = value; }
        }

        //2011-9-15 end by  winson


    }
}
