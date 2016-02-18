using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    public enum TraficDirection
    {
        [EnumMember]
        Download,
        [EnumMember]
        Upload
    }

    public enum TrafficClasses
    {
        [EnumMember]
        Conversational,
        [EnumMember]
        Streaming,
        [EnumMember]
        Interactive,
        [EnumMember]
        Background
    }

    [Serializable]
    abstract public class APNQoSSetting
    {
    }

    /// <summary>
    /// Class to indicate the QoS based on 3GPP document: 3GPP TS  23.107
    /// </summary>
    [Serializable]
    public class DetailedAPNQoSSetting : APNQoSSetting
    {
        [DataMember]
        public TraficDirection Direction { get; set; }

        /// <summary>
        /// By including the traffic class itself as an attribute, UMTS can make assumptions about the traffic
        //  source and optimise the transport for that traffic type.
        /// </summary>
        [DataMember]
        public TrafficClasses? TraficClass { get; set; }

        /// <summary>
        ///  maximum number of bits delivered by UMTS and to UMTS at a SAP within a period of time, divided by the
        ///duration of the period. The traffic is conformant with Maximum bitrate as long as it follows a token bucket algorithm
        ///where token rate equals Maximum bitrate
        ///and bucket size equals Maximum SDU size
        /// </summary>
        [DataMember]
        public Int32? MaximunBitRate { get; set; }
        
        /// <summary>
        /// guaranteed number of bits delivered by UMTS at a SAP within a period of time (provided that there is data
        ///to deliver), divided by the duration of the period. The traffic is conformant with the guaranteed bitrate as long as it
        ///follows a token bucket algorithm where token rate equals Guaranteed bitrate and bucket size equals Maximum SDU size. 
        /// </summary>
        [DataMember]
        public Int32? GuaranteedBitRate { get; set; }

        /// <summary>
        ///  indicates whether the UMTS bearer shall provide in-sequence SDU delivery or not. 
        /// </summary>
        [DataMember]
        public Boolean? DeliveryOrder { get; set; }

        /// <summary>
        /// the maximum SDU size for which the network shall satisfy the negotiated QoS.
        /// </summary>
        [DataMember]
        public Int32? MaximumSDUSize { get; set; }

        /// <summary>
        /// Definition: list of possible exact sizes of SDUs (in bits)
        /// </summary>
        [DataMember]
        public Int32[] SDUFormatInformation { get; set; }

        /// <summary>
        /// Definition: Indicates the fraction of SDUs lost or detected as
        /// erroneous. SDU error ratio is defined only for conforming traffic. 
        /// </summary>
        [DataMember]
        public float? SDUErrorRatio { get; set; }


        /// <summary>
        /// Indicates the undetected bit error ratio in the deliver 
        /// ed SDUs. If no error detection is requested, Residual bit
        /// error ratio indicates the bit error ratio in the delivered SDUs. 
        /// </summary>
        [DataMember]
        public float? ResidualBitErrorRatio { get; set; }

        /// <summary>
        /// Indicates whether SDUs detected as erroneous shall be delivered or discarded.
        /// NOTE 2: 'yes' implies that error detection is employed and
        /// that erroneous SDUs are delivered together with an error
        /// indication, 'no' implies that error detection is employed and that erroneous SDUs are discarded, and '-' (null)
        /// implies that SDUs are delivered without considering error detection. 
        /// </summary>
        [DataMember]
        public Boolean? DeliveryOfErroneousSDUs { get; set; }

        /// <summary>
        /// Indicates maximum delay for 95th percentile of the distribution of delay for all delivered SDUs during the
        /// lifetime of a bearer service, where delay for an SDU is defined as the time from a request to transfer an SDU at one
        /// SAP to its delivery at the other SAP 
        /// </summary>
        [DataMember]
        public Int32? TransferDelayMiliseconds { get; set; }

        /// <summary>
        /// specifies the relative importance for handling of all SDUs belonging to the UMTS bearer compared to the
        /// SDUs of other bearers. Within the interactive class, there is a definite need to differentiate between bearer qualities. This
        /// is handled by using the traffic handling priority attribute, to allow UMTS to schedule traffic
        /// accordingly. By definition, priority is an alternative to absolute guarantees, and thus these two
        /// attribute types cannot be used together for a single beare
        /// </summary>
        [DataMember]
        public Int32? TrafficHandlingPriority { get; set; }
    }

    /// <summary>
    /// This class is used to specify a template for the QoS rather than the values.
    /// </summary>
    [Serializable]
    public class TemplateAPNQoSSetting : APNQoSSetting
    {
        /// <summary>
        /// the QoS name and setting name, 
        /// </summary>
        [DataMember]
        public string TemplateName { get; set; }

        /// <summary>
        /// the QoS name templeate id 
        /// </summary>
        [DataMember]
        public string TemplateId { get; set; }
    }

}
