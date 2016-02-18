using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    [DataContract]
    [Serializable]
    public enum AdjustmentType
    {
        [EnumMember]
        Discount,

        [EnumMember]
        Price
    }
}