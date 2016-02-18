using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class TTDeptInfo
    {
        #region 
        private int? _DEPTID = null;
        private int? _FUID = null;
        private string _DEPTNAME;
        private int? _DEPTHEADER = null;
        private int? _DEPTPARENT = null;
        private int? _DEPTROLEID = null;
        private int? _DEPTID25 = null;
        #endregion

        #region Attribute
        public int? DEPTID
        {
            get { return _DEPTID; }
            set { _DEPTID = value; }
        }

        public int? FUID
        {
            get { return _FUID; }
            set { _FUID = value; }
        }


        public string DEPTNAME
        {
            get { return _DEPTNAME; }
            set { _DEPTNAME = value; }
        }
        public int? DEPTHEADER
        {
            get { return _DEPTHEADER; }
            set { _DEPTHEADER = value; }
        }

        public int? DEPTPARENT
        {
            get { return _DEPTPARENT; }
            set { _DEPTPARENT = value; }
        }

        public int? DEPTROLEID
        {
            get { return _DEPTROLEID; }
            set { _DEPTROLEID = value; }
        }

        public int? DEPTID25
        {
            get { return _DEPTID25; }
            set { _DEPTID25 = value; }
        }
        #endregion
    }
}
