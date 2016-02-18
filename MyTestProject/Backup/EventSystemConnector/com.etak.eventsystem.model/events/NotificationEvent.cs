using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class NotificationEvent : StrongTypedEvent
    {

        private string _resource;

        [DataMember()]
        public string Msisdn 
        {
            get
            {
                return _resource;
            }
            set
            {
                _resource = value;
                if (this.NumberInfo == null)
                {
                    this.NumberInfo = new NumberInfo() { IsLoaded = false };
                }
                this.NumberInfo.Resource = value;
            }
        }
        
        [DataMember()]
        public string Email { get; set; }
        
        [DataMember()]
        public int NotificationType { get; set; } // 1:SMS , 2:Email
        
        [DataMember()]
        public int LanguageId { get; set; }
        
        [DataMember]
        public Customer ToCustomer { get; set; }
        
        [DataMember]
        public NumberInfo NumberInfo { get; set; }
        //added by damon 2014-07-04 for select language
        
        [DataMember]
        public int USSDPushType { get; set; }
        
        [DataMember]
        public int? USSDFlowType { get; set; }      
    }
}
