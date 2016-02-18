using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class UserOrganizationInfo
    {
        private int? _ID = null;        
        private int? _UserID = null;        
        private int? _OrganizationID = null;        


        #region Attribute
        public virtual int? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual int? UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public virtual int? OrganizationID
        {
            get { return _OrganizationID; }
            set { _OrganizationID = value; }
        }
        #endregion

        
    }
}
