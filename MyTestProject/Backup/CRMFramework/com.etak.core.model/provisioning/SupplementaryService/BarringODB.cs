using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    
    public enum BarringODBTypes
    {
        #region Outgoing Voice barrings
        [EnumMember]
        BarringOutgoingCalls_BOC,
        [EnumMember]
        BarringOutgoingInternationalCalls_BOIC,
        [EnumMember]
        BarringOutgoingInternationalCallsExceptDirectedHomePLMNcountry_BOIExHOME,
        [EnumMember]
        BarringOutgoingCallsRoamingOutsidetHomePLMNcountry,
        [EnumMember]
        BarringOutgoingInterZonalcalls_BAIOG,
        [EnumMember]
        BarringOutgoingInterZonalcallsexceptthosedirectedtothehomePLMNcountry,
        [EnumMember]
        BarringOutgoingInterzonalAndInternationalCallsOutsideHPLMN,
        #endregion

        #region Incoming voice calls
        [EnumMember]
        BarringIncomingCalls_BAIC,
        [EnumMember]
        BarringIncomingCallsRoamingOutsideHomePLMNCountry_BIC_ROAM,
        [EnumMember]
        BarringIncomingCallsRoamingOutsideZoneHomePLMNCountry,
        #endregion

        #region Voice Roaming
        [EnumMember]
        BarringRoamingOutsideHomePLMN,
        [EnumMember]
        BarringRoamingOutsideHomePLMNCountry,
        #endregion

        #region Premium voice
        [EnumMember]
        BarringOutgoingPremiumRateCalls_Information,
        [EnumMember]
        BarringOutgoingPremiumRateCalls_Entertainment,
        [EnumMember]
        BarringOutgoingPremiumRateCalls_Information_And_Entertainment,
        [EnumMember]
        BarringOutgoingPremiumRateCalls_Information_RoamingOutsidePLMNCountry,
        [EnumMember]
        BarringOutgoingPremiumRateCalls_Entertainment_RoamingOutsidePLMNCountry,
        #endregion 

        #region Specific Barrings is HPLMN network 
        [EnumMember]
        WhenRegisteredHPLMNOperatorSpecificBarring_Type1,
        [EnumMember]
        WhenRegisteredHPLMNOperatorSpecificBarring_Type2,
        [EnumMember]
        WhenRegisteredHPLMNOperatorSpecificBarring_Type3,
        [EnumMember]
        WhenRegisteredHPLMNOperatorSpecificBarring_Type4,
        #endregion 

        #region Suplementary Services Barrings
        [EnumMember]
        BarringofSupplementaryServicesManagement_BASS,
        #endregion

        #region Barring of registration call forward
        [EnumMember]
        BarringRegistrationofCallForwardedToNumber,
        [EnumMember]
        BarringRegistrationofInternationalCallForwardedToNumber,
        [EnumMember]
        BarringRegistrationofInternationalCallForwardedToNumberExceptToANumberWithinHPLMNCountry,
        [EnumMember]
        BarringRegistrationofInterZoneCallForwardedToNumber,
        [EnumMember]
        BarringRegistrationofInterZoneCallForwardedToNumberExceptNumberInHPLMNCountry,
        #endregion 

        #region Barring of call transfer
        [EnumMember]
        BarringCallTransfer_ALL_ECT,
        [EnumMember]
        BarringCallTransferAnyChargedToServed_CHARGEABLE_ECT,
        [EnumMember]
        BarringCallTransferAnyInternatChargedToServed_INTL_ECT,
        [EnumMember]
        BarringCallTransferAnyInterzonalChargedToServed_INTERZONAL_ECT,
        [EnumMember]
        BarringCallTransferBothChargedToServed_DOUBLY_CHARGEABLE_ECT,
        [EnumMember]
        BarringCallTransferExistingTransferForServed_MULTIPLE_ECT,
        #endregion

        #region Barring of Data
        [EnumMember]
        BarringDataAll,
        [EnumMember]
        BarringDataRoamingToHPLMNAP,
        [EnumMember]
        BarringDataRoamingToVPLMNAP,
        #endregion
    }

    /// <summary>
    /// This class is used to defined the ODB (operator determined barring) services.
    /// </summary>
    [Serializable]
    public class BarringODB : SuplementaryService
    {
        [DataMember]
        public BarringODBTypes Service { get; set; }

        /// <summary>
        /// Set of tele services to which the barring is aplied.
        /// </summary>
        [DataMember]
        public TeleserviceTypes[] Type { get; set; }
    }
}
