using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class PackageBussinessRules
    {
        public virtual int ID { get; set; }
        public virtual PackageInfo PackageInfo { get; set; }
        public virtual PackageRule RuleInfo { get; set; }

        public override bool Equals(object obj)
        {
            PackageBussinessRules toCompare = obj as PackageBussinessRules;
            if (toCompare == null)
            {
                return false;
            }
            if (this.PackageInfo.PackageID != toCompare.PackageInfo.PackageID ||
                this.RuleInfo.Id != toCompare.RuleInfo.Id ||
                this.ID != toCompare.ID)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode() + PackageInfo.GetHashCode() + RuleInfo.GetHashCode();
        }
    }    
}