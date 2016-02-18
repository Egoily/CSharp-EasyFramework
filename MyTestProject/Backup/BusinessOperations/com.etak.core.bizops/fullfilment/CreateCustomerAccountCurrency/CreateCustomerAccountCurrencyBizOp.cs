using System;
using System.Linq;
using System.Reflection;
using com.etak.core.customer.message.CreateAccountCurrency;
using com.etak.core.GSMSubscription.messages.GetResourceMBInfosByCustomerID;
using com.etak.core.microservices.messages.GetMultiLingualDescriptionById;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.product.message.GetBillCyclesByVMNO;
using log4net;

namespace com.etak.core.bizops.fullfilment.CreateCustomerAccountCurrency
{
    /// <summary>
    /// BizOp to Create Customer AccountCurrency
    /// </summary>
    public class CreateCustomerAccountCurrencyBizOp : AbstractSinglePhaseOrderProcessor<CreateCustomerAccountCurrencyRequestDTO, CreateCustomerAccountCurrencyResponseDTO, CreateCustomerAccountCurrencyRequestInternal, CreateCustomerAccountCurrencyResponseInternal, CreateCustomerAccountCurrencyOrder>
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Mapping not Automapped Inbound Properties
        /// </summary>
        /// <param name="request">CreateCustomerAccountCurrencyRequestDTO</param>
        /// <param name="coreInput">CreateCustomerAccountCurrencyRequestInternal</param>
        protected override void MapNotAutomappedOrderInboundProperties(CreateCustomerAccountCurrencyRequestDTO request, ref CreateCustomerAccountCurrencyRequestInternal coreInput)
        {
            coreInput.CustomerInfo = request.CustomerDto.ToCore();

        }

        /// <summary>
        /// Mapping not Automapped Outbound Properties
        /// </summary>
        /// <param name="source">CreateCustomerAccountCurrencyResponseInternal</param>
        /// <param name="DTOOutput">CreateCustomerAccountCurrencyResponseDTO</param>
        protected override void MapNotAutomappedOrderOutboundProperties(CreateCustomerAccountCurrencyResponseInternal source, ref CreateCustomerAccountCurrencyResponseDTO DTOOutput)
        {
        }

        /// <summary>
        /// Process logic of CreateCustomerAccountCurrency
        /// </summary>
        /// <param name="order"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public override CreateCustomerAccountCurrencyResponseInternal ProcessRequest(CreateCustomerAccountCurrencyOrder order, CreateCustomerAccountCurrencyRequestInternal request)
        {
            var createAccountMs = MicroServiceManager.GetMicroService<CreateAccountCurrencyRequest, CreateAccountCurrencyResponse>();
            var getMultiLingualDescriptionMs = MicroServiceManager.GetMicroService<GetMultiLingualDescriptionByIdRequest, GetMultiLingualDescriptionByIdResponse>();
            var getSubscriptionMs = MicroServiceManager.GetMicroService<GetResourceMBInfosByCustomerIDRequest, GetResourceMBInfosByCustomerIDResponse>();

            #region Get Config For CreateCustomerAccountCurrencyBizOp

            var config = GetOperationConfigForDealer<CreateCustomerAccountCurrencyConfiguration>(request.MVNO);
            #endregion

            if (request.BillCycleForCustomer.IsNull())
                request.BillCycleForCustomer = GetBillCycle(request.MVNO, config.BillcycleId);

            #region Get Description and Name

            Log.Info(string.Format("Get MultiLingualDescription for description with ID {0}", config.AccountDescriptionId));
            var getMultiLingualReq = new GetMultiLingualDescriptionByIdRequest()
            {
                MultiLingualDescriptionId = config.AccountDescriptionId,
            };
            var getMultiLingualResp = getMultiLingualDescriptionMs.Process(getMultiLingualReq, null);
            if (getMultiLingualResp.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException("Cannot get the MultiLinguialDescription with Id " + config.AccountDescriptionId, BizOpsErrors.MultiLingualDescriptionNotFound);
            MultiLingualDescription accountDescription = getMultiLingualResp.MultiLingualDescription;

            Log.Info(string.Format("Get MultiLingualDescription for Name with ID {0}", config.AccountNameId));
            getMultiLingualReq.MultiLingualDescriptionId = config.AccountNameId;
            getMultiLingualResp = getMultiLingualDescriptionMs.Process(getMultiLingualReq, null);
            if (getMultiLingualResp.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException("Cannot get the MultiLinguialDescription with Id " + config.AccountDescriptionId, BizOpsErrors.MultiLingualDescriptionNotFound);
            MultiLingualDescription accountName = getMultiLingualResp.MultiLingualDescription;
            #endregion

            Log.Info(string.Format("Create Customer Account for customer {0} with Billcycle {1}, currency {2}, description {3} and name {4}", request.CustomerInfo.CustomerID.Value,
                request.BillCycleForCustomer.Id, config.AccountCurrency, accountDescription, accountName));

            var getSubscriptionRequest = new GetResourceMBInfosByCustomerIDRequest
            {
                CustomerId = request.CustomerInfo.CustomerID.Value
            };

            var getSubscriptionResponse = getSubscriptionMs.Process(getSubscriptionRequest, null);


            AccountCurrency customerAccount = new AccountCurrency()
            {
                CurrentAsignedCustomer = request.CustomerInfo,
                Balance = new BalanceForAccount() { Balance = 0 },
                BillingCycle = request.BillCycleForCustomer,
                LastBillRun = null,
                Currency = config.AccountCurrency,
                Description = accountDescription,
                Name = accountName,
            };
            customerAccount.Balance.Account = customerAccount;

            var createAccountReq = new CreateAccountCurrencyRequest()
            {
                AccountCurrency = customerAccount
            };
            var createAccountRes = createAccountMs.Process(createAccountReq, null);
            
            return new CreateCustomerAccountCurrencyResponseInternal()
            {
                CustomerAccount = createAccountRes.AccountCurrency,
                Customer = request.CustomerInfo,
                Subscription = getSubscriptionResponse.ResourceMbInfos.FirstOrDefault(x => x.StatusID == (int)ResourceStatus.Active),
                ResultType = ResultTypes.Ok,
                ErrorCode = 0,
                Message = "Create AccountCurrency success",
                CreatedOrder = order
            };
        }

        /// <summary>
        /// Operation Discriminator for CreateCustomerAccountCurrency
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.CreateCustomerAccountCurrency; }
        }

        /// <summary>
        /// Operation Code for CreateCustomerAccountCurrency
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.CreateCustomerAccountCurrency; }
        }

        /// <summary>
        /// Function to get the corresponding billcycle by Id
        /// </summary>
        /// <param name="mvno">The MVNO registering</param>
        /// <param name="billCycleId">The Id to be getted</param>
        /// <returns></returns>
        private BillCycle GetBillCycle(DealerInfo mvno, Int32 billCycleId)
        {
            var getBillCycleMs = MicroServiceManager.GetMicroService<GetBillCyclesByVMNORequest, GetBillCyclesByVMNOResponse>();

            var getBillCycleReq = new GetBillCyclesByVMNORequest() { DealerInfo = mvno };
            var billCycleResp = getBillCycleMs.Process(getBillCycleReq, null);
            if (billCycleResp.ResultType != ResultTypes.Ok || billCycleResp == null || !billCycleResp.BillCycles.Any())
                throw new BusinessLogicErrorException("Cannot get the BillCycle", BizOpsErrors.BillcycleNotFound);

            BillCycle billCycle = billCycleResp.BillCycles.FirstOrDefault(x => x.Id == billCycleId);

            if (billCycle == null)
                throw new BusinessLogicErrorException(string.Format("Cannot get the specified BillCycleId {0}", billCycleId), BizOpsErrors.BillcycleNotFound);

            return billCycle;
        }
    }
}
