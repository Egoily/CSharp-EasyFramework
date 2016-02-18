using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using com.etak.core.bizops.assurance.ApplyChargeAndSchedule;
using com.etak.core.bizops.fullfilment.ApplyCustomerPromotion;
using com.etak.core.bizops.fullfilment.AssignProductToCustomer;
using com.etak.core.bizops.fullfilment.CancelCustomerProduct;
using com.etak.core.bizops.fullfilment.CheckPurchaseProductForCustomer;
using com.etak.core.bizops.fullfilment.PurchaseProductForCustomer;
using com.etak.core.bizops.helper;
using com.etak.core.bizops.revenue.ChargeService;
using com.etak.core.bizops.revenue.ChargeService.Proxy;
using com.etak.core.customer.message.AddChargeToCustomer;
using com.etak.core.customer.message.CreateCustomerChargeSchedule;
using com.etak.core.customer.message.CreateInvoice;
using com.etak.core.customer.message.GetAccountById;
using com.etak.core.customer.message.GetActiveCustomerAccountAssociationByDate;
using com.etak.core.customer.message.GetCustomerInfoById;
using com.etak.core.customer.message.GetInvoiceById;
using com.etak.core.customer.message.GetInvoicesByCustomerIdAndLegalInvoiceNumber;
using com.etak.core.customer.message.UpdateCustomerChargeSchedule;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.dealer.messages.GetMVNOConfigActionInfosByIdAndName;
using com.etak.core.dealer.messages.GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItem;
using com.etak.core.GSMSubscription.messages.CancelCustomerProductAssignment;
using com.etak.core.GSMSubscription.messages.CreateCustomerProductAssignment;
using com.etak.core.GSMSubscription.messages.GetCustomerProductAssignmentsByCustomerId;
using com.etak.core.microservices.messages.GetTaxAuthority;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;
using com.etak.core.model.subscription.catalog;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.product.message.CalculateNextBillRunDateForBillCycle;
using com.etak.core.product.message.CreateProductInfo;
using com.etak.core.product.message.GetProductChargeOptionByProductChargeOptionId;
using com.etak.core.product.message.GetProductChargeOptionsByProductId;
using com.etak.core.product.message.GetRmPromotionPlanInfosByIds;
using com.etak.core.repository;
using com.etak.core.repository.crm.configuration;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.operation;
using com.etak.core.repository.crm.subscription.catalog;
using com.etak.core.service.messages.AddUnbilledBalance;
using com.etak.core.service.messages.CreateServicesInfo;
using com.etak.core.service.messages.CustomerHasCredit;
using com.etak.core.service.messages.GetPriorityBundleInfoFromBundleInfos;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.fullfilment.PurchaseProductForCustomer
{
    [TestFixture]
    public class PurchaseProductForCustomerBizOpTests : AbstractSinglePhaseOrderProcessorTest<PurchaseProductForCustomerBizOp, PurchaseProductForCustomerRequestDTO, PurchaseProductForCustomerResponseDTO
        , PurchaseProductForCustomerRequestInternal, PurchaseProductForCustomerResponseInternal, PurchaseProductForCustomerOrder>
    {
        #region Ini Var


        /// <summary>
        /// Counts the pass number in methods with multiple TestCases
        /// </summary>
        private static int PassCount = 0;

        private List<ProductCatalogDTO> productCatalogListOk = new List<ProductCatalogDTO>()
        {
            CreateDefaultObject.Create<ProductCatalogDTO>(),
            CreateDefaultObject.Create<ProductCatalogDTO>(),
            CreateDefaultObject.Create<ProductCatalogDTO>(),
            CreateDefaultObject.Create<ProductCatalogDTO>()
        };
        private List<ProductOffering> productListOk = new List<ProductOffering>();
        private int DealerId = 1;
        private DealerInfo actualDealerInfo;
        private ProductOfferingGroup productOfferingGroup = CreateDefaultObject.Create<ProductOfferingGroup>();
        
        

        #endregion

        #region Define Mock MicroServices

        private IMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse> _mockgetActiveCusotmerAccountMs;
        private IMicroService<CreateProductInfoRequest, CreateProductInfoResponse> _mockcreateProductInfotMs;
        private IMicroService<CreateCustomerChargeScheduleRequest, CreateCustomerChargeScheduleResponse> _mockcreateCustomerChargeScheduleResponseMs;
        private IMicroService<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest, GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse> _mockgetInvoiceByLegalInvoiceNumber;
        private IMicroService<GetMVNOConfigActionInfosByIdAndNameRequest, GetMVNOConfigActionInfosByIdAndNameResponse> _mockgetMvnoConfigActionInfosByIdAndNameMs;
        private IMicroService<CreateServicesInfoRequest, CreateServicesInfoResponse> _mockcreateServiceMs;
        private IMicroService<GetTaxAuthorityRequest, GetTaxAuthorityResponse> _mockgetTaxAuthorityMs;
        private IMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse> _mockcalculateNextBillrunDateMs;
        private IMicroService<GetPriorityBundleInfoFromBundleInfosRequest, GetPriorityBundleInfoFromBundleInfosResponse> _mockgetPriorityBundleMs;
        private IMicroService<GetRmPromotionPlanInfosByIdsRequest, GetRmPromotionPlanInfosByIdsResponse> _mockgetRmPromotionPlanInfoMs;
        private IMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse> _mockCheckAuthorizationMs;
        private IMicroService<UpdateCustomerChargeScheduleRequest, UpdateCustomerChargeScheduleResponse> _mockupdateCustomerChargeScheduleMs;
        private IMicroService<AddChargeToCustomerRequest, AddChargeToCustomerResponse> _mockupdateaddChargeMS;
        private IMicroService<CustomerHasCreditRequest, CustomerHasCreditResponse> _mockupdatehasCreditMS;
        private IMicroService<AddUnbilledBalanceRequest, AddUnbilledBalanceResponse> _mockupdateaddUnbilledBalanceMS;
        #endregion

        #region Define Mock BizOp

        private ICoreBusinessOperation<ChargeServiceRequestInternal, ChargeServiceResponseInternal> mockChargeServiceBizOp;
        private ICoreBusinessOperation<CheckPurchaseProductForCustomerRequestInternal, CheckPurchaseProductForCustomerResponseInternal> mockCheckPurchaseProductBizOp;
        private ICoreBusinessOperation<CancelCustomerProductRequestInternal, CancelCustomerProductResponseInternal> mockCancelCustomerBizOp;
        private ICoreBusinessOperation<ApplyChargeAndScheduleRequest, ApplyChargeAndScheduleResponse> mockApplyChargesBizOP;
        private ICoreBusinessOperation<ApplyCustomerPromotionRequestInternal, ApplyCustomerPromotionResponseInternal> mockApplyCustomerPromotionBizOP;
        private ICoreBusinessOperation<AssignProductOfferingToCustomerRequestInternal, AssignProductOfferingToCustomerResponseInternal> mockAssignProductOfferingToCustomerBizOp;

        #endregion

        #region Define Third party

        private IApplyRecurringChargeInterface mockApplyRecurringCharge;
        private PurchaseHelper mockPurchaseHelper;


        #endregion

        #region Define Mock Repo

        private ICustomerInfoRepository<CustomerInfo> mockCustomerRepo;
        private IProductOfferingRepository<ProductOffering> mockProductOfferingRepo;

        #endregion

        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();

            actualDealerInfo = CreateDefaultObject.Create<DealerInfo>();
            actualDealerInfo.DealerID = DealerId;



            #region Cases productList

            productCatalogListOk[0].Id = 1;
            productCatalogListOk[0].PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>() { new ProductPurchaseChargingOptionDTO() { Id = 100 } };
            productCatalogListOk[1].Id = 2;
            productCatalogListOk[1].PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>() { new ProductPurchaseChargingOptionDTO() { Id = 200 } };
            productCatalogListOk[2].Id = 3;
            productCatalogListOk[2].PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>() { new ProductPurchaseChargingOptionDTO() { Id = 300 } };
            productCatalogListOk[3].Id = 4;
            productCatalogListOk[3].PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>() { new ProductPurchaseChargingOptionDTO() { Id = 400 } };


            #endregion

            // Initialize pass number for methods with multiple cases
            PassCount = 0;

        }


        private void CommomInis()
        {
            #region Setup Data

            #endregion

            #region Create Mock MS


            _mockgetActiveCusotmerAccountMs = MockMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();
            _mockcreateProductInfotMs = MockMicroService<CreateProductInfoRequest, CreateProductInfoResponse>();
            _mockcreateCustomerChargeScheduleResponseMs = MockMicroService<CreateCustomerChargeScheduleRequest, CreateCustomerChargeScheduleResponse>();
            _mockgetInvoiceByLegalInvoiceNumber = MockMicroService<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest, GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse>();
            _mockgetMvnoConfigActionInfosByIdAndNameMs = MockMicroService<GetMVNOConfigActionInfosByIdAndNameRequest, GetMVNOConfigActionInfosByIdAndNameResponse>();
            _mockcreateServiceMs = MockMicroService<CreateServicesInfoRequest, CreateServicesInfoResponse>();
            _mockgetTaxAuthorityMs = MockMicroService<GetTaxAuthorityRequest, GetTaxAuthorityResponse>();
            _mockcalculateNextBillrunDateMs = MockMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            _mockgetPriorityBundleMs = MockMicroService<GetPriorityBundleInfoFromBundleInfosRequest, GetPriorityBundleInfoFromBundleInfosResponse>();
            _mockgetRmPromotionPlanInfoMs = MockMicroService<GetRmPromotionPlanInfosByIdsRequest, GetRmPromotionPlanInfosByIdsResponse>();
            _mockupdateCustomerChargeScheduleMs = MockMicroService<UpdateCustomerChargeScheduleRequest, UpdateCustomerChargeScheduleResponse>();
            _mockupdateaddChargeMS = MockMicroService<AddChargeToCustomerRequest, AddChargeToCustomerResponse>();
            _mockupdatehasCreditMS = MockMicroService<CustomerHasCreditRequest, CustomerHasCreditResponse>();
            _mockupdateaddUnbilledBalanceMS = MockMicroService<AddUnbilledBalanceRequest, AddUnbilledBalanceResponse>();

            _mockCheckAuthorizationMs = MockMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
            

            #endregion

            #region Create Mock BizOp


            mockChargeServiceBizOp = Substitute.For<ICoreBusinessOperation<ChargeServiceRequestInternal, ChargeServiceResponseInternal>>();
            mockCheckPurchaseProductBizOp = Substitute.For<ICoreBusinessOperation<CheckPurchaseProductForCustomerRequestInternal, CheckPurchaseProductForCustomerResponseInternal>>();
            mockCancelCustomerBizOp = Substitute.For<ICoreBusinessOperation<CancelCustomerProductRequestInternal, CancelCustomerProductResponseInternal>>();
            mockApplyChargesBizOP = Substitute.For<ICoreBusinessOperation<ApplyChargeAndScheduleRequest, ApplyChargeAndScheduleResponse>>();
            mockApplyCustomerPromotionBizOP = Substitute.For<ICoreBusinessOperation<ApplyCustomerPromotionRequestInternal, ApplyCustomerPromotionResponseInternal>>();
            mockAssignProductOfferingToCustomerBizOp = Substitute.For<ICoreBusinessOperation<AssignProductOfferingToCustomerRequestInternal, AssignProductOfferingToCustomerResponseInternal>>();


            #endregion

            #region Bind to BizOpKernel


            BusinessOperationManager.RebindCoreInterfaceToConstant<ChargeServiceRequestInternal, ChargeServiceResponseInternal>(DealerId, mockChargeServiceBizOp);
            BusinessOperationManager.RebindCoreInterfaceToConstant<CheckPurchaseProductForCustomerRequestInternal, CheckPurchaseProductForCustomerResponseInternal>(DealerId, mockCheckPurchaseProductBizOp);
            BusinessOperationManager.RebindCoreInterfaceToConstant<CancelCustomerProductRequestInternal, CancelCustomerProductResponseInternal>(DealerId, mockCancelCustomerBizOp);
            BusinessOperationManager.RebindCoreInterfaceToConstant<ApplyChargeAndScheduleRequest, ApplyChargeAndScheduleResponse>(DealerId, mockApplyChargesBizOP);
            BusinessOperationManager.RebindCoreInterfaceToConstant<ApplyCustomerPromotionRequestInternal, ApplyCustomerPromotionResponseInternal>(DealerId, mockApplyCustomerPromotionBizOP);
            BusinessOperationManager.RebindCoreInterfaceToConstant<AssignProductOfferingToCustomerRequestInternal, AssignProductOfferingToCustomerResponseInternal>(DealerId, mockAssignProductOfferingToCustomerBizOp);



            #endregion

            #region Create Mock Third party

            mockApplyRecurringCharge = Substitute.For<IApplyRecurringChargeInterface>();
            mockPurchaseHelper = Substitute.For<PurchaseHelper>();


            #endregion

            #region create Mock OperationConfiguration

            var mockedRepoConfig = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var actualOperationConfigurations = new List<OperationConfiguration>();
            var actualOperationConfiguration = CreateDefaultObject.Create<OperationConfiguration>();
            var actualBizOpConfiguration = new PurchaseProductForCustomerConfiguration();
            actualBizOpConfiguration.FirstDayOfWeek = DayOfWeek.Monday;
            actualBizOpConfiguration.CategoryMVNOConfigForPromotionLimit = "";
            actualOperationConfiguration.JSonConfig = JsonConvert.SerializeObject(actualBizOpConfiguration);
            actualOperationConfiguration.EndDate = new DateTime(2016, 5, 5);
            actualOperationConfiguration.StarTime = DateTime.Now;
            actualOperationConfigurations.Add(actualOperationConfiguration);
            mockedRepoConfig.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(actualOperationConfigurations);



            #endregion

            #region Create Mock Repository

            CustomerInfo mockedCustomer = CreateDefaultObject.Create<CustomerInfo>();
            mockedCustomer.RevenueProductsInfo = new List<CustomerProductAssignment>()
            {
                new CustomerProductAssignment()
                {
                    PurchasedProduct = CreateDefaultObject.Create<Product>(),
                    ProductOffering = CreateDefaultObject.Create<ProductOffering>()
                }
            };


            // All base Bundle the same
            foreach (ServicesInfo servicesInfo in mockedCustomer.ServicesInfo)
            {
                servicesInfo.CREDITLIMITBASEBUNDLEID = 1;
            }

            mockCustomerRepo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            mockCustomerRepo.GetById(Arg.Any<int>()).Returns(mockedCustomer);


            mockProductOfferingRepo = MockRepositoryManager.GetMockedRepository<IProductOfferingRepository<ProductOffering>>();
            mockProductOfferingRepo.GetByGroupId(Arg.Any<int>())
                .Returns(productListOk);



            #endregion

        }



        #region Static testdata

        private static object[] testData =
        {
            // One Product
            new List<ProductCatalogDTO>()
            {
                new ProductCatalogDTO()
                {
                    Id = 1,PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>()
                    {
                        new ProductPurchaseChargingOptionDTO() { Id = 100 }
                    }
                }
            },
            // 2 Products
            new List<ProductCatalogDTO>()
            {
                new ProductCatalogDTO()
                {
                    Id = 1,PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>()
                    {
                        new ProductPurchaseChargingOptionDTO() { Id = 100 }
                    }
                },
                new ProductCatalogDTO()
                {
                    Id = 2,PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>()
                    {
                        new ProductPurchaseChargingOptionDTO() { Id = 200 }
                    }
                }

            },
            // 4 Products
            new List<ProductCatalogDTO>()
            {
                new ProductCatalogDTO()
                {
                    Id = 1,PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>()
                    {
                        new ProductPurchaseChargingOptionDTO() { Id = 100 }
                    }
                },
                new ProductCatalogDTO()
                {
                    Id = 2,PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>()
                    {
                        new ProductPurchaseChargingOptionDTO() { Id = 200 }
                    }
                },
                new ProductCatalogDTO()
                {
                    Id = 3,PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>()
                    {
                        new ProductPurchaseChargingOptionDTO() { Id = 300 }
                    }
                },
                new ProductCatalogDTO()
                {
                    Id = 4,PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>()
                    {
                        new ProductPurchaseChargingOptionDTO() { Id = 400 }
                    }
                }
            }
        };

        private static object[] testData1product =
        {
            // One Product
            new List<ProductCatalogDTO>()
            {
                new ProductCatalogDTO()
                {
                    Id = 1,
                    PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>()
                    {
                        new ProductPurchaseChargingOptionDTO() {Id = 100}
                    }
                }
            },
        };

        #endregion


        [SetUp]
        public void SetupForEveryTest()
        {

        }

        [TearDown]
        public void TearDownForEveryTest()
        {
            // inc pass number for methods with multiple cases
            PassCount++;

        }



        #region Type PurchaseProduct

        /// <summary>
        /// Positive Case All products checked OK
        /// </summary>
        [Test, TestCaseSource("testData")]
        public void PurchaseProductForCustomerBizOpTests_CheckProductsAllOk_Ok(List<ProductCatalogDTO> listProductparam)
        {


            #region RequestDTO

            PurchaseProductForCustomerRequestDTO requestDto = new PurchaseProductForCustomerRequestDTO
            {
                CustomerId = 1,
                forceCreditLimit = null,
                products = listProductparam,
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                channel = "TestDTOChannel"
            };

            #endregion

            #region Commons

            MockAbstractSinglePhaseOrderProcessor(requestDto);


            CommomInis();

            #endregion

            #region Setup mock Override commons

            BusinessOperationExecution _currentOperationExecution = new BusinessOperationExecution();
            var mockedRepoBusinessOperationExecution = MockRepositoryManager.GetMockedRepository<IBusinessOperationExecutionRepository<BusinessOperationExecution>>();
            mockedRepoBusinessOperationExecution.Create(Arg.Do<BusinessOperationExecution>(x => _currentOperationExecution = x));



            #endregion

            #region Find out BizOP OperationID

            //var bizopID = new PurchaseProductForCustomerBizOp();
            //var a = bizopID.Id;

            //int operationId = bizopID.OperationCode.GetHashCode();

            #endregion

            #region Assign values

            // setup
            var actualBundleInfo = CreateDefaultObject.Create<BundleInfo>();
            actualBundleInfo.EndDate = DateTime.Now.AddDays(1);


            var actualCharge = CreateDefaultObject.Create<ChargeNonRecurring>(1);
            var actualAccount = CreateDefaultObject.Create<Account>(1);
            var actualCusotmerInfo = CreateDefaultObject.Create<CustomerInfo>(1);
            actualCusotmerInfo.StatusID = 1;

            foreach (ServicesInfo item in actualCusotmerInfo.ServicesInfo)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
                item.CreditLimit = 9999;
                item.CREDITLIMITBASEBUNDLEID = 1;
            }

            actualCharge.Prices[0].EndDate = DateTime.Now.AddDays(1);

            var actualProdutOffering = CreateDefaultObject.Create<ProductOffering>();

            // TODO: Bug in conversor, crashes if no Names.Texts or Descriptions.Texts
            actualProdutOffering.Names.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            actualProdutOffering.Description.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            actualProdutOffering.ChargingOptions = new List<ProductChargeOption>();


            productListOk.Add(actualProdutOffering);
            var actualProductCataloDTO = CreateDefaultObject.Create<ProductCatalogDTO>();
            actualProductCataloDTO.PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>();
            // productCatalogListOk.Add(actualProductCataloDTO);

            var actualProdutInfo = CreateDefaultObject.Create<ProductInfo>();
            var actualcustomeraccountAssoc = CreateDefaultObject.Create<CustomerAccountAssociation>();
            actualcustomeraccountAssoc.Account = actualAccount;

            var actualCustomerProducAssig = CreateDefaultObject.Create<CustomerProductAssignment>();
            var actualServicesInfos = new List<ServicesInfo>
            {
                CreateDefaultObject.Create<ServicesInfo>(),
                CreateDefaultObject.Create<ServicesInfo>()
            };

            foreach (ServicesInfo item in actualServicesInfos)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
            }

            // mark a master bundle
            actualServicesInfos[0].CREDITLIMITBASEBUNDLEID = (int)actualServicesInfos[0].BundleDefinition.BundleID;
            actualServicesInfos[0].CreditLimit = 9999;
            // actualCusotmerInfo.ServicesInfo = actualServicesInfos;


            var actualInvoice = CreateDefaultObject.Create<Invoice>();
            var actualInvoice2 = CreateDefaultObject.Create<Invoice>();
            var actualInvoices = new List<Invoice> { actualInvoice, actualInvoice2 };
            var actualmvnoConfig = new List<MVNOConfigActionInfo>
            {
                CreateDefaultObject.Create<MVNOConfigActionInfo>(),
                CreateDefaultObject.Create<MVNOConfigActionInfo>()
            };

            // Need to use old new approach for this entity
            var actualProductChargeOption = new ProductChargeOption();
            var actualProductChargeOption2 = new ProductChargeOption();
            actualProductChargeOption.Charges = new List<Charge> { actualCharge };
            actualProductChargeOption.ProductOffering = actualProdutOffering;
            var actualProductChargeOptions = new List<ProductChargeOption> { actualProductChargeOption, actualProductChargeOption2 };



            var actualTaxDefinition = CreateDefaultObject.Create<TaxDefinition>();
            foreach (TaxRates item in actualTaxDefinition.Rates)
            {
                item.EndDate = DateTime.Now.AddDays(1);
            }
            var actualTaxDefinitions = new List<TaxDefinition>() { actualTaxDefinition };

            var actualNextBillrun = new DateTime(2015, 6, 1);

            var actualCustomerCharges = new List<CustomerCharge>()
            {
                CreateDefaultObject.Create<CustomerCharge>(),
                CreateDefaultObject.Create<CustomerCharge>()
            };

            var actualCustomerChargeeSchedule = new List<CustomerChargeSchedule>()
            {
                CreateDefaultObject.Create<CustomerChargeSchedule>(),
                CreateDefaultObject.Create<CustomerChargeSchedule>()
            };

            var actualcustomerChargeInfo1 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo1.customerChargeId = 1;
            var actualcustomerChargeInfo2 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo2.customerChargeId = 2;

            var actualpackage = CreateDefaultObject.Create<PackageInfo>();
            var actualrmPromotoinGroup = CreateDefaultObject.Create<RmPromotionGroupInfo>();
            var actualPromotoinPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            var listTupleFlattenedProducts = new List<Tuple<ProductOffering, ProductChargeOption>>();
            foreach (ProductCatalogDTO item in listProductparam)
            {
                ProductChargeOption pchargeOption = item.PurchaseOptions[0].ToCore();
                pchargeOption.Charges = new List<Charge>()
                {
                    CreateDefaultObject.Create<ChargeRecurring>(),
                    CreateDefaultObject.Create<ChargeNonRecurring>()
                };
                ProductOffering coreProduct = item.ToCore();
                coreProduct.OfferedProduct = CreateDefaultObject.Create<Product>();
                coreProduct.OfferedProduct.AssociatedBundle = actualBundleInfo;
                coreProduct.OfferedProduct.AssociatedPackage = actualpackage;
                coreProduct.OfferedProduct.AssociatedPrmotionGroup = actualrmPromotoinGroup;
                coreProduct.OfferedProduct.AssociatedPrmotionPlan = actualPromotoinPlan;
                coreProduct.OfferedProduct.VMO = actualDealerInfo;
                coreProduct.Options = new List<ProductOfferingOption>();
                listTupleFlattenedProducts.Add(Tuple.Create(coreProduct, pchargeOption));
            }

            var actualRmPromotionPlanInfos = new List<RmPromotionPlanInfo>()
            {
                CreateDefaultObject.Create<RmPromotionPlanInfo>(),
                CreateDefaultObject.Create<RmPromotionPlanInfo>()
            };

            var actualCustomerPromotionsInfos = new List<CrmCustomersPromotionInfo>()
            {
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>(),
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>()
            };


            #endregion

            #region Setup Mocks MS
            var checkAuthRespOk = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = true,
            };
            var checkAuthRespNok = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = false,
            };
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 1), null).Returns(checkAuthRespOk);
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 2), null).Returns(checkAuthRespNok);

            _mockcreateCustomerChargeScheduleResponseMs.Process(Arg.Any<CreateCustomerChargeScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateCustomerChargeScheduleResponse()
                    {
                        CustomerChargeSchedule = actualCustomerChargeeSchedule[0]
                    }
                );



            _mockgetRmPromotionPlanInfoMs.Process(Arg.Any<GetRmPromotionPlanInfosByIdsRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetRmPromotionPlanInfosByIdsResponse()
                    {
                        RmPromotionPlanInfos = actualRmPromotionPlanInfos

                    }
                );

            _mockgetPriorityBundleMs.Process(Arg.Any<GetPriorityBundleInfoFromBundleInfosRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetPriorityBundleInfoFromBundleInfosResponse()
                    {
                        PriorityBundle = actualBundleInfo,
                        ResultType = ResultTypes.Ok
                    }
                );




            _mockcalculateNextBillrunDateMs.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CalculateNextBillRunDateForBillCycleResponse
                    {
                        NextBillRun = actualNextBillrun
                    }
                );



            _mockgetTaxAuthorityMs.Process(Arg.Any<GetTaxAuthorityRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetTaxAuthorityResponse
                    {
                        TaxDefinitions = actualTaxDefinitions
                    }
                );



            _mockcreateServiceMs.Process(Arg.Any<CreateServicesInfoRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateServicesInfoResponse
                    {
                        ServicesInfos = actualServicesInfos[0]
                    }
                );





            _mockgetMvnoConfigActionInfosByIdAndNameMs.Process(Arg.Any<GetMVNOConfigActionInfosByIdAndNameRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetMVNOConfigActionInfosByIdAndNameResponse
                    {
                        MvnoConfigActionInfos = actualmvnoConfig
                    }
                );



            _mockgetInvoiceByLegalInvoiceNumber.Process(Arg.Any<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                     new GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse
                     {
                         Invoices = actualInvoices
                     }
                );




            _mockcreateProductInfotMs.Process(Arg.Any<CreateProductInfoRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateProductInfoResponse
                    {
                        ProductInfo = actualProdutInfo
                    }
                );



            _mockgetActiveCusotmerAccountMs.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetActiveCustomerAccountAssociationByDateResponse
                    {
                        CustomerAccountAssociation = actualcustomeraccountAssoc
                    }
                );

            var actualExistingCustomerProductAssignment = CreateDefaultObject.Create<CustomerProductAssignment>();
            actualExistingCustomerProductAssignment.EndDate = null;
            actualExistingCustomerProductAssignment.PurchasedProduct.Id = actualCustomerProducAssig.PurchasedProduct.Id;


            #endregion

            #region Setup Mock BizOp

            mockAssignProductOfferingToCustomerBizOp.Process(Arg.Any<AssignProductOfferingToCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    x => new AssignProductOfferingToCustomerResponseInternal()
                    {
                        ResultType = ResultTypes.Ok,
                        productPurchased = new CustomerProductAssignment()
                        {
                            // Return values passed as parameters
                            ProductOffering = ((AssignProductOfferingToCustomerRequestInternal)x[0]).ProductOffering.Item1,
                            ProductChargePurchased = ((AssignProductOfferingToCustomerRequestInternal)x[0]).ProductOffering.Item2
                        }

                    }
                );


            mockApplyCustomerPromotionBizOP.Process(Arg.Any<ApplyCustomerPromotionRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ApplyCustomerPromotionResponseInternal()
                    {
                        ResultType = ResultTypes.Ok,
                        CrmCustomersPromotionInfos = actualCustomerPromotionsInfos,
                        Customer = actualCusotmerInfo
                    }
                );



            mockApplyChargesBizOP.Process(Arg.Any<ApplyChargeAndScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ApplyChargeAndScheduleResponse()
                    {
                        ResultType = ResultTypes.Ok,
                        ChargeAdde = actualCustomerCharges[0],
                        ScheduleAdded = actualCustomerChargeeSchedule[0]
                    }
                );


            mockCancelCustomerBizOp.Process(Arg.Any<CancelCustomerProductRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CancelCustomerProductResponseInternal()
                    {
                        ResultType = ResultTypes.Ok
                    }
                );

            mockChargeServiceBizOp.Process(Arg.Any<ChargeServiceRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ChargeServiceResponseInternal()
                    {
                        CustomerCharges = actualCustomerCharges,
                        ResultType = ResultTypes.Ok
                    }
                );


            mockCheckPurchaseProductBizOp.Process(Arg.Any<CheckPurchaseProductForCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CheckPurchaseProductForCustomerResponseInternal()
                    {
                        IsCreditEnough = true,
                        IsLimitReached = false,
                        DeprovisionList = new List<ProductOfferingSpecificationOption>(),
                        ResultType = ResultTypes.Ok
                    }
                );

            #endregion

            #region Setup Mock Third party

            mockApplyRecurringCharge.firstPeriodChargingApply(Arg.Any<chargingRequestParameters>())
                .Returns
                (
                    new chargingApplyResponse()
                    {
                        errorMsg = "",
                        flag = 0,
                        customerChargedResultList = new[] { actualcustomerChargeInfo1, actualcustomerChargeInfo2 }
                    }

                );


            mockPurchaseHelper.GetProductsAndChargesOptions(Arg.Any<List<ProductCatalogDTO>>())
                .Returns
                (
                    listTupleFlattenedProducts
                );


            #endregion

            #region Customized CallBizOP



            var response = new PurchaseProductForCustomerResponseDTO();
            using (var conn = RepositoryManager.GetNewConnection())
            {
                PurchaseProductForCustomerBizOp bizop = new PurchaseProductForCustomerBizOp(mockApplyRecurringCharge, mockPurchaseHelper);




                //PurchaseProductForCustomerResponseInternal responseInternal =bizop.ProcessRequest(null, new PurchaseProductForCustomerRequestInternal()
                //{
                //    AccountDefinition = actualAccount,
                //    Customer = actualCusotmerInfo,
                //    DatetimePurchase = DateTime.Now,
                //    Invoice = actualInvoice,
                //    ForceCreditLimit = null,
                //    ProductsList = productListOk,
                //    TypeOfPurchaseProductOperation = PurchaseProductForCustomerBizOp.TypeOfPurchaseProduct.PurchaseProduct,
                //    MVNO = actualDealerInfo,
                //    User = CreateDefaultObject.Create<LoginInfo>()
                //});



                response = bizop.ProcessFromCustomerModel(new NullValidator<PurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<PurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<PurchaseProductForCustomerResponseDTO>(), requestDto, FakeInvoker);

            }
            RepositoryManager.CloseConnection();


            // PurchaseProductForCustomerResponseDTO response = new PurchaseProductForCustomerResponseDTO();
            // response = CallBizOp(requestDto);


            #endregion

            #region Asserts

            Assert.AreEqual(response.productPurchase.Count, listProductparam.Count);
            for (int x = 0; x < listProductparam.Count; x++)
            {
                Assert.AreEqual(response.productPurchase[x].PurchasedProductOfferingId, listProductparam[x].Id);
                Assert.AreEqual(response.productPurchase[x].ProductChargePurchasedId, listProductparam[x].PurchaseOptions[0].Id);
            }
            Assert.AreEqual(response.resultType, ResultTypes.Ok);

            // Assert all Operationexecutions have mandatory fields
            Assert.NotNull(_currentOperationExecution.Channel);
            Assert.NotNull(_currentOperationExecution.MVNO);
            Assert.NotNull(_currentOperationExecution.User);
            Assert.NotNull(_currentOperationExecution.Subscription);


            #endregion

        }

        /// <summary>
        /// Negative Case Customer is null
        /// </summary>
        [Test, TestCaseSource("testData")]
        public void PurchaseProductForCustomerBizOpTests_CustomerIsNull_ShouldThrowDataValidationErrorException(List<ProductCatalogDTO> listProductparam)
        {


            #region RequestDTO

            PurchaseProductForCustomerRequestDTO requestDto = new PurchaseProductForCustomerRequestDTO
            {
                CustomerId = 1,
                forceCreditLimit = null,
                products = listProductparam,
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                channel = "TestDTOChannel"
            };

            #endregion

            #region Commons

            MockAbstractSinglePhaseOrderProcessor(requestDto);

            CommomInis();

            #region Remock Repository

            var repoCustomerInfoMock = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            CustomerInfo customerInfo = null;
            repoCustomerInfoMock.GetById(Arg.Any<int>()).Returns(customerInfo);

            #endregion


            #endregion

            #region Setup mock Override commons

            BusinessOperationExecution _currentOperationExecution = new BusinessOperationExecution();
            var mockedRepoBusinessOperationExecution = MockRepositoryManager.GetMockedRepository<IBusinessOperationExecutionRepository<BusinessOperationExecution>>();
            mockedRepoBusinessOperationExecution.Create(Arg.Do<BusinessOperationExecution>(x => _currentOperationExecution = x));



            #endregion


            var response = new PurchaseProductForCustomerResponseDTO();
            using (var conn = RepositoryManager.GetNewConnection())
            {
                PurchaseProductForCustomerBizOp bizop = new PurchaseProductForCustomerBizOp(mockApplyRecurringCharge, mockPurchaseHelper);
                response = bizop.ProcessFromCustomerModel(new NullValidator<PurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<PurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<PurchaseProductForCustomerResponseDTO>(), requestDto, FakeInvoker);

            }
            RepositoryManager.CloseConnection();


            // PurchaseProductForCustomerResponseDTO response = new PurchaseProductForCustomerResponseDTO();
            // response = CallBizOp(requestDto);




            #region Asserts

            Assert.AreEqual(response.resultType, ResultTypes.DataValidationError);

            // For now only MVNO is assigned in EmergencyTrace
            // Assert all Operationexecutions have mandatory fields
            //Assert.NotNull(_currentOperationExecution.Channel);
            Assert.NotNull(_currentOperationExecution.MVNO);
            //Assert.NotNull(_currentOperationExecution.User);
            //Assert.NotNull(_currentOperationExecution.Subscription);


            #endregion

        }

        /// <summary>
        /// returns Credit in checkproducts Error
        /// </summary>
        [Test, TestCaseSource("testData")]
        public void PurchaseProductForCustomerBizOpTests_CheckProductsFailCredit_NOk(List<ProductCatalogDTO> listProductparam)
        {


            #region RequestDTO

            PurchaseProductForCustomerRequestDTO requestDto = new PurchaseProductForCustomerRequestDTO
            {
                CustomerId = 1,
                forceCreditLimit = null,
                products = listProductparam,
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                channel = "TestDTOChannel"
            };

            #endregion

            #region Commons

            MockAbstractSinglePhaseOrderProcessor(requestDto);


            CommomInis();

            #endregion

            #region Setup mock Override commons

            BusinessOperationExecution _currentOperationExecution = new BusinessOperationExecution();
            var mockedRepoBusinessOperationExecution = MockRepositoryManager.GetMockedRepository<IBusinessOperationExecutionRepository<BusinessOperationExecution>>();
            mockedRepoBusinessOperationExecution.Create(Arg.Do<BusinessOperationExecution>(x => _currentOperationExecution = x));



            #endregion

            #region Assign values

            // setup
            var actualBundleInfo = CreateDefaultObject.Create<BundleInfo>();
            actualBundleInfo.EndDate = DateTime.Now.AddDays(1);


            var actualCharge = CreateDefaultObject.Create<ChargeNonRecurring>(1);
            var actualAccount = CreateDefaultObject.Create<Account>(1);
            var actualCusotmerInfo = CreateDefaultObject.Create<CustomerInfo>(1);
            actualCusotmerInfo.StatusID = 1;

            foreach (ServicesInfo item in actualCusotmerInfo.ServicesInfo)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
                item.CreditLimit = 9999;
            }

            actualCharge.Prices[0].EndDate = DateTime.Now.AddDays(1);

            var actualProdut = CreateDefaultObject.Create<ProductOffering>();
            productListOk.Add(actualProdut);
            var actualProductCataloDTO = CreateDefaultObject.Create<ProductCatalogDTO>();
            actualProductCataloDTO.PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>();
            // productCatalogListOk.Add(actualProductCataloDTO);

            var actualProdutInfo = CreateDefaultObject.Create<ProductInfo>();
            var actualcustomeraccountAssoc = CreateDefaultObject.Create<CustomerAccountAssociation>();
            actualcustomeraccountAssoc.Account = actualAccount;

            var actualCustomerProducAssig = CreateDefaultObject.Create<CustomerProductAssignment>();
            var actualServicesInfos = new List<ServicesInfo>
            {
                CreateDefaultObject.Create<ServicesInfo>(),
                CreateDefaultObject.Create<ServicesInfo>()
            };

            foreach (ServicesInfo item in actualServicesInfos)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
            }

            // mark a master bundle
            actualServicesInfos[0].CREDITLIMITBASEBUNDLEID = (int)actualServicesInfos[0].BundleDefinition.BundleID;
            actualServicesInfos[0].CreditLimit = 9999;
            // actualCusotmerInfo.ServicesInfo = actualServicesInfos;


            var actualInvoice = CreateDefaultObject.Create<Invoice>();
            var actualInvoice2 = CreateDefaultObject.Create<Invoice>();
            var actualInvoices = new List<Invoice> { actualInvoice, actualInvoice2 };
            var actualmvnoConfig = new List<MVNOConfigActionInfo>
            {
                CreateDefaultObject.Create<MVNOConfigActionInfo>(),
                CreateDefaultObject.Create<MVNOConfigActionInfo>()
            };

            // Need to use old new approach for this entity
            var actualProductChargeOption = new ProductChargeOption();
            var actualProductChargeOption2 = new ProductChargeOption();
            actualProductChargeOption.Charges = new List<Charge> { actualCharge };
            actualProductChargeOption.ProductOffering = actualProdut;
            var actualProductChargeOptions = new List<ProductChargeOption> { actualProductChargeOption, actualProductChargeOption2 };


            var actualTaxDefinition = CreateDefaultObject.Create<TaxDefinition>();
            foreach (TaxRates item in actualTaxDefinition.Rates)
            {
                item.EndDate = DateTime.Now.AddDays(1);
            }
            var actualTaxDefinitions = new List<TaxDefinition>() { actualTaxDefinition };

            var actualNextBillrun = new DateTime(2015, 6, 1);

            var actualCustomerCharges = new List<CustomerCharge>()
            {
                CreateDefaultObject.Create<CustomerCharge>(),
                CreateDefaultObject.Create<CustomerCharge>()
            };

            var actualCustomerChargeeSchedule = new List<CustomerChargeSchedule>()
            {
                CreateDefaultObject.Create<CustomerChargeSchedule>(),
                CreateDefaultObject.Create<CustomerChargeSchedule>()
            };

            var actualcustomerChargeInfo1 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo1.customerChargeId = 1;
            var actualcustomerChargeInfo2 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo2.customerChargeId = 2;

            var actualpackage = CreateDefaultObject.Create<PackageInfo>();
            var actualrmPromotoinGroup = CreateDefaultObject.Create<RmPromotionGroupInfo>();
            var actualPromotoinPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            var listTupleFlattenedProducts = new List<Tuple<ProductOffering, ProductChargeOption>>();
            foreach (ProductCatalogDTO item in listProductparam)
            {
                ProductChargeOption pchargeOption = item.PurchaseOptions[0].ToCore();
                pchargeOption.Charges = new List<Charge>()
                {
                    CreateDefaultObject.Create<ChargeRecurring>(),
                    CreateDefaultObject.Create<ChargeNonRecurring>()
                };
                ProductOffering coreProduct = item.ToCore();
                coreProduct.OfferedProduct = CreateDefaultObject.Create<Product>();
                coreProduct.OfferedProduct.AssociatedBundle = actualBundleInfo;
                coreProduct.OfferedProduct.AssociatedPackage = actualpackage;
                coreProduct.OfferedProduct.AssociatedPrmotionGroup = actualrmPromotoinGroup;
                coreProduct.OfferedProduct.AssociatedPrmotionPlan = actualPromotoinPlan;
                coreProduct.OfferedProduct.VMO = actualDealerInfo;
                coreProduct.Options = new List<ProductOfferingOption>();
                listTupleFlattenedProducts.Add(Tuple.Create(coreProduct, pchargeOption));
            }

            var actualRmPromotionPlanInfos = new List<RmPromotionPlanInfo>()
            {
                CreateDefaultObject.Create<RmPromotionPlanInfo>(),
                CreateDefaultObject.Create<RmPromotionPlanInfo>()
            };

            var actualCustomerPromotionsInfos = new List<CrmCustomersPromotionInfo>()
            {
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>(),
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>()
            };


            #endregion

            #region Setup Mocks MS
            var checkAuthRespOk = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = true,
            };
            var checkAuthRespNok = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = false,
            };
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 1), null).Returns(checkAuthRespOk);
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 2), null).Returns(checkAuthRespNok);

            _mockcreateCustomerChargeScheduleResponseMs.Process(Arg.Any<CreateCustomerChargeScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateCustomerChargeScheduleResponse()
                    {
                        CustomerChargeSchedule = actualCustomerChargeeSchedule[0]
                    }
                );


            _mockgetRmPromotionPlanInfoMs.Process(Arg.Any<GetRmPromotionPlanInfosByIdsRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetRmPromotionPlanInfosByIdsResponse()
                    {
                        RmPromotionPlanInfos = actualRmPromotionPlanInfos

                    }
                );


            _mockgetPriorityBundleMs.Process(Arg.Any<GetPriorityBundleInfoFromBundleInfosRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetPriorityBundleInfoFromBundleInfosResponse()
                    {
                        PriorityBundle = actualBundleInfo,
                        ResultType = ResultTypes.Ok
                    }
                );



            _mockcalculateNextBillrunDateMs.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CalculateNextBillRunDateForBillCycleResponse
                    {
                        NextBillRun = actualNextBillrun
                    }
                );




            _mockgetTaxAuthorityMs.Process(Arg.Any<GetTaxAuthorityRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetTaxAuthorityResponse
                    {
                        TaxDefinitions = actualTaxDefinitions
                    }
                );



            _mockcreateServiceMs.Process(Arg.Any<CreateServicesInfoRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateServicesInfoResponse
                    {
                        ServicesInfos = actualServicesInfos[0]
                    }
                );






            _mockgetMvnoConfigActionInfosByIdAndNameMs.Process(Arg.Any<GetMVNOConfigActionInfosByIdAndNameRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetMVNOConfigActionInfosByIdAndNameResponse
                    {
                        MvnoConfigActionInfos = actualmvnoConfig
                    }
                );



            _mockgetInvoiceByLegalInvoiceNumber.Process(Arg.Any<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                     new GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse
                     {
                         Invoices = actualInvoices
                     }
                );




            _mockcreateProductInfotMs.Process(Arg.Any<CreateProductInfoRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateProductInfoResponse
                    {
                        ProductInfo = actualProdutInfo
                    }
                );


            _mockgetActiveCusotmerAccountMs.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetActiveCustomerAccountAssociationByDateResponse
                    {
                        CustomerAccountAssociation = actualcustomeraccountAssoc
                    }
                );


            #endregion

            #region Setup Mock BizOp

            mockAssignProductOfferingToCustomerBizOp.Process(Arg.Any<AssignProductOfferingToCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    x => new AssignProductOfferingToCustomerResponseInternal()
                    {
                        ResultType = ResultTypes.Ok,
                        productPurchased = new CustomerProductAssignment()
                        {
                            // Return values passed as parameters
                            ProductOffering = ((AssignProductOfferingToCustomerRequestInternal)x[0]).ProductOffering.Item1,
                            ProductChargePurchased = ((AssignProductOfferingToCustomerRequestInternal)x[0]).ProductOffering.Item2
                        }

                    }
                );



            mockApplyCustomerPromotionBizOP.Process(Arg.Any<ApplyCustomerPromotionRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ApplyCustomerPromotionResponseInternal()
                    {
                        ResultType = ResultTypes.Ok,
                        CrmCustomersPromotionInfos = actualCustomerPromotionsInfos,
                        Customer = actualCusotmerInfo
                    }
                );



            mockApplyChargesBizOP.Process(Arg.Any<ApplyChargeAndScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ApplyChargeAndScheduleResponse()
                    {
                        ResultType = ResultTypes.Ok,
                        ChargeAdde = actualCustomerCharges[0],
                        ScheduleAdded = actualCustomerChargeeSchedule[0]
                    }
                );


            mockCancelCustomerBizOp.Process(Arg.Any<CancelCustomerProductRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CancelCustomerProductResponseInternal()
                    {
                        ResultType = ResultTypes.Ok
                    }
                );

            mockChargeServiceBizOp.Process(Arg.Any<ChargeServiceRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ChargeServiceResponseInternal()
                    {
                        CustomerCharges = actualCustomerCharges,
                        ResultType = ResultTypes.Ok
                    }
                );


            mockCheckPurchaseProductBizOp.Process(Arg.Any<CheckPurchaseProductForCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CheckPurchaseProductForCustomerResponseInternal()
                    {
                        IsCreditEnough = false,
                        IsLimitReached = false,
                        DeprovisionList = new List<ProductOfferingSpecificationOption>(),
                        ResultType = ResultTypes.Ok
                    }

                );

            #endregion

            #region Setup Mock Third party

            mockApplyRecurringCharge.firstPeriodChargingApply(Arg.Any<chargingRequestParameters>())
                .Returns
                (
                    new chargingApplyResponse()
                    {
                        errorMsg = "",
                        flag = 0,
                        customerChargedResultList = new[] { actualcustomerChargeInfo1, actualcustomerChargeInfo2 }
                    }

                );


            mockPurchaseHelper.GetProductsAndChargesOptions(Arg.Any<List<ProductCatalogDTO>>())
                .Returns
                (
                    listTupleFlattenedProducts
                );


            #endregion

            #region Customized CallBizOP



            var response = new PurchaseProductForCustomerResponseDTO();
            using (var conn = RepositoryManager.GetNewConnection())
            {
                PurchaseProductForCustomerBizOp bizop = new PurchaseProductForCustomerBizOp(mockApplyRecurringCharge, mockPurchaseHelper);




                //PurchaseProductForCustomerResponseInternal responseInternal =bizop.ProcessRequest(null, new PurchaseProductForCustomerRequestInternal()
                //{
                //    AccountDefinition = actualAccount,
                //    Customer = actualCusotmerInfo,
                //    DatetimePurchase = DateTime.Now,
                //    Invoice = actualInvoice,
                //    ForceCreditLimit = null,
                //    ProductsList = productListOk,
                //    TypeOfPurchaseProductOperation = PurchaseProductForCustomerBizOp.TypeOfPurchaseProduct.PurchaseProduct,
                //    MVNO = actualDealerInfo,
                //    User = CreateDefaultObject.Create<LoginInfo>()
                //});



                response = bizop.ProcessFromCustomerModel(new NullValidator<PurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<PurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<PurchaseProductForCustomerResponseDTO>(), requestDto, FakeInvoker);

            }
            RepositoryManager.CloseConnection();


            // PurchaseProductForCustomerResponseDTO response = new PurchaseProductForCustomerResponseDTO();
            // response = CallBizOp(requestDto);


            #endregion

            #region Asserts


            Assert.AreEqual(response.productPurchase.Count, 0);
            Assert.AreEqual(response.errorCode, BizOpsErrors.CreditNotEnough);
            Assert.AreEqual(response.resultType, ResultTypes.BussinessLogicError);

            // For now only MVNO is assigned in EmergencyTrace
            // Assert all Operationexecutions have mandatory fields
            //Assert.NotNull(_currentOperationExecution.Channel);
            Assert.NotNull(_currentOperationExecution.MVNO);
            //Assert.NotNull(_currentOperationExecution.User);
            //Assert.NotNull(_currentOperationExecution.Subscription);



            #endregion

        }



        /// <summary>
        /// returns LimitReached in checkProducts Error
        /// </summary>
        [Test, TestCaseSource("testData")]
        public void PurchaseProductForCustomerBizOpTests_CheckProductsLimitReached_NOk(List<ProductCatalogDTO> listProductparam)
        {


            #region RequestDTO

            PurchaseProductForCustomerRequestDTO requestDto = new PurchaseProductForCustomerRequestDTO
            {
                CustomerId = 1,
                forceCreditLimit = null,
                products = listProductparam,
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                channel = "TestDTOChannel"
            };

            #endregion

            #region Commons

            MockAbstractSinglePhaseOrderProcessor(requestDto);


            CommomInis();

            #endregion

            #region Setup mock Override commons

            BusinessOperationExecution _currentOperationExecution = new BusinessOperationExecution();
            var mockedRepoBusinessOperationExecution = MockRepositoryManager.GetMockedRepository<IBusinessOperationExecutionRepository<BusinessOperationExecution>>();
            mockedRepoBusinessOperationExecution.Create(Arg.Do<BusinessOperationExecution>(x => _currentOperationExecution = x));



            #endregion

            #region Assign values

            // setup
            var actualBundleInfo = CreateDefaultObject.Create<BundleInfo>();
            actualBundleInfo.EndDate = DateTime.Now.AddDays(1);


            var actualCharge = CreateDefaultObject.Create<ChargeNonRecurring>(1);
            var actualAccount = CreateDefaultObject.Create<Account>(1);
            var actualCusotmerInfo = CreateDefaultObject.Create<CustomerInfo>(1);
            actualCusotmerInfo.StatusID = 1;

            foreach (ServicesInfo item in actualCusotmerInfo.ServicesInfo)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
                item.CreditLimit = 9999;
            }

            actualCharge.Prices[0].EndDate = DateTime.Now.AddDays(1);

            var actualProdut = CreateDefaultObject.Create<ProductOffering>();

            // TODO: Bug in conversor, crashes if no Names.Texts or Descriptions.Texts
            actualProdut.Names.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            actualProdut.Description.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            actualProdut.ChargingOptions = new List<ProductChargeOption>();


            productListOk.Add(actualProdut);
            var actualProductCataloDTO = CreateDefaultObject.Create<ProductCatalogDTO>();
            actualProductCataloDTO.PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>();
            // productCatalogListOk.Add(actualProductCataloDTO);

            var actualProdutInfo = CreateDefaultObject.Create<ProductInfo>();
            var actualcustomeraccountAssoc = CreateDefaultObject.Create<CustomerAccountAssociation>();
            actualcustomeraccountAssoc.Account = actualAccount;

            var actualCustomerProducAssig = CreateDefaultObject.Create<CustomerProductAssignment>();
            var actualServicesInfos = new List<ServicesInfo>
            {
                CreateDefaultObject.Create<ServicesInfo>(),
                CreateDefaultObject.Create<ServicesInfo>()
            };

            foreach (ServicesInfo item in actualServicesInfos)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
            }

            // mark a master bundle
            actualServicesInfos[0].CREDITLIMITBASEBUNDLEID = (int)actualServicesInfos[0].BundleDefinition.BundleID;
            actualServicesInfos[0].CreditLimit = 9999;
            // actualCusotmerInfo.ServicesInfo = actualServicesInfos;


            var actualInvoice = CreateDefaultObject.Create<Invoice>();
            var actualInvoice2 = CreateDefaultObject.Create<Invoice>();
            var actualInvoices = new List<Invoice> { actualInvoice, actualInvoice2 };
            var actualmvnoConfig = new List<MVNOConfigActionInfo>
            {
                CreateDefaultObject.Create<MVNOConfigActionInfo>(),
                CreateDefaultObject.Create<MVNOConfigActionInfo>()
            };

            // Need to use old new approach for this entity
            var actualProductChargeOption = new ProductChargeOption();
            var actualProductChargeOption2 = new ProductChargeOption();
            actualProductChargeOption.Charges = new List<Charge> { actualCharge };
            actualProductChargeOption.ProductOffering = actualProdut;
            var actualProductChargeOptions = new List<ProductChargeOption> { actualProductChargeOption, actualProductChargeOption2 };


            var actualTaxDefinition = CreateDefaultObject.Create<TaxDefinition>();
            foreach (TaxRates item in actualTaxDefinition.Rates)
            {
                item.EndDate = DateTime.Now.AddDays(1);
            }
            var actualTaxDefinitions = new List<TaxDefinition>() { actualTaxDefinition };

            var actualNextBillrun = new DateTime(2015, 6, 1);

            var actualCustomerCharges = new List<CustomerCharge>()
            {
                CreateDefaultObject.Create<CustomerCharge>(),
                CreateDefaultObject.Create<CustomerCharge>()
            };

            var actualCustomerChargeeSchedule = new List<CustomerChargeSchedule>()
            {
                CreateDefaultObject.Create<CustomerChargeSchedule>(),
                CreateDefaultObject.Create<CustomerChargeSchedule>()
            };

            var actualcustomerChargeInfo1 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo1.customerChargeId = 1;
            var actualcustomerChargeInfo2 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo2.customerChargeId = 2;

            var actualpackage = CreateDefaultObject.Create<PackageInfo>();
            var actualrmPromotoinGroup = CreateDefaultObject.Create<RmPromotionGroupInfo>();
            var actualPromotoinPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            var listTupleFlattenedProducts = new List<Tuple<ProductOffering, ProductChargeOption>>();
            foreach (ProductCatalogDTO item in listProductparam)
            {
                ProductChargeOption pchargeOption = item.PurchaseOptions[0].ToCore();
                pchargeOption.Charges = new List<Charge>()
                {
                    CreateDefaultObject.Create<ChargeRecurring>(),
                    CreateDefaultObject.Create<ChargeNonRecurring>()
                };
                ProductOffering coreProduct = item.ToCore();
                coreProduct.OfferedProduct = CreateDefaultObject.Create<Product>();
                coreProduct.OfferedProduct.AssociatedBundle = actualBundleInfo;
                coreProduct.OfferedProduct.AssociatedPackage = actualpackage;
                coreProduct.OfferedProduct.AssociatedPrmotionGroup = actualrmPromotoinGroup;
                coreProduct.OfferedProduct.AssociatedPrmotionPlan = actualPromotoinPlan;
                coreProduct.OfferedProduct.VMO = actualDealerInfo;
                coreProduct.Options = new List<ProductOfferingOption>();
                listTupleFlattenedProducts.Add(Tuple.Create(coreProduct, pchargeOption));
            }

            var actualRmPromotionPlanInfos = new List<RmPromotionPlanInfo>()
            {
                CreateDefaultObject.Create<RmPromotionPlanInfo>(),
                CreateDefaultObject.Create<RmPromotionPlanInfo>()
            };

            var actualCustomerPromotionsInfos = new List<CrmCustomersPromotionInfo>()
            {
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>(),
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>()
            };


            #endregion

            #region Setup Mocks MS
            var checkAuthRespOk = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = true,
            };
            var checkAuthRespNok = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = false,
            };
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 1), null).Returns(checkAuthRespOk);
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 2), null).Returns(checkAuthRespNok);

            _mockcreateCustomerChargeScheduleResponseMs.Process(Arg.Any<CreateCustomerChargeScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateCustomerChargeScheduleResponse()
                    {
                        CustomerChargeSchedule = actualCustomerChargeeSchedule[0]
                    }
                );


            _mockgetRmPromotionPlanInfoMs.Process(Arg.Any<GetRmPromotionPlanInfosByIdsRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetRmPromotionPlanInfosByIdsResponse()
                    {
                        RmPromotionPlanInfos = actualRmPromotionPlanInfos

                    }
                );


            _mockgetPriorityBundleMs.Process(Arg.Any<GetPriorityBundleInfoFromBundleInfosRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetPriorityBundleInfoFromBundleInfosResponse()
                    {
                        PriorityBundle = actualBundleInfo,
                        ResultType = ResultTypes.Ok
                    }
                );




            _mockcalculateNextBillrunDateMs.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CalculateNextBillRunDateForBillCycleResponse
                    {
                        NextBillRun = actualNextBillrun
                    }
                );




            _mockgetTaxAuthorityMs.Process(Arg.Any<GetTaxAuthorityRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetTaxAuthorityResponse
                    {
                        TaxDefinitions = actualTaxDefinitions
                    }
                );



            _mockcreateServiceMs.Process(Arg.Any<CreateServicesInfoRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateServicesInfoResponse
                    {
                        ServicesInfos = actualServicesInfos[0]
                    }
                );




            _mockgetMvnoConfigActionInfosByIdAndNameMs.Process(Arg.Any<GetMVNOConfigActionInfosByIdAndNameRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetMVNOConfigActionInfosByIdAndNameResponse
                    {
                        MvnoConfigActionInfos = actualmvnoConfig
                    }
                );



            _mockgetInvoiceByLegalInvoiceNumber.Process(Arg.Any<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                     new GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse
                     {
                         Invoices = actualInvoices
                     }
                );




            _mockcreateProductInfotMs.Process(Arg.Any<CreateProductInfoRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateProductInfoResponse
                    {
                        ProductInfo = actualProdutInfo
                    }
                );



            _mockgetActiveCusotmerAccountMs.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetActiveCustomerAccountAssociationByDateResponse
                    {
                        CustomerAccountAssociation = actualcustomeraccountAssoc
                    }
                );


            #endregion

            #region Setup Mock BizOp

            mockAssignProductOfferingToCustomerBizOp.Process(Arg.Any<AssignProductOfferingToCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    x => new AssignProductOfferingToCustomerResponseInternal()
                    {
                        ResultType = ResultTypes.Ok,
                        productPurchased = new CustomerProductAssignment()
                        {
                            // Return values passed as parameters
                            ProductOffering =  ((AssignProductOfferingToCustomerRequestInternal)x[0]).ProductOffering.Item1,
                            ProductChargePurchased = ((AssignProductOfferingToCustomerRequestInternal)x[0]).ProductOffering.Item2
                        }

                    }
                );



            mockApplyCustomerPromotionBizOP.Process(Arg.Any<ApplyCustomerPromotionRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ApplyCustomerPromotionResponseInternal()
                    {
                        ResultType = ResultTypes.Ok,
                        CrmCustomersPromotionInfos = actualCustomerPromotionsInfos,
                        Customer = actualCusotmerInfo
                    }
                );



            mockApplyChargesBizOP.Process(Arg.Any<ApplyChargeAndScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ApplyChargeAndScheduleResponse()
                    {
                        ResultType = ResultTypes.Ok,
                        ChargeAdde = actualCustomerCharges[0],
                        ScheduleAdded = actualCustomerChargeeSchedule[0]
                    }
                );


            mockCancelCustomerBizOp.Process(Arg.Any<CancelCustomerProductRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CancelCustomerProductResponseInternal()
                    {
                        ResultType = ResultTypes.Ok
                    }
                );

            mockChargeServiceBizOp.Process(Arg.Any<ChargeServiceRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ChargeServiceResponseInternal()
                    {
                        CustomerCharges = actualCustomerCharges,
                        ResultType = ResultTypes.Ok
                    }
                );


            mockCheckPurchaseProductBizOp.Process(Arg.Any<CheckPurchaseProductForCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CheckPurchaseProductForCustomerResponseInternal()
                    {
                        IsCreditEnough = true,
                        IsLimitReached = true,
                        DeprovisionList = new List<ProductOfferingSpecificationOption>(),
                        ResultType = ResultTypes.Ok
                    }

                );

            #endregion

            #region Setup Mock Third party

            mockApplyRecurringCharge.firstPeriodChargingApply(Arg.Any<chargingRequestParameters>())
                .Returns
                (
                    new chargingApplyResponse()
                    {
                        errorMsg = "",
                        flag = 0,
                        customerChargedResultList = new[] { actualcustomerChargeInfo1, actualcustomerChargeInfo2 }
                    }

                );


            mockPurchaseHelper.GetProductsAndChargesOptions(Arg.Any<List<ProductCatalogDTO>>())
                .Returns
                (
                    listTupleFlattenedProducts
                );


            #endregion

            #region Customized CallBizOP



            var response = new PurchaseProductForCustomerResponseDTO();
            using (var conn = RepositoryManager.GetNewConnection())
            {
                PurchaseProductForCustomerBizOp bizop = new PurchaseProductForCustomerBizOp(mockApplyRecurringCharge, mockPurchaseHelper);




                //PurchaseProductForCustomerResponseInternal responseInternal =bizop.ProcessRequest(null, new PurchaseProductForCustomerRequestInternal()
                //{
                //    AccountDefinition = actualAccount,
                //    Customer = actualCusotmerInfo,
                //    DatetimePurchase = DateTime.Now,
                //    Invoice = actualInvoice,
                //    ForceCreditLimit = null,
                //    ProductsList = productListOk,
                //    TypeOfPurchaseProductOperation = PurchaseProductForCustomerBizOp.TypeOfPurchaseProduct.PurchaseProduct,
                //    MVNO = actualDealerInfo,
                //    User = CreateDefaultObject.Create<LoginInfo>()
                //});



                response = bizop.ProcessFromCustomerModel(new NullValidator<PurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<PurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<PurchaseProductForCustomerResponseDTO>(), requestDto, FakeInvoker);

            }
            RepositoryManager.CloseConnection();


            // PurchaseProductForCustomerResponseDTO response = new PurchaseProductForCustomerResponseDTO();
            // response = CallBizOp(requestDto);


            #endregion

            #region Asserts


            Assert.AreEqual(response.productPurchase.Count, 0);
            Assert.AreEqual(response.errorCode, BizOpsErrors.PromotionLimitReached);
            Assert.AreEqual(response.resultType, ResultTypes.BussinessLogicError);

            // For now only MVNO is assigned in EmergencyTrace
            // Assert all Operationexecutions have mandatory fields
            //Assert.NotNull(_currentOperationExecution.Channel);
            Assert.NotNull(_currentOperationExecution.MVNO);
            //Assert.NotNull(_currentOperationExecution.User);
            //Assert.NotNull(_currentOperationExecution.Subscription);



            #endregion

        }



        /// <summary>
        /// all ok, but one product for deprovisioning
        /// </summary>
        [Test, TestCaseSource("testData")]
        public void PurchaseProductForCustomerBizOpTests_CheckProductsDeprovisionProduct_Ok(List<ProductCatalogDTO> listProductparam)
        {


            #region RequestDTO

            PurchaseProductForCustomerRequestDTO requestDto = new PurchaseProductForCustomerRequestDTO
            {
                CustomerId = 1,
                forceCreditLimit = null,
                products = listProductparam,
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                channel = "TestDTOChannel"
            };

            #endregion

            #region Commons

            MockAbstractSinglePhaseOrderProcessor(requestDto);


            CommomInis();

            #endregion

            #region Setup mock Override commons

            BusinessOperationExecution _currentOperationExecution = new BusinessOperationExecution();
            var mockedRepoBusinessOperationExecution = MockRepositoryManager.GetMockedRepository<IBusinessOperationExecutionRepository<BusinessOperationExecution>>();
            mockedRepoBusinessOperationExecution.Create(Arg.Do<BusinessOperationExecution>(x => _currentOperationExecution = x));



            #endregion

            #region Assign values

            // setup
            var actualBundleInfo = CreateDefaultObject.Create<BundleInfo>();
            actualBundleInfo.EndDate = DateTime.Now.AddDays(1);


            var actualCharge = CreateDefaultObject.Create<ChargeNonRecurring>(1);
            var actualAccount = CreateDefaultObject.Create<Account>(1);
            var actualCusotmerInfo = CreateDefaultObject.Create<CustomerInfo>(1);
            actualCusotmerInfo.StatusID = 1;

            foreach (ServicesInfo item in actualCusotmerInfo.ServicesInfo)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
                item.CreditLimit = 9999;
                item.CREDITLIMITBASEBUNDLEID = 1;
            }

            actualCharge.Prices[0].EndDate = DateTime.Now.AddDays(1);

            var actualProdut = CreateDefaultObject.Create<ProductOffering>();

            // TODO: Bug in conversor, crashes if no Names.Texts or Descriptions.Texts
            actualProdut.Names.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            actualProdut.Description.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            actualProdut.ChargingOptions = new List<ProductChargeOption>();


            productListOk.Add(actualProdut);
            var actualProductCataloDTO = CreateDefaultObject.Create<ProductCatalogDTO>();
            actualProductCataloDTO.PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>();
            // productCatalogListOk.Add(actualProductCataloDTO);

            var actualProdutInfo = CreateDefaultObject.Create<ProductInfo>();
            var actualcustomeraccountAssoc = CreateDefaultObject.Create<CustomerAccountAssociation>();
            actualcustomeraccountAssoc.Account = actualAccount;

            var actualCustomerProducAssig = CreateDefaultObject.Create<CustomerProductAssignment>();
            var actualServicesInfos = new List<ServicesInfo>
            {
                CreateDefaultObject.Create<ServicesInfo>(),
                CreateDefaultObject.Create<ServicesInfo>()
            };

            foreach (ServicesInfo item in actualServicesInfos)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
            }

            // mark a master bundle
            actualServicesInfos[0].CREDITLIMITBASEBUNDLEID = (int)actualServicesInfos[0].BundleDefinition.BundleID;
            actualServicesInfos[0].CreditLimit = 9999;
            // actualCusotmerInfo.ServicesInfo = actualServicesInfos;


            var actualInvoice = CreateDefaultObject.Create<Invoice>();
            var actualInvoice2 = CreateDefaultObject.Create<Invoice>();
            var actualInvoices = new List<Invoice> { actualInvoice, actualInvoice2 };
            var actualmvnoConfig = new List<MVNOConfigActionInfo>
            {
                CreateDefaultObject.Create<MVNOConfigActionInfo>(),
                CreateDefaultObject.Create<MVNOConfigActionInfo>()
            };

            // Need to use old new approach for this entity
            var actualProductChargeOption = new ProductChargeOption();
            var actualProductChargeOption2 = new ProductChargeOption();
            actualProductChargeOption.Charges = new List<Charge> { actualCharge };
            actualProductChargeOption.ProductOffering = actualProdut;
            var actualProductChargeOptions = new List<ProductChargeOption> { actualProductChargeOption, actualProductChargeOption2 };


            var actualTaxDefinition = CreateDefaultObject.Create<TaxDefinition>();
            foreach (TaxRates item in actualTaxDefinition.Rates)
            {
                item.EndDate = DateTime.Now.AddDays(1);
            }
            var actualTaxDefinitions = new List<TaxDefinition>() { actualTaxDefinition };

            var actualNextBillrun = new DateTime(2015, 6, 1);

            var actualCustomerCharges = new List<CustomerCharge>()
            {
                CreateDefaultObject.Create<CustomerCharge>(),
                CreateDefaultObject.Create<CustomerCharge>()
            };

            var actualCustomerChargeeSchedule = new List<CustomerChargeSchedule>()
            {
                CreateDefaultObject.Create<CustomerChargeSchedule>(),
                CreateDefaultObject.Create<CustomerChargeSchedule>()
            };

            var actualcustomerChargeInfo1 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo1.customerChargeId = 1;
            var actualcustomerChargeInfo2 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo2.customerChargeId = 2;

            var actualpackage = CreateDefaultObject.Create<PackageInfo>();
            var actualrmPromotoinGroup = CreateDefaultObject.Create<RmPromotionGroupInfo>();
            var actualPromotoinPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            var listTupleFlattenedProducts = new List<Tuple<ProductOffering, ProductChargeOption>>();
            foreach (ProductCatalogDTO item in listProductparam)
            {
                ProductChargeOption pchargeOption = item.PurchaseOptions[0].ToCore();
                pchargeOption.Charges = new List<Charge>()
                {
                    CreateDefaultObject.Create<ChargeRecurring>(),
                    CreateDefaultObject.Create<ChargeNonRecurring>()
                };
                ProductOffering coreProduct = item.ToCore();
                coreProduct.OfferedProduct = CreateDefaultObject.Create<Product>();
                coreProduct.OfferedProduct.AssociatedBundle = actualBundleInfo;
                coreProduct.OfferedProduct.AssociatedPackage = actualpackage;
                coreProduct.OfferedProduct.AssociatedPrmotionGroup = actualrmPromotoinGroup;
                coreProduct.OfferedProduct.AssociatedPrmotionPlan = actualPromotoinPlan;
                coreProduct.OfferedProduct.VMO = actualDealerInfo;
                coreProduct.Options = new List<ProductOfferingOption>();
                listTupleFlattenedProducts.Add(Tuple.Create(coreProduct, pchargeOption));
            }

            var actualRmPromotionPlanInfos = new List<RmPromotionPlanInfo>()
            {
                CreateDefaultObject.Create<RmPromotionPlanInfo>(),
                CreateDefaultObject.Create<RmPromotionPlanInfo>()
            };

            var actualCustomerPromotionsInfos = new List<CrmCustomersPromotionInfo>()
            {
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>(),
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>()
            };


            #endregion

            #region Setup Mocks MS
            var checkAuthRespOk = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = true,
            };
            var checkAuthRespNok = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = false,
            };
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 1), null).Returns(checkAuthRespOk);
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 2), null).Returns(checkAuthRespNok);

            _mockcreateCustomerChargeScheduleResponseMs.Process(Arg.Any<CreateCustomerChargeScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateCustomerChargeScheduleResponse()
                    {
                        CustomerChargeSchedule = actualCustomerChargeeSchedule[0]
                    }
                );


            _mockgetRmPromotionPlanInfoMs.Process(Arg.Any<GetRmPromotionPlanInfosByIdsRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetRmPromotionPlanInfosByIdsResponse()
                    {
                        RmPromotionPlanInfos = actualRmPromotionPlanInfos

                    }
                );


            _mockgetPriorityBundleMs.Process(Arg.Any<GetPriorityBundleInfoFromBundleInfosRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetPriorityBundleInfoFromBundleInfosResponse()
                    {
                        PriorityBundle = actualBundleInfo,
                        ResultType = ResultTypes.Ok
                    }
                );




            _mockcalculateNextBillrunDateMs.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CalculateNextBillRunDateForBillCycleResponse
                    {
                        NextBillRun = actualNextBillrun
                    }
                );



            _mockgetTaxAuthorityMs.Process(Arg.Any<GetTaxAuthorityRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetTaxAuthorityResponse
                    {
                        TaxDefinitions = actualTaxDefinitions
                    }
                );



            _mockcreateServiceMs.Process(Arg.Any<CreateServicesInfoRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateServicesInfoResponse
                    {
                        ServicesInfos = actualServicesInfos[0]
                    }
                );





            _mockgetMvnoConfigActionInfosByIdAndNameMs.Process(Arg.Any<GetMVNOConfigActionInfosByIdAndNameRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetMVNOConfigActionInfosByIdAndNameResponse
                    {
                        MvnoConfigActionInfos = actualmvnoConfig
                    }
                );



            _mockgetInvoiceByLegalInvoiceNumber.Process(Arg.Any<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                     new GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse
                     {
                         Invoices = actualInvoices
                     }
                );




            _mockcreateProductInfotMs.Process(Arg.Any<CreateProductInfoRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateProductInfoResponse
                    {
                        ProductInfo = actualProdutInfo
                    }
                );



            _mockgetActiveCusotmerAccountMs.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetActiveCustomerAccountAssociationByDateResponse
                    {
                        CustomerAccountAssociation = actualcustomeraccountAssoc
                    }
                );


            #endregion

            #region Setup Mock BizOp

            mockAssignProductOfferingToCustomerBizOp.Process(Arg.Any<AssignProductOfferingToCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    x => new AssignProductOfferingToCustomerResponseInternal()
                    {
                        ResultType = ResultTypes.Ok,
                        productPurchased = new CustomerProductAssignment()
                        {
                            // Return values passed as parameters
                            ProductOffering = ((AssignProductOfferingToCustomerRequestInternal)x[0]).ProductOffering.Item1,
                            ProductChargePurchased = ((AssignProductOfferingToCustomerRequestInternal)x[0]).ProductOffering.Item2
                        }

                    }
                );


            mockApplyCustomerPromotionBizOP.Process(Arg.Any<ApplyCustomerPromotionRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ApplyCustomerPromotionResponseInternal()
                    {
                        ResultType = ResultTypes.Ok,
                        CrmCustomersPromotionInfos = actualCustomerPromotionsInfos,
                        Customer = actualCusotmerInfo
                    }
                );



            mockApplyChargesBizOP.Process(Arg.Any<ApplyChargeAndScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ApplyChargeAndScheduleResponse()
                    {
                        ResultType = ResultTypes.Ok,
                        ChargeAdde = actualCustomerCharges[0],
                        ScheduleAdded = actualCustomerChargeeSchedule[0]
                    }
                );


            mockCancelCustomerBizOp.Process(Arg.Any<CancelCustomerProductRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CancelCustomerProductResponseInternal()
                    {
                        ResultType = ResultTypes.Ok
                    }
                );

            mockChargeServiceBizOp.Process(Arg.Any<ChargeServiceRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ChargeServiceResponseInternal()
                    {
                        CustomerCharges = actualCustomerCharges,
                        ResultType = ResultTypes.Ok
                    }
                );


            var deprovision = new ProductOfferingSpecificationOption()
            {
                RelatedProductOffering = listProductparam[0].ToCore(),
                SpecifiedProductOffering = listProductparam[0].ToCore()
            };
            deprovision.RelatedProductOffering.Id = listProductparam[0].ToCore().Id;
            deprovision.SpecifiedProductOffering.Id = listProductparam[0].ToCore().Id;
            // Bug in conversor
            deprovision.RelatedProductOffering.Names = new MultiLingualDescription() { Texts = new List<LanguageSpecificText>() };
            deprovision.RelatedProductOffering.Description = new MultiLingualDescription() { Texts = new List<LanguageSpecificText>() };
            deprovision.RelatedProductOffering.ChargingOptions = new List<ProductChargeOption>();
            deprovision.RelatedProductOffering.Options = new List<ProductOfferingOption>();
            deprovision.SpecifiedProductOffering.Names = new MultiLingualDescription() { Texts = new List<LanguageSpecificText>() };
            deprovision.SpecifiedProductOffering.Description = new MultiLingualDescription() { Texts = new List<LanguageSpecificText>() };
            deprovision.SpecifiedProductOffering.ChargingOptions = new List<ProductChargeOption>();
            deprovision.SpecifiedProductOffering.Options = new List<ProductOfferingOption>();

            deprovision.RelatedProductOffering.OfferedProduct = CreateDefaultObject.Create<Product>();
            deprovision.SpecifiedProductOffering.OfferedProduct = CreateDefaultObject.Create<Product>();

            mockCheckPurchaseProductBizOp.Process(Arg.Any<CheckPurchaseProductForCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CheckPurchaseProductForCustomerResponseInternal()
                    {
                        IsCreditEnough = true,
                        IsLimitReached = false,
                        DeprovisionList = new List<ProductOfferingSpecificationOption>()
                        {
                            new ProductOfferingSpecificationOption()
                            {
                                RelatedProductOffering = deprovision.RelatedProductOffering ,
                                SpecifiedProductOffering =deprovision.SpecifiedProductOffering
                            }
                        },
                        ResultType = ResultTypes.Ok
                    }

                );

            #endregion

            #region Setup Mock Third party

            mockApplyRecurringCharge.firstPeriodChargingApply(Arg.Any<chargingRequestParameters>())
                .Returns
                (
                    new chargingApplyResponse()
                    {
                        errorMsg = "",
                        flag = 0,
                        customerChargedResultList = new[] { actualcustomerChargeInfo1, actualcustomerChargeInfo2 }
                    }

                );


            mockPurchaseHelper.GetProductsAndChargesOptions(Arg.Any<List<ProductCatalogDTO>>())
                .Returns
                (
                    listTupleFlattenedProducts
                );


            #endregion

            #region Customized CallBizOP



            var response = new PurchaseProductForCustomerResponseDTO();
            using (var conn = RepositoryManager.GetNewConnection())
            {
                PurchaseProductForCustomerBizOp bizop = new PurchaseProductForCustomerBizOp(mockApplyRecurringCharge, mockPurchaseHelper);




                //PurchaseProductForCustomerResponseInternal responseInternal =bizop.ProcessRequest(null, new PurchaseProductForCustomerRequestInternal()
                //{
                //    AccountDefinition = actualAccount,
                //    Customer = actualCusotmerInfo,
                //    DatetimePurchase = DateTime.Now,
                //    Invoice = actualInvoice,
                //    ForceCreditLimit = null,
                //    ProductsList = productListOk,
                //    TypeOfPurchaseProductOperation = PurchaseProductForCustomerBizOp.TypeOfPurchaseProduct.PurchaseProduct,
                //    MVNO = actualDealerInfo,
                //    User = CreateDefaultObject.Create<LoginInfo>()
                //});



                response = bizop.ProcessFromCustomerModel(new NullValidator<PurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<PurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<PurchaseProductForCustomerResponseDTO>(), requestDto, FakeInvoker);

            }
            RepositoryManager.CloseConnection();


            // PurchaseProductForCustomerResponseDTO response = new PurchaseProductForCustomerResponseDTO();
            // response = CallBizOp(requestDto);


            #endregion

            #region Asserts


            Assert.AreEqual(response.productPurchase.Count, listProductparam.Count);
            Assert.AreEqual(response.deprovisionedProductList.Count, 1);
            for (int x = 0; x < listProductparam.Count; x++)
            {
                Assert.AreEqual(response.productPurchase[x].PurchasedProductOfferingId, listProductparam[x].Id);
                Assert.AreEqual(response.productPurchase[x].ProductChargePurchasedId, listProductparam[x].PurchaseOptions[0].Id);
            }
            Assert.AreEqual(response.deprovisionedProductList[0].Id, deprovision.RelatedProductOffering.Id);
            Assert.AreEqual(response.resultType, ResultTypes.Ok);

            // Assert all Operationexecutions have mandatory fields
            Assert.NotNull(_currentOperationExecution.Channel);
            Assert.NotNull(_currentOperationExecution.MVNO);
            Assert.NotNull(_currentOperationExecution.User);
            Assert.NotNull(_currentOperationExecution.Subscription);



            #endregion

        }



        /// <summary>
        /// Positive Case All products checked OK, Purchase another BubdleID aside for the existing ones
        /// </summary>
        [Test, TestCaseSource("testData")]
        public void PurchaseProductForCustomerBizOpTests_CheckProductsAllOk_ExistingBundles_Ok(List<ProductCatalogDTO> listProductparam)
        {

            List<ServicesInfo> returnedList = new List<ServicesInfo>();

            #region RequestDTO

            PurchaseProductForCustomerRequestDTO requestDto = new PurchaseProductForCustomerRequestDTO
            {
                CustomerId = 1,
                forceCreditLimit = null,
                products = listProductparam,
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                channel = "TestDTOChannel"
            };

            #endregion

            #region Commons

            MockAbstractSinglePhaseOrderProcessor(requestDto);


            CommomInis();

            #endregion

            #region Setup mock Override commons

            BusinessOperationExecution _currentOperationExecution = new BusinessOperationExecution();
            var mockedRepoBusinessOperationExecution = MockRepositoryManager.GetMockedRepository<IBusinessOperationExecutionRepository<BusinessOperationExecution>>();
            mockedRepoBusinessOperationExecution.Create(Arg.Do<BusinessOperationExecution>(x => _currentOperationExecution = x));



            #endregion

            #region Find out BizOP OperationID

            //var bizopID = new PurchaseProductForCustomerBizOp();
            //var a = bizopID.Id;

            //int operationId = bizopID.OperationCode.GetHashCode();

            #endregion

            #region Assign values

            // setup
            var actualBundleInfo = CreateDefaultObject.Create<BundleInfo>();
            actualBundleInfo.EndDate = DateTime.Now.AddDays(1);


            var actualCharge = CreateDefaultObject.Create<ChargeNonRecurring>(1);
            var actualAccount = CreateDefaultObject.Create<Account>(1);
            var actualCusotmerInfo = CreateDefaultObject.Create<CustomerInfo>(1);
            actualCusotmerInfo.StatusID = 1;

            foreach (ServicesInfo item in actualCusotmerInfo.ServicesInfo)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
                item.CreditLimit = 9999;
                item.CREDITLIMITBASEBUNDLEID = 1;
            }

            actualCharge.Prices[0].EndDate = DateTime.Now.AddDays(1);

            var actualProdut = CreateDefaultObject.Create<ProductOffering>();

            // TODO: Bug in conversor, crashes if no Names.Texts or Descriptions.Texts
            actualProdut.Names.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            actualProdut.Description.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            actualProdut.ChargingOptions = new List<ProductChargeOption>();


            productListOk.Add(actualProdut);
            var actualProductCataloDTO = CreateDefaultObject.Create<ProductCatalogDTO>();
            actualProductCataloDTO.PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>();
            // productCatalogListOk.Add(actualProductCataloDTO);

            var actualProdutInfo = CreateDefaultObject.Create<ProductInfo>();

            foreach (ServicesInfo service in ((ProductInfo)actualProdutInfo).ServiceInfo)
            {
                service.CREDITLIMITBASEBUNDLEID = 1;

            }

            var actualcustomeraccountAssoc = CreateDefaultObject.Create<CustomerAccountAssociation>();
            actualcustomeraccountAssoc.Account = actualAccount;

            var actualCustomerProducAssig = CreateDefaultObject.Create<CustomerProductAssignment>();
            var actualServicesInfos = new List<ServicesInfo>
            {
                CreateDefaultObject.Create<ServicesInfo>(),
                CreateDefaultObject.Create<ServicesInfo>()
            };

            foreach (ServicesInfo item in actualServicesInfos)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
                item.CREDITLIMITBASEBUNDLEID = 1;
            }

            // mark a master bundle
            actualServicesInfos[0].CREDITLIMITBASEBUNDLEID = (int)actualServicesInfos[0].BundleDefinition.BundleID;
            actualServicesInfos[0].CreditLimit = 9999;
            // actualCusotmerInfo.ServicesInfo = actualServicesInfos;


            var actualInvoice = CreateDefaultObject.Create<Invoice>();
            var actualInvoice2 = CreateDefaultObject.Create<Invoice>();
            var actualInvoices = new List<Invoice> { actualInvoice, actualInvoice2 };
            var actualmvnoConfig = new List<MVNOConfigActionInfo>
            {
                CreateDefaultObject.Create<MVNOConfigActionInfo>(),
                CreateDefaultObject.Create<MVNOConfigActionInfo>()
            };

            // Need to use old new approach for this entity
            var actualProductChargeOption = new ProductChargeOption();
            var actualProductChargeOption2 = new ProductChargeOption();
            actualProductChargeOption.Charges = new List<Charge> { actualCharge };
            actualProductChargeOption.ProductOffering = actualProdut;
            var actualProductChargeOptions = new List<ProductChargeOption> { actualProductChargeOption, actualProductChargeOption2 };



            var actualTaxDefinition = CreateDefaultObject.Create<TaxDefinition>();
            foreach (TaxRates item in actualTaxDefinition.Rates)
            {
                item.EndDate = DateTime.Now.AddDays(1);
            }
            var actualTaxDefinitions = new List<TaxDefinition>() { actualTaxDefinition };

            var actualNextBillrun = new DateTime(2015, 6, 1);

            var actualCustomerCharges = new List<CustomerCharge>()
            {
                CreateDefaultObject.Create<CustomerCharge>(),
                CreateDefaultObject.Create<CustomerCharge>()
            };

            var actualCustomerChargeeSchedule = new List<CustomerChargeSchedule>()
            {
                CreateDefaultObject.Create<CustomerChargeSchedule>(),
                CreateDefaultObject.Create<CustomerChargeSchedule>()
            };

            var actualcustomerChargeInfo1 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo1.customerChargeId = 1;
            var actualcustomerChargeInfo2 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo2.customerChargeId = 2;

            var actualpackage = CreateDefaultObject.Create<PackageInfo>();
            var actualrmPromotoinGroup = CreateDefaultObject.Create<RmPromotionGroupInfo>();
            var actualPromotoinPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            var listTupleFlattenedProducts = new List<Tuple<ProductOffering, ProductChargeOption>>();
            foreach (ProductCatalogDTO item in listProductparam)
            {
                ProductChargeOption pchargeOption = item.PurchaseOptions[0].ToCore();
                pchargeOption.Charges = new List<Charge>()
                {
                    CreateDefaultObject.Create<ChargeRecurring>(),
                    CreateDefaultObject.Create<ChargeNonRecurring>()
                };
                ProductOffering coreProduct = item.ToCore();
                coreProduct.OfferedProduct = CreateDefaultObject.Create<Product>();
                coreProduct.OfferedProduct.AssociatedBundle = actualBundleInfo;
                coreProduct.OfferedProduct.AssociatedPackage = actualpackage;
                coreProduct.OfferedProduct.AssociatedPrmotionGroup = actualrmPromotoinGroup;
                coreProduct.OfferedProduct.AssociatedPrmotionPlan = actualPromotoinPlan;
                coreProduct.OfferedProduct.VMO = actualDealerInfo;
                coreProduct.Options = new List<ProductOfferingOption>();
                listTupleFlattenedProducts.Add(Tuple.Create(coreProduct, pchargeOption));
            }

            var actualRmPromotionPlanInfos = new List<RmPromotionPlanInfo>()
            {
                CreateDefaultObject.Create<RmPromotionPlanInfo>(),
                CreateDefaultObject.Create<RmPromotionPlanInfo>()
            };

            var actualCustomerPromotionsInfos = new List<CrmCustomersPromotionInfo>()
            {
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>(),
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>()
            };


            #endregion

            #region Setup Mocks MS
            var checkAuthRespOk = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = true,
            };
            var checkAuthRespNok = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = false,
            };
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 1), null).Returns(checkAuthRespOk);
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 2), null).Returns(checkAuthRespNok);

            _mockcreateCustomerChargeScheduleResponseMs.Process(Arg.Any<CreateCustomerChargeScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateCustomerChargeScheduleResponse()
                    {
                        CustomerChargeSchedule = actualCustomerChargeeSchedule[0]
                    }
                );



            _mockgetRmPromotionPlanInfoMs.Process(Arg.Any<GetRmPromotionPlanInfosByIdsRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetRmPromotionPlanInfosByIdsResponse()
                    {
                        RmPromotionPlanInfos = actualRmPromotionPlanInfos

                    }
                );

            _mockgetPriorityBundleMs.Process(Arg.Any<GetPriorityBundleInfoFromBundleInfosRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetPriorityBundleInfoFromBundleInfosResponse()
                    {
                        PriorityBundle = actualBundleInfo,
                        ResultType = ResultTypes.Ok
                    }
                );




            _mockcalculateNextBillrunDateMs.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CalculateNextBillRunDateForBillCycleResponse
                    {
                        NextBillRun = actualNextBillrun
                    }
                );



            _mockgetTaxAuthorityMs.Process(Arg.Any<GetTaxAuthorityRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetTaxAuthorityResponse
                    {
                        TaxDefinitions = actualTaxDefinitions
                    }
                );



            //_mockcreateServiceMs.Process(Arg.Any<CreateServicesInfoRequest>(), Arg.Any<RequestInvokationEnvironment>())
            //    .Returns
            //    (
            //        new CreateServicesInfoResponse
            //        {
            //            ServicesInfos = actualServicesInfos[0]
            //        }
            //    );


            // Modify return of the mock with the same argument as first parameter
            // Save values mocked in a local List for further process
            _mockcreateServiceMs.Process(Arg.Any<CreateServicesInfoRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (x =>
                {
                    var tmp =new CreateServicesInfoResponse
                    {
                        ServicesInfos = (ServicesInfo) ((CreateServicesInfoRequest) x[0]).subService
                    };
                    returnedList.Add(tmp.ServicesInfos);
                    return tmp;
                }
                );




            _mockgetMvnoConfigActionInfosByIdAndNameMs.Process(Arg.Any<GetMVNOConfigActionInfosByIdAndNameRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetMVNOConfigActionInfosByIdAndNameResponse
                    {
                        MvnoConfigActionInfos = actualmvnoConfig
                    }
                );



            _mockgetInvoiceByLegalInvoiceNumber.Process(Arg.Any<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                     new GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse
                     {
                         Invoices = actualInvoices
                     }
                );




            _mockcreateProductInfotMs.Process(Arg.Any<CreateProductInfoRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateProductInfoResponse
                    {
                        ProductInfo = actualProdutInfo
                    }
                );



            _mockgetActiveCusotmerAccountMs.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetActiveCustomerAccountAssociationByDateResponse
                    {
                        CustomerAccountAssociation = actualcustomeraccountAssoc
                    }
                );

            var actualExistingCustomerProductAssignment = CreateDefaultObject.Create<CustomerProductAssignment>();
            actualExistingCustomerProductAssignment.EndDate = null;
            actualExistingCustomerProductAssignment.PurchasedProduct.Id = actualCustomerProducAssig.PurchasedProduct.Id;


            #endregion

            #region Setup Mock BizOp

            mockAssignProductOfferingToCustomerBizOp.Process(Arg.Any<AssignProductOfferingToCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    x => new AssignProductOfferingToCustomerResponseInternal()
                    {
                        ResultType = ResultTypes.Ok,
                        productPurchased =  new CustomerProductAssignment()
                        {
                            // Return values passed as parameters
                            ProductOffering = ((AssignProductOfferingToCustomerRequestInternal) x[0]).ProductOffering.Item1,
                            ProductChargePurchased = ((AssignProductOfferingToCustomerRequestInternal)x[0]).ProductOffering.Item2
                        }
                        
                    }
                );


            mockApplyCustomerPromotionBizOP.Process(Arg.Any<ApplyCustomerPromotionRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ApplyCustomerPromotionResponseInternal()
                    {
                        ResultType = ResultTypes.Ok,
                        CrmCustomersPromotionInfos = actualCustomerPromotionsInfos,
                        Customer = actualCusotmerInfo
                    }
                );



            mockApplyChargesBizOP.Process(Arg.Any<ApplyChargeAndScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ApplyChargeAndScheduleResponse()
                    {
                        ResultType = ResultTypes.Ok,
                        ChargeAdde = actualCustomerCharges[0],
                        ScheduleAdded = actualCustomerChargeeSchedule[0]
                    }
                );


            mockCancelCustomerBizOp.Process(Arg.Any<CancelCustomerProductRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CancelCustomerProductResponseInternal()
                    {
                        ResultType = ResultTypes.Ok
                    }
                );

            mockChargeServiceBizOp.Process(Arg.Any<ChargeServiceRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ChargeServiceResponseInternal()
                    {
                        CustomerCharges = actualCustomerCharges,
                        ResultType = ResultTypes.Ok
                    }
                );


            mockCheckPurchaseProductBizOp.Process(Arg.Any<CheckPurchaseProductForCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CheckPurchaseProductForCustomerResponseInternal()
                    {
                        IsCreditEnough = true,
                        IsLimitReached = false,
                        DeprovisionList = new List<ProductOfferingSpecificationOption>(),
                        ResultType = ResultTypes.Ok
                    }
                );

            #endregion

            #region Setup Mock Third party

            mockApplyRecurringCharge.firstPeriodChargingApply(Arg.Any<chargingRequestParameters>())
                .Returns
                (
                    new chargingApplyResponse()
                    {
                        errorMsg = "",
                        flag = 0,
                        customerChargedResultList = new[] { actualcustomerChargeInfo1, actualcustomerChargeInfo2 }
                    }

                );


            mockPurchaseHelper.GetProductsAndChargesOptions(Arg.Any<List<ProductCatalogDTO>>())
                .Returns
                (
                    listTupleFlattenedProducts
                );


            #endregion

            #region Customized CallBizOP



            var response = new PurchaseProductForCustomerResponseDTO();
            using (var conn = RepositoryManager.GetNewConnection())
            {
                PurchaseProductForCustomerBizOp bizop = new PurchaseProductForCustomerBizOp(mockApplyRecurringCharge, mockPurchaseHelper);




                //PurchaseProductForCustomerResponseInternal responseInternal =bizop.ProcessRequest(null, new PurchaseProductForCustomerRequestInternal()
                //{
                //    AccountDefinition = actualAccount,
                //    Customer = actualCusotmerInfo,
                //    DatetimePurchase = DateTime.Now,
                //    Invoice = actualInvoice,
                //    ForceCreditLimit = null,
                //    ProductsList = productListOk,
                //    TypeOfPurchaseProductOperation = PurchaseProductForCustomerBizOp.TypeOfPurchaseProduct.PurchaseProduct,
                //    MVNO = actualDealerInfo,
                //    User = CreateDefaultObject.Create<LoginInfo>()
                //});



                response = bizop.ProcessFromCustomerModel(new NullValidator<PurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<PurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<PurchaseProductForCustomerResponseDTO>(), requestDto, FakeInvoker);

            }
            RepositoryManager.CloseConnection();


            // PurchaseProductForCustomerResponseDTO response = new PurchaseProductForCustomerResponseDTO();
            // response = CallBizOp(requestDto);


            #endregion

            #region Asserts

            // add services just added
            var initialbaseBundle = actualServicesInfos[0].CREDITLIMITBASEBUNDLEID;
            actualServicesInfos.AddRange(returnedList);
            bool respBase =actualServicesInfos.All(x => x.CREDITLIMITBASEBUNDLEID == initialbaseBundle);

            // Assert that all services have the same baseBubdle
            Assert.IsTrue(respBase);

            Assert.AreEqual(response.productPurchase.Count, listProductparam.Count);
            for (int x = 0; x < listProductparam.Count; x++)
            {
                Assert.AreEqual(response.productPurchase[x].PurchasedProductOfferingId, listProductparam[x].Id);
                Assert.AreEqual(response.productPurchase[x].ProductChargePurchasedId, listProductparam[x].PurchaseOptions[0].Id);
            }
            Assert.AreEqual(response.resultType, ResultTypes.Ok);

            // Assert all Operationexecutions have mandatory fields
            Assert.NotNull(_currentOperationExecution.Channel);
            Assert.NotNull(_currentOperationExecution.MVNO);
            Assert.NotNull(_currentOperationExecution.User);
            Assert.NotNull(_currentOperationExecution.Subscription);


            #endregion

        }






        #endregion

        #region External call: Register

        /// <summary>
        /// Positive Case All products checked OK
        /// </summary>
        [Test, TestCaseSource("testData")]
        public void PurchaseProductForCustomerBizOpTests_RegisterType_CheckProductsAllOk_Ok(List<ProductCatalogDTO> listProductparam)
        {

            #region RequestDTO

            PurchaseProductForCustomerRequestDTO requestDto = new PurchaseProductForCustomerRequestDTO
            {
                CustomerId = 1,
                forceCreditLimit = null,
                products = null,
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                channel = "TestDTOChannel"
            };

            #endregion

            #region Commons

            MockAbstractSinglePhaseOrderProcessor(requestDto);


            CommomInis();

            #endregion

            #region Setup mock Override commons

            BusinessOperationExecution _currentOperationExecution = new BusinessOperationExecution();
            var mockedRepoBusinessOperationExecution = MockRepositoryManager.GetMockedRepository<IBusinessOperationExecutionRepository<BusinessOperationExecution>>();
            mockedRepoBusinessOperationExecution.Create(Arg.Do<BusinessOperationExecution>(x => _currentOperationExecution = x));



            #endregion

            #region Assign values

            // setup
            var actualBundleInfo = CreateDefaultObject.Create<BundleInfo>();
            actualBundleInfo.EndDate = DateTime.Now.AddDays(1);


            var actualRecurringCharge = CreateDefaultObject.Create<ChargeRecurring>(1);
            actualRecurringCharge.Prices[0].EndDate = null;
            var actualNonRecurringCharge = CreateDefaultObject.Create<ChargeNonRecurring>(1);
            actualNonRecurringCharge.Prices[0].EndDate = null;
            var actualAccount = CreateDefaultObject.Create<Account>(1);
            var actualCusotmerInfo = CreateDefaultObject.Create<CustomerInfo>(1);
            actualCusotmerInfo.StatusID = 1;

            foreach (ServicesInfo item in actualCusotmerInfo.ServicesInfo)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
                item.CreditLimit = 9999;
            }

            actualNonRecurringCharge.Prices[0].EndDate = DateTime.Now.AddDays(1);

            var actualProdut = CreateDefaultObject.Create<ProductOffering>();

            // TODO: Bug in conversor, crashes if no Names.Texts or Descriptions.Texts
            actualProdut.Names.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            actualProdut.Description.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            actualProdut.ChargingOptions = new List<ProductChargeOption>();


            productListOk.Add(actualProdut);
            var actualProductCataloDTO = CreateDefaultObject.Create<ProductCatalogDTO>();
            actualProductCataloDTO.PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>();
            // productCatalogListOk.Add(actualProductCataloDTO);

            var actualProdutInfo = CreateDefaultObject.Create<ProductInfo>();
            var actualcustomeraccountAssoc = CreateDefaultObject.Create<CustomerAccountAssociation>();
            actualcustomeraccountAssoc.Account = actualAccount;

            var actualCustomerProducAssig = CreateDefaultObject.Create<CustomerProductAssignment>();
            var actualServicesInfos = new List<ServicesInfo>
            {
                CreateDefaultObject.Create<ServicesInfo>(),
                CreateDefaultObject.Create<ServicesInfo>()
            };

            foreach (ServicesInfo item in actualServicesInfos)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
            }

            // mark a master bundle
            actualServicesInfos[0].CREDITLIMITBASEBUNDLEID = (int)actualServicesInfos[0].BundleDefinition.BundleID;
            actualServicesInfos[0].CreditLimit = 9999;
            // actualCusotmerInfo.ServicesInfo = actualServicesInfos;


            var actualInvoice = CreateDefaultObject.Create<Invoice>();
            var actualInvoice2 = CreateDefaultObject.Create<Invoice>();
            var actualInvoices = new List<Invoice> { actualInvoice, actualInvoice2 };
            var actualmvnoConfig = new List<MVNOConfigActionInfo>
            {
                CreateDefaultObject.Create<MVNOConfigActionInfo>(),
                CreateDefaultObject.Create<MVNOConfigActionInfo>()
            };

            // Need to use old new approach for this entity
            var actualProductChargeOption = new ProductChargeOption();
            var actualProductChargeOption2 = new ProductChargeOption();
            actualProductChargeOption.Charges = new List<Charge> { actualNonRecurringCharge };
            actualProductChargeOption.ProductOffering = actualProdut;
            var actualProductChargeOptions = new List<ProductChargeOption> { actualProductChargeOption, actualProductChargeOption2 };


            var actualTaxDefinition = CreateDefaultObject.Create<TaxDefinition>();
            foreach (TaxRates item in actualTaxDefinition.Rates)
            {
                item.EndDate = DateTime.Now.AddDays(1);
            }
            var actualTaxDefinitions = new List<TaxDefinition>() { actualTaxDefinition };

            var actualNextBillrun = new DateTime(2015, 6, 1);

            var actualCustomerCharges = new List<CustomerCharge>()
            {
                CreateDefaultObject.Create<CustomerCharge>(),
                CreateDefaultObject.Create<CustomerCharge>()
            };
            actualCustomerCharges[0].ChargeDefinition = actualRecurringCharge;
            actualCustomerCharges[1].ChargeDefinition = actualNonRecurringCharge;


            var actualCustomerChargeeSchedule = new List<CustomerChargeSchedule>()
            {
                CreateDefaultObject.Create<CustomerChargeSchedule>(),
                CreateDefaultObject.Create<CustomerChargeSchedule>()
            };

            var actualcustomerChargeInfo1 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo1.customerChargeId = 1;
            var actualcustomerChargeInfo2 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo2.customerChargeId = 2;

            var actualpackage = CreateDefaultObject.Create<PackageInfo>();
            var actualrmPromotoinGroup = CreateDefaultObject.Create<RmPromotionGroupInfo>();
            var actualPromotoinPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            var listTupleFlattenedProducts = new List<Tuple<ProductOffering, ProductChargeOption>>();
            foreach (ProductCatalogDTO item in listProductparam)
            {
                ProductChargeOption pchargeOption = item.PurchaseOptions[0].ToCore();
                pchargeOption.Charges = new List<Charge>()
                {
                    CreateDefaultObject.Create<ChargeRecurring>(),
                    CreateDefaultObject.Create<ChargeNonRecurring>()
                };
                ProductOffering coreProduct = item.ToCore();
                coreProduct.OfferedProduct = CreateDefaultObject.Create<Product>();
                coreProduct.OfferedProduct.AssociatedBundle = actualBundleInfo;
                coreProduct.OfferedProduct.AssociatedPackage = actualpackage;
                coreProduct.OfferedProduct.AssociatedPrmotionGroup = actualrmPromotoinGroup;
                coreProduct.OfferedProduct.AssociatedPrmotionPlan = actualPromotoinPlan;
                coreProduct.OfferedProduct.VMO = actualDealerInfo;
                coreProduct.Options = new List<ProductOfferingOption>();
                listTupleFlattenedProducts.Add(Tuple.Create(coreProduct, pchargeOption));
            }

            var actualRmPromotionPlanInfos = new List<RmPromotionPlanInfo>()
            {
                CreateDefaultObject.Create<RmPromotionPlanInfo>(),
                CreateDefaultObject.Create<RmPromotionPlanInfo>()
            };

            var actualCustomerPromotionsInfos = new List<CrmCustomersPromotionInfo>()
            {
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>(),
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>()
            };


            #endregion

            #region Setup Mocks MS


            _mockupdateCustomerChargeScheduleMs.Process(Arg.Any<UpdateCustomerChargeScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new UpdateCustomerChargeScheduleResponse()
                    {
                        CustomerChargeSchedule = actualCustomerChargeeSchedule.FirstOrDefault(),
                        ResultType = ResultTypes.Ok
                    }
                );



            var checkAuthRespOk = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = true,
            };
            var checkAuthRespNok = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = false,
            };
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 1), null).Returns(checkAuthRespOk);
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 2), null).Returns(checkAuthRespNok);

            _mockcreateCustomerChargeScheduleResponseMs.Process(Arg.Any<CreateCustomerChargeScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateCustomerChargeScheduleResponse()
                    {
                        CustomerChargeSchedule = actualCustomerChargeeSchedule[0]
                    }
                );


            _mockgetRmPromotionPlanInfoMs.Process(Arg.Any<GetRmPromotionPlanInfosByIdsRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetRmPromotionPlanInfosByIdsResponse()
                    {
                        RmPromotionPlanInfos = actualRmPromotionPlanInfos

                    }
                );

            _mockgetPriorityBundleMs.Process(Arg.Any<GetPriorityBundleInfoFromBundleInfosRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetPriorityBundleInfoFromBundleInfosResponse()
                    {
                        PriorityBundle = actualBundleInfo,
                        ResultType = ResultTypes.Ok
                    }
                );




            _mockcalculateNextBillrunDateMs.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CalculateNextBillRunDateForBillCycleResponse
                    {
                        NextBillRun = actualNextBillrun
                    }
                );



            _mockgetTaxAuthorityMs.Process(Arg.Any<GetTaxAuthorityRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetTaxAuthorityResponse
                    {
                        TaxDefinitions = actualTaxDefinitions
                    }
                );



            _mockcreateServiceMs.Process(Arg.Any<CreateServicesInfoRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateServicesInfoResponse
                    {
                        ServicesInfos = actualServicesInfos[0]
                    }
                );





            _mockgetMvnoConfigActionInfosByIdAndNameMs.Process(Arg.Any<GetMVNOConfigActionInfosByIdAndNameRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetMVNOConfigActionInfosByIdAndNameResponse
                    {
                        MvnoConfigActionInfos = actualmvnoConfig
                    }
                );



            _mockgetInvoiceByLegalInvoiceNumber.Process(Arg.Any<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                     new GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse
                     {
                         Invoices = actualInvoices
                     }
                );




            _mockcreateProductInfotMs.Process(Arg.Any<CreateProductInfoRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateProductInfoResponse
                    {
                        ProductInfo = actualProdutInfo
                    }
                );



            _mockgetActiveCusotmerAccountMs.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetActiveCustomerAccountAssociationByDateResponse
                    {
                        CustomerAccountAssociation = actualcustomeraccountAssoc
                    }
                );


            _mockupdateaddChargeMS.Process(Arg.Any<AddChargeToCustomerRequest>(), Arg.Any<RequestInvokationEnvironment>()).Returns(
                new AddChargeToCustomerResponse()
                {
                    ChargeCreated = new CustomerCharge() { Amount = 1 },
                    ResultType = ResultTypes.Ok
                }
                );




            _mockupdatehasCreditMS.Process(Arg.Any<CustomerHasCreditRequest>(), Arg.Any<RequestInvokationEnvironment>()).Returns(
                new CustomerHasCreditResponse()
                {
                    HasCredit = true,
                    ResultType = ResultTypes.Ok
                }
                );




            _mockupdateaddUnbilledBalanceMS.Process(Arg.Any<AddUnbilledBalanceRequest>(), Arg.Any<RequestInvokationEnvironment>()).Returns(
                new AddUnbilledBalanceResponse()
                {
                    ResultType = ResultTypes.Ok
                }
                );


            #endregion

            #region Setup Mock BizOp

            mockAssignProductOfferingToCustomerBizOp.Process(Arg.Any<AssignProductOfferingToCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    x => new AssignProductOfferingToCustomerResponseInternal()
                    {
                        ResultType = ResultTypes.Ok,
                        productPurchased = new CustomerProductAssignment()
                        {
                            // Return values passed as parameters
                            ProductOffering = ((AssignProductOfferingToCustomerRequestInternal)x[0]).ProductOffering.Item1,
                            ProductChargePurchased = ((AssignProductOfferingToCustomerRequestInternal)x[0]).ProductOffering.Item2
                        }

                    }
                );



            mockApplyCustomerPromotionBizOP.Process(Arg.Any<ApplyCustomerPromotionRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ApplyCustomerPromotionResponseInternal()
                    {
                        ResultType = ResultTypes.Ok,
                        CrmCustomersPromotionInfos = actualCustomerPromotionsInfos,
                        Customer = actualCusotmerInfo
                    }
                );



            mockApplyChargesBizOP.Process(Arg.Any<ApplyChargeAndScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ApplyChargeAndScheduleResponse()
                    {
                        ResultType = ResultTypes.Ok,
                        ChargeAdde = actualCustomerCharges[0],
                        ScheduleAdded = actualCustomerChargeeSchedule[0]
                    }
                );


            mockCancelCustomerBizOp.Process(Arg.Any<CancelCustomerProductRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CancelCustomerProductResponseInternal()
                    {
                        ResultType = ResultTypes.Ok
                    }
                );

            mockChargeServiceBizOp.Process(Arg.Any<ChargeServiceRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ChargeServiceResponseInternal()
                    {
                        CustomerCharges = actualCustomerCharges,
                        ResultType = ResultTypes.Ok
                    }
                );


            mockCheckPurchaseProductBizOp.Process(Arg.Any<CheckPurchaseProductForCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CheckPurchaseProductForCustomerResponseInternal()
                    {
                        IsCreditEnough = true,
                        IsLimitReached = false,
                        DeprovisionList = new List<ProductOfferingSpecificationOption>(),
                        ResultType = ResultTypes.Ok
                    }
                );

            #endregion

            #region Setup Mock Third party

            mockApplyRecurringCharge.firstPeriodChargingApply(Arg.Any<chargingRequestParameters>())
                .Returns
                (
                    new chargingApplyResponse()
                    {
                        errorMsg = "",
                        flag = 0,
                        customerChargedResultList = new[] { actualcustomerChargeInfo1, actualcustomerChargeInfo2 }
                    }

                );


            mockPurchaseHelper.GetProductsAndChargesOptions(Arg.Any<List<ProductCatalogDTO>>())
                .Returns
                (
                    listTupleFlattenedProducts
                );


            #endregion

            #region Customized CallBizOP



            var responseInternalresponse = new PurchaseProductForCustomerResponseDTO();
            PurchaseProductForCustomerResponseInternal responseInternal = new PurchaseProductForCustomerResponseInternal();
            using (var conn = RepositoryManager.GetNewConnection())
            {
                PurchaseProductForCustomerBizOp bizop = new PurchaseProductForCustomerBizOp(mockApplyRecurringCharge, mockPurchaseHelper);




                responseInternal = bizop.Process(new PurchaseProductForCustomerRequestInternal()
                {
                    AccountDefinition = actualAccount,
                    Customer = actualCusotmerInfo,
                    DatetimePurchase = DateTime.Now,
                    Invoice = actualInvoice,
                    ForceCreditLimit = null,
                    listTuplePoducts = listTupleFlattenedProducts,
                    TypeOfPurchaseProductOperation = TypeOfPurchaseProduct.Register,
                    MVNO = actualDealerInfo,
                    User = CreateDefaultObject.Create<LoginInfo>()
                },null);



                //response = bizop.ProcessFromCustomerModel(new NullValidator<PurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<PurchaseProductForCustomerRequestDTO>(),
                //    new SameTypeConverter<PurchaseProductForCustomerResponseDTO>(), requestDto, FakeInvoker);

            }
            RepositoryManager.CloseConnection();


            // PurchaseProductForCustomerResponseDTO response = new PurchaseProductForCustomerResponseDTO();
            // response = CallBizOp(requestDto);


            #endregion

            #region Asserts

            Assert.AreEqual(responseInternal.productPurchaseList.Count, listTupleFlattenedProducts.Count);
            for (int x = 0; x < listTupleFlattenedProducts.Count; x++)
            {
                Assert.AreEqual(responseInternal.productPurchaseList[x].ProductOffering.ChargingOptions[0].Id
                    , listTupleFlattenedProducts.Where(list => list.Item1.Id == responseInternal.productPurchaseList[x].ProductOffering.Id)
                        .Select(pair => pair.Item2.Id).FirstOrDefault());
                //, listTupleFlattenedProducts[responseInternal.productPurchaseList[x].PurchasedProduct].Id);
            }
            Assert.AreEqual(responseInternal.ResultType, ResultTypes.Ok);

            // For now only MVNO is assigned in EmergencyTrace
            // Assert all Operationexecutions have mandatory fields
            //Assert.NotNull(_currentOperationExecution.Channel);
            Assert.NotNull(_currentOperationExecution.MVNO);
            //Assert.NotNull(_currentOperationExecution.User);
            //Assert.NotNull(_currentOperationExecution.Subscription);


            #endregion

        }
        [Test, TestCaseSource("testData")]
        public void PurchaseProductForCustomerBizOpTests_RegisterTypeWithoutSubscription_CheckProductsAllOk_Ok(List<ProductCatalogDTO> listProductparam)
        {

            #region RequestDTO

            PurchaseProductForCustomerRequestDTO requestDto = new PurchaseProductForCustomerRequestDTO
            {
                CustomerId = 1,
                forceCreditLimit = null,
                products = null,
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                channel = "TestDTOChannel",
                WithoutSubscription = true
                
            };

            #endregion

            #region Commons

            MockAbstractSinglePhaseOrderProcessor(requestDto);


            CommomInis();

            #endregion

            #region Setup mock Override commons

            BusinessOperationExecution _currentOperationExecution = new BusinessOperationExecution();
            var mockedRepoBusinessOperationExecution = MockRepositoryManager.GetMockedRepository<IBusinessOperationExecutionRepository<BusinessOperationExecution>>();
            mockedRepoBusinessOperationExecution.Create(Arg.Do<BusinessOperationExecution>(x => _currentOperationExecution = x));



            #endregion

            #region Assign values

            // setup
            var actualBundleInfo = CreateDefaultObject.Create<BundleInfo>();
            actualBundleInfo.EndDate = DateTime.Now.AddDays(1);


            var actualRecurringCharge = CreateDefaultObject.Create<ChargeRecurring>(1);
            actualRecurringCharge.Prices[0].EndDate = null;
            var actualNonRecurringCharge = CreateDefaultObject.Create<ChargeNonRecurring>(1);
            actualNonRecurringCharge.Prices[0].EndDate = null;
            var actualAccount = CreateDefaultObject.Create<Account>(1);
            var actualCusotmerInfo = CreateDefaultObject.Create<CustomerInfo>(1);
            actualCusotmerInfo.StatusID = 1;

            foreach (ServicesInfo item in actualCusotmerInfo.ServicesInfo)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
                item.CreditLimit = 9999;
            }

            actualNonRecurringCharge.Prices[0].EndDate = DateTime.Now.AddDays(1);

            var actualProdut = CreateDefaultObject.Create<ProductOffering>();

            // TODO: Bug in conversor, crashes if no Names.Texts or Descriptions.Texts
            actualProdut.Names.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            actualProdut.Description.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            actualProdut.ChargingOptions = new List<ProductChargeOption>();


            productListOk.Add(actualProdut);
            var actualProductCataloDTO = CreateDefaultObject.Create<ProductCatalogDTO>();
            actualProductCataloDTO.PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>();
            // productCatalogListOk.Add(actualProductCataloDTO);

            var actualProdutInfo = CreateDefaultObject.Create<ProductInfo>();
            var actualcustomeraccountAssoc = CreateDefaultObject.Create<CustomerAccountAssociation>();
            actualcustomeraccountAssoc.Account = actualAccount;

            var actualCustomerProducAssig = CreateDefaultObject.Create<CustomerProductAssignment>();
            var actualServicesInfos = new List<ServicesInfo>
            {
                CreateDefaultObject.Create<ServicesInfo>(),
                CreateDefaultObject.Create<ServicesInfo>()
            };

            foreach (ServicesInfo item in actualServicesInfos)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
            }

            // mark a master bundle
            actualServicesInfos[0].CREDITLIMITBASEBUNDLEID = (int)actualServicesInfos[0].BundleDefinition.BundleID;
            actualServicesInfos[0].CreditLimit = 9999;
            // actualCusotmerInfo.ServicesInfo = actualServicesInfos;


            var actualInvoice = CreateDefaultObject.Create<Invoice>();
            var actualInvoice2 = CreateDefaultObject.Create<Invoice>();
            var actualInvoices = new List<Invoice> { actualInvoice, actualInvoice2 };
            var actualmvnoConfig = new List<MVNOConfigActionInfo>
            {
                CreateDefaultObject.Create<MVNOConfigActionInfo>(),
                CreateDefaultObject.Create<MVNOConfigActionInfo>()
            };

            // Need to use old new approach for this entity
            var actualProductChargeOption = new ProductChargeOption();
            var actualProductChargeOption2 = new ProductChargeOption();
            actualProductChargeOption.Charges = new List<Charge> { actualNonRecurringCharge };
            actualProductChargeOption.ProductOffering = actualProdut;
            var actualProductChargeOptions = new List<ProductChargeOption> { actualProductChargeOption, actualProductChargeOption2 };


            var actualTaxDefinition = CreateDefaultObject.Create<TaxDefinition>();
            foreach (TaxRates item in actualTaxDefinition.Rates)
            {
                item.EndDate = DateTime.Now.AddDays(1);
            }
            var actualTaxDefinitions = new List<TaxDefinition>() { actualTaxDefinition };

            var actualNextBillrun = new DateTime(2015, 6, 1);

            var actualCustomerCharges = new List<CustomerCharge>()
            {
                CreateDefaultObject.Create<CustomerCharge>(),
                CreateDefaultObject.Create<CustomerCharge>()
            };
            actualCustomerCharges[0].ChargeDefinition = actualRecurringCharge;
            actualCustomerCharges[1].ChargeDefinition = actualNonRecurringCharge;


            var actualCustomerChargeeSchedule = new List<CustomerChargeSchedule>()
            {
                CreateDefaultObject.Create<CustomerChargeSchedule>(),
                CreateDefaultObject.Create<CustomerChargeSchedule>()
            };

            var actualcustomerChargeInfo1 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo1.customerChargeId = 1;
            var actualcustomerChargeInfo2 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo2.customerChargeId = 2;

            var actualpackage = CreateDefaultObject.Create<PackageInfo>();
            var actualrmPromotoinGroup = CreateDefaultObject.Create<RmPromotionGroupInfo>();
            var actualPromotoinPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            var listTupleFlattenedProducts = new List<Tuple<ProductOffering, ProductChargeOption>>();
            foreach (ProductCatalogDTO item in listProductparam)
            {
                ProductChargeOption pchargeOption = item.PurchaseOptions[0].ToCore();
                pchargeOption.Charges = new List<Charge>()
                {
                    CreateDefaultObject.Create<ChargeRecurring>(),
                    CreateDefaultObject.Create<ChargeNonRecurring>()
                };
                ProductOffering coreProduct = item.ToCore();
                coreProduct.OfferedProduct = CreateDefaultObject.Create<Product>();
                coreProduct.OfferedProduct.AssociatedBundle = actualBundleInfo;
                coreProduct.OfferedProduct.AssociatedPackage = actualpackage;
                coreProduct.OfferedProduct.AssociatedPrmotionGroup = actualrmPromotoinGroup;
                coreProduct.OfferedProduct.AssociatedPrmotionPlan = actualPromotoinPlan;
                coreProduct.OfferedProduct.VMO = actualDealerInfo;
                coreProduct.Options = new List<ProductOfferingOption>();
                listTupleFlattenedProducts.Add(Tuple.Create(coreProduct, pchargeOption));
            }

            var actualRmPromotionPlanInfos = new List<RmPromotionPlanInfo>()
            {
                CreateDefaultObject.Create<RmPromotionPlanInfo>(),
                CreateDefaultObject.Create<RmPromotionPlanInfo>()
            };

            var actualCustomerPromotionsInfos = new List<CrmCustomersPromotionInfo>()
            {
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>(),
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>()
            };


            #endregion

            #region Setup Mocks MS


            _mockupdateCustomerChargeScheduleMs.Process(Arg.Any<UpdateCustomerChargeScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new UpdateCustomerChargeScheduleResponse()
                    {
                        CustomerChargeSchedule = actualCustomerChargeeSchedule.FirstOrDefault(),
                        ResultType = ResultTypes.Ok
                    }
                );



            var checkAuthRespOk = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = true,
            };
            var checkAuthRespNok = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = false,
            };
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 1), null).Returns(checkAuthRespOk);
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 2), null).Returns(checkAuthRespNok);

            _mockcreateCustomerChargeScheduleResponseMs.Process(Arg.Any<CreateCustomerChargeScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateCustomerChargeScheduleResponse()
                    {
                        CustomerChargeSchedule = actualCustomerChargeeSchedule[0]
                    }
                );


            _mockgetRmPromotionPlanInfoMs.Process(Arg.Any<GetRmPromotionPlanInfosByIdsRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetRmPromotionPlanInfosByIdsResponse()
                    {
                        RmPromotionPlanInfos = actualRmPromotionPlanInfos

                    }
                );

            _mockgetPriorityBundleMs.Process(Arg.Any<GetPriorityBundleInfoFromBundleInfosRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetPriorityBundleInfoFromBundleInfosResponse()
                    {
                        PriorityBundle = actualBundleInfo,
                        ResultType = ResultTypes.Ok
                    }
                );




            _mockcalculateNextBillrunDateMs.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CalculateNextBillRunDateForBillCycleResponse
                    {
                        NextBillRun = actualNextBillrun
                    }
                );



            _mockgetTaxAuthorityMs.Process(Arg.Any<GetTaxAuthorityRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetTaxAuthorityResponse
                    {
                        TaxDefinitions = actualTaxDefinitions
                    }
                );



            _mockcreateServiceMs.Process(Arg.Any<CreateServicesInfoRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateServicesInfoResponse
                    {
                        ServicesInfos = actualServicesInfos[0]
                    }
                );




            _mockgetMvnoConfigActionInfosByIdAndNameMs.Process(Arg.Any<GetMVNOConfigActionInfosByIdAndNameRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetMVNOConfigActionInfosByIdAndNameResponse
                    {
                        MvnoConfigActionInfos = actualmvnoConfig
                    }
                );



            _mockgetInvoiceByLegalInvoiceNumber.Process(Arg.Any<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                     new GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse
                     {
                         Invoices = actualInvoices
                     }
                );




            _mockcreateProductInfotMs.Process(Arg.Any<CreateProductInfoRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateProductInfoResponse
                    {
                        ProductInfo = actualProdutInfo
                    }
                );


            _mockgetActiveCusotmerAccountMs.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetActiveCustomerAccountAssociationByDateResponse
                    {
                        CustomerAccountAssociation = actualcustomeraccountAssoc
                    }
                );


            _mockupdateaddChargeMS.Process(Arg.Any<AddChargeToCustomerRequest>(), Arg.Any<RequestInvokationEnvironment>()).Returns(
                new AddChargeToCustomerResponse()
                {
                    ChargeCreated = new CustomerCharge() { Amount = 1 },
                    ResultType = ResultTypes.Ok
                }
                );




            _mockupdatehasCreditMS.Process(Arg.Any<CustomerHasCreditRequest>(), Arg.Any<RequestInvokationEnvironment>()).Returns(
                new CustomerHasCreditResponse()
                {
                    HasCredit = true,
                    ResultType = ResultTypes.Ok
                }
                );




            _mockupdateaddUnbilledBalanceMS.Process(Arg.Any<AddUnbilledBalanceRequest>(), Arg.Any<RequestInvokationEnvironment>()).Returns(
                new AddUnbilledBalanceResponse()
                {
                    ResultType = ResultTypes.Ok
                }
                );


            #endregion

            #region Setup Mock BizOp

            mockAssignProductOfferingToCustomerBizOp.Process(Arg.Any<AssignProductOfferingToCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    x => new AssignProductOfferingToCustomerResponseInternal()
                    {
                        ResultType = ResultTypes.Ok,
                        productPurchased = new CustomerProductAssignment()
                        {
                            // Return values passed as parameters
                            ProductOffering =  ((AssignProductOfferingToCustomerRequestInternal)x[0]).ProductOffering.Item1,
                            ProductChargePurchased = ((AssignProductOfferingToCustomerRequestInternal)x[0]).ProductOffering.Item2
                        }

                    }
                );



            mockApplyCustomerPromotionBizOP.Process(Arg.Any<ApplyCustomerPromotionRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ApplyCustomerPromotionResponseInternal()
                    {
                        ResultType = ResultTypes.Ok,
                        CrmCustomersPromotionInfos = actualCustomerPromotionsInfos,
                        Customer = actualCusotmerInfo
                    }
                );



            mockApplyChargesBizOP.Process(Arg.Any<ApplyChargeAndScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ApplyChargeAndScheduleResponse()
                    {
                        ResultType = ResultTypes.Ok,
                        ChargeAdde = actualCustomerCharges[0],
                        ScheduleAdded = actualCustomerChargeeSchedule[0]
                    }
                );


            mockCancelCustomerBizOp.Process(Arg.Any<CancelCustomerProductRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CancelCustomerProductResponseInternal()
                    {
                        ResultType = ResultTypes.Ok
                    }
                );

            mockChargeServiceBizOp.Process(Arg.Any<ChargeServiceRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ChargeServiceResponseInternal()
                    {
                        CustomerCharges = actualCustomerCharges,
                        ResultType = ResultTypes.Ok
                    }
                );


            mockCheckPurchaseProductBizOp.Process(Arg.Any<CheckPurchaseProductForCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CheckPurchaseProductForCustomerResponseInternal()
                    {
                        IsCreditEnough = true,
                        IsLimitReached = false,
                        DeprovisionList = new List<ProductOfferingSpecificationOption>(),
                        ResultType = ResultTypes.Ok
                    }
                );

            #endregion

            #region Setup Mock Third party

            mockApplyRecurringCharge.firstPeriodChargingApply(Arg.Any<chargingRequestParameters>())
                .Returns
                (
                    new chargingApplyResponse()
                    {
                        errorMsg = "",
                        flag = 0,
                        customerChargedResultList = new[] { actualcustomerChargeInfo1, actualcustomerChargeInfo2 }
                    }

                );


            mockPurchaseHelper.GetProductsAndChargesOptions(Arg.Any<List<ProductCatalogDTO>>())
                .Returns
                (
                    listTupleFlattenedProducts
                );


            #endregion

            #region Customized CallBizOP



            var responseInternalresponse = new PurchaseProductForCustomerResponseDTO();
            PurchaseProductForCustomerResponseInternal responseInternal = new PurchaseProductForCustomerResponseInternal();
            using (var conn = RepositoryManager.GetNewConnection())
            {
                PurchaseProductForCustomerBizOp bizop = new PurchaseProductForCustomerBizOp(mockApplyRecurringCharge, mockPurchaseHelper);




                responseInternal = bizop.Process(new PurchaseProductForCustomerRequestInternal()
                {
                    AccountDefinition = actualAccount,
                    Customer = actualCusotmerInfo,
                    DatetimePurchase = DateTime.Now,
                    Invoice = actualInvoice,
                    ForceCreditLimit = null,
                    listTuplePoducts = listTupleFlattenedProducts,
                    TypeOfPurchaseProductOperation = TypeOfPurchaseProduct.Register,
                    MVNO = actualDealerInfo,
                    User = CreateDefaultObject.Create<LoginInfo>()
                }, null);



                //response = bizop.ProcessFromCustomerModel(new NullValidator<PurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<PurchaseProductForCustomerRequestDTO>(),
                //    new SameTypeConverter<PurchaseProductForCustomerResponseDTO>(), requestDto, FakeInvoker);

            }
            RepositoryManager.CloseConnection();


            // PurchaseProductForCustomerResponseDTO response = new PurchaseProductForCustomerResponseDTO();
            // response = CallBizOp(requestDto);


            #endregion

            #region Asserts

            Assert.AreEqual(responseInternal.productPurchaseList.Count, listTupleFlattenedProducts.Count);
            for (int x = 0; x < listTupleFlattenedProducts.Count; x++)
            {
                Assert.AreEqual(responseInternal.productPurchaseList[x].ProductOffering.ChargingOptions[0].Id
                    , listTupleFlattenedProducts.Where(list => list.Item1.Id == responseInternal.productPurchaseList[x].ProductOffering.Id)
                        .Select(pair => pair.Item2.Id).FirstOrDefault());
                //, listTupleFlattenedProducts[responseInternal.productPurchaseList[x].PurchasedProduct].Id);
            }
            Assert.AreEqual(responseInternal.ResultType, ResultTypes.Ok);

            // For now only MVNO is assigned in EmergencyTrace
            // Assert all Operationexecutions have mandatory fields
            //Assert.NotNull(_currentOperationExecution.Channel);
            Assert.NotNull(_currentOperationExecution.MVNO);
            //Assert.NotNull(_currentOperationExecution.User);
            //Assert.NotNull(_currentOperationExecution.Subscription);


            #endregion

        }




        #endregion

        #region External call: PortIn

        /// <summary>
        /// Positive Case All products checked OK
        /// </summary>
        [Test, TestCaseSource("testData")]
        public void PurchaseProductForCustomerBizOpTests_PortInType_CheckProductsAllOk_Ok(List<ProductCatalogDTO> listProductparam)
        {

            #region RequestDTO

            PurchaseProductForCustomerRequestDTO requestDto = new PurchaseProductForCustomerRequestDTO
            {
                CustomerId = 1,
                forceCreditLimit = null,
                products = null,
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                channel = "TestDTOChannel"
            };

            #endregion

            #region Commons

            MockAbstractSinglePhaseOrderProcessor(requestDto);


            CommomInis();

            #endregion

            #region Setup mock Override commons

            BusinessOperationExecution _currentOperationExecution = new BusinessOperationExecution();
            var mockedRepoBusinessOperationExecution = MockRepositoryManager.GetMockedRepository<IBusinessOperationExecutionRepository<BusinessOperationExecution>>();
            mockedRepoBusinessOperationExecution.Create(Arg.Do<BusinessOperationExecution>(x => _currentOperationExecution = x));



            #endregion

            #region Assign values

            // setup
            var actualBundleInfo = CreateDefaultObject.Create<BundleInfo>();
            actualBundleInfo.EndDate = DateTime.Now.AddDays(1);


            var actualRecurringCharge = CreateDefaultObject.Create<ChargeRecurring>(1);
            actualRecurringCharge.Prices[0].EndDate = null;
            var actualNonRecurringCharge = CreateDefaultObject.Create<ChargeNonRecurring>(1);
            actualNonRecurringCharge.Prices[0].EndDate = null;
            var actualAccount = CreateDefaultObject.Create<Account>(1);
            var actualCusotmerInfo = CreateDefaultObject.Create<CustomerInfo>(1);
            actualCusotmerInfo.StatusID = 1;

            foreach (ServicesInfo item in actualCusotmerInfo.ServicesInfo)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
                item.CreditLimit = 9999;
            }

            actualNonRecurringCharge.Prices[0].EndDate = DateTime.Now.AddDays(1);

            var actualProdut = CreateDefaultObject.Create<ProductOffering>();

            // TODO: Bug in conversor, crashes if no Names.Texts or Descriptions.Texts
            actualProdut.Names.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            actualProdut.Description.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            actualProdut.ChargingOptions = new List<ProductChargeOption>();


            productListOk.Add(actualProdut);
            var actualProductCataloDTO = CreateDefaultObject.Create<ProductCatalogDTO>();
            actualProductCataloDTO.PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>();
            // productCatalogListOk.Add(actualProductCataloDTO);

            var actualProdutInfo = CreateDefaultObject.Create<ProductInfo>();
            var actualcustomeraccountAssoc = CreateDefaultObject.Create<CustomerAccountAssociation>();
            actualcustomeraccountAssoc.Account = actualAccount;

            var actualCustomerProducAssig = CreateDefaultObject.Create<CustomerProductAssignment>();
            var actualServicesInfos = new List<ServicesInfo>
            {
                CreateDefaultObject.Create<ServicesInfo>(),
                CreateDefaultObject.Create<ServicesInfo>()
            };

            foreach (ServicesInfo item in actualServicesInfos)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
            }

            // mark a master bundle
            actualServicesInfos[0].CREDITLIMITBASEBUNDLEID = (int)actualServicesInfos[0].BundleDefinition.BundleID;
            actualServicesInfos[0].CreditLimit = 9999;
            // actualCusotmerInfo.ServicesInfo = actualServicesInfos;


            var actualInvoice = CreateDefaultObject.Create<Invoice>();
            var actualInvoice2 = CreateDefaultObject.Create<Invoice>();
            var actualInvoices = new List<Invoice> { actualInvoice, actualInvoice2 };
            var actualmvnoConfig = new List<MVNOConfigActionInfo>
            {
                CreateDefaultObject.Create<MVNOConfigActionInfo>(),
                CreateDefaultObject.Create<MVNOConfigActionInfo>()
            };

            // Need to use old new approach for this entity
            var actualProductChargeOption = new ProductChargeOption();
            var actualProductChargeOption2 = new ProductChargeOption();
            actualProductChargeOption.Charges = new List<Charge> { actualNonRecurringCharge };
            actualProductChargeOption.ProductOffering = actualProdut;
            var actualProductChargeOptions = new List<ProductChargeOption> { actualProductChargeOption, actualProductChargeOption2 };


            var actualTaxDefinition = CreateDefaultObject.Create<TaxDefinition>();
            foreach (TaxRates item in actualTaxDefinition.Rates)
            {
                item.EndDate = DateTime.Now.AddDays(1);
            }
            var actualTaxDefinitions = new List<TaxDefinition>() { actualTaxDefinition };

            var actualNextBillrun = new DateTime(2015, 6, 1);

            var actualCustomerCharges = new List<CustomerCharge>()
            {
                CreateDefaultObject.Create<CustomerCharge>(),
                CreateDefaultObject.Create<CustomerCharge>()
            };
            actualCustomerCharges[0].ChargeDefinition = actualRecurringCharge;
            actualCustomerCharges[1].ChargeDefinition = actualNonRecurringCharge;


            var actualCustomerChargeeSchedule = new List<CustomerChargeSchedule>()
            {
                CreateDefaultObject.Create<CustomerChargeSchedule>(),
                CreateDefaultObject.Create<CustomerChargeSchedule>()
            };

            var actualcustomerChargeInfo1 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo1.customerChargeId = 1;
            var actualcustomerChargeInfo2 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo2.customerChargeId = 2;

            var actualpackage = CreateDefaultObject.Create<PackageInfo>();
            var actualrmPromotoinGroup = CreateDefaultObject.Create<RmPromotionGroupInfo>();
            var actualPromotoinPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            var listTupleFlattenedProducts = new List<Tuple<ProductOffering, ProductChargeOption>>();
            foreach (ProductCatalogDTO item in listProductparam)
            {
                ProductChargeOption pchargeOption = item.PurchaseOptions[0].ToCore();
                pchargeOption.Charges = new List<Charge>()
                {
                    CreateDefaultObject.Create<ChargeRecurring>(),
                    CreateDefaultObject.Create<ChargeNonRecurring>()
                };
                ProductOffering coreProduct = item.ToCore();
                coreProduct.OfferedProduct = CreateDefaultObject.Create<Product>();
                coreProduct.OfferedProduct.AssociatedBundle = actualBundleInfo;
                coreProduct.OfferedProduct.AssociatedPackage = actualpackage;
                coreProduct.OfferedProduct.AssociatedPrmotionGroup = actualrmPromotoinGroup;
                coreProduct.OfferedProduct.AssociatedPrmotionPlan = actualPromotoinPlan;
                coreProduct.OfferedProduct.VMO = actualDealerInfo;
                coreProduct.Options = new List<ProductOfferingOption>();
                listTupleFlattenedProducts.Add(Tuple.Create(coreProduct, pchargeOption));
            }

            var actualRmPromotionPlanInfos = new List<RmPromotionPlanInfo>()
            {
                CreateDefaultObject.Create<RmPromotionPlanInfo>(),
                CreateDefaultObject.Create<RmPromotionPlanInfo>()
            };

            var actualCustomerPromotionsInfos = new List<CrmCustomersPromotionInfo>()
            {
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>(),
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>()
            };


            #endregion

            #region Setup Mocks MS

            var checkAuthRespOk = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = true,
            };
            var checkAuthRespNok = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = false,
            };
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 1), null).Returns(checkAuthRespOk);
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 2), null).Returns(checkAuthRespNok);

            _mockcreateCustomerChargeScheduleResponseMs.Process(Arg.Any<CreateCustomerChargeScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateCustomerChargeScheduleResponse()
                    {
                        CustomerChargeSchedule = actualCustomerChargeeSchedule[0]
                    }
                );


            _mockgetRmPromotionPlanInfoMs.Process(Arg.Any<GetRmPromotionPlanInfosByIdsRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetRmPromotionPlanInfosByIdsResponse()
                    {
                        RmPromotionPlanInfos = actualRmPromotionPlanInfos

                    }
                );

            _mockgetPriorityBundleMs.Process(Arg.Any<GetPriorityBundleInfoFromBundleInfosRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetPriorityBundleInfoFromBundleInfosResponse()
                    {
                        PriorityBundle = actualBundleInfo,
                        ResultType = ResultTypes.Ok
                    }
                );




            _mockcalculateNextBillrunDateMs.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CalculateNextBillRunDateForBillCycleResponse
                    {
                        NextBillRun = actualNextBillrun
                    }
                );




            _mockgetTaxAuthorityMs.Process(Arg.Any<GetTaxAuthorityRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetTaxAuthorityResponse
                    {
                        TaxDefinitions = actualTaxDefinitions
                    }
                );



            _mockcreateServiceMs.Process(Arg.Any<CreateServicesInfoRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateServicesInfoResponse
                    {
                        ServicesInfos = actualServicesInfos[0]
                    }
                );





            _mockgetMvnoConfigActionInfosByIdAndNameMs.Process(Arg.Any<GetMVNOConfigActionInfosByIdAndNameRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetMVNOConfigActionInfosByIdAndNameResponse
                    {
                        MvnoConfigActionInfos = actualmvnoConfig
                    }
                );



            _mockgetInvoiceByLegalInvoiceNumber.Process(Arg.Any<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                     new GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse
                     {
                         Invoices = actualInvoices
                     }
                );




            _mockcreateProductInfotMs.Process(Arg.Any<CreateProductInfoRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateProductInfoResponse
                    {
                        ProductInfo = actualProdutInfo
                    }
                );



            _mockgetActiveCusotmerAccountMs.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetActiveCustomerAccountAssociationByDateResponse
                    {
                        CustomerAccountAssociation = actualcustomeraccountAssoc
                    }
                );


            #endregion

            #region Setup Mock BizOp

            mockAssignProductOfferingToCustomerBizOp.Process(Arg.Any<AssignProductOfferingToCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    x => new AssignProductOfferingToCustomerResponseInternal()
                    {
                        ResultType = ResultTypes.Ok,
                        productPurchased = new CustomerProductAssignment()
                        {
                            // Return values passed as parameters
                            ProductOffering = ((AssignProductOfferingToCustomerRequestInternal)x[0]).ProductOffering.Item1,
                            ProductChargePurchased = ((AssignProductOfferingToCustomerRequestInternal)x[0]).ProductOffering.Item2
                        }

                    }
                );



            mockApplyCustomerPromotionBizOP.Process(Arg.Any<ApplyCustomerPromotionRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ApplyCustomerPromotionResponseInternal()
                    {
                        ResultType = ResultTypes.Ok,
                        CrmCustomersPromotionInfos = actualCustomerPromotionsInfos,
                        Customer = actualCusotmerInfo
                    }
                );



            mockApplyChargesBizOP.Process(Arg.Any<ApplyChargeAndScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ApplyChargeAndScheduleResponse()
                    {
                        ResultType = ResultTypes.Ok,
                        ChargeAdde = actualCustomerCharges[0],
                        ScheduleAdded = actualCustomerChargeeSchedule[0]
                    }
                );


            mockCancelCustomerBizOp.Process(Arg.Any<CancelCustomerProductRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CancelCustomerProductResponseInternal()
                    {
                        ResultType = ResultTypes.Ok
                    }
                );

            mockChargeServiceBizOp.Process(Arg.Any<ChargeServiceRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ChargeServiceResponseInternal()
                    {
                        CustomerCharges = actualCustomerCharges,
                        ResultType = ResultTypes.Ok
                    }
                );


            mockCheckPurchaseProductBizOp.Process(Arg.Any<CheckPurchaseProductForCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CheckPurchaseProductForCustomerResponseInternal()
                    {
                        IsCreditEnough = true,
                        IsLimitReached = false,
                        DeprovisionList = new List<ProductOfferingSpecificationOption>(),
                        ResultType = ResultTypes.Ok
                    }
                );

            #endregion

            #region Setup Mock Third party

            mockApplyRecurringCharge.firstPeriodChargingApply(Arg.Any<chargingRequestParameters>())
                .Returns
                (
                    new chargingApplyResponse()
                    {
                        errorMsg = "",
                        flag = 0,
                        customerChargedResultList = new[] { actualcustomerChargeInfo1, actualcustomerChargeInfo2 }
                    }

                );


            mockPurchaseHelper.GetProductsAndChargesOptions(Arg.Any<List<ProductCatalogDTO>>())
                .Returns
                (
                    listTupleFlattenedProducts
                );


            #endregion

            #region Customized CallBizOP



            var responseInternalresponse = new PurchaseProductForCustomerResponseDTO();
            PurchaseProductForCustomerResponseInternal responseInternal = new PurchaseProductForCustomerResponseInternal();
            using (var conn = RepositoryManager.GetNewConnection())
            {
                PurchaseProductForCustomerBizOp bizop = new PurchaseProductForCustomerBizOp(mockApplyRecurringCharge, mockPurchaseHelper);




                responseInternal = bizop.Process(new PurchaseProductForCustomerRequestInternal()
                {
                    AccountDefinition = actualAccount,
                    Customer = actualCusotmerInfo,
                    DatetimePurchase = DateTime.Now.AddDays(2),
                    Invoice = actualInvoice,
                    ForceCreditLimit = null,
                    listTuplePoducts = listTupleFlattenedProducts,
                    TypeOfPurchaseProductOperation = TypeOfPurchaseProduct.PortIn,
                    MVNO = actualDealerInfo,
                    User = CreateDefaultObject.Create<LoginInfo>()
                },null);



                //response = bizop.ProcessFromCustomerModel(new NullValidator<PurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<PurchaseProductForCustomerRequestDTO>(),
                //    new SameTypeConverter<PurchaseProductForCustomerResponseDTO>(), requestDto, FakeInvoker);

            }
            RepositoryManager.CloseConnection();


            // PurchaseProductForCustomerResponseDTO response = new PurchaseProductForCustomerResponseDTO();
            // response = CallBizOp(requestDto);


            #endregion

            #region Asserts

            Assert.AreEqual(responseInternal.productPurchaseList.Count, listTupleFlattenedProducts.Count);
            for (int x = 0; x < listTupleFlattenedProducts.Count; x++)
            {
                Assert.AreEqual(responseInternal.productPurchaseList[x].ProductOffering.ChargingOptions[0].Id
                    , listTupleFlattenedProducts.Where(list => list.Item1.Id == responseInternal.productPurchaseList[x].ProductOffering.Id)
                        .Select(pair => pair.Item2.Id).FirstOrDefault());
                //, listTupleFlattenedProducts[responseInternal.productPurchaseList[x].PurchasedProduct].Id);
            }
            Assert.AreEqual(responseInternal.ResultType, ResultTypes.Ok);

            // For now only MVNO is assigned in EmergencyTrace
            // Assert all Operationexecutions have mandatory fields
            //Assert.NotNull(_currentOperationExecution.Channel);
            Assert.NotNull(_currentOperationExecution.MVNO);
            //Assert.NotNull(_currentOperationExecution.User);
            //Assert.NotNull(_currentOperationExecution.Subscription);


            #endregion

        }




        #endregion

        #region External call: BenefitTransfer

        /// <summary>
        /// Positive Case All products checked OK
        /// </summary>
        [Test, TestCaseSource("testData")]
        public void PurchaseProductForCustomerBizOpTests_BenefitTransfer_CheckProductsAllOk_Ok(List<ProductCatalogDTO> listProductparam)
        {

            #region RequestDTO

            PurchaseProductForCustomerRequestDTO requestDto = new PurchaseProductForCustomerRequestDTO
            {
                CustomerId = 1,
                forceCreditLimit = null,
                products = null,
                user = "1644000204",
                password = "123456",
                vmno = "970100"
            };

            #endregion

            #region Commons

            MockAbstractSinglePhaseOrderProcessor(requestDto);


            CommomInis();

            #endregion

            #region Setup mock Override commons

            BusinessOperationExecution _currentOperationExecution = new BusinessOperationExecution();
            var mockedRepoBusinessOperationExecution = MockRepositoryManager.GetMockedRepository<IBusinessOperationExecutionRepository<BusinessOperationExecution>>();
            mockedRepoBusinessOperationExecution.Create(Arg.Do<BusinessOperationExecution>(x => _currentOperationExecution =x));
                


            #endregion

            #region Assign values

            // setup
            var actualBundleInfo = CreateDefaultObject.Create<BundleInfo>();
            actualBundleInfo.EndDate = DateTime.Now.AddDays(1);


            var actualRecurringCharge = CreateDefaultObject.Create<ChargeRecurring>(1);
            actualRecurringCharge.Prices[0].EndDate = null;
            var actualNonRecurringCharge = CreateDefaultObject.Create<ChargeNonRecurring>(1);
            actualNonRecurringCharge.Prices[0].EndDate = null;
            var actualAccount = CreateDefaultObject.Create<Account>(1);
            var actualCusotmerInfo = CreateDefaultObject.Create<CustomerInfo>(1);
            actualCusotmerInfo.StatusID = 1;

            foreach (ServicesInfo item in actualCusotmerInfo.ServicesInfo)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
                item.CreditLimit = 9999;
            }

            actualNonRecurringCharge.Prices[0].EndDate = DateTime.Now.AddDays(1);

            var actualProdut = CreateDefaultObject.Create<ProductOffering>();

            // TODO: Bug in conversor, crashes if no Names.Texts or Descriptions.Texts
            actualProdut.Names.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            actualProdut.Description.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            actualProdut.ChargingOptions = new List<ProductChargeOption>();



            productListOk.Add(actualProdut);
            var actualProductCataloDTO = CreateDefaultObject.Create<ProductCatalogDTO>();
            actualProductCataloDTO.PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>();
            // productCatalogListOk.Add(actualProductCataloDTO);

            var actualProdutInfo = CreateDefaultObject.Create<ProductInfo>();
            var actualcustomeraccountAssoc = CreateDefaultObject.Create<CustomerAccountAssociation>();
            actualcustomeraccountAssoc.Account = actualAccount;

            var actualCustomerProducAssig = CreateDefaultObject.Create<CustomerProductAssignment>();
            var actualServicesInfos = new List<ServicesInfo>
            {
                CreateDefaultObject.Create<ServicesInfo>(),
                CreateDefaultObject.Create<ServicesInfo>()
            };

            foreach (ServicesInfo item in actualServicesInfos)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
            }

            // mark a master bundle
            actualServicesInfos[0].CREDITLIMITBASEBUNDLEID = (int)actualServicesInfos[0].BundleDefinition.BundleID;
            actualServicesInfos[0].CreditLimit = 9999;
            // actualCusotmerInfo.ServicesInfo = actualServicesInfos;


            var actualInvoice = CreateDefaultObject.Create<Invoice>();
            var actualInvoice2 = CreateDefaultObject.Create<Invoice>();
            var actualInvoices = new List<Invoice> { actualInvoice, actualInvoice2 };
            var actualmvnoConfig = new List<MVNOConfigActionInfo>
            {
                CreateDefaultObject.Create<MVNOConfigActionInfo>(),
                CreateDefaultObject.Create<MVNOConfigActionInfo>()
            };

            // Need to use old new approach for this entity
            var actualProductChargeOption = new ProductChargeOption();
            var actualProductChargeOption2 = new ProductChargeOption();
            actualProductChargeOption.Charges = new List<Charge> { actualNonRecurringCharge };
            actualProductChargeOption.ProductOffering = actualProdut;
            var actualProductChargeOptions = new List<ProductChargeOption> { actualProductChargeOption, actualProductChargeOption2 };


            var actualTaxDefinition = CreateDefaultObject.Create<TaxDefinition>();
            foreach (TaxRates item in actualTaxDefinition.Rates)
            {
                item.EndDate = DateTime.Now.AddDays(1);
            }
            var actualTaxDefinitions = new List<TaxDefinition>() { actualTaxDefinition };

            var actualNextBillrun = new DateTime(2015, 6, 1);

            var actualCustomerCharges = new List<CustomerCharge>()
            {
                CreateDefaultObject.Create<CustomerCharge>(),
                CreateDefaultObject.Create<CustomerCharge>()
            };
            actualCustomerCharges[0].ChargeDefinition = actualRecurringCharge;
            actualCustomerCharges[1].ChargeDefinition = actualNonRecurringCharge;


            var actualCustomerChargeeSchedule = new List<CustomerChargeSchedule>()
            {
                CreateDefaultObject.Create<CustomerChargeSchedule>(),
                CreateDefaultObject.Create<CustomerChargeSchedule>()
            };

            var actualcustomerChargeInfo1 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo1.customerChargeId = 1;
            var actualcustomerChargeInfo2 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo2.customerChargeId = 2;

            var actualpackage = CreateDefaultObject.Create<PackageInfo>();
            var actualrmPromotoinGroup = CreateDefaultObject.Create<RmPromotionGroupInfo>();
            var actualPromotoinPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            var listTupleFlattenedProducts = new List<Tuple<ProductOffering, ProductChargeOption>>();
            foreach (ProductCatalogDTO item in listProductparam)
            {
                ProductChargeOption pchargeOption = item.PurchaseOptions[0].ToCore();
                pchargeOption.Charges = new List<Charge>()
                {
                    CreateDefaultObject.Create<ChargeRecurring>(),
                    CreateDefaultObject.Create<ChargeNonRecurring>()
                };
                ProductOffering coreProduct = item.ToCore();
                coreProduct.OfferedProduct = CreateDefaultObject.Create<Product>();
                coreProduct.OfferedProduct.AssociatedBundle = actualBundleInfo;
                coreProduct.OfferedProduct.AssociatedPackage = actualpackage;
                coreProduct.OfferedProduct.AssociatedPrmotionGroup = actualrmPromotoinGroup;
                coreProduct.OfferedProduct.AssociatedPrmotionPlan = actualPromotoinPlan;
                coreProduct.OfferedProduct.VMO = actualDealerInfo;
                coreProduct.Options = new List<ProductOfferingOption>();
                listTupleFlattenedProducts.Add(Tuple.Create(coreProduct, pchargeOption));
            }

            var actualRmPromotionPlanInfos = new List<RmPromotionPlanInfo>()
            {
                CreateDefaultObject.Create<RmPromotionPlanInfo>(),
                CreateDefaultObject.Create<RmPromotionPlanInfo>()
            };

            var actualCustomerPromotionsInfos = new List<CrmCustomersPromotionInfo>()
            {
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>(),
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>()
            };


            #endregion

            #region Setup Mocks MS

            var checkAuthRespOk = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = true,
            };
            var checkAuthRespNok = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = false,
            };
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 1), null).Returns(checkAuthRespOk);
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 2), null).Returns(checkAuthRespNok);

            _mockcreateCustomerChargeScheduleResponseMs.Process(Arg.Any<CreateCustomerChargeScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateCustomerChargeScheduleResponse()
                    {
                        CustomerChargeSchedule = actualCustomerChargeeSchedule[0]
                    }
                );


            _mockgetRmPromotionPlanInfoMs.Process(Arg.Any<GetRmPromotionPlanInfosByIdsRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetRmPromotionPlanInfosByIdsResponse()
                    {
                        RmPromotionPlanInfos = actualRmPromotionPlanInfos

                    }
                );

            _mockgetPriorityBundleMs.Process(Arg.Any<GetPriorityBundleInfoFromBundleInfosRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetPriorityBundleInfoFromBundleInfosResponse()
                    {
                        PriorityBundle = actualBundleInfo,
                        ResultType = ResultTypes.Ok
                    }
                );




            _mockcalculateNextBillrunDateMs.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CalculateNextBillRunDateForBillCycleResponse
                    {
                        NextBillRun = actualNextBillrun
                    }
                );




            _mockgetTaxAuthorityMs.Process(Arg.Any<GetTaxAuthorityRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetTaxAuthorityResponse
                    {
                        TaxDefinitions = actualTaxDefinitions
                    }
                );



            _mockcreateServiceMs.Process(Arg.Any<CreateServicesInfoRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateServicesInfoResponse
                    {
                        ServicesInfos = actualServicesInfos[0]
                    }
                );






            _mockgetMvnoConfigActionInfosByIdAndNameMs.Process(Arg.Any<GetMVNOConfigActionInfosByIdAndNameRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetMVNOConfigActionInfosByIdAndNameResponse
                    {
                        MvnoConfigActionInfos = actualmvnoConfig
                    }
                );



            _mockgetInvoiceByLegalInvoiceNumber.Process(Arg.Any<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                     new GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse
                     {
                         Invoices = actualInvoices
                     }
                );




            _mockcreateProductInfotMs.Process(Arg.Any<CreateProductInfoRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateProductInfoResponse
                    {
                        ProductInfo = actualProdutInfo
                    }
                );


            _mockgetActiveCusotmerAccountMs.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetActiveCustomerAccountAssociationByDateResponse
                    {
                        CustomerAccountAssociation = actualcustomeraccountAssoc
                    }
                );


            #endregion

            #region Setup Mock BizOp

            mockAssignProductOfferingToCustomerBizOp.Process(Arg.Any<AssignProductOfferingToCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    x => new AssignProductOfferingToCustomerResponseInternal()
                    {
                        ResultType = ResultTypes.Ok,
                        productPurchased = new CustomerProductAssignment()
                        {
                            // Return values passed as parameters
                            ProductOffering = ((AssignProductOfferingToCustomerRequestInternal)x[0]).ProductOffering.Item1,
                            ProductChargePurchased = ((AssignProductOfferingToCustomerRequestInternal)x[0]).ProductOffering.Item2
                        }

                    }
                );



            mockApplyCustomerPromotionBizOP.Process(Arg.Any<ApplyCustomerPromotionRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                     new ApplyCustomerPromotionResponseInternal()
                        {
                            ResultType = ResultTypes.Ok,
                            CrmCustomersPromotionInfos = actualCustomerPromotionsInfos,
                            Customer = actualCusotmerInfo
                        }
                    
                );




            int callApplyChargesBizOPCount = 0;
            mockApplyChargesBizOP.Process(Arg.Any<ApplyChargeAndScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    x =>
                    {
                        var response = new ApplyChargeAndScheduleResponse()
                        {
                            ResultType = ResultTypes.Ok,
                            ChargeAdde = actualCustomerCharges[0],
                            ScheduleAdded = actualCustomerChargeeSchedule[0]
                        };

                        callApplyChargesBizOPCount ++;
                        return response;
                    }
                );


            mockCancelCustomerBizOp.Process(Arg.Any<CancelCustomerProductRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CancelCustomerProductResponseInternal()
                    {
                        ResultType = ResultTypes.Ok
                    }
                );

            mockChargeServiceBizOp.Process(Arg.Any<ChargeServiceRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ChargeServiceResponseInternal()
                    {
                        CustomerCharges = actualCustomerCharges,
                        ResultType = ResultTypes.Ok
                    }
                );


            mockCheckPurchaseProductBizOp.Process(Arg.Any<CheckPurchaseProductForCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CheckPurchaseProductForCustomerResponseInternal()
                    {
                        IsCreditEnough = true,
                        IsLimitReached = false,
                        DeprovisionList = new List<ProductOfferingSpecificationOption>(),
                        ResultType = ResultTypes.Ok
                    }
                );

            #endregion

            #region Setup Mock Third party

            mockApplyRecurringCharge.firstPeriodChargingApply(Arg.Any<chargingRequestParameters>())
                .Returns
                (
                    new chargingApplyResponse()
                    {
                        errorMsg = "",
                        flag = 0,
                        customerChargedResultList = new[] { actualcustomerChargeInfo1, actualcustomerChargeInfo2 }
                    }

                );


            mockPurchaseHelper.GetProductsAndChargesOptions(Arg.Any<List<ProductCatalogDTO>>())
                .Returns
                (
                    listTupleFlattenedProducts
                );


            #endregion

            #region Customized CallBizOP



            var responseInternalresponse = new PurchaseProductForCustomerResponseDTO();
            PurchaseProductForCustomerResponseInternal responseInternal = new PurchaseProductForCustomerResponseInternal();
            using (var conn = RepositoryManager.GetNewConnection())
            {
                PurchaseProductForCustomerBizOp bizop = new PurchaseProductForCustomerBizOp(mockApplyRecurringCharge, mockPurchaseHelper);


                responseInternal =bizop.Process(new PurchaseProductForCustomerRequestInternal()
                {
                    AccountDefinition = actualAccount,
                    Customer = actualCusotmerInfo,
                    DatetimePurchase = DateTime.Now.AddDays(2),
                    Invoice = actualInvoice,
                    ForceCreditLimit = null,
                    listTuplePoducts = listTupleFlattenedProducts,
                    TypeOfPurchaseProductOperation = TypeOfPurchaseProduct.BenefitTransfer,
                    MVNO = actualDealerInfo,
                    User = CreateDefaultObject.Create<LoginInfo>(),
                    Channel = "TestChannel"
                }, null);



                //response = bizop.ProcessFromCustomerModel(new NullValidator<PurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<PurchaseProductForCustomerRequestDTO>(),
                //    new SameTypeConverter<PurchaseProductForCustomerResponseDTO>(), requestDto, FakeInvoker);

            }
            RepositoryManager.CloseConnection();


            // PurchaseProductForCustomerResponseDTO response = new PurchaseProductForCustomerResponseDTO();
            // response = CallBizOp(requestDto);


            #endregion

            #region Asserts

            Assert.AreEqual(responseInternal.productPurchaseList.Count, listTupleFlattenedProducts.Count);
            for (int x = 0; x < listTupleFlattenedProducts.Count; x++)
            {
                Assert.AreEqual(responseInternal.productPurchaseList[x].ProductOffering.ChargingOptions[0].Id
                    , listTupleFlattenedProducts.Where(list => list.Item1.Id == responseInternal.productPurchaseList[x].ProductOffering.Id)
                        .Select(pair => pair.Item2.Id).FirstOrDefault());
                //, listTupleFlattenedProducts[responseInternal.productPurchaseList[x].PurchasedProduct].Id);
            }
            Assert.AreEqual(responseInternal.ResultType, ResultTypes.Ok);
            Assert.IsTrue(callApplyChargesBizOPCount == 0);

            // Assert all Operationexecutions have mandatory fields
            Assert.NotNull(_currentOperationExecution.Channel);
            Assert.NotNull(_currentOperationExecution.MVNO);
            Assert.NotNull(_currentOperationExecution.User);
            Assert.NotNull(_currentOperationExecution.Subscription);

            

            #endregion

        }




        #endregion

        #region Product from another MVNO
        /// <summary>
        /// Positive Case All products checked OK
        /// </summary>
        [Test, TestCaseSource("testData1product")]
        public void PurchaseProductForCustomerBizOpTests_CheckAuthorizationProduct_NOk(List<ProductCatalogDTO> listProductparam)
        {


            #region RequestDTO

            PurchaseProductForCustomerRequestDTO requestDto = new PurchaseProductForCustomerRequestDTO
            {
                CustomerId = 100,
                forceCreditLimit = null,
                products = listProductparam,
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                channel = "TestDTOChannel"
            };

            #endregion

            #region Commons

            MockAbstractSinglePhaseOrderProcessor(requestDto);


            CommomInis();

            #endregion

            #region Setup mock Override commons

            BusinessOperationExecution _currentOperationExecution = new BusinessOperationExecution();
            var mockedRepoBusinessOperationExecution = MockRepositoryManager.GetMockedRepository<IBusinessOperationExecutionRepository<BusinessOperationExecution>>();
            mockedRepoBusinessOperationExecution.Create(Arg.Do<BusinessOperationExecution>(x => _currentOperationExecution = x));



            #endregion

            #region Assign values

            // setup
            var actualBundleInfo = CreateDefaultObject.Create<BundleInfo>();
            actualBundleInfo.EndDate = DateTime.Now.AddDays(1);


            var actualCharge = CreateDefaultObject.Create<ChargeNonRecurring>(1);
            var actualAccount = CreateDefaultObject.Create<Account>(1);
            var actualCusotmerInfo = CreateDefaultObject.Create<CustomerInfo>(1);
            actualCusotmerInfo.StatusID = 1;

            foreach (ServicesInfo item in actualCusotmerInfo.ServicesInfo)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
                item.CreditLimit = 9999;
            }

            actualCharge.Prices[0].EndDate = DateTime.Now.AddDays(1);

            var actualProdut = CreateDefaultObject.Create<ProductOffering>();

            // TODO: Bug in conversor, crashes if no Names.Texts or Descriptions.Texts
            actualProdut.Names.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            actualProdut.Description.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            actualProdut.ChargingOptions = new List<ProductChargeOption>();


            productListOk.Add(actualProdut);
            var actualProductCataloDTO = CreateDefaultObject.Create<ProductCatalogDTO>();
            actualProductCataloDTO.PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>();
            // productCatalogListOk.Add(actualProductCataloDTO);

            var actualProdutInfo = CreateDefaultObject.Create<ProductInfo>();
            var actualcustomeraccountAssoc = CreateDefaultObject.Create<CustomerAccountAssociation>();
            actualcustomeraccountAssoc.Account = actualAccount;

            var actualCustomerProducAssig = CreateDefaultObject.Create<CustomerProductAssignment>();
            var actualServicesInfos = new List<ServicesInfo>
            {
                CreateDefaultObject.Create<ServicesInfo>(),
                CreateDefaultObject.Create<ServicesInfo>()
            };

            foreach (ServicesInfo item in actualServicesInfos)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
            }

            // mark a master bundle
            actualServicesInfos[0].CREDITLIMITBASEBUNDLEID = (int)actualServicesInfos[0].BundleDefinition.BundleID;
            actualServicesInfos[0].CreditLimit = 9999;
            // actualCusotmerInfo.ServicesInfo = actualServicesInfos;


            var actualInvoice = CreateDefaultObject.Create<Invoice>();
            var actualInvoice2 = CreateDefaultObject.Create<Invoice>();
            var actualInvoices = new List<Invoice> { actualInvoice, actualInvoice2 };
            var actualmvnoConfig = new List<MVNOConfigActionInfo>
            {
                CreateDefaultObject.Create<MVNOConfigActionInfo>(),
                CreateDefaultObject.Create<MVNOConfigActionInfo>()
            };

            // Need to use old new approach for this entity
            var actualProductChargeOption = new ProductChargeOption();
            var actualProductChargeOption2 = new ProductChargeOption();
            actualProductChargeOption.Charges = new List<Charge> { actualCharge };
            actualProductChargeOption.ProductOffering = actualProdut;
            var actualProductChargeOptions = new List<ProductChargeOption> { actualProductChargeOption, actualProductChargeOption2 };



            var actualTaxDefinition = CreateDefaultObject.Create<TaxDefinition>();
            foreach (TaxRates item in actualTaxDefinition.Rates)
            {
                item.EndDate = DateTime.Now.AddDays(1);
            }
            var actualTaxDefinitions = new List<TaxDefinition>() { actualTaxDefinition };

            var actualNextBillrun = new DateTime(2015, 6, 1);

            var actualCustomerCharges = new List<CustomerCharge>()
            {
                CreateDefaultObject.Create<CustomerCharge>(),
                CreateDefaultObject.Create<CustomerCharge>()
            };

            var actualCustomerChargeeSchedule = new List<CustomerChargeSchedule>()
            {
                CreateDefaultObject.Create<CustomerChargeSchedule>(),
                CreateDefaultObject.Create<CustomerChargeSchedule>()
            };

            var actualcustomerChargeInfo1 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo1.customerChargeId = 1;
            var actualcustomerChargeInfo2 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo2.customerChargeId = 2;

            var actualpackage = CreateDefaultObject.Create<PackageInfo>();
            var actualrmPromotoinGroup = CreateDefaultObject.Create<RmPromotionGroupInfo>();
            var actualPromotoinPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            var listTupleFlattenedProducts = new List<Tuple<ProductOffering, ProductChargeOption>>();
            foreach (ProductCatalogDTO item in listProductparam)
            {
                ProductChargeOption pchargeOption = item.PurchaseOptions[0].ToCore();
                pchargeOption.Charges = new List<Charge>()
                {
                    CreateDefaultObject.Create<ChargeRecurring>(),
                    CreateDefaultObject.Create<ChargeNonRecurring>()
                };
                ProductOffering coreProduct = item.ToCore();
                coreProduct.OfferedProduct = CreateDefaultObject.Create<Product>();
                coreProduct.OfferedProduct.AssociatedBundle = actualBundleInfo;
                coreProduct.OfferedProduct.AssociatedPackage = actualpackage;
                coreProduct.OfferedProduct.AssociatedPrmotionGroup = actualrmPromotoinGroup;
                coreProduct.OfferedProduct.AssociatedPrmotionPlan = actualPromotoinPlan;
                coreProduct.OfferedProduct.VMO = actualDealerInfo;
                coreProduct.Options = new List<ProductOfferingOption>();
                listTupleFlattenedProducts.Add(Tuple.Create(coreProduct, pchargeOption));
            }

            var actualRmPromotionPlanInfos = new List<RmPromotionPlanInfo>()
            {
                CreateDefaultObject.Create<RmPromotionPlanInfo>(),
                CreateDefaultObject.Create<RmPromotionPlanInfo>()
            };

            var actualCustomerPromotionsInfos = new List<CrmCustomersPromotionInfo>()
            {
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>(),
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>()
            };


            #endregion

            #region Setup Mocks MS

            var checkAuthRespOk = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = true,
            };
            var checkAuthRespNok = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = false,
            };
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 1), null).Returns(checkAuthRespOk);
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 2), null).Returns(checkAuthRespNok);

            _mockcreateCustomerChargeScheduleResponseMs.Process(Arg.Any<CreateCustomerChargeScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateCustomerChargeScheduleResponse()
                    {
                        CustomerChargeSchedule = actualCustomerChargeeSchedule[0]
                    }
                );



            _mockgetRmPromotionPlanInfoMs.Process(Arg.Any<GetRmPromotionPlanInfosByIdsRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetRmPromotionPlanInfosByIdsResponse()
                    {
                        RmPromotionPlanInfos = actualRmPromotionPlanInfos

                    }
                );

            _mockgetPriorityBundleMs.Process(Arg.Any<GetPriorityBundleInfoFromBundleInfosRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetPriorityBundleInfoFromBundleInfosResponse()
                    {
                        PriorityBundle = actualBundleInfo,
                        ResultType = ResultTypes.Ok
                    }
                );


            _mockcalculateNextBillrunDateMs.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CalculateNextBillRunDateForBillCycleResponse
                    {
                        NextBillRun = actualNextBillrun
                    }
                );




            _mockgetTaxAuthorityMs.Process(Arg.Any<GetTaxAuthorityRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetTaxAuthorityResponse
                    {
                        TaxDefinitions = actualTaxDefinitions
                    }
                );



            _mockcreateServiceMs.Process(Arg.Any<CreateServicesInfoRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateServicesInfoResponse
                    {
                        ServicesInfos = actualServicesInfos[0]
                    }
                );

      



            _mockgetMvnoConfigActionInfosByIdAndNameMs.Process(Arg.Any<GetMVNOConfigActionInfosByIdAndNameRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetMVNOConfigActionInfosByIdAndNameResponse
                    {
                        MvnoConfigActionInfos = actualmvnoConfig
                    }
                );



            _mockgetInvoiceByLegalInvoiceNumber.Process(Arg.Any<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                     new GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse
                     {
                         Invoices = actualInvoices
                     }
                );




            _mockcreateProductInfotMs.Process(Arg.Any<CreateProductInfoRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateProductInfoResponse
                    {
                        ProductInfo = actualProdutInfo
                    }
                );



            _mockgetActiveCusotmerAccountMs.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetActiveCustomerAccountAssociationByDateResponse
                    {
                        CustomerAccountAssociation = actualcustomeraccountAssoc
                    }
                );

            var actualExistingCustomerProductAssignment = CreateDefaultObject.Create<CustomerProductAssignment>();
            actualExistingCustomerProductAssignment.EndDate = null;
            actualExistingCustomerProductAssignment.PurchasedProduct.Id = actualCustomerProducAssig.PurchasedProduct.Id;


            #endregion

            #region Setup Mock BizOp

            mockAssignProductOfferingToCustomerBizOp.Process(Arg.Any<AssignProductOfferingToCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    x => new AssignProductOfferingToCustomerResponseInternal()
                    {
                        ResultType = ResultTypes.Ok,
                        productPurchased = new CustomerProductAssignment()
                        {
                            // Return values passed as parameters
                            ProductOffering = ((AssignProductOfferingToCustomerRequestInternal)x[0]).ProductOffering.Item1,
                            ProductChargePurchased = ((AssignProductOfferingToCustomerRequestInternal)x[0]).ProductOffering.Item2
                        }

                    }
                );



            mockApplyCustomerPromotionBizOP.Process(Arg.Any<ApplyCustomerPromotionRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ApplyCustomerPromotionResponseInternal()
                    {
                        ResultType = ResultTypes.Ok,
                        CrmCustomersPromotionInfos = actualCustomerPromotionsInfos,
                        Customer = actualCusotmerInfo
                    }
                );



            mockApplyChargesBizOP.Process(Arg.Any<ApplyChargeAndScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ApplyChargeAndScheduleResponse()
                    {
                        ResultType = ResultTypes.Ok,
                        ChargeAdde = actualCustomerCharges[0],
                        ScheduleAdded = actualCustomerChargeeSchedule[0]
                    }
                );


            mockCancelCustomerBizOp.Process(Arg.Any<CancelCustomerProductRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CancelCustomerProductResponseInternal()
                    {
                        ResultType = ResultTypes.Ok
                    }
                );

            mockChargeServiceBizOp.Process(Arg.Any<ChargeServiceRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ChargeServiceResponseInternal()
                    {
                        CustomerCharges = actualCustomerCharges,
                        ResultType = ResultTypes.Ok
                    }
                );


            mockCheckPurchaseProductBizOp.Process(Arg.Any<CheckPurchaseProductForCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CheckPurchaseProductForCustomerResponseInternal()
                    {
                        IsCreditEnough = true,
                        IsLimitReached = false,
                        DeprovisionList = new List<ProductOfferingSpecificationOption>(),
                        ResultType = ResultTypes.Ok
                    }
                );

            #endregion

            #region Setup Mock Third party

            mockApplyRecurringCharge.firstPeriodChargingApply(Arg.Any<chargingRequestParameters>())
                .Returns
                (
                    new chargingApplyResponse()
                    {
                        errorMsg = "",
                        flag = 0,
                        customerChargedResultList = new[] { actualcustomerChargeInfo1, actualcustomerChargeInfo2 }
                    }

                );

            //Set the first product to another Dealer to force the Exception
            var wrongDealer = CreateDefaultObject.Create<DealerInfo>();
            wrongDealer.DealerID = DealerId + 1;
            wrongDealer.Email = "testDealer";
            listTupleFlattenedProducts.First().Item1.OfferedProduct.VMO = wrongDealer;
            mockPurchaseHelper.GetProductsAndChargesOptions(Arg.Any<List<ProductCatalogDTO>>())
                .Returns
                (
                    listTupleFlattenedProducts
                );


            #endregion

            #region Customized CallBizOP



            var response = new PurchaseProductForCustomerResponseDTO();
            using (var conn = RepositoryManager.GetNewConnection())
            {
                PurchaseProductForCustomerBizOp bizop = new PurchaseProductForCustomerBizOp(mockApplyRecurringCharge, mockPurchaseHelper);

                response = bizop.ProcessFromCustomerModel(new NullValidator<PurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<PurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<PurchaseProductForCustomerResponseDTO>(), requestDto, FakeInvoker);

            }
            RepositoryManager.CloseConnection();


            // PurchaseProductForCustomerResponseDTO response = new PurchaseProductForCustomerResponseDTO();
            // response = CallBizOp(requestDto);


            #endregion

            #region Asserts
            Assert.AreEqual(response.resultType, ResultTypes.AuthorizationError);
            #endregion

        }
        #endregion

        #region Product from another MVNO
        /// <summary>
        /// Positive Case All products checked OK
        /// </summary>
        [Test, TestCaseSource("testData1product")]
        public void PurchaseProductForCustomerBizOpTests_CheckAuthorizationCustomer_NOk(List<ProductCatalogDTO> listProductparam)
        {
            #region RequestDTO

            PurchaseProductForCustomerRequestDTO requestDto = new PurchaseProductForCustomerRequestDTO
            {
                CustomerId = 100,
                forceCreditLimit = null,
                products = listProductparam,
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                channel = "TestDTOChannel"
            };

            #endregion

            #region Commons

            MockAbstractSinglePhaseOrderProcessor(requestDto);


            CommomInis();

            #endregion

            #region Setup mock Override commons

            BusinessOperationExecution _currentOperationExecution = new BusinessOperationExecution();
            var mockedRepoBusinessOperationExecution = MockRepositoryManager.GetMockedRepository<IBusinessOperationExecutionRepository<BusinessOperationExecution>>();
            mockedRepoBusinessOperationExecution.Create(Arg.Do<BusinessOperationExecution>(x => _currentOperationExecution = x));



            #endregion

            #region Assign values

            // setup
            var actualBundleInfo = CreateDefaultObject.Create<BundleInfo>();
            actualBundleInfo.EndDate = DateTime.Now.AddDays(1);


            var actualCharge = CreateDefaultObject.Create<ChargeNonRecurring>(1);
            var actualAccount = CreateDefaultObject.Create<Account>(1);
            var actualCusotmerInfo = CreateDefaultObject.Create<CustomerInfo>(1);
            actualCusotmerInfo.StatusID = 1;

            foreach (ServicesInfo item in actualCusotmerInfo.ServicesInfo)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
                item.CreditLimit = 9999;
            }

            actualCharge.Prices[0].EndDate = DateTime.Now.AddDays(1);

            var actualProdut = CreateDefaultObject.Create<ProductOffering>();

            // TODO: Bug in conversor, crashes if no Names.Texts or Descriptions.Texts
            actualProdut.Names.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            actualProdut.Description.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            actualProdut.ChargingOptions = new List<ProductChargeOption>();



            productListOk.Add(actualProdut);
            var actualProductCataloDTO = CreateDefaultObject.Create<ProductCatalogDTO>();
            actualProductCataloDTO.PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>();
            // productCatalogListOk.Add(actualProductCataloDTO);

            var actualProdutInfo = CreateDefaultObject.Create<ProductInfo>();
            var actualcustomeraccountAssoc = CreateDefaultObject.Create<CustomerAccountAssociation>();
            actualcustomeraccountAssoc.Account = actualAccount;

            var actualCustomerProducAssig = CreateDefaultObject.Create<CustomerProductAssignment>();
            var actualServicesInfos = new List<ServicesInfo>
            {
                CreateDefaultObject.Create<ServicesInfo>(),
                CreateDefaultObject.Create<ServicesInfo>()
            };

            foreach (ServicesInfo item in actualServicesInfos)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
            }

            // mark a master bundle
            actualServicesInfos[0].CREDITLIMITBASEBUNDLEID = (int)actualServicesInfos[0].BundleDefinition.BundleID;
            actualServicesInfos[0].CreditLimit = 9999;
            // actualCusotmerInfo.ServicesInfo = actualServicesInfos;


            var actualInvoice = CreateDefaultObject.Create<Invoice>();
            var actualInvoice2 = CreateDefaultObject.Create<Invoice>();
            var actualInvoices = new List<Invoice> { actualInvoice, actualInvoice2 };
            var actualmvnoConfig = new List<MVNOConfigActionInfo>
            {
                CreateDefaultObject.Create<MVNOConfigActionInfo>(),
                CreateDefaultObject.Create<MVNOConfigActionInfo>()
            };

            // Need to use old new approach for this entity
            var actualProductChargeOption = new ProductChargeOption();
            var actualProductChargeOption2 = new ProductChargeOption();
            actualProductChargeOption.Charges = new List<Charge> { actualCharge };
            actualProductChargeOption.ProductOffering = actualProdut;
            var actualProductChargeOptions = new List<ProductChargeOption> { actualProductChargeOption, actualProductChargeOption2 };



            var actualTaxDefinition = CreateDefaultObject.Create<TaxDefinition>();
            foreach (TaxRates item in actualTaxDefinition.Rates)
            {
                item.EndDate = DateTime.Now.AddDays(1);
            }
            var actualTaxDefinitions = new List<TaxDefinition>() { actualTaxDefinition };

            var actualNextBillrun = new DateTime(2015, 6, 1);

            var actualCustomerCharges = new List<CustomerCharge>()
            {
                CreateDefaultObject.Create<CustomerCharge>(),
                CreateDefaultObject.Create<CustomerCharge>()
            };

            var actualCustomerChargeeSchedule = new List<CustomerChargeSchedule>()
            {
                CreateDefaultObject.Create<CustomerChargeSchedule>(),
                CreateDefaultObject.Create<CustomerChargeSchedule>()
            };

            var actualcustomerChargeInfo1 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo1.customerChargeId = 1;
            var actualcustomerChargeInfo2 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo2.customerChargeId = 2;

            var actualpackage = CreateDefaultObject.Create<PackageInfo>();
            var actualrmPromotoinGroup = CreateDefaultObject.Create<RmPromotionGroupInfo>();
            var actualPromotoinPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            var listTupleFlattenedProducts = new List<Tuple<ProductOffering, ProductChargeOption>>();
            foreach (ProductCatalogDTO item in listProductparam)
            {
                ProductChargeOption pchargeOption = item.PurchaseOptions[0].ToCore();
                pchargeOption.Charges = new List<Charge>()
                {
                    CreateDefaultObject.Create<ChargeRecurring>(),
                    CreateDefaultObject.Create<ChargeNonRecurring>()
                };
                ProductOffering coreProduct = item.ToCore();
                coreProduct.OfferedProduct = CreateDefaultObject.Create<Product>();
                coreProduct.OfferedProduct.AssociatedBundle = actualBundleInfo;
                coreProduct.OfferedProduct.AssociatedPackage = actualpackage;
                coreProduct.OfferedProduct.AssociatedPrmotionGroup = actualrmPromotoinGroup;
                coreProduct.OfferedProduct.AssociatedPrmotionPlan = actualPromotoinPlan;
                coreProduct.OfferedProduct.VMO = actualDealerInfo;
                coreProduct.Options = new List<ProductOfferingOption>();
                listTupleFlattenedProducts.Add(Tuple.Create(coreProduct, pchargeOption));
            }

            var actualRmPromotionPlanInfos = new List<RmPromotionPlanInfo>()
            {
                CreateDefaultObject.Create<RmPromotionPlanInfo>(),
                CreateDefaultObject.Create<RmPromotionPlanInfo>()
            };

            var actualCustomerPromotionsInfos = new List<CrmCustomersPromotionInfo>()
            {
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>(),
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>()
            };


            #endregion

            #region Setup Mocks MS


            _mockupdateCustomerChargeScheduleMs.Process(Arg.Any<UpdateCustomerChargeScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new UpdateCustomerChargeScheduleResponse()
                    {
                        CustomerChargeSchedule = actualCustomerChargeeSchedule.FirstOrDefault(),
                        ResultType = ResultTypes.Ok
                    }
                );


            var mockGetCustomerById = MockMicroService<GetCustomerInfoByIdRequest, GetCustomerInfoByIdResponse>();

            var wrongCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            wrongCustomerInfo.DealerID = DealerId + 1;
            mockGetCustomerById.Process(Arg.Any<GetCustomerInfoByIdRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns(new GetCustomerInfoByIdResponse()
                {
                    CustomerInfo = wrongCustomerInfo,
                });

            var checkAuthRespOk = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = true,
            };
            var checkAuthRespNok = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = false,
            };
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 2), null).Returns(checkAuthRespOk);
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 1), null).Returns(checkAuthRespNok);

            _mockcreateCustomerChargeScheduleResponseMs.Process(Arg.Any<CreateCustomerChargeScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateCustomerChargeScheduleResponse()
                    {
                        CustomerChargeSchedule = actualCustomerChargeeSchedule[0]
                    }
                );



            _mockgetRmPromotionPlanInfoMs.Process(Arg.Any<GetRmPromotionPlanInfosByIdsRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetRmPromotionPlanInfosByIdsResponse()
                    {
                        RmPromotionPlanInfos = actualRmPromotionPlanInfos

                    }
                );

            _mockgetPriorityBundleMs.Process(Arg.Any<GetPriorityBundleInfoFromBundleInfosRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetPriorityBundleInfoFromBundleInfosResponse()
                    {
                        PriorityBundle = actualBundleInfo,
                        ResultType = ResultTypes.Ok
                    }
                );


            _mockcalculateNextBillrunDateMs.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CalculateNextBillRunDateForBillCycleResponse
                    {
                        NextBillRun = actualNextBillrun
                    }
                );




            _mockgetTaxAuthorityMs.Process(Arg.Any<GetTaxAuthorityRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetTaxAuthorityResponse
                    {
                        TaxDefinitions = actualTaxDefinitions
                    }
                );



            _mockcreateServiceMs.Process(Arg.Any<CreateServicesInfoRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateServicesInfoResponse
                    {
                        ServicesInfos = actualServicesInfos[0]
                    }
                );





            _mockgetMvnoConfigActionInfosByIdAndNameMs.Process(Arg.Any<GetMVNOConfigActionInfosByIdAndNameRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetMVNOConfigActionInfosByIdAndNameResponse
                    {
                        MvnoConfigActionInfos = actualmvnoConfig
                    }
                );



            _mockgetInvoiceByLegalInvoiceNumber.Process(Arg.Any<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                     new GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse
                     {
                         Invoices = actualInvoices
                     }
                );




            _mockcreateProductInfotMs.Process(Arg.Any<CreateProductInfoRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateProductInfoResponse
                    {
                        ProductInfo = actualProdutInfo
                    }
                );


            _mockgetActiveCusotmerAccountMs.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetActiveCustomerAccountAssociationByDateResponse
                    {
                        CustomerAccountAssociation = actualcustomeraccountAssoc
                    }
                );

            var actualExistingCustomerProductAssignment = CreateDefaultObject.Create<CustomerProductAssignment>();
            actualExistingCustomerProductAssignment.EndDate = null;
            actualExistingCustomerProductAssignment.PurchasedProduct.Id = actualCustomerProducAssig.PurchasedProduct.Id;


            #endregion

            #region Setup Mock BizOp

            mockAssignProductOfferingToCustomerBizOp.Process(Arg.Any<AssignProductOfferingToCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    x => new AssignProductOfferingToCustomerResponseInternal()
                    {
                        ResultType = ResultTypes.Ok,
                        productPurchased = new CustomerProductAssignment()
                        {
                            // Return values passed as parameters
                            ProductOffering = ((AssignProductOfferingToCustomerRequestInternal)x[0]).ProductOffering.Item1,
                            ProductChargePurchased = ((AssignProductOfferingToCustomerRequestInternal)x[0]).ProductOffering.Item2
                        }

                    }
                );



            mockApplyCustomerPromotionBizOP.Process(Arg.Any<ApplyCustomerPromotionRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ApplyCustomerPromotionResponseInternal()
                    {
                        ResultType = ResultTypes.Ok,
                        CrmCustomersPromotionInfos = actualCustomerPromotionsInfos,
                        Customer = actualCusotmerInfo
                    }
                );



            mockApplyChargesBizOP.Process(Arg.Any<ApplyChargeAndScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ApplyChargeAndScheduleResponse()
                    {
                        ResultType = ResultTypes.Ok,
                        ChargeAdde = actualCustomerCharges[0],
                        ScheduleAdded = actualCustomerChargeeSchedule[0]
                    }
                );


            mockCancelCustomerBizOp.Process(Arg.Any<CancelCustomerProductRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CancelCustomerProductResponseInternal()
                    {
                        ResultType = ResultTypes.Ok
                    }
                );

            mockChargeServiceBizOp.Process(Arg.Any<ChargeServiceRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ChargeServiceResponseInternal()
                    {
                        CustomerCharges = actualCustomerCharges,
                        ResultType = ResultTypes.Ok
                    }
                );


            mockCheckPurchaseProductBizOp.Process(Arg.Any<CheckPurchaseProductForCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CheckPurchaseProductForCustomerResponseInternal()
                    {
                        IsCreditEnough = true,
                        IsLimitReached = false,
                        DeprovisionList = new List<ProductOfferingSpecificationOption>(),
                        ResultType = ResultTypes.Ok
                    }
                );

            #endregion

            #region Setup Mock Third party

            mockApplyRecurringCharge.firstPeriodChargingApply(Arg.Any<chargingRequestParameters>())
                .Returns
                (
                    new chargingApplyResponse()
                    {
                        errorMsg = "",
                        flag = 0,
                        customerChargedResultList = new[] { actualcustomerChargeInfo1, actualcustomerChargeInfo2 }
                    }

                );

            mockPurchaseHelper.GetProductsAndChargesOptions(Arg.Any<List<ProductCatalogDTO>>())
                .Returns
                (
                    listTupleFlattenedProducts
                );


            #endregion

            #region Customized CallBizOP



            var response = new PurchaseProductForCustomerResponseDTO();
            using (var conn = RepositoryManager.GetNewConnection())
            {
                PurchaseProductForCustomerBizOp bizop = new PurchaseProductForCustomerBizOp(mockApplyRecurringCharge, mockPurchaseHelper);

                response = bizop.ProcessFromCustomerModel(new NullValidator<PurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<PurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<PurchaseProductForCustomerResponseDTO>(), requestDto, FakeInvoker);

            }
            RepositoryManager.CloseConnection();
            #endregion

            #region Asserts
            Assert.AreEqual(response.resultType, ResultTypes.AuthorizationError);


            #endregion

        }
        #endregion

        #region TestMapOutbound

        [Test, TestCaseSource("testData1product")]
        public void PurchaseProductForCustomerBizOpTests_TestMapOutboundISubscriptionBased_Ok(List<ProductCatalogDTO> listProductparam)
        {


            #region RequestDTO

            var actualPurchaseProductForCustomerRequestDTO = new PurchaseProductForCustomerRequestDTO
            {
                CustomerId = 9,
                forceCreditLimit = null,
                products = listProductparam,
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                channel = "TestDTOChannel"
            };

            #endregion

            #region Commons

            MockAbstractSinglePhaseOrderProcessor(actualPurchaseProductForCustomerRequestDTO);


            CommomInis();

            #endregion

            #region Setup mock Override commons

            BusinessOperationExecution _currentOperationExecution = new BusinessOperationExecution();
            var mockedRepoBusinessOperationExecution = MockRepositoryManager.GetMockedRepository<IBusinessOperationExecutionRepository<BusinessOperationExecution>>();
            mockedRepoBusinessOperationExecution.Create(Arg.Do<BusinessOperationExecution>(x => _currentOperationExecution = x));



            #endregion

            #region Find out BizOP OperationID

            //var bizopID = new PurchaseProductForCustomerBizOp();
            //var a = bizopID.Id;

            //int operationId = bizopID.OperationCode.GetHashCode();

            #endregion

            #region Assign values

            // setup
            var actualBundleInfo = CreateDefaultObject.Create<BundleInfo>();
            actualBundleInfo.EndDate = DateTime.Now.AddDays(1);


            var actualCharge = CreateDefaultObject.Create<ChargeNonRecurring>(1);
            var actualAccount = CreateDefaultObject.Create<Account>(1);
            var actualCusotmerInfo = CreateDefaultObject.Create<CustomerInfo>(1);
            actualCusotmerInfo.StatusID = 1;
            actualCusotmerInfo.CustomerID = 9;
            actualCusotmerInfo.ResourceMBInfo.FirstOrDefault().CustomerInfo = actualCusotmerInfo;

            foreach (ServicesInfo item in actualCusotmerInfo.ServicesInfo)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
                item.CreditLimit = 9999;
                item.CREDITLIMITBASEBUNDLEID = 1;
            }

            actualCharge.Prices[0].EndDate = DateTime.Now.AddDays(1);

            var actualProdut = CreateDefaultObject.Create<ProductOffering>();

            // TODO: Bug in conversor, crashes if no Names.Texts or Descriptions.Texts
            actualProdut.Names.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            actualProdut.Description.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            actualProdut.ChargingOptions = new List<ProductChargeOption>();


            productListOk.Add(actualProdut);
            var actualProductCataloDTO = CreateDefaultObject.Create<ProductCatalogDTO>();
            actualProductCataloDTO.PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>();
            // productCatalogListOk.Add(actualProductCataloDTO);

            var actualProdutInfo = CreateDefaultObject.Create<ProductInfo>();
            var actualcustomeraccountAssoc = CreateDefaultObject.Create<CustomerAccountAssociation>();
            actualcustomeraccountAssoc.Account = actualAccount;

            var actualCustomerProducAssig = CreateDefaultObject.Create<CustomerProductAssignment>();
            var actualServicesInfos = new List<ServicesInfo>
            {
                CreateDefaultObject.Create<ServicesInfo>(),
                CreateDefaultObject.Create<ServicesInfo>()
            };

            foreach (ServicesInfo item in actualServicesInfos)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
            }

            // mark a master bundle
            actualServicesInfos[0].CREDITLIMITBASEBUNDLEID = (int)actualServicesInfos[0].BundleDefinition.BundleID;
            actualServicesInfos[0].CreditLimit = 9999;
            // actualCusotmerInfo.ServicesInfo = actualServicesInfos;


            var actualInvoice = CreateDefaultObject.Create<Invoice>();
            var actualInvoice2 = CreateDefaultObject.Create<Invoice>();
            var actualInvoices = new List<Invoice> { actualInvoice, actualInvoice2 };
            var actualmvnoConfig = new List<MVNOConfigActionInfo>
            {
                CreateDefaultObject.Create<MVNOConfigActionInfo>(),
                CreateDefaultObject.Create<MVNOConfigActionInfo>()
            };

            // Need to use old new approach for this entity
            var actualProductChargeOption = new ProductChargeOption();
            var actualProductChargeOption2 = new ProductChargeOption();
            actualProductChargeOption.Charges = new List<Charge> { actualCharge };
            actualProductChargeOption.ProductOffering = actualProdut;
            var actualProductChargeOptions = new List<ProductChargeOption> { actualProductChargeOption, actualProductChargeOption2 };



            var actualTaxDefinition = CreateDefaultObject.Create<TaxDefinition>();
            foreach (TaxRates item in actualTaxDefinition.Rates)
            {
                item.EndDate = DateTime.Now.AddDays(1);
            }
            var actualTaxDefinitions = new List<TaxDefinition>() { actualTaxDefinition };

            var actualNextBillrun = new DateTime(2015, 6, 1);

            var actualCustomerCharges = new List<CustomerCharge>()
            {
                CreateDefaultObject.Create<CustomerCharge>(),
                CreateDefaultObject.Create<CustomerCharge>()
            };

            var actualCustomerChargeeSchedule = new List<CustomerChargeSchedule>()
            {
                CreateDefaultObject.Create<CustomerChargeSchedule>(),
                CreateDefaultObject.Create<CustomerChargeSchedule>()
            };

            var actualcustomerChargeInfo1 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo1.customerChargeId = 1;
            var actualcustomerChargeInfo2 = CreateDefaultObject.Create<customerChargeInfo>();
            actualcustomerChargeInfo2.customerChargeId = 2;

            var actualpackage = CreateDefaultObject.Create<PackageInfo>();
            var actualrmPromotoinGroup = CreateDefaultObject.Create<RmPromotionGroupInfo>();
            var actualPromotoinPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            var listTupleFlattenedProducts = new List<Tuple<ProductOffering, ProductChargeOption>>();
            foreach (ProductCatalogDTO item in listProductparam)
            {
                ProductChargeOption pchargeOption = item.PurchaseOptions[0].ToCore();
                pchargeOption.Charges = new List<Charge>()
                {
                    CreateDefaultObject.Create<ChargeRecurring>(),
                    CreateDefaultObject.Create<ChargeNonRecurring>()
                };
                ProductOffering coreProduct = item.ToCore();
                coreProduct.OfferedProduct = CreateDefaultObject.Create<Product>();
                coreProduct.OfferedProduct.AssociatedBundle = actualBundleInfo;
                coreProduct.OfferedProduct.AssociatedPackage = actualpackage;
                coreProduct.OfferedProduct.AssociatedPrmotionGroup = actualrmPromotoinGroup;
                coreProduct.OfferedProduct.AssociatedPrmotionPlan = actualPromotoinPlan;
                coreProduct.OfferedProduct.VMO = actualDealerInfo;
                coreProduct.Options = new List<ProductOfferingOption>();
                listTupleFlattenedProducts.Add(Tuple.Create(coreProduct, pchargeOption));
            }

            var actualRmPromotionPlanInfos = new List<RmPromotionPlanInfo>()
            {
                CreateDefaultObject.Create<RmPromotionPlanInfo>(),
                CreateDefaultObject.Create<RmPromotionPlanInfo>()
            };

            var actualCustomerPromotionsInfos = new List<CrmCustomersPromotionInfo>()
            {
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>(),
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>()
            };

            var mockedCustomer = CreateDefaultObject.Create<CustomerInfo>();
            mockedCustomer.CustomerID = 9;
            mockedCustomer.ResourceMBInfo.FirstOrDefault().CustomerInfo = mockedCustomer;
            foreach (ServicesInfo item in mockedCustomer.ServicesInfo)
            {
                item.BundleDefinition = actualBundleInfo;
                item.EndDate = null;
                item.CreditLimit = 9999;
                item.CREDITLIMITBASEBUNDLEID = 1;
            } 


            #endregion

            #region Setup Mocks MS
            var checkAuthRespOk = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = true,
            };
            var checkAuthRespNok = new CheckAuthorizationResponse()
            {
                ResultType = ResultTypes.Ok,
                IsAuthorized = false,
            };
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 1), null).Returns(checkAuthRespOk);
            _mockCheckAuthorizationMs.Process(Arg.Is<CheckAuthorizationRequest>(x => x.DealerId == 2), null).Returns(checkAuthRespNok);

            _mockcreateCustomerChargeScheduleResponseMs.Process(Arg.Any<CreateCustomerChargeScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateCustomerChargeScheduleResponse()
                    {
                        CustomerChargeSchedule = actualCustomerChargeeSchedule[0]
                    }
                );



            _mockgetRmPromotionPlanInfoMs.Process(Arg.Any<GetRmPromotionPlanInfosByIdsRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetRmPromotionPlanInfosByIdsResponse()
                    {
                        RmPromotionPlanInfos = actualRmPromotionPlanInfos

                    }
                );

            _mockgetPriorityBundleMs.Process(Arg.Any<GetPriorityBundleInfoFromBundleInfosRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetPriorityBundleInfoFromBundleInfosResponse()
                    {
                        PriorityBundle = actualBundleInfo,
                        ResultType = ResultTypes.Ok
                    }
                );




            _mockcalculateNextBillrunDateMs.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CalculateNextBillRunDateForBillCycleResponse
                    {
                        NextBillRun = actualNextBillrun
                    }
                );



            _mockgetTaxAuthorityMs.Process(Arg.Any<GetTaxAuthorityRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetTaxAuthorityResponse
                    {
                        TaxDefinitions = actualTaxDefinitions
                    }
                );



            _mockcreateServiceMs.Process(Arg.Any<CreateServicesInfoRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateServicesInfoResponse
                    {
                        ServicesInfos = actualServicesInfos[0]
                    }
                );


     






            _mockgetMvnoConfigActionInfosByIdAndNameMs.Process(Arg.Any<GetMVNOConfigActionInfosByIdAndNameRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetMVNOConfigActionInfosByIdAndNameResponse
                    {
                        MvnoConfigActionInfos = actualmvnoConfig
                    }
                );



            _mockgetInvoiceByLegalInvoiceNumber.Process(Arg.Any<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                     new GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse
                     {
                         Invoices = actualInvoices
                     }
                );




            _mockcreateProductInfotMs.Process(Arg.Any<CreateProductInfoRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CreateProductInfoResponse
                    {
                        ProductInfo = actualProdutInfo
                    }
                );



            _mockgetActiveCusotmerAccountMs.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetActiveCustomerAccountAssociationByDateResponse
                    {
                        CustomerAccountAssociation = actualcustomeraccountAssoc
                    }
                );

            var actualExistingCustomerProductAssignment = CreateDefaultObject.Create<CustomerProductAssignment>();
            actualExistingCustomerProductAssignment.EndDate = null;
            actualExistingCustomerProductAssignment.PurchasedProduct.Id = actualCustomerProducAssig.PurchasedProduct.Id;


            #endregion

            #region Setup Mock BizOp

            
            mockCustomerRepo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            mockCustomerRepo.GetById(Arg.Any<int>()).Returns(mockedCustomer);

            mockAssignProductOfferingToCustomerBizOp.Process(Arg.Any<AssignProductOfferingToCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    x => new AssignProductOfferingToCustomerResponseInternal()
                    {
                        ResultType = ResultTypes.Ok,
                        productPurchased = new CustomerProductAssignment()
                        {
                            // Return values passed as parameters
                            ProductOffering = ((AssignProductOfferingToCustomerRequestInternal)x[0]).ProductOffering.Item1,
                            ProductChargePurchased = ((AssignProductOfferingToCustomerRequestInternal)x[0]).ProductOffering.Item2
                        }

                    }
                );


            mockApplyCustomerPromotionBizOP.Process(Arg.Any<ApplyCustomerPromotionRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ApplyCustomerPromotionResponseInternal()
                    {
                        ResultType = ResultTypes.Ok,
                        CrmCustomersPromotionInfos = actualCustomerPromotionsInfos,
                        Customer = actualCusotmerInfo
                    }
                );



            mockApplyChargesBizOP.Process(Arg.Any<ApplyChargeAndScheduleRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ApplyChargeAndScheduleResponse()
                    {
                        ResultType = ResultTypes.Ok,
                        ChargeAdde = actualCustomerCharges[0],
                        ScheduleAdded = actualCustomerChargeeSchedule[0]
                    }
                );


            mockCancelCustomerBizOp.Process(Arg.Any<CancelCustomerProductRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CancelCustomerProductResponseInternal()
                    {
                        ResultType = ResultTypes.Ok
                    }
                );

            mockChargeServiceBizOp.Process(Arg.Any<ChargeServiceRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new ChargeServiceResponseInternal()
                    {
                        CustomerCharges = actualCustomerCharges,
                        ResultType = ResultTypes.Ok
                    }
                );


            mockCheckPurchaseProductBizOp.Process(Arg.Any<CheckPurchaseProductForCustomerRequestInternal>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new CheckPurchaseProductForCustomerResponseInternal()
                    {
                        IsCreditEnough = true,
                        IsLimitReached = false,
                        DeprovisionList = new List<ProductOfferingSpecificationOption>(),
                        ResultType = ResultTypes.Ok
                    }
                );

            #endregion

            #region Setup Mock Third party

            mockApplyRecurringCharge.firstPeriodChargingApply(Arg.Any<chargingRequestParameters>())
                .Returns
                (
                    new chargingApplyResponse()
                    {
                        errorMsg = "",
                        flag = 0,
                        customerChargedResultList = new[] { actualcustomerChargeInfo1, actualcustomerChargeInfo2 }
                    }

                );


            mockPurchaseHelper.GetProductsAndChargesOptions(Arg.Any<List<ProductCatalogDTO>>())
                .Returns
                (
                    listTupleFlattenedProducts
                );


            #endregion

            #region Customized CallBizOP



            var actualPurchaseProductForCustomerResponseDTO = new PurchaseProductForCustomerResponseDTO();
            using (var conn = RepositoryManager.GetNewConnection())
            {
                PurchaseProductForCustomerBizOp bizop = new PurchaseProductForCustomerBizOp(mockApplyRecurringCharge, mockPurchaseHelper);
                actualPurchaseProductForCustomerResponseDTO = bizop.ProcessFromCustomerModel(new NullValidator<PurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<PurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<PurchaseProductForCustomerResponseDTO>(), actualPurchaseProductForCustomerRequestDTO, FakeInvoker);

            }
            RepositoryManager.CloseConnection();


            // PurchaseProductForCustomerResponseDTO response = new PurchaseProductForCustomerResponseDTO();
            // response = CallBizOp(requestDto);


            #endregion

            #region Asserts

            var expectedPurchaseProductForCustomerResponseDTO = new PurchaseProductForCustomerResponseDTO{Subscription = new SubscriptionDTO{CustomerId = 9}};

            Assert.IsTrue(actualPurchaseProductForCustomerResponseDTO.Subscription.CustomerId ==
                          expectedPurchaseProductForCustomerResponseDTO.Subscription.CustomerId);

            // Assert all Operationexecutions have mandatory fields
            Assert.NotNull(_currentOperationExecution.Channel);
            Assert.NotNull(_currentOperationExecution.MVNO);
            Assert.NotNull(_currentOperationExecution.User);
            Assert.NotNull(_currentOperationExecution.Subscription);


            #endregion

        }
        #endregion
    }
}
