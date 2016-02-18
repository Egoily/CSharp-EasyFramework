using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class ReportResourceInfo
    {
        #region 构造函数
        public ReportResourceInfo()
        { }

        public ReportResourceInfo(long reportId, int dealerId, int resourceType, string resourceNumber, int currentStatusId, int nextStatusId, DateTime createDate, int actionType, bool isSent,int userId)
        {
            this._ReportId = reportId;
            this._DealerId = dealerId;
            this._ResourceType = resourceType;
            this._ResourceNumber = resourceNumber;
            this._CurrentStatusId = currentStatusId;
            this._NextStatusId = nextStatusId;
            this._CreateDate = createDate;
            this._ActionType = actionType;
            this._IsSent = isSent;
            this._UserId = userId;
        }
        #endregion

        #region 成员
        private long _ReportId;
        private int _DealerId;
        private int _ResourceType;
        private string _ResourceNumber;
        private int _CurrentStatusId;
        private int _NextStatusId;
        private DateTime _CreateDate;
        private int _ActionType;
        private bool _IsSent;
        private int _UserId;
        #endregion


        #region 属性
        public virtual long ReportId
        {
            get { return _ReportId; }
            set { _ReportId = value; }
        }

        public virtual int DealerId
        {
            get { return _DealerId; }
            set { _DealerId = value; }
        }

        public virtual int ResourceType
        {
            get { return _ResourceType; }
            set { _ResourceType = value; }
        }

        public virtual string ResourceNumber
        {
            get { return _ResourceNumber; }
            set { _ResourceNumber = value; }
        }

        public virtual int CurrentStatusId
        {
            get { return _CurrentStatusId; }
            set { _CurrentStatusId = value; }
        }

        public virtual int NextStatusId
        {
            get { return _NextStatusId; }
            set { _NextStatusId = value; }
        }

        public virtual DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        public virtual int ActionType
        {
            get { return _ActionType; }
            set { _ActionType = value; }
        }

        public virtual bool IsSent
        {
            get { return _IsSent; }
            set { _IsSent = value; }
        }

        public virtual int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        #endregion

    }
}
