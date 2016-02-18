using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    /// <summary>
    /// Represents an APN template
    /// </summary>
    [Serializable]
    public class ProvisioningApnTemplate
    {
        [DataMember]
        public String TemplateName { get; set; }
        [DataMember]
        public String TemplateId { get; set; }
        [DataMember]
        public DetailedAPNQoSSetting DownStreamSettings { get; set; }
        [DataMember]
        public DetailedAPNQoSSetting UpStreamSettings { get; set; }
    }

    /// <summary>
    /// Represents an LTE 4G APN template
    /// </summary>
    public class LTE4GProvisioningApnTemplate : ProvisioningApnTemplate
    {
        [DataMember]
        public String APNName { get; set; }
    }
}
