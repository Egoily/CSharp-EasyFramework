using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class TroubleTicketQuestionInfo
    {
        public TroubleTicketQuestionInfo() { }
         public TroubleTicketQuestionInfo(string troubleType,string tourbledesc)
        {
            this._TROUBLETYPE = troubleType;
            this._TROUBLEDESC = tourbledesc;           
        }

         public TroubleTicketQuestionInfo(string troubleType, string tourbledesc, string troubleSubType, string tourbleSubDesc)
        {
            this._TROUBLETYPE = troubleType;
            this._TROUBLEDESC = tourbledesc;
            this._TTSUBTYPE = troubleSubType;
            this._TTSUBDESC = tourbleSubDesc;            
        }

        #region 
        private int? _ID = null;
        private string _TROUBLETYPE;
        private string _TROUBLEDESC;
        private string _TTSUBTYPE;
        private string _TTSUBDESC;
        private int? _QUESTIONCODE = null;
        private string _QUESTIONDESC;
        private int? _MVNOID = null;
        #endregion


        #region Attribute
        public virtual int? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string TROUBLETYPE
        {
            get { return _TROUBLETYPE; }
            set { _TROUBLETYPE = value; }
        }

        public string TROUBLEDESC
        {
            get { return _TROUBLEDESC; }
            set { _TROUBLEDESC = value; }
        }

        public string TTSUBTYPE
        {
            get { return _TTSUBTYPE; }
            set { _TTSUBTYPE = value; }
        }

        public string TTSUBDESC
        {
            get { return _TTSUBDESC; }
            set { _TTSUBDESC = value; }
        }

        public int? QUESTIONCODE
        {
            get { return _QUESTIONCODE; }
            set { _QUESTIONCODE = value; }
        }

        public string QUESTIONDESC
        {
            get { return _QUESTIONDESC; }
            set { _QUESTIONDESC = value; }
        }

        public virtual int? MVNOID
        {
            get { return _MVNOID; }
            set { _MVNOID = value; }
        }
        #endregion
    }
}
