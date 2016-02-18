using System;

namespace com.etak.core.bizops
{
    /// <summary>
    /// Discriminators for the bussiness operations. MUST BE UNIQUE
    /// </summary>
    public static class OperationDiscriminators
    {
        #region Assurance
        /// <summary>
        /// Operation code for ApplyChargeAndSchedule 
        /// </summary>
        public const string ApplyChargeAndSchedule = "APLCHR";
        #endregion

        #region Fullfilment

        /// <summary>
        /// Operation Code for ApplyCustomerPromotion
        /// </summary>
        public const String ApplyCustomerPromotionOperation = "ACPROM";

        /// <summary>
        /// OperationCode for CancelCustomerAndSubscriptions
        /// </summary>
        public const String CancelCustomerAndSubscriptionsOperation = "CCUASU";

        /// <summary>
        /// Operation code for check purcharse product for customer
        /// </summary>
        public const String CheckPurchaseProductForCustomerBizOp = "CHPPFC";

        /// <summary>
        /// OperationCode for Create Customer
        /// </summary>
        public const String CreateCustomerOperation = "CRCUST";

        /// <summary>
        /// PurchaseProduct
        /// </summary>
        public const String PurchaseProductOperation = "PURPRO";

        /// <summary>
        /// Assign Product to Customer
        /// </summary>
        public const String AssignProductToCustomerOperation = "ASSPRC";


        /// <summary>
        /// OperationCode for Query Available MSISDN Operation
        /// </summary>
        public const String QueryAvailableMSISDNOperation = "QAVMSI";

        /// <summary>
        /// OperationCode for QueryCustomerByCustomerId Operation
        /// </summary>
        public const String QueryCustomerByCustomerIdOperation = "QCCI";

        /// <summary>
        /// OperationCode for Query Customer By Document Id Operation
        /// </summary>
        public const String QueryCustomerByDocumentIdOperation = "QCDI";

        /// <summary>
        /// OperationCode for Query Customer By Exact Msisdn Operation
        /// </summary>
        public const String QueryCustomerByMsisdnOperation = "QCMS";

        /// <summary>
        /// OperationCode for Query Customer By ExternalId Operation
        /// </summary>
        public const String QueryCustomerByExternalIdOperation = "QCEI";

        /// <summary>
        /// OperationCode for query customer product operation
        /// </summary>
        public const String QueryCustomerProductOperation = "QCUPRO";

        /// <summary>
        /// OperationCode for query purchase product operation
        /// </summary>
        public const String QueryPurchaseProductOperation = "QPUPRO";

        /// <summary>
        /// OperationCode for Query Sim Card
        /// </summary>
        public const String QuerySimCard = "QUSICA";

        /// <summary>
        /// OperationCode for Query subscription by customer id
        /// </summary>
        public const string QuerySubscriptionByCustomerId = "QSUCID";

        /// <summary>
        /// OperationCode for Query Suscription by MSISDN
        /// </summary>
        public const string QuerySubscriptionByMSISDN = "QSMSIS";

        /// <summary>
        /// OperationCode for RegisterCustomer
        /// </summary>
        public const String RegisterCustomer = "REGCUS";

        /// <summary>
        /// OperationCode for ReserveMsisdn
        /// </summary>
        public const String ReserveMsisdnOperation = "RSVMSI";

        /// <summary>
        /// Operation Codes for SwapSimCard
        /// </summary>
        public const String SwapSimCardOperation = "SSCD";

        /// <summary>
        /// OperationCode for Unreserve Msisdn Operation
        /// </summary>
        public const String UnreserveMsisdnOperation = "URESMS";

        /// <summary>
        /// OperationCode for Update customer information operation
        /// </summary>
        public const String UpdateCustomerOperation = "UPCUST";
        
        /// <summary>
        /// Operation discriminator for Cancel Customer
        /// </summary>
        public const string CancelCustomerProduct = "CACUPR";

        /// <summary>
        /// Operation discriminator for FreezeCustomer
        /// </summary>
        public const string FreezeCustomer = "FRECUS";

        /// <summary>
        /// Operation discriminator for UnFreezeCustomer
        /// </summary>
        public const string UnFreezeCustomer = "UFRECU";

        /// <summary>
        /// Operation Discriminator for CreateCustomerAccountCurrency
        /// </summary>
        public const string CreateCustomerAccountCurrency = "CRCAC";

        /// <summary>
        /// Operation Discriminator for CreateTroubleTicket
        /// </summary>
        public const string CreateTroubleTicket = "CRTRTT";

        /// <summary>
        /// Operation Discriminator for CreateSession
        /// </summary>
        public const string CreateSession = "CRESES";

        

        #endregion

        #region opssuport
        /// <summary>
        /// OperationCode for Query Product Catalog By Id
        /// </summary>
        public const String QueryProductCatalogById = "QPCBID";

        /// <summary>
        /// OperationDiscriminators for Query TroubleTickets By CustomerId
        /// </summary>
        public const String QueryTroubleTicketsByCustomerId = "QTTCI";
        
        #endregion

        #region revenue
        /// <summary>
        /// OperationCode for Benefit Transfer operation
        /// </summary>
        public const String BenefitTransferOperation = "BENTRA";

        /// <summary>
        /// ChargeService API
        /// </summary>
        public const String ChargeServiceOperation = "CHRSRV";

        /// <summary>
        /// Operation code for GetActiveProducsOfCustomerBizOp
        /// </summary>
        public const String QueryActiveProductsOfCustomerOperation = "QAPOCO";

        /// <summary>
        /// OperationCode for Modify Customer Credit Limit Operation 
        /// </summary>
        public const String ModifyCustomerCreditLimitOperation = "MOCUCL";

        /// <summary>
        /// OperationCode for Query Balance Operation
        /// </summary>
        public const String QueryBalanceOperation = "QRYBAL";

        /// <summary>
        /// Operation Codes for QueryCustomerRecurringChargesOperation
        /// </summary>
        public const String QueryCustomerRecurringChargesOperation = "QCRC";

        /// <summary>
        /// OperationCode for Query Customer Unbilled Charges Operation
        /// </summary>
        public const String QueryCustomerUnbilledChargesOperation = "QCUBCH";

        /// <summary>
        /// Operation code for Query Invoices By CustomerId
        /// </summary>
        public const String QueryInvoicesByCustomerId = "QICI";

        /// <summary>
        /// Operation code for Query Invoices By MSISDN
        /// </summary>
        public const String QueryInvoicesByMSISDN = "QIMSI";

        /// <summary>
        /// Operation code for QueryTransferencesBizOp
        /// </summary>
        public const String QueryTransferencesOperation = "QTO";

        /// <summary>
        /// OperationCode for Query Usage Details Operation
        /// </summary>
        public const String QueryUsageDetailsOperation = "QUSGD";
        /// <summary>
        /// operationCode for Add Non Recurring Charge to Custoemr
        /// </summary>
        public const String AddNonRecurringChargeToCustomerOperation = "ANRCC";


        /// <summary>
        /// Operatio n code for Call DRE QuerySubscriberPromotion Operation
        /// </summary>
        public const String CallDREQuerySubscriberPromotionOperation = "DREQSP";


        /// <summary>
        /// Operatio n code for Call DRE UpdateSubscriberPromotion Operation
        /// </summary>
        public const String CallDREUpdateSubscriberPromotionOperation = "DREUSP";

        

        #endregion revenue
        /// <summary>
        /// OperationCode for QuerySubscriptionAndServicesAndPromotionsByCustomerId
        /// </summary>
        public const String QuerySubscriptionAndServicesAndPromotionsByCustomerId = "QSSPC";

        /// <summary>
        /// Operation discriminator for QuerySubscriptionAndServicesAndPromotionsByMsisdnBizOp
        /// </summary>
        public const String QuerySubscriptionAndServicesAndPromotionsByMsisdn = "QSSPBM";

        /// <summary>
        ///  Change MSISDN Holder order discriminator
        /// </summary>
        public const String ChangeOfHolder = "CANHD";

        /// <summary>
        /// Send SMS order discriminator
        /// </summary>
        public const string SendSMS = "SENSMS";
    }
}
