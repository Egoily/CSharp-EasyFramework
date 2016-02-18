using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class CrmCustomersPromotionGroup
    {
        public virtual long ID { get; set; }

        public virtual int CustomerID { get; set; }

        //public virtual int PromotionGroupID { get; set; }

        public virtual int Status { get; set; }

        public virtual DateTime StartDate { get; set; }


        public virtual RmPromotionGroupInfo PromotionGroup { get; set; }

    }
}
