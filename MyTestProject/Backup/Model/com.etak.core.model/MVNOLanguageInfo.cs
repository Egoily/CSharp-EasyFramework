using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class MVNOLanguageInfo
    {
        private int? _ID = null;        
        private int? _MVNOID = null;        
        private int? _LanguageID = null;        


        #region Attribute
        public virtual int? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual int? MVNOID
        {
            get { return _MVNOID; }
            set { _MVNOID = value; }
        }

        public virtual int? LanguageID
        {
            get { return _LanguageID; }
            set { _LanguageID = value; }
        }
        #endregion
    }
}
