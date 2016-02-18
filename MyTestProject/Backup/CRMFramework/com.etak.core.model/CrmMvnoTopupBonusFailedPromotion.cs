using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class CrmMvnoTopupBonusFailedPromotion
    {
        private int ID;
        private int DEALERID;
        private int CUSTOMERID;
        private string MSISDN;
        private int BONUSCONFIGID;
        private int PROMOTIONID;
        private long HISTORYID;
        private string ERRORMESSAGE;
        private DateTime? TOPUPDATE;

        public int Id
        {
            set { this.ID = value; }
            get { return this.ID; }
        }

        public int DealerID
        {
            set { this.DEALERID = value; }
            get { return this.DEALERID; }
        }

        public int CustomerId
        {
            set { this.CUSTOMERID = value; }
            get { return this.CUSTOMERID; }
        }

        public string Msisdn
        {
            set { this.MSISDN = value; }
            get { return this.MSISDN; }
        }

        public int BonusConfigId
        {
            set { this.BONUSCONFIGID = value; }
            get { return this.BONUSCONFIGID; }
        }

        public int PromotionId
        {
            set { this.PROMOTIONID = value; }
            get { return this.PROMOTIONID; }
        }

        public long HistoryId
        {
            set { this.HISTORYID = value; }
            get { return this.HISTORYID; }
        }

        public string ErrorMessage
        {
            set { this.ERRORMESSAGE = value; }
            get { return this.ERRORMESSAGE; }
        }

        public DateTime? TopupDate
        {
            set { this.TOPUPDATE = value; }
            get { return this.TOPUPDATE; }
        }
    }
}
