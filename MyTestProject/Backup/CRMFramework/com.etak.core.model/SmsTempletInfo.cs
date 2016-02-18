using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class SmsTempletInfo
    {
        #region 构造函数
        public SmsTempletInfo()
        { }

        public SmsTempletInfo(int templetId, SettingInfo settingInfo, int languageId, string content)
        {
            this._TempletId = templetId;
            this._SettingInfo = settingInfo;
            this._LanguageId = languageId;
            this._Content = content;
        }
        #endregion

        #region 成员
        private int _TempletId;
        private SettingInfo _SettingInfo;
        private int _LanguageId;
        private string _Content;
        #endregion


        #region 属性
        public virtual int TempletId
        {
            get { return _TempletId; }
            set { _TempletId = value; }
        }

        public virtual SettingInfo SettingInfo
        {
            get { return _SettingInfo; }
            set { _SettingInfo = value; }
        }

        public virtual int LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
        }

        public virtual string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }

        #endregion

    }
}
