using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.dto;
using com.etak.core.model.operation;
using com.etak.core.model.resource;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;
using com.etak.core.model.subscription.catalog;
using com.etak.core.model.usage;
using com.etak.core.operation.dtoConverters.customer;
using com.etak.core.operation.dtoConverters.operation;
using com.etak.core.operation.dtoConverters.product;
using com.etak.core.operation.dtoConverters.resource;
using com.etak.core.operation.dtoConverters.subscription;

namespace com.etak.core.operation.dtoConverters
{
    /// <summary>
    /// Static class to create all the mappings
    /// </summary>
    public static class DtoMappings
    {
        #region Declaration of the Dto converters
        private static readonly AddressDtoConverter AddressConverter = new AddressDtoConverter();
        private static readonly BankDtoConverter BankConverter = new BankDtoConverter();
        private static readonly CustomerDtoConverter CustomerConverter = new CustomerDtoConverter();
        private static readonly MSISDNResourceDTOConverter MsisdnResourceConverter = new MSISDNResourceDTOConverter();
        private static readonly SimCardDtoConverter SimCardConverter = new SimCardDtoConverter();
        private static readonly SubscriptionDtoConverter SubscriptionConverter = new SubscriptionDtoConverter();
        private static readonly ChargeCatalogDtoConverter ChargeCatalogConverter = new ChargeCatalogDtoConverter();
        private static readonly CustomerChargeDtoConverter CustomerChargeConverter = new CustomerChargeDtoConverter();
        private static readonly CustomerRecurringChargeDtoConverter CustomerRecurringChargeConverter = new CustomerRecurringChargeDtoConverter();
        private static readonly ProductCatalogDtoConverter ProductCatalogConverter = new ProductCatalogDtoConverter();
        private static readonly ProductPurchaseChargingOptionDtoConverter ProductPurchaseChargingOptionConverter = new ProductPurchaseChargingOptionDtoConverter();
        private static readonly CustomerProductAssingmentDtoConverter ProductPurchaseConverter = new CustomerProductAssingmentDtoConverter();
        private static readonly UsageDetailDtoConverter UsageDetailConverter = new UsageDetailDtoConverter();
        private static readonly InvoiceDtoConverter InvoiceConverter = new InvoiceDtoConverter();
        private static readonly BusinessOperationExecutionDTOConverter BizOpExecutionConverter = new BusinessOperationExecutionDTOConverter();
        private static readonly ServicesInfoDTOConverter ServicesInfoConverter = new ServicesInfoDTOConverter();
        private static readonly CrmCustomersPromotionDtoConverter CrmCustomersPromotionConverter = new CrmCustomersPromotionDtoConverter();
        private static readonly RmPromotionPlanDtoConverter RmPromotionPlanConverter = new RmPromotionPlanDtoConverter();
        private static readonly RmPromotionPlanDetailDtoConverter RmPromotionPlanDetailConverter = new RmPromotionPlanDetailDtoConverter();
        private static readonly PurchaseProductOfferingSelectionDtoConverter PurchaseProductOfferingSelectionConverter = new PurchaseProductOfferingSelectionDtoConverter();
        private static readonly MultiLingualDTOConverter MultiLingualDtoConverter = new MultiLingualDTOConverter();
        private static readonly ProductOfferingGroupDtoConverter ProductOfferingGroupConverter = new ProductOfferingGroupDtoConverter();
        private static readonly ProductOfferingSpecificationOptionDtoConverter ProductOfferingSpecificationOptionConverter = new ProductOfferingSpecificationOptionDtoConverter();
        private static readonly ProductDtoConverter ProductConverter = new ProductDtoConverter();
        private static readonly PaymentInfoDtoConverter PaymentInfoDtoConverter = new PaymentInfoDtoConverter();
        #endregion

        #region CustomerInfo Extensions
        /// <summary>
        /// Converts a CustomerDTO to it's core form CustomerInfo
        /// </summary>
        /// <param name="source">The object to be converted</param>
        /// <returns>The object in CustomerDTO</returns>
        public static CustomerInfo ToCore(this CustomerDTO source)
        {
            return CustomerConverter.Convert(source);
        }

        /// <summary>
        /// Converts a CustomerInfo to it's DTO form CustomerDTO
        /// </summary>
        /// <param name="source">The object to be converted</param>
        /// <returns>The object in CustomerDTO</returns>
        public static CustomerDTO ToDto(this CustomerInfo source)
        {
            return CustomerConverter.Convert(source);
        }
        #endregion

        #region AddresInfo Extensions
        /// <summary>
        /// Converts a AddressInfo to it's DTO form AddressDTO
        /// </summary>
        /// <param name="source">The object to be converted</param>
        /// <returns>The object in AddressDTO</returns>
        public static AddressDTO ToDto(this AddressInfo source)
        {
            return AddressConverter.Convert(source);
        }

        /// <summary>
        /// Converts a AddressDTO to it's DTO form AddressInfo
        /// </summary>
        /// <param name="source">The object to be converted</param>
        /// <returns>The object in AddressInfo</returns>
        public static AddressInfo ToCore(this AddressDTO source)
        {
            return AddressConverter.Convert(source);
        } 
        #endregion

        #region BankInfo Extensions
        /// <summary>
        /// Converts a BankInfo to it's DTO form BankInformationDTO
        /// </summary>
        /// <param name="source">The object to be converted</param>
        /// <returns>The object in BankInformationDTO</returns>
        public static BankInformationDTO ToDto(this BankInfo source)
        {
            return BankConverter.Convert(source);
        }

        /// <summary>
        /// Converts a BankInformationDTO to it's core form BankInfo
        /// </summary>
        /// <param name="source">The object to be converted</param>
        /// <returns>The object in BankInfo</returns>
        public static BankInfo ToCore(this BankInformationDTO source)
        {
            return BankConverter.Convert(source);
        }

        #endregion

        #region SimCardInfo Extensions
        /// <summary>
        /// Converts a SIMCardInfo to it's DTO form SimCardDTO
        /// </summary>
        /// <param name="source">The object to be converted</param>
        /// <returns>The object in SimCardDTO</returns>
        public static SimCardDTO ToDto(this SIMCardInfo source)
        {
            return SimCardConverter.Convert(source);
        }

        /// <summary>
        /// Converts a SimCardDTO to it's core form SIMCardInfo
        /// </summary>
        /// <param name="source">The object to be converted</param>
        /// <returns>The object in SIMCardInfo</returns>
        public static SIMCardInfo ToCore(this SimCardDTO source)
        {
            return SimCardConverter.Convert(source);
        }
        #endregion

        #region NumberInfo Extensions
        /// <summary>
        /// Converts a MSISDNResourceDTO to it's core form NumberInfo
        /// </summary>
        /// <param name="source">The object to be converted</param>
        /// <returns>The object in NumberInfo</returns>
        public static NumberInfo ToCore(this MSISDNResourceDTO source)
        {
            return MsisdnResourceConverter.Convert(source);
        }

        /// <summary>
        /// Converts a NumberInfo to it's DTO form MSISDNResourceDTO
        /// </summary>
        /// <param name="source">The object to be converted</param>
        /// <returns>The object in MSISDNResourceDTO</returns>
        public static MSISDNResourceDTO ToDto(this NumberInfo source)
        {
            return MsisdnResourceConverter.Convert(source);
        } 
        #endregion

        #region ResourceMBInfo
        /// <summary>
        /// Converts a SubscriptionDTO to it's core form ResourceMBInfo
        /// </summary>
        /// <param name="source">The object to be converted</param>
        /// <returns>The object in SubscriptionDTO</returns>
        public static ResourceMBInfo ToCore(this SubscriptionDTO source)
        {
            return SubscriptionConverter.Convert(source);
        }

        /// <summary>
        /// Converts a ResourceMBInfo to it's DTO form SubscriptionDTO
        /// </summary>
        /// <param name="source">The object to be converted</param>
        /// <returns>The object in SubscriptionDTO</returns>
        public static SubscriptionDTO ToDto(this ResourceMBInfo source)
        {
            return SubscriptionConverter.Convert(source);
        }
        #endregion

        #region ChargeCatalog
        /// <summary>
        /// Converts a Charge to it's DTO form ChargeCatalogDTO
        /// </summary>
        /// <param name="source">The object to be converted</param>
        /// <returns>The object in ChargeCatalogDTO</returns>
        public static ChargeCatalogDTO ToDto(this Charge source)
        {
            return ChargeCatalogConverter.Convert(source);
        }

        #endregion

        #region CustomerCharge
        /// <summary>
        /// Converts a CustomerCharge to it's DTO form CustomerChargeDTO
        /// </summary>
        /// <param name="source">The object to be converted</param>
        /// <returns>The object in CustomerChargeDTO</returns>
        public static CustomerChargeDTO ToDto(this CustomerCharge source)
        {
            return CustomerChargeConverter.Convert(source);
        }

        #endregion

        #region CustomerRecurringCharge
        /// <summary>
        /// Converts a CustomerCharge to it's DTO form CustomerRecurringChargeDTO
        /// </summary>
        /// <param name="source">The object to be converted</param>
        /// <returns>The object in CustomerRecurringChargeDTO</returns>
        public static CustomerRecurringChargeDTO ToRecurringChargeDto(this CustomerCharge source)
        {
            return CustomerRecurringChargeConverter.Convert(source);
        }

        #endregion

        #region ProductCatalog
        /// <summary>
        /// Converts a ProductOffering to it's DTO form ProductCatalogDTO
        /// </summary>
        /// <param name="source">The object to be converted</param>
        /// <returns>The object in ProductCatalogDTO</returns>
        public static ProductCatalogDTO ToDto(this ProductOffering source)
        {
            return ProductCatalogConverter.Convert(source);
        }

        /// <summary>
        /// Converts a ProductCatalogDTO into a Product object
        /// </summary>
        /// <param name="source">The Dto object to be converted</param>
        /// <returns></returns>
        public static ProductOffering ToCore(this ProductCatalogDTO source)
        {
            return ProductCatalogConverter.Convert(source);
        }

        #endregion

        #region ProductPurchaseChargingOption
        /// <summary>
        /// Converts a ProductChargeOption to it's DTO form ProductPurchaseChargingOptionDTO
        /// </summary>
        /// <param name="source">The object to be converted</param>
        /// <returns>The object in ProductPurchaseChargingOptionDTO</returns>
        public static ProductPurchaseChargingOptionDTO ToDto(this ProductChargeOption source)
        {
            return ProductPurchaseChargingOptionConverter.Convert(source);
        }

        /// <summary>
        /// Converts a ProductPurchaseChargingOptionDTO into a ProductChargeOption
        /// </summary>
        /// <param name="source">A dto object to be converted</param>
        /// <returns>The object as a ProductChargeOption</returns>
        public static ProductChargeOption ToCore(this ProductPurchaseChargingOptionDTO source)
        {
            return ProductPurchaseChargingOptionConverter.Convert(source);
        }

        #endregion

        #region ProductPurchase
        /// <summary>
        /// Converts a customer CustomerProductAssignment to it's DTO form CustomerProductAssignmentDTO
        /// </summary>
        /// <param name="source">The object to be converted</param>
        /// <returns>The object in CustomerProductAssignmentDTO</returns>
        public static CustomerProductAssignmentDTO ToDto(this CustomerProductAssignment source)
        {
            return ProductPurchaseConverter.Convert(source);
        }

        #endregion

        #region UsageDetail
        /// <summary>
        /// Converts a UsageDetailRecord to it's DTO form UsageDetailDTO
        /// </summary>
        /// <param name="source">The object to be converted</param>
        /// <returns>The object in UsageDetailDTO</returns>
        public static UsageDetailDTO ToDto(this UsageDetailRecord source)
        {
            return UsageDetailConverter.Convert(source);
        }

        #endregion

        #region Invoice
        /// <summary>
        /// Converts a Invoice to it's DTO form InvoiceDTO
        /// </summary>
        /// <param name="source">Invoice to be converted</param>
        /// <param name="aggregateTotalInvoiceId">The charge id corresponding to the aggregate Total Invoice</param>
        /// <returns></returns>
        public static InvoiceDTO ToDto(this Invoice source, int aggregateTotalInvoiceId)
        {
            return InvoiceConverter.Convert(source, aggregateTotalInvoiceId);
        }

        #endregion

        #region Bussiness Operation Execution
        /// <summary>
        /// Converts a  BusinessOperationExecution into it's dto form (BusinessOperationExecutionDTO)
        /// </summary>
        /// <param name="source">the BusinessOperationExecution to covert</param>
        /// <returns>the BusinessOperationExecutionDTO converted</returns>
        public static BusinessOperationExecutionDTO ToDto(this  BusinessOperationExecution source)
        {
            return BizOpExecutionConverter.Convert(source);
        }
        #endregion

        #region ServicesInfo
        /// <summary>
        /// Converts a ServicesInfo to a ServicesInfoDTO Extension method
        /// </summary>
        /// <param name="source">the source object to convert</param>
        /// <returns>the ServicesInfo in DTO form</returns>
        public static ServicesInfoDTO ToDto(this  ServicesInfo source)
        {
            return ServicesInfoConverter.Convert(source);
        }
        #endregion

        #region CrmCustomersPromotionInfo
        /// <summary>
        /// Converts a CrmCustomersPromotionInfo to a CrmCustomersPromotionInfoDto Extension method
        /// </summary>
        /// <param name="source">The source object to be converted</param>
        /// <returns>The DTO object obtained</returns>
        public static CrmCustomersPromotionInfoDTO ToDto(this CrmCustomersPromotionInfo source)
        {
            return CrmCustomersPromotionConverter.Convert(source);
        }
        /// <summary>
        /// Converts a CrmCustomersPromotionInfoDto to a CrmCustomersPromotionInfo Extension method
        /// </summary>
        /// <param name="source">The source DTO object to be converted</param>
        /// <returns>the core objecte obtained</returns>
        public static CrmCustomersPromotionInfo ToCore(this CrmCustomersPromotionInfoDTO source)
        {
            return CrmCustomersPromotionConverter.Convert(source);
        }

        #endregion

        #region RmPromotionPlanInfo

        /// <summary>
        /// Converts a RmPromotionPlanInfo to a RmPromotionPlanInfoDTO Extension method
        /// </summary>
        /// <param name="source">The Core object to be conveted</param>
        /// <returns>The Dto object obtained</returns>
        public static RmPromotionPlanInfoDTO ToDto(this RmPromotionPlanInfo source)
        {
            return RmPromotionPlanConverter.Convert(source);
        }

        /// <summary>
        /// Converts a RmPromotionPlanInfoDTO to a RmPromotionPlanInfo Extension method
        /// </summary>
        /// <param name="source">The Dto object to be converted</param>
        /// <returns>The Core object obtained</returns>
        public static RmPromotionPlanInfo ToCore(this RmPromotionPlanInfoDTO source)
        {
            return RmPromotionPlanConverter.Convert(source);
        }

        #endregion

        #region RmPromotionPlanDetailInfo
        /// <summary>
        /// Converts a RmPromotionPlanDetailInfoDTO to a RmPromotionPlanDetailInfo extension method
        /// </summary>
        /// <param name="source">The DTO object to be converted</param>
        /// <returns>The core object obtained</returns>
        public static RmPromotionPlanDetailInfo ToCore(this RmPromotionPlanDetailInfoDTO source)
        {
            return RmPromotionPlanDetailConverter.Convert(source);
        }
        /// <summary>
        /// Converts a RmPromotionPlanDetailInfo to a RmPromotionPlanDetailInfoDTO extension method
        /// </summary>
        /// <param name="source">The Core object to be converted</param>
        /// <returns>The dto object obtained</returns>
        public static RmPromotionPlanDetailInfoDTO ToDto(this RmPromotionPlanDetailInfo source)
        {
            return RmPromotionPlanDetailConverter.Convert(source);
        }

        #endregion

        #region PurchaseProductOfferingSelection
        /// <summary>
        /// Converts from PurchaseProductOfferingSelectionDTO to PurchaseProductOfferingSelection getting all 
        /// the options in a recursive way
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static PurchaseProductOfferingSelection ToCore(this PurchaseProductOfferingSelectionDTO source)
        {
            return PurchaseProductOfferingSelectionConverter.Convert(source);
        }

        #endregion

        #region MultiLingualConverter
        /// <summary>
        /// Converts from MultiLingualDescription to a list of Textual Descriptions 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IList<TextualDescription> ToDto(this MultiLingualDescription source)
        {
            return MultiLingualDtoConverter.Convert(source);
        }

        #endregion

        #region ProductOfferingGroupConverter

        /// <summary>
        /// Converts from ProductOfferingGroupOption to ProductOfferingGroupDTO
        /// </summary>
        /// <param name="source">The ProductOfferingGroupOption to be converted</param>
        /// <returns>Dto Object converted</returns>
        public static ProductOfferingGroupDTO ToDto(this ProductOfferingGroupOption source)
        {
            return ProductOfferingGroupConverter.Convert(source);
        }

        #endregion

        #region ProductOFferingSpecificationOption
        /// <summary>
        /// Convert from ProductOfferingSpecificationOption to ProductOfferingSpecificationDto
        /// </summary>
        /// <param name="source">ProductOfferingSpecificationOption to be converted</param>
        /// <returns>A dto object</returns>
        public static ProductOfferingSpecificationDTO ToDto(this ProductOfferingSpecificationOption source)
        {
            return ProductOfferingSpecificationOptionConverter.Convert(source);
        }

        #endregion

        #region Product
        /// <summary>
        /// Convert from Product entity to ProductDto, including inherited type PhysicalProduct
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ProductDto ToDto(this Product source)
        {
            return ProductConverter.Convert(source);
        }

        /// <summary>
        /// Convert from ProductDto to Product with the most basic information filled
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Product ToCore(this ProductDto source)
        {
            return ProductConverter.Convert(source);
        }

        #endregion

        #region PaymentInfo Extensions
        /// <summary>
        /// Convert from PaymentDataDTO object into PaymentInfo 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static PaymentInfo ToCore(this PaymentDataDTO dto)
        {
            return PaymentInfoDtoConverter.Convert(dto);
        }

        /// <summary>
        /// Convert from PaymentInfo into PaymentDataDto object
        /// </summary>
        /// <param name="core"></param>
        /// <returns></returns>
        public static PaymentDataDTO ToDto(this PaymentInfo core)
        {
            return PaymentInfoDtoConverter.Convert(core);
        }

        #endregion

    }
}
