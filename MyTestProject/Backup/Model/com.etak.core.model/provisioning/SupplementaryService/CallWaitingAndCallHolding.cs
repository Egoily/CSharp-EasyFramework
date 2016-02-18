using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    
    public enum CallWaitingTypes
    {
        /// <summary>
        /// The Call Waiting TypeOfBarring permits a mobile subscriber to be notified of an incoming call (as per basic call procedures) 
        /// whilst the traffic channel is not available for the incoming call and the mobile subscriber 
        /// is engaged in an active or held call. Subsequently, the subscriber can either accept, reject, or ignore the incoming call.
        /// </summary>
        [EnumMember]
        CallWaiting_CW,

        /// <summary>
        /// The call hold service allows a served mobile subscriber, who is provisioned with this Supplementary TypeOfBarring,
        /// to interrupt communication on an existing active call and then subsequently,
        /// if desired, re-establish communication. The traffic channel remains assigned to the mobile subscriber
        /// after the communication is interrupted to allow the origination or possible termination of other calls. 
        /// </summary>
        [EnumMember]
        CallHold_CH,
    }

    /// <summary>
    /// This class is used to defined the behaviors of call waiting and holding.
    /// </summary>
    [Serializable]
    public class CallWaitingAndCallHolding : SuplementaryService
    {
        [DataMember]
        public CallWaitingTypes Type { get; set; }
       
    }
}
