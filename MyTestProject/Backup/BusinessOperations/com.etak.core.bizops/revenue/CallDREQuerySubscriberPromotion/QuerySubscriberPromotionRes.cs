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

namespace com.etak.core.bizops.revenue.CallDREQuerySubscriberPromotion
{

    /// <summary>
    /// QuerySubscriberPromotionRes
    /// </summary>
    [Serializable]
    [System.Xml.Serialization.XmlRoot("QuerySubscriberPromotionRes")]
    public sealed class QuerySubscriberPromotionRes
    {
        /// <summary>
        /// sdp_id
        /// </summary>
        public string sdp_id { get; set; }

        /// <summary>
        /// promotionId
        /// </summary>
        public string promotionId { get; set; }

        /// <summary>
        /// currentLimit
        /// </summary>
        public decimal currentLimit { get; set; }

        /// <summary>
        /// currentLimitWithVat
        /// </summary>
        public decimal currentLimitWithVat { get; set; }

        /// <summary>
        /// vatRate
        /// </summary>
        public decimal vatRate { get; set; }

        /// <summary>
        /// presentationType
        /// </summary>
        public string presentationType { get; set; }

        /// <summary>
        /// currency
        /// </summary>
        public string currency { get; set; }


        /// <summary>
        /// errorCode
        /// </summary>
        public int errorCode { get; set; }

        /// <summary>
        /// currentLimit
        /// </summary>
        public decimal frozenLimit { get; set; }

        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public QuerySubscriberPromotionRes Deserialize(byte[] b)
        {
            XmlSerializer serializer = new XmlSerializer(GetType());

            using (MemoryStream stream = new MemoryStream(b))
            {
                using (XmlReader reader = XmlReader.Create(stream))
                {
                    return (QuerySubscriberPromotionRes)serializer.Deserialize(reader);
                }
            }
        }
    }


}
