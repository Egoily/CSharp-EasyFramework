
using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    [DataContract]
    [Serializable]
    public class AccountData : Account
    { 
        public virtual DataUnits BalanceUnit { get; set; }
    }
}