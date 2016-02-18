using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class InternationalZoneInfo
    {
        #region
        private int? _ZoneID = null;
        private string _ZoneName;
        private int? _DealerID = null;
        private DateTime? _StartDate = null;
        private DateTime? _EndDate = null;

        private IList<InternationalZoneDetailInfo> _InternationalZoneDetailList = new List<InternationalZoneDetailInfo>();

      
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


        public IList<InternationalZoneDetailInfo> InternationalZoneDetailList
        {
            get { return _InternationalZoneDetailList; }
            set { _InternationalZoneDetailList = value; }
        }

        #endregion
    }
}
