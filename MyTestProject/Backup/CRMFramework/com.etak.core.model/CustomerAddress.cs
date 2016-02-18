using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    /// Posible usages of an address of a customer
    /// </summary>
    [DataContract]
    [Serializable]
    public enum AddressUsages
    {
        /// <summary>
        /// Mandatory address that will be used to calculate the VAT
        /// </summary>
        [EnumMember]
        FiscalAddress = 0,

        [EnumMember]
        DeliveryAddress = 1,
        [EnumMember]
        PersonalAddress = 2,
    }

    /// <summary>
    /// For multi address
    /// <author>Oliver</author>
    /// <date>2014-09-24</date>
    /// </summary>
    [DataContract]
    [Serializable]
    public class CustomerAddress
    {
        /// <summary>
        /// THe customer that is using the address
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }
        /// <summary>
        /// The address being used by the customer
        /// </summary>
        public virtual AddressInfo Address { get; set; }
        /// <summary>
        /// The type of use that the customer is doing of the address
        /// </summary>
        public virtual AddressUsages UsageType { get; set; }
    }
}
