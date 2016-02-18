using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class CustomerDataRoamingLimitHistory
    {
        private long? _historyID;
        private int? _customerID;
        private string _msisdn;
        private int? dealerID;
        private decimal? _oldRoamingLimit;
        private decimal? _currentRoamingLimit;
        private decimal? _oldContinueSUM;
        private decimal? _currentContinueSUM;
        private decimal? _roamingLimitCounter;
        private DateTime _createDate;
        private int? _updateBy;
        private int? _historyType;
        private int? _statusID;
        private string message;

        public virtual long? HistoryID
        {
            get { return _historyID; }
            set { _historyID = value; }
        }

        public virtual int? CustomerID
        {
            get { return _customerID; }
            set { _customerID = value; }
        }

        public virtual int? DealerID
        {
            get { return dealerID; }
            set { dealerID = value; }
        }

        public virtual string Msisdn
        {
            get { return _msisdn; }
            set { _msisdn = value; }
        }
        /// <summary>
        /// Roaming Limit before change
        /// </summary>
        public virtual decimal? OldRoamingLimit
        {
            get { return _oldRoamingLimit; }
            set { _oldRoamingLimit = value; }
        }

        /// <summary>
        /// Roaming Limit after change
        /// </summary>
        public virtual decimal? CurrentRoamingLimit
        {
            get { return _currentRoamingLimit; }
            set { _currentRoamingLimit = value; }
        }

        /// <summary>
        /// ContinueSUM before change
        /// </summary>
        public virtual decimal? OldContinueSUM
        {
            get { return _oldContinueSUM; }
            set { _oldContinueSUM = value; }
        }
        /// <summary>
        /// ContinueSUM after change
        /// </summary>
        public virtual decimal? CurrentContinueSUM
        {
            get { return _currentContinueSUM; }
            set { _currentContinueSUM = value; }
        }

        public virtual decimal? RoamingLimitCounter
        {
            get { return _roamingLimitCounter; }
            set { _roamingLimitCounter = value; }
        }

        public virtual DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }

        public virtual int? UpdateBy
        {
            get { return _updateBy; }
            set { _updateBy = value; }
        }
        /// <summary>
        ///1.	Change Data Roaming Limit Success
        ///2.	Change Data Roaming Limit Failed
        ///3.	Interuption
        /// </summary>
        public virtual int? HistoryType
        {
            get { return _historyType; }
            set { _historyType = value; }
        }
        /// <summary>
        /// 0 Success, 1 failed
        /// </summary>
        public virtual int? StatusID
        {
            get { return _statusID; }
            set { _statusID = value; }
        }
        /// <summary>
        /// Error message when failed
        /// </summary>
        public virtual string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}
