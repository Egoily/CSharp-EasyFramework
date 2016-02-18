using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class MultiLingualInfo
    {
        private int? _ID = null;        
        private int? _DealerID = null;        
        private int? _LanguageID = null;        
        private string _FormFullName;        
        private string _ControlID;        
        private string _Value;        
        private string _Text;        
        private string _ToolTipText;        
        private int? _ShowOrder = null;        
        private string _Link;        
        private string _Description;        
        private DateTime? _UpdateDate = null;        
        private int? _State = null;
        private int? _DictionaryTypeID = null;
        private int? _REGEXTypeID = null;
        private string _DefaultText;
        private string _ControlType;
        private int? _ShowType = null;

        
        #region Attribute
        public virtual int? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual int? DealerID
        {
            get { return _DealerID; }
            set { _DealerID = value; }
        }

        public virtual int? LanguageID
        {
            get { return _LanguageID; }
            set { _LanguageID = value; }
        }

        public virtual string FormFullName
        {
            get { return _FormFullName; }
            set { _FormFullName = value; }
        }

        public virtual string ControlID
        {
            get { return _ControlID; }
            set { _ControlID = value; }
        }

        public virtual string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public virtual string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        public virtual string ToolTipText
        {
            get { return _ToolTipText; }
            set { _ToolTipText = value; }
        }

        public virtual int? ShowOrder
        {
            get { return _ShowOrder; }
            set { _ShowOrder = value; }
        }

        public virtual string Link
        {
            get { return _Link; }
            set { _Link = value; }
        }

        public virtual string Description
        {
            get { return _Description; }
            set { _Description = value; }
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

        public virtual int? DictionaryTypeID
        {
            get { return _DictionaryTypeID; }
            set { _DictionaryTypeID = value; }
        }
                
        public virtual int? REGEXTypeID
        {
            get { return _REGEXTypeID; }
            set { _REGEXTypeID = value; }
        }

        public virtual string DefaultText
        {
            get { return _DefaultText; }
            set { _DefaultText = value; }
        }

        public virtual string ControlType
        {
            get { return _ControlType; }
            set { _ControlType = value; }
        }

        public virtual int? ShowType
        {
            get { return _ShowType; }
            set { _ShowType = value; }
        }
        #endregion
    }
}
