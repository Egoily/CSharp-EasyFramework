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
    public class CrmDefaultProvisionCamelDataInfo: ModelBase
    {
        #region 构造函数
        public CrmDefaultProvisionCamelDataInfo()
        { }

        public CrmDefaultProvisionCamelDataInfo(int CamelDataId, bool ProvisionState, bool CallBarringNotifyCse, bool CallForwardNotifyCse, bool OdbNotifyCse)
        {
            this._CamelDataId = CamelDataId;
            this._ProvisionState = ProvisionState;
            this._CallBarringNotifyCse = CallBarringNotifyCse;
            this._CallForwardNotifyCse = CallForwardNotifyCse;
            this._OdbNotifyCse = OdbNotifyCse;
        }
        #endregion

        #region 成员
        private int _CamelDataId;
        private bool _ProvisionState;
        private bool _CallBarringNotifyCse;
        private bool _CallForwardNotifyCse;
        private bool _OdbNotifyCse;
        #endregion


        #region 属性
        public virtual int CamelDataId
        {
            get { return _CamelDataId; }
            set { _CamelDataId = value; }
        }

        public virtual bool ProvisionState
        {
            get { return _ProvisionState; }
            set { _ProvisionState = value; }
        }

        public virtual bool CallBarringNotifyCse
        {
            get { return _CallBarringNotifyCse; }
            set { _CallBarringNotifyCse = value; }
        }

        public virtual bool CallForwardNotifyCse
        {
            get { return _CallForwardNotifyCse; }
            set { _CallForwardNotifyCse = value; }
        }

        public virtual bool OdbNotifyCse
        {
            get { return _OdbNotifyCse; }
            set { _OdbNotifyCse = value; }
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
