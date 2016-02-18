using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class ModuleMvnoInfo
    {
        #region 成员
        private int _ID;
        private int? _ShowModuleID = null;
        private int? _ParentID = null;
        private int? _ModuleLevel = null;
        private string _ModuleName;
        private string _ModuleFullName;
        private int? _MvnoID = null;
        private int? _CreateUser = null;
        private DateTime? _CreateDate = null;
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

        public int? ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }

        public int? ModuleLevel
        {
            get { return _ModuleLevel; }
            set { _ModuleLevel = value; }
        }

        public string ModuleName
        {
            get { return _ModuleName; }
            set { _ModuleName = value; }
        }

        public string ModuleFullName
        {
            get { return _ModuleFullName; }
            set { _ModuleFullName = value; }
        }

        public int? MvnoID
        {
            get { return _MvnoID; }
            set { _MvnoID = value; }
        }

        public int? CreateUser
        {
            get { return _CreateUser; }
            set { _CreateUser = value; }
        }

        public DateTime? CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        #endregion
    }
}
