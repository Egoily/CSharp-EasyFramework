using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2009-9-16 16:54:50
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2009-9-16 16:54:50
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2009-9-16 16:54:50
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2009-9-16 16:54:50
    /// </summary>
    [DataContract]
    [Serializable]
    public class CrmCustomersSpecificNumberInfo : PromotionModelBase
    {
        #region 构造函数
        public CrmCustomersSpecificNumberInfo()
        { }

        public CrmCustomersSpecificNumberInfo(int CUSTOMERID, RmSpecificNumberGroupInfo rm_specificnumber_group, string SPECIFICNUMBER, DateTime? STARTDATE, DateTime? ENDDATE)
        {
            this._CUSTOMERID = CUSTOMERID;
            this._RM_SPECIFICNUMBER_GROUP = rm_specificnumber_group;
            this._SPECIFICNUMBER = SPECIFICNUMBER;
            this._STARTDATE = STARTDATE;
            this._ENDDATE = ENDDATE;
        }
        #endregion

        #region 成员
        private int _CustomersSpecificNumberId;
        private int _CUSTOMERID;
        protected RmSpecificNumberGroupInfo _RM_SPECIFICNUMBER_GROUP;
        private string _SPECIFICNUMBER;
        private DateTime? _STARTDATE;
        private DateTime? _ENDDATE;
        #endregion

        public override bool Equals(object obj)
        {
            CrmCustomersSpecificNumberInfo value = obj as CrmCustomersSpecificNumberInfo;
            if (value != null && value.CustomerId != this.CustomerId && value.SpecificNumber == this.SpecificNumber)
            {
                return true;
            }
            return false;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region 属性
        public int CustomersSpecificNumberId
        {
            get { return _CustomersSpecificNumberId; }
            set { _CustomersSpecificNumberId = value; }
        }

        public int CustomerId
        {
            get { return _CUSTOMERID; }
            set { _CUSTOMERID = value; }
        }

        int _GroupId;
        public int GroupId
        {
            get
            {
                return _GroupId;
            }
            set
            {
                _GroupId = value;
            }
        }
        //public RmSpecificNumberGroupInfo RmSpecificNumberGroupInfo
        //{
        //    get { return _RM_SPECIFICNUMBER_GROUP; }
        //    set { _RM_SPECIFICNUMBER_GROUP = value; }
        //}

        public string SpecificNumber
        {
            get { return _SPECIFICNUMBER; }
            set { _SPECIFICNUMBER = value; }
        }

        public DateTime? StartDate
        {
            get { return _STARTDATE; }
            set { _STARTDATE = value; }
        }

        public DateTime? EndDate
        {
            get { return _ENDDATE; }
            set { _ENDDATE = value; }
        }

        #endregion

    }
}
