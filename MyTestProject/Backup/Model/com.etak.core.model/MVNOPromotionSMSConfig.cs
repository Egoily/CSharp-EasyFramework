using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class MVNOPromotionSMSConfig
    {
        #region 
        private int? _ID = null;
        private int? _DealerID = null;
        private int? _ConfigID = null;
        private string _ConfigName;
        private int? _SMSType = null;
        private int? _Status = null;
        private int? _TemplateID = 0;
        private int? _LIMIT = null;
        private int? _LIMITUNIT = null;
        private int? _PROMOTIONPLANID = null;
        private int? _PROMOTIONPLANDETAILID = null;
        private string _PROMOTIONPLANNAME = string.Empty;
        private string _PROMOTIONPLANDETAILNAME = string.Empty;
       
        #endregion
        
        #region Attribute
        public int? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int? DealerID
        {
            get { return _DealerID; }
            set { _DealerID = value; }
        }

        public int? ConfigID
        {
            get { return _ConfigID; }
            set { _ConfigID = value; }
        }

        public string ConfigName
        {
            get { return _ConfigName; }
            set { _ConfigName = value; }
        }

        public int? SMSType
        {
            get { return _SMSType; }
            set { _SMSType = value; }
        }

        public int? Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public int? TemplateID
        {
            get { return _TemplateID; }
            set { _TemplateID = value; }
        }

        public int? LIMIT
        {
            get { return _LIMIT; }
            set { _LIMIT = value; }
        }

        public int? LIMITUNIT
        {
            get { return _LIMITUNIT; }
            set { _LIMITUNIT = value; }
        }
        public int? PROMOTIONPLANID
        {
            get { return _PROMOTIONPLANID; }
            set { _PROMOTIONPLANID = value; }
        }

        public string PROMOTIONPLANNAME
        {
            get { return _PROMOTIONPLANNAME; }
            set { _PROMOTIONPLANNAME = value; }
        }

        public int? PROMOTIONPLANDETAILID
        {
            get { return _PROMOTIONPLANDETAILID; }
            set { _PROMOTIONPLANDETAILID = value; }
        }

        public string PROMOTIONPLANDETAILNAME
        {
            get { return _PROMOTIONPLANDETAILNAME; }
            set { _PROMOTIONPLANDETAILNAME = value; }
        }
        #endregion
    }
}
