using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2009/9/15 AM 10:54:20
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2009/9/15 AM 10:54:20
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2009/9/15 AM 10:54:20
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2009/9/15 AM 10:54:20
    /// </summary>
    [DataContract]
    [Serializable]
    public class RMOperatorsInfo
    {
        #region 构造函数
        public RMOperatorsInfo()
        { }

        public RMOperatorsInfo(string OPERATORCODE, string OPERANAME, string COUNTRYNAME, string networkOperator, int? individualQuota, int? multQuota, int? changeWindowQuota,string status,DateTime? statusDate,string cnOperatorCode)
        {
            this._OPERATORCODE = OPERATORCODE;
            this._OPERANAME = OPERANAME;
            this._COUNTRYNAME = COUNTRYNAME;
            this.networkOperator = networkOperator;
            this.individualQuota = individualQuota;
            this.multQuota = multQuota;
            this.changeWindowQuota = changeWindowQuota;
            this.status = status;
            this.statusDate = statusDate;
            this.cnOperatorCode = cnOperatorCode;
        }
        #endregion

        #region 成员
        private string _OPERATORCODE;
        private string _OPERANAME;
        private string _COUNTRYNAME;
        private string networkOperator;
        private int? individualQuota;
        private int? multQuota;
        private int? changeWindowQuota;
        private string status;
        private DateTime? statusDate;
        private string cnOperatorCode;

        #endregion


        #region 属性
        public virtual string OperatorCode
        {
            get { return _OPERATORCODE; }
            set { _OPERATORCODE = value; }
        }

        public virtual string OperaName
        {
            get { return _OPERANAME; }
            set { _OPERANAME = value; }
        }

        public virtual string CountryName
        {
            get { return _COUNTRYNAME; }
            set { _COUNTRYNAME = value; }
        }

        public virtual string NetworkOperator
        {
            get { return networkOperator; }
            set { networkOperator = value; }
        }

        public virtual int? IndividualQuota
        {
            get { return individualQuota; }
            set { individualQuota = value; }
        }

        public virtual int? MultQuota
        {
            get { return multQuota; }
            set { multQuota = value; }
        }

        public virtual int? ChangeWindowQuota
        {
            get { return changeWindowQuota; }
            set { changeWindowQuota = value; }
        }

        public virtual string Status
        {
            get { return status; }
            set { status = value; }
        }

        public virtual DateTime? StatusDate
        {
            get { return statusDate; }
            set { statusDate = value; }
        }

        public virtual string CNOperatorCode
        {
            get { return cnOperatorCode; }
            set { cnOperatorCode = value; }
        }


        #endregion

    }
}
