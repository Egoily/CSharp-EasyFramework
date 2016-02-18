using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class ThemeInfo
    {
        private int? _ID = null;
        private string _SkinID;
        private int? _ThemeID = null;        
        private string _ControlClassName;        
        private string _ItemName;        
        private string _ItemValue;
        private string _Description;
        private int? _MVNOID = null;

        

        #region Attribute           
        public virtual int? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual string SkinID
        {
            get { return _SkinID; }
            set { _SkinID = value; }
        }

        public virtual int? ThemeID
        {
            get { return _ThemeID; }
            set { _ThemeID = value; }
        }        

        public virtual string ControlClassName
        {
            get { return _ControlClassName; }
            set { _ControlClassName = value; }
        }

        public virtual string ItemName
        {
            get { return _ItemName; }
            set { _ItemName = value; }
        }

        public virtual string ItemValue
        {
            get { return _ItemValue; }
            set { _ItemValue = value; }
        }

        public virtual string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public virtual int? MVNOID
        {
            get { return _MVNOID; }
            set { _MVNOID = value; }
        }
        #endregion
    }
}
