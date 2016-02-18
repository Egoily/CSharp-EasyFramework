using System;

namespace com.etak.core.model
{
    public enum SIMCardAssignStatus
    {
        Free = 0,
        Used = 1,
        Reserved = 2
    }

    public enum SIMCardManufacturerEncryptionType
    {
        PlainText = 0,
        DES = 1,
        AES_128 = 2
    }

    public enum SIMCardType
    {
        GSM_2G = 0,
        UMTS_3G = 1
    }

    /// <summary>
    /// SMSC priority value range 0-3
    /// </summary>
    public enum SmsPriority
    {
        /// <summary>
        /// 强制立即发
        /// </summary>
        ForceImmediately = 0,
        /// <summary>
        /// 立即发
        /// </summary>
        Immediately = 1,
        /// <summary>
        /// 定时发
        /// </summary>
        Clocked = 2,
        /// <summary>
        /// 暂停
        /// </summary>
        Suspend = 3
    }

    public enum EmailPriority
    {
        /// <summary>
        /// 立即发
        /// </summary>
        Immediately = 1,
        /// <summary>
        /// 定时发
        /// </summary>
        Clocked = 2,
        /// <summary>
        /// 暂停
        /// </summary>
        Suspend = 3
    }



    public enum SmsCategory
    {
        Normal = 0,
        AlertEmail = 1,
        Lifecycle = 2,
        Billing = 3,
        DRESynchronizedAPI = 6,
        BENJOB1002 = 1002,
        BENJOB1004 = 1004,
        BENJOB1005 = 1005,
        BENJOB1006 = 1006,
        BENJOB1009 = 1009,
        BENJOB1010 = 1010,
        BENJOB1011 = 1011,
        BENJOB2000 = 2000,
        BENJOB2001 = 2001,
        BENJOB2002 = 2002,
        BENJOB2006 = 2006,
        BENJOB2007 = 2007,
        BENJOB3006 = 3006,
        BENJOB3015 = 3015,
        BENJOB2004 = 2004,
        BENJOB3001 = 3001,
        BENJOB3002 = 3002,
        BENJOB2003 = 2003,
        BENJOB3003 = 3003,
        BENJOB4001 = 4001,
        BENJOB1003 = 1003,
        BENJOB1007 = 1007,
        BENJOB4002 = 4002,
        BENJOB3016 = 3016,
        //add by winson 
        AlertSMSLevel1 = 5001,
        AlertSMSLevel2 = 5002,
        AlertSMSLevel3 = 5003,
        AlertSMSLevel4 = 5004,
        AlertSMSLevel5 = 5005,
        //end by winson

        //add by rabi 2010 -09-15
        BENJOB8000 = 8000,
        //add by rabi 2010-09-20
        BENJOB3009 = 3009,
        BENJOB3010 = 3010,
        BENJOB3011 = 3011,
        BENJOB3012 = 3012,
        BENJOB3013 = 3013,

        #region API Jeffrey 2010-12-22
        SELFCAREAPI = 8,
        ATOSAPI = 9,
        #endregion
        //add by James 2010-12-23
        TopUpBonus = 10,

        RoamingWelcome = 11,
        PromotionLifeCycle = 12,
        SetUp = 1201,
        RenewSuccess = 1202,
        RenewFailed = 1203,
        xExhausted = 1204,
        yExhausted = 1205,
        AllExhausted = 1206,

        CommercialSMS = 13,
        UssdText = 14,

        //Added by liny,2012-08-20,for Data Roaming Limit project.
        DataRoamingThresholdNotification = 15,
		DataRoamingContinueThresholdNotification = 16,
        //End-Added by liny,2012-08-20,for Data Roaming Limit project.
        #region Added by Liny,2013-01-17 , for SRS for Balance Transfer V0.7,merged from VF_NL_Saudi_dev branch,2013-01-24
        BalanceTransfer = 1207,
        LowARPU = 1208, //Add by wood, ARPU means the Average Revenue Per User, this enum item mean the customer is not topup or spend very much money 
        #endregion
        #region added by neil at 2013/12/26 for Super Top-up
        SuperTopupActivationSuccessNotification = 23,
        SuperTopupNextPeriodStartNotification = 24,
        SuperTopupPlanExpiredNotification = 25,
        #endregion

        MinimizeAmount = 1500,
        RegisterSuccess = 1501,
        //add for sendsms by raymond 2013-11-14
        SubmitDocumentInWDaysNotificationEvent = 7000,
        SubmitDocumentInXDaysNotificationEvent = 7001,
        SubmitDocumentInYDaysNotificationEvent = 7002,
        SubmitDocumentInZDaysNotificationEvent = 7003,
        SocialMediaBundleEnabledInXDaysNotificationEvent = 7005,
        SocialMediaBundleDisabledAfterYDaysNotificationEvent = 7006,
        SocialMediaBundleDisabledNotificationEvent = 7007,
        PricePlanChangedNotificationEvent = 7008,
        EmailChangedNotificationEvent = 7009,
        //add by damon 2013-01-06
        EmergencyCreditApplyNotificationEvent = 7010,
        EmergencyCreditApplySuccessNotificationEvent = 7011,
        EmergencyCreditApplyFailNotificationEvent = 7012,
        EmergencyCreditUnPaidNotificationEvent = 7013,
        TransferRequestTerminationForANotificationEvent = 7014,
        TransferRequestTerminationForBNotificationEvent = 7015,

        //added by neil at 2014/3/27   sms notify for Elektra customer status changing
        ChangeStatusToS1 = 1601,
        ChangeStatusToS2 = 1602,
        ChangeStatusToS3 = 1603,
        ChangeStatusToS4 = 1604,
        FinishActivation = 1605,
        FinishPortability = 1606,
        DocumentValidation = 6000,
       //add by Ben 2014-2-10 
        DocumentRejectedNotificationEvent = 7016,

        //add by damon,2013-02-18 for Low Balance Notification
        LowBalanceNotificationEvent = 7017,
        //add by damon,2013-02-18 for Balance Left Notification
        CreditLimitLeftNotificationEvent = 7018,
        //add by damon,2014-04-18
        LanguageChangedNotificationEvent = 7019,
        TopUpWithEmergencyCreditNotificationEvent = 7022,
        //Added by Benny 2014-06-20
        AlElmRetryRegisterRejectEvent = 7024,
        //add by raymond 2014-06-20
        PromotionXDayResetNotificationEvent = 7023,

        SelectLanguageNotificationEvent = 7025,
        ConfirmLanguageNotificationEvent = 7026,
        SelectOtherLanguageNotificationEvent = 7027,
        WithoutSelectLanguageNotificationEvent = 7028,

        // add by Ben 2014-7-11
        CreditLimitExceededNotificationEvent = 7029,

        ChangeBillTypeEvent = 7030,

        IvoryToSilverEvent = 7031,
        SilverToGoldEvent = 7032,
        GoldToPlatinumEvent = 7033,
        SilverToIvoryEvent = 7034,
        GoldToSilverEvent = 7035,
        PlatinumToGoldEvent = 7036,

        PromotionDeactivatedEvent = 7037,

        PrepaidReferralActivedToReferralEvent = 7038,
        PrepaidReferralActivedToSponsorEvent = 7039,
        PrepaidReferralTopUpEnoughToSponsorEvent = 7040,
        PostpaidReferralActivedToReferralEvent = 7041,
        PostpaidReferralActivedToSponsorEvent = 7042,
        PostpaidReferralConsumeEnoughToSponsorEvent = 7043,

        PortInAcceptedEvent = 7044,
        PortInRejectedEvent = 7045,
        PortInActiveEvent = 7046,
        PortInActiveAndMembershipEvent = 7047,
        PortInActiveAndPromotionEvent = 7048,
        PortInTopUpEnoughEvent = 7049,
        NoSelectNotification = 7050,

        PrepaidReferralTopUpEnoughToReferralEvent = 7051,
        PostpaidReferralConsumeEnoughToReferralEvent = 7052,

        PostpaidReferralConsumeEnoughToPrepaidSponsorEvent = 7053,
        PrepaidReferralTopUpEnoughToPostpaidSponsorEvent = 7054,

        // add by sam 2014-9-19
        RoamingAllowanceFirstThresholdEvent = 7055,
        RoamingAllowanceEndThresholdEvent = 7056,
        InternationalBalanceTransfer = 1208,
        FirstIMSIAttachEvent = 7057,
        TransferOwnershipNotificationEvent = 7058,
        SuperSavingExpiredEvent = 7059,
    }

    public enum EmailCategory
    {
        AlertEmail = 1,
        Lifecycle = 2,
        ForgetPasswordOfUser = 3,
        CustomerPassword = 4,
        HlrProvisoning = 5,
        Notification = 6,
        TCForgotPassWord = 7,
        Billing = 8,
        FlexUserActivation = 10,
        CustomerActivation = 11,//added by Liny,2010-04-25,for Ecofoon customer activation
        BenAPI = 12,
        BenReplyEmail = 13,//Michael on 11/5/17 for BenEmailInbox module.
        //add by winson 
        AlertEmailLevel1 = 1001,
        AlertEmailLevel2 = 1002,
        AlertEmailLevel3 = 1003,
        AlertEmailLevel4 = 1004,
        AlertEmailLevel5 = 1005,

        //added by liny,for EventSale Customer: WelcomeEmail & Confirmation Email
        //these itmes only use for WelcomeEmail & ConfirmationEmail of EventSaleCustomer
        BenEventSaleWelcome = 9000,
        BenEventSaleConfirm = 9001,
        //End

        //added by Liny,for Ben Job,
        //These items only use for the following email:
        BenOnlineWelcome = 1009,    //we send this kind of email to the customer which buy the sim card on ben website.
        BenPortinWelcome = 1010,    //we send this kind of email to the customer which buy the sim card on ben website and request port in his original number.
        BenOnshopWelcome = 9002,    //we send this kind of email to the customer which buy the sim card in store and register himself on ben website.
        BenJob2000Alert = 2000,     //we send this kind of email to the customer which Credits will expired 1 day after.
        BenJob2002Alert = 2002,     //we send this kind of email to the customer which Credits will expired 7 day after.
        BenJob2006Alert = 2006,     //we send this kind of email to the customer which has No Top Up since 6 weeks.
        BenJob2007Alert = 2007,     //we send this kind of email to the customer which has No Top Up since 12 weeks.
        BenJob4002Alert = 4002,     //we send this kind of email to the customer which has used Ben simcard 2 months on the day of his/her birthday.
        //End

        AxiomGeneric = 8200,
        AxiomOglobaAlert = 8201,
        RoamingSwitchTurnOnNotificationEvent = 8203,
        RoamingSwitchTurnOffNotificationEvent = 8204,
        PostpaidToPostpaidNotificationEvent = 8205,
        PrepaidToPostpaidNotificationEvent = 8206,
        PostpaidToPrepaidNotificationEvent = 8207,
        PostpaidCreditLimitIncreaseNotificationEvent = 8208,
        PostpaidCreditLimitDecreaseNotificationEvent = 8209,
        AlElmRegisterFailureEvent = 8211,
        PrepaidPromotionActivatedEvent = 8212,
        DataOnlyPromotionActivatedEvent = 8213,
        PostPaidPromotionActivatedEvent = 8214,
        DataOnlyActivedEvent = 8215,
        PostpaidActivedEvent = 8216,
        DataOnlyTopupNotificationEvent = 8217,
        FirstIMSIAttachEvent = 8218,
        RoamingWelcomeEvent = 8219,
        DataOnlyPromotionNotification = 8220,
    }

    public enum EmailStatus
    {
        Init = 0,
        HasSent = 1,
        Cancel = 2,
        Locked = 3
    }
    public enum PendingStatus
    {
        Pending = 1,
        Active = 2,
        Terminated = 3,
        Rejected = 4,
        PreActive = 5,
        Frozen = 6,
        HybridFrozen = 7,
        //add By Ben 2013-10-15 for DocumentValidateion
        Validating = 7
    }

    //Added By Gary 2011.6.3
    public enum CustomerStatus
    {
        Pending = 1,
        Active = 2,
        Terminated = 3,
        Rejected = 4,
        PreActive = 5,
        Regulatory = 6,
        Deleted =20
    }

    public enum RESOURCEType
    {
        ICCID = 1,
        VoucherCard = 2,
        MSISDN = 3,
        SIMCard = 4
    }

    public enum ReportSourceType
    {
        Resouce = 1,
        Voucher = 2,
        FraudulentResource = 3
    }

    public enum ReportType
    {
        InstalledOvertime,
        InstalledAdvanceNotice,
        ActiveAdvanceNotice,
        ActiveOvertime,
        InactiveAdvanceNotice,
        InactiveOvertime,
        DeactivatedAdvanceNotice,
        DeactivatedOvertime,
        InstalledAdvanceNoticePostpaid,
        InstalledOvertimePostpaid,
        ExpiredAdvanceNotice,
        ExpiredOvertime,
        VoucherCreatedOvertime,
        VoucherCreatedAdvanceNotice,
        VoucherActiveOvertime = 2004,
        VoucherActiveAdvanceNotice,
    }

    public enum FileType
    {
        Doc,
        Xls,
        Pdf,
        Txt,
        Csv,
        Mhtml,
        Xml,
        Rar,
        Zip,
        Xlsx,
        Exe,
        EXCEL
    }

    /// <summary>
    /// 这个枚举是用来表明是状态改变是手动改变还是自动扫描激活。
    /// </summary>
    public enum ActionType
    {
        Timeout = 0,
        Manual = 1,
    }

    public enum RemindSMSType
    {
        StatusChange = 1,
        AdvanceRemind = 2
    }

    /// <summary>
    /// 事件执行类型
    /// </summary>
    public enum OperateType
    {
        Timeout = 0,
        Manual = 1
    }

    public enum RechargeType
    {
        ALL = -1,

        /// <summary>
        /// Coupon recharge by WS
        /// </summary>
        RCP = 0,
        /// <summary>
        /// API	Balance adjustment
        /// Balance adjustment
        /// </summary>
        ASL = 1,
        /// <summary>
        /// Cash top up
        /// </summary>
        CASHTOPUP = 10001,

        /// <summary>
        /// API	Recharge annulment	ANR	10
        /// </summary>
        ANR = 10,
        /// <summary>
        /// atm topup cancellation
        /// </summary>
        ACM = 11,
        /// <summary>
        /// CustomerCare	Balance adjustment
        /// </summary>
        ACL = 13,

        /// <summary>
        /// CustomerCare Recharge annulment	ANR	10
        /// </summary>
        ACR = 14,

        /// <summary>
        /// Coupon recharge annulment
        /// </summary>
        ANC = 2,
        /// <summary>
        /// Coupon recharge by USSD
        /// </summary>
        RCU = 3,
        /// <summary>
        /// Coupon recharge by IVR
        /// </summary>
        RCI = 4,
        /// <summary>
        /// recharge by ATM
        /// </summary>
        ATM = 5,

        //==add by john 2011-03-08 start 
        /// <summary>
        /// ATM network 1 id. (la Caixa) 
        /// </summary>
        ATM1 = 51,
        /// <summary>
        /// ATM network 2 id. (Santander 4B) 
        /// </summary>
        ATM2 = 52,
        /// <summary>
        /// ATM network 3 id. (SERPEMA)- future use 
        /// </summary>
        ATM3 = 53,
        /// <summary>
        /// ATM network 3 id. (4B)-> future use 
        /// </summary>
        ATM4 = 54,
        //==add by John 2011-09-15 start
        /// <summary>
        /// ATM CECA
        /// </summary>
        ATM5 = 55,
        //==add by John 2011-09-15 end
        //==add by john 2011-03-08 end

        /// <summary>
        /// recharge by WEB
        /// </summary>
        WEB = 6,
        /// <summary>
        /// Coupon recharge by Client
        /// </summary>
        RCC = 7,
        /// <summary>
        /// Coupon recharge by SMS
        /// </summary>
        RCS = 8,
        /// <summary>
        /// CRM Call Center Credit Topup
        /// </summary>
        CCC = 9,

        //==add by john 2011-03-08 start 
        /// <summary>
        /// ATM network 1 id. (la Caixa) 
        /// </summary>
        CCC1 = 91,
        /// <summary>
        /// ATM network 2 id. (Santander 4B) 
        /// </summary>
        CCC2 = 92,
        /// <summary>
        /// ATM network 3 id. (SERPEMA)- future use 
        /// </summary>
        CCC3 = 93,
        /// <summary>
        /// ATM network 3 id. (CECA)-> future use 
        /// </summary>
        CCC4 = 94,
        //==add by John 2011-09-15 start
        /// <summary>
        /// ATM CECA
        /// </summary>
        CCC5 = 95,
        //==add by John 2011-09-15 end
        //==add by john 2011-03-08 end 

        /// <summary>
        /// Transfer credit topup
        /// </summary>
        TSC = 53001,

        /// <summary>
        /// Friends & Family Initial fee
        /// </summary>
        FFI = 21,
        /// <summary>
        /// Friends & Family Change fee
        /// </summary>
        FFC = 22,
        /// <summary>
        /// Friends & Family Monthly fee
        /// </summary>
        FFM = 23,
        /// <summary>
        /// Promotion rebate
        /// </summary>
        PromotionRebate = 24,
        /// <summary>
        /// PromotionDeduct
        /// </summary> 
        PromotionDeduct = 25,
        /// <summary>
        /// PrepayToPostpayTransfer
        /// </summary>
        PrepayToPostpayTransfer = 26,
        /// <summary>
        /// LBSEventCostCharge,add 20110704
        /// </summary>
        LbSEventCostCharge = 27,
        /// <summary>
        /// CustomerCare Deposit To Credit Limit when switch deposit off	DTCC	54001
        /// </summary>
        DTCC = 54001,
        /// <summary>
        /// SelfCare	Deposit To Credit Limit when switch deposit off	DTCS	54002
        /// </summary>
        DTCS = 54002,
        /// <summary>
        /// Engine	Deposit To Credit Limit when amount not enough for deposit	DTCE	54003
        /// </summary>
        DTCE = 54003,
        /// <summary>
        /// BCC Bank To Credit Limit	54004
        /// </summary>
        BCC = 54004,

        /// <summary>
        ///  Selfcare	Bank collection To Deposit	BCD	61001
        /// </summary>
        BCD = 61001,
        /// <summary>
        /// CustomerCare Deposit To Credit Limit when switch deposit off	DTCC	61002
        /// </summary>
        DDTCC = 61002,
        /// <summary>
        /// SelfCare	Deposit To Credit Limit when switch deposit off	DTCS	61003
        /// </summary>
        DDTCS = 61003,
        /// <summary>
        /// Engine	Deposit To Credit Limit when amount not enough for deposit	DTCE	61004
        /// </summary>
        DDTCE = 61004,
        /// <summary>
        /// Adjuest to bonus (by ExtraUsage)
        /// </summary>
        ATB = 71001,
        /// <summary>
        /// package' bonus to bonus
        /// </summary>
        PATB = 71002,
        /// <summary>
        /// promotion's bonus to bonus
        /// </summary>
        PRTB = 71003,
        /// <summary>
        /// voucher top up to bonus
        /// </summary>
        VTB = 71004,
        /// <summary>
        /// clear bonus when enddate is arrivde. by engine
        /// </summary>
        CLEARB = 72001,
        /// <summary>
        /// Registration bonus for BEN
        /// Balance adjustment
        /// </summary>
        ASLP = 72002,

        /// <summary>
        /// Topup Bonus Type; add by james; 2010-08-13
        /// </summary>
        TBT = 41,

        /// <summary>
        /// TOPUP BONUS CANCEL TYPE
        /// </summary>
        TBC = 42,

        ///// <summary>
        ///// Bank collection To Deposit
        ///// </summary>
        //BCD = 51,
        ///// <summary>
        ///// Deposit To Credit Limit
        ///// </summary>
        //DTC = 52,

        /// <summary>
        /// Unknown
        /// </summary>
        UNKNOWN = 101,

        /// <summary>
        /// Transfer Out/In
        /// </summary>
        TRSO = 5101,
        TRSI = 5102,
        LBTRANSF = 5103,//add by sam 2014-10-24 Local balance transfer serive fee
        INTELTRS = 5104,//add by sam 2014-10-28 international   balance transfer 
        INTELTRSF = 5105,//add by sam 2014-10-28 international   balance transfer serive fee
        /// <summary>
        /// Ben API Top Type
        /// </summary>
        REC = 8001,
        MIN = 8002,
        RELOAD = 8003,
        //RELOAD_SMS =8003
        IBB = 8004,  //added by Liny,2010-09-14,for ben 5 euro bonus

        TRANSFER_INETBOSS = 151,
        TFD = 8006,
        RBT = 8005,
        ECA = 131,
    }

    /// <summary>
    /// 转帐类型 
    /// </summary>
    public enum CreditTransferType
    {
        None = 0,
        CreditTransfer = 1,
        AutoTransfer = 2
    }

    public enum JobType
    {
        BalanceCheck = 1,
        Normal = 2
    }

    public enum SIMCardEventCode : int
    {
        O_bind_icc = 1001,                              //绑定MSISDN
        O_provisioning = 1003,                          //安装
        Installed_to_expired_timeout = 1004,            //超时过期
        Installed_timeout_sendlist_to_mvno = 1005,      //过期前发送报表
        FirstCall = 1006,                               //首次使用时激活用户
        O_opening_icc = 1007,                           //起用MSISDN
        Active_to_inactive_timeout = 1009,              //超时过期
        Active_balance_exhaustion = 1010,               //余额耗尽
        O_cooldown_icc = 1014,                          //冷却MSISDN
        O_activated = 1015,                             //激活RESOUCE
        O_reinstalled = 1016,                           //重置RESOUCE
        Inactive_to_deactivated_timeout = 1017,         //超时过期
        Deactive_to_expired_timeout = 1020,             //超时过期
        O_frozen = 1022,                                //冻结
        O_disfrozen_for_fraud = 1023,                   //解冻因欺诈而冻结的用户
        O_disfrozen_for_obstruction = 1024,             //解冻因充值而阻塞的用户
        Postpaid_convert_to_init_timeout = 1025,        //转换超时回收
        O_postpaid_convert_to_installed = 1026,         //后付费转为预付费INSTALLED状态
        O_postpaid_convert_to_active = 1027,            //后付费转为预付费ACTIVE状态
        O_postpaid_convert_to_inactive = 1028,          //后付费转为预付费INACTIVE状态
        O_postpaid_convert_to_deactivated = 1029,       //后付费转为预付费DEACTIVATED状态
        Prepaid_convert_to_init_timeout = 1030,         //转换超时回收
        O_prepaid_convert_installed = 1031,             //预付费转为后付费INSTALLED状态
        O_prepaid_convert_active = 1032,                //后付费转为后付费ACTIVE状态        
        Expired_to_init_timeout = 1033,                 //超时回收
        Expired_timeout_sendlist_to_mvno = 1034,        //发送将要回收的RESOUCE报表
        O_expired = 1035,                               //终止服务
        O_balance_adjustment = 1036,                    //余额调整
        O_balance_adjustment_to_inactive = 1037,        //余额调整到INACTIVE状态
        O_balance_adjustment_to_deactivated = 1038,     //余额调整到DEACTIVATED状态
        O_recharge_plus = 1039,                         //充值
        O_recharge_minus = 1040,                        //扣费
        O_recycle = 1041,                               //回收
        O_locked_icc = 1044,                            //锁定
        O_locked = 1045,                                //锁定
        O_postpaid_register_submit = 1046,              //网上注册通过或不通过
        O_preactive_register_approve_success = 1048,    //网上注册通过或不通过
        O_postactive_register_approve_success = 1049,   //网上注册通过，将MSISDN->INSTALLED,ICCID->INSTALLED，RESOURCE->INSTALLED
        O_preactive_register_approve_failed = 1051,     //网上注册通过
        //O_single_recycle_icc = 1052                   //单个回收MSISDN
        O_converted_through_converted = 1053,           //通过CONVERTED状态转换
        O_prepaid_converted_directly = 1054,            //不通过CONVERTED状态转换
        O_postpaid_converted_directly = 1055,           //不通过CONVERTED状态转换
        Portin_preactivate = 1056,                      //Port in 预激活
        FraudulentTopUpPerDay = 1070,                         //号码欺诈充值
        FraudulentTopUpPerHour = 1071,                          //号码欺诈充值
        O_provisioning_for_special_preactive_customer = 1074,        //resource:Init->Init
        O_activation_for_special_preactive_customer = 1075,       //resource:Init->Installed
        O_force_to_active = 1076,//force to active
    }

    public enum MSISDNEventCode : int
    {
        O_bind_msisdn = 1001,                           //绑定MSISDN
        O_postactive_register_submit = 1002,            //网上注册分配MSISDN，RESOURCE状态为INIT
        O_provisioning = 1003,                          //安装
        Installed_to_expired_timeout = 1004,            //超时过期
        Installed_timeout_sendlist_to_mvno = 1005,      //过期前发送报表
        FirstCall = 1006,                               //首次使用时激活用户
        O_opening_msisdn = 1007,                        //起用MSISDN
        O_postactive_register_approve_failed = 1008,    //网上注册不通过，将MSISDN->INIT，删除 resource
        Active_to_inactive_timeout = 1009,              //超时过期
        Balance_exhaustion = 1010,                      //余额耗尽
        Balance_lessthan_special_sendsms = 1011,        //余额小于定额
        Active_before_inactive_timeout_sendsms = 1012,  //发送将过期提醒短信
        Active_before_inactive_timeout_sendlist = 1013, //发送将过期报表
        O_cooldown_msisdn = 1014,                       //冷却MSISDN
        O_activated = 1015,                             //激活RESOUCE
        O_reinstalled = 1016,                           //重置RESOUCE
        Inactive_to_deactivated_timeout = 1017,         //超时过期
        Inactive_before_timeout_sendlist_to_mvno = 1018,       //发送将要过期的RESOUCE报表
        Inactive_before_timeout_sendsms = 1019,                //发送将过期短信
        Deactive_to_expired_timeout = 1020,             //超时过期
        Deactive_before_timeout_sendlist_to_mvno = 1021,       //发送将要过期的RESOUCE报表
        O_frozen = 1022,                                //冻结
        O_disfrozen_for_fraud = 1023,                   //解冻因欺诈而冻结的用户
        O_disfrozen_for_obstruct = 1024,                //解冻因充值而阻塞的用户
        Postpaid_convert_to_init_timeout = 1025,        //转换超时回收
        O_postpaid_convert_to_installed = 1026,         //后付费转为预付费INSTALLED状态
        O_postpaid_convert_to_active = 1027,            //后付费转为预付费ACTIVE状态
        O_postpaid_convert_to_inactive = 1028,          //后付费转为预付费INACTIVE状态
        O_postpaid_convert_to_deactivated = 1029,       //后付费转为预付费DEACTIVATED状态
        Prepaid_convert_to_init_timeout = 1030,         //转换超时回收
        O_prepaid_convert_installed = 1031,             //预付费转为后付费INSTALLED状态
        O_prepaid_convert_active = 1032,                //后付费转为后付费ACTIVE状态        
        Expired_to_init_timeout = 1033,                 //超时回收
        Expired_timeout_sendlist_to_mvno = 1034,        //发送将要回收的RESOUCE报表
        O_expired = 1035,                               //终止服务
        O_balance_adjustment = 1036,                    //余额调整
        O_balance_adjustment_to_inactive = 1037,        //余额调整到INACTIVE状态
        O_balance_adjustment_to_deactivated = 1038,     //余额调整到DEACTIVATED状态
        O_recharge_plus = 1039,                         //充值
        O_recharge_minus = 1040,                        //扣费
        O_recycle = 1041,                               //回收
        O_portin = 1042,                                //带号移入
        O_portout = 1043,                               //带号移出
        O_locked_msisdn = 1044,                         //锁定
        O_locked = 1045,                                //锁定
        O_preactive_register_submit = 1046,             //网上注册
        O_reserved_msisdn = 1047,
        O_preactive_register_approve_success = 1048,    //网上注册通过
        O_postactive_register_approve_success = 1049,   //网上注册通过，将MSISDN->INSTALLED,ICCID->INSTALLED，RESOURCE->INSTALLED
        Deactive_before_timeout_sendsms = 1050,
        O_preactive_register_approve_failed = 1051,     //网上注册通过
        //O_single_recycle_msisdn = 1052                //单个回收MSISDN
        O_converted_through_converted = 1053,          //通过CONVERTED状态转换
        O_prepaid_converted_directly = 1054,            //不通过CONVERTED状态转换
        O_postpaid_converted_directly = 1055,           //不通过CONVERTED状态转换
        Portin_preactivate = 1056,                      //Port in 预激活
        Active_before_inactive_timeout_sendsms_second = 1057,  //发送将过期提醒短信
        Inactive_before_timeout_sendsms_second = 1058,         //发送将过期短信
        Deactive_before_timeout_sendsms_second = 1059,        //发送将过期短信
        PaymentCard_topup_send_remind_sms = 1060,
        PaymentCard_Cancellation_topup_send_remind_sms = 1061,//取消充值发送短信
        //add for mexico
        NextTopUp_NoticeSMS = 1062,
        FraudulentTopUpPerDay = 1070,                         //号码欺诈充值
        FraudulentTopUpPerHour = 1071,                   //号码欺诈充值
        O_sim_preactive_lock_msisdn = 1072,              //预激活选号锁定
        O_sim_preactive_release_msisdn = 1073,                //预激活生成卡释放号码
        O_provisioning_for_special_preactive_customer = 1074,        //resource:Init->Init
        O_activation_for_special_preactive_customer = 1075,        //resource:Init->Installed
        Frozen_R2 = 1080,//Frozen->R2
        R2_R3 = 1081,//R2->R3
        R3_Recovery = 1020,//R3->Recovery
        R0_Frozen = 1082,
        Frozen_Deactive = 1017,
        Deactive_Init = 1033,
        O_ChangeMsisdn = 1083,
        O_ChangeSimCard = 1084,
        //added by neil at 2014/3/19
        Active_After_Register = 1085,    //注册后立即激活
            O_force_to_active = 1076,//force to active
    }

    public enum VoucherEventCode : int
    {
        Created_to_expired_timeout = 1001,    //超时过期
        Created_before_expired_send_list = 1002, //发送提前报表
        Active_to_expired_timeout = 1003,     //超时过期
        Active_before_expired_send_list = 1004, //发送提前报表
        Used_to_init_timeout = 1005,          //超时过期
        Expired_to_init_timeout = 1006,       //超时过期
        Rechaging_to_expired_timeout = 1007,  //超时过期
        Annulled_to_expired_timeout = 1008,   //超时过期
        Fraudulent_to_expired_timeout = 1009, //超时过期  
        O_create = 1010,     //包括INIT->CREATED,   FRAUDULENT->CREATED
        O_active = 1011,     //包括CREATE->ACTIVE,  FRAUDULENT->ACTIVE,   RECHARGING->ACTIVE,   ANNULLED->ACTIVE
        O_used = 1012,       //包括ACTIVE->USED,RECHARGING->USED
        O_using = 1013,      //包括ACTIVE->USING
        O_recharging = 1014, //包括ACTIVE->RECHARGING
        O_annulled = 1015,   //包括ACTIVE->ANNULLED
        O_fraudulent = 1016, //包括CREATED->FRAUDULENT,     ACTIVE->FRAUDULENT
        O_recycle = 1017,    //包括USED->INIT,  EXPIRED->INIT
        O_reactive = 1018    //已经使用，用于取消充值操作，要还原充值卡状态
    }

    public enum PaymentType
    {
        Unkown = -1,
        Postpayment = 1,
        Prepayment = 2,
        Hybird = 3
    }

    /// <summary>
    /// <para>SIM Card Status</para>
    /// <para>crm_dealers_settings.itemseq=14013</para>
    /// <para>Remark:This data in db is not added by bright.</para>
    /// <para>Bright,2008/07/22</para>
    /// </summary>
    public enum SIMCardStatus
    {
        Active = 1,
        Init = 8,
        Installed = 9,
        Inactive = 10,
        Deactive = 2,
        Frozen = 11,
        Expired = 3,
        Reserved = 4,
        Locked = 5,
        R3 = 2,
        R2 = 13,
        NotInAuc = 0
    }
    /// <summary>
    /// NPM Status
    /// </summary>
    public enum MSISDNStatus
    {
        Active = 1,
        Init = 8,
        Installed = 9,
        Inactive = 10,
        Deactive = 2,
        Frozen = 11,
        Converted = 12,
        PortedIn = 7,
        PortedOut = 6,
        Expired = 3,
        Reserved = 4,
        Locked = 5,
        Deleted = 20,
        Recovery = 3,
        R2 = 13,
        FrozenMex = 14,
        R3 = 2
    }
    /// <summary>
    /// 用于表示crm_customers_resourcemb.statusid(L)
    /// Bright,2008/10/14
    /// 由于resourcemb的statusid需要整理，所以在此先做修改，以满足VFAPI开发的需要。
    /// Bright,2008/10/15
    /// 重新做了更新，并且从ETALKAPI迁移至此
    /// Bright,2008/10/30
    /// 增加了12
    /// jeffrey 2008/11/29
    /// </summary>
    public enum ResourceStatus
    {
        Active = 1,
        R1 = 1,
        Init = 8,
        Installed = 9,
        Inactive = 10,
        R0 = 10,
        Deactive = 2,
        R2 = 13,
        Frozen = 11,
        Converted = 12,
        R3 = 2,
        Expired = 3,
        Recovery = 3,
        Locked = 5,
        /// <summary>
        /// 专门为回收时使用。用来表示RESOURCEMB已经被删除
        /// </summary>
        Deleted = 20,
        FrozenMex = 14,
        //adde by neil at  for Elektra status mapping
        S1 = 10,
        S2 = 13,
        S3 = 2,
        S4 = 20,//equals Deleted
        Reserved = 15,
        Deactived
    }

    public enum ParameterType
    {
        AllSingleParameter = 1,
        SimcardInstance = 2,
        MSISDNInstance = 3,
        ResourceMBInstance = 4,
        VoucherInstance = 5
    }

    public enum EventType
    {
        PreSimcard = 1,
        Voucher = 2,
        PreMsisdn = 3,
        PostSimcard = 4,
        PostMsisdn = 5,
        HybirdMsisdn = 6,
        HybirdSimcard = 7,
        LCSetting = 8,
    }

    /// <summary>
    /// <para>Service Type</para>
    /// <para>Bright,2008/07/22</para>
    /// </summary>
    public enum ServiceType
    {
        CarrierPreSelect = 1000,
        VoIP = 2000,
        Mobile = 3000,
        WholeSale = 4000,
        PremiumRate = 5000,
        FreePhone = 6000
    }

    public enum InvoiceType : int
    {
        Invoice = 0,
        Statement = 1,
        CreditInvoice = 2,
        Upload = 3
    }


    /// <summary>
    /// 用以表示consolidation导入的状态
    /// </summary>
    public enum ConsolidationStatus
    {
        Pending = 0,
        Importing = 1,
        Complete = 2,
        Failed = 3
    }
    public enum PortabilityDirection
    {
        Outgoing = 0,
        Incoming = 1
    }

    /// <summary>
    /// Category Type
    /// </summary>
    [Serializable]
    public enum CategoryType
    {
        Gold = 1,
        Silver = 2,
        Bronze = 3,
        Normal = 4, 
        DataOnly = 5,
        Diamond = 6,
        Platinum = 7,
        Unclassified = 8
    }
    /// <summary>
    /// 卡激活类型
    /// </summary>
    public enum ActivateType
    {
        Preactive = 1,
        Postactive = 2
    }

    [Serializable]
    public enum CurrencyShort
    {
        /// <summary>
        /// EUR
        /// </summary>
        E = 978
    }

    [Serializable]
    public enum OperationResult
    {
        /// <summary>
        /// Correct
        /// </summary>
        Correct = 0,
        /// <summary>
        /// Validation not overpass
        /// </summary>
        ErrorDataValidation = 1,
        /// <summary>
        /// Authentication validation not overpass
        /// </summary>
        ErrorAutenticacion = 2,
        /// <summary>
        /// Request Processed
        /// </summary>
        RequestProcessed = 3,
        /// <summary>
        /// Logical Error
        /// </summary>
        ErrorLogic = 4,
        /// <summary>
        /// Internal Error
        /// </summary>
        ErrorInternal = 5
    }

    [Serializable]
    public enum OperationStatus
    {
        /// <summary>
        /// Completed.
        /// </summary>
        CO = 1,
        /// <summary>
        /// In execution
        /// </summary>
        EJ = 2,
        /// <summary>
        /// Canceled/Finished
        /// </summary>
        AN = 3,
        /// <summary>
        ///  Annultment Request. Temporary state previous to moving to “AN” state.
        /// </summary>
        SA = 4,
        /// <summary>
        /// Retained: Intermediate state  where there have been errors of provisioning.
        /// The passage through this state does not imply the request may not end up in state “CO”.
        /// </summary>
        RE = 5
    }

    [Serializable]
    public enum SubServiceType
    {
        MobileVoice = 3001,
        MobileSms = 3002,
        MobileData = 3003,
        MobileMms = 3004,
        MobileVideo = 3005,
        MobileDID = 3006,
        MobileCallback = 3007,
        MobileRoaming = 3101,
        Wholesale = 4001,
        CarrierPreSelect = 1003,
        PremiumRate = 5001,
        Mobile = 5010,
        Geographical = 5020,
        FreePhone = 6001,
        MediaPhone = 2001,
        /// <summary>
        /// The subservice in table RM_PROMOTIONPLAN_TOPUP_BASED_PARAMETERS.SubserviceTypeid
        /// </summary>
        FriendAndFamilyPromotionRenew = 100,
    }

    /// <summary>
    /// Result of import file
    /// Gary.Xie
    /// 2009-09-01
    /// </summary>
    public enum ImportFileResult : int
    {
        SUCCEED_ALL = 1,
        SUCCEED_PART = 0,
        ERROR_ALL = -1,
        ERROR_MASTER = -2,
        ERROR_DETAILS = -3,
        ERROR_COUNT = -4,
    }

    public enum ImportFileLogCode : int
    {
        SUCCEED = 1,
        ERROR_EXISTS_ICC_IN_SIMCARD = -1,
        ERROR_EXISTS_ICC_IN_RESOURCE = -2,
        ERROR_EXISTS_IMIS_IN_RESOURCE = -3,
        ERROR_EXISTS_MSISDN_IN_RESOURCE = -4,
        ERROR_IMPORTING_ERROR = -5
    }

    /// <summary>
    /// 
    /// </summary>
    public enum SIMCardsOrderStatus : int
    {
        Draft = 0,
        Submitted = 1,
        Requesting = 2,
        Sended = 3
    }

    public enum PreactivatedSimcardTypes : int
    {
        Initial = 0,
        Preactivated = 1
    }

    public enum SIMCardBaseObjectName
    {
        SIMCardOrder,
        SIMCard,
        DealerICC,
        DealerIMSI
    }

    /// <summary>
    /// <para>SIM Card 's Activate Type(enum)</para>
    /// <para>crm_dealers_settings.itemseq=17010</para>
    /// <para>Bright,2008/07/21</para>
    /// </summary>
    public enum SIMCardActivateType : int
    {
        Pre = 1,
        Post = 2
    }


    /// <summary>
    /// <para>Item Seq for dealers settings data</para>
    /// <para>这个数据要不断的更新维护</para>
    /// <para>Bright,2008/07/22</para>
    /// </summary>
    public enum ItemSeq
    {
        Department = 50,
        CalculationType = 2400,
        DealerType4Role = 8000,
        Currency = 11004,
        ServiceType = 12006,
        SubServiceType = 12007,
        PaymentType = 12008,
        SubscriptionCycle = 12009,
        BundleUnit = 12017,
        ExtraItem4Bundle = 12018,
        SIMCardsOrderStatus = 12100,
        SIMCardsType = 12300,
        SIMCardsActivateType = 17010,
        AlgorithmName = 17500,
        ManufacturerId = 17501,
        RateplanCategory = 25000,
        Country = 11002,
        InvoiceTemplates = 12012,

        Province = 25001,
        Nationality = 11002,
        IDType = 19011,
        CalculationMethod = 13005,
        ResourceTcStatus = 4500,
        WhiteList = 3200,
        BlackList = 3300,
        TopupType = 25003,
        PendingStatus = 19004,

        FFFeeType = 25101,
        FFChargeType = 25102,
        FFTrafficType = 25103,
        SIMCARDSTATUS = 14013,
        RESOURCESTATUS = 14014,
        CALLFORWARDTYPE = 25202,

        PromotionCategorys = 25201,

        //Mahome, for promotion 2009-7-28
        PromotionActivationType = 25203,
        PromotionBusinessType = 25204,
        PromotionCalculateType = 25205,
        PromotionCalculateUnit = 25206,
        TopUpPresentRuleId = 25207,
        PromotionReturnMethod = 25208,
        PromotionCycleType = 25209,
        PromotionRebateValueUnit = 25210,

        //Cheney.Chen 2009-7-31
        SUBSCRIPTIONPERIODUNIT = 25212,
        PromotionValidTime = 25213,
        PromotionDiscountMethodId = 25214,
        IntroducerStatus = 25218,

        //James.Zhan 2009-8-6
        PromotionLimitUnit = 25215,
        NumberCategory = 25216,
        PromotionPeriodic = 25219,   //James.Zhan 2009-08-26
    }

    public enum FFFeeType : int
    {
        InitialFee = 1,
        ChangeFee = 2,
        MonthlyFee = 3,
        All = -1

    }

    public enum FFChargeType
    {
        PerCategory = 1,
        Fixed = 2,
        PerNumber = 3
    }

    public enum FFTrafficType
    {
        None = -1,
        //All = 0,
        International = 1,
        SuperOnNet = 2,
        Mobile = 3,
        Fixed = 4,
        DID = 5,
        //modified by damon 2012-07-16
        //Company = 6,
        //PortedIn = 7
        //end by damon
    }

    public enum FFHistoryActionType
    {
        Signup = 0,
        Changes = 1,
        Resign = 2
    }

    public enum ETMessageBoxIcon : int
    {
        None = 0,
        //
        // Summary:
        //     The message box contains a symbol consisting of white X in a circle with
        //     a red background.
        Error = 16,
        //
        // Summary:
        //     The message box contains a symbol consisting of a white X in a circle with
        //     a red background.
        Hand = 16,
        //
        // Summary:
        //     The message box contains a symbol consisting of white X in a circle with
        //     a red background.
        Stop = 16,
        //
        // Summary:
        //     The message box contains a symbol consisting of a question mark in a circle.
        Question = 32,
        //
        // Summary:
        //     The message box contains a symbol consisting of an exclamation point in a
        //     triangle with a yellow background.
        Exclamation = 48,
        //
        // Summary:
        //     The message box contains a symbol consisting of an exclamation point in a
        //     triangle with a yellow background.
        Warning = 48,
        //
        // Summary:
        //     The message box contains a symbol consisting of a lowercase letter i in a
        //     circle.
        Information = 64,
        //
        // Summary:
        //     The message box contains a symbol consisting of a lowercase letter i in a
        //     circle.
        Asterisk = 64,
    }

    /// <summary>
    /// 这个枚举类型与ETalkAPI/Objects/Enums中的eRechargeType类型相关联
    /// Bright,2009/05/23
    /// 增加了两种状态：PromotionRebate 与PromotionDeduct。这是应用于promotion;James.zhan;2009-09-21
    /// </summary>
    public enum TopupType
    {
        /// <summary>
        /// Friends & Family Initial fee
        /// </summary>
        FFI = 21,
        /// <summary>
        /// Friends & Family Change fee
        /// </summary>
        FFC = 22,
        /// <summary>
        /// Friends & Family Monthly fee
        /// </summary>
        FFM = 23,
        /// <summary>
        /// Promotion rebate
        /// </summary>
        PromotionRebate = 24,
        /// <summary>
        /// PromotionDeduct
        /// </summary> 
        PromotionDeduct = 25,


        //add by camel 2009.12.14
        //增加ff各个类别的开通和修改费

        FFInternationalI = 26,
        FFInternationalC = 27,

        FFDIDI = 28,
        FFDIDC = 29,

        FFFixedI = 30,
        FFFixedC = 31,

        FFMobileI = 32,
        FFMobileC = 33,

        FFSuperOnNetI = 34,
        FFSuperOnNetC = 35
    }
    

    public enum LifecycleResponseCode : int
    {
        Success = 0,                    //Lifecycle execute success
        Failed = 1,                     //Lifecycle execute failed
        ProvisioningSynchronous = 2,    //Lifecycle succuce and Provision synchronous
        ProvisioningAsync = 3,          //Lifecycle succuce and Provision Async
        ProvisioningFailed = 4,         //Lifecycle succuce but Provision failed
    }

    public enum CustomerIdType : int
    {
        Passport = 1,
        DriversLicense = 2,
        IdentityCard = 3,
        NIF = 4,
        NIE = 5,
        CIF = 6,
        Visa = 7
    }

    public enum DictionaryType
    {
        Country = 11002,
        Language = 12001,
        Title = 11000,
        Gender = 11001,
        TrafficType = 12002,
        PaymentMethod = 12003,
        BillMethod = 12004,
        CPSValue = 13000,
        ResourceStatus = 13004,
        Service = 12006,
        SubServiceType = 12007,
        Template = 12012,
        CostId = 18000,
        CustomerType = 12000,
        CallBarringSubsOption = 17003,
        CalMethod = 13005,
        CPSStatus = 13001,
        VoucherStatus = 87000,

        Currency = 11004,
        //ari.he
        DoubleTriplePromotionMethod = 12301,
        PromotionType = 252302,
        FFFeeType = 25101,
        FFChargeType = 25102,
        FFTrafficType = 25103,
        //SIMCARDSTATUS = 14013,
        //RESOURCESTATUS = 14014,
        //CALLFORWARDTYPE = 25202, 
        SubscriptionPeriodUnit = 25212,
        PromotionCategorys = 25201,
        PromotionActivationType = 25203,
        PromotionBusinessType = 25204,
        PromotionCalculateType = 25205,
        PromotionCalculateUnit = 25206,
        TopUpPresentRuleId = 25207,
        PromotionReturnMethod = 25208,
        PromotionCycleType = 25209,
        PromotionRebateValueUnit = 25210,
        PromotionValidTime = 25213,
        PromotionDiscountMethodId = 25214,
        IntroducerStatus = 25218,
        PromotionLimitUnit = 25215,
        NumberCategory = 25216,
        PromotionPeriodic = 25219,
        TaxPlan = 12005,
        BillingEntity = 25310,
        CallType = 19000,
        TimeCategory = 1001,
        UnitCategory = 1002,
        Nationality = 11002,
        Operator = 26000,
        CustomerCategory = 260000, //added by Ego.20131230
        PromotionWallet = 1003, //added by neil at 2014/1/17
    }

    [Serializable]
    public enum ResultType : int
    {
        Correct = 0,
        DataValidationFailure = 1,
        RequestProcessed = 2,//待处理
        LogicError = 3,
        InternalError = 4,
        AuthenticationValidationFailure = 5,
        ConnectionError = 6,
        RetryRequireError = 7,
        NotEnoughBalance = 101,
        NotEnoughValidatyForSuperSaving = 102

    }

    public enum CustomerAdvanceSearchItem : int
    {
        CustomerID = 0,
        Company = 1,
        ContactPerson = 2,
        TelephoneNO = 3,
        InvoiceID = 4,
        ICC = 5,
        PendingStatus = 6,
        Msisdn = 7,
        Address = 8,
        HomeNO = 9,
        Zipcode = 10,
        City = 11,
        IDType = 12,
        IDNumber = 13
    }

    public enum PCCustomerType : int
    {
        Common = 0,
        Parent = 1,
        Child = 2
    }

    public enum CreditCheckType
    {
        IDCheck = 1,
        Preventel = 2,
        CreditCheck = 3,
        EasyCheck = 4
    }

    public enum CreditCheckedStatus
    {
        Passed = 1,
        Failed = 2,
        Pending = 3,
        Others = 4
    }

    public enum RatePlanDetailCatetory
    {
        Normal = 100,
        Premium = 101,
        Roaming = 102,
        CallForward = 103,
        OnNet = 104,
        SuperOnNet = 105
    }

    public enum UserNameLoginType : int
    {
        CustomerID = 1,
        TelephoneNumber = 2
    }
    public enum LogSource : int
    {
        Client = 0,
        WebSite = 1,
        SelfcareAPI = 2,
        BenJob = 3,
        ATOSAPI = 4,
        DREAPI = 5,
        LIQUIXAPI = 6,
        PORTINGXS = 7,
        TCAPI = 8,
        Server = 9,
        LifecycleEngine = 10

    }
    public enum LogBusiness : int
    {
        NPM = 1,
        SIMCard,
        Customer,
        VoucherCard,
        MNP,
        COIN,
        Dealer,
        SelfcareAPI,
        Hlr,
        Payment,
        Promotion,
        Rateplan,
        Bundle,
        Package,
        TroubleTicket,
        LoyaltyPoints,
        //add next 9 line by ari.he 2011-06-20 ,new log Business
        TimeCategory,
        UnitCategory,
        CallType,
        CallDirection,
        RoamingOperator,
        RoamingZone,
        InternationalZone,
        DateCategory,
        IPRange,
        EUPSConfig,
        PRSIncumbent,
        PRSCustomer,
        SRERelate,
        //end, ari.he 2011-06-20
        //2011-06-27 winson  add for operation log 
        SelfCare,
        AlertRule,//2011-7-1 winson add for alert rule operation log 
        ///2011-06-27 end  winson 
        Commission,
        UsageWSCategory,
    }

    public enum LogOperator : int
    {
        //NPM
        ChangeNumberStatus = 1,
        // Customer care -------------------------
        CustomerRegister = 6,
        CustomerUpdating = 36,
        CustomerDeletion = 37,
        MoveCustomer = 42,
        CopyCustomer = 39,
        ChangeCustomerStatus = 38,
        ChangeCreditLimit = 43,
        ResetUnbilledBalance = 44,
        SwitchSIMCard = 4,
        RegistrationResource = 26,
        SwitchNumber = 3,
        DeleteResource = 47,
        FrozenResource = 48,
        DisfrozenReource = 49,
        AcitvateResource = 50,
        ChangeResourceStatus = 2,
        AddPackage = 51,
        SwitchPackage = 5,
        EditPackage = 45,
        HLRModification = 7,
        ApplyPromotion = 8,
        AnnullPromotion = 9,
        Topup = 16,
        AnnullTopup = 17,
        //Add 2010-08-09
        BlockSIMCARD = 52,
        UNBlockSIMCARD = 53,
        MigrationFromPrepaymentToPostpayment = 54,
        MigrationFromPostpaymentToPrepayment = 55,
        BalanceAdjustment = 56,
        AlterationCallForward = 57,
        ModifySupplementaryServices = 58,
        ModifyRoamingStatus = 59,
        ModifyVoicemailStatus = 60,
        ModifyCustomerBankInfo = 61,
        AddCustomerBankInfo = 62,
        ModifyCustomerAddressInfo = 63,
        ModifyAllCustomerFFNumbers = 64,
        AddCustomerFFNumbers = 65,
        ModifyCustomerFFNumbers = 66,
        CustomerFFNumbersDeletion = 67,
        AddDID = 68,
        ModifyDID = 69,
        DeleteDID = 70,


        //下面这些不在文档，暂时保留 Brain
        //start---------------------------------------------------------------  
        ChargeMonthlyFee = 10,
        DailyDeductReset = 11,
        ResetCreditLimit = 12,
        ChargeUnpainFee = 13,
        DeactivePromotion = 14,
        Rebate = 15,
        CommissionVoucherCard = 22,
        AssignedVoucherCard = 23,
        ChangePRSCustomer = 24,
        ChangeCPSCustomer = 25,
        //end----------------------------------------------------------------

        //Voucher Card
        GenerateVoucherCard = 18,
        SwitchVoucherCardStatus = 19,

        //Coin
        PortedIn = 20,
        PortedOut = 21,

        //Dealer--------------------------------
        AddDealer = 27,
        ModifyDealer = 28,
        DeleteDealer = 29,
        CopyDealer = 30,
        CopyMVNO = 31,
        HideDealer = 32,
        UnhideDealer = 33,
        MoveDealer = 34,

        //MNP
        PortIN = 40,
        PortOut = 41,

        //下面这些不在文档，暂时保留 Brain
        //Start--------------------------------
        HlrCounter = 35,
        //end---------------------------------

        //Rateplan
        AddRateplan = 71,
        ModifyRateplan = 72,
        DeleteRateplan = 73,
        CopyRateplan = 74,

        //Bundle
        AddBundle = 75,
        ModifyBundle = 76,
        DeleteBundle = 77,

        //Package
        AddDealerPackage = 78,
        ModifyDealerPackage = 79,
        DeleteDealerPackage = 80,

        #region LoyaltyPoints
        UpdateLoyaltyPoints = 90,
        #endregion
        //add by devid 20101110根据vf2.5新LOG模块整理的日志枚举，请不要删除
        GenerateNumber,
        ModifyNumberDealer,
        ModifyNumberShareType,
        ModifyNumberStatus,
        ModifyNumberCategory,
        AddNewResource,
        ChangePackage,
        ChangeCustomerInformation,
        NewSIMCARDOrder,
        ModifySIMCARDOrder,
        DeleteSIMCARDOrder,
        ImportSIMCARD,
        AssignSIMCARD,
        ReAssignSIMCARD,
        PreActiveSIMCARD,
        AddICC,
        ModidfyICC,
        DeleteICC,
        AddIMSI,
        ModidfyIMSI,
        DeleteIMSI,
        //add by devid 20110307
        AddTroubleTicket,
        //upload voucher card 20110328
        UploadVoucherCard,
        ExportVoucherCard,
        //customer care部分
        //VFRegistrationCustomer = 100,
        //VFDeleteCustomer = 101,
        //VFMoveCustomer = 102,
        //VFCopyCustomer = 103,
        //VFChangeCustomerInformation = 104,
        //VFChangeCustomerPendingStatus = 105,
        //VFChangeCreditLimit = 106,
        //VFResetUnbilledBalance = 107,
        //VFSwitchSIMCard = 108,
        //VFSwitchNumber = 109,
        //VFAddNewResource = 110,
        //VFDeleteResource = 111,
        //VFFrozenResource = 112,
        //VFDisfrozenReource = 113,
        //VFChangeResourceStatus = 114,
        //VFChangePackageButton = 115,
        //VFChangePackage = 116,
        //VFAddCustomerBankInfo = 117,
        ////simcard 部分
        //VFNewSIMCARDOrder = 118,
        //VFModifySIMCARDOrder = 119,
        //VFDeleteSIMCARDOrder = 120,
        //VFImportSIMCARD = 121,
        //VFAssignSIMCARD = 122,
        //VFReAssignSIMCARD = 123,
        //VFPreActiveSIMCARD = 124,
        //VFAddICC = 125,
        //VFModidfyICC = 126,
        //VFDeleteICC = 127,
        //VFAddIMSI = 128,
        //VFModidfyIMSI = 129,
        //VFDeleteIMSI = 130,
        ////npm部分
        //VFGenerateNumber = 131,
        //VFModifyNumberDealer = 132,
        //VFModifyNumberShareType = 133,
        //VFModifyNumberStatus = 134,
        //VFModifyNumberCategory = 135
        //end 根据vf2.5新LOG模块整理的日志枚举，请不要删除
        #region LoyaltyPoints
        Ciot,
        BtScanner,
        #endregion
        //HLR provision
        AddMVNOProvision,
        ModifyMVNOProvision,
        DeleteMVNOProvision,
        //add by devid for vouchercard 20110614
        VoucherRangeActivation,
        //add next 44 lines by ari.he 2011-06-20 ,new log operation   
        AddRatePlanPlanDetail,
        ModifyRatePlanDetail,
        DeleteRatePlanDetail,
        TeminateRateplanDetail,
        ImportRatePlanDetail,
        AddTimeCategory,
        ModifyTimeCategory,
        DeleteTimeCategory,
        AddTimeCategaryDetail,
        ModifyTimeCategoryDetail,
        DeleteTimeCategoryDetail,
        AddUnitCategary,
        ModifyUnitCategory,
        DeleteUnitCategory,
        AddCallType,
        ModifyCallType,
        DeleteCallType,
        AddCallDirection,
        ModifyCallDirection,
        DeleteCallDirection,
        AddRoamingOperator,
        ModifyRoamingOperator,
        DeleteRoamingOperator,
        ImportRoamingOperator,
        AddRoamingZone,
        ModifyRoamingZone,
        DeleteRoamingZone,
        ImportRoamingZone,
        AddRoamingZoneDetail,
        ModifyRoamingZoneDetail,
        DeleteRoamingZoneDetail,
        ImportRoamingZoneDetail,
        AddInternationalZone,
        ModifyInternationalZone,
        DeleteInternationalZone,
        AddInternationalZoneDetail,
        ModifyInternationalZoneDetail,
        DeleteInternationalZoneDetail,
        AddDateCategory,
        ModifyDateCategory,
        DeleteDateCategory,
        AddDateCategoryDetail,
        ModifyDateCategoryDetail,
        DeleteDateCategoryDetail,
        AddIPRange,
        ModifyIPRange,
        DeleteIPRange,
        ImportIPrange,
        AddEUPS,
        ModifyEUPS,
        DeleteEUPS,
        ImportEUPS,
        AddIncumbent,
        ModifyIncumbent,
        DeleteIncumbent,
        ImportIncumbent,
        AddPRSCustomerRateplan,
        ModifyPRSCustomerRateplan,
        DeletePRSCustomerRateplan,
        ImportPRSCustomerRateplan,
        BuildRatePlan,
        AddSRERelate,
        ModifySRERelate,
        DeleteSRERelate,
        //end,ari.he 2011-06-20

        //Commission------------------------
        AddCommissionPlan,
        DisableCommissionPlan,
        QueryCommissionPlans,
        AddCommissionHistory,
        ModifyCommissionHistory,
        QueryCommissionHistories,

        //Usage WSCategory
        QueryWSCategoryDef,
        CreateWSCategoryCosts,
        ModifyWSCategoryCosts,
        QueryWSCategoryCosts,

        // Custom Vouchers
        CreateCustomVouchers,
        CancelCustomVouchers,

        //Added by Benny 2014-08-20 Device Specific Renewal Rule
        AddDeviceSpecificRenewalRule,
        ModifyDeviceSpecificRenewalRule,
        DeleteDeviceSpecificRenewalRule,
        QueryDeviceSpecificRenewalRule,
		ChangeWallet,
    }


    public enum LoginStatus : int
    {
        Login = 0,
        Logout = 1,
        LoginFailed = 2,
    }

    public enum UserType : int
    {
        CRM_Operator = 0,
        Self_Care_Enduser = 1,
        API_Outer_User = 2,
    }
    public enum UserSource : int
    {
        CRM_Client = 0,
        Self_Care,
        API_User,
        CRM_To_Self_Care,
    }

    public enum MVNOBusinessType
    {
        /// <summary>
        ///  Mobile MVNO
        /// </summary>
        Mobile = 1,
        /// <summary>
        /// WIFI MVNO
        /// </summary>
        WIFI = 2,
        /// <summary>
        /// CPS MVNO
        /// </summary>
        CPS = 3,
        /// <summary>
        /// PRS MVNO
        /// </summary>
        PRS = 4,
    }

    public enum DefaultMVNOConfig
    {
        /// <summary>
        /// CPS MVNO
        /// </summary>
        CPS = 1,
        /// <summary>
        /// PRS MVNO
        /// </summary>
        PRS = 2,
    }

    public enum CustomerBusinessType : int
    {
        /// <summary>
        /// 
        /// </summary>
        None = -1,

        /// <summary>
        /// 
        /// </summary>
        Private = 1,

        /// <summary>
        /// 
        /// </summary>
        Business = 2,

        /// <summary>
        /// 
        /// </summary>
        MultiPackagePreCustomer = 3,

        /// <summary>
        /// 
        /// </summary>
        MultiPackagePreCustomerEx = 4,

        /// <summary>
        /// 
        /// </summary>
        ParentControl_Private = 5,

        /// <summary>
        /// 
        /// </summary>
        ParentControl_Business = 6,

        Saudi = 821,

        Expat = 822,

        VIP = 823,

        AxiomStaff = 824
    }


    public enum Culture : int
    {
        /// <summary>
        /// Chinese - China
        /// </summary>
        zh_CN = 2052,

        /// <summary>
        /// English - United States
        /// </summary>
        en_US = 1033,

        /// <summary>
        /// Spanish - Spain
        /// </summary>
        es_ES = 3082,

        /// <summary>
        /// Dutch - The Netherlands
        /// </summary>
        nl_NL = 1043,

        /// <summary>
        /// Dutch - Belgium
        /// </summary>
        nl_BE = 2067,

        /// <summary>
        /// French - France
        /// </summary>
        fr_FR = 1036,

        /// <summary>
        /// French - Belgium
        /// </summary>
        fr_BE = 2060,



        /// <summary>
        /// Afrikaans
        /// </summary>
        af = 54,

        /// <summary>
        /// Afrikaans - South Africa
        /// </summary>
        af_ZA = 1078,

        /// <summary>
        /// Albanian
        /// </summary>
        sq = 28,

        /// <summary>
        /// Albanian - Albania
        /// </summary>
        sq_AL = 1052,

        /// <summary>
        /// Arabic
        /// </summary>
        ar = 1,

        /// <summary>
        /// Arabic - Algeria
        /// </summary>
        ar_DZ = 5121,

        /// <summary>
        /// 	Arabic - Bahrain
        /// </summary>
        ar_BH = 15361,

        /// <summary>
        /// Arabic - Egypt
        /// </summary>
        ar_EG = 3073,

        /// <summary>
        /// Arabic - Iraq
        /// </summary>
        ar_IQ = 2049,

        /// <summary>
        /// Arabic - Jordan
        /// </summary>
        ar_JO = 11265,

        /// <summary>
        /// Arabic - Kuwait
        /// </summary>
        ar_KW = 13313,

        /// <summary>
        /// Arabic - Lebanon
        /// </summary>
        ar_LB = 12289,

        /// <summary>
        /// Arabic - Libya
        /// </summary>
        ar_LY = 4097,

        /// <summary>
        /// Arabic - Morocco
        /// </summary>
        ar_MA = 6145,

        /// <summary>
        /// Arabic - Oman
        /// </summary>
        ar_OM = 8193,

        /// <summary>
        /// 	Arabic - Qatar
        /// </summary>
        ar_QA = 16385,

        /// <summary>
        /// Arabic - Saudi Arabia
        /// </summary>
        ar_SA = 1025,

        /// <summary>
        /// Arabic - Syria
        /// </summary>
        ar_SY = 10241,

        /// <summary>
        /// Arabic - Tunisia
        /// </summary>
        ar_TN = 7169,

        /// <summary>
        /// Arabic - United Arab Emirates
        /// </summary>
        ar_AE = 14337,

        /// <summary>
        /// Arabic - Yemen
        /// </summary>
        ar_YE = 9217,

        /// <summary>
        /// Armenian
        /// </summary>
        hy = 43,

        /// <summary>
        /// Armenian - Armenia
        /// </summary>
        hy_AM = 1067,

        /// <summary>
        /// Azeri
        /// </summary>
        az = 44,

        /// <summary>
        /// Azeri (Cyrillic) - Azerbaijan
        /// </summary>
        az_AZ_Cyrl = 2092,

        /// <summary>
        /// Azeri (Latin) - Azerbaijan
        /// </summary>
        az_AZ_Latn = 1068,

        /// <summary>
        /// Basque
        /// </summary>
        eu = 45,

        /// <summary>
        /// Basque - Basque
        /// </summary>
        eu_ES = 1069,

        /// <summary>
        /// Belarusian
        /// </summary>
        be = 35,

        /// <summary>
        /// Belarusian - Belarus
        /// </summary>
        be_BY = 1059,

        /// <summary>
        /// Bulgarian
        /// </summary>
        bg = 2,

        /// <summary>
        /// Bulgarian - Bulgaria
        /// </summary>
        bg_BG = 1026,

        /// <summary>
        /// Catalan
        /// </summary>
        ca = 3,

        /// <summary>
        /// Catalan - Catalan
        /// </summary>
        ca_ES = 1027,

        /// <summary>
        /// Chinese - Hong Kong SAR
        /// </summary>
        zh_HK = 3076,

        /// <summary>
        /// Chinese - Macau SAR
        /// </summary>
        zh_MO = 5124,

        /// <summary>
        /// Chinese (Simplified)
        /// </summary>
        zh_CHS = 4,

        /// <summary>
        /// Chinese - Singapore
        /// </summary>
        zh_SG = 4100,

        /// <summary>
        /// Chinese - Taiwan
        /// </summary>
        zh_TW = 1028,

        /// <summary>
        /// Chinese (Traditional)
        /// </summary>
        zh_CHT = 31748,

        /// <summary>
        /// Croatian
        /// </summary>
        hr = 26,

        /// <summary>
        /// Croatian - Croatia
        /// </summary>
        hr_HR = 1050,

        /// <summary>
        /// Czech
        /// </summary>
        cs = 5,

        /// <summary>
        /// Czech - Czech Republic
        /// </summary>
        cs_CZ = 1029,

        /// <summary>
        /// Danish
        /// </summary>
        da = 6,

        /// <summary>
        /// Danish - Denmark
        /// </summary>
        da_DK = 1030,

        /// <summary>
        /// Dhivehi
        /// </summary>
        div = 101,

        /// <summary>
        /// Dhivehi - Maldives
        /// </summary>
        div_MV = 1125,

        /// <summary>
        /// Dutch
        /// </summary>
        nl = 19,

        /// <summary>
        /// English
        /// </summary>
        en = 9,

        /// <summary>
        /// English - Australia
        /// </summary>
        en_AU = 3081,

        /// <summary>
        /// English - Belize
        /// </summary>
        en_BZ = 10249,

        /// <summary>
        /// English - Canada
        /// </summary>
        en_CA = 4105,

        /// <summary>
        /// English - Caribbean
        /// </summary>
        en_CB = 9225,

        /// <summary>
        /// English - Ireland
        /// </summary>
        en_IE = 6153,

        /// <summary>
        /// English - Jamaica
        /// </summary>
        en_JM = 8201,

        /// <summary>
        /// English - New Zealand
        /// </summary>
        en_NZ = 5129,

        /// <summary>
        /// English - Philippines
        /// </summary>
        en_PH = 13321,

        /// <summary>
        /// English - South Africa
        /// </summary>
        en_ZA = 7177,

        /// <summary>
        /// English - Trinidad and Tobago
        /// </summary>
        en_TT = 11273,

        /// <summary>
        /// English - United Kingdom
        /// </summary>
        en_GB = 2057,

        /// <summary>
        /// English - Zimbabwe
        /// </summary>
        en_ZW = 12297,

        /// <summary>
        /// Estonian
        /// </summary>
        et = 37,

        /// <summary>
        /// Estonian - Estonia
        /// </summary>
        et_EE = 1061,

        /// <summary>
        /// Faroese
        /// </summary>
        fo = 56,

        /// <summary>
        /// Faroese - Faroe Islands
        /// </summary>
        fo_FO = 1080,

        /// <summary>
        /// Farsi
        /// </summary>
        fa = 41,

        /// <summary>
        /// Farsi - Iran
        /// </summary>
        fa_IR = 1065,

        /// <summary>
        /// Finnish
        /// </summary>
        fi = 11,

        /// <summary>
        /// Finnish - Finland
        /// </summary>
        fi_FI = 1035,

        /// <summary>
        /// French
        /// </summary>
        fr = 12,

        /// <summary>
        /// French - Canada
        /// </summary>
        fr_CA = 3084,



        /// <summary>
        /// French - Luxembourg
        /// </summary>
        fr_LU = 5132,

        /// <summary>
        /// French - Monaco
        /// </summary>
        fr_MC = 6156,

        /// <summary>
        /// French - Switzerland
        /// </summary>
        fr_CH = 4108,

        /// <summary>
        /// Galician
        /// </summary>
        gl = 86,

        /// <summary>
        /// Galician - Galician
        /// </summary>
        gl_ES = 1110,

        /// <summary>
        /// Georgian
        /// </summary>
        ka = 55,

        /// <summary>
        /// Georgian - Georgia
        /// </summary>
        ka_GE = 1079,

        /// <summary>
        /// German
        /// </summary>
        de = 7,

        /// <summary>
        /// German - Austria
        /// </summary>
        de_AT = 3079,

        /// <summary>
        /// German - Germany
        /// </summary>
        de_DE = 1031,

        /// <summary>
        /// German - Liechtenstein
        /// </summary>
        de_LI = 5127,

        /// <summary>
        /// German - Luxembourg
        /// </summary>
        de_LU = 4103,

        /// <summary>
        /// German - Switzerland
        /// </summary>
        de_CH = 2055,

        /// <summary>
        /// Greek
        /// </summary>
        el = 8,

        /// <summary>
        /// Greek - Greece
        /// </summary>
        el_GR = 1032,

        /// <summary>
        /// Gujarati
        /// </summary>
        gu = 71,

        /// <summary>
        /// Gujarati - India
        /// </summary>
        gu_IN = 1095,

        /// <summary>
        /// Hebrew
        /// </summary>
        he = 13,

        /// <summary>
        /// Hebrew - Israel
        /// </summary>
        he_IL = 1037,

        /// <summary>
        /// Hindi
        /// </summary>
        hi = 57,

        /// <summary>
        /// Hindi - India
        /// </summary>
        hi_IN = 1081,

        /// <summary>
        /// Hungarian
        /// </summary>
        hu = 14,

        /// <summary>
        /// Hungarian - Hungary
        /// </summary>
        hu_HU = 1038,

        /// <summary>
        /// Icelandic
        /// </summary>
        Is = 15,

        /// <summary>
        /// Icelandic - Iceland
        /// </summary>
        is_IS = 1039,

        /// <summary>
        /// Indonesian
        /// </summary>
        id = 33,

        /// <summary>
        /// Indonesian - Indonesia
        /// </summary>
        id_ID = 1057,

        /// <summary>
        /// Italian
        /// </summary>
        it = 16,

        /// <summary>
        /// Italian - Italy
        /// </summary>
        it_IT = 1040,

        /// <summary>
        /// Italian - Switzerland
        /// </summary>
        it_CH = 2064,

        /// <summary>
        /// Japanese
        /// </summary>
        ja = 17,

        /// <summary>
        /// Japanese - Japan
        /// </summary>
        ja_JP = 1041,

        /// <summary>
        /// Kannada
        /// </summary>
        kn = 75,

        /// <summary>
        /// Kannada - India
        /// </summary>
        kn_IN = 1099,

        /// <summary>
        /// Kazakh
        /// </summary>
        kk = 63,

        /// <summary>
        /// Kazakh - Kazakhstan
        /// </summary>
        kk_KZ = 1087,

        /// <summary>
        /// Konkani
        /// </summary>
        kok = 87,

        /// <summary>
        /// Konkani - India
        /// </summary>
        kok_IN = 1111,

        /// <summary>
        /// Korean
        /// </summary>
        ko = 18,

        /// <summary>
        /// Korean - Korea
        /// </summary>
        ko_KR = 1042,

        /// <summary>
        /// Kyrgyz
        /// </summary>
        ky = 64,

        /// <summary>
        /// Kyrgyz - Kazakhstan
        /// </summary>
        ky_KZ = 1088,

        /// <summary>
        /// Latvian
        /// </summary>
        lv = 38,

        /// <summary>
        /// Latvian - Latvia
        /// </summary>
        lv_LV = 1062,

        /// <summary>
        /// Lithuanian
        /// </summary>
        lt = 39,

        /// <summary>
        /// Lithuanian - Lithuania
        /// </summary>
        lt_LT = 1063,

        /// <summary>
        /// Macedonian
        /// </summary>
        mk = 47,

        /// <summary>
        /// Macedonian - FYROM
        /// </summary>
        mk_MK = 1071,

        /// <summary>
        /// Malay
        /// </summary>
        ms = 62,

        /// <summary>
        /// Malay - Brunei
        /// </summary>
        ms_BN = 2110,

        /// <summary>
        /// Malay - Malaysia
        /// </summary>
        ms_MY = 1086,

        /// <summary>
        /// Marathi
        /// </summary>
        mr = 78,

        /// <summary>
        /// Marathi - India
        /// </summary>
        mr_IN = 1102,

        /// <summary>
        /// Mongolian
        /// </summary>
        mn = 80,

        /// <summary>
        /// Mongolian - Mongolia
        /// </summary>
        mn_MN = 1104,

        /// <summary>
        /// Norwegian
        /// </summary>
        no = 20,

        /// <summary>
        /// Norwegian (Bokml) - Norway
        /// </summary>
        nb_NO = 1044,

        /// <summary>
        /// Norwegian (Nynorsk) - Norway
        /// </summary>
        nn_NO = 2068,

        /// <summary>
        /// Polish
        /// </summary>
        pl = 21,

        /// <summary>
        /// Polish - Poland
        /// </summary>
        pl_PL = 1045,

        /// <summary>
        /// Portuguese
        /// </summary>
        pt = 22,

        /// <summary>
        /// Portuguese - Brazil
        /// </summary>
        pt_BR = 1046,

        /// <summary>
        /// Portuguese - Portugal
        /// </summary>
        pt_PT = 2070,

        /// <summary>
        /// Punjabi
        /// </summary>
        pa = 70,

        /// <summary>
        /// Punjabi - India
        /// </summary>
        pa_IN = 1094,

        /// <summary>
        /// Romanian
        /// </summary>
        ro = 24,

        /// <summary>
        /// Romanian - Romania
        /// </summary>
        ro_RO = 1048,

        /// <summary>
        /// Russian
        /// </summary>
        ru = 25,

        /// <summary>
        /// Russian - Russia
        /// </summary>
        ru_RU = 1049,

        /// <summary>
        /// Sanskrit
        /// </summary>
        sa = 79,

        /// <summary>
        /// Sanskrit - India
        /// </summary>
        sa_IN = 1103,

        /// <summary>
        /// Serbian (Cyrillic) - Serbia
        /// </summary>
        sr_SP_Cyrl = 3098,

        /// <summary>
        /// Serbian (Latin) - Serbia
        /// </summary>
        sr_SP_Latn = 2074,

        /// <summary>
        /// Slovak
        /// </summary>
        sk = 27,

        /// <summary>
        /// Slovak - Slovakia
        /// </summary>
        sk_SK = 1051,

        /// <summary>
        /// Slovenian
        /// </summary>
        sl = 36,

        /// <summary>
        /// Slovenian - Slovenia
        /// </summary>
        sl_SI = 1060,

        /// <summary>
        /// Spanish
        /// </summary>
        es = 10,

        /// <summary>
        /// Spanish - Argentina
        /// </summary>
        es_AR = 11274,

        /// <summary>
        /// Spanish - Bolivia
        /// </summary>
        es_BO = 16394,

        /// <summary>
        /// Spanish - Chile
        /// </summary>
        es_CL = 13322,

        /// <summary>
        /// Spanish - Colombia
        /// </summary>
        es_CO = 9226,

        /// <summary>
        /// Spanish - Costa Rica
        /// </summary>
        es_CR = 5130,

        /// <summary>
        /// Spanish - Dominican Republic
        /// </summary>
        es_DO = 7178,

        /// <summary>
        /// Spanish - Ecuador
        /// </summary>
        es_EC = 12298,

        /// <summary>
        /// Spanish - El Salvador
        /// </summary>
        es_SV = 17418,

        /// <summary>
        /// Spanish - Guatemala
        /// </summary>
        es_GT = 4106,

        /// <summary>
        /// Spanish - Honduras
        /// </summary>
        es_HN = 18442,

        /// <summary>
        /// Spanish - Mexico
        /// </summary>
        es_MX = 2058,

        /// <summary>
        /// Spanish - Nicaragua
        /// </summary>
        es_NI = 19466,

        /// <summary>
        /// Spanish - Panama
        /// </summary>
        es_PA = 6154,

        /// <summary>
        /// Spanish - Paraguay
        /// </summary>
        es_PY = 15370,

        /// <summary>
        /// Spanish - Peru
        /// </summary>
        es_PE = 10250,

        /// <summary>
        /// Spanish - Puerto Rico
        /// </summary>
        es_PR = 20490,



        /// <summary>
        /// Spanish - Uruguay
        /// </summary>
        es_UY = 14346,

        /// <summary>
        /// Spanish - Venezuela
        /// </summary>
        es_VE = 8202,

        /// <summary>
        /// Swahili
        /// </summary>
        sw = 65,

        /// <summary>
        /// Swahili - Kenya
        /// </summary>
        sw_KE = 1089,

        /// <summary>
        /// Swedish
        /// </summary>
        sv = 29,

        /// <summary>
        /// Swedish - Finland
        /// </summary>
        sv_FI = 2077,

        /// <summary>
        /// Swedish - Sweden
        /// </summary>
        sv_SE = 1053,

        /// <summary>
        /// Syriac
        /// </summary>
        syr = 90,

        /// <summary>
        /// Syriac - Syria
        /// </summary>
        syr_SY = 1114,

        /// <summary>
        /// Tamil
        /// </summary>
        ta = 73,

        /// <summary>
        /// Tamil - India
        /// </summary>
        ta_IN = 1097,

        /// <summary>
        /// Tatar
        /// </summary>
        tt = 68,

        /// <summary>
        /// Tatar - Russia
        /// </summary>
        tt_RU = 1092,

        /// <summary>
        /// Telugu
        /// </summary>
        te = 74,

        /// <summary>
        /// Telugu - India
        /// </summary>
        te_IN = 1098,

        /// <summary>
        /// Thai
        /// </summary>
        th = 30,

        /// <summary>
        /// Thai - Thailand
        /// </summary>
        th_TH = 1054,

        /// <summary>
        /// Turkish
        /// </summary>
        tr = 31,

        /// <summary>
        /// Turkish - Turkey
        /// </summary>
        tr_TR = 1055,

        /// <summary>
        /// Ukrainian
        /// </summary>
        uk = 34,

        /// <summary>
        /// Ukrainian - Ukraine
        /// </summary>
        uk_UA = 1058,

        /// <summary>
        /// Urdu
        /// </summary>
        ur = 32,

        /// <summary>
        /// Urdu - Pakistan
        /// </summary>
        ur_PK = 1056,

        /// <summary>
        /// Uzbek
        /// </summary>
        uz = 67,

        /// <summary>
        /// Uzbek (Cyrillic) - Uzbekistan
        /// </summary>
        uz_UZ_Cyrl = 2115,

        /// <summary>
        /// Uzbek (Latin) - Uzbekistan
        /// </summary>
        uz_UZ_Latn = 1091,

        /// <summary>
        /// Vietnamese
        /// </summary>
        vi = 42,

        /// <summary>
        /// Vietnamese - Vietnam
        /// </summary>
        vi_VN = 1066
    }

    public enum IntegratedRWOfLogin
    {
        SecurityInfo,
        RoleInfo,
        PermissionInfo,
        ModuleInfo,
        UserDealerInfo,
        UserOrganizationInfo,
        StyleInfo,
        ThemeInfo,
        CRMMessageInfo,
        ServerTimeZone,
        REGEX,
        SystemConfigData
    }

    public enum UIStyleType
    {
        StyleTypeLogin = 1,
        StyleTypeUIMain = 2,
        StyleTypeReport = 3,
        StyletypePanel = 4,
        StyleTypeToolStrip = 5,
        StyleFormImage = 6,
        UIMainTableBackImage = 7,
        PortalOfCustomerCare = 8,
        PortalOfTopUp = 9,
        PortalOfTroubleTicket = 10,
        PortalOfReporting = 11,
        PortalOfNPM = 12,
        InvoiceLog = 13,
        ContractLog = 14,
        StyleTyoeNavigation_Hover = 20,
        StyleTyoeNavigation_Leave = 21,
        StyleTyoeNavigation_Click = 22,
    }

    public enum UIStyleIndex
    {
        StyleTypeLogin = 0,
        StyleTypeUIMain = 0,
        StyleTypeReport = 0,
        StyletypePanel = 0,
        StyleTypeToolStrip = 0,
        StyleFormImage = 0,
        UIMainTableBackImage = 0
    }

    public enum UIStyleTypeIndex
    {
        StyleTyoeNavigation_Hover = 0,
        StyleTyoeNavigation_Leave = 0,
        StyleTyoeNavigation_Click = 0
    }

    public enum UINavigation
    {
        CustomerCare,
        COIN,
        Log,
        System,
        RatingManagement,
        Alerting,
        Reporting,
        WholeSaleBilling,
        Billing,
        Bottom
    }

    public enum UIThemeType
    {
        NavigationBackColor,
        NavigationBackFont,
        NavigationFont,
        TreeBackColor,
        UIMainBackColor,
        ETFormBackColor
    }

    public enum TeleServiceList
    {
        TS00,
        TS10,
        TS11,
        TS12,
        TS20,
        TS21,
        TS22,
        TS60,
        TS61,
        TS62,
        TS63,
        TS70,
        TS80,
        TS90,
        TS91,
        TS92,
        TSD0,
        TSD1,
        TSD2,
        TSD3,
        TSD4,
        TSD5,
        TSD6,
        TSD7,
        TSD8,
        TSD9,
        TSDA,
        TSDB,
        TSDC,
        TSDD,
        TSDE,
        TSDF
    }

    public enum BearerServiceList
    {
        BS00,
        BS10,
        BS11,
        BS12,
        BS13,
        BS14,
        BS15,
        BS16,
        BS17,
        BS18,
        BS19,
        BS1A,
        BS1B,
        BS1C,
        BS1D,
        BS1E,
        BS1F,
        BS20,
        BS21,
        BS22,
        BS23,
        BS24,
        BS25,
        BS26,
        BS27,
        BS28,
        BS29,
        BS2A,
        BS2B,
        BS2C,
        BS2D,
        BS2E,
        BS2F,
        BS30,
        BS31,
        BS32,
        BS33,
        BS34,
        BS35,
        BS36,
        BS38,
        BS40,
        BS48,
        BS50,
        BS58,
        BS60,
        BS68,
        BSD0,
        BSD1,
        BSD2,
        BSD3,
        BSD4,
        BSD5,
        BSD6,
        BSD7,
        BSD8,
        BSD9,
        BSDA,
        BSDB,
        BSDC,
        BSDD,
        BSDE,
        BSDF
    }

    public enum OdbMask
    {
        AllOGCalls,
        AllOGInternatCalls,
        AllOGInternatCallsExceptHplmn,
        AllOGInterzonalCalls,
        AllOGInterzonalCallsExceptHplmn,
        AllOGInternatExceptHplmnAndBarringInterzonalCalls,
        AllOGWhenRoamingOutsideHPLMNcountry,
        AllICCalls,
        AllICCallsWhenRoamingOutsideHplmn,
        AllICCallsWhenRoamingOutsideZoneOfHplmn,
        RoamingOutsideHplmn,
        RoamingOutsideHplmnCountry,
        PremiumRateInfo,
        PremiumRateEntertainment,
        PremiumRateInfoAndEntertainment,
        SuppServicesManagement,
        RegistrationAnyFtn,
        RegistrationInternatFtn,
        RegistrationInternatFtnExceptHplmn,
        RegistrationAnyInterzonalFtn,
        RegistrationInterzonalFtnExceptHplmn,
        CallTransfer,
        CallTransferAnyChargedToServed,
        CallTransferAnyInternatChargedToServed,
        CallTransferAnyInterzonalChargedToServed,
        CallTransferBothChargedToServed,
        CallTransferExistingTransferForServed,
        PacketServices,
        PacketServicesFromHplmnWhileInVplmn,
        PacketServicesWithinVplmn,
        OperatorSpecificType1,
        OperatorSpecificType2,
        OperatorSpecificType3,
        OperatorSpecificType4,
    }


    public enum MultiLingualShowType : int
    {
        /// <summary>
        /// Initialization information of UI [eg. Label, ComboBox]
        /// </summary>
        UIInit = 1,

        /// <summary>
        /// Translation for permission management
        /// </summary>
        PermissionTranslation = 2,

        /// <summary>
        /// 
        /// </summary>
        UIInitAndPermissionTranslation = 3
    }


    public enum SystemConfigHeadType
    {
        /// <summary>
        /// ID of MVNO
        /// </summary>
        MVNOID,

        /// <summary>
        /// Profile of customer care (eg. registration customer)
        /// </summary>
        CustomerCareServerProfile,

        /// <summary>
        /// Website of self care
        /// </summary>
        SelfcareWebSiteUrl,

        MVNOTemplateName,
        TTSubmit,
        DepositCreditRatio,
        DepositThreshold,
        CanNegative,
        Contract,
        NewSimCardType,
    }
    public enum MVNOTemplateName : int
    {
        Orbitel = 101,
        Youfone = 102,
        Telecombination_2nr = 103,
        Telecombination_Flex = 104
    }

    /// <summary>
    /// 下拉参数枚举类型
    /// 如果需要扩充，需要做如下四步：
    /// 1）增加一个对应的枚举型
    /// 2）然后在上面获取数据的方法中，增加对应取数方法，
    /// 3）更新ComboValuesDAO.COMBO_PARAMETERS
    /// 4）维护名称和类型的映射方法：GetComboValuesEnumFromParameterName
    /// 
    /// 对于参数名混淆，这个要特别注意，尤其是参数名取名不慎，本来不须选择，结果却提供了选择，或者相反。
    /// </summary>
    public enum ComboValuesEnum : int
    {
        //TIMECATEGORY,
        //UNITCATEGORY,
        //CALLTYPE,
        //CALLDIRECTION,
        //SWITCH,
        //CARRIER,
        //RATEPLAN,
        //PROMOTIONPACKAGE,
        //PROVIDER,
        DEALER,
        //LANGUAGE ,
        SERVICETYPE,
        //SUBSERVICETYPE,
        //SUBSCRIPTIONCYCLE,
        //PAYMENTTYPE,
        CURRENCY,
        TTSTATUS,
        TTPRIORITY,
        VOUCHERSTATUS,
        CUSTOMER,
        //TOPUPTYPE,
        PENDINGSTATUS,
        FLEX2NDTYPE,
        FLEX2NDACTIVE,
        DATEGROUPTYPE,
        TTDEPTID,
        TTOPERATORID,
        MVNOID,
        RESOURCESTATUS,
        YEAR,
        MONTH,
        CALLFORWARDTYPE,
        VODAFONETTDEPTID,
        VODAFONETTOPERATORID,
        FFTRAFFICTYPE,
        CUSTOMERSTATUS,
        /// <summary>
        /// 空
        /// </summary>
        NULL

    }
    public enum ElementType
    {
        Form = 0,
        CheckBox = 1,
        ComboBox = 2,
        ContextMenuStrip = 3,
        DataGridView = 4,
        DataGridViewPager = 5,
        DateTimePicker = 6,
        GroupBox = 7,
        Label = 8,
        ListView = 9,
        RadioButton = 10,
        TabControl = 11,
        TextBox = 12,
        ToolStrip = 13,
        TreeView = 14,
        VistaButton = 15,
        ToolStripButton = 16,
        ToolStripTextBox = 17,
        picturebox = 18,
        DomainUpDown = 19
    }

    public enum PaymentTypeConvert
    {
        NoChange = 0,
        PrepaidToPrepaid = 1,
        PrepaidToPostpaid = 2,
        PostToPrepaid = 3,
        PostToPostpaid = 4
    }

    public enum eInvoker
    {
        UNKNOWN = 0,
        Client = 1,
        WebPortal = 2,
        ATOSAPI = 3,
        SelfCareAPI = 4,
        Dre = 5,
        ATM = 6,
        TCApi = 7,
        PremiumSMSAPI = 8,
        LiquixAPI = 18,
        APIService = 9,
        ListenerAPISaudi = 10
    }

    public enum ProductUIEditType
    {
        Registration = 10000001,
        Modify = 10000002,
        ChangePackage = 10000003
    }
    /// <summary>
    /// Operation Status
    /// jeffrey 30/10/2008
    /// </summary>
    public enum eOperationStatus
    {
        /// <summary>
        /// Completed.
        /// </summary>
        CO = 1,
        /// <summary>
        /// In execution
        /// </summary>
        EJ = 2,
        /// <summary>
        /// Canceled/Finished
        /// </summary>
        AN = 3,
        /// <summary>
        ///  Annultment Request. Temporary state previous to moving to “AN” state.
        /// </summary>
        SA = 4,
        /// <summary>
        /// Retained: Intermediate state  where there have been errors of provisioning.
        /// The passage through this state does not imply the request may not end up in state “CO”.
        /// </summary>
        RE = 5
    }


    public enum CoinMsgStatus
    {
        Agree = 1,
        Disagree = 2,
        Send = 3,
        WaitSend = 4,
        AgreeWaitSend = 5,
        AgreeSend = 6,
        PortingEnd = 7,
        Cancel = 8
    }

    public enum CoinApiReturn
    {
        COINAPI_PORTING_SUCCESS = 352321564,
        COINAPI_PORTING_NUMBER_NOT_EMPTY = 352321565,
        COINAPI_PORTING_NUMBER_TYPE = 352321566,
        COINAPI_PORTING_NUMBER_START = 352321567,
        COINAPI_PORTING_NOTE_START = 352321568,
        COINAPI_PORTIGN_NUMBER_NOT_EXIST = 352321569,
        COINAPI_XML_MODUEL_NOT_EXITS = 352321570,
        COINAPI_USER_OPERATORS_NOT_EXITS = 352321571,
        COINAPI_ABNORMAL_SAVE_FAILURE = 352321572,
        COINAPI_PORTING_SELECT_EXITS = 352321573,
        COINAPI_PORTING_SELECT_AGREED = 352321574,
        COINAPI_PORTING_SELECT_BLOCKED = 352321575,
        COINAPI_PORTING_SELECT_SENT = 352321576,
        COINAPI_PORTING_SELECT_WAITSEND = 352321577,
        COINAPI_PORTING_SELECT_AGREEWAITSEND = 352321578,
        COINAPI_PORTING_SELECT_PORTINGPERFORMED = 352321579,
        COINAPI_PORTING_SELECT_CANCELLED = 352321580,
        COINAPI_PORTING_SELECT_IS_EXITS = 352321581,
        COINAPI_PORTING_SELECT_AGREESEND = 352321582,
        COINAPI_PORTING_SELECT_FAILURE = 352321583,
        COINAPI_PORTING_ICC_NUMBER = 352321584
    }

    public enum RechargeTarget
    {
        PREWALLET = 0,
        DEPOSIT = 1,
        BOUNS = 2
    }

    public enum Deposit
    {
        ON = 1,
        OFF = 0
    }

    public enum AutoTopupStatus
    {
        ON = 1,
        OFF = 0
    }

    public enum BankCollectionCode : int
    {
        OK = 0000, // 
        INVALID_BERICHTTYPE_ONBEKEND = 1000,
        INVALID_AMOUNT_TRANSACTION_AMOUNT = 1001,
        INVALID_CURRENCY_TRANSACTION_CURRENCY = 1002,
        INVALID_MERCHANT_ACCOUNT_ID_MERCHANT_ACCOUNT = 1003,
        INVALID_MERCHANT_SITE_ID_MERCHANT_SITE_ID = 1004,
        INVALID_MERCHANT_SITE_SECURITY_CODE_MERCHANT_SITE_SECURE_CODE = 1005,
        INVALID_TRANSACTION_ID_TRANSACTION_ID = 1006,
        INVALID_IP_ADDRESS_CUSTOMER_IPADDRESS_FORWARDEDIP = 1007,
        INVALID_DESCRIPTION_TRANSACTION_DESCRIPTION = 1008,
        RESERVED1009 = 1009,
        INVALID_VARIABLE_TRANSACTION_VAR1_VAR2_VAR3 = 1010,
        INVALID_CUSTOMER_ACCOUNT_ID = 1011,
        INVALID_CUSTOMER_SECURITY_CODE = 1012,
        INVALID_SIGNATURE_SIGNATURE = 1013,
        UNSPECIFIED_ERROR = 1014,
        UNKNOWN_ACCOUNT = 1015,
        MISSING_INFORMATION = 1016,
        INSUFFICIENT_BALANCE = 1017,
        RESERVED1018 = 1018,
        RESERVED1019 = 1019,
        UNKNOWN_ERROR = 9999,
    }

    public enum BankCollectionStatus : int
    {
        Completed = 10000001, //successfully completed
        Initialized = 11000001,// initialized, but not yet completed
        Uncleared = 12000001,// initialized , but not yet cleared (credit cards)
        Void = 13000001,// cancelled
        Declined = 14000001,
        Refunded = 15000001,
        Expired = 16000001
    }

    public enum BankWorkItem : int
    {
        /// <summary>
        /// Request to bank
        /// </summary>
        Request = 10000001,

        /// <summary>
        /// Respond from bank
        /// </summary>
        Respond = 11000001
    }

    public enum BankCollectionLogType : int
    {
        SetPaymentURL = 10001,
        GetStatus = 10002,
        //== add by john ATM topup bank provision request xml and response xml to db 2011-3-14 start
        TopUpCancellation = 10003,
        //== add by john ATM topup bank provision request xml and response xml to db 2011-3-14 end

        //== add by john ATM topup bank provision request xml and response xml to db 2012-10-10 start
        VomsRechargeRequest = 10004,
        VomsRechargeConfirm = 10005
        //== add by john ATM topup bank provision request xml and response xml to db 2012-10-10 end
    }

    public enum SaudiRegisterLogType : int
    {
        SaudiRegisterCheck = 100101,
        SaudiRegisterCreate = 100102,
        SaudiRegisterUpdate = 100103,
        SaudiRegisterTerminate = 100104
    }

    public enum IMSIType : int
    {
        /// <summary>
        /// 
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 
        /// </summary>
        MultiIMSI = 1
    }

    #region added by Liny,2010-05-21

    public enum BillingMethod
    {
        NOINVOICE = 0,
        EMAIL = 1,
        PRINT = 2,
        //modified by damon, 2014-01-24
        BothEmailAndPrint = 3,
    }

    public enum ContactPeriod
    {
        NINE = 9,
        EIGHTEEN = 18
    }

    public enum CMContactPeriod
    {
        TWELVE = 12,
        TWENTYFOUR = 24
    }

    public enum ExtaUsageType
    {
        /// <summary>
        /// Friends & Family Initial fee
        /// </summary>
        FFI = 300,
        /// <summary>
        /// Friends & Family Change fee
        /// </summary>
        FFC = 301,
        /// <summary>
        /// Friends & Family Monthly fee
        /// </summary>
        FFM = 302,
        /// <summary>
        /// Promotion rebate
        /// </summary>
        PromotionRebate = 303,
        /// <summary>
        /// PromotionDeduct
        /// </summary> 
        PromotionDeduct = 304,

        PromotionMonthlyFee = 305,
        PromotionUpdateFee = 306,
        PromotionSubscribeFee = 307,
        PromotionSubscribeMonthFee = 308,

        FFInternationalI = 309,
        FFInternationalC = 310,
        FFMobileI = 311,
        FFMobileC = 312,
        FFSuperOnNetI = 313,
        FFSuperOnNetC = 314,
        FFDIDI = 315,
        FFDIDC = 316,
        FFFixedI = 317,
        FFFixedC = 318,
        BundleMothlyFee = 319,
        BundleSubscriptionFee = 320,
        FreeNumberFee = 321,

        AutotopUp = 401,
        CreditTransfer = 402,
        AdjustByHand = 403,

        //add by winson for billing of child's location
        ChildLocationTimes = 404,
        ChildLocationPro = 405,
        //end 

        //add by James.zhan 2010-08-06
        DeductSignUpFee = 101,
        DeductPortInFee = 102,
        DeductPortOutFee = 103,
        DeductSwitchContractUpdateGradeFee = 111,
        DeductSwitchContractDownGradeFee = 112,
        DeductPackagePostToPreFee = 113,
        DeductSwitchPackageUpdateGradeFee = 114,
        DeductSwitchPackageDownGradeFee = 115,
        DeductSwithSimCardFee = 121,
        DeductSwithNumberFee = 122,
        DeductPaperInvoiceFee = 123,
        DeductAdminDeleteFee = 124,
        DeductReActiveFee = 125,
        //added by neil 2013/10/11 
        DeductPackageSubscriptionFee = 126,
        //add by James end
        DeductBreakContract = 130,//for BT
        TransferPromotionFee = 153,
        ConfigFF = 222,//add by figo
        EmergencyCredit = 131,
    }
    #endregion
    #region added by Liny,2010-06-11

    public enum SAPOrderStatus
    {
        Pending = 0,
        Submitted = 1,
        Confirmed = 2,
        Updated = 3,
        Cancelled = 4
    }

    #endregion

    public enum AssignSIMCardType : int  // Add by Gary.Xie 2010-07-14 15:39:00
    {
        Normal = 1,
        AssignToWebSite = 2
    }

    public enum ExtraUsageStatus : int
    {
        Created = 1,
        Deleted = 2
    }


    #region ManagementFeeEnum
    public enum ManagementFeeEnum
    {
        ManagementFee_SignUp,
        ManagementFee_PortIn,
        ManagementFee_PortOut,
        ManagementFee_Switch_Contract_UpdateGrade,
        ManagementFee_Switch_Contract_DownGrade,
        ManagementFee_Swtich_Package_Post_To_Pre,
        ManagementFee_Switch_Packege_UpdateGrade,
        ManagementFee_Switch_Package_DownGrade,
        ManagementFee_Switch_SimCard,
        ManagementFee_Switch_Number,
        ManagementFee_Paper_Invoice,
        ManagementFee_Admin_Delete,
        ManagementFee_ReActive,
        ManagementFee_BreakContract,//for BT
    }

    #endregion

    #region Add By Lincoln 2010-07-19
    public enum VoucherType : int
    {
        Balance = 0,
        Bonus = 1,
        Promotion = 2 //Add by Benny 2014-07-11
    }
    #endregion

    #region Add by Lincoln 2010-07-21
    //Now remove to TimeUnits.cs
    //public enum TimeUnits : int
    //{
    //    Day = 1,
    //    Week = 2,
    //    Month = 3,
    //    Year = 4
    //}
    #endregion

    public enum PortingStatusforBen
    {
        StatusPending = 0,
        Cancelled = 4,
        //modified by Gary 2011.4.6
        Ported = 6//未用
    }

    #region ATOS
    /// <summary>
    /// 用于记录汇率
    /// </summary>
    public enum Currencies
    {
        EUR = 978,
        SAR = 682,
        CHF = 2,
        GBP = 3,
        PLN = 4,
        SEK = 5,
        USD = 7
    }
    /// <summary>
    /// 充值卡业务类型
    /// </summary>
    public enum VoucherBusinessTypes
    {
        Wifi1 = 1,
        Wifi2 = 2,
        Wifi3 = 3,
        Mobile = 0
    }
    public enum Causas
    {
        /// <summary>
        ///  CHANGE IN RESIDENCE HLR
        /// </summary>
        AH,
        /// <summary>
        /// Desac. Not Paid
        /// </summary>
        AM,
        /// <summary>
        /// SALES CHANNEL QUALITY
        /// </summary>
        C1,
        /// <summary>
        /// INVOICE QUALITY
        /// </summary>
        CF,
        /// <summary>
        /// STOPPED USE, MAINTAIN AIRTEL
        /// </summary>
        CJ,
        /// <summary>
        /// STOPPED USING IT
        /// </summary>
        CM,
        /// <summary>
        /// NR
        /// </summary>
        NR,
        /// <summary>
        /// PB
        /// </summary>
        PB
    }

    /// <summary>
    /// type of result
    /// orbitel 的相关定义已经应用，atos 与其保持一致，jeffrey 2010-04-20
    /// </summary>
    public enum eTipoResultado
    {
        // Correct = 0,
        //DataValidationFailure = 1,
        //RequestProcessed = 2,
        //LogicError = 3,
        //InternalError = 4,
        //AuthenticationValidationFailure = 5

        /// <summary>
        /// Correct
        /// </summary>
        Correcto = 0,
        /// <summary>
        /// Validation not overpass
        /// </summary>
        ValidacionNoSuperada = 1,
        /// <summary>
        /// Authentication validation not overpass
        /// </summary>
        ValidacionAutenticacionNoSuperada = 5,
        /// <summary>
        /// Request Processed
        /// </summary>
        PeticionProcesada = 2,
        /// <summary>
        /// Logical Error
        /// </summary>
        ErrorLogico = 3,
        /// <summary>
        /// Internal Error
        /// </summary>
        ErrorInterno = 4
    }

    #region added by Liny,2010-07-22,for porting xs request

    public enum PortingRequestStatus
    {
        Initial = 0,
        Comfirmed = 1,
        Rejected = 2,
        CancelRequest = 3,
        Cancelled = 4,
        Ported = 5,
        Completed = 6,
        Changing_proposed = 7,
        Change_chk = 8

    }

    #endregion

   
    #region added by Liny,2010-06-24

    public enum SapOrderType
    {
        Order = 0,
        Update = 1,
        Portin = 2
    }

    #endregion

    #region added by Liny,2010-09-15,for ben customer type

    public enum BenCustomerType
    {
        OnlineRegister = 1,
        OnShopUpdate = 2,
        PortinRegister = 3,
        DealerOrderUpdate = 4,
        EventSaleUpdate = 5
    }

    #endregion

    #region added by Liny,2010-09-15,for porting type

    public enum PortingType
    {
        OnlineResPorting = 0,
        StandAlonePorting = 1
    }

    #endregion
    /// <summary>
    /// Type of change state
    /// </summary>
    public enum eCambioEstado
    {
        /// <summary>
        /// Frozen -> Active
        /// </summary>
        DeCongeladoADescongelado = 0,
        /// <summary>
        /// Active -> Frozen
        /// </summary>
        DeDescongeladoACongelado = 1
    }
    /// <summary>
    /// Coupon Distribution Channels
    /// </summary>
    public enum eCanal
    {
        Directo = 0,
        Indirecto = 1,
        Almacen = 2
    }
    /// <summary>
    /// Coupon Status
    /// </summary>
    public enum eEstado
    {
        /// <summary>
        /// Created
        /// </summary>
        Creado = 0,
        /// <summary>
        /// Active
        /// </summary>
        Activo = 1,
        /// <summary>
        /// Used
        /// </summary>
        Usado = 2,
        /// <summary>
        /// Expired
        /// </summary>
        Expirado = 3,
        /// <summary>
        /// Recharging
        /// </summary>
        Recargando = 5,
        /// <summary>
        /// Annulled
        /// </summary>
        Anulado = 6,
        /// <summary>
        /// Fraudulent
        /// </summary>
        Fraudulento = 7
    }

    /// <summary>
    /// Subscriber Type
    /// </summary>
    public enum SubscriberType
    {
        /// <summary>
        /// Private
        /// </summary>
        Particular = 0,
        /// <summary>
        /// Business(Company)
        /// </summary>
        Empresa = 1
    }
    /// <summary>
    /// Identification Type
    /// (1)	If tipoAbonado is a Company, tipoIdentificacion should be CIF.
    /// If tipoAbonado is a Private Individual, tipoIdentificacion may be NIF, Resident or Passport.
    /// </summary>
    public enum IDType
    {
        /// <summary>
        /// NIF(For Private)
        /// </summary>
        NIF = 0,
        /// <summary>
        /// CIF(For Business)
        /// </summary>
        CIF = 1,
        /// <summary>
        /// Resident(For Private)
        /// </summary>
        Residente = 2,
        /// <summary>
        /// Passport(For Private)
        /// </summary>
        Pasaporte = 3
    }
    /// <summary>
    /// Application Type(MNP)
    /// </summary>
    public enum ApplicationType
    {
        /// <summary>
        /// Single
        /// </summary>
        Individual = 0,
        /// <summary>
        /// Multiple
        /// </summary>
        Multiple = 1
    }
    /// <summary>
    /// indicate process status for each MSISDN in table CRM_MNP_PORTABILITY_MULTI
    /// </summary>
    public enum MNPProcessStatus
    {
        Processing = 0,
        Fail = 1,
        Completed = 2
    }
    /// <summary>
    /// Payment type
    /// </summary>
    public enum MNPPaymentType
    {
        NULL = 0,
        /// <summary>
        /// Prepayment
        /// </summary>
        Prepago = 2,
        /// <summary>
        /// Postpayment
        /// </summary>
        Postpago = 1
    }
    /// <summary>
    /// Type of incidence source in question
    /// </summary>
    public enum eFuente
    {
        ///与客户端保持，修改此处前两个值
        /// <summary>
        /// General
        /// </summary>
        General = 2,
        /// <summary>
        /// Individual
        /// </summary>
        Individual = 1,
        /// <summary>
        /// Scheduled Work
        /// </summary>
        TrabajoProgramado = 3,
        /// <summary>
        /// Functional Query
        /// </summary>
        ConsultaFuncional = 4,
        /// <summary>
        /// Task Request
        /// </summary>
        PeticionTarea = 5
    }
    /// <summary>
    /// Type of information being requested
    /// </summary>
    public enum eTipoConsulta
    {
        /// <summary>
        /// Massive incidences
        /// </summary>
        MAS = 0,
        /// <summary>
        /// scheduled work
        /// </summary>
        TP = 1,
        /// <summary>
        /// Both
        /// </summary>
        AM = 2
    }

    #region Recharge type

    /// <summary>
    /// Recharge Type
    /// jeffrey modified 18/03/2010
    /// </summary>
    public enum eRechargeType
    {
        /// <summary>
        /// Coupon recharge by WS
        /// </summary>
        RCP = 0,
        /// <summary>
        /// Balance adjustment
        /// </summary>
        ASL = 1,
        /// <summary>
        /// Coupon recharge annulment
        /// </summary>
        ANC = 2,
        /// <summary>
        /// Coupon recharge by USSD
        /// </summary>
        RCU = 3,
        /// <summary>
        /// Coupon recharge by IVR
        /// </summary>
        RCI = 4,
        /// <summary>
        /// recharge by ATM
        /// </summary>
        ATM = 5,
        /// <summary>
        /// ATM network 1 id. (la Caixa) 
        /// </summary>
        ATM1 = 51,
        /// <summary>
        /// ATM network 2 id. (Santander 4B) 
        /// </summary>
        ATM2 = 52,
        /// <summary>
        /// ATM network 3 id. (SERPEMA)- future use 
        /// </summary>
        ATM3 = 53,
        /// <summary>
        /// ATM network 3 id. (CECA)-> future use 
        /// </summary>
        ATM4 = 54,
        /// <summary>
        /// recharge by WEB
        /// </summary>
        WEB = 6,
        /// <summary>
        /// Coupon recharge by Client
        /// </summary>
        RCC = 7,
        /// <summary>
        /// Coupon recharge by SMS
        /// </summary>
        RCS = 8,
        /// <summary>
        /// CRM Call Center Credit Topup
        /// </summary>
        CCC = 9,
        /// <summary>
        /// ATM network 1 id. (la Caixa) 
        /// </summary>
        CCC1 = 91,
        /// <summary>
        /// ATM network 2 id. (Santander 4B) 
        /// </summary>
        CCC2 = 92,
        /// <summary>
        /// ATM network 3 id. (SERPEMA)- future use 
        /// </summary>
        CCC3 = 93,
        /// <summary>
        /// ATM network 3 id. (CECA)-> future use 
        /// </summary>
        CCC4 = 94,
        /// <summary>
        /// Recharge annulment
        /// </summary>
        ANR = 10,
        /// <summary>
        /// atm topup cancellation
        /// </summary>
        ACM = 11,
        /// <summary>
        /// atm topup check
        /// </summary>
        ATC = 12,
        /// <summary>
        /// client balanceadjustment
        /// </summary>
        ACL = 13,
        /// <summary>
        /// Recharge annulment in client
        /// </summary>
        ACR = 14,
        /// <summary>
        /// Friends & Family Initial fee
        /// </summary>
        FFI = 21,
        /// <summary>
        /// Friends & Family Change fee
        /// </summary>
        FFC = 22,
        /// <summary>
        /// Friends & Family Monthly fee
        /// </summary>
        FFM = 23,
        /// <summary>
        /// Promotion rebate
        /// </summary>
        PromotionRebate = 24,
        /// <summary>
        /// PromotionDeduct
        /// </summary> 
        PromotionDeduct = 25,
        /// <summary>
        /// PrepayToPostpayTransfer
        /// </summary>
        PrepayToPostpayTransfer = 26,
        /// <summary>
        /// LBSEventCostCharge
        /// </summary>
        LbSEventCostCharge = 27,
        /// <summary>
        /// Unknown
        /// </summary>
        UNKNOWN = 101
    }

    #endregion

    /// <summary>
    /// BRS 货币所写参数
    /// jeffrey 30/10/2008
    /// </summary>
    public enum eCurrencyShort
    {
        /// <summary>
        /// EUR
        /// </summary>
        E = 1
    }

    /// <summary>
    /// Description:表示MNP 的原因的类型，与表CRM_MNP_REASON对应
    /// Author: James.Zhan
    /// Date:2010-01-05
    /// </summary>
    public enum PortabilityReasonType
    {
        Cancel = 1,
        Reject = 2,
        CanceledDeletion = 3
    }

    /// <summary>
    /// Description:表示MNP的各种状态类型，与表CRM_MNP_PORTABILITY的字段status对应
    /// Author:James.Zhan
    /// Date:2010-01-05
    /// </summary>
    public enum PortabilityStatus
    {
        /// <summary>
        /// Send status
        /// </summary>
        AENV,

        /// <summary>
        /// Requested status
        /// </summary>
        ASOL,

        /// <summary>
        /// Confirmed status
        /// </summary>
        ACON,

        /// <summary>
        /// Rejected status
        /// </summary>
        AREC,

        /// <summary>
        /// Ported status
        /// </summary>
        APOR,

        /// <summary>
        /// Canceled status
        /// </summary>
        ACAN,

        /// <summary>
        /// Definitive deletion status
        /// </summary>
        BDEF,

        /// <summary>
        /// Notified deletion status
        /// </summary>
        BNOT,

        /// <summary>
        /// Canceled deletion status
        /// </summary>
        BCAN,

        /// <summary>
        /// Stopped deletion status
        /// </summary>
        BDET,

        /// <summary>
        /// Sent cancellation request after read
        /// </summary>
        ASOLC,

        /// <summary>
        /// Sent cancellation request after confirmed
        /// </summary>
        ACONC
    }

    public enum eInsertStatus
    {
        Successed = 0,
        Failed = 1,
        RecordExisted = 2
    }

    /// <summary>
    /// dealer 校验类型
    /// </summary>
    public enum eDealerMatchType
    {
        /// <summary>
        /// 相等校验
        /// </summary>
        Equal = 1,
        /// <summary>
        /// 向上校验（父级校验）
        /// </summary>
        Upper = 2,
        /// <summary>
        /// 向下校验（子级校验）
        /// </summary>
        Lower = 3
    }

    public enum TrafficType
    {
        None = -1,
        All = 0,
        International = 1,
        SuperOnNet = 2,
        Mobile = 3,
        Fixed = 4,
        DID = 5
    }

    public enum FeeType
    {
        InitialFee = 1,
        ChangeFee = 2,
        MonthlyFee = 3,
        All = -1
    }

    public enum ChargeType
    {
        PerCategory = 1,
        Fixed = 2,
        PerNumber = 3
    }

    public enum eResultType
    {
        Correct = 0,
        DataValidationFailure = 1,
        RequestProcessed = 2,
        LogicError = 3,
        InternalError = 4,
        AuthenticationValidationFailure = 5
    }

    /// <summary>
    /// CustomerFFServiceStatus Values
    /// Brain 08/18/2009
    /// </summary>
    public enum eFFServiceStatusValues
    {
        Pause = 0,
        Active = 1,
        Expired = 2,
        Deactive = 3
    }

    #region TT API
    public enum TTRoleEnum : int
    {
        TTR_CALL_CENTRE = 2382,
        TTR_HELP_DESK = 2383,
        TTR_ADMIN = 2384,
        TTR_COORDINATOR = 2385,
        TTR_VIZZAVI = 2386
    }

    public enum TTActionEnum : int
    {
        TTA_OPEN_TICKET = 1,
        TTA_CLOSE_TICKET,
        TTA_ASSOCIATE_TICKET,
        TTA_FORWARD_TO_HELP_DESK,
        TTA_FORWARD_TO_VIZZAVI,
        TTA_FORWARD_TO_ADMINISTRATION,
        TTA_FORWARD_TO_COORDINATOR,
        TTA_FORWARD_WITHIN_VIZZAVI,
        TTA_ASK_FOR_ADDITIONAL_INFO,
        TTA_ADD_ADDITIONAL_INFO,
        TTA_ASK_FOR_VERIFICATION,
        TTA_REOPEN_TICKET,
        TTA_REJECT_TICKET
    }

    public enum TTStatusEnum : int
    {
        TTS_Open,
        TTS_Associated,
        TTS_Rejected,
        TTS_Pending_Verification,
        TTS_Pending_Information,
        TTS_Closed,
        TTS_Resolved
    }

    public enum TTRequestTypeEnum
    {
        MAS = 0,
        TP = 1,
        AM
    }
    #endregion

    #region LBS

    public enum eLBSStatus
    {
        ServiceOn = 1,
        ServiceOff = 2
    }

    public enum eLBSChargeFlag
    {
        ChargeOn = 1,
        ChargeOff = 2
    }

    public enum eLBSChargeMode
    {
        MonthlyCharge = 1,
        EventCost = 2,
        MonthlyChargeAndEventCost = 3
    }

    public enum eLBSTracerType
    {
        EndUser = 1,
        CompanyId = 2
    }

    public enum eLBSAuthorizeStatus
    {
        Valid = 1,
        Expired = 2
    }

    #endregion


    public enum APIOperationCode
    {

        #region from CRM2.0 jeffrey

        #region provisioning
        /// <summary>
        /// VOUCHER RANGE ACTIVATION
        /// </summary>
        ARC,
        /// <summary>
        /// VOUCHER STATUS ALTERATION
        /// </summary>
        MEC,
        /// <summary>
        /// VOUCHER RECHARGE MSISDN UNBLOCK
        /// </summary>
        DRC,
        /// <summary>
        /// VOUCHER RECHARGE 
        /// </summary>
        RCP,
        /// <summary>
        /// BALANCE ADJUSTMENT 
        /// </summary>
        ASL,
        /// <summary>
        /// VOUCHER RECHARGE ANNULMENT 
        /// </summary>
        ANC,
        /// <summary>
        /// GSM SERVICES ALTERATION 
        /// </summary>
        SVG,
        /// <summary>
        /// CALL FORWARD ALTERATION
        /// </summary>
        DSV,
        /// <summary>
        /// Single subscriber deletion
        /// </summary>
        BIS,
        /// <summary>
        /// Price plans query
        /// </summary>
        CPP,
        /// <summary>
        /// SENDING OF SMS 
        /// </summary>
        SMS,
        /// <summary>
        /// SIM MANUFACTURE 
        /// </summary>
        FBS,
        /// <summary>
        /// Golden numbers query
        /// </summary>
        CNG,
        /// <summary>
        /// PREPAYMENT PRE-ACTIVATION 
        /// </summary>
        PPE,
        /// <summary>
        /// POSTPAYMENT PRE-ACTIVATION 
        /// </summary>
        PPO,
        /// <summary>
        /// ATM cancellation
        /// </summary>
        ACM,
        /// <summary>
        /// OTA ACTIVATION
        /// </summary>
        OTA,
        /// <summary>
        /// PREPAYMENT RECHARGE 
        /// </summary>
        RPE,
        /// <summary>
        /// FIRST USE ACTIVATION 
        /// </summary>
        APU,
        /// <summary>
        /// Old number validation
        /// </summary>
        VNA,
        /// <summary>
        /// PREPAYMENT DEACTIVATION 
        /// </summary>
        DSP,
        /// <summary>
        /// PREPAYMENT EXPIRY
        /// </summary>
        EXP,
        /// <summary>
        /// PREPAYMENT DELETION
        /// </summary>
        BRP,
        /// <summary>
        /// Profile network query
        /// </summary>
        CPR,
        /// <summary>
        /// LANGUAGE CHANGE 
        /// </summary>
        CID,
        /// <summary>
        /// PREPAYMENT FREEZING
        /// </summary>
        FRZ,
        /// <summary>
        /// PREPAYMENT DEFREEZING 
        /// </summary>
        DFR,
        /// <summary>
        /// Free numbers query
        /// </summary>
        CNL,
        /// <summary>
        /// Subscriber query
        /// </summary>
        CSU,
        /// <summary>
        /// PREACTIVATED SIM ERASURE 
        /// </summary>
        BSP,
        /// <summary>
        /// MSISDN CHANGE 
        /// </summary>
        CMS,
        /// <summary>
        /// TARIFF PLAN CHANGE 
        /// </summary>
        CPT,
        /// <summary>
        /// SPECIFIC PREPAYMENT PREACTIVATION 
        /// </summary>
        PEP,
        /// <summary>
        /// SPECIFIC POSTPAYMENT PREACTIVATION
        /// </summary>
        POP,
        /// <summary>
        /// MIGRATION TO POSTPAYMENT 
        /// </summary>
        MPO,
        /// <summary>
        /// MIGRATION TO PREPAYMENT 
        /// </summary>
        MPE,
        /// <summary>
        /// Screen printing
        /// </summary>
        SER,
        /// <summary>
        /// Status operation query.
        /// </summary>
        CEO,
        /// <summary>
        /// Voucher status query
        /// </summary>
        VSQ,
        /// <summary>
        /// Msisdn block query
        /// </summary>
        MBQ,
        /// <summary>
        /// ATM operations
        /// </summary>
        ATM,
        /// <summary>
        /// Available msisdn query
        /// </summary>
        AMQ,
        /// <summary>
        /// Recharge annulment
        /// </summary>
        ANR,
        /// <summary>
        /// Recharge Histor yQuery
        /// </summary>
        RHQ,
        /// <summary>
        ///  CRM Call Center Credit Topup
        /// </summary>
        CCC,
        /// <summary>
        /// ATM topup check
        /// </summary>
        ATC,
        /// <summary>
        /// Add dealer
        /// </summary>
        ADI,
        /// <summary>
        /// Modify dealer info
        /// </summary>
        MDI,
        /// <summary>
        /// Delete dealer
        /// </summary>
        DDI,
        /// <summary>
        /// Query dealer info
        /// </summary>
        QDI,
        /// <summary>
        /// Query customer id by msisdn
        /// </summary>
        QCI,

        #endregion

        #region portability
        /// <summary>
        /// PORTABILITY 
        /// </summary>
        POR,
        /// <summary>
        /// PORTABILITY REFUSAL 
        /// </summary>
        REP,
        /// <summary>
        /// PORTABILITY DEFINITIVE ERASURE  
        /// </summary>
        BDP,
        /// <summary>
        /// OUTGOING PORTABILITY ACCEPTANCE    
        /// </summary>
        APS,
        /// <summary>
        /// Cancellation portability
        /// </summary>
        CAP,
        /// <summary>
        /// Status portability query
        /// </summary>
        CPO,
        /// <summary>
        /// Cancellation by donating query
        /// </summary>
        CDQ,
        /// <summary>
        /// Incoming portability cancellation by donating
        /// </summary>
        CBD,

        PORACCP,                // PortIn Accept
        PORREJE,                // PortIn Reject
        PORAHLD,                // PortIn Hold
        PORACK,                // PortIn Ack
        PORERRN,                // PortIn ErrorNotification
        #endregion

        #region INCIDENCE
        /// <summary>
        /// INCIDENCE REGISTRATION
        /// </summary>
        AIN,
        /// <summary>
        /// INCIDENCE MODIFICATION 
        /// </summary>
        MIN,
        /// <summary>
        /// INCIDENCE CREATION 
        /// </summary>
        CRI,
        /// <summary>
        /// INCIDENCE CLOSURE
        /// </summary>
        CII,
        /// <summary>
        /// “Masivas” incidence query
        /// </summary>
        CIM,
        /// <summary>
        /// INCIDENCE REALLOCATION FROM VMO
        /// </summary>
        RIN,
        /// <summary>
        /// INCIDENCE REALLOCATION 
        /// </summary>
        REI,
        /// <summary>
        /// QueryIncidences
        /// </summary>
        CIN,
        /// <summary>
        /// Incidence closure from vmo
        /// </summary>
        CFM,
        /// <summary>
        /// QueryTroubleType
        /// </summary>
        GTT,
        /// <summary>
        /// QuerySubtroubleType
        /// </summary>
        GST,
        #endregion

        #endregion

        #region CRM 2.5
        /// <summary>
        /// HLR Provisioining Setting
        /// </summary>
        HPS,
        /// <summary>
        /// VOUCHER RANGE ACTIVATION
        /// </summary>


        /// <summary>
        /// SIM ERASURE 
        /// </summary>
        BYES,
        /// <summary>
        /// PREACTIVATED SIM ERASURE 
        /// </summary>
        CYES,
        /// <summary>
        /// TARIFF PLAN CHANGE 
        /// </summary>

        #region INCIDENCE
        /// <summary>
        /// QueryIncidences
        /// </summary>
        GII,
        #endregion
        /// <summary>
        /// REGISTER
        /// </summary>
        REG,
        /// <summary>
        /// REGISTER Extra SIM Card to Customer
        /// </summary>
        REGSIM,
        /// <summary>
        /// Create MultiDevice Group
        /// </summary>
        NEWMD,
        /// <summary>
        /// Update MultiDevice Group
        /// </summary>
        UPDMD,
        /// <summary>
        /// Delete MultiDevice Group
        /// </summary>
        DELMD,
        /// <summary>
        /// Query MultiDevice Group
        /// </summary>
        QRYMD,
        /// <summary>
        /// Add SIM to MultiDevice Group
        /// </summary>
        ADDSIMMD,
        /// <summary>
        /// Add SIM to MultiDevice Group
        /// </summary>
        MODSIMMD,
        /// <summary>
        /// Remove SIM to MultiDevice Group
        /// </summary>
        REMSIMMD,
        /// <summary>
        /// CHANGE VOICE MAIL STATUS
        /// </summary>
        VMS,
        /// <summary>
        /// GET CUSTOMER INFORMATION
        /// </summary>
        GCI,
        /// <summary>
        /// MODIFY CUSTOMER INFORMATION
        /// </summary>
        MCI,
        /// <summary>
        /// GET CUSTOMER BANK INFORMATION
        /// </summary>
        GCBI,
        /// <summary>
        /// MODIFY CUSTOMER BANK INFORMATION
        /// </summary>
        MCBI,
        /// <summary>
        /// GET CUSTOMER ADDRESS INFORMATION
        /// </summary>
        GCAI,
        /// <summary>
        /// MODIFY CUSTOMER ADDRESS INFORMATION
        /// </summary>
        MCAI,
        /// <summary>
        /// MODIFY CUSTOMER ALERT QUANTITY
        /// </summary>
        MCAQ,

        /// <summary>
        /// GET CUSTOMER RESOURCE
        /// </summary>
        GCRE,
        /// <summary>
        /// GET CUSTOMER SERVICES
        /// </summary>
        GCSE,
        /// <summary>
        /// CHANGE PASSWORD
        /// </summary>
        CHP,
        /// <summary>
        /// GET ROAMING STATUS
        /// </summary>
        GRS,
        /// <summary>
        /// MODIFY ROAMING STATUS
        /// </summary>
        MRS,
        /// <summary>
        /// ADD CUSTOEMR BANK INFORMATION
        /// </summary>
        ACBI,
        /// <summary>
        /// MODIFY DID NUMBER
        /// </summary>
        MDID,
        /// <summary>
        /// ADD DID NUMBER
        /// </summary>
        ADID,
        /// <summary>
        /// DELETE DID NUMBER
        /// </summary>
        DDID,
        /// <summary>
        /// MODIFY SUPPLEMENTARY SERVICES
        /// </summary>
        MSS,
        /// <summary>
        /// GET RAMDOM FREE MSISDNS
        /// </summary>
        GRM,
        /// <summary>
        /// Query Countries
        /// </summary>
        GAC,
        /// <summary>
        /// Query Provinces
        /// </summary>
        GPR,
        /// <summary>
        /// QueryIDTypes
        /// </summary>
        GID,
        /// <summary>
        /// QueryNationalities
        /// </summary>
        GNA,
        /// <summary>
        /// QueryLanguage
        /// </summary>
        GLA,
        /// <summary>
        /// QueryPackages
        /// </summary>
        GPA,
        ///<summary>
        ///QuertyTemplage 
        /// </summary>>
        GTA,
        /// <summary>
        /// QuerySupplementaryServices
        /// </summary>
        GSS,
        /// <summary>
        /// QueryNetworkProfile
        /// </summary>
        GNP,
        /// <summary>
        /// Find password
        /// </summary>
        GPS,
        #region FF
        /// <summary>
        /// AlterAllCustomerFFNumbers
        /// </summary>
        //old api operation code 
        //AAFF,
        CFFANM,
        /// <summary>
        /// QueryFFChargeHistory
        /// </summary>
        //old api operation code 
        //GFFC,
        CFFCFHQ,
        /// <summary>
        /// AddCustomerFFNumbers
        /// </summary>
        //old api operation code 
        //AFFN,
        CFFNA,
        /// <summary>
        /// DeleteCustomerFFNumbers
        /// </summary>
        //old api operation code 
        //DFFN,
        CFFND,
        /// <summary>
        /// ModifyCustomerFFNumbers
        /// </summary>
        //old api operation code 
        // MFFN,
        CFFNM,
        /// <summary>
        /// QueryCustomerFFNumbers
        /// </summary>
        //old api operation code 
        //GFFN,
        CFFNQ,
        /// <summary>
        /// ActivateCustomerFFService
        /// </summary>
        //old api operation code 
        //AFFS,
        CFFSA,
        /// <summary>
        /// DeactivateCustomerFFService
        /// </summary>
        //old api operation code 
        //DFFS,
        CFFSDA,
        /// <summary>
        /// QueryCustomerFFServiceStatus
        /// </summary>
        //old api operation code 
        //GFFS,
        CFFSSQ,
        /// <summary>
        /// ActivateMVNOFFService
        /// </summary>
        MFFA,
        /// <summary>
        /// DeactivateMVNOFFService
        /// </summary>
        MFFDEA,
        /// <summary>
        /// AddMVNOFFServiceFee
        /// </summary>
        //old api operation code 
        //AMFF,
        MFFFA,
        /// <summary>
        /// DeleteMVNOFFServiceFee
        /// </summary>
        //old api operation code 
        // DMFF,
        MFFFD,
        /// <summary>
        /// ModifyMVNOFFServiceFee
        /// </summary>
        //old api operation code 
        //MMFF,
        MFFFM,
        /// <summary>
        /// QueryMVNOFFServiceFees
        /// </summary>
        //old api operation code
        //GMFF,
        MFFFQ,
        /// <summary>
        /// QueryMVNOFFServiceSettings
        /// </summary>
        //old api operation code 
        //GMSE,
        MFFSQ,
        /// <summary>
        /// ModifyMVNOFFServiceSettings
        /// </summary>
        //old api operation code 
        //MMSE,
        MFFM,
        /// <summary>
        /// QueryMVNOFFServiceStatus
        /// </summary>
        //old api operation code 
        //GMSS,
        MFFSSQ,
        #endregion
        /// <summary>
        /// LoginGendarme
        /// </summary>
        LGD,
        /// <summary>
        /// SendAdvanceSMS
        /// </summary>
        SAS,
        /// <summary>
        /// SendSimpleSMS
        /// </summary>
        SSS,
        /// <summary>
        /// Get custoemr Invocices
        /// </summary>
        GIV,
        /// <summary>
        /// QueryTopupHistory
        /// </summary>
        GTH,
        /// <summary>
        /// QueryCallDetails
        /// </summary>
        GCD,
        /// <summary>
        /// QuerySubServices
        /// </summary>
        GSUS,
        /// <summary>
        /// Get balance
        /// </summary>
        GBA,
        /// <summary>
        /// GET UNBILLED BALANCE
        /// </summary>
        GUBA,
        /// <summary>
        /// GET RATES
        /// </summary>
        GRA,
        /// <summary>
        /// GET VOICE MAIL STATUS
        /// </summary>
        GVMS,
        /// <summary>
        /// Login
        /// </summary>
        LIN,
        /// <summary>
        /// LOGOUT
        /// </summary>
        LOU,
        /// <summary>
        /// Change icc
        /// </summary>
        CSI,
        /// <summary>
        /// Get simcard ss
        /// </summary>
        GSIM,
        /// <summary>
        /// GET VOUCHER STATUS
        /// </summary>
        GVS,
        /// <summary>
        /// QueryBlockMsisdnDueToFrau
        /// </summary>
        GFBM,
        /// <summary>
        /// QueryDIDNumber
        /// </summary>
        GDID,
        /// <summary>
        /// QueryBlockMsisdn
        /// </summary>
        GBM,
        /// <summary>
        /// GET OPERATIOS
        /// </summary>
        GOPS,
        /// <summary>
        /// PayMonthly
        /// </summary>
        PAY,
        /// <summary>
        /// GetBalanceInfo
        /// </summary>
        GBI,
        /// <summary>
        /// FlexUserRegister
        /// </summary>
        FRE,
        /// <summary>
        /// GetNewUpdateCustomerIdList
        /// </summary>
        NUC,
        /// <summary>
        /// CheckMainNumber
        /// </summary>
        CMN,
        /// <summary>
        /// CheckIsPrePaidMSISDN
        /// </summary>
        IPP,
        /// <summary>
        /// GetVoucherCode
        /// </summary>
        GVC,
        /// <summary>
        /// SetChildrenCreditLimit
        /// Added by Liny,2010-05-06
        /// </summary>
        SCCL,
        /// <summary>
        /// QueryChildrenSecurityNumber
        /// Added by Liny,2010-05-06
        /// </summary>
        QCSN,
        /// <summary>
        /// DeleteChildrenSecurityNumber
        /// Added by Liny,2010-05-06
        /// </summary>
        DCCN,
        /// <summary>
        /// ModifyChildrenSecurityNumber
        /// Added by Liny,2010-05-06
        /// </summary>
        MCCN,
        /// <summary>
        /// AddChildrenSecurityNumber
        /// Added by Liny,2010-05-06
        /// </summary>
        ACCN,
        /// <summary>
        /// request porting in
        /// Added by Liny,2010-05-07
        /// </summary>
        RTPI,
        /// <summary>
        /// query portin record status
        /// Added by Liny,2010-05-07
        /// </summary>
        RTQS,
        /// <summary>
        /// query all network operator 
        /// </summary>
        QANO,
        /// <summary>
        /// query all service provider
        /// </summary>
        QASP,

        /// <summary>
        /// add by Ben 2014-9-24 for telkcom F22  CheckLastTransactionCost
        /// </summary>
        CLTC,
        #endregion


        /// <summary>
        /// REGISTER Ben Customer
        /// Added by Rabi ,2010-06-07
        /// </summary>
        BRG,
        /// <summary>
        /// query extra customer infomation by ben
        /// Added by Liny,2010-06-07
        /// </summary>
        GECI,
        /// <summary>
        /// modify extra customer infomation by ben
        /// Added by Liny,2010-06-07
        /// </summary>
        MECI,
        /// <summary>
        /// query the bundle information of a customer occording to msisdn by ben
        /// Added by Liny,2010-06-07
        /// </summary>
        QBDS,
        /// <summary>
        /// query the promotion information of a customer occording to msisdn by ben
        /// Added by Liny,2010-06-07
        /// </summary>
        QPTS,
        /// <summary>
        /// query the pin of a customer occording to msisdn by ben
        /// Added by Liny,2010-06-07
        /// </summary>
        QPIN,
        /// <summary>
        /// query the puk of a customer occording to msisdn by ben
        /// Added by Liny,2010-06-07
        /// </summary>
        QPUK,
        /// <summary>
        /// for PortingRequestChk api invoked by portingXS
        /// Added by Liny,2010-07-19
        /// </summary>
        PRCHK,
        /// <summary>
        /// for PortingRequestAnswer api invoked by portingXS
        /// Added by Liny,2010-07-19
        /// </summary>
        PRANS,
        /// <summary>
        /// for PortingRestation api invoked by portingXS
        /// Added by Liny,2010-07-19
        /// </summary>
        PRRES,
        /// <summary>
        /// for ben QueryPortingStatus Api by Msisdn
        /// Added by rabi,2010-08-19
        /// </summary>
        QPSM,
        /// <summary>
        /// CreditLimit adjustment for postpaid customer
        /// </summary>
        BFP,
        /// <summary>
        /// ActiveSIMSFromInit For Ecofoon
        /// </summary>
        ASI,

        #region IVR
        /// <summary>
        /// Check Balance
        /// </summary>
        CBL,
        /// <summary>
        /// Set Customer Language
        /// </summary>
        SCL,
        /// <summary>
        /// GetMultilingualUSSDText
        /// </summary>
        GMU,
        /// <summary>
        /// SubscribePromotion
        /// </summary>
        SPM,
        /// <summary>
        /// Receive Ben Email
        /// </summary>
        RBE,

        #endregion


        #region Ben

        /// <summary>
        /// For selfcare api,UpdateCustomerAndEmail
        /// Added by Liny,2010-08-23
        /// </summary>
        UCA,

        /// <summary>
        /// For selfcare api,AuthenticateCustomerForBen
        ///  Added by Liny,2010-08-23
        /// </summary>
        ACF,

        /// <summary>
        /// For selfcare api,ActiveBonusForBen
        ///  Added by Liny,2010-08-23
        /// </summary>
        ABF,

        /// <summary>
        /// For selfcare api,AddPromotionForBen
        ///  Added by Liny,2010-08-25
        /// </summary>
        APF,

        /// <summary>
        /// For selfcare api,RequestPortinBen
        ///  Added by Liny,2010-09-24
        /// </summary>
        RPTB,
        /// <summary>
        /// For Liquix api,AddPaymentInfo
        ///  Added by darren,2010-09-28
        /// </summary>
        BAP,

        /// <summary>
        /// For Liquix api,Diable subscription
        ///  Added by darren,2010-09-28
        /// </summary>
        BDS,

        /// <summary>
        /// For Liquix api,Query simcard status
        ///  Added by darren,2010-09-28
        /// </summary>
        BQS,

        #endregion

        #region VPN
        /// <summary>
        /// VPN Create
        /// </summary>
        VC,
        /// <summary>
        /// VPN Update
        /// </summary>
        VU,
        /// <summary>
        /// VPN Delete
        /// </summary>
        VD,
        /// <summary>
        /// VPN Query
        /// </summary>
        VQ,
        /// <summary>
        /// VPN Group Create
        /// </summary>
        VGC,
        /// <summary>
        /// VPN Group Update
        /// </summary>
        VGU,
        /// <summary>
        /// VPN Group Delete
        /// </summary>
        VGD,
        /// <summary>
        /// VPN Group Query
        /// </summary>
        VGQ,
        /// <summary>
        /// VPN ShortCode Create
        /// </summary>
        VSCC,
        /// <summary>
        /// VPN ShortCode List Create
        /// </summary>
        VSCCL,
        /// <summary>
        /// VPN ShortCode Update
        /// </summary>
        VSCU,
        /// <summary>
        /// VPN ShortCode Delete
        /// </summary>
        VSCD,
        /// <summary>
        /// VPN ShortCode Query
        /// </summary>
        VSCQ,
        /// <summary>
        /// VPN User(Subscriber) Create
        /// </summary>
        VUC,
        /// <summary>
        /// VPN User(Subscriber) Update
        /// </summary>
        VUU,
        /// <summary>
        /// VPN User(Subscriber) Delete
        /// </summary>
        VUD,
        /// <summary>
        /// VPN User(Subscriber) Query
        /// </summary>
        VUQ,
        /// <summary>
        /// VPN User(ResmbHumtingGroupDest) Create
        /// </summary>
        VDC,
        /// <summary>
        /// VPN User(ResmbHumtingGroupDest) Update
        /// </summary>
        VDU,
        /// <summary>
        /// VPN User(ResmbHumtingGroupDest) Delete
        /// </summary>
        VDD,
        /// <summary>
        /// VPN User(ResmbHumtingGroupDest) Query
        /// </summary>
        VDQ,
        /// <summary>
        /// VPN User(ResmbHumtingGroup) Create
        /// </summary>
        VRC,
        /// <summary>
        /// VPN User(ResmbHumtingGroup) Update
        /// </summary>
        VRU,
        /// <summary>
        /// VPN User(ResmbHumtingGroup) Delete
        /// </summary>
        VRD,
        /// <summary>
        /// VPN User(ResmbHumtingGroup) Query
        /// </summary>
        VRQ,
        /// <summary>
        /// VPN User(HumtingGroupSetting) Create
        /// </summary>
        VGSC,
        /// <summary>
        /// VPN User(HumtingGroupSetting) Update
        /// </summary>
        VGSU,
        /// <summary>
        /// VPN User(HumtingGroupSetting) Delete
        /// </summary>
        VGSD,
        /// <summary>
        /// VPN User(HumtingGroupSetting) Query
        /// </summary>
        VGSQ,
        #endregion

        #region PromotionManagement
        /// <summary>
        /// AddCustomerPromotion
        /// </summary>
        ACP,
        /// <summary>
        /// DeleteCustomerPromotion
        /// </summary>
        DCP,
        /// <summary>
        /// DeleteAllPromotionsOfCusotmer
        /// </summary>
        DACP,
        /// <summary>
        /// ModifyCustomerPromotion
        /// </summary>
        MCP,
        /// <summary>
        /// QueryCustomerPromotions
        /// </summary>
        QCP,
        /// <summary>
        /// Query MVNO promotions
        /// </summary>
        QMP,
        #endregion
        #region Transfer Balance
        /// <summary>
        /// TransferBalanceIn
        /// </summary>
        TBAI,
        /// <summary>
        /// TransferBalanceOut
        /// </summary>
        TBAO,
        #endregion
        #region Multi IMSI Or MSISDN
        /// <summary>
        /// ChangeMultiIMSIOrMSISDN
        /// </summary>
        CMM,
        /// <summary>
        /// QueryMultiIMSIOrMSISDN
        /// </summary>
        QMM,
        #endregion

        #region Provisioning Template
        /// <summary>
        /// QueryProvisioningTemplates
        /// </summary>
        QPRT,
        /// <summary>
        /// ChangeProvisioningTemplate
        /// </summary>
        CPRT,
        #endregion

        /// <summary>
        /// Reset unbilled balance
        /// </summary>
        RUB,

        /// <summary>
        /// Corresponding to SettingBarringForBen api
        /// Added by Liny,2011-03-31
        /// </summary>
        SBFB,

        #region Network settings
        /// <summary>
        /// Change BS TS
        /// </summary>
        CBT,
        /// <summary>
        /// Change Barring
        /// </summary>
        CBA,
        /// <summary>
        /// ChangeAPNs
        /// </summary>
        CAPN,
        /// <summary>
        /// QueryAPNPdpContextIds
        /// </summary>
        QAP,

        #endregion
        /// <summary>
        /// Create Voucher Cards
        /// </summary>
        CVC,
        /// <summary>
        /// Get Encrypted Code Of Voucher Batch
        /// </summary>
        GVB,
        /// <summary>
        /// AddBlackMember
        /// </summary>
        ABM,
        /// <summary>
        /// 
        /// </summary>
        RBM,
        /// <summary>
        /// ChangeCustomerStatus
        /// </summary>
        CCS,
        /// <summary>
        /// Query Customer Status
        /// </summary>
        QCS,

        #region PremiumSMS
        SMO,
        SMT,
        DRP,
        CSR,
        TSR,
        CSRI,
        TSRI,
        #endregion

        //By Gary 2011.7.8
        /// <summary>
        /// BONUS REGISTRATION
        /// </summary>
        ABO,
        /// <summary>
        /// BONUS DELETION
        /// </summary>
        BBO,

        /// <summary>
        /// for QueryAPNofMSISDN API
        /// </summary>
        QAPNM,

        /// <summary>
        /// for RegisterAPN API
        /// </summary>
        REGAPN,

        /// <summary>
        /// for ModifyAPN API
        /// </summary>
        MODAPN,

        /// <summary>
        /// for ModifyIPofAPNForMSISDN API
        /// </summary>
        MIPAPN,

        /// <summary>
        /// for QueryAvailableStaticIPs API
        /// </summary>
        QASIP,

        /// <summary>
        /// for DeleteAPNofMSISDN API
        /// </summary>
        DELAPN,

        /// <summary>
        /// for SetQosOfMSISDN API
        /// </summary>
        SETQOS,

        /// <summary>
        /// for ResetQosOfMSISDN API
        /// </summary>
        DEFQOS,
        /// <summary>
        /// SetupLowBalanceNotification API
        /// Added by Liny,2012-03-8,for Lebara automatic top-up service
        /// </summary>
        SLBN,
        //Added by liny,2012-08-20,for Data Roaming Limit project.

        /// <summary>
        /// DataRoamingLimitSubscription
        /// </summary>
        DRLS,

        /// <summary>
        /// DataRoamingLimitUnsubscription
        /// </summary>
        DRCN,

        /// <summary>
        /// DataRoamingContinueNotification
        /// </summary>
        DRLU,

        /// <summary>
        /// DataRoamingLimitQuery
        /// </summary>
        DRLQ,

        /// <summary>
        /// DataRoamingLimitModification
        /// </summary>
        DRLM,

        /// <summary>
        /// Purchase friends & family
        /// </summary>
        PFF,

        //End-Added by liny,2012-08-20,for Data Roaming Limit project.

        #region Added by Liny,2012-09-11,for RIAN project I
        /// <summary>
        /// for QueryMsisdnsByString API
        /// </summary>
        QMBS,
        /// <summary>
        /// for ChangeFakeIcc API
        /// </summary>
        CFIC,
        /// <summary>
        /// Modify Barrings status
        /// </summary>
        MBS,
        /// <summary>
        /// Query Barrings status
        /// </summary>
        QBS,
        #endregion

        #region Added by Liny,2012-10-12,for RIAN project II

        /// <summary>
        /// for QueryRoamingSpendingNotification API
        /// </summary>
        QRSN,

        /// <summary>
        /// for ModifyRoamingSpendingNotification API
        /// </summary>
        MRSN,

        /// <summary>
        /// for QuerySpendingNotification API
        /// </summary>
        QISN,

        /// <summary>
        /// for ModifySpendingNotification API
        /// </summary>
        MISN,

        /// <summary>
        /// for QueryMonthlyTotalSpending API
        /// </summary>
        QRIS,

        #endregion

        #region Added by winson,2012-10-21,for RIAN IMEI report
        /// <summary>
        /// for QueryRoamingSpendingNotification API
        /// </summary>
        IMEI,

        #region Modified by Liny,2013-05-27, for SRS for DMC V0.3.docx,to call the interface of DMC
        /// <summary>
        /// for NotifyImeiInfo API
        /// Because the field SHORTCODE of table CRM_DRE_OPERATION_LOG has maximum lenth is 3 characters.
        /// if Using the enum IMEI listed above,the log will always be saved failed:String or binary data would be truncated.
        /// if make sure that no other api use the enum : IMEI,please remove it.
        /// </summary>
        NII,
        #endregion

        #endregion

        #region Added by Liny,2012-10-22,for RIAN project III

        /// <summary>
        /// for SetPCRFServiceStatus API
        /// </summary>
        SPSS,

        /// <summary>
        /// for QueryPCRFServiceStatus API
        /// </summary>
        QPSS,

        /// <summary>
        /// for SetFreeWebsiteThreshold API
        /// </summary>
        SFWT,

        /// <summary>
        /// for QueryFreeWebsiteThreshold API
        /// </summary>
        QFWT,

        /// <summary>
        /// for OverThresholdNotification API
        /// </summary>
        OTN,

        /// <summary>
        /// for QueryGSNIPAddress API
        /// </summary>
        QGI,

        #endregion
        
        #region merged from VF_NL_Saudi_dev branch,2013-01-24,by Liny
        /// <summary>
        /// IDVerification API
        /// Added by Liny,2012-11-27,for SRS for ZAIN Verification on Recharge
        /// </summary>
        IDVF,

        #region Added by Liny,2013-01-17 , for SRS for Balance Transfer V0.7

        /// <summary>
        /// IDVerificationRequest API
        /// </summary>
        IDVR,

        /// <summary>
        /// BalanceTransferRequest API
        /// </summary>
        BLTR,
        INTELBLTR,
        #endregion
        #endregion

        #region Added by damon,2013-05-09, for saudi upload id scan
        UPID,
        RMID,
        RTID,
        #endregion
        #region Adding by Liny,2013-04-27,for new API: CreateExternalResource

        /// <summary>
        /// for CreateExternalResource API
        /// </summary>
        CVER,

        #endregion
        
        #region Added by Liny,2013-05-15,for SRS:SRS for Neosky - API Spec for VPN External Resource V1.1.docx

        /// <summary>
        /// for QueryExternalResource API
        /// </summary>
        QVER,

        /// <summary>
        /// for ModifyExternalResource API
        /// </summary>
        MVER,

        /// <summary>
        /// for DeleteExternalResource API
        /// </summary>
        DVER,

        #endregion
        
        /// <summary>
        /// Active sim by temp imsi
        /// </summary>
        ASBT,
        /// <summary>
        /// authorizePortOut API
        /// Added by Liny,2013-10-23,for external port-out process
        /// </summary>
        ATPO,
        /// <summary>
        /// queryLocationKey API
        /// Added by Liny,2013-10-30,request from Rafael and gary
        /// </summary>
        QLTK,
        /// <summary>
        /// queryDealerByMsisdn API
        /// Added by Liny,2013-10-30,request from Rafael and gary
        /// </summary>
        QDBM,

        #region IUSACELL API

        ICCCI,
        ICCTT,
        ICCRLB,
        ICCATP,
        ICCRPT,
        ICCRPD,
        ICCV,

        #endregion

        /// <summary>
        /// queryOperatorByMSISDN API
        /// Added by Francisco,2014-04-07
        /// </summary>
        QOBM,
        /// <summary>
        /// query msisdn and SimCard's status from Iccid
        /// added by neil at 20140409
        /// </summary>
        QMFICC,
        /// <summary>
        /// Change customer wallet
        /// </summary>
        CCW,
        /// <summary>
        /// msisdnStatusTransition
        /// </summary>
        MST,
        /// <summary>
        /// queryMVNOFriendsAndFamilyPlans
        /// </summary>
        QFFP,
        /// <summary>
        /// UpdateCustomer
        /// </summary>
        UC,

        /// <summary>
        /// CancelCustomerAccountOperation API Operation
        /// </summary>
        FWDCA,

        /// <summary>
        /// CreateCustomerOperation API Operation
        /// </summary>
        FWCC,

        /// <summary>
        /// LogicalCustomerDeleteOperation API Operation
        /// </summary>
        FWDC,

        /// <summary>
        /// QueryCustomerByCustomerIdOperation API Operation
        /// </summary>
        FWQCI,

        /// <summary>
        /// QueryCustomerByDocumentIdOperation API Operation
        /// </summary>
        FWQCD,

        /// <summary>
        /// QueryCustomerByExternalIdOperation API Operation
        /// </summary>
        FWQCE,

        /// <summary>
        /// UpdateCustomerOperation API Operation
        /// </summary>
        FWMCI,

        /// <summary>
        /// AddNonRecurringChargeToCustomerOperat API Operationion
        /// </summary>
        FWANRC,

        /// <summary>
        /// QueryChargeCatalogByCategoryOperation API Operation
        /// </summary>
        FWQCO,

        /// <summary>
        /// QueryChargeCatalogByChargeIdOperation API Operation
        /// </summary>
        FWQCC,

        /// <summary>
        /// QueryCustomerProductOperation API Operation
        /// </summary>
        FWQCP,

        /// <summary>
        /// QueryCustomerRecurringChargesOperation API Operation
        /// </summary>
        FWQCRC,

        /// <summary>
        /// QueryCustomerUnbilledChargesOperation API Operation
        /// </summary>
        FWQCUB,

        /// <summary>
        /// QueryProductCatalogByIdOperation API Operation
        /// </summary>
        FWQPI,

        /// <summary>
        /// QueryProductChargeOptionOperation API Operation
        /// </summary>
        FWQPCO,

        /// <summary>
        /// QueryPurchasedProductsByCustomerIdOperation API Operation
        /// </summary>
        FWQPC,

        /// <summary>
        /// AssignBundleForCustomerOperation API Operation
        /// </summary>
        FWABC,

        /// <summary>
        /// CancelSubscriptionOperation API Operation
        /// </summary>
        FWDS,

        /// <summary>
        /// ModifyDivertsOperation API Operation
        /// </summary>
        FWMD,

        /// <summary>
        /// QuerySubscriptionByMsisdnOperation API Operation
        /// </summary>
        FWQSM,

        /// <summary>
        /// ReserveMsisdn API Operation
        /// </summary>
        FWRSV,

        /// <summary>
        /// UnReserveMsisdn API Operation
        /// </summary>
        FWURSV,
        /// <summary>
        /// Query Sim Card
        /// </summary>
        QSD,
        
        /// <summary>
        /// QuerySubscriptionByCustomerIdOperation API Operation
        /// </summary>
        FWQSC,

        /// <summary>
        /// PurchaseProductForCustomerOperation API Operation
        /// </summary>
        FWPPC = 336,

        /// <summary>
        /// QueryInvoicesByCustomerId
        /// </summary>
        FWQCIN,

        /// <summary>
        /// QueryInvoiceReversalsByCustomerIdOperation
        /// </summary>
        FWQRIC,
        /// <summary>
        /// query msisdn and subcriber’s status
        /// Added by Luke,2014-11-18
        /// </summary>
        QMBIMSI,
        
        /// <summary>
        /// QueryUsageDetails API Operation
        /// </summary>
        FWQUD = 340,
        /// <summary>
        /// RemovePurchasedProduct  API Operation
        /// </summary>
        FWRPP,

        #region Added by Ego,2013-11-04,for Axiom club
        /// <summary>
        /// AddCustomerMembership,for SelfCareAPI
        /// </summary>
        ACMS,
        /// <summary>
        /// ModifyCustomerMembership,for SelfCareAPI
        /// </summary>
        MCMS,

        /// <summary>
        /// UpdateCustomerMembershipInfo,for SelfCareAPI
        /// </summary>
        UCMSI,

        /// <summary>
        /// QueryCustomerMembership,for SelfCareAPI
        /// </summary>
        QCMS,

        /// <summary>
        /// QueryCustomerMembershipHistory,for SelfCareAPI
        /// </summary>s
        QCMSH,
        #endregion

        #region Added by damon,2013-12-02
        /// <summary>
        /// Apply emergency credit
        /// </summary>
        AEE,
        #endregion

        #region Added by damon,2013=12-30
        CHBT,
        QBT,
        #endregion

        #region Added by wood, 2013-12-26
        /// <summary>
        /// AddCommissionPlan API
        /// </summary>
        ACPA,
        /// <summary>
        /// DisableCommissionPlan API
        /// </summary>
        DCPA,
        /// <summary>
        /// QueryCommissionPlans API
        /// </summary>
        QCPA,
        /// <summary>
        /// ModifyComissionPlan API
        /// </summary>
        MCPA,
        /// <summary>
        /// AddCommissionHistory API
        /// </summary>
        ACH,
        /// <summary>
        /// ModifyCommissionHistory API
        /// </summary>
        MCH,
        /// <summary>
        /// QueryCommissionHistories API
        /// </summary>
        QCH,
        #endregion

        #region Added by damon 2014-01-09
        QBAT,
        #endregion

        //modified by damon,2014-01-24
        QMFI,

        //add by damon,2014-02-07
        CMB,

        //add by damon, 2014-02-13 for QueryCustomerBySegment
        QCBS,

        // add by ben 2014-3-18 for QueryReverseChargeCall
        QRCC,

        // add by ben 2014-4-24 for QueryActivedeadlinedate
        QADLD,

        #region Added by francisco 2014-03-24
        // CreateExternalVouchers
        CREV,

        // CancelExternalVouchers
        CAEV,
        // UploadNumbers  add by Ben 2014-6-11
        UPNS,

        // EndLandingPageRedirection  add by sam 2014-06-26
        ELPR,
        #endregion
        //add by sam 2014-07-02 for QueryAssets
        QAS,
        //add by sam 2014-07-03 for QueryBarringStatus
        QBARS,
        //add by sam 2014-07-22 for QueryActivationChannel
        QAC,
        //add by sam 2014-07-22 for QueryCallmebackHistory
        QCBH,
        //add by sam 2014-07-24 for QueryCustomerDataBalance
        QCDB,
        //add by sam 2014-07-24 for QueryEmergencyCreditHistory
        QECH,
        //add by sam 2014-07-24 for QueryCreditTransferHistory
        QCTH,
        //add by sam 2014-07-28 for SendUSSDMessage
        SUSSD,
        //add by sam 2014-07-29 for QueryCustomerProfileUpdateTime
        QCPU,
        //add by sam 2014-08-07 for QueryInternationalBBRoamingStatus
        QBBS,
        //add by sam 2014-08-07 for ModifyInternationalBBRoamingStatus
        MBBS,
        #region SIMBlockingReason (SIM Blocking CR)
        //add by APB 2014-07-21 for QueryBlockingStatus
        QBSTA,
        //add by APB 2014-07-21 for QueryBlockedCustomers
        QBCUS,
        //add by APB 2014-07-21 for QueryBlockingCodes
        QBCOD,
        //add by APB 2014-07-21 for ModifyBlockingCodes
        MBCOD,
        #endregion

        #region QueryCustomerMembershipHistory
        QCMH,
        #endregion

        #region Device Specific Operation
        ADSRR,
        MDSRR,
        DDSRR,
        QDSRR,
        #endregion


        TRANSOS,//add by sam 2014-08-29 for TransferOwnership
        CTRANSO,//add by sam 2014-09-03 for ClearanceTransferOwnership
        QTOR,//add by sam 2014-09-03 for QueryTransferOwnershipRequest
        DTRO,//add by sam 2014-09-04 for DeleteTransferOwnership


        QREF,

        #region AlElm Operation
        QAAS,
        #endregion

        #region News, AnnaM: 20140926
        /// <summary>
        /// GetNews
        /// </summary>
        GNEW,
        #endregion


        QCAS,//Added by Benny 2014-11-05 for QueryCustomerAlertSetting
        MCAS //Added by Benny 2014-11-05 for ModifyCustomerAlertStatus
    }

    #endregion

    #region Service modification Jose Luis 2012-10-01
    public enum BarringTypes
    {
        PremiumVoice,
        InternationalCalls
    }
    #endregion

    public enum eAtosTTStates
    {
        MANTENER_ESTADO_ACTUAL = -999,
        CREADO = 100,
        ASOCIADO = 101,
        PENDIENTE_DEL_OMV = 102,
        PENDIENTE_DE_VERIFICACION = 103,
        RECHAZADA = 104,
        CERRADO = 105,
        EN_CURSO = 106,
        FINALIZADA = 107
    }

    /// <summary>
    /// Type of incidence source in question
    /// </summary>
    public enum eAtosFuente
    {
        General = 2,
        Individual = 1,
        TrabajoProgramado = 3,
        ConsultaFuncional = 4,
        PeticionTarea = 5
    }

    /// <summary>
    /// Identification Type
    /// (1)	If tipoAbonado is a Company, tipoIdentificacion should be CIF.
    /// If tipoAbonado is a Private Individual, tipoIdentificacion may be NIF, Resident or Passport.
    /// </summary>
    public enum eTipoIdentificacion
    {
        /// <summary>
        /// NIF(For Private)
        /// </summary>
        NIF = 0,
        /// <summary>
        /// CIF(For Business)
        /// </summary>
        CIF = 1,
        /// <summary>
        /// Resident(For Private)
        /// </summary>
        Residente = 2,
        /// <summary>
        /// Passport(For Private)
        /// </summary>
        Pasaporte = 3
    }
    /// <summary>
    /// Application Type(MNP)
    /// </summary>
    public enum eTipoSolicitud
    {
        /// <summary>
        /// Single
        /// </summary>
        Individual = 0,
        /// <summary>
        /// Multiple
        /// </summary>
        Multiple = 1
    }
    /// <summary>
    /// Payment type
    /// </summary>
    public enum eModoPago
    {
        NULL = 0,
        /// <summary>
        /// Prepayment
        /// </summary>
        Prepago = 2,
        /// <summary>
        /// Postpayment
        /// </summary>
        Postpago = 1
    }

    /// <summary>
    /// Subscriber Type
    /// </summary>
    public enum eTipoAbonado
    {
        /// <summary>
        /// Private
        /// </summary>
        Particular = 0,
        /// <summary>
        /// Business(Company)
        /// </summary>
        Empresa = 1
    }

    public enum MNPIncomingEffectStatus
    {
        Pending = 0,
        /// <summary>
        /// 正在处理
        /// </summary>
        Processing = 1,
        /// <summary>
        /// 处理成功
        /// </summary>
        Complete = 2,
        /// <summary>
        /// 处理失败
        /// </summary>
        Failed = 3

    }

    public enum MNPIncomingEffectType
    {
        Normal = 0,
        /// <summary>
        /// 移入时启用临时号码
        /// </summary>
        WithTempNumber = 1
    }

    public enum OperatorResult
    {
        Success = 0,
        Failed = 1,
    }

    //add by camel
    public enum VPNNumberCategory
    {
        Mobile = 0,
        FixedLine = 1
    }

    public enum VPNResourceType
    {
        CRMCustomerResource = 0,
        ExternalResource = 1
    }

    public enum VPNStatus
    {
        InActive = 0,
        Active = 1
    }
    #region added by Liny,2010-12-08,for Ben config data in table BEN_ID_MAPPINGS

    public enum BenConfigDataIDType
    {
        DealerID = 0,
        PackageID = 1,
        PromotionID = 2,
        CallCreditID = 3
    }

    #endregion

    #region added by James,2011-01-18,ResourceTCInfo中的StatusID

    public enum PRSConnectionStatus
    {
        Active = 0,
        Reserved = 1,
        Disconnected = 2
    }

    #endregion
    #region add by john 2011-01-26 (for service run information upload to monitor system)
    //add by john 2011-01-26 (for service run information upload to monitor system) start
    public enum MonitorSysAlertType
    {
        Normal = 0,
        Warning = 1,
        SeriousWarning = 2,
        Fault = 3
    }
    public enum MonitorSysServiceRuningStatus
    {
        Normal = 0,
        Fault = 1
    }
    //add by john 2011-01-26 (for service run information upload to monitor system) end
    #endregion
    #region add by john 2011-02-11 (for add Print Functions in Billing Module) start
    public enum ButtonControl : int
    {
        /// <summary>
        /// 开始状态
        /// </summary>
        INITIAL = 0,
        /// <summary>
        /// 正在打印状态
        /// </summary>
        PRINTING = 1,
        /// <summary>
        /// 出现异常状态
        /// </summary>
        EXCEPTION = 2,
        /// <summary>
        /// 打印完成状态
        /// </summary>
        COMPLETED = 3,
        /// <summary>
        /// 终止状态
        /// </summary>
        ABORT = 4
    }
    #endregion john 2011-02-11 (for add Print Functions in Billing Module) end
    public enum PackageLevel
    {
        NotChange = 0,
        DownGrade = 1,
        UpGrade = 2
    }
    public enum Switch_UnBilledBalance
    {
        Off = 0,
        On = 1
    }
    //== add by john 2011-03-10 start  CRM_MVNO_BRS_FINANCIALENTITY表中entitycode值
    //==modify by John 
    public enum MVNO_BRS_FINANCIALENTITY_Type
    {
        /// <summary>
        /// La Caixa
        /// </summary>
        ATM1 = 1,
        /// <summary>
        /// 4B
        /// </summary>
        ATM2 = 2,
        /// <summary>
        /// SERMEPA
        /// </summary>
        ATM3 = 3,
        /// <summary>
        /// CECA
        /// </summary>
        ATM4 = 4,
        //==add by John 2011-09-15 start
        /// <summary>
        /// CECA
        /// </summary>
        ATM5 = 5,
        //==add by John 2011-09-15 end
        /// <summary>
        /// La Caixa
        /// </summary>
        CCC1 = 1,
        /// <summary>
        /// 4B
        /// </summary>
        CCC2 = 2,
        /// <summary>
        /// SERMEPA
        /// </summary>
        CCC3 = 3,
        /// <summary>
        /// CECA
        /// </summary>
        CCC4 = 4,
        //==add by John 2011-09-15 start
        /// <summary>
        /// CECA
        /// </summary>
        CCC5 = 5,
        //==add by John 2011-09-15 end
    }
    //==modify by John 
    //== add by john 2011-03-10 end  CRM_MVNO_BRS_FINANCIALENTITY表中entitycode值
    public enum MNPMultiPortInStatus
    {
        Processing = 0,
        Fail = 1,
        Completed = 2
    }
    // Add by winson  for Alert setting 
    public enum AlertRuleType
    {
        VoucherIVRTimes = 0,
        VoucherIVRAmount = 1,
        VoucherVPOSTimes = 2,
        VoucherVPOSAmount = 3,
        VoucherCRMTimes = 4,
        VoucherCRMAmount = 5,
        VoucherTotalTimes = 6,
        VoucherTotalAmount = 7,
        AllTotalTimes = 8,
        AllTotalAmount = 9,
        PremiumcallAmount = 10,
        VoucherMaxSerialerro = 11,
        // add by winson 2011-6-17
        ATMTopupMaxTime = 12,
        ATMTopupMaxAmount = 13,
        ATMTopupMinAmount = 14,
        ATMTopupCancellationInternal = 15
        //end add
    }

    public enum AlertRulePeriodType
    {
        PreHour = 0,//2011-6-22 winson change the wrong word
        PreDay = 1//2011-6-22 winson change the wrong word
    }
    //2011-9-15 winson add for new requirement   
    public enum AlertRuleStatus
    {
        Off = 0,
        On = 1,
        ReportOnly = 2
    }
    //2011-9-15 end by  winson
    //add by devid 20110125 for bt new feature
    public enum CustomerAgentType
    {
        Reseller = 1,
        Soho = 2,
        Pyme = 3
    }
    //== add by John 2011-03-25 for SMS template config start
    #region SMS template config
    public enum SMSTemplateRequestType
    {
        Unknow = 0,
        RoamingWelcome = 1,
        PromotionLifeCycle = 2,
        CommercialSMS = 3,
        UssdText = 4,
        BalanceSMS = 5,
		 RoamingLimit = 6,
        NDayNHourNotifyBeforeExpired = 6,//added by Ego.20131217
        //added by damon 2013-01-15
        TransferIDVerification = 11,
        TransferRequestSuccessForA = 12,
        TransferRequestFailureForA = 13,
        TransferFailureForA = 14,
        TransferFailureForB = 15,
        TransferRequestTerminationForA = 16,
        TransferRequestTerminationForB = 17,
		TransferRequestSuccessForB = 18,
        ExpirationNotification = 18,
        FirstTopupPerWeekNotification = 19,
        //NotReachLimitNotification = 20, 用整数结尾的会在生成短信模板时，取id最大值和其他类型的冲突。例如：2 和 20， 3 和 30。
        LostCurrentPackageNotification = 21,
        NDayNHourAfterActivationNotification = 22,
        SuperTopupActivationSuccessNotification = 23,
        SuperTopupNextPeriodStartNotification = 24,
        SuperTopupPlanExiredNotification = 25,
        NotReachLimitNotification = 26,
        ReachMinimiseTopupAmount = 27,
        NotReachMinimiseTopupAmount = 28,
        //added by neil at 2014/4/8
        FirstTopUp = 29,
        ReachedMaxValue = 30,
        ChangePackage = 31,
        //added by Benny 2014-05-22
        LargeTopUp = 32,
        NotifyBalanceInsufficient = 33,//added by figo 2014-7-1
        PromotionGroupNDayNotifyBeforeExpired, //Added by Ignasi 2014/08/27
            //added by JAnton for Telkom Indonesia intl transfers via Telkom API
        TelkomTransferRequestSuccessForA = 19,
        InternationalTransferFailureForA = 25,//other error code 
        InternationalTransferFailureForA_100 = 26,//  error code 100(wrong number)
        InternationalTransferFailureForA_Timeout = 27,//  error code other

        //add by Ben 2013-10-23 for Axiom DocumentValidation
        ValidateWithInXDay = 21,
        ValidateWithInYDay = 22,
        ValidateWithInZDay = 23,
        ValidateWithInWDay = 24,
    }

    public enum SMSTemplatePromotionLifeCycleSubType
    {
        NoValue = 0,
        SetUp = 1,
        RenewSuccess = 2,
        RenewFailed = 3,
        xExhausted = 4,
        yExhausted = 5,
        AllExhausted = 6,
        AllPromotionExhausted = 7,
        //added by Ego.20131217
        NDayNHourBeforePromotionExhausted = 8,


        //added by Lincoln 2014-4-15 for promotion group begin
        PromotionGroupSignUp = 11,
        PromotionGroupRenewSuccess = 12,
        PromotionGroupRenewFailed = 13,
        PromotionGroupDisabled = 14,
        //added by Lincoln 2014-4-15 for promotion group end
        ExpiryDateToRemind = 8,
        USSDCommandRemind = 9,
        xExhaustedEmail = 104,
        AllExhaustedEmail = 106
    }

    public enum SMSTemplateResponseErrorType
    {
        Success = 0,
        ResourceNull = 1,
        TemplateIDNull = 2,
        PromotionIDNull = 3,
        UnkownSMSTemplateType = 4,
        GetResourceMBInfoNull = 5,
        GetSMSTemplateNull = 6,
        ResourceMBCustomerIDNull = 7,
        VLRGTTNull = 8,
        CatchException = 9,
        GetPromotionInformationNull = 10,
        ZoneIDNull = 11,
        RoamingWelcomeSubTypeNull = 12,
        MVNOIDNull = 13,
        LanguageIDNull = 14,
        BundleIDNull = 15,
        PaymentTypeNull = 16,
        NetTypeNull = 17,
        DataRoamingNotificationTypeNull = 18,
        DataRoamingThresholdLimitNull = 19
    }

    public enum SMSTemplateRoamingWelcomeSubType
    {
        RoamingWelcomeSMS = 1,
        RoamingRateplanSMS = 2,
        RoamingDataSMS = 3
    }

    public enum SMSTemplateRoamingDataNotificationType
    {
        None = 0,
        AppliedLimit = 1,
        ConsumptionThresholdX = 2,
        //ConsumptionThresholdY = 3,
        DailyAccumulatedConsumption = 3,
        DataRoamingContinue = 4,
        ConsumptionInterruption = 5,
        ThresholdControl = 6,
        ContinueInterruption = 7,
        ConsumptionThresholdY = 8,
        ContinueThresholdX = 9,
        ContinueThresholdY = 10,
        ContinueControl = 11
    }
    public enum SMSTemplateRoamingDataSMSSubType
    {
        None = 0,
        AppliedLimit = 1,
        ConsumptionThresholdX = 2,
        ConsumptionThresholdY = 3,
        DailyAccumulatedConsumption = 4,
        DataRoamingContinue = 5,
        ConsumptionInterruption = 6,
        ThresholdControl = 7,
        ContinueThresholdX = 8,
        ContinueThresholdY = 9,
        ContinueInterruption = 10,
        ContinueControl = 11
    }
    public enum SMSTemplatePaymentType
    {
        All = -1,
        PostPaid = 1,
        PrePaid = 2
    }

    public enum SMSTemplateNetType
    {
        All = -1,
        Camel = 1,
        NonCamel = 2
    }

    public enum SMSTemplateStatus
    {
        Unkown = -1,
        Active = 1,
        Inactive = 0
    }

    public enum SMSTemplateFormOwner
    {
        DealerManagement = 1,
        RoamingRatePlanConfig = 2,
        RoamingWelcomeConfig = 3,
        PromotionConfig = 4,
        frmSMSConfig = 5,
        PromotionPlanConfig = 6,
        RoamingDataConfig = 7
    }

    #endregion
    //== add by John 2011-03-25 for SMS template config end

    //== add by John 2011-05-17 for SMS template config start
    #region premium SMS
    public enum CallFrom
    {
        IN = 1,
        IPX = 2
    }
    public enum VASServiceType
    {
        PremiumSMS = 1001,
        PremiumMMS = 2001,
        LocationBasedService = 3001
    }

    public enum VASServiceStatus
    {
        Actived = 1,
        Terminated = 2,
        Deactived = 3
    }
    public enum VASBlackWhiteListType
    {
        None = 0,
        BlackList = 1,
        WhiteList = 2,
        All = 3
    }

    public enum VASStatusID
    {
        ACTIVE = 1,
        INACTIVE = 2,
        ANNULLED = 3,
    }
    public enum VASServicePropertyCode
    {
        MAXTARIFF = 1001,
        CURRENCY = 1002,
        MINAGE = 1003,
        MAXMTPERHOUR = 1004,
        MAXMTPERDAY = 1005,
        MAXMTPERWEEK = 1006,
        MAXMTPERMONTH = 1007,
        MAXMTPERYEAR = 1008,
        APPLYONBLACKWHITELIST = 1009
    }

    public enum VASBlackWhiteType
    {
        BLACKLIST = 1,
        WHITELIST = 2,
    }

    public enum VASPropertyType
    {
        VAS_SERVICE_PROPERTY_CODE
    }

    public enum VASDataType
    {
        S,
        N,
        D
    }
    #endregion
    //== add by John 2011-05-17 for SMS template config end

    //add by richard 20111021
    public enum AccountType
    {
        NormalAccount = 0,
        PromotedAccount = 1,
        None = 2
    }

    public enum NotificationFrom : byte
    {
        EngineScan = 0,
        DRE = 1
    }

    public enum TopupHistoryQueryType : int
    {
        AtosApi = 0,
        ClientAdvance = 1,
        ClientFuzzy = 2
    }
    //==add by John for Fixed IP 2011-11-11
    public enum IPStatus : int
    {
        Free = 0,
        Used = 1,
        Lock = 2
    }
    //add by damon 2012-07-31
    public enum PromotionDeactiveReason
    {
        ResetFailed = 1,
        Delete = 2
    }
    //add by damon 2012-10-16,Rain
    public enum CustomerSpendingStatus
    {
        Unnecessary = 0,
        Necessary = 1,
        Sent = 2
    }

    #region Added by Liny,2012-10-22,for RIAN project III

    public enum PCRFServiceType
    {
        AdultContentBarring = 0,
        FreeWebsite = 1
    }

    #endregion
    //added by damon 2012-09-11
    public enum ErrorStatus
    {
        Pending = 0,
        Passed = 1,
        WonntBePassed = 2,
        FixedByManully = 3,
        Canceled = 4
    }

    public enum Gender
    {
        Company = 0,
        Female = 1,
        Male = 2,
        Autres = 3
    }

    public enum Title
    {
        Mr = 1,
        Mrs = 2
    }
    //added by damon 2012-09-11

    #region Added by Liny,2013-01-17 , for SRS for Balance Transfer V0.7,merged from VF_NL_Saudi_dev branch,2013-01-24

    public enum BalanceTransferChannel
    {
        UNKNOWN = 0,
        USSD = 1,
        Selfcare = 2,
        SMS = 3,
        Client = 4,
        IVR = 5
    }

    public enum BalanceTransferRequestStatus
    {
        Pending = 0,
        IDPassed = 1,
        TransferSucceed = 2,
        Teminated = 3,
        TransferFailed = -1
    }

    public enum BalanceTransferType
    {
        Transfer_To = 0,
        Transfer_From = 1
    }

    #endregion


    //add by  richard 20130626
    public enum ScheDuleType
    {
        DailyStatusChange = 0,
        DailySMS = 1,
        DailyReport = 2
    }
    /// <summary>
    /// Use this enum to partial update service of subscriber, in HPS model.
    /// </summary>
    public enum SuplementaryServiceType
    {
        Callforward = 0,
        SDBBarring = 1,
        ODBBarring = 2,
        APNService = 3,
        CallPresentation = 4,
        CallWaitHolding = 5,
        CallTransfer = 6,
        AdviceOfCharge = 7,
        MultiParty = 8,
        BearerService = 9,
        TeleService = 10,
        NetworkAccessModel = 11,
        CamelRestrictedService = 12,
        FTNRule = 13,
        CamelService = 14,
        USSDTemplateService = 15,
        LTE4GService = 16,
    }
    /// <summary>
    /// The TK HLR CSI type.
    /// </summary>
    public enum Camel_CSI_Type_TK
    {
        O_CSI = 1,
        T_CSI = 2,
        VT_CSI = 3,
        GPRS_CSI = 4,
        OSMS_CSI = 5,
        D_CSI = 6,
        M_CSI = 7,
        U_CSI = 8,
        TIF_CSI = 9,
        SS_CSI = 10,
        N_CSI = 11,
    }

    public enum CRM_CallForwardType
    {
        CFU = 33,
        CFB = 41,
        CFNRy = 42,
        CFNRc = 43,
        Unknown = -1,
    }
    public enum BSGID
    {
        TS11_TS12 = 1,
        TS21_TS22_TS23 = 2,
        TS61_TS62 = 6,
        //BS10=7,
        //BS18=8,
        TS91_TS92 = 12
    }

    public enum IPVersionType
    {
        IPV4 = 0,
        IPV6 = 1,
        X25 = 2,
        PPP = 3,
        OSP_IHOST = 4,
    }

    public enum AddFriendAndFamilyCreditResult
    {
        Ok = 0,
        No_FF_Plan = 1,
        Plan_Expired = 2,
        Customer_Status_Invalid = 3,
        Insufficient_Balance = 4,
        TopupPlanNotFound = 5,
        Internal_Error = 6,
        Exceed_Maximum_Allow_Limit = 7,

        Invalid_User = 1001,
        Invalid_Vmo = 1002,
        Password_Incorrect = 1003,
        User_has_No_Permission = 1004,
        No_Permission_Acces_Msisdn = 1005,
        No_Info_For_Number = 1006,
        Error_Checking_Permission = 1007,
        MVNO_Has_No_Permission_On_The_Customer = 1008,
    }

    public enum CommonErrorResult
    {
        Renew_Successful = 0,
        Other_Error = 1,
        Dealer_Autorization_Error = 2,
        Customer_Info_Not_Found = 3,
        Invalid_User = 4,
        Invalid_Vmo = 5,
        Password_Incorrect = 6,
        No_Info_For_Number = 7,
        User_has_No_Permission = 8,
        No_Permission_Acces_Msisdn = 9,
        Error_Checking_Permission = 10,
        MVNO_Has_No_Permission_On_The_Customer = 11,

    }

    public enum RenewCustomerFriendAndFamilyResult
    {
        /// <summary>
        /// Renew ok.
        /// </summary>
        Renew_Successful = 0,
        /// <summary>
        /// The customer have no FF promotion.
        /// </summary>
        CustomerHaveNoFFPromtoionForRenew = 1,
        /// <summary>
        /// The period is not valid.
        /// </summary>
        Invalid_Period = 2,
        /// <summary>
        /// Customer's remain balance not enough.
        /// </summary>
        Insufficient_Balance = 3,

        Internal_Error = 4,

        PromotionGroup_NotExist = 5,

        PromotionGroupMember_NotExist = 6,

        Dealer_Autorization_Error = 7,
        Customer_Info_Not_Found = 8,

        Invalid_User = 1001,
        Invalid_Vmo = 1002,
        Password_Incorrect = 1003,
        User_has_No_Permission = 1004,
        No_Permission_Acces_Msisdn = 1005,
        No_Info_For_Number = 1006,
        Error_Checking_Permission = 1007,
        MVNO_Has_No_Permission_On_The_Customer = 1008,
    }

    public enum ApplyFriendAndFamilyResult
    {
        Ok = 0,
        /// <summary>
        /// The FF plan has been applied to the customer already.
        /// </summary>
        AppliedAlready = 1,
        /// <summary>
        /// A higher priority FF plan has been applied already.
        /// </summary>
        BetterPlanExists = 2,
        /// <summary>
        /// There is no enough balance for the customer to apply this plan.
        /// </summary>
        Insufficient_balance = 3,
        /// <summary>
        /// No shit.
        /// </summary>
        Plan_Not_Exists = 4,
        /// <summary>
        /// plan end date > now.
        /// </summary>
        Plan_Expired = 5,
        /// <summary>
        /// who the fuck know what error it is.
        /// </summary>
        Internal_Error = 6,
        /// <summary>
        /// There´s no subscription asociated to the requested MSISDN
        /// </summary>
        UnknownMSISDN = 7,

        Invalid_User = 1001,
        Invalid_Vmo = 1002,
        Password_Incorrect = 1003,
        User_has_No_Permission = 1004,
        No_Permission_Acces_Msisdn = 1005,
        No_Info_For_Number = 1006,
        Error_Checking_Permission = 1007,
        MVNO_Has_No_Permission_On_The_Customer = 1008,
    }

    public enum OperationFFNumberResult
    {
        Ok = 0,
        /// <summary>
        /// The new FF item exceed RmPromotionPlan.MAX_WHITELIST_NUMBERS
        /// </summary>
        Number_Exceed_Maxmium_Allowed = 1,
        /// <summary>
        /// The deleted FF item charge the customer $20 per item(fuck, so expensive).
        /// If customer's current limit lesser then the fee, get this error.
        /// </summary>
        Insufficient_Balance = 2,

        Internal_Error = 3,
        /// <summary>
        /// Data validation error - the input data has logical failure.
        /// </summary>
        Data_Validation_Error = 4,
        /// <summary>
        /// Data validation error - the input data has logical failure.
        /// </summary>
        AddedNumber_Not_Equal_DroppedNumber = 5,

        ///
        Add_MSISDN_Not_Belong_To_Allowed_MVNO = 6,
        Add_List_And_Dropped_List_Have_Same_Number = 7,
        Added_MSISDN_Has_Existed_For_Promotion = 8,
        Dropped_MSISDN_Not_Exist_For_Promotion = 9,

        Dealer_Not_Found = 10,
        Fiscal_Unit_Not_Found = 11,
        Credit_Limit_Not_Found = 12,
        Dealer_Autorization_Error = 13,
        Customer_Info_Not_Found = 14,
        Own_MSISDN_Unable_To_Be_Added_And_Deleted = 15,

        Cannot_Add_Yourself_to_promotion = 16,
        Cannot_Delete_Yourself_to_promotion = 17,
        Cannot_Access_To_MSISDN_Data = 18,
        Customer_Has_No_FF_Promotion = 19,
        Customer_Has_No_FN_Promotion = 20,

        Invalid_User = 1001,
        Invalid_Vmo = 1002,
        Password_Incorrect = 1003,
        User_has_No_Permission = 1004,
        No_Permission_Acces_Msisdn = 1005,
        No_Info_For_Number = 1006,
        Error_Checking_Permission = 1007,
        MVNO_Has_No_Permission_On_The_Customer = 1008,
    }
    //Added by Ego.20131219
    /// <summary>
    /// Notification Type Enum of NotificationType of MVNONotificationSettingInfo
    /// </summary>
    public enum OtherTypesOfNotificationTypes
    {
        /// <summary>
        /// N days/ N Hours expiration notification for Lifecycle 
        /// </summary>
        NDayNHourBeforeExpirationNotif = 1,
        /// <summary>
        /// Once per week notification until first top-up
        /// </summary>
        OncePerWeekUntilFirstTopupNotif = 2,
        /// <summary>
        /// notify when not reach limit in specified sum days after activation
        /// </summary>
        NotReachLimitNotif = 3,
        /// <summary>
        /// notify lost current package when not reach 150 I n specified sum days.
        /// </summary>
        LostCurrentPackageNotif = 4,
        /// <summary>
        /// Notify on N days/N hours after activation whenever the customer has not reached specified limit
        /// </summary>
        NDayNHourAfterActivationNotif = 5,

        SuperTopupActivationSuccessNotif = 6,
        SuperTopupNextPeriodStartNotif = 7,
        SuperTopupPlanExpiredNotif = 8,

        MinimizeTopupEnough = 9,
        MinimizeTopupNotEnough = 10,

        FirstTopup = 11,
        ReachedMaxValue = 12,
        WelcomeNotif = 13,
        /// <summary>
        /// value1 = PromotionGROUPID
        /// </summary>
        GroupRenewFailed = 15,
        GroupRenewSuccess = 14,
        GroupRenovation = 16,
        //Added by Benny 2014-05-23
        LargeTopUp = 17,
        NotifyBalanceInsufficient = 18,//add by figo 2014-7-1
        //START Added by JavierA on 2014-07-07
        /// <summary>
        /// Notify requested new package (changePlanPrice) is same as currently used
        /// </summary>
        PackageAlreadyInUse = 19,
        /// <summary>
        /// Notify (changePlanPrice) current package is still active and cannot be changed
        /// </summary>
        DoublePackageActivation = 20,
        //END Added by JavierA on 2014-07-07
        GroupGracePeriodRenewFailed = 23,
        GroupGracePeriodRenewSuccess = 22,
        GroupDisabled = 21,

        //added by Oliver on 2014-08-11
        GroupUpgrade = 24,
        GroupRenewSuccessSecondNotif = 25,
        /// <summary>
        /// for low balance notification
        /// </summary>
        LowBalance = 26,
        /// <summary>
        /// Promotion renew successfully
        /// </summary>
        PromotionRenewSuccessfully = 27,
        /// <summary>
        /// Promotion Renew failed
        /// </summary>
        PromotionRenewFailed = 28
    }
    public enum NotificationTimeUnits
    {
        Hour = 1,
        Day = 2

    }

    public enum DATATYPEID
    {
        LAST_TOPUP = 10001,
        LAST_CHANGE_PACKAGE_TOPUP = 10002
    }

    public enum PackageChangeType
    {
        NON_TO_NON = 0,
        NON_TO_PKTLLAMEN = 1,
        PKTLLAMEN_TO_PKTLLAMEN = 2,
        PKTLLAMEN_TO_NON = 3
    }

    public enum ConfigActionCategory
    {
        PKTLLAMEN_PACKAGE = 10010,
        PKTLLAMEN_PROMOTION = 10020,
        TOPUP_MINIMIZE = 10030,
        FF_FEE = 10040,
        GROUP_MSISDN_ALLOW = 10050,
        AFFECTED_PROMOTIONID_LIST = 10060,
        PCRF_EMAIL_RECIEVER=10070,
        PCRF_ENGINE_RUNTIME=10071,
    }

    public enum BonusRelationshipTypes
    {
        PackageToPromoiom = 0,
        PromotionToPromotion = 1,
        PackageToPromotionGroup = 2,
        MinimizeTopupActivation,
    }

    //added by neil at 2014/3/17 for Elektra resource status truansfer
    public enum TransferStatusTypes : int
    {
        InitialToActive = 0,
        ActiveToS1 = 1,
        S1ToS2 = 2,
        S2ToS3 = 3,
        S3ToDeleted = 4,
        S1ToActive = 5,
        S2ToActive = 6,
        S3ToActive = 7,
        S2ToS1 = 8,
        S3ToS2 = 9
    }
    /// <summary>
    /// Lifecycle setting event
    /// </summary>
    public enum LCSettingEvent
    {
        VoucherCreatedOvertime = 2001,
        VoucherCreatedAheadOvertimeReport = 2002,
        VoucherActiveOvertime = 2003,
        VoucherActiveAheadOvertimeReport = 2004,
        VoucherUsedOvertime = 2005,
        VoucherExpiredOvertime = 2006,
        VoucherRechargingOvertime = 2007,
        VoucherAnnulledOvertime = 2008,
        VoucherFraudulentOvertime = 2009,

        InstalledOvertime = 3004,
        InstalledAheadOvertimeReport = 3005,
        ActiveOvertime = 3009,
        ActiveBalanceExhaustion = 3010,
        ActiveLowerBalance = 3011,
        ActiveAheadOvertimeFirstSms = 3012,
        ActiveAheadOvertimeSecondSms = 3057,
        ActiveAheadOvertimeReport = 3013,
        ActivateUserSms = 3015,
        InactiveOvertime = 3017,
        InactiveAheadOvertimeReport = 3018,
        InactiveAheadOvertimeFirstSms = 3019,
        InactiveAheadOvertimeSecondSms = 3058,
        DeactivatedOvertime = 3020,
        DeactivatedAheadOvertimeReport = 3021,
        DeactivatedAheadOvertimeFirstSms = 3050,
        DeactivatedAheadOvertimeSecondSms = 3059,
        ExpiredOvertime = 3033,
        ExpiredAheadOvertimeReport = 3034,

        BalanceAdjustment = 3036,
        VoucherRecharge = 3039,
        PaymentCardRecharge = 3060,
        CancelPaymentCardRecharge = 3061,


        InstalledPostOvertime = 5004,
        InstalledPostAheadOvertimeReport = 5005,
        ActivePostCreditExhaustion = 5010,
        ActivePostLowerCredit = 5011,
        ActivateUserSmsPost = 5015,
        ExpiredPostOvertime = 5033,
        ExpiredPostAheadOvertimeReport = 5034,

        ToStatus1 = 8001,
        ToStatus2 = 8002,
        ToStatus3 = 8003,
        ToStatus5 = 8005,
        ToStatus8 = 8008,
        ToStatus9 = 8009,
        ToStatus10 = 8010,
        ToStatus11 = 8011,
        ToStatus12 = 8012,
        ToStatus13 = 8013,
        ToStatus14 = 8014,
        ToStatus20 = 8020,






    }

    /// <summary>
    /// Promotion Group Category
    /// </summary>
    public enum PromotionGroupCategory
    {
        _3FF = 1,
        _5FF = 2,
        _10FF = 3,
        _15FF = 4,
    }

    public enum InvocationRetryCategories
    {
        IusacellPCRF = 1001,
    }

    public enum EligibleResult : byte
    {
        /// <summary>
        /// The rule has been able to determine if the customer was elegible for the promotion
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// The customer was elegible for the promotion
        /// </summary>
        Yes = 1,
        /// <summary>
        /// The customer was not elegible for the promotion
        /// </summary>
        No = 2,
        /// <summary>
        /// Promotion is not valid for package
        /// </summary>
        PromoGroupNotAllowedForPackage = 3,
        /// <summary>
        /// Package has been prepared to be upgraded at expiry time
        /// </summary>
        PackagePreparedForChange = 4,
        /// <summary>
        /// Not enough balance for operation
        /// </summary>
        NotEnoughBalance = 5
    }

    public enum APIVisible
    {
        /// <summary>
        /// Not show entity at API response
        /// </summary>
        Invisible = 0,
        VisibleOnAdd = 1,
        VisibleOnQuery = 2,
        /// <summary>
        /// Show entity at API response
        /// </summary>
        Visible = 3
    }
  
}
