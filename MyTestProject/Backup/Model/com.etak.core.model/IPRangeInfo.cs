using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class IPRangeInfo
    {
        #region 成员
        private int? _RANGEID;

        public int? RANGEID
        {
            get { return _RANGEID; }
            set { _RANGEID = value; }
        }
        private string _TSC;

        public string TSC
        {
            get { return _TSC; }
            set { _TSC = value; }
        }
        private Int64? _IPRANGE1;

        public Int64? IPRANGE1
        {
            get { return _IPRANGE1; }
            set { _IPRANGE1 = value; }
        }
        private Int64? _IPRANGE2;

        public Int64? IPRANGE2
        {
            get { return _IPRANGE2; }
            set { _IPRANGE2 = value; }
        }
        private DateTime? _STARTDATE;

        public DateTime? STARTDATE
        {
            get { return _STARTDATE; }
            set { _STARTDATE = value; }
        }
        private DateTime? _ENDDATE;

        public DateTime? ENDDATE
        {
            get { return _ENDDATE; }
            set { _ENDDATE = value; }
        }
        #endregion

    }
}
