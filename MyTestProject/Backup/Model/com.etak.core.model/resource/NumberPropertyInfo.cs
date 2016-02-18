using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2009-7-11 14:44:45
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2009-7-11 14:44:45
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2009-7-11 14:44:45
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2009-7-11 14:56:45
    /// </summary>
    [DataContract]
    [Serializable]
    public class NumberPropertyInfo
    {

        #region 成员

        private string _Resource;
        private Boolean ? _HideCLI;
        private int? _StatusID;
        private int? _CoolDownPeriod;
        private DateTime? _CoolDownDurDate;
        private int? _LockedBy;
        private DateTime? _LockedDate;
        private string _TariffCode;
        private DateTime? _ChangeStatusDate;
        private int? _CreateUserID;
        private DateTime? _CreateDate;
        private int? _UpdateUserID;
        private DateTime? _UpdateDate;
        private int? _DataStatus;
        private string _locationid;
        private string _numberTypeID;
        private NumberInfo _numerNumberInfo;

        #endregion


        #region 属性

        public virtual string Resource
        {
            get { return _Resource; }
            set { _Resource = value; }
        }

        public virtual Boolean ? HideCLI
        {
            get { return _HideCLI; }
            set { _HideCLI = value; }
        }

        public virtual int? StatusID
        {
            get { return _StatusID; }
            set { _StatusID = value; }
        }

        public virtual int? CoolDownPeriod
        {
            get { return _CoolDownPeriod; }
            set { _CoolDownPeriod = value; }
        }

        public virtual DateTime? CoolDownDurDate
        {
            get { return _CoolDownDurDate; }
            set { _CoolDownDurDate = value; }
        }

        public virtual int? LockedBy
        {
            get { return _LockedBy; }
            set { _LockedBy = value; }
        }

        public virtual DateTime? LockedDate
        {
            get { return _LockedDate; }
            set { _LockedDate = value; }
        }

        public virtual string TariffCode
        {
            get { return _TariffCode; }
            set { _TariffCode = value; }
        }

        public virtual DateTime? ChangeStatusDate
        {
            get { return _ChangeStatusDate; }
            set { _ChangeStatusDate = value; }
        }

        public virtual int? CreateUserID
        {
            get { return _CreateUserID; }
            set { _CreateUserID = value; }
        }

        public virtual DateTime? CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        public virtual int? UpdateUserID
        {
            get { return _UpdateUserID; }
            set { _UpdateUserID = value; }
        }

        public virtual DateTime? UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }

        public virtual int? DataStatus
        {
            get { return _DataStatus; }
            set { _DataStatus = value; }
        }

        public virtual string locationid
        {
            get { return _locationid; }
            set { _locationid = value; }
        }

        public virtual string numbertypeid
        {
            get { return _numberTypeID; }
            set { _numberTypeID = value; }
        }

        public virtual NumberInfo NumberInfo
        {
            get { return _numerNumberInfo; }
            set { _numerNumberInfo = value; }
        }

        #endregion

   }
}
