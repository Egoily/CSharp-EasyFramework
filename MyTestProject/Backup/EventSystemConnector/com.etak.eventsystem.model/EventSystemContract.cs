using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using com.etak.eventsystem.model.events;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model
{
    [ServiceContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    [ServiceKnownType("GetKnownTypes", typeof(ExtensionManager))]
    public interface EventSystemContract
    { 
        [OperationContract(IsOneWay = true)]
        void ProcessEvent(Event ev);
    }
}
