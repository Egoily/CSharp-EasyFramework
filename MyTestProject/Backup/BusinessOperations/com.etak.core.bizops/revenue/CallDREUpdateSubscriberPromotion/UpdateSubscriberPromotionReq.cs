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
    /// UpdateSubscriberPromotionReq
    /// </summary>
    [Serializable]
    [System.Xml.Serialization.XmlRoot("UpdateSubscriberPromotionReq")]
    public sealed class UpdateSubscriberPromotionReq
    {

        private string _sdpId;
        private string _msisdn;
        private long _promotionId;
        private float _incrementValue;
        private float _decrementValue;
        private long _startDate;
        private long _endDate;

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
        /// Msisdn
        /// </summary>
        [System.Xml.Serialization.XmlElement("msisdn")]
        public string Msisdn
        {
            get { return _msisdn; }
            set { _msisdn = value; }
        }

        /// <summary>
        /// PromotionId
        /// </summary>
        [System.Xml.Serialization.XmlElement("promotionId")]
        public long PromotionId
        {
            get { return _promotionId; }
            set { _promotionId = value; }
        }

        /// <summary>
        /// IncrementValueString
        /// </summary>
        [System.Xml.Serialization.XmlElement("incrementValue")]
        public string IncrementValueString
        {
            get
            {
                if (_incrementValue <= 0f)
                    return null;
                else
                    return Convert.ToString(_incrementValue);
            }
            set
            {
                if (value == null)
                {
                    _incrementValue = 0f;
                    return;
                }

                if (value.Trim().Length == 0)
                {
                    _incrementValue = 0f;
                    return;
                }

                try
                {
                    _incrementValue = Convert.ToSingle(value);
                    if (_incrementValue < 0f)
                        _incrementValue = 0f;
                }
                catch
                {
                    _incrementValue = 0f;
                }
            }
        }

        /// <summary>
        /// DecrementValueString
        /// </summary>
        [System.Xml.Serialization.XmlElement("decrementValue")]
        public string DecrementValueString
        {
            get
            {
                if (_decrementValue <= 0f)
                    return null;
                else
                    return Convert.ToString(_decrementValue);
            }
            set
            {
                if (value == null)
                {
                    _decrementValue = 0f;
                    return;
                }

                if (value.Trim().Length == 0)
                {
                    _decrementValue = 0f;
                    return;
                }

                try
                {
                    _decrementValue = Convert.ToSingle(value);
                    if (_decrementValue < 0f)
                        _decrementValue = 0f;
                }
                catch
                {
                    _decrementValue = 0f;
                }
            }
        }

        /// <summary>
        /// IncrementValue
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public float IncrementValue
        {
            get { return _incrementValue; }
            set { _incrementValue = value; }
        }

        /// <summary>
        /// DecrementValue
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public float DecrementValue
        {
            get { return _decrementValue; }
            set { _decrementValue = value; }
        }


        /// <summary>
        /// StartDate
        /// </summary>
        [System.Xml.Serialization.XmlElement("startDate")]
        public long StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        /// <summary>
        /// EndDate
        /// </summary>
        [System.Xml.Serialization.XmlElement("endDate")]
        public long EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }


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
