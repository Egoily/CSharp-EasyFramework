using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class PerformanceTraceConfigInfo
    {
        #region 成员
        private long? _ID = null;
        private string _AssembleName;
        private string _ClassName;
        private string _MethodName;
        private string _Description;
        private int? _IsTrace = null;
        private int? _CreateUserID = null;
        private DateTime? _CreateDate = null;

        private string _FullClassName;
        #endregion

        public long? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string AssembleName
        {
            get { return _AssembleName; }
            set { _AssembleName = value; }
        }

        public string ClassName
        {
            get { return _ClassName; }
            set { _ClassName = value; }
        }

        public string MethodName
        {
            get { return _MethodName; }
            set { _MethodName = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public int? IsTrace
        {
            get { return _IsTrace; }
            set { _IsTrace = value; }
        }

        public int? CreateUserID
        {
            get { return _CreateUserID; }
            set { _CreateUserID = value; }
        }

        public DateTime? CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        public string FullClassName
        {
            get { return _FullClassName; }
            set { _FullClassName = value; }
        }
    }
}
