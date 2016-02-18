using System;

namespace com.etak.core.model
{
    [Serializable]
    public class CrmCustomersMSISDNGroupMembersHistory
    {
        private int customerID;
        public virtual int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        private int _MSISDNGroupTypeId;
        public virtual int MSISDNGROUPTYPEID
        {
            get { return _MSISDNGroupTypeId; }
            set { _MSISDNGroupTypeId = value; }
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

        private DateTime endDate;
        public virtual DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj is CrmCustomersMSISDNGroupMembersHistory)
            {
                CrmCustomersMSISDNGroupMembersHistory second = obj as CrmCustomersMSISDNGroupMembersHistory;
                if (this.customerID == second.CustomerID
                     && this.msisdn == second.MSISDN
                    && this.MSISDNGROUPTYPEID == second.MSISDNGROUPTYPEID)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
