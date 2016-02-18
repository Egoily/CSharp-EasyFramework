using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class MNPReasonInfo
    {

        private int reasonId;
        private string reasonCode;
        private string reasonDescription;
        private int? reasonType;
        private int languageID;

        public MNPReasonInfo()
        {

        }

        public MNPReasonInfo(int reasonId, string reasonCode, string reasonDescription, int? reasonType)
        {
            this.reasonId = reasonId;
            this.reasonCode = reasonCode;
            this.reasonDescription = reasonDescription;
            this.reasonType = reasonType;
        }

        public virtual int ReasonId
        {
            get { return reasonId; }
            set { reasonId = value; }
        }

        public virtual string ReasonCode
        {
            get { return reasonCode; }
            set { reasonCode = value; }
        }

        public virtual string ReasonDescription
        {
            get { return reasonDescription; }
            set { reasonDescription = value; }
        }

        public virtual int? ReasonType
        {
            get { return reasonType; }
            set { reasonType = value; }
        }

        public virtual int LanguageID
        {
            get { return languageID; }
            set { languageID = value; }
        }
    }
}
