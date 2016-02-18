using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class SettingInfo
    {
        #region 构造函数
        public SettingInfo()
        {
            this.SettingDetailInfos = new List<SettingDetailInfo>();
            this.SmsTempletInfos = new List<SmsTempletInfo>();
            //SchedulUnit = "PerDay";
        }

        public SettingInfo(int settingId, EventInfo eventInfo, string description, int dealerId, bool enabled, int schedulIntervalDays, DateTime schedulOriginalDate, string schedulTime, string schedulUnit, DateTime createDate)
        {
            this._SettingId = settingId;
            this._EventInfo = eventInfo;
            this._Description = description;
            this._DealerId = dealerId;
            this._Enabled = enabled;
            this._SchedulIntervalDays = schedulIntervalDays;
            this._SchedulOriginalDate = schedulOriginalDate;
            this._SchedulTime = schedulTime;
            this._SchedulUnit = schedulUnit;
            this._CreateDate = createDate;

            this.SettingDetailInfos = new List<SettingDetailInfo>();
            this.SmsTempletInfos = new List<SmsTempletInfo>();
        }
        #endregion

        #region 成员
        protected IList<SettingDetailInfo> _SettingDetailInfos;
        protected IList<SmsTempletInfo> _SmsTempletInfos;
        private int _SettingId;
        protected EventInfo _EventInfo;
        private string _Description;
        private int _DealerId;
        private bool _Enabled;
        private int _SchedulIntervalDays;
        private DateTime _SchedulOriginalDate;
        private string _SchedulTime;
        private string _SchedulUnit;
        private DateTime _CreateDate;
        private int _scheduleType;
        #endregion

        #region 属性
        public virtual int SettingId
        {
            get { return _SettingId; }
            set { _SettingId = value; }
        }

        public virtual EventInfo EventInfo
        {
            get { return _EventInfo; }
            set { _EventInfo = value; }
        }

        public virtual string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public virtual int DealerId
        {
            get { return _DealerId; }
            set { _DealerId = value; }
        }

        public virtual bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        public virtual int SchedulIntervalDays
        {
            get { return _SchedulIntervalDays; }
            set { _SchedulIntervalDays = value; }
        }

        public virtual DateTime SchedulOriginalDate
        {
            get { return _SchedulOriginalDate; }
            set { _SchedulOriginalDate = value; }
        }

        public virtual string SchedulTime
        {
            get { return _SchedulTime; }
            set { _SchedulTime = value; }
        }

        public virtual string SchedulUnit
        {
            get { return _SchedulUnit; }
            set { _SchedulUnit = value; }
        }

        public virtual DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        public virtual int ScheduleType
        {
            get { return _scheduleType; }
            set { _scheduleType = value; }
        }

        public virtual IList<SettingDetailInfo> SettingDetailInfos
        {
            get { return _SettingDetailInfos; }
            set { _SettingDetailInfos = value; }
        }

        public virtual IList<SmsTempletInfo> SmsTempletInfos
        {
            get { return _SmsTempletInfos; }
            set { _SmsTempletInfos = value; }
        }

        #endregion

        public virtual SettingInfo Clone()
        {
            SettingInfo settingInfo = this.MemberwiseClone() as SettingInfo;
            return settingInfo;
        }
    }
}
