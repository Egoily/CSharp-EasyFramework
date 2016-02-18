using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    /// <summary>
    /// Indicate the status of the subscriber.
    /// </summary>

    public enum SubscriberStatus
    {
        /// <summary>
        /// pass this value to indicate the status of the subscriber should not be changed in modification.
        /// </summary>
        [EnumMember]
        Keep = 0,
        /// <summary>
        /// indicate the status of the subscriber is active.
        /// Pass the value to modify the subscriber's status in hlr.
        /// </summary>
        [EnumMember]
        Active = 1,
        /// <summary>
        /// indicate the status of the subscriber is inactive.
        /// Pass the value to modify the subscriber's status in hlr
        /// </summary>
        [EnumMember]
        Inactive = 2,
    }

    [Serializable]
    public class ProvisioningSubscriber
    {
        /// <summary>
        /// The status of the subscriber.
        /// When querying the subscriber from HLR, this variable is for output.
        /// When modifying the subscriber from HLR, this variable is for input. And if you don't want to
        /// change the status of the subscriber, you should pass Keep, then HPS will not change the subscriber's status when modifying.
        /// Otherwise, pass active or inactive to activate/deactivate the subscriber's status.
        /// </summary>
        [DataMember]
        public SubscriberStatus SubscriberStatus { get; set; }
        /// <summary>
        /// International Mobile Subscriber Identification Number
        /// </summary>
        [DataMember]
        public String Imsi { get; set; }
        /// <summary>
        /// Mobile Station international ISDN number
        /// </summary>
        [DataMember]
        public String Msisdn { get; set; }
        /// <summary>
        /// The provisioning template of subscriber. 
        /// When provisioning a new subscriber to HLR, it is quite a generic behavior to copy 
        /// the subscriber provisioning settings from a pre-defined template.
        /// </summary>
        [DataMember]
        public String TemplateId { get; set; }
        /// <summary>
        /// The suplementary services of subscriber.
        /// </summary>
        [DataMember]
        public SuplementaryService[] Services { get; set; }
        /// <summary>
        /// Subscriber's real time data in SGSN
        /// </summary>
        [DataMember]
        public VolatileDataForSGSN CurrentVolatileDataForSGSN { get; set; }
        /// <summary>
        /// Subscriber's real time data in VLR
        /// </summary>
        [DataMember]
        public VolatileDataForVLR CurrentVolatileDataForVLR { get; set; } 

        public ProvisioningSubscriber()
        {
            Services = new SuplementaryService[] { };
        }

        /// <summary>
        /// Gets all the Call forwarding services from Service list.
        /// </summary>
        public IList<CallForwarding> CallForwardings 
        {
            get
            {
                    var services = Services.OfType<CallForwarding>();
                    return services.Any() ? services.ToList() : new List<CallForwarding>();
            }
        }

        /// <summary>
        /// Gets all the Subscriber call barring services from Service list. 
        /// It is a barring setting in subscriber level, subscriber can change this setting if they want.
        /// </summary>
        public IList<BarringSDB> SubscriberBarrings
        {
            get
            {
                var services = Services.OfType<BarringSDB>();
                return services.Any() ? services.ToList() : new List<BarringSDB>();
            }
        }

        /// <summary>
        /// Gets all the operator call barring services from Service list
        /// It is a barring setting in operator level, it is very common that operator barring subscriber service by business reason, for example barring subscriber's roaming
        /// </summary>
        public IList<BarringODB> OperatorBarrings
        {
            get
            {
                var services = Services.OfType<BarringODB>();
                return services.Any() ? services.ToList() : new List<BarringODB>();
            }
        }

        /// <summary>
        /// Gets all the Tele services from Service list
        /// </summary>
        public IList<TeleService> TeleServices
        {
            get
            {
                var services = Services.OfType<TeleService>();
                return services.Any() ? services.ToList() : new List<TeleService>();
            }
        }

        /// <summary>
        /// Gets all the Bearer services from Service list
        /// </summary>
        public IList<CircuitBearerService> BearerServices
        {
            get
            {
                var services = Services.OfType<CircuitBearerService>();
                return services.Any() ? services.ToList() : new List<CircuitBearerService>();
            }
        }

        /// <summary>
        /// Gets all the APN services from Service list
        /// </summary>
        public IList<APNServiceBase> APNServices
        {
            get
            {
                var services = Services.OfType<APNServiceBase>();
                return services.Any() ? services.ToList() : new List<APNServiceBase>();
            }
        }

        /// <summary>
        /// Gets all the AdviceOfCharge services from Service list
        /// </summary>
        public IList<AdviceOfCharge> AdviceOfCharges
        {
            get
            {
                var services = Services.OfType<AdviceOfCharge>();
                return services.Any() ? services.ToList() : new List<AdviceOfCharge>();
            }
        }

        /// <summary>
        /// Gets all the Camel service from Service list
        /// </summary>
        public CamelService CamelService { get { return Services.OfType<CamelService>().FirstOrDefault(); } }

        /// <summary>
        /// Gets all the ExplicitCallTransfer service from Service list
        /// </summary>
        public ExplicitCallTransfer ExplicitCallTransfer { get { return Services.OfType<ExplicitCallTransfer>().FirstOrDefault(); } }

        /// <summary>
        /// Gets all the MultiParty service from Service list
        /// </summary>
        public MultiParty MultiParty { get { return Services.OfType<MultiParty>().FirstOrDefault(); } }

        /// <summary>
        /// Gets all the NetworkAccessModel service from Service list
        /// </summary>
        public NetworkAccessModel NetworkAccessModel { get { return Services.OfType<NetworkAccessModel>().FirstOrDefault(); } }

        /// <summary>
        /// Gets all the FTNValidationRuleTemplate service from Service list
        /// </summary>
        public FTNValidationRuleTemplate FTNValidationRuleTemplate
        {
            get
            {
                var services = Services.OfType<FTNValidationRuleTemplate>();
                return services.Any() ? services.FirstOrDefault() : null;
            }
        }

        /// <summary>
        /// Gets all the BehaviourPerVLRTemplate service from Service list
        /// </summary>
        public BehaviourPerVLRTemplate CamelRestrictionTemplate 
        {
            get
            {
                var services = Services.OfType<BehaviourPerVLRTemplate>();
                return services.Any() ? services.FirstOrDefault() : null;
            }
        }

        /// <summary>
        /// Gets all the USSDTemplateService service from Service list
        /// </summary>
        public USSDTemplateService USSDTemplateService
        {
            get
            {
                var services = Services.OfType<USSDTemplateService>();
                return services.Any() ? services.FirstOrDefault() : null;
            }
        }

        /// <summary>
        /// Gets all the CallWaiting service from Service list
        /// </summary>
        public CallWaitingAndCallHolding CallWaiting
        {
            get
            {
                return Services.OfType<CallWaitingAndCallHolding>()
                .FirstOrDefault(s => s.Type == CallWaitingTypes.CallWaiting_CW);
            }
        }

        /// <summary>
        /// Gets all the CallHolding service from Service list
        /// </summary>
        public CallWaitingAndCallHolding CallHolding
        {
            get
            {
                return Services.OfType<CallWaitingAndCallHolding>()
                .FirstOrDefault(s => s.Type == CallWaitingTypes.CallHold_CH);
            }
        }

        /// <summary>
        /// Gets all the CallingLineIdentificationPresentation service from Service list
        /// </summary>
        public CallPresentationService CallingLineIdentificationPresentation
        {
            get
            {
                return Services.OfType<CallPresentationService>()
               .FirstOrDefault(s => s.Service == CallPresentationServices.CallingLineIdentificationPresentation_CLIP);
            }
        }
        /// <summary>
        /// Gets all the CallingLineIdentificationRestriction service from Service list
        /// </summary>
        public CallPresentationService CallingLineIdentificationRestriction
        {
            get
            {
                return Services.OfType<CallPresentationService>()
               .FirstOrDefault(s => s.Service == CallPresentationServices.CallingLineIdentificationRestriction_CLIR);
            }
        }
        /// <summary>
        /// Gets all the ConnectedLineIdentificationPresentation service from Service list
        /// </summary>
        public CallPresentationService ConnectedLineIdentificationPresentation
        {
            get
            {
                return Services.OfType<CallPresentationService>()
               .FirstOrDefault(s => s.Service == CallPresentationServices.ConnectedLineIdentificationPresentation_COLP);
            }
        }

        /// <summary>
        /// Gets all the ConnectedLineIdentificationRestriction service from Service list
        /// </summary>
        public CallPresentationService ConnectedLineIdentificationRestriction
        {
            get
            {
                return Services.OfType<CallPresentationService>()
               .FirstOrDefault(s => s.Service == CallPresentationServices.ConnectedLineIdentificationRestriction_COLR);
            }
        }

        /// <summary>
        /// Gets all the LTE4GService service from Service list
        /// </summary>
        public LTE4GService LTE4GService
        {
            get
            {
                var filterServices = Services.OfType<LTE4GService>();
                return filterServices.Any() ? filterServices.FirstOrDefault() : null;
            }
        }
    }
}
