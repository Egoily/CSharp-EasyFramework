using System;
using System.Collections.Generic;
using com.etak.core.bizops.fullfilment.PurchaseProductForCustomer;
using com.etak.core.customer.message.AddCrmCustomersBalanceTransationHistory;
using com.etak.core.customer.message.GetActiveCustomerAccountAssociationByDate;
using com.etak.core.customer.message.GetCustomersActivePromotionInfo;
using com.etak.core.customer.message.GetInvoicesByCustomerIdAndLegalInvoiceNumber;
using com.etak.core.customer.message.SubtractBalance;
using com.etak.core.GSMSubscription.messages.CheckProductListDependencyRelationsForCustomer;
using com.etak.core.GSMSubscription.messages.GetCustomerProductAssignmentsByCustomerId;
using com.etak.core.microservices.messages.GetSucessfulOperationExecutionForCustomer;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.operation.manager;
using com.etak.core.product.message.GetProductByProductId;
using com.etak.core.product.message.GetProductChargeOptionByProductChargeOptionId;
using com.etak.core.product.message.GetProductChargeOptionsByProductId;
using com.etak.core.product.message.GetProductOfferingByProductOfferingId;
using com.etak.core.promotion.messages.UpdateCustomersPromotion;
using com.etak.core.repository.crm.configuration;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.subscription.catalog;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using com.etak.core.bizops.revenue.BenefitTransfer;
using System.Linq;
using List = NHibernate.Mapping.List;
using com.etak.core.bizops.revenue.CallDREQuerySubscriberPromotion;
using com.etak.core.bizops.revenue.CallDREUpdateSubscriberPromotion;

namespace com.etak.core.bizops.UnitTests.revenue.BenefitTransfer
{
    [TestFixture]
    public class BenefitTransferBizOpUnitTest : AbstractSinglePhaseOrderProcessorTest<BenefitTransferBizOp, BenefitTransferRequestDTO, BenefitTransferResponseDTO, BenefitTransferRequestInternal, BenefitTransferResponseInternal, BenefitTransferOrder>
    {
        private ICoreBusinessOperation<PurchaseProductForCustomerRequestInternal, PurchaseProductForCustomerResponseInternal> mockPurchaseProductBizop;
        public void StandardMSMocks(BenefitTransferRequestDTO benefitRequestDto)
        {


            #region Product

            var actualProduct = CreateDefaultObject.Create<Product>();

            #endregion

            #region ActualCustomerInfo

            var actualCustomerSourceInfo = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomerSourceInfo.CustomerID = benefitRequestDto.SourceCustomerId;
            actualCustomerSourceInfo.Promotions.Clear();
            var customerpromo1 = CreateDefaultObject.Create<CrmCustomersPromotionInfo>();
            customerpromo1.Active = true;
            customerpromo1.PromotionDetail.RmPromotionPlanInfo.Accumulative = true;
            customerpromo1.PromotionDetail.SubServiceTypeId = 0;
            customerpromo1.CurrentLimit = benefitRequestDto.amount + 1;
            actualCustomerSourceInfo.Promotions.Add(customerpromo1);

            var actualCustomerDestinationInfo = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomerDestinationInfo.CustomerID = benefitRequestDto.DestinationCustomerId;
            actualCustomerDestinationInfo.Promotions.Clear();
            var customerpromo2 = CreateDefaultObject.Create<CrmCustomersPromotionInfo>();
            customerpromo2.Active = true;
            customerpromo2.PromotionDetail.RmPromotionPlanInfo.Accumulative = true;
            customerpromo2.PromotionDetail.SubServiceTypeId = 0;
            customerpromo2.CurrentLimit = 1;
            actualCustomerDestinationInfo.Promotions.Add(customerpromo2);
            #endregion


            #region mock getcustomerbyId


            var mockedCustomerRepo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            mockedCustomerRepo.GetById(benefitRequestDto.SourceCustomerId).Returns(actualCustomerSourceInfo);
            mockedCustomerRepo.GetById(benefitRequestDto.DestinationCustomerId).Returns(actualCustomerDestinationInfo);





            #endregion

            #region Mock OperationConfig
            var mockedRepoConfig = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var operationConfiguration = CreateDefaultObject.Create<OperationConfiguration>();
            var benefitConfig = new BenefitTransferConfiguration()
            {
                BenefitSourceTransferProductId = 2,
                BenefitDestinationTransferProductId = 1,
                ValidSourcePromotions = new List<int> { 1, 2, 3, 4 },
                BenefitTransferSenderLimit = 12,
                MaxTransferDestinationLimit = 1,
                BenefitTransferReceiverLimit = 10,
                TotalBenefitLimit = 14
            };
            operationConfiguration.JSonConfig = JsonConvert.SerializeObject(benefitConfig);
            operationConfiguration.StarTime = DateTime.Now;
            operationConfiguration.EndDate = DateTime.Now.AddYears(1);
            mockedRepoConfig.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(new List<OperationConfiguration>() { operationConfiguration });
            #endregion

            #region Mock getCustomerProductAssignmentsByCustomerId

            var mockgetCustomerProductAssignmentsByCustomerIdMs = MockMicroServiceManager.GetMockedMicroService<GetCustomerProductAssignmentsByCustomerIdRequest, GetCustomerProductAssignmentsByCustomerIdResponse>();
            var actualSourceCustomerProductAssignment = CreateDefaultObject.Create<CustomerProductAssignment>();
            var actualSourceCustomerProductAssignments = new List<CustomerProductAssignment> { actualSourceCustomerProductAssignment };
            var actualDestinationCustomerProductAssignment = CreateDefaultObject.Create<CustomerProductAssignment>();
            var actualDestinationCustomerProductAssignments = new List<CustomerProductAssignment> { actualDestinationCustomerProductAssignment };

            var sourceGetCustomerProductAssignmentsByCustomerIdResponse = new GetCustomerProductAssignmentsByCustomerIdResponse()
            {
                CustomerProductAssignments = actualSourceCustomerProductAssignments,
            };
            var destinationGetCustomerProductAssignmentsByCustomerIdResponse = new GetCustomerProductAssignmentsByCustomerIdResponse()
            {
                CustomerProductAssignments = actualDestinationCustomerProductAssignments,
            };
            mockgetCustomerProductAssignmentsByCustomerIdMs.Process(
                new GetCustomerProductAssignmentsByCustomerIdRequest { CustomerId = benefitRequestDto.SourceCustomerId },
                null).ReturnsForAnyArgs(sourceGetCustomerProductAssignmentsByCustomerIdResponse);
            mockgetCustomerProductAssignmentsByCustomerIdMs.Process(
                new GetCustomerProductAssignmentsByCustomerIdRequest { CustomerId = benefitRequestDto.DestinationCustomerId },
                null).ReturnsForAnyArgs(destinationGetCustomerProductAssignmentsByCustomerIdResponse);

            #endregion

            #region mockGetProductByProductId
            var mockGetProductByProductId = MockMicroServiceManager.GetMockedMicroService<GetProductByProductIdRequest, GetProductByProductIdResponse>();
            GetProductByProductIdRequest getProductByProductIdRequest = new GetProductByProductIdRequest()
            {
                ProductId = 1
            };
            var benefitproduct = CreateDefaultObject.Create<ProductOffering>();
            benefitproduct.Names.Texts = new List<LanguageSpecificText> { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            benefitproduct.Description.Texts = new List<LanguageSpecificText> { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            var benefitChargeOption = new ProductChargeOption
            {
                CreateDate = CreateDefaultObject.Create<DateTime>(),
                EndDate = CreateDefaultObject.Create<DateTime>(),
                Id = CreateDefaultObject.Create<int>(),
                IsDefaultOption = DefaultOptions.Y,
                ProductOffering = CreateDefaultObject.Create<ProductOffering>(),
                StartDate = CreateDefaultObject.Create<DateTime>(),
                Name = CreateDefaultObject.Create<MultiLingualDescription>(),
                Description = CreateDefaultObject.Create<MultiLingualDescription>()
            };
            var benefitCharge = CreateDefaultObject.Create<ChargeNonRecurring>();
            benefitCharge.Name = CreateDefaultObject.Create<MultiLingualDescription>();
            benefitCharge.Description = CreateDefaultObject.Create<MultiLingualDescription>();
            benefitChargeOption.Charges = new List<Charge> { benefitCharge };
            benefitproduct.ChargingOptions = new List<ProductChargeOption> { benefitChargeOption };
            benefitproduct.OfferedProduct = actualProduct;
            benefitproduct.Options = new List<ProductOfferingOption>();
            benefitproduct.OfferedProduct.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList.Add(CreateDefaultObject.Create<RmPromotionPlanDetailInfo>());
            var getProductByProductIdResponse = new GetProductByProductIdResponse { Product = benefitproduct.OfferedProduct };
            mockGetProductByProductId.Process(getProductByProductIdRequest, null).ReturnsForAnyArgs(getProductByProductIdResponse);
            #endregion

            #region MockCheckProductListDependencyRelationsForCustomerMs

            var mockCheckProductListDependencyRelationsForCustomerMS = MockMicroServiceManager.GetMockedMicroService<CheckProductListDependencyRelationsForCustomerRequest, CheckProductListDependencyRelationsForCustomerResponse>();
            var checkSourceProductListDependencyRelationsForCustomerRequest = new CheckProductListDependencyRelationsForCustomerRequest()
            {
                ProductsToPurchase = new List<ProductOffering>(),
                CustomerProducts = sourceGetCustomerProductAssignmentsByCustomerIdResponse.CustomerProductAssignments.ToList()
            };
            var checkSourceProductListDependencyRelationsForCustomerResponse = new CheckProductListDependencyRelationsForCustomerResponse
            {
                IsListCompatibleWithCustomerProducts = true,
                AreListRequirementsSatisfiedForCustomer = true
            };
            var checkDestinationProductListDependencyRelationsForCustomerRequest = new CheckProductListDependencyRelationsForCustomerRequest()
            {
                ProductsToPurchase = new List<ProductOffering>(),
                CustomerProducts = sourceGetCustomerProductAssignmentsByCustomerIdResponse.CustomerProductAssignments.ToList()
            };
            var checkDestinationProductListDependencyRelationsForCustomerResponse = new CheckProductListDependencyRelationsForCustomerResponse()
            {
                IsListCompatibleWithCustomerProducts = true,
                AreListRequirementsSatisfiedForCustomer = true
            };
            mockCheckProductListDependencyRelationsForCustomerMS.Process(checkSourceProductListDependencyRelationsForCustomerRequest, null).ReturnsForAnyArgs(checkSourceProductListDependencyRelationsForCustomerResponse);
            mockCheckProductListDependencyRelationsForCustomerMS.Process(checkDestinationProductListDependencyRelationsForCustomerRequest, null).ReturnsForAnyArgs(checkDestinationProductListDependencyRelationsForCustomerResponse);
            #endregion

            #region Mock GetActiveCustomerAccountAssociationByDateMs

            var actualGetActiveCustomerAccountAssociationByDateResponse =
                new GetActiveCustomerAccountAssociationByDateResponse { CustomerAccountAssociation = new CustomerAccountAssociation { Account = CreateDefaultObject.Create<Account>() } };
            var mockGetActiveCustomerAccountAssociationByDateMs = MockMicroServiceManager.GetMockedMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();
            mockGetActiveCustomerAccountAssociationByDateMs.Process(
                new GetActiveCustomerAccountAssociationByDateRequest(), null)
                .ReturnsForAnyArgs(actualGetActiveCustomerAccountAssociationByDateResponse);

            #endregion

            #region mock GetInvoicesByCustomerIdAndLegalInvoiceNumber

            var invoice = CreateDefaultObject.Create<Invoice>();
            invoice.StartDate = DateTime.MinValue;
            invoice.EndDate = DateTime.MaxValue;
            invoice.ChargingAccount =
                actualGetActiveCustomerAccountAssociationByDateResponse.CustomerAccountAssociation.Account;
            var actualGetInvoicesByCustomerIdAndLegalInvoiceNumberResponse =
                new GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse { Invoices = new List<Invoice> { invoice } };
            var mockGetInvoicesByCustomerIdAndLegalInvoiceNumberMs = MockMicroServiceManager.GetMockedMicroService<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest, GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse>();
            mockGetInvoicesByCustomerIdAndLegalInvoiceNumberMs.Process(
                new GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest(), null)
                .ReturnsForAnyArgs(actualGetInvoicesByCustomerIdAndLegalInvoiceNumberResponse);
            #endregion

            #region mock GetSucessfulOperationExecutionForCustomerMS

            var getSucessfulOperationExecutionForCustomerResponse =
                new GetSucessfulOperationExecutionForCustomerResponse { Operations = new List<BusinessOperationExecution> { CreateDefaultObject.Create<BusinessOperationExecution>() } };
            var mockGetSucessfulOperationExecutionForCustomerMs = MockMicroServiceManager.GetMockedMicroService<GetSucessfulOperationExecutionForCustomerRequest, GetSucessfulOperationExecutionForCustomerResponse>();
            mockGetSucessfulOperationExecutionForCustomerMs.Process(
                new GetSucessfulOperationExecutionForCustomerRequest(), null)
                .ReturnsForAnyArgs(getSucessfulOperationExecutionForCustomerResponse);
            #endregion

            #region mock GetCustomersActivePromotionInfoMs

            var mockGetCustomersActivePromotionInfoMs = MockMicroServiceManager.GetMockedMicroService<GetCustomersActivePromotionInfoRequest, GetCustomersActivePromotionInfoResponse>();
            var getSourceCustomersActivePromotionInfoResponse =
                new GetCustomersActivePromotionInfoResponse { Promotions = actualCustomerSourceInfo.Promotions.ToList() };

            mockGetCustomersActivePromotionInfoMs.Process(
                 Arg.Is<GetCustomersActivePromotionInfoRequest>(x => x.Customer.CustomerID == actualCustomerSourceInfo.CustomerID), null)
                .Returns(getSourceCustomersActivePromotionInfoResponse);

            var getDestinationCustomersActivePromotionInfoResponse =
                new GetCustomersActivePromotionInfoResponse { Promotions = actualCustomerDestinationInfo.Promotions.ToList() };
            mockGetCustomersActivePromotionInfoMs.Process(
                Arg.Is<GetCustomersActivePromotionInfoRequest>(x => x.Customer.CustomerID == actualCustomerDestinationInfo.CustomerID), null)
                .Returns(getDestinationCustomersActivePromotionInfoResponse);
            #endregion

            #region mock InternalBizopPurchaseProductForCustomerMs


            mockPurchaseProductBizop = Substitute.For<ICoreBusinessOperation<PurchaseProductForCustomerRequestInternal, PurchaseProductForCustomerResponseInternal>>();
            BusinessOperationManager.RebindCoreInterfaceToConstant(actualCustomerDestinationInfo.DealerID.Value, mockPurchaseProductBizop);
            var mockProduct = CreateDefaultObject.Create<CustomerProductAssignment>();
            mockProduct.PurchasedProduct.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            mockProduct.PurchasedProduct.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList = new RmPromotionPlanDetailInfo[] { CreateDefaultObject.Create<RmPromotionPlanDetailInfo>() };

            mockPurchaseProductBizop.Process(Arg.Any<PurchaseProductForCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new PurchaseProductForCustomerResponseInternal()
                    {
                        productPurchaseList = new List<CustomerProductAssignment> { mockProduct },
                        ResultType = ResultTypes.Ok
                    }
                );
            #endregion

            #region mock GetProductChargeOptionByProductChargeOptionId
            var mockGetProductChargeOptionByProductChargeOptionIdMs = MockMicroServiceManager.GetMockedMicroService<GetProductChargeOptionByProductChargeOptionIdRequest, GetProductChargeOptionByProductChargeOptionIdResponse>();
            var getProductChargeOptionByProductChargeOptionIdResponse =
                new GetProductChargeOptionByProductChargeOptionIdResponse { ProductChargeOption = CreateDefaultObject.Create<ProductChargeOption>() };
            mockGetProductChargeOptionByProductChargeOptionIdMs.Process(
                 new GetProductChargeOptionByProductChargeOptionIdRequest(), null)
                .ReturnsForAnyArgs(getProductChargeOptionByProductChargeOptionIdResponse);
            #endregion

            #region mock GetProductChargeOptionsByProductId
            var mockGetProductChargeOptionsByProductIdMs = MockMicroServiceManager.GetMockedMicroService<GetProductOfferingByProductOfferingIdRequest, GetProductOfferingByProductOfferingIdResponse>();

            var getProductChargeOptionsByProductIdResponse =
                new GetProductOfferingByProductOfferingIdResponse { ProductOffering = benefitproduct };
            mockGetProductChargeOptionsByProductIdMs.Process(
                 new GetProductOfferingByProductOfferingIdRequest(), null)
                .ReturnsForAnyArgs(getProductChargeOptionsByProductIdResponse);
            #endregion

            #region mock AddCrmCustomersBalanceTransationHistory
            var mockAddCrmCustomersBalanceTransationHistoryMs = MockMicroServiceManager.GetMockedMicroService<AddCrmCustomersBalanceTransationHistoryRequest, AddCrmCustomersBalanceTransationHistoryResponse>();
            var addCrmCustomersBalanceTransationHistoryResponse =
                new AddCrmCustomersBalanceTransationHistoryResponse { customersBalanceTransationHistory = CreateDefaultObject.Create<CrmCustomersBalanceTransationHistory>() };
            mockAddCrmCustomersBalanceTransationHistoryMs.Process(
                 new AddCrmCustomersBalanceTransationHistoryRequest(), null)
                .ReturnsForAnyArgs(addCrmCustomersBalanceTransationHistoryResponse);
            #endregion

            #region mock UpdateCustomersPromotion
            var mockAUpdateCustomersPromotionMs = MockMicroServiceManager.GetMockedMicroService<UpdateCustomersPromotionRequest, UpdateCustomersPromotionResponse>();
            var updateCustomersPromotionResponse =
                new UpdateCustomersPromotionResponse { CrmCustomersPromotionInfo = CreateDefaultObject.Create<CrmCustomersPromotionInfo>() };
            mockAUpdateCustomersPromotionMs.Process(
                 new UpdateCustomersPromotionRequest(), null)
                .ReturnsForAnyArgs(updateCustomersPromotionResponse);
            #endregion

            #region mock SubtractBalance
            var mockSubtractBalanceMs = MockMicroServiceManager.GetMockedMicroService<SubtractBalanceRequest, SubtractBalanceResponse>();
            var subtractBalanceResponse =
                new SubtractBalanceResponse { };
            mockSubtractBalanceMs.Process(
                 new SubtractBalanceRequest(), null)
                .ReturnsForAnyArgs(subtractBalanceResponse);
            #endregion

            #region mock dre query

            var mockCallDRE_queryBizop = Substitute.For<ICoreBusinessOperation<CallDREQuerySubscriberPromotionRequestInternal, CallDREQuerySubscriberPromotionResponseInternal>>();
            BusinessOperationManager.RebindCoreInterfaceToConstant(actualCustomerDestinationInfo.DealerID.Value, mockCallDRE_queryBizop);

            mockCallDRE_queryBizop.Process(Arg.Any<CallDREQuerySubscriberPromotionRequestInternal>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CallDREQuerySubscriberPromotionResponseInternal()
                    {
                        currentLimit = 10,
                        frozenLimit = 0,
                        errorCode = 0,
                    }
                );

            #endregion

            #region Activate IProductOfferingRepository
            var mockedProductOfferingRepo = MockRepositoryManager.GetMockedRepository<IProductOfferingRepository<ProductOffering>>(); 
            #endregion

            #region mock DRE API update

            var mockCallDRE_updateBizop = Substitute.For<ICoreBusinessOperation<CallDREUpdateSubscriberPromotionRequestInternal, CallDREUpdateSubscriberPromotionResponseInternal>>();
            BusinessOperationManager.RebindCoreInterfaceToConstant(actualCustomerDestinationInfo.DealerID.Value, mockCallDRE_updateBizop);

            mockCallDRE_updateBizop.Process(Arg.Any<CallDREUpdateSubscriberPromotionRequestInternal>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CallDREUpdateSubscriberPromotionResponseInternal()
                    {
                        ErrorCode = 0,
                    }
                );

            #endregion

        }

        [TestFixtureSetUp]
        public static void Initialize()
        {
            FakeSessionFactorySingleton.Init();
        }

        [Test()]
        public void BenefitTransferBizOp_CorrectRequestGiven_CustomerBalanceTransfered()
        {

            var benefitTransferRequestDTO = new BenefitTransferRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                SourceCustomerId = 1676017516,
                DestinationCustomerId = 1673020302,
                amount = 10,
                TransferType = 3003
            };

            MockAbstractSinglePhaseOrderProcessor(benefitTransferRequestDTO);
            StandardMSMocks(benefitTransferRequestDTO);

            var actualBenefitTransferResponseDTO = CallBizOp(benefitTransferRequestDTO);

            Assert.IsTrue(actualBenefitTransferResponseDTO.resultType == ResultTypes.Ok);
        }

    }
}
