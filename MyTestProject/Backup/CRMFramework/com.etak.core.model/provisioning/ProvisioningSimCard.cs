//using System;
//using System.Runtime.Serialization;
//using System.Xml.Serialization;

//namespace com.etak.core.model.provisioning
//{
//    public enum SimFormat
//    {
//        [EnumMember]
//        Full_size,
//        [EnumMember]
//        Mini_SIM,
//        [EnumMember]
//        Micro_SIM,
//        [EnumMember]
//        Nano_SIM,
//        [EnumMember]
//        Embedded_SIM,
//        [EnumMember]
//        NotSpecified,
//    }

//    [KnownType(typeof(PlainTextKey))]
//    [KnownType(typeof(EncryptedKey))]
//    abstract public class SimKey
//    {
//    }

//    public class PlainTextKey : SimKey
//    {
//        [DataMember]
//        public String PlainKey { get; set; }
//    }


//    public class EncryptedKey : SimKey
//    {
//        [DataMember]
//        public String EncKey { get; set; }
//        [DataMember]
//        public String HLRIndexKeyForEncryption { get; set; }
//    }

//    public enum SimStatus
//    {
//        /// <summary>
//        /// The SIM is installed in HLR SIM module.
//        /// </summary>
//        [EnumMember]
//        Installed = 0,
//        /// <summary>
//        /// The SIM is installed in HLR SIM module, and has been assoicated to a Subscriber (MSISDN)
//        /// </summary>
//        [EnumMember]
//        Active = 1,
//        /// <summary>
//        /// The SIM is detached from the subscriber, but, it can be assoicated to other subscriber again.
//        /// </summary>
//        [EnumMember]
//        Delete = 2,
//    }


//    public abstract class ProvisioningSimCard
//    {
//        [DataMember]
//        public String[] IMSI { get; set; }
//        [DataMember]
//        public String ICCID { get; set; }
//        [DataMember]
//        public SimKey Key { get; set; }
//        [DataMember]
//        public SimStatus? Status { get; set; }
//        [DataMember]
//        public SimFormat? PhisicalFormat { get; set; }
//    }


//    public class SimCard2G : ProvisioningSimCard
//    {
//    }

//    public class SimCard3G : ProvisioningSimCard
//    {
//    }

//    public class SimCard4G : ProvisioningSimCard
//    {
//    }
//}
