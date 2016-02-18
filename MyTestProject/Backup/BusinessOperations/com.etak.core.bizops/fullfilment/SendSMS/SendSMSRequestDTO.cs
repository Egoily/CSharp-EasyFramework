using com.etak.core.model.operation.messages;
using System;

namespace com.etak.core.bizops.fullfilment.SendSMS
{
    /// <summary>
    /// Input paramer for SendSMS in DTO form.
    /// </summary>
    public class SendSMSRequestDTO : OrderRequestDTO
    {
        /// <summary>
        /// MobileNumbers, It can be several mobile numbers seperated with ";"
        /// </summary>
        public string MSISDNs { get; set; }

        /// <summary>
        /// Scheduled delivery time, null means to deliver immediately.
        /// </summary>
        public DateTime? ScheduledDeliveryTime { get; set; }

        /// <summary>
        /// Validity period
        /// </summary>
        public int? Validity { get; set; }

        /// <summary>
        /// Delivery report
        /// </summary>
        public bool DeliveryReport { get; set; }

        /// <summary>
        /// Priority
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Emergent flag
        /// </summary>
        public bool EmergentFlag { get; set; }

        /// <summary>
        /// Body of sms
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// CategoryId
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// SMS sender.
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// SearchByIsSent
        /// </summary>
        public bool SearchByIsSent { get; set; }

        /// <summary>
        /// IsSent
        /// </summary>
        public bool IsSent { get; set; }

        /// <summary>
        /// SearchByVirtualDeleted
        /// </summary>
        public bool SearchByVirtualDeleted { get; set; }

        /// <summary>
        /// VirtualDeleted
        /// </summary>
        public bool VirtualDeleted { get; set; }

        /// <summary>
        /// TrySentTimes
        /// </summary>
        public int? TrySentTimes { get; set; }

        /// <summary>
        /// MaxSentTimes
        /// </summary>
        public int? MaxSentTimes { get; set; }

        /// <summary>
        /// Name1
        /// </summary>
        public string Name1 { get; set; }

        /// <summary>
        /// Name2
        /// </summary>
        public string Name2 { get; set; }

        /// <summary>
        /// Name3
        /// </summary>
        public string Name3 { get; set; }

        /// <summary>
        /// Name4
        /// </summary>
        public string Name4 { get; set; }

        /// <summary>
        /// Name5
        /// </summary>
        public string Name5 { get; set; }

        /// <summary>
        /// Name6
        /// </summary>
        public string Name6 { get; set; }

        /// <summary>
        /// Value1
        /// </summary>
        public string Value1 { get; set; }

        /// <summary>
        /// Value2
        /// </summary>
        public string Value2 { get; set; }
        
        /// <summary>
        /// Value3
        /// </summary>
        public string Value3 { get; set; }

        /// <summary>
        /// Value4
        /// </summary>
        public string Value4 { get; set; }

        /// <summary>
        /// Value5
        /// </summary>
        public string Value5 { get; set; }

        /// <summary>
        /// Value6
        /// </summary>
        public string Value6 { get; set; }
        
        /// <summary>
        /// NotificationType
        /// </summary>
        public int NotificationType { get; set; }
    }
}
