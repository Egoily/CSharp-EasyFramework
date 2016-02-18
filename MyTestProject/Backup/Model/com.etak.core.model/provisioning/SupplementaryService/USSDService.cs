using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    /// <summary>
    /// Represents the USSD Code that can be used by MS subscriber ask for some predefined services in operator side.
    /// </summary>
    [Serializable]
    public class USSDService : SuplementaryService
    {

    }
    
    /// <summary>
    /// Represents the USSD template include set of USSD code.
    /// </summary>
    [Serializable]
    public class USSDTemplateService : SuplementaryService
    {
        [DataMember]
        public String TemplateName { get; set; }
    }
}
