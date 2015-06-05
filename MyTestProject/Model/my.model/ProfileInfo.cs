using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract, Serializable]
    public class ProfileInfo
    {
        public virtual Int64 Id { get; set; }

        public virtual PersonInfo Owner { get; set; }

        public virtual AddressInfo Address { get; set; }

        public virtual string Phone { get; set; }

        public virtual string Email { get; set; }

        public virtual PersonInfo Father { get; set; }

        public virtual PersonInfo Mother { get; set; }

        public virtual PersonInfo Spouse { get; set; }
    }
}