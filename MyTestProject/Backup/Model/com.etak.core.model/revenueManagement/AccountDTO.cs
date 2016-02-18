using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    [DataContract]
    public enum AccountStatus
    {
        /// <summary>
        /// Pending description
        /// </summary>
        [EnumMember] Active,

        /// <summary>
        /// Pending description
        /// </summary>
        [EnumMember] Deactive
    }

    [DataContract]
    public abstract class AccountDTO
    {
        /// <summary>
        /// Unique Id of the account
        /// </summary>
        [DataMember] public String Id;

        /// <summary>
        /// Id of the customer that will handle the payment of this account
        /// </summary>
        [DataMember] public String CustomerId;

        /// <summary>
        /// The status of the account
        /// </summary>
        [DataMember] public AccountStatus Status;

        /// <summary>
        /// The name of the account if present to be shown in GUIs
        /// </summary>
        [DataMember] public IList<TextualDescription> Names;

        /// <summary>
        /// The textual description of the account to be presented to customers
        /// </summary>
        [DataMember] public IList<TextualDescription> Descriptions;

        /// <summary>
        /// The date in which the last bill was issued
        /// </summary>
        [DataMember] public DateTime? LastBilledDate;
    }


    [DataContract]
    public class MoneyAccountDTO : AccountDTO
    {
        /// <summary>
        /// The amount of balance in this account
        /// </summary>
        [DataMember] public Decimal Balance;

        /// <summary>
        /// The currency in which the balance is expressed
        /// </summary>
        [DataMember] public ISO4217CurrencyCodes Curreny;
    }

    [DataContract]
    public class TimeAccountDTO : AccountDTO
    {
        /// <summary>
        /// The amount of balance in this account
        /// </summary>
        [DataMember]
        public Decimal Time;

        /// <summary>
        /// The unit of time in which time is expressed
        /// </summary>
        [DataMember]
        public TimeUnits TimeUnit;
    }


    [DataContract]
    public class DataAccountDTO : AccountDTO
    {
        /// <summary>
        /// The amount of balance in this account
        /// </summary>
        [DataMember]
        public Decimal DataVolume;

        /// <summary>
        /// The unit of time in which time is expressed
        /// </summary>
        [DataMember]
        public DataUnits DataUnit;
    }


}