using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2011/4/8 10:17:20
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2011/4/8 10:17:20
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2011/4/8 10:17:20
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2011/4/8 10:17:20
    /// </summary>
    [DataContract]
    [Serializable]
    public class RoamingSmsHistoryInfo
    {
        #region 构造函数
        public RoamingSmsHistoryInfo()
        { }

        public RoamingSmsHistoryInfo(long ID, string MSISDN, string OMV, string OperatorRoaming, string ZONECODE, DateTime? CREATEDATE)
        {
            this._ID = ID;
            this._MSISDN = MSISDN;
            this._OMV = OMV;
            this._OperatorRoaming = OperatorRoaming;
            this._ZONECODE = ZONECODE;
            this._CREATEDATE = CREATEDATE;
        }
        #endregion

        #region 成员
        private long _ID;
        private string _MSISDN;
        private string _OMV;
        private string _OperatorRoaming;
        private string _ZONECODE;
        private DateTime? _CREATEDATE;
        #endregion


        #region 属性
        public long ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string MsIsdn
        {
            get { return _MSISDN; }
            set { _MSISDN = value; }
        }

        public string OMV
        {
            get { return _OMV; }
            set { _OMV = value; }
        }

        public string OperatorRoaming
        {
            get { return _OperatorRoaming; }
            set { _OperatorRoaming = value; }
        }

        public string ZoneCode
        {
            get { return _ZONECODE; }
            set { _ZONECODE = value; }
        }

        public int BundleId
        {
            get;
            set;
        }

        public DateTime? CreateDate
        {
            get { return _CREATEDATE; }
            set { _CREATEDATE = value; }
        }

        public string SMS
        {
            get;
            set;
        }

        public string NetType
        {
            get;
            set;
        }

        public string PaymentType
        {
            get;
            set;
        }

        public string CountryName
        {
            get;
            set;
        }

        public string OperatorName
        {
            get;
            set;
        }
        #endregion

    }
}
