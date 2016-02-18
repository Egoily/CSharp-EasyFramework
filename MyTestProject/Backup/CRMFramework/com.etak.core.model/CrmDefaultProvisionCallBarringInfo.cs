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
    public class CrmDefaultProvisionCallBarringInfo : ModelBase
    {
        #region 构造函数
        public CrmDefaultProvisionCallBarringInfo()
        { }

        public CrmDefaultProvisionCallBarringInfo(int CALLBARRINGID, CrmDefaultProvisionInfo esCrmDefaultProvision, int BSGID, bool BAOCPROV, bool BOICPROV, bool BOICEXHCPROV, bool BAICPROV, bool BICROAMPROV, int BAOCACT, int BOICACT, int BOICEXHCACT, int BAICACT, int BICROAMACT)
        {
            this._CALLBARRINGID = CALLBARRINGID;
            this._esCrmDefaultProvision = esCrmDefaultProvision;
            this._BSGID = BSGID;
            this._BAOCPROV = BAOCPROV;
            this._BOICPROV = BOICPROV;
            this._BOICEXHCPROV = BOICEXHCPROV;
            this._BAICPROV = BAICPROV;
            this._BICROAMPROV = BICROAMPROV;
            this._BAOCACT = BAOCACT;
            this._BOICACT = BOICACT;
            this._BOICEXHCACT = BOICEXHCACT;
            this._BAICACT = BAICACT;
            this._BICROAMACT = BICROAMACT;
        }
        #endregion

        #region 成员
        private int _CALLBARRINGID;
        protected CrmDefaultProvisionInfo _esCrmDefaultProvision;
        private int _BSGID;
        private bool _BAOCPROV;
        private bool _BOICPROV;
        private bool _BOICEXHCPROV;
        private bool _BAICPROV;
        private bool _BICROAMPROV;
        private int _BAOCACT;
        private int _BOICACT;
        private int _BOICEXHCACT;
        private int _BAICACT;
        private int _BICROAMACT;

        private bool _IsDelete = false;
        #endregion


        #region 属性
        public virtual int CALLBARRINGID
        {
            get { return _CALLBARRINGID; }
            set { _CALLBARRINGID = value; }
        }

        public virtual CrmDefaultProvisionInfo CrmDefaultProvisionInfo
        {
            get { return _esCrmDefaultProvision; }
            set { _esCrmDefaultProvision = value; }
        }

        public virtual int BSGID
        {
            get { return _BSGID; }
            set { _BSGID = value; }
        }

        public virtual bool BAOCPROV
        {
            get { return _BAOCPROV; }
            set { _BAOCPROV = value; }
        }

        public virtual bool BOICPROV
        {
            get { return _BOICPROV; }
            set { _BOICPROV = value; }
        }

        public virtual bool BOICEXHCPROV
        {
            get { return _BOICEXHCPROV; }
            set { _BOICEXHCPROV = value; }
        }

        public virtual bool BAICPROV
        {
            get { return _BAICPROV; }
            set { _BAICPROV = value; }
        }

        public virtual bool BICROAMPROV
        {
            get { return _BICROAMPROV; }
            set { _BICROAMPROV = value; }
        }

        public virtual int BAOCACT
        {
            get { return _BAOCACT; }
            set { _BAOCACT = value; }
        }

        public virtual int BOICACT
        {
            get { return _BOICACT; }
            set { _BOICACT = value; }
        }

        public virtual int BOICEXHCACT
        {
            get { return _BOICEXHCACT; }
            set { _BOICEXHCACT = value; }
        }

        public virtual int BAICACT
        {
            get { return _BAICACT; }
            set { _BAICACT = value; }
        }

        public virtual int BICROAMACT
        {
            get { return _BICROAMACT; }
            set { _BICROAMACT = value; }
        }


        public virtual bool IsDelete
        {
            get { return _IsDelete; }
            set { _IsDelete = value; }
        }
        #endregion

    }
}
