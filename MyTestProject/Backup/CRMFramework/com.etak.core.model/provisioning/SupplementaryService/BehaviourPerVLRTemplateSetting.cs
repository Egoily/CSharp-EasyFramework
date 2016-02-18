using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    [Serializable]
    public class BehaviourPerVLRTemplate : SuplementaryService
    {
        [DataMember]
        public String TemplateId { get; set; }
        [DataMember]
        public String TemplateName { get; set; }
    }
}
