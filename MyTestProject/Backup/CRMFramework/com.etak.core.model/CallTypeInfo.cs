using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class CallTypeInfo
    {
        #region 
        private int? _CallTypeID = null;
        private string _CallTypeName;
        #endregion

        #region Attribute

        public int? CallTypeID
        {
            get { return _CallTypeID; }
            set { _CallTypeID = value; }
        }

        public string CallTypeName
        {
            get { return _CallTypeName; }
            set { _CallTypeName = value; }
        }
        #endregion
    }
}
