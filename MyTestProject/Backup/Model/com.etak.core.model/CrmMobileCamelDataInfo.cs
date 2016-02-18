using System;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2010-5-25 16:09:25
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2010-5-25 16:09:25
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2010-5-25 16:09:25
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2010-5-25 16:09:25
    /// </summary>
    [Serializable]
    public class CrmMobileCamelDataInfo : ModelBase
    {
        #region 构造函数
        public CrmMobileCamelDataInfo()
        { }

        public CrmMobileCamelDataInfo(int CamelDataId, int ResourceId, bool ProvisionState, bool CallBarringNotifyCse, bool CallForwardNotifyCse, bool OdbNotifyCse)
        {
            this._CamelDataId = CamelDataId;
            this._ResourceId = ResourceId;
            this._ProvisionState = ProvisionState;
            this._CallBarringNotifyCse = CallBarringNotifyCse;
            this._CallForwardNotifyCse = CallForwardNotifyCse;
            this._OdbNotifyCse = OdbNotifyCse;
        }
        #endregion

        #region 成员
        private int _CamelDataId;
        private int _ResourceId;
        private bool _ProvisionState;
        private bool _CallBarringNotifyCse;
        private bool _CallForwardNotifyCse;
        private bool _OdbNotifyCse;
        #endregion


        #region 属性
        //CrmCustomersResourceMbInfo _CrmCustomersResourceMbInfo;
        //public virtual CrmCustomersResourceMbInfo CrmCustomersResourceMbInfo
        //{
        //    get { return _CrmCustomersResourceMbInfo; }
        //    set { _CrmCustomersResourceMbInfo = value; }
        //}

        public virtual int CamelDataId
        {
            get { return _CamelDataId; }
            set { _CamelDataId = value; }
        }

        public virtual int ResourceId
        {
            get { return _ResourceId; }
            set { _ResourceId = value; }
        }
        string _imsi;
        public string Imsi
        {
            get { return _imsi; }
            set { _imsi = value; }
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
    }
}
