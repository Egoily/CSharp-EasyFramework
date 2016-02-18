using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2010-7-16 10:48:20
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2010-7-16 10:48:20
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2010-7-16 10:48:20
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2010-7-16 10:48:20
    /// </summary>
    [DataContract]
    [Serializable]
    public class CrmCustomersFfInitialChargeInfo
    {
        #region 构造函数
        public CrmCustomersFfInitialChargeInfo()
        { }

        public CrmCustomersFfInitialChargeInfo(int CUSTOMERID, int CATEGORYID, DateTime? CHARGEDATE)
        {
            this._CUSTOMERID = CUSTOMERID;
            this._CATEGORYID = CATEGORYID;
            this._CHARGEDATE = CHARGEDATE;
        }
        #endregion

        #region 成员
        private int _CUSTOMERID;
        private int _CATEGORYID;
        private DateTime? _CHARGEDATE;
        #endregion


        #region 属性
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is CrmCustomersFfInitialChargeInfo)
            {
                CrmCustomersFfInitialChargeInfo other = obj as CrmCustomersFfInitialChargeInfo;
                return this.CustomerId == other.CustomerId && this.CategoryId == other.CategoryId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CustomerId
        {
            get { return _CUSTOMERID; }
            set { _CUSTOMERID = value; }
        }

        public int CategoryId
        {
            get { return _CATEGORYID; }
            set { _CATEGORYID = value; }
        }

        public DateTime? ChargeDate
        {
            get { return _CHARGEDATE; }
            set { _CHARGEDATE = value; }
        }

        #endregion


    }
}
