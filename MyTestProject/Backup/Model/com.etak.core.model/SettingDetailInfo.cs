using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class SettingDetailInfo
    {
        #region 构造函数
        public SettingDetailInfo()
        {
        }

        public SettingDetailInfo(int detailId, int subItemId, SettingInfo settingInfo, string description, int interval, string unit, bool enabled, bool sendReport, bool sendSms, DateTime createDate)
        {
            this._DetailId = detailId;
            this._SubItemId = subItemId;
            this._SettingInfo = settingInfo;
            this._Description = description;
            this._Interval = interval;
            this._Unit = unit;
            this._Enabled = enabled;
            this._SendReport = sendReport;
            this._SendSms = sendSms;
            this._CreateDate = createDate;
        }
        #endregion

        #region 成员
        private int _DetailId;
        private int _SubItemId;
        protected SettingInfo _SettingInfo;
        private string _Description;
        private int _Interval;
        private string _Unit;
        private bool _Enabled;
        private bool _SendReport;
        private bool _SendSms;
        private DateTime _CreateDate;
        #endregion


        #region 属性
        public virtual int DetailId
        {
            get { return _DetailId; }
            set { _DetailId = value; }
        }


        public virtual int SubItemId
        {
            get { return _SubItemId; }
            set { _SubItemId = value; }
        }

        public virtual SettingInfo SettingInfo
        {
            get { return _SettingInfo; }
            set { _SettingInfo = value; }
        }

        public virtual string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public virtual int Interval
        {
            get { return _Interval; }
            set { _Interval = value; }
        }

        public virtual string Unit
        {
            get { return _Unit; }
            set { _Unit = value; }
        }

        public virtual bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        public virtual bool SendReport
        {
            get { return _SendReport; }
            set { _SendReport = value; }
        }

        public virtual bool SendSms
        {
            get { return _SendSms; }
            set { _SendSms = value; }
        }

        public virtual DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        #endregion
    }
}