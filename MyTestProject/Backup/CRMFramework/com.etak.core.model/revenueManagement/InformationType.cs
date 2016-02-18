using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    [DataContract]
    [Serializable]
    public enum TimesOfCharge
    {
        [EnumMember]
        InAdvance = 0,

        [EnumMember]
        Arrear = 1,
    }
}