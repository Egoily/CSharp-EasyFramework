using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    [DataContract]
    [Serializable]
    public class AccountTime : Account
    {
        public virtual TimeUnits BalanceUnit { get; set; }
    }
}