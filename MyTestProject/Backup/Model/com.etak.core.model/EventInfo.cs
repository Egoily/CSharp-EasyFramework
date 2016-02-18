using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class EventInfo
    {
        #region 构造函数
        public EventInfo()
        { }

        public EventInfo(int eventId, string eventCode, string eventName, int eventType, string operateType)
        {
            this._EventId = eventId;
            this._EventCode = eventCode;
            this._EventName = eventName;
            this._EventType = eventType;
            this._OperateType = operateType;
        }
        #endregion

        #region 成员

        private IList<SettingInfo> _SettingInfos;
        private int _EventId;
        private string _EventCode;
        private string _EventName;
        private int _EventType;
        private string _OperateType;
        #endregion

        #region 属性

        public virtual int EventId 
        {
            get { return _EventId; }
            set { _EventId = value; }
        }

        public virtual string EventCode
        {
            get { return _EventCode; }
            set { _EventCode = value; }
        }

        public virtual string EventName
        {
            get { return _EventName; }
            set { _EventName = value; }
        }

        public virtual int EventType
        {
            get { return _EventType; }
            set { _EventType = value; }
        }

        public virtual string OperateType
        {
            get { return _OperateType; }
            set { _OperateType = value; }
        }

        public virtual IList<SettingInfo> SettingInfos
        {
            get { return _SettingInfos; }
            set { _SettingInfos = value; }
        }

        #endregion
    }
}
