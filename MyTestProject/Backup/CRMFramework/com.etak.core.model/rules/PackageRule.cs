using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [Serializable]
    public class PackageRuleParam
    {
        public IEnumerable<BundleInfo> NewBundles { get; set; }
        public bool Chargeable { get; set; }
        public int UserId { get; set; }
        public int LanguageId { get; set; }
        public decimal TopupAmount { get; set; }
        public CustomerInfo CustomerInfo { get; set; }
        public ProductInfo ProductInfo { get; set; }
        public RmPromotionGroupInfo PromoGroupInfo { get; set; }
        public IEnumerable<RmPromotionGroupInfo> PromoGroupInfoList { get; set; }
        public PackageInfo PackageInfo { get; set; }
        public BundleInfo BundleInfo { get; set; }
        public PackageRule PackageRule { get; set; }
        public ServicesInfo CustServicesInfo { get; set; }
        public IEnumerable<CrmCustomersBonusRelationShipInfo> CustBonusRelationInfoList { get; set; }        // By Francisco 2014.08.04
        public string ActionsRequestedPCRF { get; set; }   
        public string AdjustmentCode { get; set; }
        public DateTime? ScheduleDeliveryTime { get; set; }
    }
    
    public enum RuleCaller
    {
        changePackageAPI = 0,
        topUp =1,
        applyPromotionGroupAPI=2,
        topUp4SmartLibre =3,
        UnlimitedSocialNetwork=4,
    }

    [DataContract]
    public abstract class PackageRule : BussinessRule
    {
        public abstract void ExecuteActions(PackageRuleParam param, RuleCaller ruleCaller);
        public abstract EligibleResult IsEligible(PackageRuleParam param, RuleCaller ruleCaller);
    }
}
