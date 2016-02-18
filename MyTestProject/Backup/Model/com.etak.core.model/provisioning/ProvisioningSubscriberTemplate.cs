using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    /// <summary>
    /// Class represent a template to provision subscriber.
    /// </summary>
    [System.Serializable]

    public class ProvisioningSubscriberTemplate 
    {
        /// <summary>
        /// ID of the template
        /// </summary>
        [DataMember]
        public string TemplateId = "";

        /// <summary>
        /// Name of the template
        /// </summary>
        [DataMember]
        public string DisplayName = "";

        /// <summary>
        /// SuplementaryService of the template.
        /// </summary>
        [DataMember]
        public SuplementaryService[] Services = { };

        [DataMember]
        public SubscriberStatus SubscriberStatus { get; set; }

        //[DataMember]
        //public OperationCode LastAction { get; set; }
        //[DataMember]
        //public DateTime? LastActionTime { get; set; }


        public IList<CallForwarding> CallForwardings
        {
            get
            {
                var services = Services.OfType<CallForwarding>();
                return services.Any() ? services.ToList() : new List<CallForwarding>();
            }
        }

        public IList<BarringSDB> SubscriberBarrings
        {
            get
            {
                var services = Services.OfType<BarringSDB>();
                return services.Any() ? services.ToList() : new List<BarringSDB>();
            }
        }

        public IList<BarringODB> OperatorBarrings
        {
            get
            {
                var services = Services.OfType<BarringODB>();
                return services.Any() ? services.ToList() : new List<BarringODB>();
            }
        }

        public IList<TeleService> TeleServices
        {
            get
            {
                var services = Services.OfType<TeleService>();
                return services.Any() ? services.ToList() : new List<TeleService>();
            }
        }

        public IList<CircuitBearerService> BearerServices
        {
            get
            {
                var services = Services.OfType<CircuitBearerService>();
                return services.Any() ? services.ToList() : new List<CircuitBearerService>();
            }
        }

        public IList<APNServiceBase> APNServices
        {
            get
            {
                var services = Services.OfType<APNServiceBase>();
                return services.Any() ? services.ToList() : new List<APNServiceBase>();
            }
        }

        public IList<AdviceOfCharge> AdviceOfCharges
        {
            get
            {
                var services = Services.OfType<AdviceOfCharge>();
                return services.Any() ? services.ToList() : new List<AdviceOfCharge>();
            }
        }

        public CamelService CamelService { get { return Services.OfType<CamelService>().FirstOrDefault(); } }

        public ExplicitCallTransfer ExplicitCallTransfer { get { return Services.OfType<ExplicitCallTransfer>().FirstOrDefault(); } }

        public MultiParty MultiParty { get { return Services.OfType<MultiParty>().FirstOrDefault(); } }

        public NetworkAccessModel NetworkAccessModel { get { return Services.OfType<NetworkAccessModel>().FirstOrDefault(); } }

        public FTNValidationRuleTemplate FTNValidationRuleTemplate
        {
            get
            {
                var services = Services.OfType<FTNValidationRuleTemplate>();
                return services.Any() ? services.FirstOrDefault() : null;
            }
        }

        public BehaviourPerVLRTemplate CamelRestrictionTemplate
        {
            get
            {
                var services = Services.OfType<BehaviourPerVLRTemplate>();
                return services.Any() ? services.FirstOrDefault() : null;
            }
        }

        public USSDTemplateService USSDTemplateService
        {
            get
            {
                var services = Services.OfType<USSDTemplateService>();
                return services.Any() ? services.FirstOrDefault() : null;
            }
        }


        public CallWaitingAndCallHolding CallWaiting
        {
            get
            {
                return Services.OfType<CallWaitingAndCallHolding>()
                .FirstOrDefault(s => s.Type == CallWaitingTypes.CallWaiting_CW);
            }
        }

        public CallWaitingAndCallHolding CallHolding
        {
            get
            {
                return Services.OfType<CallWaitingAndCallHolding>()
                .FirstOrDefault(s => s.Type == CallWaitingTypes.CallHold_CH);
            }
        }

        public CallPresentationService CallingLineIdentificationPresentation
        {
            get
            {
                return Services.OfType<CallPresentationService>()
               .FirstOrDefault(s => s.Service == CallPresentationServices.CallingLineIdentificationPresentation_CLIP);
            }
        }

        public CallPresentationService CallingLineIdentificationRestriction
        {
            get
            {
                return Services.OfType<CallPresentationService>()
               .FirstOrDefault(s => s.Service == CallPresentationServices.CallingLineIdentificationRestriction_CLIR);
            }
        }

        public CallPresentationService ConnectedLineIdentificationPresentation
        {
            get
            {
                return Services.OfType<CallPresentationService>()
               .FirstOrDefault(s => s.Service == CallPresentationServices.ConnectedLineIdentificationPresentation_COLP);
            }
        }

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
