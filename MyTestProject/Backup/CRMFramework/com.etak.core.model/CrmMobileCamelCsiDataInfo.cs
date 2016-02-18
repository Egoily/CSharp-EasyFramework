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
	public class CrmMobileCamelCsiDataInfo : ModelBase
    {
        #region 构造函数
        public CrmMobileCamelCsiDataInfo()
        { }

        public CrmMobileCamelCsiDataInfo(int CamelCsiDataId, int ResourceId, int CsiType, bool ProvisionState, int CamelPhase, bool NotifyCse, int ActionOnUnsCamelPh, bool ActiveState)
        {
            this._ResourceId = ResourceId;
            this._CsiType = CsiType;
            this._ProvisionState = ProvisionState;
            this._CamelPhase = CamelPhase;
            this._NotifyCse = NotifyCse;
            this._ActionOnUnsCamelPh = ActionOnUnsCamelPh;
			this._ActiveState = ActiveState;
        }
        #endregion

        #region 成员

        private int _ResourceId;
        private int _CsiType;
        private bool _ProvisionState;
        private int _CamelPhase;
        private bool _NotifyCse;
        private int _ActionOnUnsCamelPh;
		private bool _ActiveState;
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
		//End-Modify by wood, for non camel project on 2013-04-22
		//public int CamelCsiDataId
		//{
		//    get { return _CamelCsiDataId; }
		//    set { _CamelCsiDataId = value; }
		//}

        public virtual int RESOURCEID
        {
            get { return _ResourceId; }
            set { _ResourceId = value; }
        }
		// Modify by wood, for non camel project on 2013-04-22
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

        
		public virtual bool ActiveState
        {
            get { return _ActiveState; }
            set { _ActiveState = value; }
        }

		public virtual bool ProvisionState
        {
            get { return _ProvisionState; }
            set { _ProvisionState = value; }
        }

		public virtual int CamelPhase
        {
            get { return _CamelPhase; }
            set { _CamelPhase = value; }
        }

		public virtual bool NotifyCse
        {
            get { return _NotifyCse; }
            set { _NotifyCse = value; }
        }

		public virtual int ActionOnUnsCamelPh
        {
            get { return _ActionOnUnsCamelPh; }
            set { _ActionOnUnsCamelPh = value; }
        }
        /// <summary>
        /// 0 (Always Send) 
        ///1 (Don’t send 
        ///when in HPLMN) 
        /// </summary>
		public virtual int Inhibition
        {
            get;
            set;
        }

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
