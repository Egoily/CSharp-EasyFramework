using System;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.provisioning;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using com.etak.core.repository.crm.configuration;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.drl;
using com.etak.core.repository.crm.network;
using com.etak.core.repository.crm.Nhibernate.drl;
using com.etak.core.repository.crm.Nhibernate.network;
using com.etak.core.repository.crm.Nhibernate.operation;
using com.etak.core.repository.crm.Nhibernate.operation.mapping;
using com.etak.core.repository.crm.Nhibernate.portability;
using com.etak.core.repository.crm.Nhibernate.provisioning;
using com.etak.core.repository.crm.Nhibernate.resource;
using com.etak.core.repository.crm.Nhibernate.revenueManagement;
using com.etak.core.repository.crm.Nhibernate.services;
using com.etak.core.repository.crm.Nhibernate.subscription.catalog;
using com.etak.core.repository.crm.Nhibernate.usage;
using com.etak.core.repository.crm.operation;
using com.etak.core.repository.crm.portability;
using com.etak.core.repository.crm.promotion;
using com.etak.core.repository.crm.provisioning;
using com.etak.core.repository.crm.resource;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.crm.rules;
using com.etak.core.repository.crm.services;
using com.etak.core.repository.crm.subscription;
using com.etak.core.repository.crm.subscription.catalog;
using com.etak.core.repository.crm.topup;
using com.etak.core.repository.crm.user;
using com.etak.core.repository.NHibernate;
using NHibernate;
using NHibernate.Dialect;
using Ninject.Modules;
using NHinnerConfig = global::NHibernate.Cfg.ConfigurationSchema;
using com.etak.core.repository.crm.Nhibernate.inventory;
using com.etak.core.model.inventory;
using com.etak.core.model.order;
using com.etak.core.repository.crm.dealer;
using com.etak.core.repository.crm.Nhibernate.dealer;


namespace com.etak.core.repository.crm.Nhibernate.Factory
{
    /// <summary>
    /// Helper class to force loading the ninject module defined in RealHelper
    /// Initializes Nhibernate Repository and session factories.
    /// </summary>
    public class SessionFactoryHelper 
    {
        
    }

    /// <summary>
    /// Ninject module that initializes the Nhibernate Repository and session factories.
    /// Binds Nhibernate repositories to Repository interfaces. 
    /// </summary>
    public class RealHelper : NinjectModule
    {
        /// <summary>
        /// Builds the session factory and binds the IRepositories to each 
        /// RepositoryNH
        /// </summary>
        public override void Load()
        {
            ISessionFactory sesFactory = SessionManagement.GetInstance().GetSessionFactory("CRM");
            NHibernateConnectionProvider aa = new NHibernateConnectionProvider(sesFactory);

            //Set the connection provider to the session factory existing in CRM
            Bind<IConnectionProvider>().ToConstant(aa);

            ISession sesion = sesFactory.OpenSession();
            Dialect dialect = sesion.GetSessionImplementation().Factory.Dialect;

            if (dialect is MsSql2012Dialect)
            {
                //Sequence generator to SQL Server 2012+ native sequences. => this is only valid for 2012+
                Bind<ISequenceProvider>().To<SqlServerNativeSequenceProvider>();
            }
            else
            {
                //Sequence generator for engines that does not support native sequences => Pre 2012versions.
                Bind<ISequenceProvider>().To<TableBasedSequenceProvider>();
            }

            //Map/Bind the interfaces to NH implementations
            Bind<IBRSTaxCodePostCodeRepository<BRSTaxCodePostCode>>().To<BRSTaxCodePostCodeRepositoryNH<BRSTaxCodePostCode>>();
            Bind<IBRSTaxCodeRepository<BRSTaxCode>>().To<BRSTaxCodeRepositoryNH<BRSTaxCode>>();
            Bind<IBundleInfoRepository<BundleInfo>>().To<BundleInfoRepositoryNH<BundleInfo>>();
            Bind<ICrmCustomerPromotionPropertyInfoRepository<CrmCustomerPromotionPropertyInfo>>().To<CrmCustomerPromotionPropertyInfoRepositoryNH<CrmCustomerPromotionPropertyInfo>>();
            Bind<ICrmCustomersBonusRelationShipInfoRepository<CrmCustomersBonusRelationShipInfo>>().To<CrmCustomersBonusRelationShipInfoRepositoryNH<CrmCustomersBonusRelationShipInfo>>();
            Bind<ICrmCustomersBundleAssignmentHistoryInfoRepository<CrmCustomersBundleAssignmentHistoryInfo>>().To<CrmCustomersBundleAssignmentHistoryInfoRepositoryNH<CrmCustomersBundleAssignmentHistoryInfo>>();
            Bind<ICrmCustomersExtraUsageInfoRepository<CrmCustomersExtraUsageInfo>>().To<CrmCustomersExtraUsageInfoRepositoryNH<CrmCustomersExtraUsageInfo>>();
            Bind<ICrmCustomersPromotionGroupRepository<CrmCustomersPromotionGroup>>().To<CrmCustomersPromotionGroupRepositoryNH<CrmCustomersPromotionGroup>>();
            Bind<ICrmCustomersPromotionInfoRepository<CrmCustomersPromotionInfo>>().To<CrmCustomersPromotionInfoRepositoryNH<CrmCustomersPromotionInfo>>();
            Bind<ICrmCustomersPromotionOperationLogInfoRepository<CrmCustomersPromotionOperationLogInfo>>().To<CrmCustomersPromotionOperationLogInfoRepositoryNH<CrmCustomersPromotionOperationLogInfo>>();
            Bind<ICrmCustomersResourceMbInfoRepository<CrmCustomersResourceMbInfo>>().To<CrmCustomersResourceMbInfoRepositoryNH<CrmCustomersResourceMbInfo>>();
            Bind<ICrmCustomersTopupBonusApplyCountInfoRepository<CrmCustomersTopupBonusApplyCountInfo>>().To<CrmCustomersTopupBonusApplyCountInfoRepositoryNH<CrmCustomersTopupBonusApplyCountInfo>>();
            Bind<ICrmCustomersTopupPromotionApplyCountInfoRepository<CrmCustomersTopupPromotionApplyCountInfo>>().To<CrmCustomersTopupPromotionApplyCountInfoRepositoryNH<CrmCustomersTopupPromotionApplyCountInfo>>();
            Bind<ICrmCustomersUnpaidFeeInfoRepository<CrmCustomersUnpaidFeeInfo>>().To<CrmCustomersUnpaidFeeInfoRepositoryNH<CrmCustomersUnpaidFeeInfo>>();

            Bind<ICRMMessageInfoRepository<CRMMessageInfo>>().To<CRMMessageInfoRepositoryNH<CRMMessageInfo>>();
            Bind<ICrmMVNOPromotedTopupHistoryInfoRepository<CrmMVNOPromotedTopupHistoryInfo>>().To<CrmMVNOPromotedTopupHistoryInfoRepositoryNH<CrmMVNOPromotedTopupHistoryInfo>>();
            Bind<ICrmMvnoTopupBonusDetailInfoRepository<CrmMvnoTopupBonusDetailInfo>>().To<CrmMvnoTopupBonusDetailInfoRepositoryNH<CrmMvnoTopupBonusDetailInfo>>();
            Bind<ICrmMvnoTopupBonusPromotionInfoRepository<CrmMvnoTopupBonusPromotionInfo>>().To<CrmMvnoTopupBonusPromotionInfoRepositoryNH<CrmMvnoTopupBonusPromotionInfo>>();
            Bind<ICustomerInfoRepository<CustomerInfo>>().To<CustomerInfoRepositoryNH<CustomerInfo>>();
            Bind<IDealerInfoRepository<DealerInfo>>().To<DealerInfoRepositoryNH<DealerInfo>>();
            Bind<IDictionaryInfoRepository<DictionaryInfo>>().To<DictionaryInfoRepositoryNH<DictionaryInfo>>();
            Bind<IEventInfoRepository<EventInfo>>().To<EventInfoRepositoryNH<EventInfo>>();
            Bind<IHistoryInfoRepository<HistoryInfo>>().To<HistoryInfoRepositoryNH<HistoryInfo>>();
            Bind<IIDMappingInfoRepository<IDMappingInfo>>().To<IDMappingInfoRepositoryNH<IDMappingInfo>>();
            Bind<ILanguageTypeInfoRepository<LanguageTypeInfo>>().To<LanguageTypeInfoRepositoryNH<LanguageTypeInfo>>();
            Bind<ILifecycleLogInfoExtendedRepository<LifecycleLogInfoExtended>>().To<LifecycleLogInfoExtendedRepositoryNH<LifecycleLogInfoExtended>>();
            Bind<ILifecycleLogInfoRepository<LifecycleLogInfo>>().To<LifecycleLogInfoRepositoryNH<LifecycleLogInfo>>();
            Bind<ILoginInfoRepository<LoginInfo>>().To<LoginInfoRepositoryNH<LoginInfo>>();
            Bind<IMVNOConfigActionInfoRepository<MVNOConfigActionInfo>>().To<MVNOConfigActionInfoRepositoryNH<MVNOConfigActionInfo>>();
            Bind<IMVNONotificationSettingInfoRepository<MVNONotificationSettingInfo>>().To<MVNONotificationSettingInfoRepositoryNH<MVNONotificationSettingInfo>>();
            Bind<IMVNOPromotionSMSConfigRepository<MVNOPromotionSMSConfig>>().To<MVNOPromotionSMSConfigRepositoryNH<MVNOPromotionSMSConfig>>();
            Bind<IMVNOPropertiesRepository<MVNOPropertiesInfo>>().To<MVNOPropertiesRepositoryNH<MVNOPropertiesInfo>>();
            Bind<IMvnoTopupBonusInfoRepository<CrmMvnoTopupBonusInfo>>().To<MvnoTopupBonusInfoRepositoryNH<CrmMvnoTopupBonusInfo>>();
            Bind<IMVNOTopupBundleBasedInfoRepository<MVNOTopupBundleBasedInfo>>().To<MVNOTopupBundleBasedInfoRepositoryNH<MVNOTopupBundleBasedInfo>>();
            Bind<INumberPropertyInfoRepository<NumberPropertyInfo>>().To<NumberPropertyInfoRepositoryNH<NumberPropertyInfo>>();
            Bind<IOperationInfoRepository<OperationInfo>>().To<OperationInfoRepositoryNH<OperationInfo>>();
            Bind<IOperationLogRepository<OperationLog>>().To<OperationLogRepositoryNH<OperationLog>>();
            Bind<IPackageDealerMappingInfoRepository<PackageDealerMappingInfo>>().To<PackageDealerMappingInfoRepositoryNH<PackageDealerMappingInfo>>();
            Bind<IPackageInfoRepository<PackageInfo>>().To<PackageInfoRepositoryNH<PackageInfo>>();
            Bind<IPricePlanPackageVodafoneFileMapRepository<PricePlanPackageVodafoneFileMap>>().To<PricePlanPackageVodafoneFileMapRepositoryNH<PricePlanPackageVodafoneFileMap>>();
            Bind<IResourceMBRepository<ResourceMBInfo>>().To<ResourceMBRepositoryNH<ResourceMBInfo>>();
            Bind<IRmPromotionGroupInfoRepository<RmPromotionGroupInfo>>().To<RmPromotionGroupInfoRepositoryNH<RmPromotionGroupInfo>>();
            Bind<IRmPromotionGroupMemberRepository<RmPromotionGroupMember>>().To<RmPromotionGroupMemberRepositoryNH<RmPromotionGroupMember>>();
            Bind<IRmPromotionPlanDetailInfoRepository<RmPromotionPlanDetailInfo>>().To<RmPromotionPlanDetailInfoRepositoryNH<RmPromotionPlanDetailInfo>>();
            Bind<IRmPromotionPlanInfoRepository<RmPromotionPlanInfo>>().To<RmPromotionPlanInfoRepositoryNH<RmPromotionPlanInfo>>();
            Bind<IRmPromotionPlanTopupBasedParametersRepository<RmPromotionPlanTopupBasedParameters>>().To<RmPromotionPlanTopupBasedParametersRepositoryNH<RmPromotionPlanTopupBasedParameters>>();
            Bind<IServicesInfoRepository<ServicesInfo>>().To<ServicesInfoRepositoryNH<ServicesInfo>>();
            Bind<ISettingExtendDetailInfoRepository<SettingExtendDetailInfo>>().To<SettingExtendDetailInfoRepositoryNH<SettingExtendDetailInfo>>();
            Bind<ISettingInfoRepository<SettingInfo>>().To<SettingInfoRepositoryNH<SettingInfo>>();
            Bind<ISIMCardInfoRepository<SIMCardInfo>>().To<SIMCardInfoRepositoryNH<SIMCardInfo>>();
            Bind<ISmsLogInfoRepository<SmsLogInfo>>().To<SmsLogInfoRepositoryNH<SmsLogInfo>>();
            Bind<ISmsLogQueueRepository<SmsLogQueue>>().To<SmsLogQueueRepositoryNH<SmsLogQueue>>();
            Bind<ISmsTemplateInfoRepository<SmsTemplateInfo>>().To<SmsTemplateInfoRepositoryNH<SmsTemplateInfo>>();
            Bind<ISystemConfigDataInfoRepository<SystemConfigDataInfo>>().To<SystemConfigDataInfoRepositoryNH<SystemConfigDataInfo>>();
            Bind<ITransitionInfoRepository<TransitionInfo>>().To<TransitionInfoRepositoryNH<TransitionInfo>>();
            Bind<IUserDealerInfoRepository<UserDealerInfo>>().To<UserDealerInfoRepositoryNH<UserDealerInfo>>();
            Bind<ICRMCUSTOMERSDATESINFORepository<CRMCUSTOMERSDATESINFO>>().To<CRMCUSTOMERSDATESINFORepositoryNH<CRMCUSTOMERSDATESINFO>>();
            Bind<IPackageRelationShipsRepository<PackageRelationShips>>().To<PackageRelationShipsRepositoryNH<PackageRelationShips>>();
            Bind<IStatusChangedLogInfoRepository<StatusChangedLogInfo>>().To<StatusChangedLogInfoRepositoryNH<StatusChangedLogInfo>>();
            Bind<ICrmCustomersBalanceTransationHistoryRepository<CrmCustomersBalanceTransationHistory>>().To<CrmCustomersBalanceTransationHistoryRepositoryNH<CrmCustomersBalanceTransationHistory>>();

            //Added by Ignasi 06/04/2014
            Bind<ICrmCustomersMSISDNGroupMembersRepository<CrmCustomersMSISDNGroupMembers>>().To<CrmCustomersMSISDNGroupMembersRepositoryNH<CrmCustomersMSISDNGroupMembers>>();



            //Added by Benny 2014-06-05
            Bind<IMultiLingualInfoRepository<MultiLingualInfo>>().To<MultiLingualInfoRepositoryNH<MultiLingualInfo>>();

            //Bind<ICrmMvnoInvocationRetryInfoRepository<CrmMvnoInvocationRetryInfo>>().To<CrmMovoInvocationRetryInfoRepositoryNH<CrmMvnoInvocationRetryInfo>>();

            Bind<IProductInfoRepository<ProductInfo>>().To<ProductInfoRepositoryNH<ProductInfo>>();
            Bind<IDealerNumberInfoRepository<DealerNumberInfo>>().To<DealerNumberInfoRepository<DealerNumberInfo>>();


            //Bind<ICrmPromotionsPriorityExceptionsInfoRepository<CrmPromotionsPriorityExceptionsInfo>>().To<CrmPromotionsPriorityExceptionsInfoRepositoryNH<CrmPromotionsPriorityExceptionsInfo>>();

            //Added by JavierA on 07/08/2014
            Bind<ICrmBussinessRuleInfoRepository<BussinessRule>>().To<CrmBusinessRuleInfoRepositoryNH<BussinessRule>>();
            Bind<IPropertyInfoRepository<PropertyInfo>>().To<PropertyInfoRepositoryNH<PropertyInfo>>();

            Bind<INumberInfoRepository<NumberInfo>>().To<NumberInfoRepositoryNH<NumberInfo>>();

            //added by Oliver 02/10/2014 for Postpaid
            Bind<IAccountRepository<Account>>().To<AccountRepositoryNH<Account>>();
            Bind<ICustomerChargeScheduleRepository<CustomerChargeSchedule>>().To<CustomerChargeScheduleRepositoryNH<CustomerChargeSchedule>>();
            Bind<IBillRunRepository<BillRun>>().To<BillRunRepositoryNH<BillRun>>();
            Bind<IAccountDataRepository<AccountData>>().To<AccountDataRepositoryNH<AccountData>>();
            Bind<IAccountCurrencyRepository<AccountCurrency>>().To<AccountCurrencyRepositoryNH<AccountCurrency>>();
            Bind<IBalanceForAccountRepository<BalanceForAccount>>().To<BalanceForAccountRepositoryNH<BalanceForAccount>>();
            Bind<ICustomerChargeRepository<CustomerCharge>>().To<CustomerChargeRepositoryNH<CustomerCharge>>();
            Bind<IChargePriceRepository<ChargePrice>>().To<ChargePriceRepositoryNH<ChargePrice>>();
            Bind<IProductRepository<Product>>().To<ProductRepositoryNH<Product>>();
            Bind<IProductChargeOptionRepository<ProductChargeOption>>().To<ProductChargeOptionRepositoryNH<ProductChargeOption>>();
            Bind<IAccountTimeRepository<AccountTime>>().To<AccountTimeRepositoryNH<AccountTime>>();
            Bind<IBillCycleRepository<BillCycle>>().To<BillCycleRepositoryNH<BillCycle>>();
            Bind<ICustomerProductAssignmentRepository<CustomerProductAssignment>>().To<CustomerProductAssignmentRepositoryNH<CustomerProductAssignment>>();
            Bind<IChargeRepository<Charge>>().To<ChargeRepositoryNH<Charge>>();
            Bind<IChargeRepository<ChargeNonRecurring>>().To<ChargeRepositoryNH<ChargeNonRecurring>>();
            Bind<IChargeRepository<ChargeRecurring>>().To<ChargeRepositoryNH<ChargeRecurring>>();
            Bind<IChargeRepository<ChargeDiscount>>().To<ChargeRepositoryNH<ChargeDiscount>>();
            Bind<IInvoiceRepository<Invoice>>().To<InvoiceRepositoryNH<Invoice>>();
            Bind<ICustomerAccountAssociationRepository<CustomerAccountAssociation>>().To<CustomerAccountAssociationRepositoryNH<CustomerAccountAssociation>>();
            Bind<IAddressInfoRepository<AddressInfo>>().To<AddressInfoRepositoryNH<AddressInfo>>();
            Bind<IRepository<MultiLingualDescription, Int32>>().To<NHibernateRepository<MultiLingualDescription, Int32>>();
            Bind<ITaxDefinitionRepository<TaxDefinition>>().To<TaxDefinitionRepositoryNH<TaxDefinition>>();
            Bind<IHLRRequestErrorsRepository<HLRRequestErrors>>().To<HLRRequestErrorsRepositoryNH<HLRRequestErrors>>();
            Bind<IReportResourceInfoRepository<ReportResourceInfo>>().To<ReportResourceInfoRepositoryNH<ReportResourceInfo>>();

            Bind<IMNPPortabilityInfoRepository<MNPPortabilityInfo>>().To<MNPPortabilityInfoRepositoryNH<MNPPortabilityInfo>>();
            Bind<IMNPIncomingEffectInfoRepository<MNPIncomingEffectInfo>>().To<MNPIncomingEffectInfoNH<MNPIncomingEffectInfo>>();
            Bind<IRoamingBlackListInfoRepository<RoamingBlackListInfo>>().To<RoamingBlackListInfoRepositoryNH<RoamingBlackListInfo>>();
            Bind<IImeiAssnRepository<ImeiAssn>>().To<ImeiAssnRepositoryNH<ImeiAssn>>();
            Bind<IImeiAssnHistRepository<ImeiAssnHist>>().To<ImeiAssnHistRepositoryNH<ImeiAssnHist>>();
            Bind<IUsageDetailRecordRepository<UsageDetailRecord>>().To<UsageDetailRecordRepositoryNH<UsageDetailRecord>>();
            Bind<IMVNOAPNIPPoolInfoRepository<MVNOAPNIPPoolInfo>>().To<MVNOAPNIPPoolInfoRepositoryNH<MVNOAPNIPPoolInfo>>();

            Bind<ICustomerDataRoamingLimitRepository<CustomerDataRoamingLimit>>().To<CustomerDataRoamingLimitRepositoryNH<CustomerDataRoamingLimit>>();
            Bind<ICustomerDataRoamingLimitNotificationRepository<CustomerDataRoamingLimitNotification>>().To<CustomerDataRoamingLimitNotificationRepositoryNH<CustomerDataRoamingLimitNotification>>();

            Bind<IMNPPortabilityMultiPortInInfoRepository<MNPPortabilityMultiPortInInfo>>().To<MNPPortabilityMultiPortInInfoRepositoryNH<MNPPortabilityMultiPortInInfo>>();
            Bind<IMNPPortabilityCustomerInfoRepository<MNPPortabilityCustomerInfo>>().To<MNPPortabilityCustomerInfoRepositoryNH<MNPPortabilityCustomerInfo>>();
            Bind<ICrmDefaultProvisionInfoRepository<CrmDefaultProvisionInfo>>().To<CrmDefaultProvisionInfoRepositoryNH<CrmDefaultProvisionInfo>>();
            Bind<IRMOperatorsInfoRepository<RMOperatorsInfo>>().To<RMOperatorsInfoRepositoryNH<RMOperatorsInfo>>();
            Bind<IHolidayInfoRepository<HolidayInfo>>().To<HolidayInfoRepositoryNH<HolidayInfo>>();
            Bind<IRmPromotionGroupThresholdRepository<RmPromotionGroupThreshold>>().To<RmPromotionGroupThresholdRepositoryNH<RmPromotionGroupThreshold>>();

            Bind<IMNPSessionInfoRepository<MNPSessionInfo>>().To<MNPSessionInfoRepositoryNH<MNPSessionInfo>>();

            Bind<IBusinessOperationExecutionRepository<BusinessOperationExecution>>().To<BusinessOperationExecutionRepositoryNH<BusinessOperationExecution>>();

            Bind<IOrderRepository<Order>>().To<OrderRepositoryNH<Order>>();
            Bind<IOperationConfigurationRepository<OperationConfiguration>>().To<OperationConfigurationRepositoryNH<OperationConfiguration>>();

            Bind<IMultiLingualDescriptionRepository<MultiLingualDescription>>().To<MultiLingualDescriptionRepositoryNH<MultiLingualDescription>>();
            Bind<IBusinessOperationRepository<BusinessOperation>>().To<BusinessOperationRepositoryNH<BusinessOperation>>();
            Bind<IMVNODataRoamingLimitNotificationRepository<MVNODataRoamingLimitNotification>>().To<MVNODataRoamingLimitNotificationRepositoryNH<MVNODataRoamingLimitNotification>>();
            Bind<IMNPOriginalNrnInfoRepository<MNPOriginalNrnInfo>>().To<MNPOriginalNrnInfoRepositoryNH<MNPOriginalNrnInfo>>();
            Bind<IMNPNpdbEsvfInfoRepository<MNPNpdbEsvfInfo>>().To<MNPNpdbEsvfInfoRepositoryNH<MNPNpdbEsvfInfo>>();

            // By AnnaM
            Bind<ICrmMobileNetworkInfoRepository<CrmMobileNetWorkInfo>>().To<CrmMobileNetworkInfoRepositoryNH<CrmMobileNetWorkInfo>>();
            Bind<ICrmMobileMultipleImsiInfoRepository<CrmMobileMultipleImsiInfo>>().To<CrmMobileMultipleImsiInfoRepositoryNH<CrmMobileMultipleImsiInfo>>();

            // By NugrohoS
            Bind<ITroubleTicketInfoRepository<TroubleTicketInfo>>().To<TroubleTicketInfoRepositoryNH<TroubleTicketInfo>>();
            Bind<ITroubleTicketQuestionInfoRepository<TroubleTicketQuestionInfo>>().To<TroubleTicketQuestionInfoRepositoryNH<TroubleTicketQuestionInfo>>();
            Bind<ITTHistoryInfoRepository<TTHistoryInfo>>().To<TTHistoryInfoRepositoryNH<TTHistoryInfo>>();

            Bind<IProductOfferingCatalogRepository<ProductOfferingCatalog>>().To<ProductOfferingCatalogRepositoryNH<ProductOfferingCatalog>>();
            Bind<IProductOfferingRepository<ProductOffering>>().To<ProductOfferingRepositoryNH<ProductOffering>>();
            Bind<IProductOfferingGroupRepository<ProductOfferingGroup>>().To<ProductOfferingGroupRepositoryNH<ProductOfferingGroup>>();

            //for inventory
            Bind<IPhysicalResourceSpecificationRepository<PhysicalResourceSpecification>>().To<PhysicalResourceSpecificationRepositoryNH<PhysicalResourceSpecification>>();
            Bind<IProductRepository<PhysicalProduct>>().To<ProductRepositoryNH<PhysicalProduct>>();
            Bind<IOrderRepository<CustomerOrder>>().To<OrderRepositoryNH<CustomerOrder>>();
            Bind<IProductInventoryRepository<ProductInventory>>().To<ProductInventoryRepositoryNH<ProductInventory>>();
            Bind<IVoucherCardInfoRepository<VoucherCardInfo>>().To<VoucherCardInfoRepositoryNH<VoucherCardInfo>>();


            Bind<ISessionInfoRepository<SessionInfo>>().To<SessionInfoRepositoryNH<SessionInfo>>();

            //Dani
            Bind<ICarrierRepository<Carrier>>().To<CarrierRepositoryNH<Carrier>>();
            Bind<IPaymentInfoRepository<PaymentInfo>>().To<PaymentInfoRepositoryNH<PaymentInfo>>();

            Bind<IMVNORetailMarginRepository<MVNORetailMargin>>().To<MVNORetailMarginRepositoryNH<MVNORetailMargin>>();


            Type orderRepoGenericType = typeof (IOrderRepository<>);
            Type orderRepoNHGenericType = typeof (OrderRepositoryNH<>);
            
            //OrderDynamicTypes
            foreach (Type orderType in DuplicatedDiscriminatorChecker.GetAllOrders())
            {
                Type orderRepository = orderRepoGenericType.MakeGenericType(orderType);
                Type nhOrderRepo = orderRepoNHGenericType.MakeGenericType(orderType);
                Bind(orderRepository).To(nhOrderRepo);
            }

            //Businesss Opertaions Dynamic types
            Type requestRepoGenericType = typeof(IBusinessOperationRepository<>);
            Type requestRepoNHGenericType = typeof(BusinessOperationRepositoryNH<>);
            foreach (Type requestType in DuplicatedDiscriminatorChecker.GetAllBizOps())
            {
                Type requestRepository = requestRepoGenericType.MakeGenericType(requestType);
                Type nhRequestRepo = requestRepoNHGenericType.MakeGenericType(requestType);
                Bind(requestRepository).To(nhRequestRepo);
            }
        }
    }
}
