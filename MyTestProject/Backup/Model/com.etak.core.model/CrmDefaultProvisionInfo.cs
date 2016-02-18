using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using com.etak.core.model.provisioning;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2009-7-28 11:42:09
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2009-7-28 11:42:09
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2009-7-28 11:42:09
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2009-7-28 11:42:09
    /// </summary>
    [DataContract]
    [Serializable]
    public class CrmDefaultProvisionInfo : ModelBase
    {
        #region 构造函数
        public CrmDefaultProvisionInfo()
        {
            Initialize();
        }

        private void Initialize()
        {

        }

        public CrmDefaultProvisionInfo(int PROVISIONID, string PROVISIONAME, int DEALERID, string HLRSERVERIP, int HLRSERVERPORT, string HLRSERVERUSER, string HLRSERVERPWD, string VMS, string HELPDESK, int TELEPHONENUMBERS, bool CLIP, bool CLIPOVERRIDE, bool CLIR, bool COLP, bool COLPOVERRIDE, bool COLR, int CLIRPRESENTATIONMODE, bool CALLWAITING, bool CALLHOLD, bool MULTIPARTY, bool ECT, bool CALLWAITING_BSG_1, bool CALLWAITING_BSG_6, bool CALLWAITING_BSG_7, bool CALLWAITING_BSG_8, bool CALLWAITING_BSG_12, bool AOCC, bool AOCI, string TELESERVICELIST, string BEARERSERVICELIST, string ODBMASK, string SERVICECODE, DateTime? CREATEDATE, int USERID, int CB_SUBSOPTION, string CB_PASSWORD, int CB_WRONGATTEMPTS)
            : this()
        {
            this._PROVISIONID = PROVISIONID;
            this._PROVISIONAME = PROVISIONAME;
            this._DEALERID = DEALERID;
            this._HLRSERVERIP = HLRSERVERIP;
            this._HLRSERVERPORT = HLRSERVERPORT;
            this._HLRSERVERUSER = HLRSERVERUSER;
            this._HLRSERVERPWD = HLRSERVERPWD;
            this._VMS = VMS;
            this._HELPDESK = HELPDESK;
            this._TELEPHONENUMBERS = TELEPHONENUMBERS;
            this._CLIP = CLIP;
            this._CLIPOVERRIDE = CLIPOVERRIDE;
            this._CLIR = CLIR;
            this._COLP = COLP;
            this._COLPOVERRIDE = COLPOVERRIDE;
            this._COLR = COLR;
            this._CLIRPRESENTATIONMODE = CLIRPRESENTATIONMODE;
            this._CALLWAITING = CALLWAITING;
            this._CALLHOLD = CALLHOLD;
            this._MULTIPARTY = MULTIPARTY;
            this._ECT = ECT;
            this._CALLWAITING_BSG_1 = CALLWAITING_BSG_1;
            this._CALLWAITING_BSG_6 = CALLWAITING_BSG_6;
            this._CALLWAITING_BSG_7 = CALLWAITING_BSG_7;
            this._CALLWAITING_BSG_8 = CALLWAITING_BSG_8;
            this._CALLWAITING_BSG_12 = CALLWAITING_BSG_12;
            this._AOCC = AOCC;
            this._AOCI = AOCI;
            this._TELESERVICELIST = TELESERVICELIST;
            this._BEARERSERVICELIST = BEARERSERVICELIST;
            this._ODBMASK = ODBMASK;
            this._SERVICECODE = SERVICECODE;
            this._CREATEDATE = CREATEDATE;
            this._USERID = USERID;
            this._CB_SUBSOPTION = CB_SUBSOPTION;
            this._CB_PASSWORD = CB_PASSWORD;
            this._CB_WRONGATTEMPTS = CB_WRONGATTEMPTS;
        }
        #endregion

        #region 成员
        private int _PROVISIONID;
        private string _PROVISIONAME;
        private int _DEALERID;
        private string _HLRSERVERIP;
        private int _HLRSERVERPORT;
        private string _HLRSERVERUSER;
        private string _HLRSERVERPWD;
        private string _VMS;
        private string _HELPDESK;
        private int _TELEPHONENUMBERS;
        private bool _CLIP;
        private bool _CLIPOVERRIDE;
        private bool _CLIR;
        private bool _COLP;
        private bool _COLPOVERRIDE;
        private bool _COLR;
        private int _CLIRPRESENTATIONMODE;
        private bool _CALLWAITING;
        private bool _CALLHOLD;
        private bool _MULTIPARTY;
        private bool _ECT;
        private bool _CALLWAITING_BSG_1;
        private bool _CALLWAITING_BSG_6;
        private bool _CALLWAITING_BSG_7;
        private bool _CALLWAITING_BSG_8;
        private bool _CALLWAITING_BSG_12;
        private bool _AOCC;
        private bool _AOCI;
        private string _TELESERVICELIST;
        private string _BEARERSERVICELIST;
        private string _ODBMASK;
        private string _SERVICECODE;
        private DateTime? _CREATEDATE = null;
        private int _USERID;
        private int _CB_SUBSOPTION;
        private string _CB_PASSWORD;
        private int _CB_WRONGATTEMPTS;
        string _prepayCsiTypeList;
        string _postpayCsiTypeList;
        private bool _IsDelete = false;
        #endregion


        #region 属性
        public virtual int PROVISIONID
        {
            get { return _PROVISIONID; }
            set { _PROVISIONID = value; }
        }

        public virtual string PROVISIONAME
        {
            get { return _PROVISIONAME; }
            set { _PROVISIONAME = value; }
        }

        public virtual int DEALERID
        {
            get { return _DEALERID; }
            set { _DEALERID = value; }
        }

        public virtual string HLRSERVERIP
        {
            get { return _HLRSERVERIP; }
            set { _HLRSERVERIP = value; }
        }

        public virtual int HLRSERVERPORT
        {
            get { return _HLRSERVERPORT; }
            set { _HLRSERVERPORT = value; }
        }

        public virtual string HLRSERVERUSER
        {
            get { return _HLRSERVERUSER; }
            set { _HLRSERVERUSER = value; }
        }

        public virtual string HLRSERVERPWD
        {
            get { return _HLRSERVERPWD; }
            set { _HLRSERVERPWD = value; }
        }

        public virtual string VMS
        {
            get { return _VMS; }
            set { _VMS = value; }
        }

        public virtual string HELPDESK
        {
            get { return _HELPDESK; }
            set { _HELPDESK = value; }
        }

        public virtual int TELEPHONENUMBERS
        {
            get { return _TELEPHONENUMBERS; }
            set { _TELEPHONENUMBERS = value; }
        }

        bool _UssdAllowed;
        public virtual bool UssdAllowed
        {
            get { return _UssdAllowed; }
            set { _UssdAllowed = value; }
        }

        public virtual bool CLIP
        {
            get { return _CLIP; }
            set { _CLIP = value; }
        }

        public virtual bool CLIPOVERRIDE
        {
            get { return _CLIPOVERRIDE; }
            set { _CLIPOVERRIDE = value; }
        }

        public virtual bool CLIR
        {
            get { return _CLIR; }
            set { _CLIR = value; }
        }

        public virtual bool COLP
        {
            get { return _COLP; }
            set { _COLP = value; }
        }

        public virtual bool COLPOVERRIDE
        {
            get { return _COLPOVERRIDE; }
            set { _COLPOVERRIDE = value; }
        }

        public virtual bool COLR
        {
            get { return _COLR; }
            set { _COLR = value; }
        }

        private bool _COLROVERRIDE;

        public virtual bool COLROVERRIDE
        {
            get { return _COLROVERRIDE; }
            set { _COLROVERRIDE = value; }
        }

        public virtual int CLIRPRESENTATIONMODE
        {
            get { return _CLIRPRESENTATIONMODE; }
            set { _CLIRPRESENTATIONMODE = value; }
        }

        public virtual bool CALLWAITING
        {
            get { return _CALLWAITING; }
            set { _CALLWAITING = value; }
        }

        public virtual bool CALLHOLD
        {
            get { return _CALLHOLD; }
            set { _CALLHOLD = value; }
        }

        public virtual bool MULTIPARTY
        {
            get { return _MULTIPARTY; }
            set { _MULTIPARTY = value; }
        }

        public virtual bool ECT
        {
            get { return _ECT; }
            set { _ECT = value; }
        }

        public virtual bool CALLWAITING_BSG_1
        {
            get { return _CALLWAITING_BSG_1; }
            set { _CALLWAITING_BSG_1 = value; }
        }

        public virtual bool CALLWAITING_BSG_6
        {
            get { return _CALLWAITING_BSG_6; }
            set { _CALLWAITING_BSG_6 = value; }
        }

        public virtual bool CALLWAITING_BSG_7
        {
            get { return _CALLWAITING_BSG_7; }
            set { _CALLWAITING_BSG_7 = value; }
        }

        public virtual bool CALLWAITING_BSG_8
        {
            get { return _CALLWAITING_BSG_8; }
            set { _CALLWAITING_BSG_8 = value; }
        }

        public virtual bool CALLWAITING_BSG_12
        {
            get { return _CALLWAITING_BSG_12; }
            set { _CALLWAITING_BSG_12 = value; }
        }

        public virtual bool AOCC
        {
            get { return _AOCC; }
            set { _AOCC = value; }
        }

        public virtual bool AOCI
        {
            get { return _AOCI; }
            set { _AOCI = value; }
        }

        public virtual string TELESERVICELIST
        {
            get { return _TELESERVICELIST; }
            set { _TELESERVICELIST = value; }
        }

        public virtual string BEARERSERVICELIST
        {
            get { return _BEARERSERVICELIST; }
            set { _BEARERSERVICELIST = value; }
        }

        private string _BearerService_3GPP;
        public virtual string BearerService_3GPP
        {
            get { return _BearerService_3GPP; }
            set { _BearerService_3GPP = value; }
        }

        public virtual string ODBMASK
        {
            get { return _ODBMASK; }
            set { _ODBMASK = value; }
        }

        public virtual string SERVICECODE
        {
            get { return _SERVICECODE; }
            set { _SERVICECODE = value; }
        }

        public virtual DateTime? CREATEDATE
        {
            get { return _CREATEDATE; }
            set { _CREATEDATE = value; }
        }

        public virtual int USERID
        {
            get { return _USERID; }
            set { _USERID = value; }
        }

        public virtual int CB_SUBSOPTION
        {
            get { return _CB_SUBSOPTION; }
            set { _CB_SUBSOPTION = value; }
        }

        public virtual string CB_PASSWORD
        {
            get { return _CB_PASSWORD; }
            set { _CB_PASSWORD = value; }
        }

        public virtual int CB_WRONGATTEMPTS
        {
            get { return _CB_WRONGATTEMPTS; }
            set { _CB_WRONGATTEMPTS = value; }
        }

        int _POSTPAYOCPLMNTEMPLATE;
        public virtual int PostPayPlmnTemplate
        {
            get
            {
                return _POSTPAYOCPLMNTEMPLATE;
            }
            set
            {
                _POSTPAYOCPLMNTEMPLATE = value;
            }
        }

        int _CamelRestrictedStatus;
        public virtual int CamelRestrictedStatus
        {
            get
            {
                return _CamelRestrictedStatus;
            }
            set
            {
                _CamelRestrictedStatus = value;
            }
        }

        int _PREPAYOCPLMNTEMPLATE;
        public virtual int PrepayPlmnTemplate
        {
            get
            {
                return _PREPAYOCPLMNTEMPLATE;
            }
            set
            {
                _PREPAYOCPLMNTEMPLATE = value;
            }
        }

        int _NAM;
        public virtual int NAM
        {
            get
            {
                return _NAM;
            }
            set
            {
                _NAM = value;
            }
        }

        string _FTNRULE;
        public virtual string FTNRule
        {
            get { return _FTNRULE; }
            set { _FTNRULE = value; }
        }

        public virtual string PrepayCsiTypeList
        {
            get { return _prepayCsiTypeList; }
            set { _prepayCsiTypeList = value; }
        }

        public virtual string PostpayCsiTypeList
        {
            get { return _postpayCsiTypeList; }
            set { _postpayCsiTypeList = value; }
        }
       

        public virtual bool IsDelete
        {
            get { return _IsDelete; }
            set { _IsDelete = value; }
        }

        /// <summary>
        /// This flag indicates if this object has been synchronized to HLR template.
        /// </summary>
        public virtual bool IsSynchronizedToHLRTemplate
        {
            get;
            set;
        }

        #endregion


        public virtual IList<CrmDefaultProvisionCallBarringInfo> CallBarringInfoList
        {
            get;
            set;
        }

        public virtual IList<CrmDefaultProvisionCallForwardInfor> CallForwardInfoList
        {
            get;
            set;
        }

        public virtual IList<CrmDefaultProvisionNetWorkInfo> NetWorkInforList
        {
            get;
            set;
        }


        public virtual IList<CrmDefaultProvisionCamelDataInfo> CamelDataList
        {
            get;
            set;
        }

        public virtual IList<CrmDefaultProvisionCamelCsiDataInfo> CamelCsiDataList
        {
            get;
            set;
        }


        public virtual IList<CrmDefaultProvisionCamelCsiDPInfo> CamelCsiDPList
        {
            get;
            set;
        }

        public virtual IList<CrmDefaultProvisionRoamingSettingInfo> RoamingSettingList
        {
            get;
            set;
        }

        public virtual ProvisioningSubscriberTemplate ProvisioningSubscriberTemplate { get; set; }
    }
}
