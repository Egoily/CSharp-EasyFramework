using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class ModelInfo
    {
        private int? _ModuleID = null;        
        private int? _ParentID = null;        
        private string _ModuleName;        
        private string _ModuleForm;        
        private string _ModuleFullPath;
        private int? _ModuleLevel = null;  
        private string _Remark;        
        private DateTime? _CreateDate = null;        
        private int? _CreateUserID = null;        
        private int? _Status = null;        
        private DateTime? _UpdateDate = null;        
        private int? _UpdateUserID = null;
        private int? _ShowOrder = null;        


        #region Attribute
        public virtual int? ModuleID
        {
            get { return _ModuleID; }
            set { _ModuleID = value; }
        }

        public virtual int? ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }

        public virtual string ModuleName
        {
            get { return _ModuleName; }
            set { _ModuleName = value; }
        }

        public virtual string ModuleForm
        {
            get { return _ModuleForm; }
            set { _ModuleForm = value; }
        }

        public virtual string ModuleFullPath
        {
            get { return _ModuleFullPath; }
            set { _ModuleFullPath = value; }
        }

        public virtual int? ModuleLevel
        {
            get { return _ModuleLevel; }
            set { _ModuleLevel = value; }
        }

        public virtual string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }

        public virtual DateTime? CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        public virtual int? CreateUserID
        {
            get { return _CreateUserID; }
            set { _CreateUserID = value; }
        }

        public virtual int? Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public virtual DateTime? UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }

        public virtual int? UpdateUserID
        {
            get { return _UpdateUserID; }
            set { _UpdateUserID = value; }
        }

        public virtual int? ShowOrder
        {
            get { return _ShowOrder; }
            set { _ShowOrder = value; }
        }
        #endregion
    }
}
