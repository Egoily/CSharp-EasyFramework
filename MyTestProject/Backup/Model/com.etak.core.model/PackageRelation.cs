using System;

namespace com.etak.core.model
{
    [Serializable]
    public class PackageRelation
    {
        public virtual Int32 Id { get; set; }
        public virtual PackageInfo OriginPacket { get; set; }
        public virtual PackageInfo TargetPacket { get; set; }
    }
}
