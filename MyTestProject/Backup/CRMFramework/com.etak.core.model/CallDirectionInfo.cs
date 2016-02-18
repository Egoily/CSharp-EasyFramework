using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class CallDirectionInfo
    {
        #region Member
        private int? _CallDirectionID;
        private string _CallDirectionName;

        #endregion

        #region Attribute

        public int? CallDirectionID
        {
            get { return _CallDirectionID; }
            set { _CallDirectionID = value; }
        }

        public string CallDirectionName
        {
            get { return _CallDirectionName; }
            set { _CallDirectionName = value; }
        }
        #endregion

    }
}
