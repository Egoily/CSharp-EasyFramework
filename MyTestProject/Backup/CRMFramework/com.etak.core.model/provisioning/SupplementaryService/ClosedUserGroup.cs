using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    
    public enum CUGAccessTypes
    {
        /// <summary>
        /// A preferential CUG, which can be specified for each basic service group, is the nominated default 
        /// CUG to be used when no explicit CUG index is received by the network
        /// </summary>
        [EnumMember]
        Preferrential_CUG,

        /// <summary>
        /// An arrangement which allows a member of a CUG to receive calls from outside the CUG.
        /// </summary>
        [EnumMember]
        IncomingAccess_IA,


        /// <summary>
        /// An arrangement which allows a member of a CUG to place calls outside the CUG.
        /// </summary>
        [EnumMember]
        OutgoingAccess_OA,
        
        /// <summary>
        /// An access restriction that prevents a CUG member from receiving calls from other members of that group.
        /// </summary>
        [EnumMember]
        IncomingCallsBarredWithinA_CUG_ICB,

        /// <summary>
        /// An access restriction that prevents a CUG member from placing calls to other members of that group.
        /// </summary>
        [EnumMember]
        OutgoingCallsBarredWithinA_CUG_OCB,
    }

    [Serializable]
    public class ClosedUserGroup : SuplementaryService
    {
        [DataMember]
        public CUGAccessTypes Type { get; set; }
        [DataMember]
        public String GroupIp { get; set; }
        
    }
}
