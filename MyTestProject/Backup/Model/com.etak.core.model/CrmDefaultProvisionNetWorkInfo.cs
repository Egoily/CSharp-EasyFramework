using System;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2009-7-28 11:42:06
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2009-7-28 11:42:06
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2009-7-28 11:42:06
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2009-7-28 11:42:06
    /// </summary>
    [Serializable]
    public class CrmDefaultProvisionNetWorkInfo : ModelBase
    {
        #region 构造函数
        public CrmDefaultProvisionNetWorkInfo()
        { }

        public CrmDefaultProvisionNetWorkInfo(int NETWORKID, CrmDefaultProvisionInfo esCrmDefaultProvision, int NAM, int NetworkType, string APN, bool MMS, bool WAP, bool OBOPRE, bool OBOPRI, int PDPCONTEXTID, int PDPTYPE, string PDPADDRESS, bool VPLMNADDRESSALLOWED, int QOSALLOCATIONRETENTIONPRIORITY, int QOSTRAFFICCLASS, int QOSDELIVERYERRONEOUSSDU, int QOSMAXSDUSIZE, int QOSDELIVERYORDER, int QOSMAXBITRATEDOWN, int QOSMAXBITRATEUP, int QOSRESIDUALBER, int QOSSDUERRORRATIO, int QOSTRANSFERDELAY, int QOSTRAFFICHANDLINGPRIORITY, int QOSGUARANTEEDBITRATEUP, int QOSGUARANTEEDBITRATEDOWN, int QOSSIGNALLINGINDICATION)
        {
            this._NETWORKID = NETWORKID;
            this._esCrmDefaultProvision = esCrmDefaultProvision;
            this._NAM = NAM;
            this._NetworkType = NetworkType;
            this._APN = APN;
            this._MMS = MMS;
            this._WAP = WAP;
            this._OBOPRE = OBOPRE;
            this._OBOPRI = OBOPRI;
            this._PDPCONTEXTID = PDPCONTEXTID;
            this._PDPTYPE = PDPTYPE;
            this._PDPADDRESS = PDPADDRESS;
            this._VPLMNADDRESSALLOWED = VPLMNADDRESSALLOWED;
            this._QOSALLOCATIONRETENTIONPRIORITY = QOSALLOCATIONRETENTIONPRIORITY;
            this._QOSTRAFFICCLASS = QOSTRAFFICCLASS;
            this._QOSDELIVERYERRONEOUSSDU = QOSDELIVERYERRONEOUSSDU;
            this._QOSMAXSDUSIZE = QOSMAXSDUSIZE;
            this._QOSDELIVERYORDER = QOSDELIVERYORDER;
            this._QOSMAXBITRATEDOWN = QOSMAXBITRATEDOWN;
            this._QOSMAXBITRATEUP = QOSMAXBITRATEUP;
            this._QOSRESIDUALBER = QOSRESIDUALBER;
            this._QOSSDUERRORRATIO = QOSSDUERRORRATIO;
            this._QOSTRANSFERDELAY = QOSTRANSFERDELAY;
            this._QOSTRAFFICHANDLINGPRIORITY = QOSTRAFFICHANDLINGPRIORITY;
            this._QOSGUARANTEEDBITRATEUP = QOSGUARANTEEDBITRATEUP;
            this._QOSGUARANTEEDBITRATEDOWN = QOSGUARANTEEDBITRATEDOWN;
            this._QOSSIGNALLINGINDICATION = QOSSIGNALLINGINDICATION;
        }
        #endregion

        #region 成员
        private int _NETWORKID;
        protected CrmDefaultProvisionInfo _esCrmDefaultProvision;
        private int _NAM;
        private int _NetworkType;
        private string _APN;
        private string _PreAPN;
        private bool _MMS;
        private bool _WAP;
        private bool _OBOPRE;
        private bool _OBOPRI;
        private int _PDPCONTEXTID;
        private int _PDPTYPE;
        private string _PDPADDRESS;
        private bool _VPLMNADDRESSALLOWED;
        private int _QOSALLOCATIONRETENTIONPRIORITY;
        private int _QOSTRAFFICCLASS;
        private int _QOSDELIVERYERRONEOUSSDU;
        private int _QOSMAXSDUSIZE;
        private int _QOSDELIVERYORDER;
        private int _QOSMAXBITRATEDOWN;
        private int _QOSMAXBITRATEUP;
        private int _QOSRESIDUALBER;
        private int _QOSSDUERRORRATIO;
        private int _QOSTRANSFERDELAY;
        private int _QOSTRAFFICHANDLINGPRIORITY;
        private int _QOSGUARANTEEDBITRATEUP;
        private int _QOSGUARANTEEDBITRATEDOWN;
        private int _QOSSIGNALLINGINDICATION;

        private bool _IsDelete = false;

       
        #endregion


        #region 属性
        public virtual int NETWORKID
        {
            get { return _NETWORKID; }
            set { _NETWORKID = value; }
        }

        public virtual CrmDefaultProvisionInfo CrmDefaultProvisionInfo
        {
            get { return _esCrmDefaultProvision; }
            set { _esCrmDefaultProvision = value; }
        }

        public virtual int NAM
        {
            get { return _NAM; }
            set { _NAM = value; }
        }

        public virtual int NetworkType
        {
            get { return _NetworkType; }
            set { _NetworkType = value; }
        }

        public virtual string APN
        {
            get { return _APN; }
            set { _APN = value; }
        }

        public virtual string PreAPN
        {
            get { return _PreAPN; }
            set { _PreAPN = value; }
        }

        public virtual bool MMS
        {
            get { return _MMS; }
            set { _MMS = value; }
        }

        public virtual bool WAP
        {
            get { return _WAP; }
            set { _WAP = value; }
        }

        public virtual bool OBOPRE
        {
            get { return _OBOPRE; }
            set { _OBOPRE = value; }
        }

        public virtual bool OBOPRI
        {
            get { return _OBOPRI; }
            set { _OBOPRI = value; }
        }

        public virtual int PDPCONTEXTID
        {
            get { return _PDPCONTEXTID; }
            set { _PDPCONTEXTID = value; }
        }

        public virtual int PDPTYPE
        {
            get { return _PDPTYPE; }
            set { _PDPTYPE = value; }
        }

        public virtual string PDPADDRESS
        {
            get { return _PDPADDRESS; }
            set { _PDPADDRESS = value; }
        }

        public virtual bool VPLMNADDRESSALLOWED
        {
            get { return _VPLMNADDRESSALLOWED; }
            set { _VPLMNADDRESSALLOWED = value; }
        }

        public virtual int QOSALLOCATIONRETENTIONPRIORITY
        {
            get { return _QOSALLOCATIONRETENTIONPRIORITY; }
            set { _QOSALLOCATIONRETENTIONPRIORITY = value; }
        }

        public virtual int QOSTRAFFICCLASS
        {
            get { return _QOSTRAFFICCLASS; }
            set { _QOSTRAFFICCLASS = value; }
        }

        public virtual int QOSDELIVERYERRONEOUSSDU
        {
            get { return _QOSDELIVERYERRONEOUSSDU; }
            set { _QOSDELIVERYERRONEOUSSDU = value; }
        }

        public virtual int QOSMAXSDUSIZE
        {
            get { return _QOSMAXSDUSIZE; }
            set { _QOSMAXSDUSIZE = value; }
        }

        public virtual int QOSDELIVERYORDER
        {
            get { return _QOSDELIVERYORDER; }
            set { _QOSDELIVERYORDER = value; }
        }

        public virtual int QOSMAXBITRATEDOWN
        {
            get { return _QOSMAXBITRATEDOWN; }
            set { _QOSMAXBITRATEDOWN = value; }
        }

        public virtual int QOSMAXBITRATEUP
        {
            get { return _QOSMAXBITRATEUP; }
            set { _QOSMAXBITRATEUP = value; }
        }

        public virtual int QOSRESIDUALBER
        {
            get { return _QOSRESIDUALBER; }
            set { _QOSRESIDUALBER = value; }
        }

        public virtual int QOSSDUERRORRATIO
        {
            get { return _QOSSDUERRORRATIO; }
            set { _QOSSDUERRORRATIO = value; }
        }

        public virtual int QOSTRANSFERDELAY
        {
            get { return _QOSTRANSFERDELAY; }
            set { _QOSTRANSFERDELAY = value; }
        }

        public virtual int QOSTRAFFICHANDLINGPRIORITY
        {
            get { return _QOSTRAFFICHANDLINGPRIORITY; }
            set { _QOSTRAFFICHANDLINGPRIORITY = value; }
        }

        public virtual int QOSGUARANTEEDBITRATEUP
        {
            get { return _QOSGUARANTEEDBITRATEUP; }
            set { _QOSGUARANTEEDBITRATEUP = value; }
        }

        public virtual int QOSGUARANTEEDBITRATEDOWN
        {
            get { return _QOSGUARANTEEDBITRATEDOWN; }
            set { _QOSGUARANTEEDBITRATEDOWN = value; }
        }

        public virtual int QOSSIGNALLINGINDICATION
        {
            get { return _QOSSIGNALLINGINDICATION; }
            set { _QOSSIGNALLINGINDICATION = value; }
        }


        public virtual bool IsDelete
        {
            get { return _IsDelete; }
            set { _IsDelete = value; }
        }
        #endregion

    }
}
