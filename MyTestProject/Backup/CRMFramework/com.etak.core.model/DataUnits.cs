using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    /// Units to specify data volumes expressed, in computer teminology, KiloByte = 1024 Bytes
    /// </summary>
    [DataContract(Namespace = "http://com.etak.frontend")]
    [Serializable]
    public enum DataUnits
    {
        [EnumMember] Bit = 0,
        [EnumMember] Byte = 1,
        [EnumMember] KiloBit = 2,
        [EnumMember] KiloByte = 3,
        [EnumMember] MegaBit = 4,
        [EnumMember] MegaByte = 5,
        [EnumMember] GigaBit = 6,
        [EnumMember] GigaByte = 7,
        [EnumMember] TeraBit = 8,
        [EnumMember] TeraByte = 9,
    }
}