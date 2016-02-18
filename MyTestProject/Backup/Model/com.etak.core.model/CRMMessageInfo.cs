using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class CRMMessageInfo
    {
        private long? _ID = null;
        private long? _MessageID = null;
        private long? _SourceID = null;
        private int? _LanguageID = null;
        private int? _TypeCode = null;        
        private string _UserMessageCaption;        
        private string _UserMessageText;        
        private string _SystemMessageCaption;        
        private string _SystemMessageText;        
        private DateTime? _UpdateDate = null;
        private int? _State = null;
        

        #region Attribute
        public virtual long? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual long? MessageID
        {
            get { return _MessageID; }
            set { _MessageID = value; }
        }

        public virtual long? SourceID
        {
            get { return _SourceID; }
            set { _SourceID = value; }
        }

        public virtual int? LanguageID
        {
            get { return _LanguageID; }
            set { _LanguageID = value; }
        }

        public virtual int? TypeCode
        {
            get { return _TypeCode; }
            set { _TypeCode = value; }
        }

        public virtual string UserMessageCaption
        {
            get { return _UserMessageCaption; }
            set { _UserMessageCaption = value; }
        }

        public virtual string UserMessageText
        {
            get { return _UserMessageText; }
            set { _UserMessageText = value; }
        }

        public virtual string SystemMessageCaption
        {
            get { return _SystemMessageCaption; }
            set { _SystemMessageCaption = value; }
        }

        public virtual string SystemMessageText
        {
            get { return _SystemMessageText; }
            set { _SystemMessageText = value; }
        }

        public virtual DateTime? UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }

        public virtual int? State
        {
            get { return _State; }
            set { _State = value; }
        }
        #endregion
    }
}
