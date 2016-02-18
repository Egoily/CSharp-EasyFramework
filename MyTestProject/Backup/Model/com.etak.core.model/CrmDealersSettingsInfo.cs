using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2009-9-18 18:13:59
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2009-9-18 18:13:59
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2009-9-18 18:13:59
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2009-9-18 18:13:59
    /// </summary>
    [DataContract]
    [Serializable]
    public class CrmDealersSettingsInfo
    {
        #region 构造函数
        public CrmDealersSettingsInfo()
        { }

        public CrmDealersSettingsInfo(int DEALERID, int SETTINGID, int LANGUAGEID, int ITEMSEQ, int ITEMID, int ITEMINDEX, string ITEMVALUE, string ITEMLINK, string ITEMDESCRIPTION)
        {
            this._DEALERID = DEALERID;
            this._SETTINGID = SETTINGID;
            this._LANGUAGEID = LANGUAGEID;
            this._ITEMSEQ = ITEMSEQ;
            this._ITEMID = ITEMID;
            this._ITEMINDEX = ITEMINDEX;
            this._ITEMVALUE = ITEMVALUE;
            this._ITEMLINK = ITEMLINK;
            this._ITEMDESCRIPTION = ITEMDESCRIPTION;
        }
        #endregion

        #region 成员
        private int _DEALERID;
        private int _SETTINGID;
        private int _LANGUAGEID;
        private int _ITEMSEQ;
        private int _ITEMID;
        private int _ITEMINDEX;
        private string _ITEMVALUE;
        private string _ITEMLINK;
        private string _ITEMDESCRIPTION;
        #endregion


        #region 属性
        public int DealerId
        {
            get { return _DEALERID; }
            set { _DEALERID = value; }
        }

        public int SettingId
        {
            get { return _SETTINGID; }
            set { _SETTINGID = value; }
        }

        public int LanguageId
        {
            get { return _LANGUAGEID; }
            set { _LANGUAGEID = value; }
        }

        public int ItemSeq
        {
            get { return _ITEMSEQ; }
            set { _ITEMSEQ = value; }
        }

        public int ItemId
        {
            get { return _ITEMID; }
            set { _ITEMID = value; }
        }

        public int ItemIndex
        {
            get { return _ITEMINDEX; }
            set { _ITEMINDEX = value; }
        }

        public string ItemValue
        {
            get { return _ITEMVALUE; }
            set { _ITEMVALUE = value; }
        }

        public string ItemLink
        {
            get { return _ITEMLINK; }
            set { _ITEMLINK = value; }
        }

        public string ItemDescription
        {
            get { return _ITEMDESCRIPTION; }
            set { _ITEMDESCRIPTION = value; }
        }

        #endregion

    }
}
