using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [Serializable]
    public enum ProvisioningType
    {
        /// <summary>
        /// 
        /// </summary>
        Sim,
        /// <summary>
        /// 
        /// </summary>
        AddressOfRecord,
        /// <summary>
        /// 
        /// </summary>
        SubscriberProfile,
        /// <summary>
        /// 
        /// </summary>
        SipSubscriberProfile,
        /// <summary>
        /// 
        /// </summary>
        CallBarringSS,
        /// <summary>
        /// 
        /// </summary>
        CallBarringOG_BSG,
        /// <summary>
        /// 
        /// </summary>
        CallBarringSubsOption,
        /// <summary>
        /// 
        /// </summary>
        CallForward,
        /// <summary>
        /// 
        /// </summary>
        CallForwardBsg,
        /// <summary>
        /// 
        /// </summary>
        CamelSubsInfo,
        /// <summary>
        /// 
        /// </summary>
        CamelTdp,
        /// <summary>
        /// 
        /// </summary>
        HlrCamelUCsi,
        /// <summary>
        /// 
        /// </summary>
        CugFeature,
        /// <summary>
        /// 
        /// </summary>
        CugSubscription,
        /// <summary>
        /// 
        /// </summary>
        GprsContext,
        /// <summary>
        /// 
        /// </summary>
        MobileStation,
        /// <summary>
        /// 
        /// </summary>
        MultiImsi,
        /// <summary>
        /// 
        /// </summary>
        SSCallWaitActivStatus,
        /// <summary>
        /// 
        /// </summary>
        SSProvisionStatus,

        /// <summary>
        /// 
        /// </summary>
        CamelCsiDP_Ussd,
    }

    public enum PreferredRoutingNetworkDomain
    {
        Sip = 0, Gsm = 1,
    }

    public enum CallForwardBsg
    {
        Speech = 1,
        FacsimileServices = 6,
        AllDataCircuitAsynchronous = 7,
        AllDataCircuitSynchronous = 8,
        VoiceGroupServices = 12
    }

    public enum CallBarringBSG
    {
        Speech = 1,
        ShortMessageService = 2,
        FacsimileServices = 6,
        AllDataCircuitAsynchronous = 7,
        AllDataCircuitSynchronous = 8,
        VoiceGroupServices = 12,
    }

    public enum NetWorkAccessMode
    {
        NonGprsAndGprs = 0,
        NonGprsOnly = 1,
        GprsOnly = 2,
    }

    public enum CallForwardType
    {
        CFU = 33,
        CFB = 41,
        CFNRY = 42,
        CFNRC = 43,
    }

    public enum CallBarringSS
    {
        BAOC = 146,
        BOIC = 147,
        BOICEXHC = 148,
        BAIC = 154,
        BICROAM = 155,
    }

    public enum PdpType
    {
        X25 = 0,
        PPP = 1,
        OspIhoss = 2,
        IPv4 = 33,
        IPv6 = 87,
    }

    public enum QosPeakThroughput
    {
        UpTo1KoctetPerS = 1,
        UpTo2KoctetPerS,
        UpTo4KoctetPerS,
        UpTo8KoctetPerS,
        UpTo16KoctetPerS,
        UpTo32KoctetPerS,
        UpTo64KoctetPerS,
        UpTo128KoctetPerS,
    }

    public enum QosPrecedenceClass
    {
        HighPriority = 1,
        NormalPriority,
        LowPriority,
    }

    public enum QosAllocationRetentionPriority
    {
        HighPriority = 1,
        NormalPriority,
        LowPriority,
    }

    public enum QosTrafficClass
    {
        Unknown = 0,
        Conversational = 1,
        Streaming,
        Interactive,
        Background,
    }

    public enum QosDeliveryErroneousSDU
    {
        Unknown = 0,
        NoDetect = 1,
        Yes,
        No,
    }
    public enum QosDeliveryOrder
    {
        Unknown = 0,
        Yes,
        No,
    }
    public enum QosResidualBER
    {
        Unknown = 0,
        Yes,
        No,
    }
    public enum QosTrafficHandlingPriority
    {
        Unknown = 0,
        Level1,
        Level2,
        Level3,
    }

    public enum QosSignallingIndication
    {
        NonOptimized = 0,
        Optimized,
    }

    public enum Subsoption
    {
        Password = 0,
        SvcProvider = 1,
    }
}
