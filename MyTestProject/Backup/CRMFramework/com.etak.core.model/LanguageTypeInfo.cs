using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class LanguageTypeInfo
    {
        private int? _ID;        
        private int? _LanguageID;        
        private string _LanguageName;
        private DateTime? _UpdateDate = null;        
        private int? _State = null;
        private string _CultureName;        
        private int? _CultureIdentifier = null;        
        private string _LanguageCountryRegion;
        private string _ShortCode = string.Empty;


        #region Attribute
        public virtual int? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual int? LanguageID
        {
            get { return _LanguageID; }
            set { _LanguageID = value; }
        }

        public virtual string LanguageName
        {
            get { return _LanguageName; }
            set { _LanguageName = value; }
        }

        public virtual DateTime? UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }

        public virtual int? State
        {
            get { return _State; }
            set { _State = value; }
        }

        public virtual string CultureName
        {
            get { return _CultureName; }
            set { _CultureName = value; }
        }

        public virtual int? CultureIdentifier
        {
            get { return _CultureIdentifier; }
            set { _CultureIdentifier = value; }
        }

        public virtual string LanguageCountryRegion
        {
            get { return _LanguageCountryRegion; }
            set { _LanguageCountryRegion = value; }
        }

        public virtual string ShortCode
        {
            get { return _ShortCode; }
            set { _ShortCode = value; }
        }
        #endregion
    }
}
