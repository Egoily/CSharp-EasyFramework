using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2009-9-16 16:54:46
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2009-9-16 16:54:46
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2009-9-16 16:54:46
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2009-9-16 16:54:46
    /// </summary>
    [DataContract]
    [Serializable]
    public class CrmCustomersFfHistoryInfo : PromotionModelBase
    {
        #region 构造函数
        public CrmCustomersFfHistoryInfo()
        { }

        public CrmCustomersFfHistoryInfo(int CUSTOMERID, string MSISDN, int ACTIONTYPE, DateTime ACTIONDATE, string CURRENTNUMBER1, string CURRENTNUMBER2, int USERID)
        {
            this._CUSTOMERID = CUSTOMERID;
            this._MSISDN = MSISDN;
            this._ACTIONTYPE = ACTIONTYPE;
            this._ACTIONDATE = ACTIONDATE;
            this._CURRENTNUMBER1 = CURRENTNUMBER1;
            this._CURRENTNUMBER2 = CURRENTNUMBER2;
            this._USERID = USERID;
        }
        #endregion

        #region 成员
        private int _CUSTOMERID;
        private string _MSISDN;
        private int _ACTIONTYPE;
        private DateTime? _ACTIONDATE = null;
        private string _CURRENTNUMBER1;
        private string _CURRENTNUMBER2;
        private int _USERID;
        #endregion


        #region 属性
        public int CustomerId
        {
            get { return _CUSTOMERID; }
            set { _CUSTOMERID = value; }
        }

        public string MsIsdn
        {
            get { return _MSISDN; }
            set { _MSISDN = value; }
        }

        public int ActionType
        {
            get { return _ACTIONTYPE; }
            set { _ACTIONTYPE = value; }
        }

        public DateTime? ActionDate
        {
            get { return _ACTIONDATE; }
            set { _ACTIONDATE = value; }
        }

        public string CurrentNumber1
        {
            get { return _CURRENTNUMBER1; }
            set { _CURRENTNUMBER1 = value; }
        }

        public string CurrentNumber2
        {
            get { return _CURRENTNUMBER2; }
            set { _CURRENTNUMBER2 = value; }
        }

        public int UserId
        {
            get { return _USERID; }
            set { _USERID = value; }
        }

        #endregion

    }
}
