using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.fullfilment.CheckPurchaseProductForCustomer;
using com.etak.core.bizops.helper;
using com.etak.core.customer.message.GetActiveCustomerAccountAssociationByDate;
using com.etak.core.dealer.messages.GetMVNOConfigActionInfosByIdAndName;
using com.etak.core.GSMSubscription.messages.CheckProductListDependencyRelationsForCustomer;
using com.etak.core.GSMSubscription.messages.GetCustomerProductAssignmentsByCustomerId;
using com.etak.core.microservices.messages.GetTaxDefinitonsByCategory;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using com.etak.core.operation.implementation;
using com.etak.core.product.message.CalculateNextBillRunDateForBillCycle;
using com.etak.core.product.message.GetProductByProductId;
using com.etak.core.product.message.GetProductOfferingByProductOfferingId;
using com.etak.core.repository;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.subscription.catalog;
using com.etak.core.service.messages.GetPriorityBundleInfoFromBundleInfos;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.IntTests.fullfilment
{
    [TestFixture]
    public class CheckPurchaseProductForCustomerBizOpIntTests : AbstractBusinessOperationTest<CheckPurchaseProductForCustomerBizOp, CheckPurchaseProductForCustomerRequestDTO,
                CheckPurchaseProductForCustomerResponseDTO, CheckPurchaseProductForCustomerRequestInternal, CheckPurchaseProductForCustomerResponseInternal>
    {
        private DateTime _startDate = DateTime.Now.AddMonths(-3);
        private DateTime _purchaseDate = DateTime.Now.AddMonths(-3);
        private DateTime _endDate = DateTime.Now.AddMonths(3);
        private DateTime _nextBillRunStartDate;
        private DateTime _nextBillRunEndDate = DateTime.Now.AddMonths(2);
        private List<Tuple<ProductOffering, ProductChargeOption>> _actualListTuplesPurchaseProducts = new List<Tuple<ProductOffering, ProductChargeOption>>();
        private PurchaseHelper _mockedPurchaseHelper = CreateDefaultObject.Create<PurchaseHelper>();
        private ProductOfferingSpecificationOption _actualProductDependencyRelation;
        private List<ProductCatalogDTO> _actualProductCatalogDtOs = new List<ProductCatalogDTO> { CreateDefaultObject.Create<ProductCatalogDTO>() };
        private ProductOffering _actualProductOffering;
        private Product _actualProductOrig;
        private RmPromotionPlanDetailInfo _actualRmPromotionPlanDetailInfo;
        private RmPromotionPlanDetailInfo _actualRmPromotionPlanDetailInfo2;
        private RmPromotionPlanDetailInfo _actualRmPromotionPlanDetailInfo3;
        private ProductOffering _actualCommercialProduct;
        private ProductChargeOption _actualProductChargeOption;
        private ChargeRecurring _actualChargeRecurring;
        private ChargePrice _actualChargePrice;
        private ChargePrice _actualChargePrice2;
        private ProductChargeOption _actualCommercialProductChargeOption;
        private CustomerInfo _actualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
        private ResourceMBInfo _actualResourceMbInfo;
        private ServicesInfo _serviceInfo;
        private CustomerProductAssignment _customerProductAssn;
        private CustomerProductAssignment _customerProductAssn2;
        private CrmCustomersPromotionInfo _crmCustomersPromotionInfoNextBillRunStartDate;
        private CrmCustomersPromotionInfo _crmCustomersPromotionInfoNotActive;
        private CrmCustomersPromotionInfo _crmCustomersPromotionInfo;
        private BundleInfo _actualBundleInfo;




        private void StandardMocks()
        {
            #region Define standard objects
            _nextBillRunStartDate = DateTime.Now.AddMonths(1);
            _serviceInfo = CreateDefaultObject.Create<ServicesInfo>();
            _crmCustomersPromotionInfoNextBillRunStartDate = CreateDefaultObject.Create<CrmCustomersPromotionInfo>();
            _crmCustomersPromotionInfoNotActive = CreateDefaultObject.Create<CrmCustomersPromotionInfo>();
            _crmCustomersPromotionInfo = CreateDefaultObject.Create<CrmCustomersPromotionInfo>();
            _actualProductDependencyRelation = CreateDefaultObject.Create<ProductOfferingSpecificationOption>();
            _actualProductDependencyRelation.RelatedProductOffering = CreateDefaultObject.Create<ProductOffering>();
            _actualProductDependencyRelation.SpecifiedProductOffering = CreateDefaultObject.Create<ProductOffering>();
            _actualProductDependencyRelation.SpecifiedProductOffering.OfferedProduct = CreateDefaultObject.Create<Product>();
            _actualProductDependencyRelation.SpecifiedProductOffering.OfferedProduct.AssociatedPrmotionPlan =
                CreateDefaultObject.Create<RmPromotionPlanInfo>();
            _actualListTuplesPurchaseProducts = new List<Tuple<ProductOffering, ProductChargeOption>>();
            _actualProductOrig = CreateDefaultObject.Create<Product>();
            _actualProductOrig.Type = CreateDefaultObject.Create<ProductType>();
            _actualProductOrig.Type.Description = "Commercial";

            _actualProductOffering = CreateDefaultObject.Create<ProductOffering>();
            _actualProductOffering.OfferedProduct = _actualProductOrig;
            _actualProductOffering.OfferedProduct.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();

            _customerProductAssn = CreateDefaultObject.Create<CustomerProductAssignment>();
            _customerProductAssn.ProductOffering = _actualProductOffering;
            _customerProductAssn2 = CreateDefaultObject.Create<CustomerProductAssignment>();
            _customerProductAssn2.ProductOffering = _actualProductOffering;

            _actualRmPromotionPlanDetailInfo = CreateDefaultObject.Create<RmPromotionPlanDetailInfo>();
            _actualRmPromotionPlanDetailInfo.StartDate = _startDate;
            _actualRmPromotionPlanDetailInfo.EndDate = _endDate;
            _actualRmPromotionPlanDetailInfo.SubServiceTypeId = 1;

            _actualRmPromotionPlanDetailInfo2 = CreateDefaultObject.Create<RmPromotionPlanDetailInfo>();
            _actualRmPromotionPlanDetailInfo2.Limit = 20;
            _actualRmPromotionPlanDetailInfo2.StartDate = _startDate;
            _actualRmPromotionPlanDetailInfo2.EndDate = _endDate;
            _actualRmPromotionPlanDetailInfo2.SubServiceTypeId = 20;

            _actualRmPromotionPlanDetailInfo3 = CreateDefaultObject.Create<RmPromotionPlanDetailInfo>();
            _actualRmPromotionPlanDetailInfo3.Limit = 20;
            _actualRmPromotionPlanDetailInfo3.StartDate = _nextBillRunStartDate;
            _actualRmPromotionPlanDetailInfo3.EndDate = _endDate;
            _actualRmPromotionPlanDetailInfo3.SubServiceTypeId = 1;

            _actualProductOrig.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList = new List<RmPromotionPlanDetailInfo>() { _actualRmPromotionPlanDetailInfo3, _actualRmPromotionPlanDetailInfo, _actualRmPromotionPlanDetailInfo2 };

            _actualCommercialProduct = CreateDefaultObject.Create<ProductOffering>();
            _actualCommercialProduct.Id = 2;

            _actualProductOrig.Type = CreateDefaultObject.Create<ProductType>();
            _actualProductOrig.Type.Description = "Commercial";

            _actualProductChargeOption = CreateDefaultObject.Create<ProductChargeOption>();
            _actualChargeRecurring = CreateDefaultObject.Create<ChargeRecurring>();

            _actualChargePrice = CreateDefaultObject.Create<ChargePrice>();
            _actualChargePrice.StartDate = _startDate;
            _actualChargePrice.EndDate = _endDate;
            _actualChargePrice.Amount = 100;

            _actualChargePrice2 = CreateDefaultObject.Create<ChargePrice>();
            _actualChargePrice2.StartDate = _startDate;
            _actualChargePrice2.EndDate = _endDate;
            _actualChargePrice2.Amount = 100;

            _actualChargeRecurring.Prices.Add(_actualChargePrice);
            _actualChargeRecurring.Prices.Add(_actualChargePrice2);

            _actualProductChargeOption.Charges = new List<Charge>() { _actualChargeRecurring };
            _actualProductChargeOption.StartDate = _startDate;
            _actualProductChargeOption.EndDate = _endDate;

            _actualCommercialProductChargeOption = CreateDefaultObject.Create<ProductChargeOption>();
            _actualCommercialProductChargeOption.Charges = new List<Charge>() { _actualChargeRecurring };
            _actualCommercialProductChargeOption.StartDate = _startDate;
            _actualCommercialProductChargeOption.EndDate = _endDate;

            _actualListTuplesPurchaseProducts.Add(Tuple.Create(_actualProductOffering, _actualProductChargeOption));
            _actualListTuplesPurchaseProducts.Add(Tuple.Create(_actualCommercialProduct, _actualCommercialProductChargeOption));

            _actualResourceMbInfo = CreateDefaultObject.Create<ResourceMBInfo>();
            _actualResourceMbInfo.CustomerInfo = _actualCustomerInfo;
            _actualCustomerInfo.ResourceMBInfo.Clear();
            _actualCustomerInfo.ResourceMBInfo.Add(_actualResourceMbInfo);

            _actualBundleInfo = CreateDefaultObject.Create<BundleInfo>();
            _actualBundleInfo.CreditLimit = 300;

            #endregion
            #region Mocked getActiveCusotmerAccountMs
            var mockedGetActiveCusotmerAccountMs = MockMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();
            mockedGetActiveCusotmerAccountMs.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(), null)
               .Returns(new GetActiveCustomerAccountAssociationByDateResponse()
               {
                   CustomerAccountAssociation = CreateDefaultObject.Create<CustomerAccountAssociation>(),
                   ResultType = ResultTypes.Ok,

               });
            #endregion
            #region Mocked GetMVNOConfigActionInfosByIdAndNameMS
            var mockedGetMVNOConfigActionInfosByIdAndNameMS = MockMicroService<GetMVNOConfigActionInfosByIdAndNameRequest, GetMVNOConfigActionInfosByIdAndNameResponse>();
            var actualGetMVNOConfigActionInfosByIdAndNameRequest =
                Arg.Is<GetMVNOConfigActionInfosByIdAndNameRequest>(x => x.MvnoId == 1 && x.CategoryName == "PURCHASE_PRODUCT_LIMIT_PROMOTION");
            var actualMVNOConfigActionInfo = CreateDefaultObject.Create<MVNOConfigActionInfo>();
            actualMVNOConfigActionInfo.BAK1 = "1";
            actualMVNOConfigActionInfo.Value = "200";

            var actualMVNOConfigActionInfo2 = CreateDefaultObject.Create<MVNOConfigActionInfo>();
            actualMVNOConfigActionInfo2.BAK1 = "20";
            actualMVNOConfigActionInfo2.Value = "500";

            var actualGetMVNOConfigActionInfosByIdAndNameResponse = new GetMVNOConfigActionInfosByIdAndNameResponse()
            {
                MvnoConfigActionInfos = new List<MVNOConfigActionInfo>() { actualMVNOConfigActionInfo, actualMVNOConfigActionInfo2 },
                ResultType = ResultTypes.Ok,
            };
            mockedGetMVNOConfigActionInfosByIdAndNameMS.Process(actualGetMVNOConfigActionInfosByIdAndNameRequest, null)
                .Returns(actualGetMVNOConfigActionInfosByIdAndNameResponse);

            var actualGetMVNOConfigActionInfosByIdAndNameRequest2 =
              Arg.Is<GetMVNOConfigActionInfosByIdAndNameRequest>(x => x.MvnoId == 1 && x.CategoryName == "PURCHASE_PRODUCT_UNLIMITED_PROMOTION");
            var actualMVNOConfigActionInfo3 = CreateDefaultObject.Create<MVNOConfigActionInfo>();
            actualMVNOConfigActionInfo3.BAK1 = "1";
            actualMVNOConfigActionInfo3.Value = "100,101";
            mockedGetMVNOConfigActionInfosByIdAndNameMS.Process(actualGetMVNOConfigActionInfosByIdAndNameRequest2, null)
               .Returns(new GetMVNOConfigActionInfosByIdAndNameResponse()
               {
                   MvnoConfigActionInfos = new List<MVNOConfigActionInfo>() { actualMVNOConfigActionInfo3 },
                   ResultType = ResultTypes.Ok,
               });
            #endregion
            #region Mocked GetTaxDefinitonsByCategoryRequest
            var mockedGetTaxDefinitonsByCategoryMs = MockMicroService<GetTaxDefinitonsByCategoryRequest, GetTaxDefinitonsByCategoryResponse>();
            var actualGetTaxDefinitonsByCategoryRequest =
                Arg.Is<GetTaxDefinitonsByCategoryRequest>(x => x.TaxCategory == 1);
            var actualTaxDefinition = CreateDefaultObject.Create<TaxDefinition>();
            var actualTaxRates = new TaxRates();
            actualTaxRates.Percentage = 10;
            actualTaxRates.StartDate = _startDate;
            actualTaxRates.EndDate = _endDate;
            actualTaxDefinition.Rates = new List<TaxRates>() { actualTaxRates };

            var actualGetTaxDefinitonsByCategoryResponse = new GetTaxDefinitonsByCategoryResponse()
            {
                TaxDefinitions = new List<TaxDefinition>() { actualTaxDefinition },
                ResultType = ResultTypes.Ok,
            };
            mockedGetTaxDefinitonsByCategoryMs.Process(actualGetTaxDefinitonsByCategoryRequest, null)
                .Returns(actualGetTaxDefinitonsByCategoryResponse);
            #endregion
            #region Mocked PurchaseHelper.GetProductsAndChargesOptions
            _mockedPurchaseHelper = Substitute.ForPartsOf<PurchaseHelper>();
            _mockedPurchaseHelper.GetProductsAndChargesOptions(Arg.Any<List<ProductCatalogDTO>>()).Returns(_actualListTuplesPurchaseProducts);

            #endregion
            #region Mocked GetCustomerProductAssignmentsByCustomerId
            var mockedGetCustomerProductAssignmentsByCustomerMs = MockMicroService<GetCustomerProductAssignmentsByCustomerIdRequest, GetCustomerProductAssignmentsByCustomerIdResponse>();
            var actualGetCustomerProductAssignmentsByCustomerIdRequest =
                Arg.Is<GetCustomerProductAssignmentsByCustomerIdRequest>(x => x.CustomerId == _actualCustomerInfo.CustomerID);

            var actualGetCustomerProductAssignmentsByCustomerIdResponse = new GetCustomerProductAssignmentsByCustomerIdResponse()
            {
                CustomerProductAssignments = new List<CustomerProductAssignment>() { _customerProductAssn, _customerProductAssn2 },
                ResultType = ResultTypes.Ok,
            };
            mockedGetCustomerProductAssignmentsByCustomerMs.Process(actualGetCustomerProductAssignmentsByCustomerIdRequest, null)
                .Returns(actualGetCustomerProductAssignmentsByCustomerIdResponse);
            #endregion
            #region Mocked CheckProductListDependencyRelationsForCustomer
            var mockedCheckProductListDependencyRelationsForCustomerMs = MockMicroService<CheckProductListDependencyRelationsForCustomerRequest, CheckProductListDependencyRelationsForCustomerResponse>();
            var actualCheckProductListDependencyRelationsForCustomerRequest =
                Arg.Is<CheckProductListDependencyRelationsForCustomerRequest>(x => x.CustomerProducts.Any(y => y.Id == 1) && x.ProductsToPurchase.Any(y => y.Id == _customerProductAssn.Id));
            var actualCheckProductListDependencyRelationsForCustomerRequest2 =
                Arg.Is<CheckProductListDependencyRelationsForCustomerRequest>(x => x.CustomerProducts.Count == 0);
            var actualCheckProductListDependencyRelationsForCustomerRequest3 =
                Arg.Is<CheckProductListDependencyRelationsForCustomerRequest>(x => x.CustomerProducts.Any(y => y.Id == 2));
            var actualCheckProductListDependencyRelationsForCustomerResponse = new CheckProductListDependencyRelationsForCustomerResponse()
            {
                AreListRequirementsSatisfiedForCustomer = true,
                IsListCompatibleWithCustomerProducts = true,
                DeprovisionList = new List<ProductOfferingSpecificationOption>() { },

                ResultType = ResultTypes.Ok,
            };
            var actualCheckProductListDependencyRelationsForCustomerResponse3 = new CheckProductListDependencyRelationsForCustomerResponse()
            {
                AreListRequirementsSatisfiedForCustomer = true,
                IsListCompatibleWithCustomerProducts = false,
                DeprovisionList = new List<ProductOfferingSpecificationOption>() { },

                ResultType = ResultTypes.Ok,
            };
            mockedCheckProductListDependencyRelationsForCustomerMs.Process(actualCheckProductListDependencyRelationsForCustomerRequest, null)
                .Returns(actualCheckProductListDependencyRelationsForCustomerResponse);
            mockedCheckProductListDependencyRelationsForCustomerMs.Process(actualCheckProductListDependencyRelationsForCustomerRequest2, null)
               .Returns(actualCheckProductListDependencyRelationsForCustomerResponse);
            mockedCheckProductListDependencyRelationsForCustomerMs.Process(actualCheckProductListDependencyRelationsForCustomerRequest3, null)
               .Returns(actualCheckProductListDependencyRelationsForCustomerResponse3);





            #endregion
            #region Mocked calculateNextBillRunDateMS
            var mockedCalculateNextBillRunDateMs = MockMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            mockedCalculateNextBillRunDateMs.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(), null)
               .Returns(new CalculateNextBillRunDateForBillCycleResponse()
               {
                   NextBillRun = _nextBillRunStartDate,
                   ResultType = ResultTypes.Ok,

               });
            #endregion
            #region Mocked GetCustomerById
            var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            _serviceInfo.BundleDefinition.BundleID = 1;
            _serviceInfo.UnBilledBalance = 10;
            _actualCustomerInfo.ServicesInfo = new List<ServicesInfo>() { _serviceInfo };

            _actualCustomerInfo.RevenueProductsInfo = new List<CustomerProductAssignment>() { _customerProductAssn, _customerProductAssn2 };

            _crmCustomersPromotionInfo.StartDate = _startDate;
            _crmCustomersPromotionInfo.EndDate = _endDate;
            _crmCustomersPromotionInfo.PromotionDetail = _actualRmPromotionPlanDetailInfo;
            _crmCustomersPromotionInfo.Active = true;

            _crmCustomersPromotionInfoNotActive.StartDate = _startDate;
            _crmCustomersPromotionInfoNotActive.EndDate = _endDate;
            _crmCustomersPromotionInfoNotActive.PromotionDetail = _actualRmPromotionPlanDetailInfo;
            _crmCustomersPromotionInfoNotActive.Active = false;

            _crmCustomersPromotionInfoNextBillRunStartDate.StartDate = _nextBillRunStartDate;
            _crmCustomersPromotionInfoNextBillRunStartDate.EndDate = _nextBillRunEndDate;
            _crmCustomersPromotionInfoNextBillRunStartDate.PromotionDetail = _actualRmPromotionPlanDetailInfo;
            _crmCustomersPromotionInfoNextBillRunStartDate.Active = true;



            _actualCustomerInfo.Promotions = new List<CrmCustomersPromotionInfo>() { _crmCustomersPromotionInfo, _crmCustomersPromotionInfoNextBillRunStartDate, _crmCustomersPromotionInfoNotActive };
            mockedRepoCustomerInfo.GetById(_actualCustomerInfo.CustomerID.Value).Returns(_actualCustomerInfo);

            #endregion
            #region Mocked GetPriorityBundleInfoFromBundleInfosMs
            var mockedGetPriorityBundleInfoFromBundleInfosMs = MockMicroService<GetPriorityBundleInfoFromBundleInfosRequest, GetPriorityBundleInfoFromBundleInfosResponse>();

            var getPriorityBundleInfoFromBundleInfosResponse = new GetPriorityBundleInfoFromBundleInfosResponse()
            {
                PriorityBundle = _actualBundleInfo,
                ResultType = ResultTypes.Ok,
            };
            mockedGetPriorityBundleInfoFromBundleInfosMs.Process(Arg.Any<GetPriorityBundleInfoFromBundleInfosRequest>(), null)
                .Returns(getPriorityBundleInfoFromBundleInfosResponse);
            #endregion
            #region Mocked getProductByProductIdMS
            var mockedGetProductByProductIdMS = MockMicroService<GetProductByProductIdRequest, GetProductByProductIdResponse>();

            var getProductByProductIdResponse = new GetProductByProductIdResponse()
            {
                Product = _actualProductOrig,
                ResultType = ResultTypes.Ok,
            };

            mockedGetProductByProductIdMS.Process(Arg.Any<GetProductByProductIdRequest>(), null)
                .Returns(getProductByProductIdResponse);
            #endregion
            #region Mocked GetProductOfferingByProductOfferingIdMs
            var mockedGetProductOfferingByProductOfferingIdMs = MockMicroService<GetProductOfferingByProductOfferingIdRequest, GetProductOfferingByProductOfferingIdResponse>();

            var getGetProductOfferingByProductOfferingIdResponse = new GetProductOfferingByProductOfferingIdResponse()
            {
                ResultType = ResultTypes.Ok,
                ProductOffering = _actualProductOffering
            };

            mockedGetProductOfferingByProductOfferingIdMs.Process(Arg.Any<GetProductOfferingByProductOfferingIdRequest>(), null)
                .Returns(getGetProductOfferingByProductOfferingIdResponse);
            #endregion
            #region Mocked IProductRepository
            MockRepositoryManager.GetMockedRepository<IProductOfferingRepository<ProductOffering>>();
            #endregion


        }

        [SetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
        }

        [Test()]
        public void CheckPurchaseProductForCustomerBizOp_CorrectPurchaseProductGiven_ShouldOK()
        {

            var request = new CheckPurchaseProductForCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                ForceCreditLimit = 100,
                ProductCatalogDTOs = _actualProductCatalogDtOs,
                PurchaseDate = _purchaseDate,
                TaxCategory = 1,
                CustomerId = 1,
            };

            MockAbsctractBusinessOperation(request);
            StandardMocks();

            CheckPurchaseProductForCustomerResponseDTO response;
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var bizop = new CheckPurchaseProductForCustomerBizOp(_mockedPurchaseHelper);

                response = bizop.ProcessFromCustomerModel(new NullValidator<CheckPurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<CheckPurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<CheckPurchaseProductForCustomerResponseDTO>(), request, FakeInvoker);

            }
            RepositoryManager.CloseConnection();

            Assert.IsTrue(response.IsCreditEnough == false);
            Assert.IsTrue(response.resultType == ResultTypes.Ok);

            request.ForceCreditLimit = null;

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var bizop = new CheckPurchaseProductForCustomerBizOp(_mockedPurchaseHelper);

                response = bizop.ProcessFromCustomerModel(new NullValidator<CheckPurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<CheckPurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<CheckPurchaseProductForCustomerResponseDTO>(), request, FakeInvoker);

            }
            RepositoryManager.CloseConnection();

            Assert.IsTrue(response.IsCreditEnough);
            Assert.IsTrue(response.resultType == ResultTypes.Ok);
            Assert.IsNotNull(response.Subscription);
        }


        [Test()]
        public void CheckPurchaseProductForCustomerBizOpWithoutNextBillRun_CorrectPurchaseProductGiven_ShouldOK()
        {

            var request = new CheckPurchaseProductForCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                ForceCreditLimit = 100,
                ProductCatalogDTOs = _actualProductCatalogDtOs,
                PurchaseDate = _purchaseDate,
                TaxCategory = 1,
                CustomerId = 1,
            };

            MockAbsctractBusinessOperation(request);
            StandardMocks();
            _nextBillRunStartDate = DateTime.MinValue;
            CheckPurchaseProductForCustomerResponseDTO response;

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var bizop = new CheckPurchaseProductForCustomerBizOp(_mockedPurchaseHelper);

                response = bizop.ProcessFromCustomerModel(new NullValidator<CheckPurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<CheckPurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<CheckPurchaseProductForCustomerResponseDTO>(), request, FakeInvoker);

            }
            RepositoryManager.CloseConnection();

            Assert.IsTrue(response.IsCreditEnough == false);
            Assert.IsTrue(response.resultType == ResultTypes.Ok);
            Assert.IsNotNull(response.Subscription);

            request.ForceCreditLimit = null;

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var bizop = new CheckPurchaseProductForCustomerBizOp(_mockedPurchaseHelper);

                response = bizop.ProcessFromCustomerModel(new NullValidator<CheckPurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<CheckPurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<CheckPurchaseProductForCustomerResponseDTO>(), request, FakeInvoker);

            }
            RepositoryManager.CloseConnection();

            Assert.IsTrue(response.IsCreditEnough);
            Assert.IsTrue(response.resultType == ResultTypes.Ok);
        }

        [Test()]
        public void CheckPurchaseProductForCustomerBizOpWithoutNextBillRun_PurchaseProductGivenWithHighLimit_ShouldReturnLimitReached()
        {

            var request = new CheckPurchaseProductForCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                ForceCreditLimit = 100,
                ProductCatalogDTOs = _actualProductCatalogDtOs,
                PurchaseDate = _purchaseDate,
                TaxCategory = 1,
                CustomerId = 1,
            };

            MockAbsctractBusinessOperation(request);
            StandardMocks();
            _crmCustomersPromotionInfo.CurrentLimit = 500;
            _crmCustomersPromotionInfo.PromotionDetail.Limit = 500;
            _actualProductDependencyRelation = null;
            CheckPurchaseProductForCustomerResponseDTO response;

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var bizop = new CheckPurchaseProductForCustomerBizOp(_mockedPurchaseHelper);

                response = bizop.ProcessFromCustomerModel(new NullValidator<CheckPurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<CheckPurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<CheckPurchaseProductForCustomerResponseDTO>(), request, FakeInvoker);

            }
            RepositoryManager.CloseConnection();

            Assert.IsTrue(response.IsLimitReached);
            Assert.IsTrue(response.resultType == ResultTypes.Ok);

        }


        [Test()]
        public void CheckPurchaseProductForCustomerBizOpWithCustomerPromotionLimitReachedConfiguration_PurchaseProductGivenWithDifferentSubServiceTypeId_ShouldReturnOk()
        {

            var request = new CheckPurchaseProductForCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                ForceCreditLimit = 100,
                ProductCatalogDTOs = _actualProductCatalogDtOs,
                PurchaseDate = _purchaseDate,
                TaxCategory = 1,
                CustomerId = _actualCustomerInfo.CustomerID.Value,
            };
            MockAbsctractBusinessOperation(request);
            StandardMocks();
            CheckPurchaseProductForCustomerResponseDTO response;

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var bizop = new CheckPurchaseProductForCustomerBizOp(_mockedPurchaseHelper);

                response = bizop.ProcessFromCustomerModel(new NullValidator<CheckPurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<CheckPurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<CheckPurchaseProductForCustomerResponseDTO>(), request, FakeInvoker);

            }
            RepositoryManager.CloseConnection();

            Assert.IsTrue(response.IsCreditEnough == false);
            Assert.IsTrue(response.resultType == ResultTypes.Ok);
            Assert.IsNotNull(response.Subscription);

            request.ForceCreditLimit = null;

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var bizop = new CheckPurchaseProductForCustomerBizOp(_mockedPurchaseHelper);

                response = bizop.ProcessFromCustomerModel(new NullValidator<CheckPurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<CheckPurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<CheckPurchaseProductForCustomerResponseDTO>(), request, FakeInvoker);

            }
            RepositoryManager.CloseConnection();

            Assert.IsTrue(response.IsCreditEnough);
            Assert.IsTrue(response.resultType == ResultTypes.Ok);
            Assert.IsNotNull(response.Subscription);
        }


        [Test()]
        public void CheckPurchaseProductForCustomerBizOpWithCustomerPromotionOk_PurchaseProductGivenWithNoCommercialProductLimitReached_ShouldReturnOk()
        {

            var request = new CheckPurchaseProductForCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                ForceCreditLimit = 10,
                ProductCatalogDTOs = _actualProductCatalogDtOs,
                PurchaseDate = _purchaseDate,
                TaxCategory = 1,
                CustomerId = _actualCustomerInfo.CustomerID.Value,
            };

            MockAbsctractBusinessOperation(request);
            StandardMocks();
            _actualProductOrig.Type.Description = "NoCommercial";
            CheckPurchaseProductForCustomerResponseDTO response;

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var bizop = new CheckPurchaseProductForCustomerBizOp(_mockedPurchaseHelper);

                response = bizop.ProcessFromCustomerModel(new NullValidator<CheckPurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<CheckPurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<CheckPurchaseProductForCustomerResponseDTO>(), request, FakeInvoker);

            }
            RepositoryManager.CloseConnection();

            Assert.IsTrue(response.IsCreditEnough == false);
            Assert.IsTrue(response.resultType == ResultTypes.Ok);
            Assert.IsNotNull(response.Subscription);

            request.ForceCreditLimit = null;

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var bizop = new CheckPurchaseProductForCustomerBizOp(_mockedPurchaseHelper);

                response = bizop.ProcessFromCustomerModel(new NullValidator<CheckPurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<CheckPurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<CheckPurchaseProductForCustomerResponseDTO>(), request, FakeInvoker);

            }
            RepositoryManager.CloseConnection();

            Assert.IsTrue(response.IsCreditEnough);
            Assert.IsTrue(response.resultType == ResultTypes.Ok);
            Assert.IsNotNull(response.Subscription);
        }

        [Test()]
        public void CheckPurchaseProductForCustomerBizOpWithCustomerPromotionOk_PurchaseProductsGivenWithStartDateNextBillRunAndProductCustomerIncompatibleWithActualBillRunEndDate_ShouldReturnOk()
        {

            var request = new CheckPurchaseProductForCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                ForceCreditLimit = null,
                ProductCatalogDTOs = _actualProductCatalogDtOs,
                PurchaseDate = _purchaseDate,
                TaxCategory = 1,
                CustomerId = _actualCustomerInfo.CustomerID.Value,
            };
            MockAbsctractBusinessOperation(request);
            StandardMocks();
            _crmCustomersPromotionInfoNextBillRunStartDate.EndDate = _nextBillRunStartDate;
            _customerProductAssn2.Id = 2;

            CheckPurchaseProductForCustomerResponseDTO response;

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var bizop = new CheckPurchaseProductForCustomerBizOp(_mockedPurchaseHelper);

                response = bizop.ProcessFromCustomerModel(new NullValidator<CheckPurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<CheckPurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<CheckPurchaseProductForCustomerResponseDTO>(), request, FakeInvoker);

            }
            RepositoryManager.CloseConnection();

            Assert.IsTrue(response.IsListCompatibleWithCustomerProducts);
            Assert.IsTrue(response.resultType == ResultTypes.Ok);
            Assert.IsNotNull(response.Subscription);
        }

        [Test()]
        public void CheckPurchaseProductForCustomerBizOpWithCustomerPromotionUnlimtedConfiguration_NotCountUnlimitedPromotion_ReturnOK()
        {

            var request = new CheckPurchaseProductForCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                ForceCreditLimit = 100,
                ProductCatalogDTOs = _actualProductCatalogDtOs,
                PurchaseDate = _purchaseDate,
                TaxCategory = 1,
                CustomerId = _actualCustomerInfo.CustomerID.Value,
            };

            MockAbsctractBusinessOperation(request);
            StandardMocks();
            CheckPurchaseProductForCustomerResponseDTO response;

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var bizop = new CheckPurchaseProductForCustomerBizOp(_mockedPurchaseHelper);

                response = bizop.ProcessFromCustomerModel(new NullValidator<CheckPurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<CheckPurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<CheckPurchaseProductForCustomerResponseDTO>(), request, FakeInvoker);

            }
            RepositoryManager.CloseConnection();

            Assert.IsTrue(!response.IsLimitReached);
            Assert.IsTrue(response.resultType == ResultTypes.Ok);
        }
    }
}
