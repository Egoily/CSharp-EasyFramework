using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    [Serializable]
    [DataContract]
    public class AccountCurrency : Account
    {
        public virtual ISO4217CurrencyCodes Currency { get; set; }
    }
}