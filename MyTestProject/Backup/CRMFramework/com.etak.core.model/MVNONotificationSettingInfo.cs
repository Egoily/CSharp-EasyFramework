using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    /// MVNONotificationSettingInfo is for those notificaition that are not for roaming, promotion to link to a SMS
    /// template.
    /// </summary>
	[DataContract]
    [Serializable]
    public class MVNONotificationSettingInfo
	{
        /// <summary>
        /// Identity
        /// </summary>
		public int ID { get; set; }
        /// <summary>
        /// MNO Id
        /// </summary>
		public int? MVNOID { get; set; }
        /// <summary>
        /// enum OtherTypesOfNotificationTypes
        /// </summary>
		public int? NotificationType { get; set; }
        /// <summary>
        /// Template Id to SMS Template
        /// </summary>
		public int? TemplateId { get; set; }
        /// <summary>
        /// StatusID, 0: disabled, 1:Enabled
        /// </summary>
		public int? StatusID { get; set; }
        /// <summary>
        /// extention value 1
        /// </summary>
		public String Value1 { get; set; }
        /// <summary>
        /// extention value 2
        /// </summary>
		public String Value2 { get; set; }
        /// <summary>
        /// extention value 3
        /// </summary>
		public String Value3 { get; set; }
        /// <summary>
        /// extention value 4
        /// </summary>
		public String Value4 { get; set; }
        /// <summary>
        /// extention value 5
        /// </summary>
		public String Value5 { get; set; }
        /// <summary>
        /// extention value 6
        /// </summary>
		public String Value6 { get; set; }
	}
}
