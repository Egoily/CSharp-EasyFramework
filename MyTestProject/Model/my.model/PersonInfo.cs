using System;
using System.Runtime.Serialization;

using com.etak.core.model.Enums;

namespace com.etak.core.model
{
    [DataContract, Serializable]
    public class PersonInfo
    {
        public virtual Int64 Id { get; set; }

        public virtual string Surname { get; set; }

        public virtual string GivenName { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual int Age { get; set; }

        public virtual string Comments { get; set; }

        public virtual ProfileInfo Profile { get; set; }
    }
}