using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class LifecycleLogInfo
    {
        #region 构造函数
        public LifecycleLogInfo()
        { }

        public LifecycleLogInfo(long logId, string transactionTime, string transactionSource, string transactionType, string msisdn, int dealerId, string packageName, string language, string createTime, string status, string rechargeTime, string expireTime, decimal balance, string status2, string rechargeTime2, string expireTime2, decimal balance2, string channel, string pin, int userId, bool used, DateTime usedTime, DateTime activeDeadLineDate, int orderCode, DateTime changeStatusDate, int paymentTypeId, DateTime lastConsumeEffectiveDate)
        {
            this.LogId = logId;
            this.TransactionTime = transactionTime;
            this.TransactionSource = transactionSource;
            this.TransactionType = transactionType;
            this.MSISDN = msisdn;
            this.DealerId = dealerId;
            this.PackageName = packageName;
            this.Language = language;
            this.CreateTime = createTime;
            this.Status = status;
            this.RechargeTime = rechargeTime;
            this.ExpireTime = expireTime;
            this.Balance = balance;
            this.Status2 = status2;
            this.RechargeTime2 = rechargeTime2;
            this.ExpireTime2 = expireTime2;
            this.Balance2 = balance2;
            this.Channel = channel;
            this.Pin = pin;
            this.UserId = userId;
            this.Used = used;
            this.UsedTime = usedTime;
            this.ActiveDeadLineDate = activeDeadLineDate;
            this.OrderCode = orderCode;
            this.ChangeStatusDate = changeStatusDate;
            this.PaymentTypeId = paymentTypeId;
            this.LastConsumeEffectiveDate = lastConsumeEffectiveDate;
        }
        #endregion

        #region 成员
        private long _LogId;
        private string _TransactionTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string _TransactionSource = "MNGR";
        private string _TransactionType;
        private string _Channel = null;
        private string _CreateTime = DateTime.Now.ToString("yyyyMMddHHmmss");

        /// <summary>
        /// Fiscalunit ID
        /// </summary>
        private int _DealerId;
        private int _UserId = -1;
        private bool _Used = false;
        private DateTime? _UsedTime = null;
        private long? _OrderCode = -1;
        private string _Language = "ESP";
        private int? _PaymentTypeId = (int)PaymentType.Prepayment;

        //Get these data before upgrade resourcemb info
        private string _Msisdn = null;
        private string _voucherPINCode;
        private string _PackageName;
        private string _Status = null;
        private string _RechargeTime;
        private string _ExpireTime;
        private decimal _Balance;
        private DateTime? _ActiveDeadLineDate;
        private DateTime? _ChangeStatusDate;
        private DateTime? _LastConsumeEffectiveDate;

        //Get these data after upgrade resourcemb info
        private string _Status2 = null;
        private string _RechargeTime2 = null;
        private string _ExpireTime2 = null;
        private decimal _Balance2 = 0;

        #endregion


        #region 属性



        public virtual long LogId
        {
            get { return _LogId; }
            set { _LogId = value; }
        }

        public virtual string TransactionTime
        {
            get { return _TransactionTime; }
            set { _TransactionTime = value; }
        }

        public virtual string TransactionSource
        {
            get { return _TransactionSource; }
            set { _TransactionSource = value; }
        }

        public virtual string TransactionType
        {
            get { return _TransactionType; }
            set { _TransactionType = value; }
        }

        public virtual string MSISDN
        {
            get { return _Msisdn; }
            set { _Msisdn = value; }
        }

        public virtual int DealerId
        {
            get { return _DealerId; }
            set { _DealerId = value; }
        }

        public virtual string PackageName
        {
            get { return _PackageName; }
            set { _PackageName = value; }
        }

        public virtual string Language
        {
            get { return _Language; }
            set { _Language = value; }
        }

        public virtual string CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }

        public virtual string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public virtual string RechargeTime
        {
            get { return _RechargeTime; }
            set { _RechargeTime = value; }
        }

        public virtual string ExpireTime
        {
            get { return _ExpireTime; }
            set { _ExpireTime = value; }
        }

        public virtual decimal Balance
        {
            get { return _Balance; }
            set { _Balance = value; }
        }

        public virtual string Status2
        {
            get { return _Status2; }
            set { _Status2 = value; }
        }

        public virtual string RechargeTime2
        {
            get { return _RechargeTime2; }
            set { _RechargeTime2 = value; }
        }

        public virtual string ExpireTime2
        {
            get { return _ExpireTime2; }
            set { _ExpireTime2 = value; }
        }

        public virtual decimal Balance2
        {
            get { return _Balance2; }
            set { _Balance2 = value; }
        }

        public virtual string Channel
        {
            get { return _Channel; }
            set { _Channel = value; }
        }

        public virtual int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public virtual string Pin
        {
            get { return _voucherPINCode; }
            set { _voucherPINCode = value; }
        }

        public virtual bool Used
        {
            get { return _Used; }
            set { _Used = value; }
        }

        public virtual DateTime? UsedTime
        {
            get { return _UsedTime; }
            set { _UsedTime = value; }
        }

        public virtual DateTime? ActiveDeadLineDate
        {
            get { return _ActiveDeadLineDate; }
            set { _ActiveDeadLineDate = value; }
        }

        public virtual long? OrderCode
        {
            get { return _OrderCode; }
            set { _OrderCode = value; }
        }

        public virtual DateTime? ChangeStatusDate
        {
            get { return _ChangeStatusDate; }
            set { _ChangeStatusDate = value; }
        }

        public virtual int? PaymentTypeId
        {
            get { return _PaymentTypeId; }
            set { _PaymentTypeId = value; }
        }

        public virtual DateTime? LastConsumeEffectiveDate
        {
            get { return _LastConsumeEffectiveDate; }
            set { _LastConsumeEffectiveDate = value; }
        }
        #endregion
    }
}
