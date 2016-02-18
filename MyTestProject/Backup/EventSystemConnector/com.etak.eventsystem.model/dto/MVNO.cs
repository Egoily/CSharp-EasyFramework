using System;
using System.Runtime.Serialization;

namespace com.etak.eventsystem.model.dto
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]    
    public class MVNO
    {
        [DataMember(EmitDefaultValue = false)]
        public virtual Int64 ID { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual String OperatorCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual String MVNOName { get; set; }
    }
}
