using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class UserDealerInfo
    {
        public virtual Int32 ID { get; set; }
        public virtual Int32 UserID { get; set; }
        public virtual Int32 DealerID { get; set; }
     //   public virtual LoginInfo User { get; set; }
    }
}
