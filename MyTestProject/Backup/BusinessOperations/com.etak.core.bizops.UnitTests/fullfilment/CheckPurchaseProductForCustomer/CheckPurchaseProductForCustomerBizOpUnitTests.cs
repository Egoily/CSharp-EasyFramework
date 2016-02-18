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
using com.etak.core.operation;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.product.message.CalculateNextBillRunDateForBillCycle;
using com.etak.core.product.message.GetProductByProductId;
using com.etak.core.repository;
using com.etak.core.repository.crm.customer;
using com.etak.core.service.messages.GetPriorityBundleInfoFromBundleInfos;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using FizzWare.NBuilder.Dates;
using NHibernate.Mapping;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.fullfilment.CheckPurchaseProductForCustomer
{
    [TestFixture]
    public class CheckPurchaseProductForCustomerBizOpUnitTests : AbstractBusinessOperationTest <CheckPurchaseProductForCustomerBizOp, CheckPurchaseProductForCustomerRequestDTO,
                CheckPurchaseProductForCustomerResponseDTO, CheckPurchaseProductForCustomerRequestInternal, CheckPurchaseProductForCustomerResponseInternal>
    {
        private DateTime startDate = new DateTime(2015, 4, 6);
        private DateTime purchaseDate = new DateTime(2015, 5, 6);
        private DateTime endDate = new DateTime(2015, 6, 6);
        private DateTime nextBillRunStartDate = new DateTime(2016, 7, 6);
        private DateTime nextBillRunDate = new DateTime(2016, 5, 6);
        private List<Tuple<ProductOffering, ProductChargeOption>> actualListTuplesPurchaseProducts = new List<Tuple<ProductOffering, ProductChargeOption>>();
        private PurchaseHelper mockedPurchaseHelper = null;


        [SetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();


            actualListTuplesPurchaseProducts = new List<Tuple<ProductOffering, ProductChargeOption>>();
            var actualProduct = CreateDefaultObject.Create<ProductOffering>();
            var actualProductOrig = CreateDefaultObject.Create<Product>();
            actualProductOrig.Type = CreateDefaultObject.Create<ProductType>();
            actualProductOrig.Type.Description = "Commercial";
            actualProduct.Id = 1;
            actualProduct.OfferedProduct = actualProductOrig;
            actualProduct.OfferedProduct.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            var actualRmPromotionPlanDetailInfo = new RmPromotionPlanDetailInfo();
            actualRmPromotionPlanDetailInfo.Limit = 10;
            actualRmPromotionPlanDetailInfo.StartDate = new DateTime(2013, 5, 6);
            actualRmPromotionPlanDetailInfo.EndDate = null;
            actualRmPromotionPlanDetailInfo.SubServiceTypeId = 1;
            var actualRmPromotionPlanDetailInfo2 = new RmPromotionPlanDetailInfo();
            actualRmPromotionPlanDetailInfo2.Limit = 20;
            actualRmPromotionPlanDetailInfo2.StartDate = new DateTime(2013, 5, 6);
            actualRmPromotionPlanDetailInfo2.EndDate = new DateTime(2017, 5, 6);
            actualRmPromotionPlanDetailInfo2.SubServiceTypeId = 1;

            var actualRmPromotionPlanDetailInfo3 = new RmPromotionPlanDetailInfo();
            actualRmPromotionPlanDetailInfo3.Limit = 20;
            actualRmPromotionPlanDetailInfo3.StartDate = nextBillRunStartDate;
            actualRmPromotionPlanDetailInfo3.EndDate = new DateTime(2017, 5, 6);
            actualRmPromotionPlanDetailInfo3.SubServiceTypeId = 1;

            actualProductOrig.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList = new List<RmPromotionPlanDetailInfo>() { actualRmPromotionPlanDetailInfo3, actualRmPromotionPlanDetailInfo, actualRmPromotionPlanDetailInfo2 };

            var actualCommercialProduct = CreateDefaultObject.Create<ProductOffering>();
            actualCommercialProduct.Id = 2;
            actualProductOrig.Type = CreateDefaultObject.Create<ProductType>();
            actualProductOrig.Type.Description = "Commercial";

            var actualProductChargeOption = new ProductChargeOption();
            var actualChargeRecurring = new ChargeRecurring();
            var actualChargePrice = new ChargePrice();
            actualChargePrice.StartDate = new DateTime(2013, 5, 6);
            actualChargePrice.EndDate = new DateTime(2017, 5, 6);
            actualChargePrice.Amount = 100;

            var actualChargePrice2 = new ChargePrice();
            actualChargePrice2.StartDate = new DateTime(2013, 5, 6);
            actualChargePrice2.EndDate = new DateTime(2017, 5, 6);
            actualChargePrice2.Amount = 100;

            
            actualChargeRecurring.Prices.Add(actualChargePrice);
            actualChargeRecurring.Prices.Add(actualChargePrice2);
            
            actualProductChargeOption.Charges = new List<Charge>() { actualChargeRecurring };
            actualProductChargeOption.StartDate = new DateTime(2013, 5, 6);
            actualProductChargeOption.EndDate = new DateTime(2016, 3, 3);

            var actualCommercialProductChargeOption = new ProductChargeOption();
            actualCommercialProductChargeOption.Charges = new List<Charge>() { actualChargeRecurring };
            actualCommercialProductChargeOption.StartDate = nextBillRunDate;
            actualCommercialProductChargeOption.EndDate = nextBillRunDate;

            actualListTuplesPurchaseProducts.Add(Tuple.Create(actualProduct, actualProductChargeOption));
            actualListTuplesPurchaseProducts.Add(Tuple.Create(actualCommercialProduct, actualCommercialProductChargeOption));

            //Mocked getActiveCusotmerAccountMs
            var mockedGetActiveCusotmerAccountMs = MockMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();
            mockedGetActiveCusotmerAccountMs.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(), null)
               .Returns(new GetActiveCustomerAccountAssociationByDateResponse()
               {
                   CustomerAccountAssociation = CreateDefaultObject.Create<CustomerAccountAssociation>(),
                   ResultType = ResultTypes.Ok,

               });

            //Mocked GetMVNOConfigActionInfosByIdAndNameMS 
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
                   MvnoConfigActionInfos = new  List<MVNOConfigActionInfo>() {actualMVNOConfigActionInfo3},
                   ResultType = ResultTypes.Ok,
               });

            //Mocked GetTaxDefinitonsByCategoryRequest  
            var mockedGetTaxDefinitonsByCategoryMs = MockMicroService<GetTaxDefinitonsByCategoryRequest, GetTaxDefinitonsByCategoryResponse>();
            var actualGetTaxDefinitonsByCategoryRequest =
                Arg.Is<GetTaxDefinitonsByCategoryRequest>(x => x.TaxCategory == 1);
            var actualTaxDefinition = CreateDefaultObject.Create<TaxDefinition>();
            var actualTaxRates = new TaxRates();
            actualTaxRates.Percentage = 10;
            actualTaxRates.StartDate = new DateTime(2013, 5, 6);
            actualTaxRates.EndDate = new DateTime(2017, 5, 6);
            actualTaxDefinition.Rates = new List<TaxRates>() { actualTaxRates };

            var actualGetTaxDefinitonsByCategoryResponse = new GetTaxDefinitonsByCategoryResponse()
            {
                TaxDefinitions = new List<TaxDefinition>() { actualTaxDefinition },
                ResultType = ResultTypes.Ok,
            };
            mockedGetTaxDefinitonsByCategoryMs.Process(actualGetTaxDefinitonsByCategoryRequest, null)
                .Returns(actualGetTaxDefinitonsByCategoryResponse);

            //Mocked PurchaseHelper.GetProductsAndChargesOptions
            mockedPurchaseHelper = Substitute.ForPartsOf<PurchaseHelper>();
            mockedPurchaseHelper.GetProductsAndChargesOptions(Arg.Any<List<ProductCatalogDTO>>()).Returns(actualListTuplesPurchaseProducts);


            //Mocked GetCustomerProductAssignmentsByCustomerId
            var mockedGetCustomerProductAssignmentsByCustomerMs = MockMicroService<GetCustomerProductAssignmentsByCustomerIdRequest, GetCustomerProductAssignmentsByCustomerIdResponse>();
            var actualGetCustomerProductAssignmentsByCustomerIdRequest =
                Arg.Is<GetCustomerProductAssignmentsByCustomerIdRequest>(x => x.CustomerId == 1);
            var actualCustomerProductAssignment = CreateDefaultObject.Create<CustomerProductAssignment>();
            actualCustomerProductAssignment.PurchasedProduct = CreateDefaultObject.Create<Product>();
            actualCustomerProductAssignment.PurchasedProduct.Id = 1;
            actualCustomerProductAssignment.PurchasedProduct.AssociatedBundle = CreateDefaultObject.Create<BundleInfo>();

            var actualCustomerProductAssignment2 = CreateDefaultObject.Create<CustomerProductAssignment>();
            actualCustomerProductAssignment2.PurchasedProduct = CreateDefaultObject.Create<Product>();
            actualCustomerProductAssignment2.PurchasedProduct.Id = 2;
            actualCustomerProductAssignment2.StartDate = new DateTime(2013, 5, 6);
            actualCustomerProductAssignment2.EndDate = new DateTime(2016, 3, 3);
            actualCustomerProductAssignment2.PurchasedProduct.AssociatedBundle = CreateDefaultObject.Create<BundleInfo>();

            
            var actualGetCustomerProductAssignmentsByCustomerIdResponse = new GetCustomerProductAssignmentsByCustomerIdResponse()
            {
                CustomerProductAssignments = new List<CustomerProductAssignment>() { actualCustomerProductAssignment, actualCustomerProductAssignment2 },
                ResultType = ResultTypes.Ok,
            };
            mockedGetCustomerProductAssignmentsByCustomerMs.Process(actualGetCustomerProductAssignmentsByCustomerIdRequest, null)
                .Returns(actualGetCustomerProductAssignmentsByCustomerIdResponse);

            //Mocked CheckProductListDependencyRelationsForCustomer
            var mockedCheckProductListDependencyRelationsForCustomerMs = MockMicroService<CheckProductListDependencyRelationsForCustomerRequest, CheckProductListDependencyRelationsForCustomerResponse>();
            var actualCheckProductListDependencyRelationsForCustomerRequest =
                Arg.Is<CheckProductListDependencyRelationsForCustomerRequest>(x => x.CustomerProducts.Any(y => y.Id == 1) && x.ProductsToPurchase.Any(y => y.Id == 1));
            var actualProductDependencyRelation = CreateDefaultObject.Create<ProductOfferingSpecificationOption>();

            var actualCheckProductListDependencyRelationsForCustomerResponse = new CheckProductListDependencyRelationsForCustomerResponse()
            {
                AreListRequirementsSatisfiedForCustomer = true,
                IsListCompatibleWithCustomerProducts = true,
                DeprovisionList = new List<ProductOfferingSpecificationOption>() { actualProductDependencyRelation },

                ResultType = ResultTypes.Ok,
            };
            mockedCheckProductListDependencyRelationsForCustomerMs.Process(actualCheckProductListDependencyRelationsForCustomerRequest, null)
                .Returns(actualCheckProductListDependencyRelationsForCustomerResponse);


            var actualCheckProductListDependencyRelationsForCustomerRequest2 =
               Arg.Is<CheckProductListDependencyRelationsForCustomerRequest>(x => x.CustomerProducts.Count == 0);

            mockedCheckProductListDependencyRelationsForCustomerMs.Process(actualCheckProductListDependencyRelationsForCustomerRequest2, null)
               .Returns(actualCheckProductListDependencyRelationsForCustomerResponse);
        }

        [Test()]
        public void CheckPurchaseProductForCustomerBizOp_CorrectPurchaseProductGiven_ShouldOK()
        {
            var actualProductCatalogDTOs = new List<ProductCatalogDTO>() { CreateDefaultObject.Create<ProductCatalogDTO>() };

            var request = new CheckPurchaseProductForCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                ForceCreditLimit = 100,
                ProductCatalogDTOs = actualProductCatalogDTOs,
                PurchaseDate = purchaseDate,
                TaxCategory = 1,
                CustomerId = 1,
            };



            //Mocked CheckProductListDependencyRelationsForCustomer
            var mockedCheckProductListDependencyRelationsForCustomerMs = MockMicroService<CheckProductListDependencyRelationsForCustomerRequest, CheckProductListDependencyRelationsForCustomerResponse>();
            var actualProductDependencyRelation = CreateDefaultObject.Create<ProductOfferingSpecificationOption>();
            actualProductDependencyRelation.RelatedProductOffering.OfferedProduct.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();

            var actualCheckProductListDependencyRelationsForCustomerResponse = new CheckProductListDependencyRelationsForCustomerResponse()
            {
                AreListRequirementsSatisfiedForCustomer = true,
                IsListCompatibleWithCustomerProducts = true,
                DeprovisionList = new List<ProductOfferingSpecificationOption>() { actualProductDependencyRelation },

                ResultType = ResultTypes.Ok,
            };
            mockedCheckProductListDependencyRelationsForCustomerMs.Process(Arg.Any<CheckProductListDependencyRelationsForCustomerRequest>(), null)
                .Returns(actualCheckProductListDependencyRelationsForCustomerResponse);



            //Mocked calculateNextBillRunDateMS
            var mockedCalculateNextBillRunDateMS = MockMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            mockedCalculateNextBillRunDateMS.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(), null)
               .Returns(new CalculateNextBillRunDateForBillCycleResponse()
               {
                   NextBillRun = nextBillRunDate,
                   ResultType = ResultTypes.Ok,

               });
            


            MockAbsctractBusinessOperation(request);

            CheckPurchaseProductForCustomerResponseDTO response;

            //Mocked GetCustomerById
            var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            var actualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomerInfo.CustomerID = request.CustomerId;
            var serviceInfo = CreateDefaultObject.Create<ServicesInfo>();
            serviceInfo.BundleDefinition.BundleID = 1;
            serviceInfo.UnBilledBalance = 10;
            actualCustomerInfo.ServicesInfo = new List<ServicesInfo>() { serviceInfo };

            var customerProductAssn = CreateDefaultObject.Create<CustomerProductAssignment>();
            actualCustomerInfo.RevenueProductsInfo = new List<CustomerProductAssignment>() { customerProductAssn };

            var crmCustomersPromotionInfo = new CrmCustomersPromotionInfo();
            crmCustomersPromotionInfo.StartDate = startDate;
            crmCustomersPromotionInfo.EndDate = endDate;
            crmCustomersPromotionInfo.PromotionDetail = new RmPromotionPlanDetailInfo();
            crmCustomersPromotionInfo.PromotionDetail.SubServiceTypeId = 1;
            crmCustomersPromotionInfo.PromotionDetail.Limit = 20;
            crmCustomersPromotionInfo.Active = true;


            var crmCustomersPromotionInfoNotActive = new CrmCustomersPromotionInfo();
            crmCustomersPromotionInfoNotActive.StartDate = startDate;
            crmCustomersPromotionInfoNotActive.EndDate = endDate;
            crmCustomersPromotionInfoNotActive.PromotionDetail = new RmPromotionPlanDetailInfo();
            crmCustomersPromotionInfoNotActive.PromotionDetail.SubServiceTypeId = 1;
            crmCustomersPromotionInfoNotActive.PromotionDetail.Limit = 20;
            crmCustomersPromotionInfoNotActive.Active = false;

            var crmCustomersPromotionInfoNextBillRunStartDate = new CrmCustomersPromotionInfo();
            crmCustomersPromotionInfoNextBillRunStartDate.StartDate = nextBillRunStartDate;
            crmCustomersPromotionInfoNextBillRunStartDate.EndDate = nextBillRunStartDate;
            crmCustomersPromotionInfoNextBillRunStartDate.PromotionDetail = new RmPromotionPlanDetailInfo();
            crmCustomersPromotionInfoNextBillRunStartDate.PromotionDetail.SubServiceTypeId = 1;
            crmCustomersPromotionInfoNextBillRunStartDate.PromotionDetail.Limit = 30;
            crmCustomersPromotionInfoNextBillRunStartDate.Active = true;



            actualCustomerInfo.Promotions = new List<CrmCustomersPromotionInfo>() { crmCustomersPromotionInfo, crmCustomersPromotionInfoNextBillRunStartDate, crmCustomersPromotionInfoNotActive };
            actualCustomerInfo.ResourceMBInfo.Add(CreateDefaultObject.Create<ResourceMBInfo>());
            mockedRepoCustomerInfo.GetById(request.CustomerId).Returns(actualCustomerInfo);


            //Mocked GetPriorityBundleInfoFromBundleInfosMs
            var mockedGetPriorityBundleInfoFromBundleInfosMs = MockMicroService<GetPriorityBundleInfoFromBundleInfosRequest, GetPriorityBundleInfoFromBundleInfosResponse>();

            var actualBundleInfo = CreateDefaultObject.Create<BundleInfo>();
            actualBundleInfo.CreditLimit = 300;

            var getPriorityBundleInfoFromBundleInfosResponse = new GetPriorityBundleInfoFromBundleInfosResponse()
            {
                PriorityBundle = actualBundleInfo,
                ResultType = ResultTypes.Ok,
            };
            mockedGetPriorityBundleInfoFromBundleInfosMs.Process(Arg.Any<GetPriorityBundleInfoFromBundleInfosRequest>(), null)
                .Returns(getPriorityBundleInfoFromBundleInfosResponse);

            //Mocked getProductByProductIdMS
            var mockedGetProductByProductIdMS = MockMicroService<GetProductByProductIdRequest, GetProductByProductIdResponse>();

            var getProductByProductIdResponse = new GetProductByProductIdResponse()
            {
                ResultType = ResultTypes.Ok,
            };

            mockedGetProductByProductIdMS.Process(Arg.Any<GetProductByProductIdRequest>(), null)
                .Returns(getProductByProductIdResponse);

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var bizop = new CheckPurchaseProductForCustomerBizOp(mockedPurchaseHelper);

                response = bizop.ProcessFromCustomerModel(new NullValidator<CheckPurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<CheckPurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<CheckPurchaseProductForCustomerResponseDTO>(), request, FakeInvoker);

            }
            RepositoryManager.CloseConnection();

            Assert.IsTrue(response.IsCreditEnough == false);
            Assert.IsTrue(response.resultType == ResultTypes.Ok);

            request.ForceCreditLimit = null;

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var bizop = new CheckPurchaseProductForCustomerBizOp(mockedPurchaseHelper);

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
            var actualProductCatalogDTOs = new List<ProductCatalogDTO>() { CreateDefaultObject.Create<ProductCatalogDTO>() };

            var request = new CheckPurchaseProductForCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                ForceCreditLimit = 100,
                ProductCatalogDTOs = actualProductCatalogDTOs,
                PurchaseDate = purchaseDate,
                TaxCategory = 1,
                CustomerId = 1,
            };


            //Mocked CheckProductListDependencyRelationsForCustomer
            var mockedCheckProductListDependencyRelationsForCustomerMs = MockMicroService<CheckProductListDependencyRelationsForCustomerRequest, CheckProductListDependencyRelationsForCustomerResponse>();
            var actualProductDependencyRelation = CreateDefaultObject.Create<ProductOfferingSpecificationOption>();
            actualProductDependencyRelation.RelatedProductOffering.OfferedProduct.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();

            var actualCheckProductListDependencyRelationsForCustomerResponse = new CheckProductListDependencyRelationsForCustomerResponse()
            {
                AreListRequirementsSatisfiedForCustomer = true,
                IsListCompatibleWithCustomerProducts = true,
                DeprovisionList = new List<ProductOfferingSpecificationOption>() { actualProductDependencyRelation },

                ResultType = ResultTypes.Ok,
            };
            mockedCheckProductListDependencyRelationsForCustomerMs.Process(Arg.Any<CheckProductListDependencyRelationsForCustomerRequest>(), null)
                .Returns(actualCheckProductListDependencyRelationsForCustomerResponse);



            //Mocked calculateNextBillRunDateMS
            var mockedCalculateNextBillRunDateMS = MockMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            mockedCalculateNextBillRunDateMS.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(), null)
               .Returns(new CalculateNextBillRunDateForBillCycleResponse()
               {
                   ResultType = ResultTypes.Ok,

               });



            MockAbsctractBusinessOperation(request);

            //Mocked GetCustomerById
            var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            var actualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomerInfo.CustomerID = request.CustomerId;
            var serviceInfo = CreateDefaultObject.Create<ServicesInfo>();
            serviceInfo.BundleDefinition.BundleID = 1;
            serviceInfo.UnBilledBalance = 10;
            actualCustomerInfo.ServicesInfo = new List<ServicesInfo>() { serviceInfo };

            var customerProductAssn = CreateDefaultObject.Create<CustomerProductAssignment>();
            actualCustomerInfo.RevenueProductsInfo = new List<CustomerProductAssignment>() { customerProductAssn };

            actualCustomerInfo.ServicesInfo = new List<ServicesInfo>() { serviceInfo };

            var crmCustomersPromotionInfo = new CrmCustomersPromotionInfo();
            crmCustomersPromotionInfo.StartDate = startDate;
            crmCustomersPromotionInfo.EndDate = endDate;
            crmCustomersPromotionInfo.PromotionDetail = new RmPromotionPlanDetailInfo();
            crmCustomersPromotionInfo.PromotionDetail.SubServiceTypeId = 1;
            crmCustomersPromotionInfo.PromotionDetail.Limit = 20;
            crmCustomersPromotionInfo.Active = true;


            var crmCustomersPromotionInfoNotActive = new CrmCustomersPromotionInfo();
            crmCustomersPromotionInfoNotActive.StartDate = startDate;
            crmCustomersPromotionInfoNotActive.EndDate = endDate;
            crmCustomersPromotionInfoNotActive.PromotionDetail = new RmPromotionPlanDetailInfo();
            crmCustomersPromotionInfoNotActive.PromotionDetail.SubServiceTypeId = 1;
            crmCustomersPromotionInfoNotActive.PromotionDetail.Limit = 20;
            crmCustomersPromotionInfoNotActive.Active = false;

            var crmCustomersPromotionInfoNextBillRunStartDate = new CrmCustomersPromotionInfo();
            crmCustomersPromotionInfoNextBillRunStartDate.StartDate = nextBillRunStartDate;
            crmCustomersPromotionInfoNextBillRunStartDate.EndDate = nextBillRunStartDate;
            crmCustomersPromotionInfoNextBillRunStartDate.PromotionDetail = new RmPromotionPlanDetailInfo();
            crmCustomersPromotionInfoNextBillRunStartDate.PromotionDetail.SubServiceTypeId = 1;
            crmCustomersPromotionInfoNextBillRunStartDate.PromotionDetail.Limit = 30;
            crmCustomersPromotionInfoNextBillRunStartDate.Active = true;

            var actualCustomerProduct = CreateDefaultObject.Create<Product>();
            actualCustomerProduct.Type = CreateDefaultObject.Create<ProductType>();
            actualCustomerProduct.Type.Description = "Commercial";
            actualCustomerProduct.Id = 1;
            actualCustomerProduct.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            actualCustomerProduct.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList = new List<RmPromotionPlanDetailInfo>() { crmCustomersPromotionInfo.PromotionDetail };





            actualCustomerInfo.Promotions = new List<CrmCustomersPromotionInfo>() { crmCustomersPromotionInfo, crmCustomersPromotionInfoNextBillRunStartDate, crmCustomersPromotionInfoNotActive };
            mockedRepoCustomerInfo.GetById(request.CustomerId).Returns(actualCustomerInfo);



            //Mocked getProductByProductIdMS
            var mockedGetProductByProductIdMS = MockMicroService<GetProductByProductIdRequest, GetProductByProductIdResponse>();

            var getProductByProductIdResponse = new GetProductByProductIdResponse()
            {
                Product = actualCustomerProduct,
                ResultType = ResultTypes.Ok,
            };

            mockedGetProductByProductIdMS.Process(Arg.Is<GetProductByProductIdRequest>(x => x.ProductId == 1), null)
                .Returns(getProductByProductIdResponse);


            //Mocked GetPriorityBundleInfoFromBundleInfosMs
            var mockedGetPriorityBundleInfoFromBundleInfosMs = MockMicroService<GetPriorityBundleInfoFromBundleInfosRequest, GetPriorityBundleInfoFromBundleInfosResponse>();

            var actualBundleInfo = CreateDefaultObject.Create<BundleInfo>();
            actualBundleInfo.CreditLimit = 300;
            
            var getPriorityBundleInfoFromBundleInfosResponse = new GetPriorityBundleInfoFromBundleInfosResponse()
            {
                PriorityBundle = actualBundleInfo,
                ResultType = ResultTypes.Ok,
            };
            mockedGetPriorityBundleInfoFromBundleInfosMs.Process(Arg.Any<GetPriorityBundleInfoFromBundleInfosRequest>(), null)
                .Returns(getPriorityBundleInfoFromBundleInfosResponse);


            CheckPurchaseProductForCustomerResponseDTO response;

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var bizop = new CheckPurchaseProductForCustomerBizOp(mockedPurchaseHelper);

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
                var bizop = new CheckPurchaseProductForCustomerBizOp(mockedPurchaseHelper);

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
            var actualProductCatalogDTOs = new List<ProductCatalogDTO>() { CreateDefaultObject.Create<ProductCatalogDTO>() };

            var request = new CheckPurchaseProductForCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                ForceCreditLimit = 100,
                ProductCatalogDTOs = actualProductCatalogDTOs,
                PurchaseDate = purchaseDate,
                TaxCategory = 1,
                CustomerId = 1,
            };


            //Mocked CheckProductListDependencyRelationsForCustomer
            var mockedCheckProductListDependencyRelationsForCustomerMs = MockMicroService<CheckProductListDependencyRelationsForCustomerRequest, CheckProductListDependencyRelationsForCustomerResponse>();

            var actualProductDependencyRelation = CreateDefaultObject.Create<ProductOfferingSpecificationOption>();
            actualProductDependencyRelation.RelatedProductOffering.OfferedProduct.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();

            var actualCheckProductListDependencyRelationsForCustomerResponse = new CheckProductListDependencyRelationsForCustomerResponse()
            {
                AreListRequirementsSatisfiedForCustomer = true,
                IsListCompatibleWithCustomerProducts = true,
                DeprovisionList = new List<ProductOfferingSpecificationOption>() { actualProductDependencyRelation },

                ResultType = ResultTypes.Ok,
            };

            mockedCheckProductListDependencyRelationsForCustomerMs.Process(Arg.Any<CheckProductListDependencyRelationsForCustomerRequest>(), null)
                .Returns(actualCheckProductListDependencyRelationsForCustomerResponse);



            actualListTuplesPurchaseProducts = new List<Tuple<ProductOffering, ProductChargeOption>>();

            var actualProduct = CreateDefaultObject.Create<ProductOffering>();
            var actualProductOrig = CreateDefaultObject.Create<Product>();
            actualProduct.OfferedProduct = actualProductOrig;
            actualProductOrig.Type = CreateDefaultObject.Create<ProductType>();
            actualProductOrig.Type.Description = "Commercial";
            actualProductOrig.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            var actualRmPromotionPlanDetailInfo = new RmPromotionPlanDetailInfo();
            actualRmPromotionPlanDetailInfo.Limit = 600;
            actualRmPromotionPlanDetailInfo.StartDate = new DateTime(2013, 5, 6);
            actualRmPromotionPlanDetailInfo.EndDate = null;
            actualRmPromotionPlanDetailInfo.SubServiceTypeId = 1;

            actualProductOrig.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList = new List<RmPromotionPlanDetailInfo>() { actualRmPromotionPlanDetailInfo };

            

            var actualProductChargeOption = new ProductChargeOption();
            var actualChargeRecurring = new ChargeRecurring();
            var actualChargePrice = new ChargePrice();
            actualChargePrice.StartDate = new DateTime(2013, 5, 6);
            actualChargePrice.EndDate = new DateTime(2017, 5, 6);
            actualChargePrice.Amount = 10;

            var actualChargePrice2 = new ChargePrice();
            actualChargePrice2.StartDate = new DateTime(2013, 5, 6);
            actualChargePrice2.EndDate = new DateTime(2017, 5, 6);
            actualChargePrice2.Amount = 10;

            actualChargeRecurring.Prices.Add(actualChargePrice);
            actualChargeRecurring.Prices.Add(actualChargePrice2);
            actualProductChargeOption.Charges = new List<Charge>() { actualChargeRecurring };
            actualListTuplesPurchaseProducts.Add(Tuple.Create(actualProduct, actualProductChargeOption));

            //Mocked PurchaseHelper.GetProductsAndChargesOptions
            mockedPurchaseHelper = Substitute.ForPartsOf<PurchaseHelper>();
            mockedPurchaseHelper.GetProductsAndChargesOptions(Arg.Any<List<ProductCatalogDTO>>()).Returns(actualListTuplesPurchaseProducts);

            //Mocked calculateNextBillRunDateMS
            var mockedCalculateNextBillRunDateMS = MockMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            mockedCalculateNextBillRunDateMS.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(), null)
               .Returns(new CalculateNextBillRunDateForBillCycleResponse()
               {
                   ResultType = ResultTypes.Ok,

               });



            MockAbsctractBusinessOperation(request);

            //Mocked GetCustomerById
            var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            var actualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomerInfo.CustomerID = request.CustomerId;
            var serviceInfo = CreateDefaultObject.Create<ServicesInfo>();
            serviceInfo.BundleDefinition.BundleID = 1;
            serviceInfo.UnBilledBalance = 10;
            actualCustomerInfo.ServicesInfo = new List<ServicesInfo>() { serviceInfo };

            var customerProductAssn = CreateDefaultObject.Create<CustomerProductAssignment>();
            actualCustomerInfo.RevenueProductsInfo = new List<CustomerProductAssignment>() { customerProductAssn };

            actualCustomerInfo.ServicesInfo = new List<ServicesInfo>() { serviceInfo };

            var crmCustomersPromotionInfo = new CrmCustomersPromotionInfo();
            crmCustomersPromotionInfo.StartDate = startDate;
            crmCustomersPromotionInfo.EndDate = endDate;
            crmCustomersPromotionInfo.PromotionDetail = new RmPromotionPlanDetailInfo();
            crmCustomersPromotionInfo.PromotionDetail.SubServiceTypeId = 1;
            crmCustomersPromotionInfo.PromotionDetail.Limit = 500;
            crmCustomersPromotionInfo.PromotionDetail.RmPromotionPlanInfo =CreateDefaultObject.Create<RmPromotionPlanInfo>();
            crmCustomersPromotionInfo.CurrentLimit = 500;
            crmCustomersPromotionInfo.Active = true;


            var crmCustomersPromotionInfoNotActive = new CrmCustomersPromotionInfo();
            crmCustomersPromotionInfoNotActive.StartDate = startDate;
            crmCustomersPromotionInfoNotActive.EndDate = endDate;
            crmCustomersPromotionInfoNotActive.PromotionDetail = new RmPromotionPlanDetailInfo();
            crmCustomersPromotionInfoNotActive.PromotionDetail.SubServiceTypeId = 1;
            crmCustomersPromotionInfoNotActive.PromotionDetail.Limit = 20;
            crmCustomersPromotionInfoNotActive.Active = false;

            var crmCustomersPromotionInfoNextBillRunStartDate = new CrmCustomersPromotionInfo();
            crmCustomersPromotionInfoNextBillRunStartDate.StartDate = nextBillRunStartDate;
            crmCustomersPromotionInfoNextBillRunStartDate.EndDate = nextBillRunStartDate;
            crmCustomersPromotionInfoNextBillRunStartDate.PromotionDetail = new RmPromotionPlanDetailInfo();
            crmCustomersPromotionInfoNextBillRunStartDate.PromotionDetail.SubServiceTypeId = 1;
            crmCustomersPromotionInfoNextBillRunStartDate.PromotionDetail.Limit = 30;
            crmCustomersPromotionInfoNextBillRunStartDate.Active = true;


            var actualCustomerProduct = CreateDefaultObject.Create<Product>();
            actualCustomerProduct.Type = CreateDefaultObject.Create<ProductType>();
            actualCustomerProduct.Type.Description = "Commercial";
            actualCustomerProduct.Id = 1;
            actualCustomerProduct.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            actualCustomerProduct.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList = new List<RmPromotionPlanDetailInfo>() { crmCustomersPromotionInfo.PromotionDetail };




            actualCustomerInfo.Promotions = new List<CrmCustomersPromotionInfo>() { crmCustomersPromotionInfo, crmCustomersPromotionInfoNextBillRunStartDate, crmCustomersPromotionInfoNotActive };
            mockedRepoCustomerInfo.GetById(request.CustomerId).Returns(actualCustomerInfo);


            //Mocked GetPriorityBundleInfoFromBundleInfosMs
            var mockedGetPriorityBundleInfoFromBundleInfosMs = MockMicroService<GetPriorityBundleInfoFromBundleInfosRequest, GetPriorityBundleInfoFromBundleInfosResponse>();

            var actualBundleInfo = CreateDefaultObject.Create<BundleInfo>();
            actualBundleInfo.CreditLimit = 300;

            var getPriorityBundleInfoFromBundleInfosResponse = new GetPriorityBundleInfoFromBundleInfosResponse()
            {
                PriorityBundle = actualBundleInfo,
                ResultType = ResultTypes.Ok,
            };
            mockedGetPriorityBundleInfoFromBundleInfosMs.Process(Arg.Any<GetPriorityBundleInfoFromBundleInfosRequest>(), null)
                .Returns(getPriorityBundleInfoFromBundleInfosResponse);

            //Mocked getProductByProductIdMS
            var mockedGetProductByProductIdMS = MockMicroService<GetProductByProductIdRequest, GetProductByProductIdResponse>();

            var getProductByProductIdResponse = new GetProductByProductIdResponse()
            {
                Product = actualCustomerProduct,
                ResultType = ResultTypes.Ok,
            };

            mockedGetProductByProductIdMS.Process(Arg.Is<GetProductByProductIdRequest>(x => x.ProductId == 1), null)
                .Returns(getProductByProductIdResponse);



            CheckPurchaseProductForCustomerResponseDTO response;

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var bizop = new CheckPurchaseProductForCustomerBizOp(mockedPurchaseHelper);

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
            var actualProductCatalogDTOs = new List<ProductCatalogDTO>()
            {
                CreateDefaultObject.Create<ProductCatalogDTO>()
            };

            var request = new CheckPurchaseProductForCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                ForceCreditLimit = 100,
                ProductCatalogDTOs = actualProductCatalogDTOs,
                PurchaseDate = purchaseDate,
                TaxCategory = 1,
                CustomerId = 1,
            };


            //Mocked CheckProductListDependencyRelationsForCustomer
            var mockedCheckProductListDependencyRelationsForCustomerMs = MockMicroService<CheckProductListDependencyRelationsForCustomerRequest, CheckProductListDependencyRelationsForCustomerResponse>();
            var actualProductDependencyRelation = CreateDefaultObject.Create<ProductOfferingSpecificationOption>();
            actualProductDependencyRelation.RelatedProductOffering.OfferedProduct.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();

            var actualCheckProductListDependencyRelationsForCustomerResponse = new CheckProductListDependencyRelationsForCustomerResponse()
            {
                AreListRequirementsSatisfiedForCustomer = true,
                IsListCompatibleWithCustomerProducts = true,
                DeprovisionList = new List<ProductOfferingSpecificationOption>() { actualProductDependencyRelation },

                ResultType = ResultTypes.Ok,
            };



            mockedCheckProductListDependencyRelationsForCustomerMs.Process(Arg.Any<CheckProductListDependencyRelationsForCustomerRequest>(), null)
                .Returns(actualCheckProductListDependencyRelationsForCustomerResponse);



            //Mocked calculateNextBillRunDateMS
            var mockedCalculateNextBillRunDateMS = MockMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            mockedCalculateNextBillRunDateMS.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(), null)
               .Returns(new CalculateNextBillRunDateForBillCycleResponse()
               {
                   ResultType = ResultTypes.Ok,
                   NextBillRun = nextBillRunDate,
               });



            MockAbsctractBusinessOperation(request);

            //Mocked GetCustomerById
            var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            var actualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomerInfo.CustomerID = request.CustomerId;
            var serviceInfo = CreateDefaultObject.Create<ServicesInfo>();
            serviceInfo.BundleDefinition.BundleID = 1;
            serviceInfo.UnBilledBalance = 10;
            actualCustomerInfo.ServicesInfo = new List<ServicesInfo>() { serviceInfo };

            var customerProductAssn = CreateDefaultObject.Create<CustomerProductAssignment>();
            actualCustomerInfo.RevenueProductsInfo = new List<CustomerProductAssignment>() { customerProductAssn };


            actualCustomerInfo.ServicesInfo = new List<ServicesInfo>() { serviceInfo };

            var crmCustomersPromotionInfo = new CrmCustomersPromotionInfo();
            crmCustomersPromotionInfo.StartDate = startDate;
            crmCustomersPromotionInfo.EndDate = endDate;
            crmCustomersPromotionInfo.PromotionDetail = new RmPromotionPlanDetailInfo();
            crmCustomersPromotionInfo.PromotionDetail.SubServiceTypeId = 1;
            crmCustomersPromotionInfo.PromotionDetail.Limit = 10;
            crmCustomersPromotionInfo.Active = true;
            crmCustomersPromotionInfo.PromotionId = 10;

            var crmCustomersPromotionInfo2 = new CrmCustomersPromotionInfo();
            crmCustomersPromotionInfo2.StartDate = startDate;
            crmCustomersPromotionInfo2.EndDate = endDate;
            crmCustomersPromotionInfo2.PromotionDetail = new RmPromotionPlanDetailInfo();
            crmCustomersPromotionInfo2.PromotionDetail.SubServiceTypeId = 20;
            crmCustomersPromotionInfo2.PromotionDetail.Limit = 2000;
            crmCustomersPromotionInfo2.Active = true;
            crmCustomersPromotionInfo2.PromotionId = 20;

            var crmCustomersPromotionInfoNextBillRunStartDate = new CrmCustomersPromotionInfo();
            crmCustomersPromotionInfoNextBillRunStartDate.StartDate = nextBillRunStartDate;
            crmCustomersPromotionInfoNextBillRunStartDate.EndDate = nextBillRunStartDate;
            crmCustomersPromotionInfoNextBillRunStartDate.PromotionDetail = new RmPromotionPlanDetailInfo();
            crmCustomersPromotionInfoNextBillRunStartDate.PromotionDetail.SubServiceTypeId = 20;
            crmCustomersPromotionInfoNextBillRunStartDate.PromotionDetail.Limit = 3000;
            crmCustomersPromotionInfoNextBillRunStartDate.Active = true;
            crmCustomersPromotionInfoNextBillRunStartDate.PromotionId = 30;

            var actualCustomerProduct = CreateDefaultObject.Create<Product>();
            actualCustomerProduct.Type = CreateDefaultObject.Create<ProductType>();
            actualCustomerProduct.Type.Description = "Commercial";
            actualCustomerProduct.Id = 1;
            actualCustomerProduct.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            actualCustomerProduct.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList = new List<RmPromotionPlanDetailInfo>() { crmCustomersPromotionInfo.PromotionDetail };





            actualCustomerInfo.Promotions = new List<CrmCustomersPromotionInfo>() { crmCustomersPromotionInfo, crmCustomersPromotionInfoNextBillRunStartDate, crmCustomersPromotionInfo2 };
            mockedRepoCustomerInfo.GetById(request.CustomerId).Returns(actualCustomerInfo);

            //Mocked getProductByProductIdMS
            var mockedGetProductByProductIdMS = MockMicroService<GetProductByProductIdRequest, GetProductByProductIdResponse>();

            var getProductByProductIdResponse = new GetProductByProductIdResponse()
            {
                Product = actualCustomerProduct,
                ResultType = ResultTypes.Ok,
            };

            mockedGetProductByProductIdMS.Process(Arg.Is<GetProductByProductIdRequest>(x => x.ProductId == 1), null)
                .Returns(getProductByProductIdResponse);



            //Mocked GetPriorityBundleInfoFromBundleInfosMs
            var mockedGetPriorityBundleInfoFromBundleInfosMs = MockMicroService<GetPriorityBundleInfoFromBundleInfosRequest, GetPriorityBundleInfoFromBundleInfosResponse>();

            var actualBundleInfo = CreateDefaultObject.Create<BundleInfo>();
            actualBundleInfo.CreditLimit = 300;
            
            var getPriorityBundleInfoFromBundleInfosResponse = new GetPriorityBundleInfoFromBundleInfosResponse()
            {
                PriorityBundle = actualBundleInfo,
                ResultType = ResultTypes.Ok,
            };
            mockedGetPriorityBundleInfoFromBundleInfosMs.Process(Arg.Any<GetPriorityBundleInfoFromBundleInfosRequest>(), null)
                .Returns(getPriorityBundleInfoFromBundleInfosResponse);


            CheckPurchaseProductForCustomerResponseDTO response;

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var bizop = new CheckPurchaseProductForCustomerBizOp(mockedPurchaseHelper);

                response = bizop.ProcessFromCustomerModel(new NullValidator<CheckPurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<CheckPurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<CheckPurchaseProductForCustomerResponseDTO>(), request, FakeInvoker);

            }
            RepositoryManager.CloseConnection();

            Assert.IsTrue(response.IsCreditEnough == false);
            Assert.IsTrue(response.resultType == ResultTypes.Ok);

            request.ForceCreditLimit = null;

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var bizop = new CheckPurchaseProductForCustomerBizOp(mockedPurchaseHelper);

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
            var actualProductCatalogDTOs = new List<ProductCatalogDTO>()
            {
                CreateDefaultObject.Create<ProductCatalogDTO>()
            };

            var request = new CheckPurchaseProductForCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                ForceCreditLimit = 100,
                ProductCatalogDTOs = actualProductCatalogDTOs,
                PurchaseDate = purchaseDate,
                TaxCategory = 1,
                CustomerId = 1,
            };


            //Mocked CheckProductListDependencyRelationsForCustomer
            var mockedCheckProductListDependencyRelationsForCustomerMs = MockMicroService<CheckProductListDependencyRelationsForCustomerRequest, CheckProductListDependencyRelationsForCustomerResponse>();
            var actualProductDependencyRelation = CreateDefaultObject.Create<ProductOfferingSpecificationOption>();
            actualProductDependencyRelation.RelatedProductOffering.OfferedProduct.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();

            var actualCheckProductListDependencyRelationsForCustomerResponse = new CheckProductListDependencyRelationsForCustomerResponse()
            {
                AreListRequirementsSatisfiedForCustomer = true,
                IsListCompatibleWithCustomerProducts = true,
                DeprovisionList = new List<ProductOfferingSpecificationOption>() { actualProductDependencyRelation },

                ResultType = ResultTypes.Ok,
            };

            mockedCheckProductListDependencyRelationsForCustomerMs.Process(Arg.Any<CheckProductListDependencyRelationsForCustomerRequest>(), null)
                .Returns(actualCheckProductListDependencyRelationsForCustomerResponse);




            //Mocked calculateNextBillRunDateMS
            var mockedCalculateNextBillRunDateMS = MockMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            mockedCalculateNextBillRunDateMS.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(), null)
               .Returns(new CalculateNextBillRunDateForBillCycleResponse()
               {
                   ResultType = ResultTypes.Ok,
                   NextBillRun = nextBillRunDate,
               });



            MockAbsctractBusinessOperation(request);

            //Mocked GetCustomerById
            var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            var actualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomerInfo.CustomerID = request.CustomerId;
            var serviceInfo = CreateDefaultObject.Create<ServicesInfo>();
            serviceInfo.BundleDefinition.BundleID = 1;
            serviceInfo.UnBilledBalance = 10;
            actualCustomerInfo.ServicesInfo = new List<ServicesInfo>() { serviceInfo };

            var customerProductAssn = CreateDefaultObject.Create<CustomerProductAssignment>();
            actualCustomerInfo.RevenueProductsInfo = new List<CustomerProductAssignment>() { customerProductAssn };

            actualCustomerInfo.ServicesInfo = new List<ServicesInfo>() { serviceInfo };

            var crmCustomersPromotionInfo = new CrmCustomersPromotionInfo();
            crmCustomersPromotionInfo.StartDate = startDate;
            crmCustomersPromotionInfo.EndDate = endDate;
            crmCustomersPromotionInfo.PromotionDetail = new RmPromotionPlanDetailInfo();
            crmCustomersPromotionInfo.PromotionDetail.SubServiceTypeId = 1;
            crmCustomersPromotionInfo.PromotionDetail.Limit = 10;
            crmCustomersPromotionInfo.Active = true;
            crmCustomersPromotionInfo.PromotionId = 10;
            crmCustomersPromotionInfo.PromotionDetail.PromotionPlanDetailId = 1;

            var crmCustomersPromotionInfo2 = new CrmCustomersPromotionInfo();
            crmCustomersPromotionInfo2.StartDate = startDate;
            crmCustomersPromotionInfo2.EndDate = endDate;
            crmCustomersPromotionInfo2.PromotionDetail = new RmPromotionPlanDetailInfo();
            crmCustomersPromotionInfo2.PromotionDetail.SubServiceTypeId = 1;
            crmCustomersPromotionInfo2.PromotionDetail.Limit = 2000;
            crmCustomersPromotionInfo2.Active = true;
            crmCustomersPromotionInfo2.PromotionId = 20;
            
            actualCustomerInfo.Promotions = new List<CrmCustomersPromotionInfo>() { crmCustomersPromotionInfo, crmCustomersPromotionInfo2 };

            var customerProductInfo = CreateDefaultObject.Create<ProductInfo>();
            customerProductInfo.ProductID = 1;

            var customerProductInfo2 = CreateDefaultObject.Create<ProductInfo>();
            customerProductInfo2.ProductID = 2;

            actualCustomerInfo.ProductsInfo = new List<ProductInfo>();
            actualCustomerInfo.ProductsInfo.Add(customerProductInfo);
            actualCustomerInfo.ProductsInfo.Add(customerProductInfo2);
            mockedRepoCustomerInfo.GetById(request.CustomerId).Returns(actualCustomerInfo);

            var actualCustomerProduct = CreateDefaultObject.Create<Product>();
            actualCustomerProduct.Type = CreateDefaultObject.Create<ProductType>();
            actualCustomerProduct.Type.Description = "Commercial";
            actualCustomerProduct.Id = 1;
            actualCustomerProduct.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            actualCustomerProduct.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList = new List<RmPromotionPlanDetailInfo>() { crmCustomersPromotionInfo.PromotionDetail };

            var actualCustomerProduct2 = CreateDefaultObject.Create<Product>();
            actualCustomerProduct2.Type = CreateDefaultObject.Create<ProductType>();
            actualCustomerProduct2.Type.Description = "NonCommercial";
            actualCustomerProduct2.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            actualCustomerProduct2.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList = new List<RmPromotionPlanDetailInfo>() { crmCustomersPromotionInfo2.PromotionDetail };
            actualCustomerProduct.Id = 2;
            


            //Mocked getProductByProductIdMS
            var mockedGetProductByProductIdMS = MockMicroService<GetProductByProductIdRequest, GetProductByProductIdResponse>();

            var getProductByProductIdResponse = new GetProductByProductIdResponse()
            {
                Product = actualCustomerProduct,
                ResultType = ResultTypes.Ok,
            };

            mockedGetProductByProductIdMS.Process(Arg.Is<GetProductByProductIdRequest>(x => x.ProductId == 1), null)
                .Returns(getProductByProductIdResponse);


            var getProductByProductIdResponse2 = new GetProductByProductIdResponse()
            {
                Product = actualCustomerProduct2,
                ResultType = ResultTypes.Ok,
            };
            mockedGetProductByProductIdMS.Process(Arg.Is<GetProductByProductIdRequest>(x => x.ProductId == 2), null)
                .Returns(getProductByProductIdResponse2);

            //Mocked GetPriorityBundleInfoFromBundleInfosMs
            var mockedGetPriorityBundleInfoFromBundleInfosMs = MockMicroService<GetPriorityBundleInfoFromBundleInfosRequest, GetPriorityBundleInfoFromBundleInfosResponse>();

            var actualBundleInfo = CreateDefaultObject.Create<BundleInfo>();
            actualBundleInfo.CreditLimit = 300;

            var getPriorityBundleInfoFromBundleInfosResponse = new GetPriorityBundleInfoFromBundleInfosResponse()
            {
                PriorityBundle = actualBundleInfo,
                ResultType = ResultTypes.Ok,
            };
            mockedGetPriorityBundleInfoFromBundleInfosMs.Process(Arg.Any<GetPriorityBundleInfoFromBundleInfosRequest>(), null)
                .Returns(getPriorityBundleInfoFromBundleInfosResponse);


            CheckPurchaseProductForCustomerResponseDTO response;

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var bizop = new CheckPurchaseProductForCustomerBizOp(mockedPurchaseHelper);

                response = bizop.ProcessFromCustomerModel(new NullValidator<CheckPurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<CheckPurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<CheckPurchaseProductForCustomerResponseDTO>(), request, FakeInvoker);

            }
            RepositoryManager.CloseConnection();

            Assert.IsTrue(response.IsCreditEnough == false);
            Assert.IsTrue(response.resultType == ResultTypes.Ok);

            request.ForceCreditLimit = null;

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var bizop = new CheckPurchaseProductForCustomerBizOp(mockedPurchaseHelper);

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
            var actualProductCatalogDTOs = new List<ProductCatalogDTO>()
            {
                CreateDefaultObject.Create<ProductCatalogDTO>()
            };

            var request = new CheckPurchaseProductForCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                ForceCreditLimit = null,
                ProductCatalogDTOs = actualProductCatalogDTOs,
                PurchaseDate = purchaseDate,
                TaxCategory = 1,
                CustomerId = 1,
            };


            //Mocked calculateNextBillRunDateMS
            var mockedCalculateNextBillRunDateMS = MockMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            mockedCalculateNextBillRunDateMS.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(), null)
               .Returns(new CalculateNextBillRunDateForBillCycleResponse()
               {
                   ResultType = ResultTypes.Ok,
                   NextBillRun = nextBillRunDate,
               });


            MockAbsctractBusinessOperation(request);

            actualListTuplesPurchaseProducts = new List<Tuple<ProductOffering, ProductChargeOption>>();
            var actualProduct = CreateDefaultObject.Create<ProductOffering>();
            var actualProductOrig = CreateDefaultObject.Create<Product>();
            actualProduct.OfferedProduct = actualProductOrig;
            actualProduct.Id = 1;
            actualProductOrig.Type = CreateDefaultObject.Create<ProductType>();
            actualProductOrig.Type.Description = "Commercial";
            actualProductOrig.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            var actualRmPromotionPlanDetailInfo = new RmPromotionPlanDetailInfo();
            actualRmPromotionPlanDetailInfo.Limit = 10;
            actualRmPromotionPlanDetailInfo.StartDate = new DateTime(2013, 5, 6);
            actualRmPromotionPlanDetailInfo.EndDate = null;
            actualRmPromotionPlanDetailInfo.SubServiceTypeId = 1;
            var actualRmPromotionPlanDetailInfo2 = new RmPromotionPlanDetailInfo();
            actualRmPromotionPlanDetailInfo2.Limit = 20;
            actualRmPromotionPlanDetailInfo2.StartDate = new DateTime(2013, 5, 6);
            actualRmPromotionPlanDetailInfo2.EndDate = new DateTime(2017, 5, 6);
            actualRmPromotionPlanDetailInfo2.SubServiceTypeId = 1;

            var actualRmPromotionPlanDetailInfo3 = new RmPromotionPlanDetailInfo();
            actualRmPromotionPlanDetailInfo3.Limit = 20;
            actualRmPromotionPlanDetailInfo3.StartDate = nextBillRunStartDate;
            actualRmPromotionPlanDetailInfo3.EndDate = new DateTime(2017, 5, 6);
            actualRmPromotionPlanDetailInfo3.SubServiceTypeId = 1;

            actualProductOrig.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList = new List<RmPromotionPlanDetailInfo>() { actualRmPromotionPlanDetailInfo3, actualRmPromotionPlanDetailInfo, actualRmPromotionPlanDetailInfo2 };

            var actualCommercialProduct = CreateDefaultObject.Create<ProductOffering>();
            actualCommercialProduct.Id = 2;
            actualProductOrig.Type = CreateDefaultObject.Create<ProductType>();
            actualProductOrig.Type.Description = "Commercial";

            var actualProductChargeOption = new ProductChargeOption();
            var actualChargeRecurring = new ChargeRecurring();
            var actualChargePrice = new ChargePrice();
            actualChargePrice.StartDate = new DateTime(2013, 5, 6);
            actualChargePrice.EndDate = new DateTime(2017, 5, 6);
            actualChargePrice.Amount = 100;

            var actualChargePrice2 = new ChargePrice();
            actualChargePrice2.StartDate = new DateTime(2013, 5, 6);
            actualChargePrice2.EndDate = new DateTime(2017, 5, 6);
            actualChargePrice2.Amount = 100;


            actualChargeRecurring.Prices.Add(actualChargePrice);
            actualChargeRecurring.Prices.Add(actualChargePrice2);

            actualProductChargeOption.Charges = new List<Charge>() { actualChargeRecurring };
            actualProductChargeOption.StartDate = new DateTime(2013, 5, 6);
            actualProductChargeOption.EndDate = new DateTime(2016, 3, 3);

            var actualCommercialProductChargeOption = new ProductChargeOption();
            actualCommercialProductChargeOption.Charges = new List<Charge>() { actualChargeRecurring };
            actualCommercialProductChargeOption.StartDate = nextBillRunDate;
            actualCommercialProductChargeOption.EndDate = nextBillRunDate;

            actualListTuplesPurchaseProducts.Add(Tuple.Create(actualProduct, actualProductChargeOption));
            actualListTuplesPurchaseProducts.Add(Tuple.Create(actualCommercialProduct, actualCommercialProductChargeOption));

            //Mocked getProductByProductIdMS
            var mockedGetProductByProductIdMS = MockMicroService<GetProductByProductIdRequest, GetProductByProductIdResponse>();

            var getProductByProductIdResponse = new GetProductByProductIdResponse()
            {
                Product = actualProduct.OfferedProduct,
                ResultType = ResultTypes.Ok,
            };

            mockedGetProductByProductIdMS.Process(Arg.Is<GetProductByProductIdRequest>(x => x.ProductId == 1), null)
                .Returns(getProductByProductIdResponse);


            var getProductByProductIdResponse2 = new GetProductByProductIdResponse()
            {
                Product = actualCommercialProduct.OfferedProduct,
                ResultType = ResultTypes.Ok,
            };
            mockedGetProductByProductIdMS.Process(Arg.Is<GetProductByProductIdRequest>(x => x.ProductId == 2), null)
                .Returns(getProductByProductIdResponse2);

            //Mocked GetPriorityBundleInfoFromBundleInfosMs
            var mockedGetPriorityBundleInfoFromBundleInfosMs = MockMicroService<GetPriorityBundleInfoFromBundleInfosRequest, GetPriorityBundleInfoFromBundleInfosResponse>();

            var actualBundleInfo = CreateDefaultObject.Create<BundleInfo>();
            actualBundleInfo.CreditLimit = 300;

            var getPriorityBundleInfoFromBundleInfosResponse = new GetPriorityBundleInfoFromBundleInfosResponse()
            {
                PriorityBundle = actualBundleInfo,
                ResultType = ResultTypes.Ok,
            };
            mockedGetPriorityBundleInfoFromBundleInfosMs.Process(Arg.Any<GetPriorityBundleInfoFromBundleInfosRequest>(), null)
                .Returns(getPriorityBundleInfoFromBundleInfosResponse);

            //Mocked GetCustomerById
            var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            var actualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomerInfo.CustomerID = request.CustomerId;
            var serviceInfo = CreateDefaultObject.Create<ServicesInfo>();
            serviceInfo.BundleDefinition.BundleID = 1;
            serviceInfo.UnBilledBalance = 10;

            actualCustomerInfo.ServicesInfo = new List<ServicesInfo>() { serviceInfo };

            var customerProductAssn = CreateDefaultObject.Create<CustomerProductAssignment>();
            actualCustomerInfo.RevenueProductsInfo = new List<CustomerProductAssignment>() { customerProductAssn };


            actualCustomerInfo.ServicesInfo = new List<ServicesInfo>() { serviceInfo };

            var crmCustomersPromotionInfo = new CrmCustomersPromotionInfo();
            crmCustomersPromotionInfo.StartDate = startDate;
            crmCustomersPromotionInfo.EndDate = endDate;
            crmCustomersPromotionInfo.PromotionDetail = new RmPromotionPlanDetailInfo();
            crmCustomersPromotionInfo.PromotionDetail.SubServiceTypeId = 1;
            crmCustomersPromotionInfo.PromotionDetail.Limit = 10;
            crmCustomersPromotionInfo.Active = true;
            crmCustomersPromotionInfo.PromotionId = 10;

            var crmCustomersPromotionInfo2 = new CrmCustomersPromotionInfo();
            crmCustomersPromotionInfo2.StartDate = startDate;
            crmCustomersPromotionInfo2.EndDate = endDate;
            crmCustomersPromotionInfo2.PromotionDetail = new RmPromotionPlanDetailInfo();
            crmCustomersPromotionInfo2.PromotionDetail.SubServiceTypeId = 20;
            crmCustomersPromotionInfo2.PromotionDetail.Limit = 2000;
            crmCustomersPromotionInfo2.Active = true;
            crmCustomersPromotionInfo2.PromotionId = 20;

            var crmCustomersPromotionInfoNextBillRunStartDate = new CrmCustomersPromotionInfo();
            crmCustomersPromotionInfoNextBillRunStartDate.StartDate = nextBillRunStartDate;
            crmCustomersPromotionInfoNextBillRunStartDate.EndDate = nextBillRunStartDate;
            crmCustomersPromotionInfoNextBillRunStartDate.PromotionDetail = new RmPromotionPlanDetailInfo();
            crmCustomersPromotionInfoNextBillRunStartDate.PromotionDetail.SubServiceTypeId = 20;
            crmCustomersPromotionInfoNextBillRunStartDate.PromotionDetail.Limit = 3000;
            crmCustomersPromotionInfoNextBillRunStartDate.Active = true;
            crmCustomersPromotionInfoNextBillRunStartDate.PromotionId = 30;

            actualCustomerInfo.Promotions = new List<CrmCustomersPromotionInfo>() { crmCustomersPromotionInfo, crmCustomersPromotionInfoNextBillRunStartDate, crmCustomersPromotionInfo2 };
            mockedRepoCustomerInfo.GetById(request.CustomerId).Returns(actualCustomerInfo);



            //Mocked CheckProductListDependencyRelationsForCustomer
            var mockedCheckProductListDependencyRelationsForCustomerMs = MockMicroService<CheckProductListDependencyRelationsForCustomerRequest, CheckProductListDependencyRelationsForCustomerResponse>();
            var actualCheckProductListDependencyRelationsForCustomerRequest =
                Arg.Is<CheckProductListDependencyRelationsForCustomerRequest>(x => x.CustomerProducts.Any(y => y.Id == 2) && x.ProductsToPurchase.Any(y => y.Id == 2));


            var actualProductDependencyRelation = CreateDefaultObject.Create<ProductOfferingSpecificationOption>();
            actualProductDependencyRelation.RelatedProductOffering.OfferedProduct.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();

            var actualCheckProductListDependencyRelationsForCustomerResponse = new CheckProductListDependencyRelationsForCustomerResponse()
            {
                AreListRequirementsSatisfiedForCustomer = true,
                IsListCompatibleWithCustomerProducts = false,
                DeprovisionList = new List<ProductOfferingSpecificationOption>() { actualProductDependencyRelation },

                ResultType = ResultTypes.Ok,
            };



            mockedCheckProductListDependencyRelationsForCustomerMs.Process(actualCheckProductListDependencyRelationsForCustomerRequest, null)
                .Returns(actualCheckProductListDependencyRelationsForCustomerResponse);

            var actualCheckProductListDependencyRelationsForCustomerRequest2 =
              Arg.Is<CheckProductListDependencyRelationsForCustomerRequest>(x => x.CustomerProducts.Count == 0);
            actualCheckProductListDependencyRelationsForCustomerResponse.IsListCompatibleWithCustomerProducts = true;
            mockedCheckProductListDependencyRelationsForCustomerMs.Process(actualCheckProductListDependencyRelationsForCustomerRequest2, null)
               .Returns(actualCheckProductListDependencyRelationsForCustomerResponse);

            var actualCheckProductListDependencyRelationsForCustomerRequest3 =
              Arg.Is<CheckProductListDependencyRelationsForCustomerRequest>(x => x.ProductsToPurchase.Any(y => y.Id == 1));
            
            mockedCheckProductListDependencyRelationsForCustomerMs.Process(actualCheckProductListDependencyRelationsForCustomerRequest3, null)
               .Returns(actualCheckProductListDependencyRelationsForCustomerResponse);

            CheckPurchaseProductForCustomerResponseDTO response;

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var bizop = new CheckPurchaseProductForCustomerBizOp(mockedPurchaseHelper);

                response = bizop.ProcessFromCustomerModel(new NullValidator<CheckPurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<CheckPurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<CheckPurchaseProductForCustomerResponseDTO>(), request, FakeInvoker);

            }
            RepositoryManager.CloseConnection();

            Assert.IsTrue(response.IsListCompatibleWithCustomerProducts);
            Assert.IsTrue(response.resultType == ResultTypes.Ok);
            Assert.IsNotNull(response.Subscription);





            actualListTuplesPurchaseProducts = new List<Tuple<ProductOffering, ProductChargeOption>>();
            actualProduct = CreateDefaultObject.Create<ProductOffering>();
            actualProduct.Id = 1;
            actualProductOrig.Type = CreateDefaultObject.Create<ProductType>();
            actualProductOrig.Type.Description = "Commercial";
            actualProductOrig.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            actualRmPromotionPlanDetailInfo = new RmPromotionPlanDetailInfo();
            actualRmPromotionPlanDetailInfo.Limit = 10;
            actualRmPromotionPlanDetailInfo.StartDate = new DateTime(2013, 5, 6);
            actualRmPromotionPlanDetailInfo.EndDate = null;
            actualRmPromotionPlanDetailInfo.SubServiceTypeId = 1;
            actualRmPromotionPlanDetailInfo2 = new RmPromotionPlanDetailInfo();
            actualRmPromotionPlanDetailInfo2.Limit = 20;
            actualRmPromotionPlanDetailInfo2.StartDate = new DateTime(2013, 5, 6);
            actualRmPromotionPlanDetailInfo2.EndDate = new DateTime(2017, 5, 6);
            actualRmPromotionPlanDetailInfo2.SubServiceTypeId = 1;

            actualRmPromotionPlanDetailInfo3 = new RmPromotionPlanDetailInfo();
            actualRmPromotionPlanDetailInfo3.Limit = 20;
            actualRmPromotionPlanDetailInfo3.StartDate = nextBillRunStartDate;
            actualRmPromotionPlanDetailInfo3.EndDate = new DateTime(2017, 5, 6);
            actualRmPromotionPlanDetailInfo3.SubServiceTypeId = 1;

            actualProductOrig.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList = new List<RmPromotionPlanDetailInfo>() { actualRmPromotionPlanDetailInfo3, actualRmPromotionPlanDetailInfo, actualRmPromotionPlanDetailInfo2 };

            actualCommercialProduct = CreateDefaultObject.Create<ProductOffering>();
            actualCommercialProduct.Id = 2;
            actualProductOrig.Type = CreateDefaultObject.Create<ProductType>();
            actualProductOrig.Type.Description = "Commercial";

            actualProductChargeOption = new ProductChargeOption();
            actualChargeRecurring = new ChargeRecurring();
            actualChargePrice = new ChargePrice();
            actualChargePrice.StartDate = new DateTime(2013, 5, 6);
            actualChargePrice.EndDate = new DateTime(2017, 5, 6);
            actualChargePrice.Amount = 100;

            actualChargePrice2 = new ChargePrice();
            actualChargePrice2.StartDate = new DateTime(2013, 5, 6);
            actualChargePrice2.EndDate = new DateTime(2017, 5, 6);
            actualChargePrice2.Amount = 100;


            actualChargeRecurring.Prices.Add(actualChargePrice);
            actualChargeRecurring.Prices.Add(actualChargePrice2);

            actualProductChargeOption.Charges = new List<Charge>() { actualChargeRecurring };
            actualProductChargeOption.StartDate = new DateTime(2013, 5, 6);
            actualProductChargeOption.EndDate = new DateTime(2016, 3, 3);

            actualCommercialProductChargeOption = new ProductChargeOption();
            actualCommercialProductChargeOption.Charges = new List<Charge>() { actualChargeRecurring };
            actualCommercialProductChargeOption.StartDate = new DateTime(2013, 5, 6); 
            actualCommercialProductChargeOption.EndDate = nextBillRunDate;
            

            actualListTuplesPurchaseProducts.Add(Tuple.Create(actualProduct, actualProductChargeOption));
            actualListTuplesPurchaseProducts.Add(Tuple.Create(actualCommercialProduct, actualCommercialProductChargeOption));

            actualCheckProductListDependencyRelationsForCustomerRequest =
                Arg.Is<CheckProductListDependencyRelationsForCustomerRequest>(x => x.CustomerProducts.Any(y => y.Id == 1) && x.ProductsToPurchase.Any(y => y.Id == 1));



            mockedCheckProductListDependencyRelationsForCustomerMs.Process(actualCheckProductListDependencyRelationsForCustomerRequest, null)
                .Returns(actualCheckProductListDependencyRelationsForCustomerResponse);

            //Mocked PurchaseHelper.GetProductsAndChargesOptions
            mockedPurchaseHelper = Substitute.ForPartsOf<PurchaseHelper>();
            mockedPurchaseHelper.GetProductsAndChargesOptions(Arg.Any<List<ProductCatalogDTO>>()).Returns(actualListTuplesPurchaseProducts);

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var bizop = new CheckPurchaseProductForCustomerBizOp(mockedPurchaseHelper);

                response = bizop.ProcessFromCustomerModel(new NullValidator<CheckPurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<CheckPurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<CheckPurchaseProductForCustomerResponseDTO>(), request, FakeInvoker);

            }
            RepositoryManager.CloseConnection();

            Assert.IsTrue(!response.IsListCompatibleWithCustomerProducts);
            
        }

        [Test()]
        public void CheckPurchaseProductForCustomerBizOpWithCustomerPromotionUnlimtedConfiguration_NotCountUnlimitedPromotion_ReturnOK()
        {
            var actualProductCatalogDTOs = new List<ProductCatalogDTO>() { CreateDefaultObject.Create<ProductCatalogDTO>() };

            var request = new CheckPurchaseProductForCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                ForceCreditLimit = 100,
                ProductCatalogDTOs = actualProductCatalogDTOs,
                PurchaseDate = purchaseDate,
                TaxCategory = 1,
                CustomerId = 1,
            };


            //Mocked CheckProductListDependencyRelationsForCustomer
            var mockedCheckProductListDependencyRelationsForCustomerMs = MockMicroService<CheckProductListDependencyRelationsForCustomerRequest, CheckProductListDependencyRelationsForCustomerResponse>();

            var actualProductDependencyRelation = CreateDefaultObject.Create<ProductOfferingSpecificationOption>();
            actualProductDependencyRelation.RelatedProductOffering.OfferedProduct.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();

            var actualCheckProductListDependencyRelationsForCustomerResponse = new CheckProductListDependencyRelationsForCustomerResponse()
            {
                AreListRequirementsSatisfiedForCustomer = true,
                IsListCompatibleWithCustomerProducts = true,
                DeprovisionList = new List<ProductOfferingSpecificationOption>() { actualProductDependencyRelation },

                ResultType = ResultTypes.Ok,
            };
            mockedCheckProductListDependencyRelationsForCustomerMs.Process(Arg.Any<CheckProductListDependencyRelationsForCustomerRequest>(), null)
                .Returns(actualCheckProductListDependencyRelationsForCustomerResponse);



            actualListTuplesPurchaseProducts = new List<Tuple<ProductOffering, ProductChargeOption>>();

            var actualProduct = CreateDefaultObject.Create<ProductOffering>();
            var actualProductOrig = CreateDefaultObject.Create<Product>();
            actualProduct.OfferedProduct = actualProductOrig;
            actualProductOrig.Type = CreateDefaultObject.Create<ProductType>();
            actualProductOrig.Type.Description = "Commercial";
            actualProductOrig.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            var actualRmPromotionPlanDetailInfo = new RmPromotionPlanDetailInfo();
            actualRmPromotionPlanDetailInfo.Limit = 50;
            actualRmPromotionPlanDetailInfo.StartDate = new DateTime(2013, 5, 6);
            actualRmPromotionPlanDetailInfo.EndDate = null;
            actualRmPromotionPlanDetailInfo.SubServiceTypeId = 1;

            actualProductOrig.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList = new List<RmPromotionPlanDetailInfo>() { actualRmPromotionPlanDetailInfo };



            var actualProductChargeOption = new ProductChargeOption();
            var actualChargeRecurring = new ChargeRecurring();
            var actualChargePrice = new ChargePrice();
            actualChargePrice.StartDate = new DateTime(2013, 5, 6);
            actualChargePrice.EndDate = new DateTime(2017, 5, 6);
            actualChargePrice.Amount = 10;

            var actualChargePrice2 = new ChargePrice();
            actualChargePrice2.StartDate = new DateTime(2013, 5, 6);
            actualChargePrice2.EndDate = new DateTime(2017, 5, 6);
            actualChargePrice2.Amount = 10;

            actualChargeRecurring.Prices.Add(actualChargePrice);
            actualChargeRecurring.Prices.Add(actualChargePrice2);
            actualProductChargeOption.Charges = new List<Charge>() { actualChargeRecurring };
            actualListTuplesPurchaseProducts.Add(Tuple.Create(actualProduct, actualProductChargeOption));

            //Mocked PurchaseHelper.GetProductsAndChargesOptions
            mockedPurchaseHelper = Substitute.ForPartsOf<PurchaseHelper>();
            mockedPurchaseHelper.GetProductsAndChargesOptions(Arg.Any<List<ProductCatalogDTO>>()).Returns(actualListTuplesPurchaseProducts);

            //Mocked calculateNextBillRunDateMS
            var mockedCalculateNextBillRunDateMS = MockMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            mockedCalculateNextBillRunDateMS.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(), null)
               .Returns(new CalculateNextBillRunDateForBillCycleResponse()
               {
                   ResultType = ResultTypes.Ok,

               });



            MockAbsctractBusinessOperation(request);

            //Mocked GetCustomerById
            var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            var actualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomerInfo.CustomerID = request.CustomerId;
            var serviceInfo = CreateDefaultObject.Create<ServicesInfo>();
            serviceInfo.BundleDefinition.BundleID = 1;
            serviceInfo.UnBilledBalance = 10;
            actualCustomerInfo.ServicesInfo = new List<ServicesInfo>() { serviceInfo };

            var customerProductAssn = CreateDefaultObject.Create<CustomerProductAssignment>();
            actualCustomerInfo.RevenueProductsInfo = new List<CustomerProductAssignment>() { customerProductAssn };

            actualCustomerInfo.ServicesInfo = new List<ServicesInfo>() { serviceInfo };

      

            var crmCustomersPromotionInfo = new CrmCustomersPromotionInfo();
            crmCustomersPromotionInfo.StartDate = startDate;
            crmCustomersPromotionInfo.EndDate = endDate; 
            crmCustomersPromotionInfo.CurrentLimit = 150;
            crmCustomersPromotionInfo.Active = true;
            crmCustomersPromotionInfo.PromotionDetail = new RmPromotionPlanDetailInfo();
            crmCustomersPromotionInfo.PromotionDetail.PromotionPlanDetailId = 99;
            crmCustomersPromotionInfo.PromotionDetail.SubServiceTypeId = 1;
            crmCustomersPromotionInfo.PromotionDetail.Limit = 500;
            crmCustomersPromotionInfo.PromotionDetail.RmPromotionPlanInfo = new RmPromotionPlanInfo();
            crmCustomersPromotionInfo.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 99;

            var crmCustomersPromotionInfoUnlimited = new CrmCustomersPromotionInfo();
            crmCustomersPromotionInfoUnlimited.StartDate = startDate;
            crmCustomersPromotionInfoUnlimited.EndDate = endDate;
            crmCustomersPromotionInfoUnlimited.CurrentLimit = 300;
            crmCustomersPromotionInfoUnlimited.Active = true;
            crmCustomersPromotionInfoUnlimited.PromotionDetail = new RmPromotionPlanDetailInfo();
            crmCustomersPromotionInfoUnlimited.PromotionDetail.PromotionPlanDetailId = 100;
            crmCustomersPromotionInfoUnlimited.PromotionDetail.SubServiceTypeId = 1;
            crmCustomersPromotionInfoUnlimited.PromotionDetail.Limit = 500;
            crmCustomersPromotionInfoUnlimited.PromotionDetail.RmPromotionPlanInfo = new RmPromotionPlanInfo();
            crmCustomersPromotionInfoUnlimited.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 100;

            var crmCustomersPromotionInfoNotActive = new CrmCustomersPromotionInfo();
            crmCustomersPromotionInfoNotActive.StartDate = startDate;
            crmCustomersPromotionInfoNotActive.EndDate = endDate;
            crmCustomersPromotionInfoNotActive.PromotionDetail = new RmPromotionPlanDetailInfo();
            crmCustomersPromotionInfoNotActive.PromotionDetail.SubServiceTypeId = 1;
            crmCustomersPromotionInfoNotActive.PromotionDetail.Limit = 20;
            crmCustomersPromotionInfoNotActive.Active = false;

            var crmCustomersPromotionInfoNextBillRunStartDate = new CrmCustomersPromotionInfo();
            crmCustomersPromotionInfoNextBillRunStartDate.StartDate = nextBillRunStartDate;
            crmCustomersPromotionInfoNextBillRunStartDate.EndDate = null;
            crmCustomersPromotionInfoNextBillRunStartDate.PromotionDetail = new RmPromotionPlanDetailInfo();
            crmCustomersPromotionInfoNextBillRunStartDate.PromotionDetail.SubServiceTypeId = 1;
            crmCustomersPromotionInfoNextBillRunStartDate.PromotionDetail.Limit = 30;
            crmCustomersPromotionInfoNextBillRunStartDate.Active = true;


            var actualCustomerProduct = CreateDefaultObject.Create<Product>();
            actualCustomerProduct.Type = CreateDefaultObject.Create<ProductType>();
            actualCustomerProduct.Type.Description = "Commercial";
            actualCustomerProduct.Id = 1;
            actualCustomerProduct.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            actualCustomerProduct.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList = new List<RmPromotionPlanDetailInfo>() { crmCustomersPromotionInfo.PromotionDetail, crmCustomersPromotionInfoUnlimited.PromotionDetail };




            actualCustomerInfo.Promotions = new List<CrmCustomersPromotionInfo>()
            {
                crmCustomersPromotionInfoUnlimited,crmCustomersPromotionInfo, crmCustomersPromotionInfoNextBillRunStartDate, crmCustomersPromotionInfoNotActive
            };
            mockedRepoCustomerInfo.GetById(request.CustomerId).Returns(actualCustomerInfo);


            //Mocked GetPriorityBundleInfoFromBundleInfosMs
            var mockedGetPriorityBundleInfoFromBundleInfosMs = MockMicroService<GetPriorityBundleInfoFromBundleInfosRequest, GetPriorityBundleInfoFromBundleInfosResponse>();

            var actualBundleInfo = CreateDefaultObject.Create<BundleInfo>();
            actualBundleInfo.CreditLimit = 300;

            var getPriorityBundleInfoFromBundleInfosResponse = new GetPriorityBundleInfoFromBundleInfosResponse()
            {
                PriorityBundle = actualBundleInfo,
                ResultType = ResultTypes.Ok,
            };
            mockedGetPriorityBundleInfoFromBundleInfosMs.Process(Arg.Any<GetPriorityBundleInfoFromBundleInfosRequest>(), null)
                .Returns(getPriorityBundleInfoFromBundleInfosResponse);

            //Mocked getProductByProductIdMS
            var mockedGetProductByProductIdMS = MockMicroService<GetProductByProductIdRequest, GetProductByProductIdResponse>();

            var getProductByProductIdResponse = new GetProductByProductIdResponse()
            {
                Product = actualCustomerProduct,
                ResultType = ResultTypes.Ok,
            };

            mockedGetProductByProductIdMS.Process(Arg.Is<GetProductByProductIdRequest>(x => x.ProductId == 1), null)
                .Returns(getProductByProductIdResponse);



            CheckPurchaseProductForCustomerResponseDTO response;

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var bizop = new CheckPurchaseProductForCustomerBizOp(mockedPurchaseHelper);

                response = bizop.ProcessFromCustomerModel(new NullValidator<CheckPurchaseProductForCustomerRequestDTO>(), new SameTypeConverter<CheckPurchaseProductForCustomerRequestDTO>(),
                    new SameTypeConverter<CheckPurchaseProductForCustomerResponseDTO>(), request, FakeInvoker);

            }
            RepositoryManager.CloseConnection();

            Assert.IsTrue(!response.IsLimitReached);
            Assert.IsTrue(response.resultType == ResultTypes.Ok);
        }
    }
}
