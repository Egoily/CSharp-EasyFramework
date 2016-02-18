using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2011/4/8 10:17:19
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2011/4/8 10:17:19
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2011/4/8 10:17:19
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2011/4/8 10:17:19
    /// </summary>
    [DataContract]
    [Serializable]
    public class RoamingBlackListInfo
    {
        #region 构造函数
        public RoamingBlackListInfo()
        { }

        public RoamingBlackListInfo(int BLACKLISTID, int DEALERID, string MSISDN, string DESCRIPTION, int CUSTOMERID)
        {
            this._BLACKLISTID = BLACKLISTID;
            this._DEALERID = DEALERID;
            this._MSISDN = MSISDN;
            this._DESCRIPTION = DESCRIPTION;
            this._CUSTOMERID = CUSTOMERID;  // 2014-2-28 winson add for iCR_14_01_0017_ Welcome Roaming SMS Blacklist
        }
        #endregion

        #region 成员
        private int _BLACKLISTID;
        private int _DEALERID;
        private string _MSISDN;
        private string _DESCRIPTION;
        private int _CUSTOMERID;// 2014-2-28 winson add for iCR_14_01_0017_ Welcome Roaming SMS Blacklist
        #endregion


        #region 属性
        public int BlackListID
        {
            get { return _BLACKLISTID; }
            set { _BLACKLISTID = value; }
        }

        public int DealerID
        {
            get { return _DEALERID; }
            set { _DEALERID = value; }
        }

        public string MsIsdn
        {
            get { return _MSISDN; }
            set { _MSISDN = value; }
        }

        public string Description
        {
            get { return _DESCRIPTION; }
            set { _DESCRIPTION = value; }
        }

        public DateTime? CreateDate
        {
            get;
            set;
        }
        // 2014-2-28 winson add for iCR_14_01_0017_ Welcome Roaming SMS Blacklist
        public int CustomerID
        {
            get { return _CUSTOMERID; }
            set { _CUSTOMERID = value; }
        }
        #endregion

    }
}
