using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class InternationalZoneDetailInfo
    {
        #region 
        private int? _ZoneDetailID = null;
        private string _CountryCode;
        private string _CountryName;
        private DateTime? _StartDate = null;
        private DateTime? _EndDate = null;
        private int? _ZoneID = null;

        private InternationalZoneInfo _InternationalZoneInfo;

       
        #endregion

        #region Attribute

        public int? ZoneID
        {
            get { return _ZoneID; }
            set { _ZoneID = value; }
        }

        public int? ZoneDetailID
        {
            get { return _ZoneDetailID; }
            set { _ZoneDetailID = value; }
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

        public DateTime? StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        public DateTime? EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }


        public InternationalZoneInfo InternationalZoneInfo
        {
            get { return _InternationalZoneInfo; }
            set { _InternationalZoneInfo = value; }
        }

        #endregion

        
    }
}
