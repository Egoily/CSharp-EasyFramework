using System;
using System.Runtime.Serialization;

namespace com.etak.eventsystem.model.dto
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
	public class CommissionRequestError : LoadeableEntity
	{
		[DataMember]
		public virtual Int64 ID { get; set; }
		[DataMember]
		public virtual Int64 CommissionID { get; set; }
		[DataMember]
		public virtual Int32 ReceiverType { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual DateTime CreateDate { get; set; }
		[DataMember]
		public virtual Int32 ErrorCode { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual String Request { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual String Response { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual Int32 Retry { get; set; }
	}
}
