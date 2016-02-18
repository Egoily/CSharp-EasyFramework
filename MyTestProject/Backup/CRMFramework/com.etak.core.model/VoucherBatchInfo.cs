using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{

    /// <summary>
    /// the voucher batch information.
    /// </summary>
    [DataContract]
    [Serializable]
    public class VoucherBatchInfo
    {
        #region 成员
        private int? dealerId;
        private string batchId;
        private string snStart;
        private string snEnd;
        private decimal? credit;
        private DateTime? createDate;
        private string vKey;
        private string supplier;
        #endregion


        #region 属性
        /// <summary>
        /// Gets or sets the dealer id.
        /// </summary>
        /// <value>The dealer id.</value>
        public int? DealerId
        {
            get { return dealerId; }
            set { dealerId = value; }
        }

        public string BatchId
        {
            get { return batchId; }
            set { batchId = value; }
        }

        public string SnStart
        {
            get { return snStart; }
            set { snStart = value; }
        }

        public string SnEnd
        {
            get { return snEnd; }
            set { snEnd = value; }
        }

        public decimal? Credit
        {
            get { return credit; }
            set { credit = value; }
        }

        public DateTime? CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        public string VKey
        {
            get { return vKey; }
            set { vKey = value; }
        }

        public string Supplier
        {
            get { return supplier; }
            set { supplier = value; }
        }

        #endregion

        // By Francisco 2014.02.03
        public int? PONumber
        {
            get;
            set;
        }
        public int? PackageType
        {
            get;
            set;
        }
        public string PackageTypeInfo
        {
            get;
            set;
        }
        public int? OrgIDCurrentMVO
        {
            get;
            set;
        }
        public int? OrganizationIDCurrentMVNO
        {
            get;
            set;
        }
        public string PGPFileName
        {
            get;
            set;
        }


    }
}
