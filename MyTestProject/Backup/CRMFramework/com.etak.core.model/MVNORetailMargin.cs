using com.etak.core.model.provisioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class MVNORetailMargin
    {
        public virtual int Id { get; set; }
        public virtual DealerInfo Dealer { get; set; }
        public virtual Carrier Carrier { get; set; }
        public virtual decimal MarginValue { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        
    }
}
