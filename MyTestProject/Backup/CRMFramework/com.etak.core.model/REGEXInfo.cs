using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class REGEXInfo
    {
        #region Member
        private int? _ID = null;
        private string _REGEXText;
        private string _Description;
        private string _DefaultMessage;       
        private long? _MessageID = null;
        private int? _TypeID = null;

        
        #endregion


        #region Attribute
        public virtual int? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual string REGEXText
        {
            get { return _REGEXText; }
            set { _REGEXText = value; }
        }

        public virtual string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public virtual string DefaultMessage
        {
            get { return _DefaultMessage; }
            set { _DefaultMessage = value; }
        }

        public virtual long? MessageID
        {
            get { return _MessageID; }
            set { _MessageID = value; }
        }

        public virtual int? TypeID
        {
            get { return _TypeID; }
            set { _TypeID = value; }
        }
        #endregion
    }
}
