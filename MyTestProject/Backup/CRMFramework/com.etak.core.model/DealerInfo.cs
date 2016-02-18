using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model
{

    [Serializable]
    public enum EDealerType
    {
        FiscalUnit = 1,
        Reseller = 2,
        Agent = 3,
        Subagent = 4
    }

    [DataContract]
    [Serializable]
    public class DealerInfo
    {
        public DealerInfo()
        {
            DealerBankList = new List<DealerBankInfo>();
            DealerLoyaltyList = new List<DealerLoyaltyInfo>();
            DealerOBOPRSList = new List<DealerOBOPRSInfo>();
            DealerPropertiesList = new List<DealerPropertiesInfo>();
            CurrentCrmDefaultProvisionInfoList = new List<CrmDefaultProvisionInfo>();
            DealerRatePlanList = new List<DealerRatePlanInfo>();
            RoamingSettingList = new List<RoamingSettingInfo>();
            MvnoDataRoamingLimitList = new List<MvnoDataRoamingLimitInfo>();
            MVNODataRoamingLimitNotificationList = new List<MVNODataRoamingLimitNotification>();
            MVNOConfigActionList = new List<MVNOConfigActionInfo>();
        }

        /// <summary>
        /// ATM Topup configration info.
        /// </summary>
        virtual public DealerATMTopUpConfigInfo DealerATMTopUpConfigInfo { get; set; }
        /// <summary>
        /// MVNO Properties
        /// </summary>
        virtual public MVNOPropertiesInfo MVNOPropertiesInfo { get; set; }
        /// <summary>
        /// MVNO ConfigActions
        /// </summary>
        virtual public IList<MVNOConfigActionInfo> MVNOConfigActionList { get; set; }
        /// <summary>
        /// Dealer bank
        /// </summary>
        virtual public IList<DealerBankInfo> DealerBankList { get; set; }
        virtual public IList<DealerLoyaltyInfo> DealerLoyaltyList { get; set; }
        /// <summary>
        /// Dealer Properties.
        /// </summary>
        virtual public IList<DealerPropertiesInfo> DealerPropertiesList { get; set; }
        /// <summary>
        /// MVNO Provision Setting
        /// </summary>
        virtual public IList<CrmDefaultProvisionInfo> CurrentCrmDefaultProvisionInfoList { get; set; }

        public virtual IList<DealerRatePlanInfo> DealerRatePlanList { get; set; }
        public virtual IList<RoamingSettingInfo> RoamingSettingList { get; set; }
        /// <summary>
        /// MVNO Data Roaming Limit Setting
        /// </summary>
        public virtual IList<MvnoDataRoamingLimitInfo> MvnoDataRoamingLimitList { get; set; }

        /// <summary>
        ///Data Roaming Limit Notification Settings
        /// </summary>
        public virtual IList<MVNODataRoamingLimitNotification> MVNODataRoamingLimitNotificationList { get; set; }
        /// <summary>
        /// for beta9
        /// </summary>
        public virtual IList<DealerOBOPRSInfo> DealerOBOPRSList
        {
            get;
            set;
        }
        virtual public int? DealerID { get; set; }
        virtual public int? ParentID { get; set; }
        virtual public string DealerNode { get; set; }
        virtual public int? DealerTypeID { get; set; }
        virtual public int? FiscalUnitID { get; set; }
        virtual public int? ResellerID { get; set; }
        virtual public int? AgentID { get; set; }
        virtual public int? SubagentID { get; set; }
        virtual public string Company { get; set; }
        virtual public string Contact { get; set; }
        virtual public int? TitleID { get; set; }
        virtual public int? GenderID { get; set; }
        virtual public string Address { get; set; }
        virtual public string HouseNO { get; set; }
        virtual public string Zipcode { get; set; }
        virtual public string City { get; set; }
        virtual public int? CountryID { get; set; }
        virtual public string Telephone { get; set; }
        virtual public string Telefax { get; set; }
        virtual public string Email { get; set; }
        virtual public string CHOC { get; set; }
        virtual public string VAT { get; set; }
        virtual public DateTime? CreateDate { get; set; }
        virtual public int? UserID { get; set; }
        virtual public int? CreateUser { get; set; }
        virtual public int? UpdateUser { get; set; }
        virtual public DateTime? UpdateDate { get; set; }
        virtual public int? State { get; set; }
        virtual public int? Hide { get; set; }
        virtual public int? MvnotypeID { get; set; }
        //TODO: added for KSA to Base
        virtual public string ExternalId { get; set; }
    }
}
