using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class RoamingZoneDetailInfo
    {
        #region 
        private int? _ZoneID = null;
        private int? _ZoneDetailID = null;
        private string _ZoneName;
        private string _CCNDC;
        private string _TSC;
        private bool _Preferred;
        private string _CountryName;
        private string _OperatorName;
        private DateTime? _StartDate = null;
        private DateTime? _EndDate = null;

        private RoamingZoneInfo _RoamingZoneInfo;

        

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

        public string ZoneName
        {
            get { return _ZoneName; }
            set { _ZoneName = value; }
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

        public bool Preferred
        {
            get { return _Preferred; }
            set { _Preferred = value; }
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

        public RoamingZoneInfo RoamingZoneInfo
        {
            get { return _RoamingZoneInfo; }
            set { _RoamingZoneInfo = value; }
        }
        #endregion
    }
}
