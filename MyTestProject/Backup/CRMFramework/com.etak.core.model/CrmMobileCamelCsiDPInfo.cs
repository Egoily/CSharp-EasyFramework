using System;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2010-5-25 16:09:26
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2010-5-25 16:09:26
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2010-5-25 16:09:26
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2010-5-25 16:09:26
    /// </summary>
    [Serializable]
	public class CrmMobileCamelCsiDPInfo : ModelBase
    {
        #region 构造函数
        public CrmMobileCamelCsiDPInfo()
        { }

        public CrmMobileCamelCsiDPInfo(int CamelCsiDPId, int ResourceId, int CsiType, int DpType, int GsmScfId, int ServiceKey, int ProvisionState, int DefaultCallHandling, int BasicServiceCritPresent, string BasicServiceCriteriaTSList, string BasicServiceCriteriaBSList, int ForwardingCritPresent, int ForwardedCall, int DstNumberCritPresent, int DstNumberNAI, int DstNumberCriteriaMatchType, string DstNumberCriteriaLengthList, bool TsciProvisionState, bool SsCsiProvisionState, bool TifCsiProvisionState, int OCsiCamelPhase, bool OCsiActivateState, bool OCsiProvisionState, int TSciCamelPhase, bool TcsiActiveState, string SsCsiNoTifCritSSList, int SsCsiGsmscfId, bool SsCsiActiveState, bool TifCsiFlag)
        {
            this._CamelCsiDPId = CamelCsiDPId;
            this._ResourceId = ResourceId;
            this._CsiType = CsiType;
            this._DpType = DpType;
            this._GsmScfId = GsmScfId;
            this._ServiceKey = ServiceKey;
            this._ProvisionState = ProvisionState;
            this._DefaultCallHandling = DefaultCallHandling;
            this._BasicServiceCritPresent = BasicServiceCritPresent;
            this._BasicServiceCriteriaTSList = BasicServiceCriteriaTSList;
            this._BasicServiceCriteriaBSList = BasicServiceCriteriaBSList;
            this._ForwardingCritPresent = ForwardingCritPresent;
            this._ForwardedCall = ForwardedCall;
            this._DstNumberCritPresent = DstNumberCritPresent;
            this._DstNumberNAI = DstNumberNAI;
            this._DstNumberCriteriaMatchType = DstNumberCriteriaMatchType;
            this._DstNumberCriteriaLengthList = DstNumberCriteriaLengthList;
        }
        #endregion

        #region 成员
        private int _CamelCsiDPId;
        private int _ResourceId;
        private int _CsiType;
        private int _DpType;
        private int _GsmScfId;
        private int _ServiceKey;
        private int _ProvisionState;
        private int _DefaultCallHandling;
        private int _BasicServiceCritPresent;
        private string _BasicServiceCriteriaTSList;
        private string _BasicServiceCriteriaBSList;
        private int _ForwardingCritPresent;
        private int _ForwardedCall;
        private int _DstNumberCritPresent;
        private int _DstNumberNAI;
        private int _DstNumberCriteriaMatchType;
        private string _DstNumberCriteriaLengthList;
        private int _CamelPhase;
        #endregion



        #region 属性
		// Modify by wood, for non camel project on 2013-04-22
		CrmCustomersResourceMbInfo _CrmCustomersResourceMbInfo;
		public virtual CrmCustomersResourceMbInfo CrmCustomersResourceMbInfo
		{
			get { return _CrmCustomersResourceMbInfo; }
			set { _CrmCustomersResourceMbInfo = value; }
		}

		bool _DeleateFlag;
		public virtual bool DeleteFlag
		{
			get { return _DeleateFlag; }
			set { _DeleateFlag = value; }
		}
		// End-Modify by wood, for non camel project on 2013-04-22
        public virtual int CamelCsiDPId
        {
            get { return _CamelCsiDPId; }
            set { _CamelCsiDPId = value; }
        }

		public virtual int RESOURCEID
        {
            get { return _ResourceId; }
            set { _ResourceId = value; }
        }
		//Modify by wood, for non camel project on 2013-04-22
		//string _imsi;
		//public string Imsi
		//{
		//    get { return _imsi; }
		//    set { _imsi = value; }
		//}
		//End-Modify by wood, for non camel project on 2013-04-22
        public virtual int CsiType
        {
            get { return _CsiType; }
            set { _CsiType = value; }
        }

        public virtual int DpType
        {
            get { return _DpType; }
            set { _DpType = value; }
        }

        public virtual int GsmScfId
        {
            get { return _GsmScfId; }
            set { _GsmScfId = value; }
        }

        public virtual int ServiceKey
        {
            get { return _ServiceKey; }
            set { _ServiceKey = value; }
        }

        public virtual int ProvisionState
        {
            get { return _ProvisionState; }
            set { _ProvisionState = value; }
        }

        public virtual int DefaultCallHandling
        {
            get { return _DefaultCallHandling; }
            set { _DefaultCallHandling = value; }
        }

        public virtual int BasicServiceCritPresent
        {
            get { return _BasicServiceCritPresent; }
            set { _BasicServiceCritPresent = value; }
        }

        public virtual string BasicServiceCriteriaTSList
        {
            get { return _BasicServiceCriteriaTSList; }
            set { _BasicServiceCriteriaTSList = value; }
        }

        public virtual string BasicServiceCriteriaBSList
        {
            get { return _BasicServiceCriteriaBSList; }
            set { _BasicServiceCriteriaBSList = value; }
        }

        public virtual int ForwardingCritPresent
        {
            get { return _ForwardingCritPresent; }
            set { _ForwardingCritPresent = value; }
        }

        public virtual int ForwardedCall
        {
            get { return _ForwardedCall; }
            set { _ForwardedCall = value; }
        }

        public virtual int DstNumberCritPresent
        {
            get { return _DstNumberCritPresent; }
            set { _DstNumberCritPresent = value; }
        }

        public virtual int DstNumberNAI
        {
            get { return _DstNumberNAI; }
            set { _DstNumberNAI = value; }
        }

        public virtual int DstNumberCriteriaMatchType
        {
            get { return _DstNumberCriteriaMatchType; }
            set { _DstNumberCriteriaMatchType = value; }
        }

        public virtual string DstNumberCriteriaLengthList
        {
            get { return _DstNumberCriteriaLengthList; }
            set { _DstNumberCriteriaLengthList = value; }
        }

        string _SsEventList;
        public virtual string SsEventList
        {
            get { return _SsEventList; }
            set { _SsEventList = value; }
        }

        public virtual int CamelPhase
        {
            get { return _CamelPhase; }
            set { _CamelPhase = value; }
        }

        //public virtual bool TsciProvisionState
        //{
        //    get { return _TsciProvisionState; }
        //    set { _TsciProvisionState = value; }
        //}

        //public virtual bool SsCsiProvisionState
        //{
        //    get { return _SsCsiProvisionState; }
        //    set { _SsCsiProvisionState = value; }
        //}

        //public virtual bool TifCsiProvisionState
        //{
        //    get { return _TifCsiProvisionState; }
        //    set { _TifCsiProvisionState = value; }
        //}

        //public virtual int OCsiCamelPhase
        //{
        //    get { return _OCsiCamelPhase; }
        //    set { _OCsiCamelPhase = value; }
        //}

        //public virtual bool OCsiActivateState
        //{
        //    get { return _OCsiActivateState; }
        //    set { _OCsiActivateState = value; }
        //}

        //public virtual bool OCsiProvisionState
        //{
        //    get { return _OCsiProvisionState; }
        //    set { _OCsiProvisionState = value; }
        //}

        //public virtual int TSciCamelPhase
        //{
        //    get { return _TSciCamelPhase; }
        //    set { _TSciCamelPhase = value; }
        //}

        //public virtual bool TcsiActiveState
        //{
        //    get { return _TcsiActiveState; }
        //    set { _TcsiActiveState = value; }
        //}

        //public virtual string SsCsiNoTifCritSSList
        //{
        //    get { return _SsCsiNoTifCritSSList; }
        //    set { _SsCsiNoTifCritSSList = value; }
        //}

        //public virtual int SsCsiGsmscfId
        //{
        //    get { return _SsCsiGsmscfId; }
        //    set { _SsCsiGsmscfId = value; }
        //}

        //public virtual bool SsCsiActiveState
        //{
        //    get { return _SsCsiActiveState; }
        //    set { _SsCsiActiveState = value; }
        //}

        //public virtual bool TifCsiFlag
        //{
        //    get { return _TifCsiFlag; }
        //    set { _TifCsiFlag = value; }
        //}

        #endregion
        private string _OPERATION;
        private bool _FINISHED;
        private DateTime? _CREATETIME;
        private DateTime? _HANDLETIME;
        public string OPERATION
        {
            get { return _OPERATION; }
            set { _OPERATION = value; }
        }

        public bool FINISHED
        {
            get { return _FINISHED; }
            set { _FINISHED = value; }
        }

        public DateTime? CREATETIME
        {
            get { return _CREATETIME; }
            set { _CREATETIME = value; }
        }

        public DateTime? HANDLETIME
        {
            get { return _HANDLETIME; }
            set { _HANDLETIME = value; }
        }

		private bool _insertFlag;
		public bool InsertFlag
		{
			get { return _insertFlag; }
			set { _insertFlag = value; }
		}

		public override bool Equals(object obj)
		{
			return this.GetHashCode() == obj.GetHashCode();
		}

		public override int GetHashCode()
		{
			return (this.GetType() + "|" + _ResourceId + _CsiType).GetHashCode();
		}
    }
}
