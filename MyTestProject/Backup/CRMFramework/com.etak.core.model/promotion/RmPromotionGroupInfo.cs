using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class RmPromotionGroupInfo
    {
        public RmPromotionGroupInfo() 
        {
            Members = new List<RmPromotionGroupMember>();
            Thresholds = new List<RmPromotionGroupThreshold>();
        }

        public virtual int PromotionGroupID { get; set; }

        public virtual int MvnoID { get; set; }

        public virtual string GroupName { get; set; }

        public virtual string Description { get; set; }

        public virtual decimal? Price { get; set; }

        public virtual int? Validity { get; set; }

        public virtual int? Priority { get; set; }

        public virtual int? GroupType { get; set; }
        public virtual int? GroupCategory { get; set; }
        public virtual int? StartPeriod { get; set; }
        public virtual int? EndPeriod { get; set; }
        public virtual int? GracePeriod { get; set; }

        public virtual IList<RmPromotionGroupMember> Members { get; set; }
        public virtual IList<RmPromotionGroupThreshold> Thresholds { get; set; }
        public virtual IList<PromotionGroupBusinessRules> GroupRules { get; set; } 
    }
}
