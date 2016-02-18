using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    
    public enum NetworkAccessModelType
    {
        [EnumMember]
        BothGPRSandNonGPRS = 0,
        [EnumMember]
        OnlyNonGPRS = 1,
        [EnumMember]
        OnlyGPRS = 2,
    }

    /// <summary>
    /// Represents the network access mode.
    /// </summary>
    [Serializable]
    public class NetworkAccessModel : SuplementaryService
    {
        [DataMember]
        public NetworkAccessModelType networkAccessModelType = NetworkAccessModelType.BothGPRSandNonGPRS;
    }
}
