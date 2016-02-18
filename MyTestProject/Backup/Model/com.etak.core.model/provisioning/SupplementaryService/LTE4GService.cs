using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    /// <summary>
    /// Represents LTE 4G service.
    /// </summary>
    [Serializable]
    public class LTE4GService : SuplementaryService
    {
        [DataMember]
        public SuplementaryService[] Services { get; set; }
    }
}
