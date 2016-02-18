
using com.etak.core.operation.contract;

namespace com.etak.core.bizops.fullfilment.CancelCustomerAndSubscriptions
{
    /// <summary>
    /// Configuration with the settings requierd to perfrom CancelCustomerAndSubscription
    /// </summary>
    public class CancelCustomerAndSubscriptionConfiguration : BasicOperationConfiguration
    {
        /// <summary>
        /// Can delete promotions
        /// </summary>
        public bool CanDeletePromotions { get; set; }

        /// <summary>
        /// Can remove roamingblacklist
        /// </summary>
        public bool CanRemoveRoamingBlackList { get; set; }


        /// <summary>
        /// Can delete CustomerDataRoamingLimit and CustomerDataRoamingLimitNotifications
        /// </summary>
        public bool CanDeleteDataRoamingLimit { get; set; }

        /// <summary>
        /// Can un bind IMEI to OTA
        /// </summary>
        public bool CanUnBindIMEIToOTA { get; set; }

        /// <summary>
        /// SettingId to get SettingInfo to acquire ActiveDeadlineDate
        /// </summary>
        public int SettingId { get; set; }

        /// <summary>
        /// DetailId to get SettingDetailInfo to acquire ActiveDeadlineDate
        /// </summary>
        public int DetailId { get; set; }
        /// <summary>
        /// Boolean to decide if the HPS should be used
        /// </summary>
        public bool HasToUseHLR { get; set; }

    }
}
