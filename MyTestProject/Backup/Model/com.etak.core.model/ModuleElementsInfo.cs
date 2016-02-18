using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class ModuleElementsInfo
    {
        #region 成员
        private int _ID;
        private int? _ShowModuleID;
        private string _FullClassName;
        private string _ElementID;
        private int? _ElementType;
        #endregion

        #region 属性
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int? ShowModuleID
        {
            get { return _ShowModuleID; }
            set { _ShowModuleID = value; }
        }

        public string FullClassName
        {
            get { return _FullClassName; }
            set { _FullClassName = value; }
        }

        public string ElementID
        {
            get { return _ElementID; }
            set { _ElementID = value; }
        }

        public int? ElementType
        {
            get { return _ElementType; }
            set { _ElementType = value; }
        }
        #endregion
    }
}
