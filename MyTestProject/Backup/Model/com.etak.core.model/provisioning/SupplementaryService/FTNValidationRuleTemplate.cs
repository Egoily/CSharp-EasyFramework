using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    /// <summary>
    /// Represents the FTN Rule template.
    /// </summary>
    [Serializable]
    public class FTNValidationRuleTemplate : SuplementaryService
    {
        [DataMember]
        public String TemplateID { get; set; }
        [DataMember]
        public String TemplateName { get; set; }
    }
}
