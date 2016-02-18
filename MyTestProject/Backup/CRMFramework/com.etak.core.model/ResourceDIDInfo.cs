using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class ResourceDIDInfo
    {
        private ResourceMBInfo resourceMBInfo;
        private int resourceID;
        private string didnumber;
        private DateTime? startDate;
        private DateTime? endDate;

        public virtual ResourceMBInfo ResourceMBInfo
        {
            get { return resourceMBInfo; }
            set { resourceMBInfo = value; }
        }

        public virtual int ResourceID
        {
            get { return resourceID; }
            set { resourceID = value; }
        }

        public virtual string DIDNumber
        {
            get { return didnumber; }
            set { didnumber = value; }
        }

        public virtual DateTime? StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public virtual DateTime? EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public virtual ResourceDIDInfo Clone()
        {
            return this.MemberwiseClone() as ResourceDIDInfo;
        }
    }
}