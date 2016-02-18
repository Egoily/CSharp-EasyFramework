using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    
    public enum ClassForwardingTypes
    {
        /// <summary>
        /// This service permits a called mobile subscriber to have the network send all incoming calls, 
        /// or just those associated with a specific Basic service group, addressed to the called mobile subscriber's directory 
        /// number to another directory number. The ability of the served mobile subscriber to originate calls is unaffected. If this service is activated, calls are forwarded no matter what the condition of the termination.
        /// </summary>
        [EnumMember]
        CallForwardingUnconditional_CFU,


        /// <summary>
        /// This service permits a called mobile subscriber to have the network send all incoming calls, 
        /// or just those associated with a specific Basic service group, addressed to the called mobile subscriber's 
        /// directory number and which meet mobile subscriber busy to another directory number. 
        /// The ability of the served mobile subscriber to originate calls is unaffected. 
        /// If this service is activated, a call is forwarded only if the call meets mobile subscriber busy.
        /// </summary>
        [EnumMember]
        CallForwardingOnMobileSubscriberBusy_CFB,


        /// <summary>
        /// This service permits a called mobile subscriber to have the network send all incoming calls, 
        /// or just those associated with a specific Basic service group, addressed to the called mobile 
        /// subscriber's directory number and which meet no reply to another directory number. The ability
        /// of the served mobile subscriber to originate calls is unaffected. If this service is activated,
        /// a call is forwarded only if the call meets no reply.
        /// </summary>
        [EnumMember]
        CallForwardingOnNoReply_CFNRy,


        /// <summary>
        /// This service permits a called mobile subscriber to have the network send all incoming calls, 
        /// or just those associated with a specific Basic service group, addressed to the called mobile
        /// subscriber's directory number, but which is not reachable, to another directory number. 
        /// The ability of the served mobile subscriber to originate calls is principally unaffected, 
        /// but practically it is affected if the mobile subscriber is de-registered, 
        /// if there is radio congestion or if the mobile subscriber for example is being out of radio coverage. 
        /// If this service is activated, a call is forwarded only if the mobile subscriber is not reachable.
        /// </summary>
        [EnumMember]
        CallForwardingOnMobileSubscriberNotReachable_CFNRc,

        /// <summary>
        /// CFD is a HP HLR specical call forwarding feature. It is lower priority than CFU, CFB, CFNRy and CFNRc.
        /// So when CFU, CFB, CFNRy and CFNRc is not enabled, CFD is in place to forward the call to the destination defined by operator.
        /// </summary>
        [EnumMember]
        CallForwardingDefault_CFD,

        /// <summary>
        /// Not in use now.
        /// </summary>
        [EnumMember]
        CallForwardingAll,
    }

    /// <summary>
    /// This class is used to define the call forwarding service, it is allow to forward a (telephy, sms, data, fax) call from A to B in some specific cases. 
    /// </summary>
    [Serializable]
    public class CallForwarding : SuplementaryService
    {
        [DataMember]
        public ClassForwardingTypes ForwardingType { get; set; }
        [DataMember]
        public CallForwardingForService[] ForwardForServices { get; set; }
    }

    [Serializable]
    public class CallForwardingForService
    {
        /// <summary>
        /// Indicates what kind of tele service wants to be forwarded.
        /// </summary>
        [DataMember]
        public TeleserviceTypes? ServiceToForward { get; set; }
        /// <summary>
        /// Indicates what kind of bearer service wants to be forwarded.
        /// </summary>
        [DataMember]
        public CircuitBearerServices? DataServiceToForward { get; set; }
        /// <summary>
        /// The destination number to be forwarded.
        /// </summary>
        [DataMember]
        public String ForwardDestination { get; set; }
        /// <summary>
        /// The timeout seconds of call forwarding not reply (CFNRY), or call forwarding default (CFD)
        /// </summary>
        [DataMember]
        public Int32? TimeoutSeconds { get; set; }
        /// <summary>
        /// Indicates if to notify forwarded destination when the call forwarding happens.
        /// </summary>
        [DataMember]
        public Boolean NotifyToForwardedParty { get; set; }
        /// <summary>
        /// Indicates if to notify caller when the call forwarding happens.
        /// Note: it should be calling party, the name is incorrect.
        /// </summary>
        [DataMember]
        public Boolean NotifyToCallWaitingParty { get; set; }
        /// <summary>
        /// Indicates if to show the MSISDN number in destination's phone while a call is forwarding in.
        /// </summary>
        [DataMember]
        public Boolean PresentMsisdn { get; set; }
        /// <summary>
        /// indicates if the service is enabled or not.
        /// </summary>
        [DataMember]
        public Boolean Enabled { get; set; }
        /// <summary>
        /// The destination sub-address to be forwarded.
        /// </summary>
        [DataMember]
        public string Subaddress { get; set; }
    }
}
