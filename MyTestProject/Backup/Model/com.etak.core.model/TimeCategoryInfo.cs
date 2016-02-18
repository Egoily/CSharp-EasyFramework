using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class TimeCategoryInfo
    {
        #region 
       
        private string _TimeCategoryName;
        private int? _StartTime = null;
        private int? _EndTime = null;
        private string _DayOfWeek;
        private string _Remark;


        private TimeCategoryPKInfo _PKInfo;

        public TimeCategoryPKInfo PKInfo
        {
            get { return _PKInfo; }
            set { _PKInfo = value; }
        }
        #endregion
        
        #region Attribute
        public string TimeCategoryName
        {
            get { return _TimeCategoryName; }
            set { _TimeCategoryName = value; }
        }

        public int? StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }

        public int? EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }

        public string DayOfWeek
        {
            get { return _DayOfWeek; }
            set { _DayOfWeek = value; }
        }

        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        #endregion

        public override bool Equals(object obj)
        {
            if (obj is TimeCategoryInfo)
            {
                TimeCategoryInfo second = obj as TimeCategoryInfo;
                if (this.PKInfo.TimeCategoryID == second.PKInfo.TimeCategoryID
                     && this.PKInfo.TimeCategoryID == second.PKInfo.TimeCategoryIDSeq)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = PKInfo.TimeCategoryID.GetHashCode();
                result = 29 * result + PKInfo.TimeCategoryIDSeq.GetHashCode();

                return result;
            }
        }

    }

    [DataContract]
    [Serializable]
    public class TimeCategoryPKInfo
    {
        #region 
        private int? _TimeCategoryID = null;
        private int? _TimeCategoryIDSeq = 0;
        #endregion

        #region Attribute
        public int? TimeCategoryID
        {
            get { return _TimeCategoryID; }
            set { _TimeCategoryID = value; }
        }

        public int? TimeCategoryIDSeq
        {
            get { return _TimeCategoryIDSeq; }
            set { _TimeCategoryIDSeq = value; }
        }
        #endregion

        /// <summary>
        /// Override Equals method needed by Nhibernate to map the entity 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == this) return true;
            var other = obj as TimeCategoryPKInfo;
            if (other == null) return false;

            if (TimeCategoryID == other.TimeCategoryID
                && TimeCategoryID == other.TimeCategoryIDSeq)
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
                int result = TimeCategoryID.GetHashCode();
                result = 29*result + TimeCategoryIDSeq.GetHashCode();

                return result;
            }
        }
    }
}
