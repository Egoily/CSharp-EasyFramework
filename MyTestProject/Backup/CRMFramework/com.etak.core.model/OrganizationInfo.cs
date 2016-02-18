using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class OrganizationInfo
    {
        private int? _ID = null;        
        private int? _ParentID = null;        
        private string _OrgNode;        
        private string _Contract;        
        private int? _TitleID = null;        
        private int? _GenderID = null;        
        private string _Telephone;        
        private string _TeleFax;        
        private string _Email;        
        private DateTime? _CreateDate = null;        
        private int? _CreateUserID = null;        
        private int? _Status = null;        
        private DateTime? _UpdateDate = null;        
        private int? _UpdateUserID = null;
        private int? _MVNOID = null;

        #region Attribute
        public virtual int? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual int? ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }

        public virtual string OrgNode
        {
            get { return _OrgNode; }
            set { _OrgNode = value; }
        }

        public virtual string Contract
        {
            get { return _Contract; }
            set { _Contract = value; }
        }

        public virtual int? TitleID
        {
            get { return _TitleID; }
            set { _TitleID = value; }
        }

        public virtual int? GenderID
        {
            get { return _GenderID; }
            set { _GenderID = value; }
        }

        public virtual string Telephone
        {
            get { return _Telephone; }
            set { _Telephone = value; }
        }

        public virtual string TeleFax
        {
            get { return _TeleFax; }
            set { _TeleFax = value; }
        }

        public virtual string Email
        {
            get { return _Email; }
            set { _Email = value; }
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

        public virtual int? MVNOID
        {
            get { return _MVNOID; }
            set { _MVNOID = value; }
        }  
        #endregion
    }
}
