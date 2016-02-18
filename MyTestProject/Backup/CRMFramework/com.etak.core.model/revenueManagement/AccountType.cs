
using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    [DataContract]
    [Serializable]
    public enum AccountType
    {
        Data,
        Currency,
        Time
    }
}