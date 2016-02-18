using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class ActionTypeInfo
    {

        #region Member
        private int? _TypeID = null;
        private string _ActionType;
        private int? _ModuleID = null;
        private string _Description;
        #endregion


        #region attribute
        public int? TypeID
        {
            get { return _TypeID; }
            set { _TypeID = value; }
        }

        public string ActionType
        {
            get { return _ActionType; }
            set { _ActionType = value; }
        }

        public int? ModuleID
        {
            get { return _ModuleID; }
            set { _ModuleID = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        #endregion

    }
}
