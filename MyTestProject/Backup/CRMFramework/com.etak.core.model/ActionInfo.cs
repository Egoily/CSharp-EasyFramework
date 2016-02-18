using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class ActionInfo
    {

        #region 构造函数
        public ActionInfo()
        { }

        public ActionInfo(int actionId, string assemblyName, string className, string methodName, int dealerId, string description, string parameters)
        {
            this._ActionId = actionId;
            this._AssemblyName = assemblyName;
            this._ClassName = className;
            this._MethodName = methodName;
            this._DealerId = dealerId;
            this._Description = description;
            this._Parameters = parameters;
        }
        #endregion

        #region 成员
        private int _ActionId;
        private string _AssemblyName;
        private string _ClassName;
        private string _MethodName;
        private int _DealerId;
        private string _Description;
        private string _Parameters;
        #endregion


        #region Properties
        public virtual int ActionId
        {
            get { return _ActionId; }
            set { _ActionId = value; }
        }

        public virtual string AssemblyName
        {
            get { return _AssemblyName; }
            set { _AssemblyName = value; }
        }

        public virtual string ClassName
        {
            get { return _ClassName; }
            set { _ClassName = value; }
        }

        public virtual string MethodName
        {
            get { return _MethodName; }
            set { _MethodName = value; }
        }

        public virtual int DealerId
        {
            get { return _DealerId; }
            set { _DealerId = value; }
        }

        public virtual string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public virtual string Parameters
        {
            get { return _Parameters; }
            set { _Parameters = value; }
        }

        #endregion


        #region Methods
        public override bool Equals(object obj)
        {
            ActionInfo actionInfo = (ActionInfo) obj;
            if (this.DealerId == actionInfo.DealerId && this.ActionId == actionInfo.ActionId)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();

        }

        #endregion
    }
}
