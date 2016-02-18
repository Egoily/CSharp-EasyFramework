using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    public enum RadioAccessNetworkTypes
    {
        /// <summary>
        /// GSM Edge Radio Access Network
        /// </summary>
        [EnumMember]
        GERAN,

        /// <summary>
        /// UMTS Terrestrial Radio Access Network
        /// </summary>
        [EnumMember]
        UTRAN,

        /// <summary>
        /// Evolved UMTS Terrestrial Radio Access Network
        /// </summary>
        [EnumMember]
        E_UTRAN,
    }

    [Serializable]
    public class RadioAccessNetwork : SuplementaryService
    {
       [DataMember]
       public RadioAccessNetworkTypes Type { get; set; }
    }
}
