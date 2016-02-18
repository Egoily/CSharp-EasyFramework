using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class CrmCustomersMSISDNGroupMembers
    {
        private int customerID;
        public virtual int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        private int mSISDNGroupTypeId;
        public virtual int MSISDNGroupTypeId
        {
            get { return mSISDNGroupTypeId; }
            set { mSISDNGroupTypeId = value; }
        }

        private string msisdn;
        public virtual string MSISDN
        {
            get { return msisdn; }
            set { msisdn = value; }
        }

        private DateTime startDate;
        public virtual DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj is CrmCustomersMSISDNGroupMembers)
            {
                CrmCustomersMSISDNGroupMembers second = obj as CrmCustomersMSISDNGroupMembers;
                if (this.customerID == second.CustomerID
                     && this.msisdn == second.MSISDN
                    && this.MSISDNGroupTypeId == second.MSISDNGroupTypeId)
                {
                    return true;
                }
                else return false;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
