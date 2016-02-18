using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    
    public enum BarringSDBTypes
    {
        [EnumMember]
        BarringofAllOutgoingCalls_BAOC,
        [EnumMember]
        BarringofOutgoingInternationalCalls_BOIC,
        [EnumMember]
        BarringofOutgoingInternationalCallsExceptHomeCountry_BOIC_exHC,
        [EnumMember]
        BarringofAllIncomingCalls_BAIC,
        [EnumMember]
        BarringofIncomingCallsRoamingOutsideHomeCountry_BIC_Roam,
        [EnumMember]
        AnonymousCallRejection_ACR,
    }

    [Serializable]
    public class BarringSDBForService
    {
        [DataMember]
        public TeleserviceTypes? ServiceToSetSDB { get; set; }
        [DataMember]
        public bool Enabled { get; set; }
        [DataMember]
        public CircuitBearerServices? DataServiceToSetSDB { get; set; }
    }

    /// <summary>
    /// This class is used to defined the SDB (subscriber determined barring) services.
    /// </summary>
    [Serializable]
    public class BarringSDB : SuplementaryService 
    {
        [DataMember]
        public BarringSDBTypes TypeOfBarring { get; set; }

        /// <summary>
        /// Set of tele services to which the barring is aplied.
        /// </summary>
        [DataMember]
        public BarringSDBForService[] ServicesToBarr { get; set; }
    }
}
