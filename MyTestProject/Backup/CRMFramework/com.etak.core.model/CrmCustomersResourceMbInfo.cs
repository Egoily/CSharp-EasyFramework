using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using com.etak.core.model.provisioning;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class CrmCustomersResourceMbInfo : ModelBase
    {
        public CrmCustomersResourceMbInfo()
        {
            Initilize();
        }

        void Initilize()
        {
            CrmCustomersResourceMbPropertyInfo = new List<CrmCustomersResourceMbPropertyInfo>();
            CrmMobileCallBarringInfo = new List<CrmMobileCallBarringInfo>();
            CrmMobileCallForwardInfo = new List<CrmMobileCallForwardInfo>();
            CrmMobileCallWaitingInfo = new List<CrmMobileCallWaitingInfo>();
            CrmMobileCamelUcsiInfo = new List<CrmMobileCamelUcsiInfo>();
            CrmMobileCugFeatureInfo = new List<CrmMobileCugFeatureInfo>();
            CrmMobileCugSubsInfo = new List<CrmMobileCugSubsInfo>();
            CrmMobileMultipleImsiInfo = new List<CrmMobileMultipleImsiInfo>();
            CrmMobileNetWorkInfo = new List<CrmMobileNetWorkInfo>();
            CrmMobileSSInfo = new List<CrmMobileSSInfo>();
            MobileCamelDataList = new List<CrmMobileCamelDataInfo>();
            CrmMobileCamelCsiDataList = new List<CrmMobileCamelCsiDataInfo>();
            CrmMobileCamelCsiDPList = new List<CrmMobileCamelCsiDPInfo>();
            ProvisioningSubscriber = new ProvisioningSubscriber();
            CustomerInfo = new CustomerInfo();
            OperatorInfo = new DealerInfo();
        }

        public virtual ProvisioningSubscriber ProvisioningSubscriber { get; set; }

        public virtual CustomerInfo CustomerInfo { get; set; }
        public virtual DealerInfo OperatorInfo { get; set; }

        public virtual int RESOURCEID { get; set; }
        public virtual string RESOURCE { get; set; }
        public virtual string ICC { get; set; }
        public virtual string IMSI { get; set; }
        public virtual string REMARKS { get; set; }
        public virtual string MSISDNALERTIND { get; set; }
        public virtual string ODBMASK { get; set; }
        public virtual bool USSDALLOWED { get; set; }
        public virtual int CB_SUBSOPTION { get; set; }
        public virtual string CB_PASSWORD { get; set; }
        public virtual int CB_WRONGATTEMPTS { get; set; }
        public virtual int CALCULATION { get; set; }
        public virtual int STATUSID { get; set; }
        public virtual DateTime? FIRSTUSED { get; set; }
        public virtual DateTime? LASTUSED { get; set; }
        public virtual DateTime? STARTDATE { get; set; }
        public virtual DateTime? ENDDATE { get; set; }
        public virtual DateTime? CREATEDATE{ get; set; }
        public virtual int USERID { get; set; }
        public virtual string PUK { get; set; }
        public virtual string TELESERVICELIST { get; set; }
        public virtual string BEARERSERVICELIST { get; set; }

        /// <summary>
        /// Legacy problem: In CRM, BEARERSERVICELIST is old TK code, not 3GPP standard.
        /// We introduce this _BS_3GPP field to represent 3GPP standard BS code.
        /// The _BS_3GPP is NOT mapped to CRM database, but a runtime property.
        /// One CrmCustomerResourceMBInfo object always have two fields , one to contain legacy TK BS code,
        /// another to contain 3GPP BS code.
        /// </summary>
        
        public virtual string BearerService_3GPP { get; set; }
        public virtual DateTime? CHANGESTATUSDATE { get; set; }
        public virtual DateTime? LASTCONSUMEDATE { get; set; }
        public virtual DateTime? ACTIVEDEADLINEDATE { get; set; }

        public virtual int PININVALIDTIMES { get; set; }
        public virtual int PININVALIDTIMESTOTAL { get; set; }
        public virtual int OCPPlmnTemplateId { get; set; }
        public virtual int ProvisionId { get; set; }
        public virtual int NAM { get; set; }       
        public virtual string FTNRule { get; set; }

        public virtual IList<CrmCustomersResourceMbPropertyInfo> CrmCustomersResourceMbPropertyInfo { get; set; }

        public virtual bool DeleteFlag { get; set; }
        public virtual int CamelRestrictedStatus { get; set; }

        public virtual IList<CrmMobileCallBarringInfo> CrmMobileCallBarringInfo
        {
            get;
            set;
        }

        public virtual IList<CrmMobileCallForwardInfo> CrmMobileCallForwardInfo
        {
            get;
            set;
        }

        public virtual IList<CrmMobileCallWaitingInfo> CrmMobileCallWaitingInfo
        {
            get;
            set;
        }

        public virtual IList<CrmMobileCamelUcsiInfo> CrmMobileCamelUcsiInfo
        {
            get;
            set;
        }

        public virtual IList<CrmMobileCugFeatureInfo> CrmMobileCugFeatureInfo
        {
            get;
            set;
        }

        public virtual IList<CrmMobileCugSubsInfo> CrmMobileCugSubsInfo
        {
            get;
            set;
        }

        public virtual IList<CrmMobileMultipleImsiInfo> CrmMobileMultipleImsiInfo
        {
            get;
            set;
        }

        public virtual IList<CrmMobileNetWorkInfo> CrmMobileNetWorkInfo
        {
            get;
            set;
        }

        public virtual IList<CrmMobileSSInfo> CrmMobileSSInfo
        {
            get;
            set;
        }


        public virtual IList<CrmMobileCamelDataInfo> MobileCamelDataList
        {
            get;
            set;
        }
        // Modify by wood, for non camel project on 2013-04-22
        public virtual IList<CrmMobileCamelCsiDataInfo> CrmMobileCamelCsiDataList
        {
            get;
            set;
        }


        public virtual IList<CrmMobileCamelCsiDPInfo> CrmMobileCamelCsiDPList
        {
            get;
            set;
        }
        // End-modify by wood, for non camel project on 2013-04-22
    }
}
