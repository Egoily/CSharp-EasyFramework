using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{

    /// <summary>
    /// the voucher card information.
    /// </summary>
    [DataContract]
    [Serializable]
    public class VoucherCardInfo
    {
        #region 成员
        private string _SN;
        private string _VoucherCode;
        private string _VcEncrypt;
        private int? _DealerId;
        private int? _CMSDealerId;
        private decimal? _InitialCredit;
        private decimal? _CurrentCredit;
        private int? _CurrencyId;
        private DateTime? _CreateDate;
        private int? _Status;
        private DateTime? _StartDate;
        private DateTime? _EndDate;
        private DateTime? _ChangeStatusDate;
        private DateTime? _ActiveDateLineDate;
        private decimal? _VATRate;
        private int? taxCode;
        private decimal? _CreditWithVAT;
        private int? _VoucherType;
       
        #endregion

        #region 属性
        public string SN
        {
            get { return _SN; }
            set { _SN = value; }
        }

        public string VoucherCode
        {
            get { return _VoucherCode; }
            set { _VoucherCode = value; }
        }

        public string VcEncrypt
        {
            get { return _VcEncrypt; }
            set { _VcEncrypt = value; }
        }

        public int? DealerId
        {
            get { return _DealerId; }
            set { _DealerId = value; }
        }

        public int? CMSDealerId
        {
            get { return _CMSDealerId; }
            set { _CMSDealerId = value; }
        }

        public decimal? InitialCredit
        {
            get { return _InitialCredit; }
            set { _InitialCredit = value; }
        }

        public decimal? CurrentCredit
        {
            get { return _CurrentCredit; }
            set { _CurrentCredit = value; }
        }

        public int? CurrencyId
        {
            get { return _CurrencyId; }
            set { _CurrencyId = value; }
        }

        public DateTime? CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        public int? Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public DateTime? StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        public DateTime? EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        public DateTime? ActiveDeadLineDate
        {
            get { return _ActiveDateLineDate; }
            set { _ActiveDateLineDate = value; }
        }

        public DateTime? ChangeStatusDate
        {
            get { return _ChangeStatusDate; }
            set { _ChangeStatusDate = value; }
        }

        public decimal? VATRate
        {
            get { return _VATRate; }
            set { _VATRate = value; }
        }

        public decimal? CreditWithVAT
        {
            get { return _CreditWithVAT; }
            set { _CreditWithVAT = value; }
        }

        
        public virtual int? TaxCode
        {
            get { return taxCode; }
            set { taxCode = value; }
        }

        public virtual int? VoucherType
        {
            get { return _VoucherType; }
            set { _VoucherType = value; }
        }
        #endregion
        /// <summary>
        /// Referenced Product offering, if not null, that mean the product need to be provisioned to customer with this voucher card
        /// </summary>
        public virtual ProductOffering ProductOffering { get; set; }

    }
}
