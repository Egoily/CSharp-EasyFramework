using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    [Serializable]
    public class KeyValue
    {
        [DataMember]
        public object Key { get; set; }
        [DataMember]
        public object Value { get; set; }
    }
}
