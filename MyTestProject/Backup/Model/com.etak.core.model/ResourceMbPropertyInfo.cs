using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2010-10-16 13:06:57
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2010-10-16 13:06:57
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2010-10-16 13:06:57
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2010-10-16 13:06:57
    /// </summary>
    [DataContract]
    [Serializable]
    public class ResourceMbPropertyInfo : ICloneable
    {
        #region 成员
        private int? _PROPERTYID;
        private int _ResourceID;
        private bool _SMSSTATUS;
        private decimal? mAXDAILYDATAONROAMING;
        private int? _PREPAIDBALANCELOWERSENDSMSSTATUS;
        private int? _PREPAIDBALANCEEXHAUSIONSENDSMSSTATUS;
        private int? _POSTPAIDCREDITLOWERSENDSMSSTATUS;
        private int? _POSTPAIDCREDITEXHAUSIONSENDSMSSTATUS;
        #endregion


        #region 属性

        public virtual int? PREPAIDBALANCELOWERSENDSMSSTATUS
        {
            get { return _PREPAIDBALANCELOWERSENDSMSSTATUS; }
            set { _PREPAIDBALANCELOWERSENDSMSSTATUS = value; }
        }
        public virtual int? PREPAIDBALANCEEXHAUSIONSENDSMSSTATUS
        {
            get { return _PREPAIDBALANCEEXHAUSIONSENDSMSSTATUS; }
            set { _PREPAIDBALANCEEXHAUSIONSENDSMSSTATUS = value; }
        }
        public virtual int? POSTPAIDCREDITLOWERSENDSMSSTATUS
        {
            get { return _POSTPAIDCREDITLOWERSENDSMSSTATUS; }
            set { _POSTPAIDCREDITLOWERSENDSMSSTATUS = value; }
        }
        public virtual int? POSTPAIDCREDITEXHAUSIONSENDSMSSTATUS
        {
            get { return _POSTPAIDCREDITEXHAUSIONSENDSMSSTATUS; }
            set { _POSTPAIDCREDITEXHAUSIONSENDSMSSTATUS = value; }
        }

        public virtual int? PROPERTYID
        {
            get { return _PROPERTYID; }
            set { _PROPERTYID = value; }
        }

        public virtual int ResourceID
        {
            get { return _ResourceID; }
            set { _ResourceID = value; }
        }

        public virtual bool SMSSTATUS
        {
            get { return _SMSSTATUS; }
            set { _SMSSTATUS = value; }
        }

        public virtual decimal? MAXDAILYDATAONROAMING
        {
            get { return mAXDAILYDATAONROAMING; }
            set{mAXDAILYDATAONROAMING=value;}
        }


        bool _DeleateFlag;
        public virtual bool DeleteFlag
        {
            get { return _DeleateFlag; }
            set { _DeleateFlag = value; }
        }

        #endregion

        public virtual object Clone()
        {
            ResourceMbPropertyInfo _ResourceMbPropertyInfo = new ResourceMbPropertyInfo();
            _ResourceMbPropertyInfo.MAXDAILYDATAONROAMING = this.MAXDAILYDATAONROAMING;
            _ResourceMbPropertyInfo.POSTPAIDCREDITEXHAUSIONSENDSMSSTATUS = this.POSTPAIDCREDITEXHAUSIONSENDSMSSTATUS;
            _ResourceMbPropertyInfo.POSTPAIDCREDITLOWERSENDSMSSTATUS = this.POSTPAIDCREDITLOWERSENDSMSSTATUS;
            _ResourceMbPropertyInfo.PREPAIDBALANCEEXHAUSIONSENDSMSSTATUS = this.PREPAIDBALANCEEXHAUSIONSENDSMSSTATUS;
            _ResourceMbPropertyInfo.PREPAIDBALANCELOWERSENDSMSSTATUS = this.PREPAIDBALANCELOWERSENDSMSSTATUS;
            _ResourceMbPropertyInfo.SMSSTATUS = this.SMSSTATUS;
            _ResourceMbPropertyInfo.PROPERTYID = this.PROPERTYID;
            return _ResourceMbPropertyInfo;
        }

    }
}
