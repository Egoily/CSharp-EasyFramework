using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class RoamingZoneInfo
    {
        #region
        private int? _ZoneID;
        private string _ZoneName;
        private int? _DealerID;
        private DateTime? _StartDate;
        private DateTime? _EndDate;
        private int? _SubserviceTypeID;
        private int? _TrafficTypeID;

        private IList<RoamingZoneDetailInfo> _RoamingZoneDetailList = new List<RoamingZoneDetailInfo>();

        #endregion

        #region Attribute

        public int? ZoneID
        {
            get { return _ZoneID; }
            set { _ZoneID = value; }
        }

        public string ZoneName
        {
            get { return _ZoneName; }
            set { _ZoneName = value; }
        }

        public int? DealerID
        {
            get { return _DealerID; }
            set { _DealerID = value; }
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
 
        public int? SubserviceTypeID
        {
            get { return _SubserviceTypeID; }
            set { _SubserviceTypeID = value; }
        }

        public int? TrafficTypeID
        {
            get { return _TrafficTypeID; }
            set { _TrafficTypeID = value; }
        }

        public IList<RoamingZoneDetailInfo> RoamingZoneDetailList
        {
            get { return _RoamingZoneDetailList; }
            set { _RoamingZoneDetailList = value; }
        }
        #endregion

    }
}
