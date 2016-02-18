using com.etak.core.bizops.revenue.CallDREQuerySubscriberPromotion;

namespace com.etak.core.bizops.helper
{
    /// <summary>
    /// Interface for DREHttpebRequest
    /// </summary>
    public interface IHttpWebRequestHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="msec"></param>
        /// <param name="serializedRequest"></param>
        /// <returns></returns>
        byte[] GetResponse(string url, int? msec, byte[] serializedRequest);
    }
}