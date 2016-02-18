using System;

namespace com.etak.core.bizops
{
	/// <summary>
	/// Collection of errors codes produced in the Bussiness Operation solution, Starts in 50000 (Error base)
	/// Must be unique 
	/// </summary>
	public class BizOpsErrors
	{
		/// <summary>
		/// The operation has been successfull
		/// </summary>resharper
		public const int Ok = 0;

		/// <summary>
		/// Start range for errors prosuced in the bussiness Operations
		/// </summary>
		public const Int32 ErrorBase = 50000;

		/// <summary>
		/// Unable to find the customer provided
		/// </summary>
		public const Int32 CustomerNotFound = ErrorBase + 1;

		/// <summary>
		/// The customer provided does not have a subscription
		/// </summary>
		public const Int32 CustomerWithoutSubscriptions = ErrorBase + 2;

		/// <summary>
		/// The customer has multiple subscriptions, can't determine in which one the operation needs
		/// to be performed
		/// </summary>
		public const Int32 CustomerMultipleSubscriptions = ErrorBase + 3;

		/// <summary>
		/// Unable to find the dealer number
		/// </summary>
		public const Int32 DealerNumberNotFound = ErrorBase + 4;

		/// <summary>
		/// Dealer not Found
		/// </summary>
		public const Int32 DealerInfoNotFound = ErrorBase + 5;

		/// <summary>
		/// Owner related with entity not match
		/// </summary>
		public const Int32 OwnerNotMatch = ErrorBase + 6;

		/// <summary>
		/// Resource not in proper status
		/// </summary>
		public const Int32 ResourceNotInProperStatus = ErrorBase + 7;

		#region BenefitTransfer

		/// <summary>
		/// BenefitTransferSourceCustomerNull
		/// </summary>
		public const Int32 BenefitTransferSourceCustomerNull = ErrorBase + 8;

		/// <summary>
		/// BenefitTransferDestinationCustomerNull
		/// </summary>
		public const Int32 BenefitTransferDestinationCustomerNull = ErrorBase + 9;

		/// <summary>
		/// BenefitTransferSenderLimit
		/// </summary>
		public const Int32 BenefitTransferSenderLimit = ErrorBase + 10;

		/// <summary>
		/// MaxTransferDestinationLimit
		/// </summary>
		public const Int32 MaxTransferDestinationLimit = ErrorBase + 11;

		/// <summary>
		/// BenefitTransferReceiverLimit
		/// </summary>
		public const Int32 BenefitTransferReceiverLimit = ErrorBase + 12;
		/// <summary>
		/// TotalBenefitLimit
		/// </summary>
		public const Int32 TotalBenefitLimit = ErrorBase + 13;

		/// <summary>
		/// BenefitTransferNotEnoughBalance
		/// </summary>
		public const Int32 BenefitTransferNotEnoughBalance = ErrorBase + 14;

		/// <summary>
		/// BenefitTransferDonorNotActive
		/// </summary>
		public const Int32 BenefitTransferDonorNotActive = ErrorBase + 15;

		/// <summary>
		/// BenefitTransferReceiverNotActive
		/// </summary>
		public const Int32 BenefitTransferReceiverNotActive = ErrorBase + 16;
		#endregion

		/// <summary>
		/// CustomerPromotion not found
		/// </summary>
		public const Int32 CustomerPromotionNotFound = ErrorBase + 17;

		/// <summary>
		/// ProductChargeConfigurationError
		/// </summary>
		public const Int32 ProductChargeConfigurationError = ErrorBase + 18;

		/// <summary>
		/// CustomerAccountNotFound
		/// </summary>
		public const Int32 CustomerAccountNotFound = ErrorBase + 19;

		/// <summary>
		/// CustomerAccountAssociationNotFound
		/// </summary>
		public const Int32 CustomerAccountAssociationNotFound = ErrorBase + 20;

		/// <summary>
		/// BaseBundlePriorityNotFound
		/// </summary>
		public const Int32 BaseBundlePriorityNotFound = ErrorBase + 21;

		/// <summary>
		/// CreditNotEnough
		/// </summary>
		public const Int32 CreditNotEnough = ErrorBase + 22;

		/// <summary>
		/// NoPriceForCharge
		/// </summary>
		public const Int32 NoPriceForCharge = ErrorBase + 23;

		/// <summary>
		/// NoPromotionPlanIdsFound
		/// </summary>
		public const Int32 NoPromotionPlanIdsFound = ErrorBase + 24;

		/// <summary>
		/// ApplyCustomerPromotionMustProvideCustomerInfo
		/// </summary>
		public const Int32 ApplyCustomerPromotionMustProvideCustomerInfo = ErrorBase + 25;

		/// <summary>
		/// ApplyCustomerPromotionMustProvidePromotionPlanId
		/// </summary>
		public const Int32 ApplyCustomerPromotionMustProvidePromotionPlanId = ErrorBase + 26;

		/// <summary>
		/// Error getting information of RmPromotionPlanInfos
		/// </summary>
		public const Int32 ApplyCustomerPromotionCantGetPromotionsList = ErrorBase + 27;

		/// <summary>
		/// PromotionPlanHasNotSameDealerAsCustomer
		/// </summary>
		public const Int32 PromotionPlanHasNotSameDealerAsCustomer = ErrorBase + 28;

		/// <summary>
		/// PromotionExpired
		/// </summary>
		public const Int32 PromotionExpired = ErrorBase + 29;

		/// <summary>
		/// PromotionPlansNotFoundForSomeProvidedPromotionPlanIds
		/// </summary>
		public const Int32 PromotionPlansNotFoundForSomeProvidedPromotionPlanIds = ErrorBase + 30;

		/// <summary>
		/// IncompatibleProductsInList
		/// </summary>
		public const Int32 IncompatibleProductsInList = ErrorBase + 31;

		/// <summary>
		/// InvoiceNotFound
		/// </summary>
		public const Int32 InvoiceNotFound = ErrorBase + 32;

		/// <summary>
		/// NoTaxForPurchase
		/// </summary>
		public const Int32 NoTaxForPurchase = ErrorBase + 33;

		/// <summary>
		/// MoreThanOneTaxForPurcharse
		/// </summary>
		public const Int32 MoreThanOneTaxForPurcharse = ErrorBase + 34;

		/// <summary>
		/// AddressesNotFound
		/// </summary>
		public const Int32 AddressesNotFound = ErrorBase + 35;

		/// <summary>
		/// CustomerNoZipcode
		/// </summary>
		public const Int32 CustomerNoZipcode = ErrorBase + 36;

		/// <summary>
		/// ZipCodeWithNoTaxDefinition
		/// </summary>
		public const Int32 ZipCodeWithNoTaxDefinition = ErrorBase + 37;

		/// <summary>
		/// ZipCodeWithMoreOneTaxDefinition
		/// </summary>
		public const Int32 ZipCodeWithMoreOneTaxDefinition = ErrorBase + 38;

		/// <summary>
		/// SpanishTaxAuthorityCustomerNotProvided
		/// </summary>
		public const Int32 SpanishTaxAuthorityCustomerNotProvided = ErrorBase + 39;

		/// <summary>
		/// SpanishAuthorityTaxPercentageNoCustomerPRovided
		/// </summary>
		public const Int32 SpanishAuthorityTaxPercentageNoCustomerPRovided = ErrorBase + 40;

		/// <summary>
		/// NoTaxRatesForTaxDefinitionId
		/// </summary>
		public const Int32 NoTaxRatesForTaxDefinitionId = ErrorBase + 41;

		/// <summary>
		/// ProductChargeOptionOfAnotherProduct
		/// </summary>
		public const Int32 ProductChargeOptionOfAnotherProduct = ErrorBase + 42;

		/// <summary>
		/// ProductChargeOptionNotFound
		/// </summary>
		public const Int32 ProductChargeOptionNotFound = ErrorBase + 43;

		/// <summary>
		/// UnableFindBaseBundlePriorityForCreateCustomerService
		/// </summary>
		public const Int32 UnableFindBaseBundlePriorityForCreateCustomerService = ErrorBase + 44;

		/// <summary>
		/// NoLimitServicesConfiguration
		/// </summary>
		public const Int32 NoLimitServicesConfiguration = ErrorBase + 45;

		/// <summary>
		/// ErrorGettingLimitServicesConfiguration
		/// </summary>
		public const Int32 ErrorGettingLimitServicesConfiguration = ErrorBase + 46;

		/// <summary>
		/// CustomerAlreadyDeleted
		/// </summary>
		public const Int32 CustomerAlreadyDeleted = ErrorBase + 47;

		/// <summary>
		/// ProductNotFound
		/// </summary>
		public const Int32 ProductNotFound = ErrorBase + 48;


		/// <summary>
		/// CustomerWithoutDealer
		/// </summary>
		public const int CustomerWithoutDealer = ErrorBase + 49;
		/// <summary>
		/// InvoicesIsNull
		/// </summary>
		public const int InvoicesIsNull = ErrorBase + 50;
		/// <summary>
		/// CreditLimtiInRequestLessThanZero
		/// </summary>
		public const int CreditLimtiInRequestLessThanZero = ErrorBase + 51;
		/// <summary>
		/// ServicesInfoIsNull
		/// </summary>
		public const int ServicesInfoIsNull = ErrorBase + 52;
		/// <summary>
		/// CustomerDoesNotHaveABaseCreditLimit
		/// </summary>
		public const int CustomerDoesNotHaveABaseCreditLimit = ErrorBase + 53;
		/// <summary>
		/// DealerId is null
		/// </summary>
		public const int DealerIdIsNull = ErrorBase + 54;

		/// <summary>
		/// Product is not active
		/// </summary>
		public const Int32 ProductNotActive = ErrorBase + 55;

		/// <summary>
		/// Error updating unbilled balance
		/// </summary>
		public const Int32 ErrorUpdatingUnbilledBalance = ErrorBase + 56;

		/// <summary>
		/// Customer is not valid
		/// </summary>
		public const Int32 NotValidCustomer = ErrorBase + 57;

		/// <summary>
		///  Cuustomer by Id not found
		/// </summary>
		public const Int32 CustomerByIdNotFound = ErrorBase + 58;
		/// <summary>
		/// Customer Info Not Found
		/// </summary>
		public const Int32 CustomerInfoNotFound = ErrorBase + 59;
		/// <summary>
		/// Customer Does Not Have External CustomerId
		/// </summary>
		public const Int32 CustomerDoesNotHaveExternalCustomerId = ErrorBase + 60;
		/// <summary>
		/// Customer Does Not Have First Name
		/// </summary>
		public const Int32 CustomerDoesNotHaveFirstName = ErrorBase + 61;
		/// <summary>
		/// Customer Does Not Have Last Name
		/// </summary>
		public const Int32 CustomerDoesNotHaveLastName = ErrorBase + 62;
		/// <summary>
		/// Customer Does Not Have LastName2
		/// </summary>
		public const Int32 CustomerDoesNotHaveLastName2 = ErrorBase + 63;
		/// <summary>
		/// Customer Does Not Have DocumentNumber
		/// </summary>
		public const Int32 CustomerDoesNotHaveDocumentNumber = ErrorBase + 64;
		/// <summary>
		/// Customer Does Not Have FiscalAddress
		/// </summary>
		public const Int32 CustomerDoesNotHaveFiscalAddress = ErrorBase + 65;
		/// <summary>
		/// Customer Does Not Have CustomerAddress
		/// </summary>
		public const Int32 CustomerDoesNotHaveCustomerAddress = ErrorBase + 66;
		/// <summary>
		/// Customer Does Not HaveDelivery Address
		/// </summary>
		public const Int32 CustomerDoesNotHaveDeliveryAddress = ErrorBase + 67;
		/// <summary>
		/// CustomerDoesNotHaveBankInformation
		/// </summary>
		public const Int32 CustomerDoesNotHaveBankInformation = ErrorBase + 68;
		/// <summary>
		/// CustomerDoesNotHaveBankNumber
		/// </summary>
		public const Int32 CustomerDoesNotHaveBankNumber = ErrorBase + 69;
		/// <summary>
		/// CustomerDoesNotHaveOwnerBankInformation
		/// </summary>
		public const Int32 CustomerDoesNotHaveOwnerBankInformation = ErrorBase + 70;
		/// <summary>
		/// CustomerDoesNotHaveNationality
		/// </summary>
		public const Int32 CustomerDoesNotHaveNationality = ErrorBase + 71;
		/// <summary>
		/// CustomerAddressListIsNull
		/// </summary>
		public const Int32 CustomerAddressListIsNull = ErrorBase + 72;
		/// <summary>
		/// CustomerDoesNotHaveCity
		/// </summary>
		public const Int32 CustomerDoesNotHaveCity = ErrorBase + 73;
		/// <summary>
		/// CustomerDoesNotHaveCountryId
		/// </summary>
		public const Int32 CustomerDoesNotHaveCountryId = ErrorBase + 74;
		/// <summary>
		/// CustomerDoesNotHaveHouseNo
		/// </summary>
		public const Int32 CustomerDoesNotHaveHouseNo = ErrorBase + 75;
		/// <summary>
		/// CustomerDoesNotHaveState
		/// </summary>
		public const Int32 CustomerDoesNotHaveState = ErrorBase + 76;

		/// <summary>
		/// MSISDN not found
		/// </summary>
		public const int MSISDNNotFound = ErrorBase + 77;

		/// <summary>
		/// ResourceMBNotFound
		/// </summary>
		public const int ResourceMBNotFound = ErrorBase + 78;

		/// <summary>
		/// NumberPropertyNotFound
		/// </summary>
		public const int NumberPropertyNotFound = ErrorBase + 79;

		/// <summary>
		/// CustomerInDeletedStatus
		/// </summary>
		public const int CustomerInDeletedStatus = ErrorBase + 80;



		/// <summary>
		/// HPSErrorCode recibed when execute DeleteSubscriber
		/// </summary>
		public const int DeleteSubscriberHPSErrorCode = ErrorBase + 81;

		/// <summary>
		/// CustomersPromotionNotFound
		/// </summary>
		public const int CustomersPromotionNotFound = ErrorBase + 82;

		/// <summary>
		/// Status is not correct
		/// </summary>
		public const int StatusError = ErrorBase + 83;

		/// <summary>
		/// Customer not created
		/// </summary>
		public const int CustomerNotCreated = ErrorBase + 84;

		/// <summary>
		/// The operation configuration is required and was not found
		/// </summary>
		public const int MissingOperationConfiguration = ErrorBase + 85;
		/// <summary>
		/// If the subscription is not found
		/// </summary>
		public static int SubscriptionNotFound = ErrorBase + 86;
		/// <summary>
		/// When the subscribtion has CustomerInfo = null
		/// </summary>
		public static int SubcriptionWithoutCustomer = ErrorBase + 87;

		/// <summary>
		/// Error getting the MultiLingualDescription from DB
		/// </summary>
		public static int MultiLingualDescriptionNotFound = ErrorBase + 88;
		/// <summary>
		/// Error getting BillCycle from DB
		/// </summary>
		public static int BillcycleNotFound = ErrorBase + 89;
		/// <summary>
		/// Error creating a new subscription
		/// </summary>
		public static int ErrorCreatingSubscription = ErrorBase + 90;
		/// <summary>
		/// Error creating a new customer account
		/// </summary>
		public static int ErrorCreatingcustomerAccount = ErrorBase + 91;
		/// <summary>
		/// Error creating a new invoice
		/// </summary>
		public static int ErrorCreatingInvoice = ErrorBase + 92;
		/// <summary>
		/// BizOp PurchaseProduct resturned with an error
		/// </summary>
		public static int ErrorPurchasingProducts = ErrorBase + 93;

		/// <summary>
		/// Customer data is null
		/// </summary>
		public const int CustomerIsNull = ErrorBase + 94;



		/// <summary>
		/// Invoice Due Date Is Not Valid
		/// </summary>
		public const int InvoiceDueDateIsNotValid = ErrorBase + 95;
		/// <summary>
		/// Failed to create Address
		/// </summary>
		public const int CreateAddressFailed = ErrorBase + 96;
		/// <summary>
		/// Customer doesn't have business type
		/// </summary>
		public const int BussinesTypeIsNull = ErrorBase + 97;
		/// <summary>
		/// Invoice detail is not vaild/null
		/// </summary>
		public const int InvoiceDetailIsNull = ErrorBase + 98;
		/// <summary>
		/// Registration type is null
		/// </summary>
		public const int RegistrationTypeIsNull = ErrorBase + 99;
		/// <summary>
		/// Payment type is null
		/// </summary>
		public const int PaymentTypeIsNull = ErrorBase + 100;
		/// <summary>
		/// Customer is in pending status
		/// </summary>
		public const int CustomerHaveInvalidStatus = ErrorBase + 101;
		/// <summary>
		/// The Number Status is not valid
		/// </summary>
		public static int NumberInWrongStatus = ErrorBase + 102;
		/// <summary>
		/// Error trying to activate the Simcard
		/// </summary>
		public static int ErrorActivatingSimcard = ErrorBase + 103;
		/// <summary>
		/// Error trying to activate the Number
		/// </summary>
		public static int ErrorActivatingNumber = ErrorBase + 104;
		/// <summary>
		/// When the microservice CalculateNextBillRun returns an error
		/// </summary>
		public static int ErrorGettingNextBillRun = ErrorBase + 105;


		/// <summary>
		/// Start date is not provided
		/// </summary>
		public const Int32 StartDateIsNotProvided = ErrorBase + 106;

		/// <summary>
		/// End date is not provided
		/// </summary>
		public const Int32 EndDateIsNotProvided = ErrorBase + 107;

		/// <summary>
		/// Operation disiscriminator is not provided
		/// </summary>
		public const Int32 OperationDiscriminatorIsNotProvided = ErrorBase + 108;

		/// <summary>
		/// Limit of the promotions reached (configurable in DB).
		/// </summary>
		public const int PromotionLimitReached = ErrorBase + 109;
		/// <summary>
		/// CheckPurchase unexpected fail
		/// </summary>
		public const int CheckPurchaseProductsFailed = ErrorBase + 110;

		/// <summary>
		/// Error claculating nect BillrunDate
		/// </summary>
		public const int CalculateNextBillrunDateError = ErrorBase + 111;

		/// <summary>
		/// Unexpected error creating customerProduct assginment
		/// </summary>
		public const int CreateCustomerProductAssignmentError = ErrorBase + 112;

		/// <summary>
		/// Creating charge And Schedule fail unexpected
		/// </summary>
		public const int ApplyChargeAndScheduleError = ErrorBase + 113;

		/// <summary>
		/// Create customerchargeandschedule unexpected error
		/// </summary>
		public const int CreateCustomerChargeAndScheduleError = ErrorBase + 114;

		/// <summary>
		/// List of requirements not satisfied for customer
		/// </summary>
		public const Int32 ListRequirementsNotSatisfiedForCustomer = ErrorBase + 115;

		/// <summary>
		/// List not compatible with customer products
		/// </summary>
		public const Int32 ListNotCompatibleWithCustomerProducts = ErrorBase + 116;

		/// <summary>
		/// There is not a price for ChargeID int this date
		/// </summary>
		public const Int32 NotPriceForChargeIdInDate = ErrorBase + 117;

		/// <summary>
		/// Cannot Create a Customer Promotion
		/// </summary>
		public const Int32 CustomerPromotionCreationError = ErrorBase + 118;

		#region SwapSimCard
		/// <summary>
		/// if Old Sim is Null
		/// </summary>
		public static int SourceSimNull = ErrorBase + 119;
		/// <summary>
		/// if Destination Sim is null
		/// </summary>
		public static int DestinationSimNull = ErrorBase + 120;
		/// <summary>
		/// Error if Resource not found
		/// </summary>
		public static int ResourceNotFound = ErrorBase + 121;
		/// <summary>
		/// Error for Different Resources
		/// </summary>
		public static int ResourceDiferent = ErrorBase + 122;
		#endregion

		/// <summary>
		/// Create CustomerProductsNotFound unexpected error
		/// </summary>
		public const int CustomerProductsNotFound = ErrorBase + 123;

		/// <summary>
		/// SimCardNotFound
		/// </summary>
		public const int SimCardNotFound = ErrorBase + 124;

		/// <summary>
		/// Can not cancel ServiceInfo from a product
		/// </summary>
		public const Int32 CanNotCancelService = ErrorBase + 125;

		/// <summary>
		/// Can not cancel Packages from a product
		/// </summary>
		public const Int32 CanNotCancelPackage = ErrorBase + 126;

		/// <summary>
		/// Can not cancel customerChargeSchedule from a product
		/// </summary>
		public const Int32 CanNotCancelCustomerChargeSchedule = ErrorBase + 127;

		/// <summary>
		/// Can not cancel Product
		/// </summary>
		public const Int32 CanNotCancelProduct = ErrorBase + 128;


		/// <summary>
		/// Imei not found
		/// </summary>
		public const Int32 ImeiNotFound = ErrorBase + 129;

		/// <summary>
		/// Disable 4g service fail
		/// </summary>
		public const Int32 Disable4GServiceFail = ErrorBase + 130;

		/// <summary>
		/// we don't have any 4g productids configurations
		/// </summary>
		public const Int32 No4GProductsIdsConfiguration = ErrorBase + 131;
		/// <summary>
		/// The ProvisionTemplate subministrated cannot be found in the Database
		/// </summary>
		public const Int32 ProvisionTemplateNotFound = ErrorBase + 132;

		/// <summary>
		/// ApplyCustomerPromtion BizOP unexpected error
		/// </summary>
		public const Int32 ApplyCustomerPromotionError = ErrorBase + 133;

		/// <summary>
		/// ResetFske
		/// </summary>
		public const Int32 ResetFakePromotoinError = ErrorBase + 134;

		/// <summary>
		/// Customer is in pending status
		/// </summary>
		public const int CustomerInPendingSatus = ErrorBase + 135;

		/// <summary>
		/// The CustomerProductAssignment Id Cannot be found between the given dates
		/// </summary>
		public const Int32 CustomerProductAssignmentNotFound = ErrorBase + 136;

		/// <summary>
		/// The Schedule Id cannot be found
		/// </summary>
		public const Int32 ScheduleNotFound = ErrorBase + 137;

		/// <summary>
		/// The TaxDefinition cannot be found by its category
		/// </summary>
		public const Int32 TaxDefinitionNotFoundByCategory = ErrorBase + 138;

		/// <summary>
		/// The Charge cannot be found by its id
		/// </summary>
		public const Int32 ChargeNotFound = ErrorBase + 139;
		/// <summary>
		/// The customer is not active
		/// </summary>
		public const Int32 CustomerIsNotActive = ErrorBase + 140;
		/// <summary>
		/// Charge is not a of type non-recurring
		/// </summary>
		public const Int32 ChargeInfoNotChargeNonRecurringType = ErrorBase + 141;
		/// <summary>
		/// doesn't exist information for the CustomerID
		/// </summary>
		public const Int32 CustomerInfoByCustomerIdNotFound = ErrorBase + 142;
		/// <summary>
		/// Cannot find Account
		/// </summary>
		public const Int32 AccountInfoNotFound = ErrorBase + 143;
		/// <summary>
		/// datetime less than the Invoice start date
		/// </summary>
		public const Int32 InvalidChargeDate = ErrorBase + 144;
		/// <summary>
		/// CustomerDefinition not found
		/// </summary>
		public const Int32 CustomerDefinitionNotFound = ErrorBase + 145;
		/// <summary>
		/// DateActiveAccount not found
		/// </summary>
		public const Int32 DateActiveAccountNotFound = ErrorBase + 146;
		/// <summary>
		/// Not enough credit
		/// </summary>
		public const Int32 HasNotCreditForPurchase = ErrorBase + 147;
		/// <summary>
		/// ActiveCustomerAccountAssociation Not Found
		/// </summary>
		public const Int32 ActiveCustomerAccountAssociationNotFound = ErrorBase + 148;
		/// <summary>
		/// ChargeInfo cannot be found
		/// </summary>
		public const Int32 ChargeInfoByChargeCatalogIdNotFound = ErrorBase + 149;

		/// <summary>
		/// Unable to find the property of customer provided
		/// </summary>
		public const Int32 CustomerProperyNotFound = ErrorBase + 150;
		/// <summary>
		/// CustomerChargeSchedule is null
		/// </summary>
		public const Int32 CustomerChargeScheduleIsNull = ErrorBase + 151;
		/// <summary>
		/// Error calling the Update Customer Microservice
		/// </summary>
		public const Int32 ErrorUpdatingCustomer = ErrorBase + 152;
		/// <summary>
		/// NoNumberOfInvoicesConfiguration
		/// </summary>
		public const Int32 NoNumberOfInvoicesConfiguration = ErrorBase + 153;

		/// <summary>
		/// InvalidMNVNO
		/// </summary>
		public const Int32 MVNODontHavePermision = ErrorBase + 154;
		/// <summary>
		/// Unexpected error assigning Association for the customer account
		/// </summary>
		public const Int32 ErrorCreatingcustomerAccountASSN = ErrorBase + 155;
		/// <summary>
		/// Authorize user and password with customer id
		/// </summary>
		public const Int32 AuthorizeErrorUser = ErrorBase + 156;

		/// <summary>
		/// StartDate Is Later Than EndDate
		/// </summary>
		public const Int32 StartDateIsLaterThanEndDate = ErrorBase + 157;

		/// <summary>
		/// Wrong charge when calling specific API
		/// </summary>
		public const Int32 WrongCharge = ErrorBase + 158;
		/// <summary>
		/// StatusId is 20 in ResourceMBInfo by CustomerId
		/// </summary>
		public const Int32 CustomerIdDeleted = ErrorBase + 159;
		/// <summary>
		/// Customer Does Not Have an Active Resource
		/// </summary>
		public const Int32 CustomerDoesNotHaveActiveResource = ErrorBase + 160;
		/// <summary>
		/// customer did't have send benefit permission 
		/// </summary>
		public const Int32 BenefitTransferNoSendPermission = ErrorBase + 161;

		/// <summary>
		///  customer did't have receive benefit permission 
		/// </summary>
		public const Int32 BenefitTransferNoReceivePermission = ErrorBase + 162;
		/// <summary>
		/// customer is expired
		/// </summary>
		public const Int32 CustomerTerminated = ErrorBase + 163;
		/// <summary>
		/// customer is rejected
		/// </summary>
		public const Int32 CustomerRejected = ErrorBase + 164;

		/// <summary>
		/// SettingInfos is null/empty
		/// </summary>
		public const Int32 SettingInfosIsNull = ErrorBase + 165;
		/// <summary>
		/// SettingInfo With Certain SettingId Is Null
		/// </summary>
		public const Int32 SettingInfoWithCertainSettingIdIsNull = ErrorBase + 166;
		/// <summary>
		/// SettingDetailInfos Is Null
		/// </summary>
		public const Int32 SettingDetailInfosIsNull = ErrorBase + 167;
		/// <summary>
		/// SettingDetailInfo Is Null
		/// </summary>
		public const Int32 SettingDetailInfoIsNull = ErrorBase + 168;
		/// <summary>
		/// SettingDetailInfo Unit Is Not Recognizable
		/// </summary>
		public const Int32 SettingDetailInfoUnitIsNotRecognizable = ErrorBase + 169;

		/// <summary>
		/// Product Has Been Expired Cannot be Removed
		/// </summary>
		public const int ProductHasBeenExpired = ErrorBase + 170;

		/// <summary>
		///  the cancel date to product is not valid
		/// </summary>
		public const int InvalidCancelDate = ErrorBase + 171;

		/// <summary>
		/// Detected more than 1 Base Bundle generating new services for the Customer
		/// </summary>
		public static int MultipleBaseBubdleInCustomerServices = ErrorBase + 172;

        /// <summary>
        /// failed when creating Trouble Ticket
        /// </summary>
        public static int TroubleTicketInfoIsNull = ErrorBase + 173;

        /// <summary>
        /// Trouble Ticket Question info is null
        /// </summary>
        public static int TroubleTicketQuestionInfoIsNull = ErrorBase + 174;

        /// <summary>
        /// Deteced Document Type has changed for the customer
        /// </summary>
        public static int CustomerDocumentTypeHasChanged = ErrorBase + 175;

        /// <summary>
        /// Deteced Document Number has changed for the customer
        /// </summary>
        public static int CustomerDocumentNumberHasChanged = ErrorBase + 176;

        /// <summary>
        /// the requesting DocumentNumbmer doesn't match with the MSISDN
        /// </summary>
        public static int DocumentNumbmerDoesnotMatchWithTheMSISDN = ErrorBase + 177;
        
        /// <summary>
        /// the new documentID has been binded with another msisdn which is frozen status
        /// </summary>
	public static int ResourceStatusIsFrozen = ErrorBase + 178;

        /// <summary>
        /// Benefit transfer invoke DRE querySubscriberPromotion api Failure
        /// </summary>
        public const Int32 InvokeDREQuerySubscriberPromotionFailure = ErrorBase + 179;

        /// <summary>
        /// Benefit transfer invoke DRE updateSubscriberPromotion api Failure
        /// </summary>
        public const Int32 InvokeDREUpdateSubscriberPromotionFailure = ErrorBase + 180;
        /// <summary>
        /// When a master bundle for a customer cannot be found
        /// </summary>
	    public const Int32 MasterBundleNotFound = ErrorBase + 181;
	}
}

