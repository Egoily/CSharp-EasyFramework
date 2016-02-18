using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class RoleInfo
    {
        private int? _RoleID = null;        
        private int? _ModuleID = null;        
        private int? _DealerID = null;        
        private string _RoleName;        
        private string _ModuleName;        
        private string _DealerNode;        
        private DateTime? _CreateDate = null;        
        private int? _CreateUserID = null;        
        private int? _Status = null;        
        private DateTime? _UpdateDate = null;        
        private int? _UpdateUserID = null;        


        #region Attribute
        public virtual int? RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }

        public virtual int? ModuleID
        {
            get { return _ModuleID; }
            set { _ModuleID = value; }
        }

        public virtual int? DealerID
        {
            get { return _DealerID; }
            set { _DealerID = value; }
        }

        public virtual string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }

        public virtual string ModuleName
        {
            get { return _ModuleName; }
            set { _ModuleName = value; }
        }

        public virtual string DealerNode
        {
            get { return _DealerNode; }
            set { _DealerNode = value; }
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
        #endregion
    }
}
