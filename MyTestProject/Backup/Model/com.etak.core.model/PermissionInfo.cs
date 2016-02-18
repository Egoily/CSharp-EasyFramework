using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class PermissionInfo
    {
        private int? _PermissionID = null;
        private int? _RoleID = null; 
        private int? _ModuleID = null; 
        private string _FormName;        
        private string _FullClassName; 
        private string _ElementID;        
        private string _ElementName;        
        private bool _IsRead;        
        private bool _IsWrite;        
        private bool _IsHide;        
        private bool _IsMandetory;        
        private string _Format;        
        private string _Event;        
        private string _DefaultValue;
        private int? _XAXIS;
        private int? _YAXIS;

        #region Attribute
        public virtual int? PermissionID
        {
            get { return _PermissionID; }
            set { _PermissionID = value; }
        }

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

        public virtual string FormName
        {
            get { return _FormName; }
            set { _FormName = value; }
        }

        public virtual string FullClassName
        {
            get { return _FullClassName; }
            set { _FullClassName = value; }
        }

        public virtual string ElementID
        {
            get { return _ElementID; }
            set { _ElementID = value; }
        }

        public virtual string ElementName
        {
            get { return _ElementName; }
            set { _ElementName = value; }
        }

        public virtual bool IsRead
        {
            get { return _IsRead; }
            set { _IsRead = value; }
        }

        public virtual bool IsWrite
        {
            get { return _IsWrite; }
            set { _IsWrite = value; }
        }

        public virtual bool IsHide
        {
            get { return _IsHide; }
            set { _IsHide = value; }
        }

        public virtual bool IsMandetory
        {
            get { return _IsMandetory; }
            set { _IsMandetory = value; }
        }

        public virtual string Format
        {
            get { return _Format; }
            set { _Format = value; }
        }

        public virtual string Event
        {
            get { return _Event; }
            set { _Event = value; }
        }

        public virtual string DefaultValue
        {
            get { return _DefaultValue; }
            set { _DefaultValue = value; }
        }

        public virtual int? XAXIS
        {
            get { return _XAXIS; }
            set { _XAXIS = value; }
        }

        public virtual int? YAXIS
        {
            get { return _YAXIS; }
            set { _YAXIS = value; }
        }
        #endregion


        #region Clone
        public virtual PermissionInfo Clone()
        {
            PermissionInfo permissionInfo = this.MemberwiseClone() as PermissionInfo;

            return permissionInfo;
        }
        #endregion
    }
}
