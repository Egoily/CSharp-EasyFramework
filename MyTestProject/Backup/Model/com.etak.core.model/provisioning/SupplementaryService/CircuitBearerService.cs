using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    
    public enum CircuitBearerServices
    {
        /*
         * 20	Asynchronous General Bearer Service
         * 30	Synchronous General Bearer Service
         */
        /// <summary>
        /// General asynchronous bearer service
        /// </summary>
        [EnumMember]
        BS20,
        /// <summary>
        /// Data circuit duplex asynch. 300 bit/s 
        /// </summary>
        [EnumMember]
        BS21,
        /// <summary>
        ///  Data circuit duplex asynch. 1 200 bit/s
        /// </summary>
        [EnumMember]
        BS22,

        /// <summary>
        /// Data circuit duplex asynch. 1 200/75 bit/s
        /// </summary>
        [EnumMember]
        BS23,

        /// <summary>
        /// Data circuit duplex asynch. 2 400 bit/s
        /// </summary>
        [EnumMember]
        BS24,
        /// <summary>
        /// Data circuit duplex asynch. 4 800 bit/s
        /// </summary>
        [EnumMember]
        BS25,

        /// <summary>
        /// Data circuit duplex asynch. 9 600 bit/s
        /// </summary>
        [EnumMember]
        BS26,

        /// <summary>
        /// Synchronous General Bearer Service
        /// </summary>
        [EnumMember]
        BS30,

        /// <summary>
        /// Synchronous 1.2 kbps
        /// </summary>
        [EnumMember]
        BS31,

        /// <summary>
        /// Synchronous 2.4 kbps
        /// </summary>
        [EnumMember]
        BS32,

        /// <summary>
        /// Synchronous 4.8 kbps
        /// </summary>
        [EnumMember]
        BS33,
        /// <summary>
        /// Synchronous 9.6 kbps
        /// </summary>
        [EnumMember]
        BS34,
        /// <summary>
        /// General PAD Access Bearer Service
        /// </summary>
        [EnumMember]
        BS40,
        /// <summary>
        /// PAD Access 300 bps
        /// </summary>
        [EnumMember]
        BS41,
        /// <summary>
        /// PAD Access 1.2 kbps
        /// </summary>
        [EnumMember]
        BS42,
        /// <summary>
        /// PAD Access 1200/75 bps
        /// </summary>
        [EnumMember]
        BS43,
        /// <summary>
        /// PAD Access 2.4 kbps
        /// </summary>
        [EnumMember]
        BS44,
        /// <summary>
        /// PAD Access 4.8 kbps
        /// </summary>
        [EnumMember]
        BS45,

        /// <summary>
        /// PAD Access 9.6 kbps
        /// </summary>
        [EnumMember]
        BS46,

        /// <summary>
        /// General Packet Access Bearer Service
        /// </summary>
        [EnumMember]
        BS50,

        /// <summary>
        /// Packet Access 2.4 kbps
        /// </summary>
        [EnumMember]
        BS51,

        /// <summary>
        /// Packet Access 4.8 kbps
        /// </summary>
        [EnumMember]
        BS52,

        /// <summary>
        /// Packet Access 9.6 kbps
        /// </summary>
        [EnumMember]
        BS53,

        /// <summary>
        /// Alternate Speech/Data
        /// </summary>
        [EnumMember]
        BS61,
        /// <summary>
        /// Speech Followed by Data
        /// </summary>
        [EnumMember]
        BS81,

    }

    /// <summary>
    /// This class is used to defined the circuit bearer service of subscriber.
    /// </summary>
    [Serializable]
    public class CircuitBearerService : SuplementaryService
    {
        [DataMember]
        public CircuitBearerServices Service { get; set; }       
    }
}
