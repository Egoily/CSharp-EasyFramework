using System;

namespace com.etak.core.queue.Messages
{
    public enum DeliverResult
    {
        OK, 
        Error,
    }

    public class QueueDeliverMessageResponse
    {
        public DeliverResult Result { get; set; }
        public Int32 ResultCode { get; set; }
        public Int32 ElementsProccessed { get; set; }
        public String ResultMessage { get; set; }

        public override string ToString()
        {
            return "Result: " + Result + " ResultCode: " + ResultCode + " ResultMessage:" + ResultMessage + " ElementsProccessed: " + ElementsProccessed;
        }
    }
}
