using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using XmlSerializer = System.Xml.Serialization.XmlSerializer;
using XmlSerializerNamespaces = System.Xml.Serialization.XmlSerializerNamespaces;

namespace com.etak.core.bizops.revenue.CallDREUpdateSubscriberPromotion
{

    /// <summary>
    /// UpdateSubscriberPromotionRes
    /// </summary>
    [Serializable]
    [System.Xml.Serialization.XmlRoot("UpdateSubscriberPromotionRes")]
    public sealed class UpdateSubscriberPromotionRes
    {
        private string _sdpId;
        private string _promotionId;
        private int _errorCode;

        /// <summary>
        /// SdpId
        /// </summary>
        [System.Xml.Serialization.XmlElement("sdp_id")]
        public string SdpId
        {
            get { return _sdpId; }
            set { _sdpId = value; }
        }


        /// <summary>
        /// PromotionId
        /// </summary>
        [System.Xml.Serialization.XmlElement("promotionId")]
        public string PromotionId
        {
            get { return _promotionId; }
            set { _promotionId = value; }
        }

        /// <summary>
        /// ErrorCode
        /// </summary>
        [System.Xml.Serialization.XmlElement("errorCode")]
        public int ErrorCode
        {
            get { return _errorCode; }
            set { _errorCode = value; }
        }
        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public UpdateSubscriberPromotionRes Deserialize(byte[] b)
        {
            XmlSerializer serializer = new XmlSerializer(GetType());

            using (MemoryStream stream = new MemoryStream(b))
            {
                using (XmlReader reader = XmlReader.Create(stream))
                {
                    return (UpdateSubscriberPromotionRes)serializer.Deserialize(reader);
                }
            }
        }
    }


}
