using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class RatePlanInfo
    {
        #region
        private int? _RatePlanID = null;
        private string _RatePlanName;
        private int? _DealerID = null;
        private int? _ServiceTypeID = null;
        private int? _SubserviceType = null;


       
        #endregion

        #region Attribute

        public int? RatePlanID
        {
            get { return _RatePlanID; }
            set { _RatePlanID = value; }
        }
 
        public string RatePlanName
        {
            get { return _RatePlanName; }
            set { _RatePlanName = value; }
        }

        public int? DealerID
        {
            get { return _DealerID; }
            set { _DealerID = value; }
        }

        public int? ServiceTypeID
        {
            get { return _ServiceTypeID; }
            set { _ServiceTypeID = value; }
        }

        public int? SubserviceType
        {
            get { return _SubserviceType; }
            set { _SubserviceType = value; }
        }

        int _RATETYPE;
        public int RateType
        {
            get
            {
                return _RATETYPE;
            }
            set
            {
                _RATETYPE = value;
            }
        }
   
        #endregion
    }
}
