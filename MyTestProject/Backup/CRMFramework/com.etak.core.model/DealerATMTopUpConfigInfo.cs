using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class DealerATMTopUpConfigInfo
    {
       
        private int? _DealerID = null;
        private int? _MaxTimesPreday = null;
        private decimal? _MaxTotalAmountPreday = null;
        private decimal? _MinAmountPertime = null;

        private DealerInfo _DealerInfo;
       

        #region Attribute
        public virtual int? DealerID
        {
            get { return _DealerID; }
            set { _DealerID = value; }
        }

        public virtual int? MaxTimesPreday
        {
            get { return _MaxTimesPreday; }
            set { _MaxTimesPreday = value; }
        }

        public virtual decimal? MaxTotalAmountPreday
        {
            get { return _MaxTotalAmountPreday; }
            set { _MaxTotalAmountPreday = value; }
        }

        public virtual decimal? MinAmountPertime
        {
            get { return _MinAmountPertime; }
            set { _MinAmountPertime = value; }
        }

        public virtual DealerInfo DealerInfo
        {
            get { return _DealerInfo; }
            set { _DealerInfo = value; }
        }
        #endregion

    }
}
