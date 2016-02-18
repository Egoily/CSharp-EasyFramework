using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class DealersVoucherMvnoInfo
    {
        #region 构造函数
        public DealersVoucherMvnoInfo()
        { }

        public DealersVoucherMvnoInfo(int dealerId, string snCode, string contryCode)
        {
            this.DealerId = dealerId;
            this.SnCode = snCode;
            this.CountryCode = contryCode;
        }
        #endregion

        #region 成员
        private int? _dealerId;
        private string _snCode;
        private string _countryCode;
        #endregion


        #region 属性
        public int? DealerId
        {
            get { return _dealerId; }
            set { _dealerId = value; }
        }

        public string SnCode
        {
            get { return _snCode; }
            set { _snCode = value; }
        }

        public string CountryCode
        {
            get { return _countryCode; }
            set { _countryCode = value; }
        }

        #endregion

    }
}
