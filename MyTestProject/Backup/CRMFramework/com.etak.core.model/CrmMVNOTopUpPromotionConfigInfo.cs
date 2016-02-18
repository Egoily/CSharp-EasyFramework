using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class CrmMVNOTopUpPromotionConfigInfo
    {       
        #region Member
        private int? _DealerID;        
        private decimal? _StartAmount;        
        private bool _StartAmountIncluded;
        private decimal? _EndAmount;
        private bool _EndAmountIncluded;
        private int? _PromotionID;
        private int? _DurationUnit;
        private int? _Duration;
        private DateTime? _StartDate;
        private DateTime? _EndDate;
        private bool _Enabled;
        private int? _Priority;        
        #endregion


        #region Attribute
        public virtual int? DealerID
        {
            get { return _DealerID; }
            set { _DealerID = value; }
        }

        public virtual decimal? StartAmount
        {
            get { return _StartAmount; }
            set { _StartAmount = value; }
        }

        public virtual bool StartAmountIncluded
        {
            get { return _StartAmountIncluded; }
            set { _StartAmountIncluded = value; }
        }

        public virtual decimal? EndAmount
        {
            get { return _EndAmount; }
            set { _EndAmount = value; }
        }

        public virtual bool EndAmountIncluded
        {
            get { return _EndAmountIncluded; }
            set { _EndAmountIncluded = value; }
        }

        public virtual int? PromotionID
        {
            get { return _PromotionID; }
            set { _PromotionID = value; }
        }

        public virtual int? DurationUnit
        {
            get { return _DurationUnit; }
            set { _DurationUnit = value; }
        }

        public virtual int? Duration
        {
            get { return _Duration; }
            set { _Duration = value; }
        }

        public virtual DateTime? StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        public virtual DateTime? EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        public virtual bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        public virtual int? Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }
        #endregion

        /// <summary>
        /// Override Equals method needed by Nhibernate to map the entity 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;

            var other = obj as CrmMVNOTopUpPromotionConfigInfo;
            if (other == null) return false;

            //Check all the fields that compounds the key
            if (other.DealerID == DealerID
                && other.StartAmountIncluded == StartAmountIncluded
                && other.StartAmount == StartAmount
                && other.EndAmount == EndAmount
                && other.StartDate == StartDate
                && other.Enabled == Enabled)
            {
                return true;
            }
            

            return false;

        }

        /// <summary>
        /// Override GetHashCode method needed by Nhibernate to map the entity
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int result;
                result = DealerID.GetHashCode();
                result = 29 * result + StartAmountIncluded.GetHashCode();
                result = 38 * result + StartAmount.GetHashCode();
                result = 47 * result + EndAmount.GetHashCode();
                result = 56 * result + StartDate.GetHashCode();
                result = 65 * result + Enabled.GetHashCode();
                return result;
            }
        }

    }
}
