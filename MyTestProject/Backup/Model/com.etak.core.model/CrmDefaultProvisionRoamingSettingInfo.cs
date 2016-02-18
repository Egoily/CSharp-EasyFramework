using System;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2010-7-7 14:18:19
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2010-7-7 14:18:19
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2010-7-7 14:18:19
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2010-7-7 14:18:19
    /// </summary>
    [Serializable]
    public class CrmDefaultProvisionRoamingSettingInfo : ModelBase
    {
        #region 构造函数
        public CrmDefaultProvisionRoamingSettingInfo()
        { }

        public CrmDefaultProvisionRoamingSettingInfo(int SETTINGID, int DEALERID, string SMSTEXT, string SMSTEXT_CAMEL, bool ENABLEPREPAID, bool ENABLEPOSTPAID)
        {
            this._SETTINGID = SETTINGID;
            this._DEALERID = DEALERID;
            this._SMSTEXT = SMSTEXT;
            this._SMSTEXT_CAMEL = SMSTEXT_CAMEL;
            this._ENABLEPREPAID = ENABLEPREPAID;
            this._ENABLEPOSTPAID = ENABLEPOSTPAID;
        }
        #endregion

        #region 成员
        private int _SETTINGID;
        private int _DEALERID;
        private string _SMSTEXT;
        private string _SMSTEXT_CAMEL;
        private bool _ENABLEPREPAID;
        private bool _ENABLEPOSTPAID;
        #endregion


        #region 属性

        public int SettingId
        {
            get { return _SETTINGID; }
            set { _SETTINGID = value; }
        }

        public int DealerId
        {
            get { return _DEALERID; }
            set { _DEALERID = value; }
        }

        int _ProvisionId;
        public int ProvisionId
        {
            get
            {
                return _ProvisionId;
            }
            set
            {
                _ProvisionId = value;
            }
        }

        public string SmsText
        {
            get { return _SMSTEXT; }
            set { _SMSTEXT = value; }
        }

        public string SmsTextCamel
        {
            get { return _SMSTEXT_CAMEL; }
            set { _SMSTEXT_CAMEL = value; }
        }

        public bool EnablePrepaid
        {
            get { return _ENABLEPREPAID; }
            set { _ENABLEPREPAID = value; }
        }

        public bool EnablePostpaid
        {
            get { return _ENABLEPOSTPAID; }
            set { _ENABLEPOSTPAID = value; }
        }
        bool _Activated;
        public bool Activated
        {
            get
            {
                return _Activated;
            }
            set
            {
                _Activated = value;
            }
        }

        bool _EnableSmsCamel;
        public bool EnableSmsCamel
        {
            get
            {
                return _EnableSmsCamel;
            }
            set
            {
                _EnableSmsCamel = value;
            }
        }

        bool _EnableSms;
        public bool EnableSms
        {
            get
            {
                return _EnableSms;
            }
            set
            {
                _EnableSms = value;
            }
        }
        DealerInfo _DealerInfo;
        public DealerInfo DealerInfo
        {
            get
            {
                return _DealerInfo;
            }
            set
            {
                _DealerInfo = value;
            }
        }
        #endregion

    }
}
