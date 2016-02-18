using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    
    public enum UserToSingnalingServices
    {
        /// <summary>
        /// UUI can be sent and received during the origination and termination of a call, 
        /// with UUI embedded within call control messages. The service 1 can be activated 
        /// implicit by inserting UUI when set-up a call or explicit with an appropriate procedure.
        /// </summary>
        [EnumMember]
        Service1,

        /// <summary>
        /// UUI can be sent and received after the served subscriber has received an indication 
        /// that the remote party is being informed of the call and prior to the establishment of the connection. 
        /// UUI sent by the served subscriber prior to receiving the acceptance of the call by the remote party, 
        /// may as a network option be delivered to the remote party after the call has been established. 
        /// The service 2 shall be activated explicitly.
        /// </summary>
        [EnumMember]
        Service2,

        /// <summary>
        /// User-to-user-information can be sent and received only while the connection is established. 
        /// The service 3 shall be activated explicitly
        /// </summary>
        [EnumMember]
        Service3,
    }

    [Serializable]
    public class UserToUserSignalling : SuplementaryService
    {
        [DataMember]
        public UserToSingnalingServices Serice {get;set;}
    }
}
