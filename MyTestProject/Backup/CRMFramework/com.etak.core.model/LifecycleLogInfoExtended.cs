using System;

namespace com.etak.core.model
{
    [Serializable]
    public class LifecycleLogInfoExtended
    {
        public virtual Int64 ExtendId { get; set; }
        public virtual LifecycleLogInfo LCLogInfo { get; set; }
        public virtual Int32 TopUpType { get; set; }
        public virtual String NormalChargeTime { get; set; }
        public virtual String NormalChargeTime2 { get; set; }
    }
}
