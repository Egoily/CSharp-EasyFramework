namespace com.etak.core.model
{
    [System.Serializable]
    public class CrmMVNOMSISDNGroupTypeInfo
    {
        private int msisdnGroupTypeID;
        public virtual int MSISDNGroupTypeID
        {
            get { return msisdnGroupTypeID; }
            set { msisdnGroupTypeID = value; }
        }

        private int _MVNOID;
        public virtual int MVNOID 
        {
            get { return _MVNOID; }
            set { _MVNOID = value; }
        }

        private string description ;
        public virtual string Description 
        {
            get { return description; }
            set { description = value; }
        }

        private int? maxMemberCount;
        public virtual int? MaxMemberCount
        {
            get { return maxMemberCount; }
            set { maxMemberCount = value; }
        }
    }
}
