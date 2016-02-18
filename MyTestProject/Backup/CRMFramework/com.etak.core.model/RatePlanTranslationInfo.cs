using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class RatePlanTranslationInfo
    {
        #region
        private int? _TranslationID = null;
        private int? _RatePlanID = null;
        private int? _LanguageID = null;
        private string _CountryCode;
        private string _CountryName;

        #endregion

        #region Attribute

        public int? TranslationID
        {
            get { return _TranslationID; }
            set { _TranslationID = value; }
        }

        public int? RatePlanID
        {
            get { return _RatePlanID; }
            set { _RatePlanID = value; }
        }

        public int? LanguageID
        {
            get { return _LanguageID; }
            set { _LanguageID = value; }
        }

        public string CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }

        public string CountryName
        {
            get { return _CountryName; }
            set { _CountryName = value; }
        }
        #endregion
    }
}
