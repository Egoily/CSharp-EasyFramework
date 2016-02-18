using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2011/4/12 10:54:05
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2011/4/12 10:54:05
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2011/4/12 10:54:05
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2011/4/12 10:54:05
    /// </summary>
    [DataContract]
    [Serializable]
    public class RoamingSmsConfigInfo
    {
        #region 构造函数
        public RoamingSmsConfigInfo()
        { }

        public RoamingSmsConfigInfo(int ID, int DEALERID, int BundleID, int ZoneID, DateTime? StartDate, DateTime? EndDate, int SMSTemplateId, int SMSType, int Status, string Description)
        {
            this._ID = ID;
            this._DEALERID = DEALERID;
            this._BundleID = BundleID;
            this._ZoneID = ZoneID;
            this._StartDate = StartDate;
            this._EndDate = EndDate;
            this._SMSTemplateId = SMSTemplateId;
            this._SMSType = SMSType;
            this._Status = Status;
            this._Description = Description;
        }
        #endregion

        #region 成员
        private int _ID;
        private int _DEALERID;
        private int _BundleID;
        private int _ZoneID;
        private DateTime? _StartDate;
        private DateTime? _EndDate;
        private int _SMSTemplateId;
        private int _SMSType;
        private int _Status;
        private string _Description;

        private int _Priority;
        private int _CountryCode;
        #endregion


        #region 属性
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int DealerID
        {
            get { return _DEALERID; }
            set { _DEALERID = value; }
        }

        public int BundleID
        {
            get { return _BundleID; }
            set { _BundleID = value; }
        }

        public int ZoneID
        {
            get { return _ZoneID; }
            set { _ZoneID = value; }
        }

        public DateTime? StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        public DateTime? EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        public int SMSTemplateId
        {
            get { return _SMSTemplateId; }
            set { _SMSTemplateId = value; }
        }

        public int SMSType
        {
            get { return _SMSType; }
            set { _SMSType = value; }
        }

        public int NetType { get; set; }

        public int PaymentType { get; set; }

        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }


        public int Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }

        public int CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }
        #endregion

    }
}
