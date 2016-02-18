using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using com.etak.core.customer.message.AddChargeScheduleToCustomer;
using com.etak.core.customer.message.AddChargeToCustomer;
using com.etak.core.customer.message.GetAccountById;
using com.etak.core.customer.message.GetActiveCustomerAccountAssociationByDate;
using com.etak.core.customer.message.GetCustomerChargesScheduleById;
using com.etak.core.customer.message.GetInvoicesByCustomerIdAndLegalInvoiceNumber;
using com.etak.core.dealer.messages.GetDealerInfoById;
using com.etak.core.GSMSubscription.messages.GetCustomerProductAssignmentsByCustomerIDAndDatePeriod;
using com.etak.core.microservices.messages.GetTaxDefinitonsByCategory;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.contract;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.product.message.GetChargeById;
using com.etak.core.service.messages.AddUnbilledBalance;
using com.etak.core.service.messages.CustomerHasCredit;
using com.etak.eventsystem.model.dto;
using com.etak.eventsystem.model.events;
using com.etak.eventsystem.wcfSender;
using log4net;
using Newtonsoft.Json;

namespace com.etak.core.bizops.assurance.ApplyChargeAndSchedule
{
	/// <summary>
	/// Busines operation that adds a CustomerCharge and/or CustomerCharge schedule if necesary
	/// </summary>
	public class ApplyChargeAndScheduleBizOp : AbstractSinglePhaseOrderProcessor<ApplyChargeAndScheduleDTORequest, ApplyChargeAndScheduleDTOResponse,
													  ApplyChargeAndScheduleRequest, ApplyChargeAndScheduleResponse, 
													  ApplyChargeAndScheduleOrder>
	{
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// Maps all the inboud properties of the request that are not mapped by the framework
		/// </summary>
		/// <param name="request">the request in ET DTO Form</param><param name="coreInput">the resquest partially mapped by the core and needs to be updated</param>
		protected override void MapNotAutomappedOrderInboundProperties(ApplyChargeAndScheduleDTORequest request, ref ApplyChargeAndScheduleRequest coreInput)
		{
			var getChargeByIdMS = MicroServiceManager.GetMicroService<GetChargeByIdRequest, GetChargeByIdResponse>();
            var getLastInvoiceMS = MicroServiceManager.GetMicroService<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest, GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse>();
			var getCustomerProductAssignment = MicroServiceManager.GetMicroService<GetCustomerProductAssignmentsByCustomerIDAndDatePeriodRequest, GetCustomerProductAssignmentsByCustomerIDAndDatePeriodResponse>();
			var getDealerByIdMS = MicroServiceManager.GetMicroService<GetDealerInfoByIdRequest, GetDealerInfoByIdResponse>();
			var getScheduleByIdMS = MicroServiceManager.GetMicroService<GetCustomerChargesScheduleByIdRequest, GetCustomerChargesScheduleByIdResponse>();
            var getTaxDefinition = MicroServiceManager.GetMicroService<GetTaxDefinitonsByCategoryRequest, GetTaxDefinitonsByCategoryResponse>();
            var getCustomerAccount = MicroServiceManager.GetMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();
            var getCustomerInfoByAccount = MicroServiceManager.GetMicroService<GetAccountByIdRequest, GetAccountByIdResponse>();
			CustomerInfo customer = coreInput.Customer;

		    if (customer == null)
		    {
                var customerInfoRequest = new GetAccountByIdRequest()
		        {
		            AccountId = request.AccountId
		        };
                var customerInfoResponse = getCustomerInfoByAccount.Process(customerInfoRequest, null);
                if (customerInfoResponse.ResultType != ResultTypes.Ok || customerInfoResponse.Account == null)
                    throw new BusinessLogicErrorException(string.Format("Cannot find CustomerInfo for AccountId {0}", customerInfoRequest.AccountId), BizOpsErrors.CustomerIsNull);

		        var customerInfoResult = customerInfoResponse.Account.CurrentAsignedCustomer;
		        if (customerInfoResult == null)
		        {
                    throw new DataValidationErrorException(String.Format("CustomerInfo can't be found by {0} AccountId", customerInfoRequest.AccountId), BizOpsErrors.CustomerInfoNotFound);
		        }
		        customer = customerInfoResult;
		        coreInput.Customer = customer;
		    }

			#region Get Charge
			loggerInfo(string.Format("Get Charge By Id {0}", request.ChargeId));
			var getChargeResp = getChargeByIdMS.Process(new GetChargeByIdRequest()
			{
				ChargeId = request.ChargeId,
			}, null);
			var chargeToAdd = getChargeResp.Charge;
			coreInput.ChargeToAdd = chargeToAdd;
			if (coreInput.ChargeToAdd == null)
				throw new BusinessLogicErrorException(String.Format("Cannot find Charge with Id {0}", request.ChargeId), BizOpsErrors.ChargeNotFound);
			#endregion

			#region GetInvoice
            loggerInfo(string.Format("Get last invoice for CustomerID {0}",customer.CustomerID.Value));
            var getLastInvoiceResp = getLastInvoiceMS.Process(new GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest()
			{
				CustomerId = customer.CustomerID.Value,
                LegalInvoiceNumber = null
			}, null);
			if (getLastInvoiceResp.ResultType != ResultTypes.Ok || getLastInvoiceResp.Invoices == null || !getLastInvoiceResp.Invoices.Any())
                throw new BusinessLogicErrorException(String.Format("Cannot get the  last Invoice for CustomerID {0}",
                     customer.CustomerID.Value), BizOpsErrors.InvoiceNotFound);

			coreInput.InvoiceOfCharge = getLastInvoiceResp.Invoices.FirstOrDefault();
                 
			#endregion

			#region Get CustomerProductAssignment

			if (request.CustomerProductAssignmentId.HasValue)
			{
				loggerInfo(string.Format("Get Customer Product Assignment for CustomerID {0}, with CustomerProductAssigmentID {1}, StartDate {2} and EndDate {3}",
										 customer.CustomerID.Value, request.CustomerProductAssignmentId, request.StartDate, request.StartDate));
				var getCustProdAssignResp = getCustomerProductAssignment.Process(new GetCustomerProductAssignmentsByCustomerIDAndDatePeriodRequest()
					{
						CustomerID = customer.CustomerID.Value,
						StartDate = request.StartDate,
						EndDate = request.StartDate,
					}, null);

                if (getCustProdAssignResp.ResultType != ResultTypes.Ok || getCustProdAssignResp.CustomerProductAssignments == null || getCustProdAssignResp.CustomerProductAssignments.FirstOrDefault(x => x.Id == request.CustomerProductAssignmentId) == null)
                    throw new BusinessLogicErrorException(string.Format("The CustomerProductAssignmentId {0} cannot be found for Customer {1} between dates {2} and {3}",
                        request.CustomerProductAssignmentId, customer.CustomerID.Value, request.StartDate, request.StartDate), BizOpsErrors.CustomerProductAssignmentNotFound);

				coreInput.CustomerProductAssignment = getCustProdAssignResp.CustomerProductAssignments.FirstOrDefault(x => x.Id == request.CustomerProductAssignmentId);
			}

			#endregion

			#region Get Dealer
			loggerInfo(string.Format("Get DealerInfo for Customer {0} and DealerId {1}", customer.CustomerID.Value, customer.DealerID.Value));
			var getDealerResp = getDealerByIdMS.Process(new GetDealerInfoByIdRequest()
			{
				DealerId = customer.DealerID.Value,
			}, null);
			coreInput.CustomerDealer = getDealerResp.DealerInfo;
			if (coreInput.CustomerDealer == null)
				throw new BusinessLogicErrorException(String.Format("Cannot get DealerInformation for Customer {0} and DealerId {1}", customer.CustomerID.Value, customer.DealerID.Value),BizOpsErrors.DealerInfoNotFound);
			#endregion

			#region Get Schedule

			if (request.ScheduleId.HasValue)
			{
				loggerInfo(string.Format("Getting Schedule Id {0} to be applied to the Customer", request.ScheduleId.Value));
				var getScheduleResp = getScheduleByIdMS.Process(new GetCustomerChargesScheduleByIdRequest()
				{
					customerChargeId = request.ScheduleId.Value,
				}, null);
				coreInput.Schedule = getScheduleResp.CustomerChargeSchedule;
				if (coreInput.Schedule != null)
					throw new BusinessLogicErrorException(string.Format("Cannot get Schedule Id {0}", request.ScheduleId.Value), BizOpsErrors.ScheduleNotFound);
			}

			#endregion

			#region Get Tax Definition
			var getTaxDefResp = getTaxDefinition.Process(new GetTaxDefinitonsByCategoryRequest()
			{
				TaxCategory = request.TaxCategory,
			}, null);
			coreInput.TaxDefinition = getTaxDefResp.TaxDefinitions.FirstOrDefault();
			if (coreInput.TaxDefinition == null)
				throw new BusinessLogicErrorException(String.Format("Cannot get TaxDefinition by category {0}", request.TaxCategory), BizOpsErrors.TaxDefinitionNotFoundByCategory);
			#endregion

            #region Checking account data exist
            if (coreInput.Account == null)
            {
                var getCustomerAccountreq = new GetActiveCustomerAccountAssociationByDateRequest()
                {
                    CustomerInfo = new CustomerInfo()
                    {
                        CustomerID = request.CustomerId
                    },
                    ActiveCustomerAccountAssociationDate = DateTime.Now
                };
                var accountTemp = getCustomerAccount.Process(getCustomerAccountreq, null);
                if (accountTemp.CustomerAccountAssociation == null || accountTemp.CustomerAccountAssociation.Account == null)
                    throw new BusinessLogicErrorException(String.Format("Cannot get Customer Account with {0} CustomerId", request.CustomerId), BizOpsErrors.AccountInfoNotFound);

                coreInput.Account = accountTemp.CustomerAccountAssociation.Account;
            } 
            #endregion

			coreInput.StartDate = request.StartDate;
			coreInput.PriceEffectiveDate = request.PriceEffectiveDate;
			coreInput.CustomAmount = request.Amount;
		    coreInput.TypeOfCharges = request.TypeOfCharges;

		}

		/// <summary>
		/// Maps all the outboud properties of the response that are not mapped by the framework
		/// </summary>
		/// <param name="source">the response of the core operation that needs to be mapped</param><param name="DTOOutput">the response of the operation in DTO format</param>
		protected override void MapNotAutomappedOrderOutboundProperties(ApplyChargeAndScheduleResponse source, ref ApplyChargeAndScheduleDTOResponse DTOOutput)
		{
			DTOOutput.customerCharge = source.ChargeAdde.ToDto();
		}

		/// <summary>
		/// The implementation of IOrderProcessor
		/// </summary>
		/// <param name="order">The order to be processed</param><param name="request">The request to process</param>
		/// <returns>
		/// The result of the process
		/// </returns>
		public override ApplyChargeAndScheduleResponse ProcessRequest(ApplyChargeAndScheduleOrder order, ApplyChargeAndScheduleRequest request)
		{

			ValidateRequest(request);

			#region Request Parameters
			Charge chargeToAdd = request.ChargeToAdd;
			CustomerInfo customerToBeCharged = request.Customer;
			Invoice invoiceOfCharge = request.InvoiceOfCharge;
			Account accountOfCharge = request.Account;
			CustomerProductAssignment customerProductAssignment = request.CustomerProductAssignment;
			DealerInfo customerDealer = request.CustomerDealer;
			DateTime startDate = request.StartDate;
			DateTime? priceEffectiveDate = request.PriceEffectiveDate;
			Decimal? amountCharged = request.CustomAmount;
			#endregion

			#region Response Parameters
			CustomerCharge chargeCreated = null;
			CustomerChargeSchedule chargeScheduleCreated = null; 
			#endregion

			#region Micro Services
			IMicroService<AddChargeToCustomerRequest, AddChargeToCustomerResponse> addChargeMS = MicroServiceManager.GetMicroService<AddChargeToCustomerRequest, AddChargeToCustomerResponse>();
			IMicroService<AddChargeScheduleToCustomerRequest, AddChargeScheduleToCustomerResponse> addScheduleMS = MicroServiceManager.GetMicroService<AddChargeScheduleToCustomerRequest, AddChargeScheduleToCustomerResponse>();
			IMicroService<CustomerHasCreditRequest, CustomerHasCreditResponse> hasCreditMS = MicroServiceManager.GetMicroService<CustomerHasCreditRequest, CustomerHasCreditResponse>(); 
			IMicroService<AddUnbilledBalanceRequest, AddUnbilledBalanceResponse> addUnbilledBalanceMS = MicroServiceManager.GetMicroService<AddUnbilledBalanceRequest, AddUnbilledBalanceResponse>();
			#endregion

            #region Get Configuration
            var config = GetOperationConfigForDealer<ApplyChargeAndScheduleConfiguration>(request.MVNO);
            #endregion

            //If it's Recurring we need to crate the Shceduling and calculate the prorating information
			//Invoiking pospaid charging system 
		    if (chargeToAdd is ChargeRecurring)
		    {
                loggerInfo(string.Format("Trying to add a Recurring charge {0}... Validate we can do it", chargeToAdd.Id));
		        if (request.TypeOfCharges == TypeOfCharges.NonRecurringChargeAllowed)
		        {
                    throw new BusinessLogicErrorException(string.Format("Cannot apply recurring charge {0} to customer {1} as TypeOfCharges is set to {2}",
                                                          chargeToAdd.Id, customerToBeCharged.CustomerID.Value, request.TypeOfCharges), BizOpsErrors.WrongCharge);
		        }

		        loggerInfo("Charge to be added is Recurring... Calling AddChargeScheduleToCustomer");

		        #region Create Recurring Charge

		        AddChargeScheduleToCustomerRequest sceduleReq = new AddChargeScheduleToCustomerRequest
		        {
		            AccountCharged = accountOfCharge,
		            ChargeInCatalog = chargeToAdd,
		            CustomerToBeCharged = customerToBeCharged,
		            NextChargeDate = startDate,
		            NextPeriodNumber = 1,
		            OriginatingPurchase = customerProductAssignment,
		            PriceEffectiveDate = priceEffectiveDate,
		        };

		        var schedRes = addScheduleMS.Process(sceduleReq, null);
		        chargeScheduleCreated = schedRes.CreatedSchedule;
                #endregion
		    }
		    else
		    {
                loggerInfo(string.Format("Trying to add a Non Recurring Charge {0}... Validate we can do it", chargeToAdd.Id));
		        if (request.TypeOfCharges == TypeOfCharges.RecurringChargeAllowed)
		        {
                    throw new BusinessLogicErrorException(string.Format("Cannot apply non recurring charge {0} to customer {1} as TypeOfCharges is set to {2}",
                                                          chargeToAdd.Id, customerToBeCharged.CustomerID.Value, request.TypeOfCharges), BizOpsErrors.WrongCharge);
		        }

		        loggerInfo("Charge to be added is Non Recurring... Calling AddChargeToCustomer");
		        #region Create Charge
                AddChargeToCustomerRequest chargeReq = new AddChargeToCustomerRequest
		        {
		            Amount = amountCharged,
		            TaxToApply = request.TaxDefinition,

		            AccountToBeCharged = accountOfCharge,
		            ChargeInCatalog = chargeToAdd,
		            ChargingDate = startDate,
		            CustomerProductAssingment = customerProductAssignment,
		            CustomerToBeCharged = customerToBeCharged,
		            InvoceToBeCharged = invoiceOfCharge,

		            CycleNumber = request.CycleNumber,
		            Schedule = request.Schedule,
		            PeriodNumber = request.CycleNumber,
                    AmountIsInformational = request.AmountIsInformational
		        };
                
		        var chargeRes = addChargeMS.Process(chargeReq, null);
		        chargeCreated = chargeRes.ChargeCreated;
		        //Let's inform the amount to check if the customer has credit
		        if (chargeCreated == null)
		            throw new BusinessLogicErrorException(string.Format("Data CustomerCharge for Customer {0} is not found !!!",request.Customer.CustomerID.Value),BizOpsErrors.ErrorBase);
		        amountCharged = chargeCreated.Amount;

		        #endregion


		    }

		    #region Check if Customer has credit
			var hasCreditReq = new CustomerHasCreditRequest()
		    {
				Amount = amountCharged ?? 0,
				DateOfCharge = startDate,
				CustomerInfo = customerToBeCharged,
			};

			var hasCreditResp = hasCreditMS.Process(hasCreditReq, null);

			if (!hasCreditResp.HasCredit)
                    throw new BusinessLogicErrorException("The Customer doesn't have enough credit", BizOpsErrors.CreditNotEnough);
		    #endregion


			#region Send Event of Charge Applied and Update Unbilled Balance (if corresponds)
			if (amountCharged.HasValue && amountCharged.Value > 0)
			{
				if (customerDealer == null)
					throw new BusinessLogicErrorException(string.Format("The DealerInfo cannot be null for customer {0} " +
														  "when it is needed to send an Event for the applied charge {1}",
														  customerToBeCharged.CustomerID.Value, chargeToAdd.Id),
														  BizOpsErrors.DealerIdIsNull);

				ServicesInfo masterBundle = hasCreditResp.MasterBundle;
				if(masterBundle == null)
					throw new BusinessLogicErrorException(string.Format("Data masterBundle is not found !!!"), BizOpsErrors.ErrorBase);
				var oldBalance = masterBundle.UnBilledBalance;
				var newBalance = masterBundle.UnBilledBalance + amountCharged;

				#region Update Unbilled Balance
				var addUnbilledReq = new AddUnbilledBalanceRequest()
				{
					Amount = amountCharged.Value,
					ServicesInfo = masterBundle,
				};
				var addUnbilledResp = addUnbilledBalanceMS.Process(addUnbilledReq, null);

				if (addUnbilledResp.ResultType != ResultTypes.Ok)
					throw new BusinessLogicErrorException(string.Format("Error updating the Unbilled Balance ({0}) with an amount of {1} for Customer {2}",
															masterBundle.UnBilledBalance, amountCharged, customerToBeCharged.CustomerID.Value), BizOpsErrors.ErrorUpdatingUnbilledBalance);
				#endregion


                if (config.SendChargeEvent)
				    SendChargeAppliedEvent(request, customerDealer, oldBalance, newBalance);
			}
			
			#endregion

			return new ApplyChargeAndScheduleResponse
			{
				ErrorCode = 0,
				ResultType = ResultTypes.Ok,
				Message = String.Empty,
				ScheduleAdded = chargeScheduleCreated,
				ChargeAdde = chargeCreated,
                Subscription = request.Customer.ResourceMBInfo.FirstOrDefault()

			};
		}

		/// <summary>
		/// Code that will be stored in the operation log for the operation
		/// </summary>
		public override string OperationCode
		{
			get { return OperationCodes.ApplyChargeAndSchedule; }
		}

		/// <summary>
		/// Unique identifier of the operation
		/// </summary>
		public override string OperationDiscriminator
		{
			get { return OperationDiscriminators.ApplyChargeAndSchedule; }
		}

        #region Private methods
        /// <summary>
        /// Validate the parameters on Request that needs it
        /// </summary>
        /// <param name="request"></param>
        private void ValidateRequest(ApplyChargeAndScheduleRequest request)
        {
            if (request.ChargeToAdd == null)
                throw new BusinessLogicErrorException("Charge cannot be null", BizOpsErrors.NotValidCustomer);

            if (request.StartDate == null || request.StartDate == DateTime.MinValue)
                throw new BusinessLogicErrorException("Start date cannot be null", BizOpsErrors.NotValidCustomer);

        }

        /// <summary>
        /// SendChargeAppliedEvent to EventSystem
        /// </summary>
        /// <param name="bizOpReq"></param>
        /// <param name="custDealer"></param>
        /// <param name="oldUnbilledBalance"></param>
        /// <param name="newUnbilledBalance"></param>
        private void SendChargeAppliedEvent(ApplyChargeAndScheduleRequest bizOpReq, DealerInfo custDealer, decimal? oldUnbilledBalance, decimal? newUnbilledBalance)
        {
            CustomPayloadEvent ev = new CustomPayloadEvent();
            ev.EventType = "ChargeApplied";
            ev.EventDate = DateTime.Now;
            DateTime EpochDate = new DateTime(2014, 01, 01);
            TimeSpan Elapsed = DateTime.Now.Date - EpochDate;
            int index = Elapsed.Seconds;
            ev.EventId = new Random(index).Next(0, 2147483647).ToString();
            CustomerInfo customer = bizOpReq.Customer;
            DealerInfo customerDealer = custDealer;

            ev.MVNOID = customerDealer.FiscalUnitID.Value;

            List<Service> iServiceList = customer.ServicesInfo.Select(t => EventToCoreModelHelper.FromCoreServicesInfo(t)).ToList();

            var schedule = bizOpReq.Schedule != null
                ? JsonConvert.SerializeObject(EventToCoreModelHelper.FromCoreCustomerChargeSchedule(bizOpReq.Schedule))
                : string.Empty;

            var resourceMb = customer.ResourceMBInfo.Any()
                ? customer.ResourceMBInfo.FirstOrDefault().Resource
                : string.Empty;

            ev.Payload = "{Customer:" + JsonConvert.SerializeObject(EventToCoreModelHelper.FromCoreCustomerInfo(customer)) +
                         ", Account: " + JsonConvert.SerializeObject(EventToCoreModelHelper.FromCoreAccount(bizOpReq.Account)) +
                         ", CustomerProductAssignment: " + JsonConvert.SerializeObject(EventToCoreModelHelper.FromCoreCustomerProductAssignment(bizOpReq.CustomerProductAssignment)) +
                         ", ChargeDefinition: " + JsonConvert.SerializeObject(EventToCoreModelHelper.FromCoreCharge(bizOpReq.ChargeToAdd)) +
                         ", Schedule: " + schedule +
                         ", Services : " + JsonConvert.SerializeObject(iServiceList) +
                         ", Msisdn: '" + resourceMb + "'" +
                         ", OldUnbilledBalance: " + oldUnbilledBalance +
                         ", NewUnbilledBalance: " + newUnbilledBalance + "}";

            QueuedEventSender.GetInstance().ProcessEvent(ev);
        }

        /// <summary>
        /// Create Informational Log 
        /// </summary>
        /// <param name="message"></param>
        private void loggerInfo(String message)
        {
            if (logger.IsDebugEnabled)
                logger.Info(message);
        } 
        #endregion

        /// <summary>
        /// Enum to configure the allowance to apply a certain type of charge
        /// </summary>
        public enum TypeOfCharges
        {
            /// <summary>
            /// Default Option, all charges can be applied
            /// </summary>
            AllChargesAllowed = 0,
            /// <summary>
            /// Allow only Recurring Charges to be applied
            /// </summary>
            RecurringChargeAllowed = 1,
            /// <summary>
            /// Allow only Non Recurring Charges to be applied
            /// </summary>
            NonRecurringChargeAllowed = 2,

        }
	}
}
