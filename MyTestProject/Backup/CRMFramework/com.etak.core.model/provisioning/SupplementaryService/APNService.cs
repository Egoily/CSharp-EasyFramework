using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    
    public enum IPVersion
    {
        /// <summary>
        /// The IPV4 address, which is default
        /// </summary>
        [EnumMember]
        IPV4_Address = 0xF121,

        /// <summary>
        /// The IPV6 address.
        /// </summary>
        [EnumMember]
        IPV6_Address = 0xF157,

        /// <summary>
        /// X25 Address.
        /// </summary>
        [EnumMember]
        X25_Address = 0xF000,

        /// <summary>
        /// PPP Address.
        /// </summary>
        [EnumMember]
        PPP_Address = 0xF001,

        /// <summary>
        /// OSP IHOST Address.
        /// </summary>
        [EnumMember]
        OSP_IHOST_Address = 0xF002,

        /// <summary>
        /// Not defined.
        /// </summary>
        [EnumMember]
        Undefined = 0,
    }

    [Serializable]
    abstract public class APNServiceBase : SuplementaryService
    {
        [DataMember]
        public string APNName { get; set; }
        [DataMember]
        public int APNIndex { get; set; }
        [DataMember]
        public APNQoSSetting DownStreamQoS { get; set; }
        [DataMember]
        public APNQoSSetting UpStreamQoS { get; set; }
        [DataMember]
        public String PdpContextAddress { get; set; }
        [DataMember]
        public IPVersion IPVersion { get; set; }   
    }

    /// <summary>
    /// This class is used to specify a single APN service.
    /// </summary>
    [Serializable]
    public class APNService : APNServiceBase
    {        
        
    }

    /// <summary>
    /// This class is used to specify a template for the APN rather than the values.
    /// </summary>
    [Serializable]
    public class TemplatedAPNService : APNServiceBase
    {
        [DataMember]
        public string TemplateId { get; set; }
    }

    /// <summary>
    /// This class is used to specify the user equipment level APN service.
    /// </summary>
    [Serializable]
    public class UserEquipmentAPNService : APNServiceBase
    {
    }
}
