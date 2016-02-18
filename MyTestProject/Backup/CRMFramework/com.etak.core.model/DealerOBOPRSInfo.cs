using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class DealerOBOPRSInfo
    {

        #region

        public bool IsDelete
        {
            get;
            set;
        }

        #endregion

        #region Attribute

        public int? OBOPRSID
        {
            get;
            set;
        }

        public int? DealerID
        {
            get;
            set;
        }

        public string Prefix
        {
            get;
            set;
        }

        public int? PrsTypeID
        {
            get;
            set;
        }

        public DateTime? CreateDate
        {
            get;
            set;
        }

        public int? UserID
        {
            get;
            set;
        }

        public DealerInfo DealerInfo
        {
            get;
            set;
        }

        #endregion
    }
}