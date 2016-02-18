using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class StyleInfo
    {
        private int? _ID;        
        private int? _StyleID;        
        private string _StyleName;
        private int? _OrgID;        
        private byte[] _Image;        
        private string _ImageName;
        private int? _StyleType;
        private int? _Index;
        private DateTime? _UpdateDate;        
        

        #region Attribute           
        public virtual int? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual int? StyleID
        {
            get { return _StyleID; }
            set { _StyleID = value; }
        }

        public virtual string StyleName
        {
            get { return _StyleName; }
            set { _StyleName = value; }
        }

        public virtual int? OrgID
        {
            get { return _OrgID; }
            set { _OrgID = value; }
        }

        public virtual byte[] Image
        {
            get { return _Image; }
            set { _Image = value; }
        }

        public virtual string ImageName
        {
            get { return _ImageName; }
            set { _ImageName = value; }
        }

        public virtual int? StyleType
        {
            get { return _StyleType; }
            set { _StyleType = value; }
        }

        public virtual int? Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        public virtual DateTime? UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }
        #endregion
    }
}
