using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using com.etak.simpleeventsystem.model.events;

namespace com.etak.simpleeventsystem.model
{
    [ServiceContract(Namespace = "http://com.etak.simpleeventsystem")]
    public interface SimpleEventSystemContract
    {
        [OperationContract(IsOneWay = true)]
        void ProcessEvent(SimpleEvent ev);
    }

    [DataContract]
    public enum SimpleEventProcessResultTypes
    {
        Ok,
        Error,
    }

    [DataContract]
    public class SimpleEventProcessResult
    {
        public SimpleEventProcessResultTypes Result { get; set; }
        public String[] Messages { get; set; }
    }
}
