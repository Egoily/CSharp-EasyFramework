using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetSystemConfigDataInfosByItem
{
    /// <summary>
    /// Request to GetSystemConfigDataInfosByItem
    /// </summary>
    public class GetSystemConfigDataInfosByItemRequest : RequestBase
    {

        /// <summary>
        /// The Item to look for
        /// </summary>
        public string Item { get; set; }

       
    }
}
