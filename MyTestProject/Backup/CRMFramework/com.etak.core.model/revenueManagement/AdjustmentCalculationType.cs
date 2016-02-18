using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    [DataContract]
    [Serializable]
    public enum AdjustmentCalculationType
    {
        [EnumMember]
        Percentage = 0,
        [EnumMember]
        AbsoluteValue = 1,
    }
}