using System;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2010-5-24 18:34:10
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2010-5-24 18:34:10
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2010-5-24 18:34:10
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2010-5-24 18:34:10
    /// </summary>
    [Serializable]
    public class CrmDefaultProvisionCamelCsiDataInfo : ModelBase
    {
        #region 构造函数
        public CrmDefaultProvisionCamelCsiDataInfo()
        { }

        public CrmDefaultProvisionCamelCsiDataInfo(int CamelCsiDataId, int CsiType, bool ProvisionState, int CamelPhase, bool NotifyCse, int ActionOnUnsCamelPh)
        {
            this._CamelCsiDataId = CamelCsiDataId;
            this._CsiType = CsiType;
            this._ProvisionState = ProvisionState;
            this._CamelPhase = CamelPhase;
            this._NotifyCse = NotifyCse;
            this._ActionOnUnsCamelPh = ActionOnUnsCamelPh;
        }
        #endregion

        #region 成员
        private int _CamelCsiDataId;
        private int _CsiType;
        private bool _ProvisionState;
        private int _CamelPhase;
        private bool _NotifyCse;
        private int _ActionOnUnsCamelPh;
        #endregion


        #region 属性
        public virtual int CamelCsiDataId
        {
            get { return _CamelCsiDataId; }
            set { _CamelCsiDataId = value; }
        }

        public virtual int CsiType
        {
            get { return _CsiType; }
            set { _CsiType = value; }
        }

        public virtual bool ProvisionState
        {
            get { return _ProvisionState; }
            set { _ProvisionState = value; }
        }
        bool _ActiveState;
        public bool ActiveState
        {
            get { return _ActiveState; }
            set { _ActiveState = value; }
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
        CrmDefaultProvisionInfo _esCrmDefaultProvision;
        public virtual CrmDefaultProvisionInfo CrmDefaultProvisionInfo
        {
            get { return _esCrmDefaultProvision; }
            set { _esCrmDefaultProvision = value; }
        }

        bool _IsDelete;
        public virtual bool IsDelete
        {
            get { return _IsDelete; }
            set { _IsDelete = value; }
        }
        #endregion

    }
}
