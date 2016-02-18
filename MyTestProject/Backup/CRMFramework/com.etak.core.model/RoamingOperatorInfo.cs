using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class RoamingOperatorInfo
    {
        #region 
        private int? _ID = null;
        private string _CCNDC;
        private string _TSC;
        private string _CountryName;
        private string _OperatorName;
        private string _CC;
        private string _NDC;
        private string _CountryIS02Code;
        private string _CountryIS03Code;

        #endregion


        #region Attribute

        public int? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string CCNDC
        {
            get { return _CCNDC; }
            set { _CCNDC = value; }
        }

        public string TSC
        {
            get { return _TSC; }
            set { _TSC = value; }
        }

        public string CountryName
        {
            get { return _CountryName; }
            set { _CountryName = value; }
        }

        public string OperatorName
        {
            get { return _OperatorName; }
            set { _OperatorName = value; }
        }

        public string CC
        {
            get { return _CC; }
            set { _CC = value; }
        }

        public string NDC
        {
            get { return _NDC; }
            set { _NDC = value; }
        }

        public string CountryIS02Code
        {
            get { return _CountryIS02Code; }
            set { _CountryIS02Code = value; }
        }

        public string CountryIS03Code
        {
            get { return _CountryIS03Code; }
            set { _CountryIS03Code = value; }
        }
        #endregion
    }
}
