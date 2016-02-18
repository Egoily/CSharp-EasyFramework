using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class PackageDealerMappingInfo
    {
        #region 成员
        private int? _ID = null;

        public int? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private int? _DealerID = null;

        public int? DealerID
        {
            get { return _DealerID; }
            set { _DealerID = value; }
        }
        private int? _PackageID = null;

        public int? PackageID
        {
            get { return _PackageID; }
            set { _PackageID = value; }
        }
        private DateTime? _StartDate = null;

        public DateTime? StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private DateTime? _EndDate = null;

        public DateTime? EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        private int? _Status = null;

        public int? Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private int? _Priority = null;

        public int? Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }
        private int? _MVNOID = null;

        public int? MVNOID
        {
            get { return _MVNOID; }
            set { _MVNOID = value; }
        }
        private DateTime? _CreateDate = null;

        public DateTime? CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }
        #endregion
    }
}
