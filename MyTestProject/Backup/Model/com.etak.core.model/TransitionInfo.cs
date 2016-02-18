using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class TransitionInfo
    {
        public virtual int TransitionId { get; set; }
        public virtual int EventId { get; set; }
        public virtual int CurrentStatusId { get; set; }
        public virtual int NextStatusId { get; set; }
        public virtual int NextNPMStstusId { get; set; }
        public virtual int NextICCStatusId { get; set; }
        public virtual int ActionId { get; set; }
    }
}
