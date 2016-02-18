using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    /// Possible types of units of balances
    /// </summary>
    [DataContract]
    [Serializable]
    public enum UnitCategories : byte
    {
        /// <summary>
        /// If the quantity has no dimension
        /// </summary>
        [EnumMember]
        NoDimension,

        /// <summary>
        /// This value is used to indicate a data volume, always should be used in combination with a currency code<see cref="DataUnits"/>
        /// </summary>
        [EnumMember] Data,

        /// <summary>
        /// This value is used to indicate a data volume, always should be used in combination with a currency code<see cref="TimeUnits"/>
        /// </summary>
        [EnumMember] Time,

        /// <summary>
        /// The type of unit is a monetary value, always should be used in combination with a currency code<see cref="ISO4217CurrencyCodes"/>
        /// </summary>
        [EnumMember]
        Currency,
    }
}