using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class BundlePackageInfo
    {
        #region 
        private int? _RelationID = null;
        private int? _PackageID = null;
        private int? _BundleID = null;

        private bool _IsDelete = false;

       

        private PackageInfo _PackageInfo;

       

        #endregion

        #region Attribute

        public int? RelationID
        {
            get { return _RelationID; }
            set { _RelationID = value; }
        }

        public int? PackageID
        {
            get { return _PackageID; }
            set { _PackageID = value; }
        }

        public int? BundleID
        {
            get { return _BundleID; }
            set { _BundleID = value; }
        }


        public PackageInfo PackageInfo
        {
            get { return _PackageInfo; }
            set { _PackageInfo = value; }
        }

        public bool IsDelete
        {
            get { return _IsDelete; }
            set { _IsDelete = value; }
        }
        #endregion
    }
}
