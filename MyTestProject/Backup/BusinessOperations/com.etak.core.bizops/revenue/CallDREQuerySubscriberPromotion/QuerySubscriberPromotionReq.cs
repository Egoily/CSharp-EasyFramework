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
    /// QuerySubscriberPromotionReq
    /// </summary>
    [Serializable]
    [System.Xml.Serialization.XmlRoot("QuerySubscriberPromotionReq")]
    public sealed class QuerySubscriberPromotionReq
    {
        /// <summary>
        /// scp_id
        /// </summary>
        public string scp_id { get; set; }

        /// <summary>
        /// msisdn
        /// </summary>
        public string msisdn { get; set; }

        /// <summary>
        /// promotionId
        /// </summary>
        public string promotionId { get; set; }


        /// <summary>
        /// Serialize
        /// </summary>
        /// <returns></returns>
        public byte[] Serialize()
        {
            XmlSerializer serializer = new XmlSerializer(GetType());

            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializerNamespaces customNamespace = new XmlSerializerNamespaces();
                customNamespace.Add("", "");

                XmlWriterSettings setting = new XmlWriterSettings();
                setting.Encoding = new UTF8Encoding(false);
                setting.Indent = false;

                using (XmlWriter writer = XmlWriter.Create(stream, setting))
                {
                    serializer.Serialize(writer, this, customNamespace);
                }

                return stream.ToArray();
            }
        }
    }
}
