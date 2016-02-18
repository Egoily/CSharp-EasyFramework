using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class DealerBankInfo
    {
        virtual public bool IsDelete { get; set; }
        virtual public int? BankID { get; set; }
        virtual public string BankName { get; set; }
        virtual public string BankNumber { get; set; }
        virtual public int? CurrencyID { get; set; }
        virtual public string Owner { get; set; }
        virtual public string City { get; set; }
        virtual public int? CountryID { get; set; }
        virtual public string IBAN { get; set; }
        virtual public string SWIFT { get; set; }
        virtual public string ABI { get; set; }
        virtual public string CAB { get; set; }
        virtual public DateTime? CreateDate { get; set; }
        virtual public int? UserID { get; set; }
        virtual public DealerInfo DealerInfo { get; set; }

    }
}
