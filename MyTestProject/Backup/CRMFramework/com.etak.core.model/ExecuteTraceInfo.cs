using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class ExecuteTraceInfo
    {
        private long? _ID = null;
        public long? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private int? _TYPEID = null;
        public int? TYPEID
        {
            get { return _TYPEID; }
            set { _TYPEID = value; }
        }

        private string _ASSEMBLYNAME;
        public string ASSEMBLYNAME
        {
            get { return _ASSEMBLYNAME; }
            set { _ASSEMBLYNAME = value; }
        }

        private string _CLASSNAME;
        public string CLASSNAME
        {
            get { return _CLASSNAME; }
            set { _CLASSNAME = value; }
        }

        private string _METHODNAME;
        public string METHODNAME
        {
            get { return _METHODNAME; }
            set { _METHODNAME = value; }
        }
        
        private byte[] _INPUTPARAM = null;
        public byte[] INPUTPARAM
        {
            get { return _INPUTPARAM; }
            set { _INPUTPARAM = value; }
        }

        private DateTime? _BEGINDATE = null;
        public DateTime? BEGINDATE
        {
            get { return _BEGINDATE; }
            set { _BEGINDATE = value; }
        }

        private DateTime? _ENDDATE = null;
        public DateTime? ENDDATE
        {
            get { return _ENDDATE; }
            set { _ENDDATE = value; }
        }

        private string _DATASIZE;
        public string DATASIZE
        {
            get { return _DATASIZE; }
            set { _DATASIZE = value; }
        }

        private string _PRECPULOAD;
        public string PRECPULOAD
        {
            get { return _PRECPULOAD; }
            set { _PRECPULOAD = value; }
        }

        private string _PREMEMORYSTATUS;
        public string PREMEMORYSTATUS
        {
            get { return _PREMEMORYSTATUS; }
            set { _PREMEMORYSTATUS = value; }
        }

        private string _POSTCPULOAD;
        public string POSTCPULOAD
        {
            get { return _POSTCPULOAD; }
            set { _POSTCPULOAD = value; }
        }

        private string _POSTMEMORYSTATUS;
        public string POSTMEMORYSTATUS
        {
            get { return _POSTMEMORYSTATUS; }
            set { _POSTMEMORYSTATUS = value; }
        }

        private string _TRACEORDER;
        public string TRACEORDER
        {
             get { return _TRACEORDER; }
            set { _TRACEORDER = value; }
        }

        private string _DESCRIPTION;
        public string DESCRIPTION
        {
            get { return _DESCRIPTION; }
            set { _DESCRIPTION = value; }
        }

        private int _STEP;
        public int STEP
        {
            get { return _STEP; }
            set { _STEP = value; }
        }
    }
}
