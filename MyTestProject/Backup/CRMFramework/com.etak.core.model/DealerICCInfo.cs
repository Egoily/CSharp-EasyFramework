using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class DealerICCInfo
    {
        private int? _DealerID = null;        
        private int? _RangeSEQ = null;        
        private string _ICCID1;        
        private string _ICCID2;        
        private int? _Total = null;        
        private int? _TotalUsed = null;        
        private string _RangeUsed;        
        private string _RangeAvailable;        

        

        #region Attribute
        public virtual int? DealerID
        {
            get { return _DealerID; }
            set { _DealerID = value; }
        }

        public virtual int? RangeSEQ
        {
            get { return _RangeSEQ; }
            set { _RangeSEQ = value; }
        }

        public virtual string ICCID1
        {
            get { return _ICCID1; }
            set { _ICCID1 = value; }
        }

        public virtual string ICCID2
        {
            get { return _ICCID2; }
            set { _ICCID2 = value; }
        }

        public virtual int? Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        public virtual int? TotalUsed
        {
            get { return _TotalUsed; }
            set { _TotalUsed = value; }
        }

        public virtual string RangeUsed
        {
            get { return _RangeUsed; }
            set { _RangeUsed = value; }
        }

        public virtual string RangeAvailable
        {
            get { return _RangeAvailable; }
            set { _RangeAvailable = value; }
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

            var other = obj as DealerICCInfo;
            if (other == null) return false;

            if (DealerID == other.DealerID && RangeSEQ == other.RangeSEQ)
                return true;

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
                int result = DealerID.GetHashCode();
                result = 29* result + RangeSEQ.GetHashCode();

                return result;
            }
        }
        
    }
}
