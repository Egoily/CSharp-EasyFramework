using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    /// the possible units of time the proration is based upon 
    /// </summary>
    [DataContract(Namespace = "http://com.etak.frontend")]
    [Serializable]
    public enum TimeUnits 
    {
        [EnumMember] Second = 5,
        [EnumMember] Minute = 6,
        [EnumMember] Hour = 7,
        [EnumMember] Day = 1,
        [EnumMember] Week = 2,
        [EnumMember] Month = 3,
        [EnumMember] Year = 4
    }
}