using System;

namespace com.etak.core.bizops
{
    /// <summary>
    /// Class discriminator for Orders
    /// </summary>
    static class OrderDiscriminators
    {
        #region assurance
        /// <summary>
        /// Discriminator for ApplyChargeAndSchedule order
        /// </summary>
        public const string ApplyChargeAndSchedule = "APLCHR";
        #endregion

        #region fullfilment
        /// <summary>
        /// Discriminator for ApplyCustomerPromotion
        /// </summary>
        public const String ApplyCustomerPromotionOrder = "ACPROM";

        /// <summary>
        /// Discriminator for CancelCustomerAndSubscription order
        /// </summary>
        public const string CancelCustomerAndSubscription = "CCUASU";

        /// <summary>
        /// Discriminator for Create Customer
        /// </summary>
        public const String CreateCustomerOperation = "CRCUST";

        /// <summary>
        /// PurchaseProduct
        /// </summary>
        public const String PurchaseProductOperation = "PURPRO";


        /// <summary>
        /// Assign Product to Customer
        /// </summary>
        public const String AssignProductToCustomerOrder = "ASSPRC";


        /// <summary>
        /// OperationCode for RegisterCustomer
        /// </summary>
        public const String RegisterCustomer = "REGCUS";

        /// <summary>
        /// Discriminator for ReserveMsisdn
        /// </summary>
        public const String ReserveMsisdnOrder = "RSVMSI";

        /// <summary>
        /// Discriminator for SwapSimCardOrder
        /// </summary>
        public const String SwapSimCardOrder = "SSIMC";

        /// <summary>
        /// Discriminator for UnreserveMsisdnOrder 
        /// </summary>
        public const String UnreserveMsisdnOrder = "URESMS";

        /// <summary>
        /// Class order discriminator for Update Customer 
        /// </summary>
        public const String UpdateCustomerOrder = "UPCUST";
        /// <summary>
        /// Order discriminator for QuerySubscriptionAndServicesAndPromotionsByMsisdnBizOp
        /// </summary>
        public const String QuerySubscriptionAndServicesAndPromotionsByMsisdn = "QSSPBM";

        /// <summary>
        /// Discriminator for Cancel Customer
        /// </summary>
        public const string CancelCustomerProduct = "CACUPR";

        /// <summary>
        /// Discriminator for FreezeCustomer
        /// </summary>
        public const string FreezeCustomer = "FRECUS";

        /// <summary>
        /// Discriminator for UnFreezeCustomer
        /// </summary>
        public const string UnFreezeCustomer = "UFRECU";

        /// <summary>
        /// Discriminator for CreateCustomerAccountCurrency
        /// </summary>
        public const string CreateCustomerAccountCurrency = "CRCAC";

        /// <summary>
        /// Discriminator for CreateTroubleTicket
        /// </summary>
        public const string CreateTroubleTicket = "CRTRTT";

        /// <summary>
        /// Discriminator for CreateTroubleTicket
        /// </summary>
        public const string CreateSession = "CRESES";

        #endregion

        #region opssuport
        #endregion

        #region revenue
        /// <summary>
        /// Discriminator for BenefitTransferOrder
        /// </summary>
        public const String BenefitTransferOrder = "BENTRA";

        /// <summary>
        /// ChargeService API
        /// </summary>
        public const String ChargeServiceOperation = "CHRSRV";

        /// <summary>
        /// Discriminator for ModifyCustomerCreditLimitOrder 
        /// </summary>
        public const String ModifyCustomerCreditLimitOrder = "MOCUCL";

         /// <summary>
        /// Discriminator for AddNonRecurringChargeToCustomerOrder
        /// </summary>
        public const String AddNonRecurringChargeToCustomerOrder = "ANRCC";


        /// <summary>
        /// Discriminator for CallDREQuerySubscriberPromotionOrder
        /// </summary>
        public const String CallDREQuerySubscriberPromotionOrder = "DREQSP";

        /// <summary>
        ///  Discriminator for CallDREUpdateSubscriberPromotionOrder
        /// </summary>
        public const String CallDREUpdateSubscriberPromotionOrder = "DREUSP";


        
        

        #endregion  

       
        /// <summary>
        ///  Change MSISDN Holder order discriminator
        /// </summary>
        public const String ChangeOfHolder = "CANHD";

        /// <summary>
        /// Send SMS order discriminator
        /// </summary>
        public const string SendSMSOrder = "SENSMS";
    }
}
