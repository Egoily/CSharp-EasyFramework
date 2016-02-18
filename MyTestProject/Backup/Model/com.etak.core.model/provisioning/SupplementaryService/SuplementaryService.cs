﻿using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace com.etak.core.model.provisioning
{
    [KnownType(typeof(APNService))]
    [KnownType(typeof(APNServiceBase))]
    [KnownType(typeof(AdviceOfCharge))]
    [KnownType(typeof(BarringODB))]
    [KnownType(typeof(BarringSDB))]
    [KnownType(typeof(BehaviourPerVLRTemplate))]
    [KnownType(typeof(CallDeflection))]
    [KnownType(typeof(CallForwarding))]
    [KnownType(typeof(CallPresentationService))]
    [KnownType(typeof(CallWaitingAndCallHolding))]
    [KnownType(typeof(CallingNamePresentation))]
    [KnownType(typeof(CamelService))]
    [KnownType(typeof(CircuitBearerService))]
    [KnownType(typeof(ClosedUserGroup))]
    [KnownType(typeof(CompletionOfCallsBusySubscriber))]
    [KnownType(typeof(ExplicitCallTransfer))]
    [KnownType(typeof(FTNValidationRuleTemplate))]
    [KnownType(typeof(MultiParty))]
    [KnownType(typeof(NameIdentification))]
    [KnownType(typeof(NetworkAccessModel))]
    [KnownType(typeof(TeleService))]
    [KnownType(typeof(TemplatedAPNService))]
    [KnownType(typeof(USSDService))]
    [KnownType(typeof(USSDTemplateService))]
    [KnownType(typeof(UserToUserSignalling))]
    [KnownType(typeof(RoamingWelcomeSMSService))]
    [KnownType(typeof(UserEquipmentAPNService))]
    [KnownType(typeof(LTE4GService))]
    [KnownType(typeof(RadioAccessNetwork))]

    [XmlInclude(typeof(APNService))]
    [XmlInclude(typeof(APNServiceBase))]
    [XmlInclude(typeof(AdviceOfCharge))]
    [XmlInclude(typeof(BarringODB))]
    [XmlInclude(typeof(BarringSDB))]
    [XmlInclude(typeof(BehaviourPerVLRTemplate))]
    [XmlInclude(typeof(CallDeflection))]
    [XmlInclude(typeof(CallForwarding))]
    [XmlInclude(typeof(CallPresentationService))]
    [XmlInclude(typeof(CallWaitingAndCallHolding))]
    [XmlInclude(typeof(CallingNamePresentation))]
    [XmlInclude(typeof(CamelService))]
    [XmlInclude(typeof(CircuitBearerService))]
    [XmlInclude(typeof(ClosedUserGroup))]
    [XmlInclude(typeof(CompletionOfCallsBusySubscriber))]
    [XmlInclude(typeof(ExplicitCallTransfer))]
    [XmlInclude(typeof(FTNValidationRuleTemplate))]
    [XmlInclude(typeof(MultiParty))]
    [XmlInclude(typeof(NameIdentification))]
    [XmlInclude(typeof(NetworkAccessModel))]
    [XmlInclude(typeof(TeleService))]
    [XmlInclude(typeof(TemplatedAPNService))]
    [XmlInclude(typeof(USSDService))]
    [XmlInclude(typeof(USSDTemplateService))]
    [XmlInclude(typeof(UserToUserSignalling))]
    [XmlInclude(typeof(RoamingWelcomeSMSService))]
    [XmlInclude(typeof(UserEquipmentAPNService))]
    [XmlInclude(typeof(LTE4GService))]
    [XmlInclude(typeof(RadioAccessNetwork))]

    [Serializable]
    public class SuplementaryService
    {
        [DataMember]
        public Boolean Enabled;
        [DataMember]
        public KeyValue[] SupplymentaryData =  { };
    }
}
