﻿using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events
{
	[DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class InvokeCommissionExternalInterfaceEvent : StrongTypedEvent
	{
		[DataMember()]
		public virtual CommissionHistory CommissionHistory { get; set; }
	}
}
