using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
	[DataContract]
    [Serializable]
    public class MVNONotificationSettingInfo
	{
		public int ID { get; set; }
		public int? MVNOID { get; set; }
		public int? NotificationType { get; set; }
		public int? TemplateId { get; set; }
		public int? StatusID { get; set; }
		public String Value1 { get; set; }
		public String Value2 { get; set; }
		public String Value3 { get; set; }
		public String Value4 { get; set; }
		public String Value5 { get; set; }
		public String Value6 { get; set; }
	}
}
