using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class RolePermissionInfo
    {
        private int? _ID = null;    
        private int? _RoleID = null;        
        private int? _PermissionID = null;
        

        #region Attribute
        public virtual int? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public virtual int? RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }

        public virtual int? PermissionID
        {
            get { return _PermissionID; }
            set { _PermissionID = value; }
        }
        #endregion
    }
}
