using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2009-9-16 16:54:48
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2009-9-16 16:54:48
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2009-9-16 16:54:48
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2009-9-16 16:54:48
    /// </summary>
    [DataContract]
    [Serializable]
    public class RmFfFeeInfo : PromotionModelBase
    {
        #region 构造函数
        public RmFfFeeInfo()
        { }

        public RmFfFeeInfo(int DEALERID, int FEETYPEID, int CHARGETYPEID, int TRAFFICTYPEID, decimal PRICE)
        {
            this._DEALERID = DEALERID;
            this._FEETYPEID = FEETYPEID;
            this._CHARGETYPEID = CHARGETYPEID;
            this._TRAFFICTYPEID = TRAFFICTYPEID;
            this._PRICE = PRICE;
        }
        #endregion

        #region 成员
        private int _FeeId;
        private int _DEALERID;
        private int _FEETYPEID;
        private int _CHARGETYPEID;
        private int _TRAFFICTYPEID;
        private decimal _PRICE;
        #endregion

        public override bool Equals(object obj)
        {
            RmFfFeeInfo info = obj as RmFfFeeInfo;
            if (info != null)
            {
                RmFfFeeInfo ff = info;
                if (ff.DealerId == this.DealerId && ff.FeeTypeId == this.FeeTypeId && ff.ChargeTypeId == this.ChargeTypeId
                    && ff.TrafficTypeId == this.TrafficTypeId)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region 属性
        public int FeeId
        {
            get { return _FeeId; }
            set { _FeeId = value; }
        }

        public int DealerId
        {
            get { return _DEALERID; }
            set { _DEALERID = value; }
        }

        public int FeeTypeId
        {
            get { return _FEETYPEID; }
            set { _FEETYPEID = value; }
        }

        public int ChargeTypeId
        {
            get { return _CHARGETYPEID; }
            set { _CHARGETYPEID = value; }
        }

        public int TrafficTypeId
        {
            get { return _TRAFFICTYPEID; }
            set { _TRAFFICTYPEID = value; }
        }

        public decimal Price
        {
            get { return _PRICE; }
            set { _PRICE = value; }
        }

        #endregion

    }
}
