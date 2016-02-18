using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class SmsTemplateInfo
    {
        private int _ID;
        private int _MVNOID;
        private int? _LANGUAGEID = Convert.ToInt32(Culture.en_US);
        private int _CODE;
        private string _DESCRIPTION;
        private string _TITLE;
        private string _TITLEARGS;
        private string _BODY;
        private string _BODYARGS;
        private int? _SMSCODING = 0;
        private Boolean _ENABLED;
        private DateTime? _STARTDATE;
        private DateTime? _ENDDATE;
        private int? _TEXTTYPE;
      
        public virtual int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual int MVNOID
        {
            get { return _MVNOID; }
            set { _MVNOID = value; }
        }

        public virtual int? LANGUAGEID
        {
            get { return _LANGUAGEID; }
            set { _LANGUAGEID = value; }
        }

        public virtual int CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public virtual string DESCRIPTION
        {
            get { return _DESCRIPTION; }
            set { _DESCRIPTION = value; }
        }

        public virtual string TITLE
        {
            get { return _TITLE; }
            set { _TITLE = value; }
        }

        public virtual string TITLEARGS
        {
            get { return _TITLEARGS; }
            set { _TITLEARGS = value; }
        }

        public virtual string BODY
        {
            get { return _BODY; }
            set { _BODY = value; }
        }

        public virtual string BODYARGS
        {
            get { return _BODYARGS; }
            set { _BODYARGS = value; }
        }

        public virtual int? SMSCODING
        {
            get { return _SMSCODING; }
            set { _SMSCODING = value; }
        }

        public virtual Boolean ENABLED
        {
            get { return _ENABLED; }
            set { _ENABLED = value; }
        }

        public virtual DateTime? STARTDATE
        {
            get { return _STARTDATE; }
            set { _STARTDATE = value; }
        }

        public virtual DateTime? ENDDATE
        {
            get { return _ENDDATE; }
            set { _ENDDATE = value; }
        }

        public virtual int? TEXTTYPE
        {
            get { return _TEXTTYPE; }
            set { _TEXTTYPE = value; }
        }
    }

    [DataContract]
    [Serializable]
    public class ITextTemplate
    {
        private int _MvnoId;
        private int _Code;
        private string _Title;
        private string _Body;
        private int? _LANGUAGEID = Convert.ToInt32(Culture.en_US);

        public virtual int MvnoId
        {
            get { return _MvnoId; }
            set { _MvnoId = value; }
        }
        public virtual int Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
        public virtual string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        public virtual string Body
        {
            get { return _Body; }
            set { _Body = value; }
        }
        public virtual int? LANGUAGEID
        {
            get { return _LANGUAGEID; }
            set { _LANGUAGEID = value; }
        }
    }
}
