using System;
using System.Runtime.Serialization;
using com.etak.core.model.operation;

namespace com.etak.core.model.revenueManagement
{
    [DataContract]
    [Serializable]
    public class PaymentInfo
    {
        public virtual Int64 Id { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual Order Order { get; set; }

        public virtual Int32 Status { get; set; }

        public virtual Int32 PaymentMethod { get; set; }

        public virtual Decimal Amount { get; set; }

        public virtual Decimal Discount { get; set; }

        public virtual ISO4217CurrencyCodes Currency { get; set; }

        public virtual String ExternalPayment { get; set; }

        public virtual String PaymentInfoText { get; set; }
        public virtual String TaxInfo { get; set; }
    }
}
