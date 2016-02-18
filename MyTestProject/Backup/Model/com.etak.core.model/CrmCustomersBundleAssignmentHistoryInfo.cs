using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class CrmCustomersBundleAssignmentHistoryInfo
    {
        #region 
        private int? _CreditLimitBaseBundleID = null;
        private DateTime? _EndDate = null;
        private long? _HistoryID = null;
       
        private CrmCustomersBundleAssignmentHistoryPKInfo _PKInfo;

        public CrmCustomersBundleAssignmentHistoryPKInfo PKInfo
        {
            get { return _PKInfo; }
            set { _PKInfo = value; }
        }
        #endregion
        
        #region Attribute

        public int? CreditLimitBaseBundleID
        {
            get { return _CreditLimitBaseBundleID; }
            set { _CreditLimitBaseBundleID = value; }
        }

        public DateTime? EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        public long? HistoryID
        {
            get { return _HistoryID; }
            set { _HistoryID = value; }
        }
        //added by neil at 2014/6/6
        public DateTime TimeStamp { get; set; }
        #endregion

        public override bool Equals(object obj)
        {
            if (obj is CrmCustomersBundleAssignmentHistoryInfo)
            {
                CrmCustomersBundleAssignmentHistoryInfo second = obj as CrmCustomersBundleAssignmentHistoryInfo;
                if (this.PKInfo.CustomerID == second.PKInfo.CustomerID
                    && this.PKInfo.ServiceID == second.PKInfo.ServiceID
                    && this.PKInfo.BundleID == second.PKInfo.BundleID
                    && this.PKInfo.StartDate == second.PKInfo.StartDate)
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
                int result;
                result = PKInfo.CustomerID.GetHashCode();
                result = 29 * result + PKInfo.BundleID.GetHashCode();
                return result;
            }
        }
    }

    [DataContract]
    [Serializable]
    public class CrmCustomersBundleAssignmentHistoryPKInfo
    {
        #region 
        private int? _CustomerID = null;
        private int? _ServiceID = null;
        private int? _BundleID = null;
        private DateTime? _StartDate = null;
        #endregion

        #region Attribute
        public int? CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }

        public int? ServiceID
        {
            get { return _ServiceID; }
            set { _ServiceID = value; }
        }

        public int? BundleID
        {
            get { return _BundleID; }
            set { _BundleID = value; }
        }

        public DateTime? StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
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
            var other = obj as CrmCustomersBundleAssignmentHistoryPKInfo;
            if (other == null) return false;

            if (other.CustomerID == CustomerID && other.ServiceID == ServiceID
                && other.BundleID == BundleID && other.StartDate == StartDate)
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
                result = CustomerID.GetHashCode();
                result = 29 * result + BundleID.GetHashCode();
                return result;
            }
        }
    }
}