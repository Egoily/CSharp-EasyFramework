using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class SettingExtendDetailInfo
    {

        #region 成员
        
        private int _Dealerid;
        private int _SettingId;
        private int _CategoryId;
        private int _StatusId;
        private int _ItemId;
        private string _ItemName;
        private string _ItemValue;
        private string _ItemDescription;
        private decimal? itemStart;
        private decimal? itemEnd;

        #endregion


        #region 属性

        public virtual int DealerId
        {
            get { return _Dealerid; }
            set { _Dealerid = value; }
        }

        public virtual int SettingId
        {
            get { return _SettingId; }
            set { _SettingId = value; }
        }

        public virtual int CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }

        public virtual int StatusId
        {
            get { return _StatusId; }
            set { _StatusId = value; }
        }

        public virtual int ItemId
        {
            get { return _ItemId; }
            set { _ItemId = value; }
        }

        public virtual string ItemName
        {
            get { return _ItemName; }
            set { _ItemName = value; }
        }

        public virtual string ItemValue
        {
            get { return _ItemValue; }
            set { _ItemValue = value; }
        }

        public virtual string ItemDescription
        {
            get { return _ItemDescription; }
            set { _ItemDescription = value; }
        }

        public virtual decimal? ItemStart
        {
            get { return itemStart; }
            set { itemStart = value; }
        }

        public virtual decimal? ItemEnd
        {
            get { return itemEnd; }
            set { itemEnd = value; }
        }

        #endregion

    }
}
