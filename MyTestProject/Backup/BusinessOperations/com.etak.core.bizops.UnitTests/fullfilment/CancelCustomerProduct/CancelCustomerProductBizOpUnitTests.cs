using System;
using System.Collections.Generic;
using com.etak.core.bizops.fullfilment.CancelCustomerProduct;
using com.etak.core.customer.message.CancelCustomerChargeSchedule;
using com.etak.core.customer.message.GetActiveCustomerAccountAssociationByDate;
using com.etak.core.customer.message.GetChargeSchedulesByCustomer;
using com.etak.core.customer.message.GetCustomerChargesByCustomerId;
using com.etak.core.GSMSubscription.messages.CancelCustomerProductAssignment;
using com.etak.core.GSMSubscription.messages.GetCustomerProductAssignmentById;
using com.etak.core.model;
using com.etak.core.model.dto;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;
using com.etak.core.model.subscription.catalog;
using com.etak.core.product.message.CalculateNextBillRunDateForBillCycle;
using com.etak.core.product.message.CancelCustomerPackages;
using com.etak.core.promotion.messages.CancelCustomersPromotion;
using com.etak.core.promotion.messages.CreateCustomerPromotionLogInfo;
using com.etak.core.promotion.messages.CreateLogPromotion;
using com.etak.core.promotion.messages.GetCustomerPromotionOperationLogByCustomerIDAndPromotion;
using com.etak.core.repository.crm.subscription.catalog;
using com.etak.core.service.messages.CancelServicesInfo;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.fullfilment.CancelCustomerProduct
{
    [TestFixture]
    internal class CancelCustomerProductBizOpUnitTests :
        AbstractSinglePhaseOrderProcessorTest
            <CancelCustomerProductBizOp, CancelCustomerProductRequestDTO, CancelCustomerProductResponseDTO,
                CancelCustomerProductRequestInternal, CancelCustomerProductResponseInternal, CancelCustomerProductOrder>
    {
        private DateTime cancelDate = DateTime.Now.AddDays(1);
        private DateTime startDate = new DateTime(2014, 5, 6);
        private DateTime startDateClosest = new DateTime(2014, 5, 5);
        
        private DateTime endDate = new DateTime(2016, 5, 6);
        private DateTime nextBillRunDate = new DateTime(2015, 5, 6);

        private int customerid = 99;
        private int productid = 999;



        #region Define Mock Repo

        private IProductOfferingRepository<ProductOffering> _mockProductOfferingRepo;

        #endregion


        [TestFixtureSetUp]
        public static void Initialize()
        {
            FakeSessionFactorySingleton.Init();
        }

        [Test()]
        public void CancelCustomerProductBizOp_CorrectCustomerProductAssignmentGiven_ShouldCancelCustomerProductOK()
        {
            var request = new CancelCustomerProductRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerProductAssignmentId = 1,
                CancelDate = cancelDate,
                UseNextBillCycleEndDateInRecurring = true
            };


            var customerProductAssignment = new CustomerProductAssignment();
            customerProductAssignment.ProductOffering = CreateDefaultObject.Create<ProductOffering>();
            customerProductAssignment.ProductOffering.Id = productid;
            customerProductAssignment.ProductOffering.Names = CreateDefaultObject.Create<MultiLingualDescription>();
            customerProductAssignment.ProductOffering.Description = CreateDefaultObject.Create<MultiLingualDescription>();
            customerProductAssignment.ProductOffering.ChargingOptions = new List<ProductChargeOption>();
            customerProductAssignment.ProductOffering.Options = new List<ProductOfferingOption>();


            customerProductAssignment.PurchasedProduct = CreateDefaultObject.Create<Product>();
            customerProductAssignment.PurchasedProduct.Id = productid;
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan.PromotionPlanId = 1;
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan.StartDate = startDateClosest;
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan.EndDate = endDate;
            customerProductAssignment.PurchasedProduct.AssociatedPackage = CreateDefaultObject.Create<PackageInfo>();
            customerProductAssignment.PurchasedProduct.Names = CreateDefaultObject.Create<MultiLingualDescription>();
            customerProductAssignment.PurchasedProduct.Description = CreateDefaultObject.Create<MultiLingualDescription>();
            customerProductAssignment.PurchasedProduct.AssociatedBundle = CreateDefaultObject.Create<BundleInfo>();
            //customerProductAssignment.PurchasedProduct.ChargingOptions = new List<ProductChargeOption>();

            customerProductAssignment.PurchasingCustomer = CreateDefaultObject.Create<CustomerInfo>();
            customerProductAssignment.PurchasingCustomer.CustomerID = customerid;
            var actualResourcembInfo = CreateDefaultObject.Create<ResourceMBInfo>();
            actualResourcembInfo.CustomerInfo.CustomerID = customerid;
            customerProductAssignment.PurchasingCustomer.ResourceMBInfo = new List<ResourceMBInfo> { actualResourcembInfo };
            customerProductAssignment.PurchasingCustomer.CustomerID = customerid;
            var servicesInfo1 = CreateDefaultObject.Create<ServicesInfo>();
            servicesInfo1.StartDate = startDate;
            servicesInfo1.EndDate = endDate;
            servicesInfo1.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();
            var servicesInfo2 = CreateDefaultObject.Create<ServicesInfo>();
            servicesInfo2.StartDate = startDate;
            servicesInfo2.EndDate = null;
            servicesInfo2.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();

            customerProductAssignment.PurchasingCustomer.ServicesInfo = new List<ServicesInfo>() { servicesInfo1, servicesInfo2 };
            var promotion1 = new CrmCustomersPromotionInfo();
            promotion1.Active = false;
            promotion1.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion1.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 3;
            promotion1.StartDate = startDate;
            promotion1.EndDate = endDate;
            var promotion2 = new CrmCustomersPromotionInfo();
            promotion2.Active = true;
            promotion2.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion2.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 2;
            promotion2.StartDate = startDate;
            promotion2.EndDate = endDate;
            var promotion3 = new CrmCustomersPromotionInfo();
            promotion3.Active = true;
            promotion3.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion3.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 1;
            promotion3.StartDate = startDate;
            promotion3.EndDate = null;
            var promotion4 = new CrmCustomersPromotionInfo();
            promotion4.Active = true;
            promotion4.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion4.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 1;
            promotion4.StartDate = startDateClosest;
            promotion4.EndDate = endDate;
            var promotion5 = new CrmCustomersPromotionInfo();
            promotion5.Active = true;
            promotion5.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion5.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 1;
            promotion5.StartDate = endDate;
            promotion5.EndDate = endDate;

            customerProductAssignment.PurchasingCustomer.Promotions = new List<CrmCustomersPromotionInfo>() { promotion1, promotion2, promotion3, promotion4, promotion5 };

            customerProductAssignment.ProductChargePurchased = new ProductChargeOption();
            var charge1 = CreateDefaultObject.Create<ChargeNonRecurring>();
            var charge2 = CreateDefaultObject.Create<ChargeRecurring>();
            var charge3 = CreateDefaultObject.Create<ChargeNonRecurring>();
            var charge4 = CreateDefaultObject.Create<ChargeRecurring>();
            customerProductAssignment.ProductChargePurchased.Charges = new List<Charge>() { charge1, charge2, charge3, charge4 };
            
            //Mocked getCustomerProductAssignmentByIdMS
            var mockedGetCustomerProductAssignmentByIdMS = MockMicroService<GetCustomerProductAssignmentByIdRequest, GetCustomerProductAssignmentByIdResponse>();
            mockedGetCustomerProductAssignmentByIdMS.Process(Arg.Any<GetCustomerProductAssignmentByIdRequest>(), null)
               .Returns(new GetCustomerProductAssignmentByIdResponse()
               {
                   CustomerProductAssignment = customerProductAssignment,
                   ResultType = ResultTypes.Ok,

               });

            //Mocked getActiveCusotmerAccountMs
            var mockedGetActiveCusotmerAccountMs = MockMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();
            mockedGetActiveCusotmerAccountMs.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(), null)
               .Returns(new GetActiveCustomerAccountAssociationByDateResponse()
               {
                   CustomerAccountAssociation = CreateDefaultObject.Create<CustomerAccountAssociation>(),
                   ResultType = ResultTypes.Ok,

               });

            //Mocked calculateNextBillRunDateMS
            var mockedCalculateNextBillRunDateMS = MockMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            mockedCalculateNextBillRunDateMS.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(), null)
               .Returns(new CalculateNextBillRunDateForBillCycleResponse()
               {
                   NextBillRun = nextBillRunDate,
                   ResultType = ResultTypes.Ok,

               });
            

            var mockedCancelCustomersPromotionMS = MockMicroService<CancelCustomersPromotionRequest, CancelCustomersPromotionResponse>();
            mockedCancelCustomersPromotionMS.Process(Arg.Any<CancelCustomersPromotionRequest>(), null)
                .Returns(new CancelCustomersPromotionResponse()
                {
                    ResultType = ResultTypes.Ok,
                    CrmCustomersPromotionInfo = CreateDefaultObject.Create<CrmCustomersPromotionInfo>(),

                });
            var mockedGetCustomerPromotionOperationLogByCustomerIDAndPromotionMS = MockMicroService<GetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest, GetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse>();
            mockedGetCustomerPromotionOperationLogByCustomerIDAndPromotionMS.Process(Arg.Any<GetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest>(), null)
               .Returns(new GetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse()
               {
                   LogInfo = CreateDefaultObject.Create<CrmCustomersPromotionOperationLogInfo>(),
                   ResultType = ResultTypes.Ok,

               });
            
            
            var mockedCreateCustomerPromotionLogInfoMS = MockMicroService<CreateCustomerPromotionLogInfoRequest, CreateCustomerPromotionLogInfoResponse>();
            mockedCreateCustomerPromotionLogInfoMS.Process(Arg.Any<CreateCustomerPromotionLogInfoRequest>(), null)
               .Returns(new CreateCustomerPromotionLogInfoResponse()
               {
                   ResultType = ResultTypes.Ok,

               });
            var mockedCancelServiceInfoMS = MockMicroService<CancelServicesInfoRequest, CancelServicesInfoResponse>();
            mockedCancelServiceInfoMS.Process(Arg.Any<CancelServicesInfoRequest>(), null)
               .Returns(new CancelServicesInfoResponse()
               {
                   ResultType = ResultTypes.Ok,

               });
            var mockedCancelCustomerPackagesMS = MockMicroService<CancelCustomerPackagesRequest, CancelCustomerPackagesResponse>();
            mockedCancelCustomerPackagesMS.Process(Arg.Any<CancelCustomerPackagesRequest>(), null)
               .Returns(new CancelCustomerPackagesResponse()
               {
                   ResultType = ResultTypes.Ok,

               });
            var mockedGetCustomerChargesByCustomerIdMS = MockMicroService<GetCustomerChargesByCustomerIdRequest, GetCustomerChargesByCustomerIdResponse>();
            var customerCharge = CreateDefaultObject.Create<CustomerCharge>();
            customerCharge.Schedule.ChargeDefinition = charge4;
            mockedGetCustomerChargesByCustomerIdMS.Process(Arg.Any<GetCustomerChargesByCustomerIdRequest>(), null)
               .Returns(new GetCustomerChargesByCustomerIdResponse()
               {
                   ResultType = ResultTypes.Ok,
                   CustomerCharges = new List<CustomerCharge>() { customerCharge },
               });
            var mockedGetCustomerChargeScheduleByCustomerMS = MockMicroService<GetCustomerChargeScheduleByCustomerRequest, GetCustomerChargeScheduleByCustomerResponse>();
            var customerChargeSchedule = CreateDefaultObject.Create<CustomerChargeSchedule>();
            customerChargeSchedule.ChargeDefinition = charge2;
            mockedGetCustomerChargeScheduleByCustomerMS.Process(Arg.Any<GetCustomerChargeScheduleByCustomerRequest>(), null)
               .Returns(new GetCustomerChargeScheduleByCustomerResponse()
               {
                   ResultType = ResultTypes.Ok,
                   CustomerChargeSchedule = new List<CustomerChargeSchedule>() { customerChargeSchedule },
               });
            var mockedCancelCustomerChargeScheduleMs = MockMicroService<CancelCustomerChargeScheduleRequest, CancelCustomerChargeScheduleResponse>();
            mockedCancelCustomerChargeScheduleMs.Process(Arg.Any<CancelCustomerChargeScheduleRequest>(), null)
               .Returns(new CancelCustomerChargeScheduleResponse()
               {
                   ResultType = ResultTypes.Ok,

               });
            var cancelCustomerProductAssignmentMs = MockMicroService<CancelCustomerProductAssignmentRequest, CancelCustomerProductAssignmentResponse>();
            cancelCustomerProductAssignmentMs.Process(Arg.Any<CancelCustomerProductAssignmentRequest>(), null)
               .Returns(new CancelCustomerProductAssignmentResponse()
               {
                   ResultType = ResultTypes.Ok,

               });

            var getActiveCustomerAccountAssociationByDate = MockMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();
            getActiveCustomerAccountAssociationByDate.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(), null)
               .Returns(new GetActiveCustomerAccountAssociationByDateResponse()
               {
                   CustomerAccountAssociation = CreateDefaultObject.Create<CustomerAccountAssociation>(),
                   ResultType = ResultTypes.Ok,

               });


            var calculateNextBillRunDateForBillCycleMs = MockMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            calculateNextBillRunDateForBillCycleMs.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(), null)
               .Returns(new CalculateNextBillRunDateForBillCycleResponse()
               {
                   NextBillRun = nextBillRunDate,
                   ResultType = ResultTypes.Ok,

               });


            #region Create Mock Repository

            var actualProductOffering = CreateDefaultObject.Create<ProductOffering>();

            _mockProductOfferingRepo = MockRepositoryManager.GetMockedRepository<IProductOfferingRepository<ProductOffering>>();
            _mockProductOfferingRepo.GetByGroupId(Arg.Any<int>())
                .Returns(new List<ProductOffering>() { actualProductOffering });



            #endregion


            MockAbstractSinglePhaseOrderProcessor(request);

            var actualCancelCustomerProductResponseDTO = CallBizOp(request);
            var expectedCancelCustomerProductResponseDTO = new CancelCustomerProductResponseDTO
            {
                Customer = new CustomerDTO { CustomerId = customerid },
                Subscription = new SubscriptionDTO { CustomerId = customerid },
                ProductCatalog = new ProductCatalogDTO { Id = productid }
            };

            Assert.IsTrue(actualCancelCustomerProductResponseDTO.resultType == ResultTypes.Ok);
            Assert.IsTrue(actualCancelCustomerProductResponseDTO.Customer.CustomerId == expectedCancelCustomerProductResponseDTO.Customer.CustomerId);
            Assert.IsTrue(actualCancelCustomerProductResponseDTO.Subscription.CustomerId == expectedCancelCustomerProductResponseDTO.Subscription.CustomerId);
            Assert.IsTrue(actualCancelCustomerProductResponseDTO.ProductCatalog.Id == expectedCancelCustomerProductResponseDTO.ProductCatalog.Id);
        }

        [Test()]
        public void CancelCustomerProductBizOp_CorrectCustomerProductAssignmentGivenButExpiredProduct_ShouldOK()
        {
            var request = new CancelCustomerProductRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerProductAssignmentId = 1,
                CancelDate = cancelDate,
                UseNextBillCycleEndDateInRecurring = true
            };


            var customerProductAssignment = new CustomerProductAssignment();
            customerProductAssignment.ProductOffering = CreateDefaultObject.Create<ProductOffering>();
            customerProductAssignment.ProductOffering.Id = productid;
            customerProductAssignment.ProductOffering.Names = CreateDefaultObject.Create<MultiLingualDescription>();
            customerProductAssignment.ProductOffering.Description = CreateDefaultObject.Create<MultiLingualDescription>();
            customerProductAssignment.ProductOffering.ChargingOptions = new List<ProductChargeOption>();
            customerProductAssignment.ProductOffering.Options = new List<ProductOfferingOption>();

            customerProductAssignment.PurchasedProduct = CreateDefaultObject.Create<Product>();
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan.PromotionPlanId = 1;
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan.StartDate = startDateClosest;
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan.EndDate = endDate;
            customerProductAssignment.PurchasedProduct.AssociatedPackage = CreateDefaultObject.Create<PackageInfo>();
            customerProductAssignment.PurchasedProduct.Names = CreateDefaultObject.Create<MultiLingualDescription>();
            customerProductAssignment.PurchasedProduct.Description = CreateDefaultObject.Create<MultiLingualDescription>();



            customerProductAssignment.PurchasedProduct.AssociatedBundle = CreateDefaultObject.Create<BundleInfo>();

            customerProductAssignment.PurchasingCustomer = CreateDefaultObject.Create<CustomerInfo>();
            customerProductAssignment.PurchasingCustomer.CustomerID = customerid;
            var actualResourcembInfo = CreateDefaultObject.Create<ResourceMBInfo>();
            actualResourcembInfo.CustomerInfo.CustomerID = customerid;
            customerProductAssignment.PurchasingCustomer.ResourceMBInfo = new List<ResourceMBInfo> { actualResourcembInfo };
            customerProductAssignment.PurchasingCustomer.CustomerID = customerid;
            var servicesInfo1 = CreateDefaultObject.Create<ServicesInfo>();
            servicesInfo1.StartDate = startDate;
            servicesInfo1.EndDate = endDate;
            servicesInfo1.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();
            var servicesInfo2 = CreateDefaultObject.Create<ServicesInfo>();
            servicesInfo2.StartDate = startDate;
            servicesInfo2.EndDate = null;
            servicesInfo2.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();

            customerProductAssignment.PurchasingCustomer.ServicesInfo = new List<ServicesInfo>() { servicesInfo1, servicesInfo2 };
            var promotion1 = new CrmCustomersPromotionInfo();
            promotion1.Active = true;
            promotion1.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion1.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 1;
            promotion1.StartDate = endDate;
            promotion1.EndDate = endDate;      

            customerProductAssignment.PurchasingCustomer.Promotions = new List<CrmCustomersPromotionInfo>() { promotion1 };

            customerProductAssignment.ProductChargePurchased = new ProductChargeOption();
            var charge1 = CreateDefaultObject.Create<ChargeNonRecurring>();
            var charge2 = CreateDefaultObject.Create<ChargeRecurring>();
            var charge3 = CreateDefaultObject.Create<ChargeNonRecurring>();
            var charge4 = CreateDefaultObject.Create<ChargeRecurring>();
            customerProductAssignment.ProductChargePurchased.Charges = new List<Charge>() { charge1, charge2, charge3, charge4 };

            //Mocked getCustomerProductAssignmentByIdMS
            var mockedGetCustomerProductAssignmentByIdMS = MockMicroService<GetCustomerProductAssignmentByIdRequest, GetCustomerProductAssignmentByIdResponse>();
            mockedGetCustomerProductAssignmentByIdMS.Process(Arg.Any<GetCustomerProductAssignmentByIdRequest>(), null)
               .Returns(new GetCustomerProductAssignmentByIdResponse()
               {
                   CustomerProductAssignment = customerProductAssignment,
                   ResultType = ResultTypes.Ok,

               });

            //Mocked getActiveCusotmerAccountMs
            var mockedGetActiveCusotmerAccountMs = MockMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();
            mockedGetActiveCusotmerAccountMs.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(), null)
               .Returns(new GetActiveCustomerAccountAssociationByDateResponse()
               {
                   CustomerAccountAssociation = CreateDefaultObject.Create<CustomerAccountAssociation>(),
                   ResultType = ResultTypes.Ok,

               });

            //Mocked calculateNextBillRunDateMS
            var mockedCalculateNextBillRunDateMS = MockMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            mockedCalculateNextBillRunDateMS.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(), null)
               .Returns(new CalculateNextBillRunDateForBillCycleResponse()
               {
                   NextBillRun = nextBillRunDate,
                   ResultType = ResultTypes.Ok,

               });


            var mockedCancelCustomersPromotionMS = MockMicroService<CancelCustomersPromotionRequest, CancelCustomersPromotionResponse>();
            mockedCancelCustomersPromotionMS.Process(Arg.Any<CancelCustomersPromotionRequest>(), null)
                .Returns(new CancelCustomersPromotionResponse()
                {
                    ResultType = ResultTypes.Ok,
                    CrmCustomersPromotionInfo = CreateDefaultObject.Create<CrmCustomersPromotionInfo>(),

                });
            var mockedGetCustomerPromotionOperationLogByCustomerIDAndPromotionMS = MockMicroService<GetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest, GetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse>();
            mockedGetCustomerPromotionOperationLogByCustomerIDAndPromotionMS.Process(Arg.Any<GetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest>(), null)
               .Returns(new GetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse()
               {
                   LogInfo = CreateDefaultObject.Create<CrmCustomersPromotionOperationLogInfo>(),
                   ResultType = ResultTypes.Ok,

               });


            var mockedCreateCustomerPromotionLogInfoMS = MockMicroService<CreateCustomerPromotionLogInfoRequest, CreateCustomerPromotionLogInfoResponse>();
            mockedCreateCustomerPromotionLogInfoMS.Process(Arg.Any<CreateCustomerPromotionLogInfoRequest>(), null)
               .Returns(new CreateCustomerPromotionLogInfoResponse()
               {
                   ResultType = ResultTypes.Ok,

               });
            var mockedCancelServiceInfoMS = MockMicroService<CancelServicesInfoRequest, CancelServicesInfoResponse>();
            mockedCancelServiceInfoMS.Process(Arg.Any<CancelServicesInfoRequest>(), null)
               .Returns(new CancelServicesInfoResponse()
               {
                   ResultType = ResultTypes.Ok,

               });
            var mockedCancelCustomerPackagesMS = MockMicroService<CancelCustomerPackagesRequest, CancelCustomerPackagesResponse>();
            mockedCancelCustomerPackagesMS.Process(Arg.Any<CancelCustomerPackagesRequest>(), null)
               .Returns(new CancelCustomerPackagesResponse()
               {
                   ResultType = ResultTypes.Ok,

               });
            var mockedGetCustomerChargesByCustomerIdMS = MockMicroService<GetCustomerChargesByCustomerIdRequest, GetCustomerChargesByCustomerIdResponse>();
            var customerCharge = CreateDefaultObject.Create<CustomerCharge>();
            customerCharge.Schedule.ChargeDefinition = charge4;
            mockedGetCustomerChargesByCustomerIdMS.Process(Arg.Any<GetCustomerChargesByCustomerIdRequest>(), null)
               .Returns(new GetCustomerChargesByCustomerIdResponse()
               {
                   ResultType = ResultTypes.Ok,
                   CustomerCharges = new List<CustomerCharge>() { customerCharge },
               });
            var mockedGetCustomerChargeScheduleByCustomerMS = MockMicroService<GetCustomerChargeScheduleByCustomerRequest, GetCustomerChargeScheduleByCustomerResponse>();
            var customerChargeSchedule = CreateDefaultObject.Create<CustomerChargeSchedule>();
            customerChargeSchedule.ChargeDefinition = charge2;
            mockedGetCustomerChargeScheduleByCustomerMS.Process(Arg.Any<GetCustomerChargeScheduleByCustomerRequest>(), null)
               .Returns(new GetCustomerChargeScheduleByCustomerResponse()
               {
                   ResultType = ResultTypes.Ok,
                   CustomerChargeSchedule = new List<CustomerChargeSchedule>() { customerChargeSchedule },
               });
            var mockedCancelCustomerChargeScheduleMs = MockMicroService<CancelCustomerChargeScheduleRequest, CancelCustomerChargeScheduleResponse>();
            mockedCancelCustomerChargeScheduleMs.Process(Arg.Any<CancelCustomerChargeScheduleRequest>(), null)
               .Returns(new CancelCustomerChargeScheduleResponse()
               {
                   ResultType = ResultTypes.Ok,

               });
            var cancelCustomerProductAssignmentMs = MockMicroService<CancelCustomerProductAssignmentRequest, CancelCustomerProductAssignmentResponse>();
            cancelCustomerProductAssignmentMs.Process(Arg.Any<CancelCustomerProductAssignmentRequest>(), null)
               .Returns(new CancelCustomerProductAssignmentResponse()
               {
                   ResultType = ResultTypes.Ok,

               });

            var getActiveCustomerAccountAssociationByDate = MockMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();
            getActiveCustomerAccountAssociationByDate.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(), null)
               .Returns(new GetActiveCustomerAccountAssociationByDateResponse()
               {
                   CustomerAccountAssociation = CreateDefaultObject.Create<CustomerAccountAssociation>(),
                   ResultType = ResultTypes.Ok,

               });


            var calculateNextBillRunDateForBillCycleMs = MockMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            calculateNextBillRunDateForBillCycleMs.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(), null)
               .Returns(new CalculateNextBillRunDateForBillCycleResponse()
               {
                   NextBillRun = nextBillRunDate,
                   ResultType = ResultTypes.Ok,

               });

            #region Create Mock Repository

            var actualProductOffering = CreateDefaultObject.Create<ProductOffering>();

            _mockProductOfferingRepo = MockRepositoryManager.GetMockedRepository<IProductOfferingRepository<ProductOffering>>();
            _mockProductOfferingRepo.GetByGroupId(Arg.Any<int>())
                .Returns(new List<ProductOffering>() { actualProductOffering });



            #endregion


            MockAbstractSinglePhaseOrderProcessor(request);

            var actualCancelCustomerProductResponseDTO = CallBizOp(request);
            var expectedCancelCustomerProductResponseDTO = new CancelCustomerProductResponseDTO
            {
                Customer = new CustomerDTO { CustomerId = customerid },
                Subscription = new SubscriptionDTO { CustomerId = customerid },
                ProductCatalog = new ProductCatalogDTO { Id = productid }
            };

            Assert.IsTrue(actualCancelCustomerProductResponseDTO.resultType == ResultTypes.Ok);
            Assert.IsTrue(actualCancelCustomerProductResponseDTO.Customer.CustomerId == expectedCancelCustomerProductResponseDTO.Customer.CustomerId);
            Assert.IsTrue(actualCancelCustomerProductResponseDTO.Subscription.CustomerId == expectedCancelCustomerProductResponseDTO.Subscription.CustomerId);
            Assert.IsTrue(actualCancelCustomerProductResponseDTO.ProductCatalog.Id == expectedCancelCustomerProductResponseDTO.ProductCatalog.Id);
        }

        [Test()]
        public void CancelCustomerProductBizOp_MoreThanOneCustomerChargeScheduleToBeCancelled_ShouldCancelCustomerProductOK()
        {
            var request = new CancelCustomerProductRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerProductAssignmentId = 1,
                CancelDate = cancelDate,
                UseNextBillCycleEndDateInRecurring = true
            };


            var customerProductAssignment = new CustomerProductAssignment();
            customerProductAssignment.ProductOffering = CreateDefaultObject.Create<ProductOffering>();
            customerProductAssignment.ProductOffering.Id = productid;
            customerProductAssignment.ProductOffering.Names = CreateDefaultObject.Create<MultiLingualDescription>();
            customerProductAssignment.ProductOffering.Description = CreateDefaultObject.Create<MultiLingualDescription>();
            customerProductAssignment.ProductOffering.ChargingOptions = new List<ProductChargeOption>();
            customerProductAssignment.ProductOffering.Options = new List<ProductOfferingOption>();

            customerProductAssignment.PurchasedProduct = CreateDefaultObject.Create<Product>();
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan.PromotionPlanId = 1;
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan.StartDate = startDateClosest;
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan.EndDate = endDate;
            customerProductAssignment.PurchasedProduct.AssociatedPackage = CreateDefaultObject.Create<PackageInfo>();
            customerProductAssignment.PurchasedProduct.Names = CreateDefaultObject.Create<MultiLingualDescription>();
            customerProductAssignment.PurchasedProduct.Description = CreateDefaultObject.Create<MultiLingualDescription>();
            

            customerProductAssignment.PurchasedProduct.AssociatedBundle = CreateDefaultObject.Create<BundleInfo>();

            customerProductAssignment.PurchasingCustomer = CreateDefaultObject.Create<CustomerInfo>();
            customerProductAssignment.PurchasingCustomer.CustomerID = customerid;
            var actualResourcembInfo = CreateDefaultObject.Create<ResourceMBInfo>();
            actualResourcembInfo.CustomerInfo.CustomerID = customerid;
            customerProductAssignment.PurchasingCustomer.ResourceMBInfo = new List<ResourceMBInfo> { actualResourcembInfo };
            customerProductAssignment.PurchasingCustomer.CustomerID = customerid;
            var servicesInfo1 = CreateDefaultObject.Create<ServicesInfo>();
            servicesInfo1.StartDate = startDate;
            servicesInfo1.EndDate = endDate;
            servicesInfo1.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();
            var servicesInfo2 = CreateDefaultObject.Create<ServicesInfo>();
            servicesInfo2.StartDate = startDate;
            servicesInfo2.EndDate = null;
            servicesInfo2.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();

            customerProductAssignment.PurchasingCustomer.ServicesInfo = new List<ServicesInfo>() { servicesInfo1, servicesInfo2 };
            var promotion1 = new CrmCustomersPromotionInfo();
            promotion1.Active = false;
            promotion1.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion1.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 3;
            promotion1.StartDate = startDate;
            promotion1.EndDate = endDate;
            var promotion2 = new CrmCustomersPromotionInfo();
            promotion2.Active = true;
            promotion2.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion2.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 2;
            promotion2.StartDate = startDate;
            promotion2.EndDate = endDate;
            var promotion3 = new CrmCustomersPromotionInfo();
            promotion3.Active = true;
            promotion3.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion3.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 1;
            promotion3.StartDate = startDate;
            promotion3.EndDate = null;
            var promotion4 = new CrmCustomersPromotionInfo();
            promotion4.Active = true;
            promotion4.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion4.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 1;
            promotion4.StartDate = startDateClosest;
            promotion4.EndDate = endDate;
            var promotion5 = new CrmCustomersPromotionInfo();
            promotion5.Active = true;
            promotion5.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion5.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 1;
            promotion5.StartDate = endDate;
            promotion5.EndDate = endDate;

            customerProductAssignment.PurchasingCustomer.Promotions = new List<CrmCustomersPromotionInfo>() { promotion1, promotion2, promotion3, promotion4, promotion5 };

            customerProductAssignment.ProductChargePurchased = new ProductChargeOption();
            var charge1 = CreateDefaultObject.Create<ChargeNonRecurring>();
            var charge2 = CreateDefaultObject.Create<ChargeRecurring>();
            var charge3 = CreateDefaultObject.Create<ChargeNonRecurring>();
            var charge4 = CreateDefaultObject.Create<ChargeRecurring>();
            customerProductAssignment.ProductChargePurchased.Charges = new List<Charge>() { charge1, charge2, charge3, charge4 };

            //Mocked getCustomerProductAssignmentByIdMS
            var mockedGetCustomerProductAssignmentByIdMS = MockMicroService<GetCustomerProductAssignmentByIdRequest, GetCustomerProductAssignmentByIdResponse>();
            mockedGetCustomerProductAssignmentByIdMS.Process(Arg.Any<GetCustomerProductAssignmentByIdRequest>(), null)
               .Returns(new GetCustomerProductAssignmentByIdResponse()
               {
                   CustomerProductAssignment = customerProductAssignment,
                   ResultType = ResultTypes.Ok,

               });

            //Mocked getActiveCusotmerAccountMs
            var mockedGetActiveCusotmerAccountMs = MockMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();
            mockedGetActiveCusotmerAccountMs.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(), null)
               .Returns(new GetActiveCustomerAccountAssociationByDateResponse()
               {
                   CustomerAccountAssociation = CreateDefaultObject.Create<CustomerAccountAssociation>(),
                   ResultType = ResultTypes.Ok,

               });

            //Mocked calculateNextBillRunDateMS
            var mockedCalculateNextBillRunDateMS = MockMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            mockedCalculateNextBillRunDateMS.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(), null)
               .Returns(new CalculateNextBillRunDateForBillCycleResponse()
               {
                   NextBillRun = nextBillRunDate,
                   ResultType = ResultTypes.Ok,

               });


            var mockedCancelCustomersPromotionMS = MockMicroService<CancelCustomersPromotionRequest, CancelCustomersPromotionResponse>();
            mockedCancelCustomersPromotionMS.Process(Arg.Any<CancelCustomersPromotionRequest>(), null)
                .Returns(new CancelCustomersPromotionResponse()
                {
                    ResultType = ResultTypes.Ok,
                    CrmCustomersPromotionInfo = CreateDefaultObject.Create<CrmCustomersPromotionInfo>(),

                });
            var mockedGetCustomerPromotionOperationLogByCustomerIDAndPromotionMS = MockMicroService<GetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest, GetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse>();
            mockedGetCustomerPromotionOperationLogByCustomerIDAndPromotionMS.Process(Arg.Any<GetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest>(), null)
               .Returns(new GetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse()
               {
                   LogInfo = CreateDefaultObject.Create<CrmCustomersPromotionOperationLogInfo>(),
                   ResultType = ResultTypes.Ok,

               });


            var mockedCreateCustomerPromotionLogInfoMS = MockMicroService<CreateCustomerPromotionLogInfoRequest, CreateCustomerPromotionLogInfoResponse>();
            mockedCreateCustomerPromotionLogInfoMS.Process(Arg.Any<CreateCustomerPromotionLogInfoRequest>(), null)
               .Returns(new CreateCustomerPromotionLogInfoResponse()
               {
                   ResultType = ResultTypes.Ok,

               });
            var mockedCancelServiceInfoMS = MockMicroService<CancelServicesInfoRequest, CancelServicesInfoResponse>();
            mockedCancelServiceInfoMS.Process(Arg.Any<CancelServicesInfoRequest>(), null)
               .Returns(new CancelServicesInfoResponse()
               {
                   ResultType = ResultTypes.Ok,

               });
            var mockedCancelCustomerPackagesMS = MockMicroService<CancelCustomerPackagesRequest, CancelCustomerPackagesResponse>();
            mockedCancelCustomerPackagesMS.Process(Arg.Any<CancelCustomerPackagesRequest>(), null)
               .Returns(new CancelCustomerPackagesResponse()
               {
                   ResultType = ResultTypes.Ok,

               });
            var mockedGetCustomerChargesByCustomerIdMS = MockMicroService<GetCustomerChargesByCustomerIdRequest, GetCustomerChargesByCustomerIdResponse>();
            var customerCharge = CreateDefaultObject.Create<CustomerCharge>();
            customerCharge.Schedule.ChargeDefinition = charge4;
            mockedGetCustomerChargesByCustomerIdMS.Process(Arg.Any<GetCustomerChargesByCustomerIdRequest>(), null)
               .Returns(new GetCustomerChargesByCustomerIdResponse()
               {
                   ResultType = ResultTypes.Ok,
                   CustomerCharges = new List<CustomerCharge>() { customerCharge },
               });
            var mockedGetCustomerChargeScheduleByCustomerMS = MockMicroService<GetCustomerChargeScheduleByCustomerRequest, GetCustomerChargeScheduleByCustomerResponse>();
            var customerChargeSchedule = CreateDefaultObject.Create<CustomerChargeSchedule>();
            customerChargeSchedule.ChargeDefinition = charge2;
            mockedGetCustomerChargeScheduleByCustomerMS.Process(Arg.Any<GetCustomerChargeScheduleByCustomerRequest>(), null)
               .Returns(new GetCustomerChargeScheduleByCustomerResponse()
               {
                   ResultType = ResultTypes.Ok,
                   CustomerChargeSchedule = new List<CustomerChargeSchedule>() { customerChargeSchedule,customerChargeSchedule },
               });
            var mockedCancelCustomerChargeScheduleMs = MockMicroService<CancelCustomerChargeScheduleRequest, CancelCustomerChargeScheduleResponse>();
            mockedCancelCustomerChargeScheduleMs.Process(Arg.Any<CancelCustomerChargeScheduleRequest>(), null)
               .Returns(new CancelCustomerChargeScheduleResponse()
               {
                   ResultType = ResultTypes.Ok,

               });
            var cancelCustomerProductAssignmentMs = MockMicroService<CancelCustomerProductAssignmentRequest, CancelCustomerProductAssignmentResponse>();
            cancelCustomerProductAssignmentMs.Process(Arg.Any<CancelCustomerProductAssignmentRequest>(), null)
               .Returns(new CancelCustomerProductAssignmentResponse()
               {
                   ResultType = ResultTypes.Ok,

               });

            var getActiveCustomerAccountAssociationByDate = MockMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();
            getActiveCustomerAccountAssociationByDate.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(), null)
               .Returns(new GetActiveCustomerAccountAssociationByDateResponse()
               {
                   CustomerAccountAssociation = CreateDefaultObject.Create<CustomerAccountAssociation>(),
                   ResultType = ResultTypes.Ok,

               });


            var calculateNextBillRunDateForBillCycleMs = MockMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            calculateNextBillRunDateForBillCycleMs.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(), null)
               .Returns(new CalculateNextBillRunDateForBillCycleResponse()
               {
                   NextBillRun = nextBillRunDate,
                   ResultType = ResultTypes.Ok,

               });

            #region Create Mock Repository

            var actualProductOffering = CreateDefaultObject.Create<ProductOffering>();

            _mockProductOfferingRepo = MockRepositoryManager.GetMockedRepository<IProductOfferingRepository<ProductOffering>>();
            _mockProductOfferingRepo.GetByGroupId(Arg.Any<int>())
                .Returns(new List<ProductOffering>() { actualProductOffering });



            #endregion


            MockAbstractSinglePhaseOrderProcessor(request);

            var actualCancelCustomerProductResponseDTO = CallBizOp(request);
            var expectedCancelCustomerProductResponseDTO = new CancelCustomerProductResponseDTO
            {
                Customer = new CustomerDTO { CustomerId = customerid },
                Subscription = new SubscriptionDTO { CustomerId = customerid },
                ProductCatalog = new ProductCatalogDTO { Id = productid }
            };

            Assert.IsTrue(actualCancelCustomerProductResponseDTO.resultType == ResultTypes.Ok);
            Assert.IsTrue(actualCancelCustomerProductResponseDTO.Customer.CustomerId == expectedCancelCustomerProductResponseDTO.Customer.CustomerId);
            Assert.IsTrue(actualCancelCustomerProductResponseDTO.Subscription.CustomerId == expectedCancelCustomerProductResponseDTO.Subscription.CustomerId);
            Assert.IsTrue(actualCancelCustomerProductResponseDTO.ProductCatalog.Id == expectedCancelCustomerProductResponseDTO.ProductCatalog.Id);
        }

        [Test()]
        public void CancelCustomerProductBizOp_CustomerProductAssignmentIsNotNeededToBeUpdated_ShouldCancelCustomerProductOK()
        {
            var request = new CancelCustomerProductRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerProductAssignmentId = 1,
                CancelDate = cancelDate,
                UseNextBillCycleEndDateInRecurring = true
            };


            var customerProductAssignment = new CustomerProductAssignment();
            customerProductAssignment.ProductOffering = CreateDefaultObject.Create<ProductOffering>();
            customerProductAssignment.ProductOffering.Id = productid;
            customerProductAssignment.ProductOffering.Names = CreateDefaultObject.Create<MultiLingualDescription>();
            customerProductAssignment.ProductOffering.Description = CreateDefaultObject.Create<MultiLingualDescription>();
            customerProductAssignment.ProductOffering.ChargingOptions = new List<ProductChargeOption>();
            customerProductAssignment.ProductOffering.Options = new List<ProductOfferingOption>();


            customerProductAssignment.PurchasedProduct = CreateDefaultObject.Create<Product>();
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan.PromotionPlanId = 1;
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan.StartDate = startDateClosest;
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan.EndDate = endDate;
            customerProductAssignment.PurchasedProduct.AssociatedPackage = CreateDefaultObject.Create<PackageInfo>();
            customerProductAssignment.PurchasedProduct.Names = CreateDefaultObject.Create<MultiLingualDescription>();
            customerProductAssignment.PurchasedProduct.Description = CreateDefaultObject.Create<MultiLingualDescription>();


            customerProductAssignment.PurchasedProduct.AssociatedBundle = CreateDefaultObject.Create<BundleInfo>();

            customerProductAssignment.PurchasingCustomer = CreateDefaultObject.Create<CustomerInfo>();
            customerProductAssignment.PurchasingCustomer.CustomerID = customerid;
            var actualResourcembInfo = CreateDefaultObject.Create<ResourceMBInfo>();
            actualResourcembInfo.CustomerInfo.CustomerID = customerid;
            customerProductAssignment.PurchasingCustomer.ResourceMBInfo = new List<ResourceMBInfo> { actualResourcembInfo };
            customerProductAssignment.PurchasingCustomer.CustomerID = customerid;
            var servicesInfo1 = CreateDefaultObject.Create<ServicesInfo>();
            servicesInfo1.StartDate = startDate;
            servicesInfo1.EndDate = endDate;
            servicesInfo1.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();
            var servicesInfo2 = CreateDefaultObject.Create<ServicesInfo>();
            servicesInfo2.StartDate = startDate;
            servicesInfo2.EndDate = null;
            servicesInfo2.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();

            customerProductAssignment.PurchasingCustomer.ServicesInfo = new List<ServicesInfo>() { servicesInfo1, servicesInfo2 };
            customerProductAssignment.EndDate = DateTime.Now.AddMonths(-1);
            var promotion1 = new CrmCustomersPromotionInfo();
            promotion1.Active = false;
            promotion1.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion1.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 3;
            promotion1.StartDate = startDate;
            promotion1.EndDate = endDate;
            var promotion2 = new CrmCustomersPromotionInfo();
            promotion2.Active = true;
            promotion2.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion2.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 2;
            promotion2.StartDate = startDate;
            promotion2.EndDate = endDate;
            var promotion3 = new CrmCustomersPromotionInfo();
            promotion3.Active = true;
            promotion3.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion3.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 1;
            promotion3.StartDate = startDate;
            promotion3.EndDate = null;
            var promotion4 = new CrmCustomersPromotionInfo();
            promotion4.Active = true;
            promotion4.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion4.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 1;
            promotion4.StartDate = startDateClosest;
            promotion4.EndDate = endDate;
            var promotion5 = new CrmCustomersPromotionInfo();
            promotion5.Active = true;
            promotion5.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion5.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 1;
            promotion5.StartDate = endDate;
            promotion5.EndDate = endDate;

            customerProductAssignment.PurchasingCustomer.Promotions = new List<CrmCustomersPromotionInfo>() { promotion1, promotion2, promotion3, promotion4, promotion5 };

            customerProductAssignment.ProductChargePurchased = new ProductChargeOption();
            var charge1 = CreateDefaultObject.Create<ChargeNonRecurring>();
            var charge2 = CreateDefaultObject.Create<ChargeRecurring>();
            var charge3 = CreateDefaultObject.Create<ChargeNonRecurring>();
            var charge4 = CreateDefaultObject.Create<ChargeRecurring>();
            customerProductAssignment.ProductChargePurchased.Charges = new List<Charge>() { charge1, charge2, charge3, charge4 };

            //Mocked getCustomerProductAssignmentByIdMS
            var mockedGetCustomerProductAssignmentByIdMS = MockMicroService<GetCustomerProductAssignmentByIdRequest, GetCustomerProductAssignmentByIdResponse>();
            mockedGetCustomerProductAssignmentByIdMS.Process(Arg.Any<GetCustomerProductAssignmentByIdRequest>(), null)
               .Returns(new GetCustomerProductAssignmentByIdResponse()
               {
                   CustomerProductAssignment = customerProductAssignment,
                   ResultType = ResultTypes.Ok,

               });

            //Mocked getActiveCusotmerAccountMs
            var mockedGetActiveCusotmerAccountMs = MockMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();
            mockedGetActiveCusotmerAccountMs.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(), null)
               .Returns(new GetActiveCustomerAccountAssociationByDateResponse()
               {
                   CustomerAccountAssociation = CreateDefaultObject.Create<CustomerAccountAssociation>(),
                   ResultType = ResultTypes.Ok,

               });

            //Mocked calculateNextBillRunDateMS
            var mockedCalculateNextBillRunDateMS = MockMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            mockedCalculateNextBillRunDateMS.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(), null)
               .Returns(new CalculateNextBillRunDateForBillCycleResponse()
               {
                   NextBillRun = nextBillRunDate,
                   ResultType = ResultTypes.Ok,

               });


            var mockedCancelCustomersPromotionMS = MockMicroService<CancelCustomersPromotionRequest, CancelCustomersPromotionResponse>();
            mockedCancelCustomersPromotionMS.Process(Arg.Any<CancelCustomersPromotionRequest>(), null)
                .Returns(new CancelCustomersPromotionResponse()
                {
                    ResultType = ResultTypes.Ok,
                    CrmCustomersPromotionInfo = CreateDefaultObject.Create<CrmCustomersPromotionInfo>(),

                });
            var mockedGetCustomerPromotionOperationLogByCustomerIDAndPromotionMS = MockMicroService<GetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest, GetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse>();
            mockedGetCustomerPromotionOperationLogByCustomerIDAndPromotionMS.Process(Arg.Any<GetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest>(), null)
               .Returns(new GetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse()
               {
                   LogInfo = CreateDefaultObject.Create<CrmCustomersPromotionOperationLogInfo>(),
                   ResultType = ResultTypes.Ok,

               });


            var mockedCreateCustomerPromotionLogInfoMS = MockMicroService<CreateCustomerPromotionLogInfoRequest, CreateCustomerPromotionLogInfoResponse>();
            mockedCreateCustomerPromotionLogInfoMS.Process(Arg.Any<CreateCustomerPromotionLogInfoRequest>(), null)
               .Returns(new CreateCustomerPromotionLogInfoResponse()
               {
                   ResultType = ResultTypes.Ok,

               });
            var mockedCancelServiceInfoMS = MockMicroService<CancelServicesInfoRequest, CancelServicesInfoResponse>();
            mockedCancelServiceInfoMS.Process(Arg.Any<CancelServicesInfoRequest>(), null)
               .Returns(new CancelServicesInfoResponse()
               {
                   ResultType = ResultTypes.Ok,

               });
            var mockedCancelCustomerPackagesMS = MockMicroService<CancelCustomerPackagesRequest, CancelCustomerPackagesResponse>();
            mockedCancelCustomerPackagesMS.Process(Arg.Any<CancelCustomerPackagesRequest>(), null)
               .Returns(new CancelCustomerPackagesResponse()
               {
                   ResultType = ResultTypes.Ok,

               });
            var mockedGetCustomerChargesByCustomerIdMS = MockMicroService<GetCustomerChargesByCustomerIdRequest, GetCustomerChargesByCustomerIdResponse>();
            var customerCharge = CreateDefaultObject.Create<CustomerCharge>();
            customerCharge.Schedule.ChargeDefinition = charge4;
            mockedGetCustomerChargesByCustomerIdMS.Process(Arg.Any<GetCustomerChargesByCustomerIdRequest>(), null)
               .Returns(new GetCustomerChargesByCustomerIdResponse()
               {
                   ResultType = ResultTypes.Ok,
                   CustomerCharges = new List<CustomerCharge>() { customerCharge },
               });
            var mockedGetCustomerChargeScheduleByCustomerMS = MockMicroService<GetCustomerChargeScheduleByCustomerRequest, GetCustomerChargeScheduleByCustomerResponse>();
            var customerChargeSchedule = CreateDefaultObject.Create<CustomerChargeSchedule>();
            customerChargeSchedule.ChargeDefinition = charge2;
            mockedGetCustomerChargeScheduleByCustomerMS.Process(Arg.Any<GetCustomerChargeScheduleByCustomerRequest>(), null)
               .Returns(new GetCustomerChargeScheduleByCustomerResponse()
               {
                   ResultType = ResultTypes.Ok,
                   CustomerChargeSchedule = new List<CustomerChargeSchedule>() { customerChargeSchedule, customerChargeSchedule },
               });
            var mockedCancelCustomerChargeScheduleMs = MockMicroService<CancelCustomerChargeScheduleRequest, CancelCustomerChargeScheduleResponse>();
            mockedCancelCustomerChargeScheduleMs.Process(Arg.Any<CancelCustomerChargeScheduleRequest>(), null)
               .Returns(new CancelCustomerChargeScheduleResponse()
               {
                   ResultType = ResultTypes.Ok,

               });
            var cancelCustomerProductAssignmentMs = MockMicroService<CancelCustomerProductAssignmentRequest, CancelCustomerProductAssignmentResponse>();
            cancelCustomerProductAssignmentMs.Process(Arg.Any<CancelCustomerProductAssignmentRequest>(), null)
               .Returns(new CancelCustomerProductAssignmentResponse()
               {
                   ResultType = ResultTypes.Ok,

               });

            var getActiveCustomerAccountAssociationByDate = MockMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();
            getActiveCustomerAccountAssociationByDate.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(), null)
               .Returns(new GetActiveCustomerAccountAssociationByDateResponse()
               {
                   CustomerAccountAssociation = CreateDefaultObject.Create<CustomerAccountAssociation>(),
                   ResultType = ResultTypes.Ok,

               });


            var calculateNextBillRunDateForBillCycleMs = MockMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            calculateNextBillRunDateForBillCycleMs.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(), null)
               .Returns(new CalculateNextBillRunDateForBillCycleResponse()
               {
                   NextBillRun = nextBillRunDate,
                   ResultType = ResultTypes.Ok,

               });

            #region Create Mock Repository

            var actualProductOffering = CreateDefaultObject.Create<ProductOffering>();

            _mockProductOfferingRepo = MockRepositoryManager.GetMockedRepository<IProductOfferingRepository<ProductOffering>>();
            _mockProductOfferingRepo.GetByGroupId(Arg.Any<int>())
                .Returns(new List<ProductOffering>() { actualProductOffering });



            #endregion


            MockAbstractSinglePhaseOrderProcessor(request);

            var actualCancelCustomerProductResponseDTO = CallBizOp(request);
            var expectedCancelCustomerProductResponseDTO = new CancelCustomerProductResponseDTO
            {
                Customer = new CustomerDTO { CustomerId = customerid },
                Subscription = new SubscriptionDTO { CustomerId = customerid },
                ProductCatalog = new ProductCatalogDTO { Id = productid }
            };

            Assert.IsTrue(actualCancelCustomerProductResponseDTO.resultType == ResultTypes.Ok);
            Assert.IsTrue(actualCancelCustomerProductResponseDTO.Customer.CustomerId == expectedCancelCustomerProductResponseDTO.Customer.CustomerId);
            Assert.IsTrue(actualCancelCustomerProductResponseDTO.Subscription.CustomerId == expectedCancelCustomerProductResponseDTO.Subscription.CustomerId);
            Assert.IsTrue(actualCancelCustomerProductResponseDTO.ProductCatalog.Id == expectedCancelCustomerProductResponseDTO.ProductCatalog.Id);
        }

        [Test()]
        public void CancelCustomerProductBizOp_CancelDateEarlierThanNow_ShouldNotOK()
        {
            var request = new CancelCustomerProductRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerProductAssignmentId = 1,
                CancelDate = DateTime.Now.AddSeconds(-1),
                CurrentTime = DateTime.Now,
                UseNextBillCycleEndDateInRecurring = true
            };
            
            #region
            var customerProductAssignment = new CustomerProductAssignment();
            customerProductAssignment.ProductOffering = CreateDefaultObject.Create<ProductOffering>();
            customerProductAssignment.ProductOffering.Id = productid;
            customerProductAssignment.ProductOffering.Names = CreateDefaultObject.Create<MultiLingualDescription>();
            customerProductAssignment.ProductOffering.Description = CreateDefaultObject.Create<MultiLingualDescription>();
            customerProductAssignment.ProductOffering.ChargingOptions = new List<ProductChargeOption>();
            customerProductAssignment.ProductOffering.Options = new List<ProductOfferingOption>();

            customerProductAssignment.PurchasedProduct = CreateDefaultObject.Create<Product>();
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan.PromotionPlanId = 1;
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan.StartDate = startDateClosest;
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan.EndDate = endDate;
            customerProductAssignment.PurchasedProduct.AssociatedPackage = CreateDefaultObject.Create<PackageInfo>();

            customerProductAssignment.PurchasedProduct.AssociatedBundle = CreateDefaultObject.Create<BundleInfo>();

            customerProductAssignment.PurchasingCustomer = CreateDefaultObject.Create<CustomerInfo>();
            var servicesInfo1 = CreateDefaultObject.Create<ServicesInfo>();
            servicesInfo1.StartDate = startDate;
            servicesInfo1.EndDate = endDate;
            servicesInfo1.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();
            var servicesInfo2 = CreateDefaultObject.Create<ServicesInfo>();
            servicesInfo2.StartDate = startDate;
            servicesInfo2.EndDate = null;
            servicesInfo2.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();

            customerProductAssignment.PurchasingCustomer.ServicesInfo = new List<ServicesInfo>() { servicesInfo1, servicesInfo2 };
            var promotion1 = new CrmCustomersPromotionInfo();
            promotion1.Active = false;
            promotion1.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion1.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 3;
            promotion1.StartDate = startDate;
            promotion1.EndDate = endDate;
            var promotion2 = new CrmCustomersPromotionInfo();
            promotion2.Active = true;
            promotion2.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion2.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 2;
            promotion2.StartDate = startDate;
            promotion2.EndDate = endDate;
            var promotion3 = new CrmCustomersPromotionInfo();
            promotion3.Active = true;
            promotion3.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion3.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 1;
            promotion3.StartDate = startDate;
            promotion3.EndDate = null;
            var promotion4 = new CrmCustomersPromotionInfo();
            promotion4.Active = true;
            promotion4.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion4.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 1;
            promotion4.StartDate = startDateClosest;
            promotion4.EndDate = endDate;
            var promotion5 = new CrmCustomersPromotionInfo();
            promotion5.Active = true;
            promotion5.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion5.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 1;
            promotion5.StartDate = endDate;
            promotion5.EndDate = endDate;

            customerProductAssignment.PurchasingCustomer.Promotions = new List<CrmCustomersPromotionInfo>() { promotion1, promotion2, promotion3, promotion4, promotion5 };

            customerProductAssignment.ProductChargePurchased = new ProductChargeOption();
            var charge1 = CreateDefaultObject.Create<ChargeNonRecurring>();
            var charge2 = CreateDefaultObject.Create<ChargeRecurring>();
            var charge3 = CreateDefaultObject.Create<ChargeNonRecurring>();
            var charge4 = CreateDefaultObject.Create<ChargeRecurring>();
            customerProductAssignment.ProductChargePurchased.Charges = new List<Charge>() { charge1, charge2, charge3, charge4 };
            #endregion
            //Mocked getCustomerProductAssignmentByIdMS
            var mockedGetCustomerProductAssignmentByIdMS = MockMicroService<GetCustomerProductAssignmentByIdRequest, GetCustomerProductAssignmentByIdResponse>();
            mockedGetCustomerProductAssignmentByIdMS.Process(Arg.Any<GetCustomerProductAssignmentByIdRequest>(), null)
               .Returns(new GetCustomerProductAssignmentByIdResponse()
               {
                   CustomerProductAssignment = customerProductAssignment,
                   ResultType = ResultTypes.Ok,

               });

            //Mocked getActiveCusotmerAccountMs
            var mockedGetActiveCusotmerAccountMs = MockMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();
            mockedGetActiveCusotmerAccountMs.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(), null)
               .Returns(new GetActiveCustomerAccountAssociationByDateResponse()
               {
                   CustomerAccountAssociation = CreateDefaultObject.Create<CustomerAccountAssociation>(),
                   ResultType = ResultTypes.Ok,

               });
            //Mocked GetCustomerChargesByCustomerIdMS
            var calculateNextBillRunDateForBillCycleMs = MockMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            calculateNextBillRunDateForBillCycleMs.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(), null)
               .Returns(new CalculateNextBillRunDateForBillCycleResponse()
               {
                   NextBillRun = nextBillRunDate,
                   ResultType = ResultTypes.Ok,

               });

            #region Create Mock Repository

            var actualProductOffering = CreateDefaultObject.Create<ProductOffering>();

            _mockProductOfferingRepo = MockRepositoryManager.GetMockedRepository<IProductOfferingRepository<ProductOffering>>();
            _mockProductOfferingRepo.GetByGroupId(Arg.Any<int>())
                .Returns(new List<ProductOffering>() { actualProductOffering });



            #endregion


            MockAbstractSinglePhaseOrderProcessor(request);

            var response = CallBizOp(request);

            Assert.IsTrue(response.errorCode == BizOpsErrors.InvalidCancelDate);
        }

        [Test()]
        public void CancelCustomerProductBizOp_CancelDateLaterThanNow_ShouldOK()
        {
            var request = new CancelCustomerProductRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerProductAssignmentId = 1,
                CancelDate = DateTime.Now.AddSeconds(1),
                CurrentTime = DateTime.Now,
                UseNextBillCycleEndDateInRecurring = true
            };

            #region
            var customerProductAssignment = new CustomerProductAssignment();
            customerProductAssignment.ProductOffering = CreateDefaultObject.Create<ProductOffering>();
            customerProductAssignment.ProductOffering.Id = productid;
            customerProductAssignment.ProductOffering.Names = CreateDefaultObject.Create<MultiLingualDescription>();
            customerProductAssignment.ProductOffering.Description = CreateDefaultObject.Create<MultiLingualDescription>();
            customerProductAssignment.ProductOffering.ChargingOptions = new List<ProductChargeOption>();
            customerProductAssignment.ProductOffering.Options = new List<ProductOfferingOption>();


            customerProductAssignment.PurchasedProduct = CreateDefaultObject.Create<Product>();
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan.PromotionPlanId = 1;
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan.StartDate = startDateClosest;
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan.EndDate = endDate;
            customerProductAssignment.PurchasedProduct.AssociatedPackage = CreateDefaultObject.Create<PackageInfo>();
            customerProductAssignment.PurchasedProduct.Names = CreateDefaultObject.Create<MultiLingualDescription>();
            customerProductAssignment.PurchasedProduct.Description = CreateDefaultObject.Create<MultiLingualDescription>();


            customerProductAssignment.PurchasedProduct.AssociatedBundle = CreateDefaultObject.Create<BundleInfo>();

            customerProductAssignment.PurchasingCustomer = CreateDefaultObject.Create<CustomerInfo>();
            customerProductAssignment.PurchasingCustomer.CustomerID = customerid;
            var actualResourcembInfo = CreateDefaultObject.Create<ResourceMBInfo>();
            actualResourcembInfo.CustomerInfo.CustomerID = customerid;
            customerProductAssignment.PurchasingCustomer.ResourceMBInfo = new List<ResourceMBInfo> { actualResourcembInfo };
            customerProductAssignment.PurchasingCustomer.CustomerID = customerid;
            var servicesInfo1 = CreateDefaultObject.Create<ServicesInfo>();
            servicesInfo1.StartDate = startDate;
            servicesInfo1.EndDate = endDate;
            servicesInfo1.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();
            var servicesInfo2 = CreateDefaultObject.Create<ServicesInfo>();
            servicesInfo2.StartDate = startDate;
            servicesInfo2.EndDate = null;
            servicesInfo2.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();

            customerProductAssignment.PurchasingCustomer.ServicesInfo = new List<ServicesInfo>() { servicesInfo1, servicesInfo2 };
            var promotion1 = new CrmCustomersPromotionInfo();
            promotion1.Active = false;
            promotion1.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion1.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 3;
            promotion1.StartDate = startDate;
            promotion1.EndDate = endDate;
            var promotion2 = new CrmCustomersPromotionInfo();
            promotion2.Active = true;
            promotion2.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion2.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 2;
            promotion2.StartDate = startDate;
            promotion2.EndDate = endDate;
            var promotion3 = new CrmCustomersPromotionInfo();
            promotion3.Active = true;
            promotion3.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion3.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 1;
            promotion3.StartDate = startDate;
            promotion3.EndDate = null;
            var promotion4 = new CrmCustomersPromotionInfo();
            promotion4.Active = true;
            promotion4.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion4.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 1;
            promotion4.StartDate = startDateClosest;
            promotion4.EndDate = endDate;
            var promotion5 = new CrmCustomersPromotionInfo();
            promotion5.Active = true;
            promotion5.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion5.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 1;
            promotion5.StartDate = endDate;
            promotion5.EndDate = endDate;

            customerProductAssignment.PurchasingCustomer.Promotions = new List<CrmCustomersPromotionInfo>() { promotion1, promotion2, promotion3, promotion4, promotion5 };

            customerProductAssignment.ProductChargePurchased = new ProductChargeOption();
            var charge1 = CreateDefaultObject.Create<ChargeNonRecurring>();
            var charge2 = CreateDefaultObject.Create<ChargeRecurring>();
            var charge3 = CreateDefaultObject.Create<ChargeNonRecurring>();
            var charge4 = CreateDefaultObject.Create<ChargeRecurring>();
            customerProductAssignment.ProductChargePurchased.Charges = new List<Charge>() { charge1, charge2, charge3, charge4 };
            #endregion
            //Mocked getCustomerProductAssignmentByIdMS
            var mockedGetCustomerProductAssignmentByIdMS = MockMicroService<GetCustomerProductAssignmentByIdRequest, GetCustomerProductAssignmentByIdResponse>();
            mockedGetCustomerProductAssignmentByIdMS.Process(Arg.Any<GetCustomerProductAssignmentByIdRequest>(), null)
               .Returns(new GetCustomerProductAssignmentByIdResponse()
               {
                   CustomerProductAssignment = customerProductAssignment,
                   ResultType = ResultTypes.Ok,

               });

            //Mocked getActiveCusotmerAccountMs
            var mockedGetActiveCusotmerAccountMs = MockMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();
            mockedGetActiveCusotmerAccountMs.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(), null)
               .Returns(new GetActiveCustomerAccountAssociationByDateResponse()
               {
                   CustomerAccountAssociation = CreateDefaultObject.Create<CustomerAccountAssociation>(),
                   ResultType = ResultTypes.Ok,

               });

            //Mocked calculateNextBillRunDateMS
            var mockedCalculateNextBillRunDateMS = MockMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            mockedCalculateNextBillRunDateMS.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(), null)
               .Returns(new CalculateNextBillRunDateForBillCycleResponse()
               {
                   NextBillRun = nextBillRunDate,
                   ResultType = ResultTypes.Ok,

               });


            var mockedCancelCustomersPromotionMS = MockMicroService<CancelCustomersPromotionRequest, CancelCustomersPromotionResponse>();
            mockedCancelCustomersPromotionMS.Process(Arg.Any<CancelCustomersPromotionRequest>(), null)
                .Returns(new CancelCustomersPromotionResponse()
                {
                    ResultType = ResultTypes.Ok,
                    CrmCustomersPromotionInfo = CreateDefaultObject.Create<CrmCustomersPromotionInfo>(),

                });
            var mockedGetCustomerPromotionOperationLogByCustomerIDAndPromotionMS = MockMicroService<GetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest, GetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse>();
            mockedGetCustomerPromotionOperationLogByCustomerIDAndPromotionMS.Process(Arg.Any<GetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest>(), null)
               .Returns(new GetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse()
               {
                   LogInfo = CreateDefaultObject.Create<CrmCustomersPromotionOperationLogInfo>(),
                   ResultType = ResultTypes.Ok,

               });


            var mockedCreateCustomerPromotionLogInfoMS = MockMicroService<CreateCustomerPromotionLogInfoRequest, CreateCustomerPromotionLogInfoResponse>();
            mockedCreateCustomerPromotionLogInfoMS.Process(Arg.Any<CreateCustomerPromotionLogInfoRequest>(), null)
               .Returns(new CreateCustomerPromotionLogInfoResponse()
               {
                   ResultType = ResultTypes.Ok,

               });
            var mockedCancelServiceInfoMS = MockMicroService<CancelServicesInfoRequest, CancelServicesInfoResponse>();
            mockedCancelServiceInfoMS.Process(Arg.Any<CancelServicesInfoRequest>(), null)
               .Returns(new CancelServicesInfoResponse()
               {
                   ResultType = ResultTypes.Ok,

               });
            var mockedCancelCustomerPackagesMS = MockMicroService<CancelCustomerPackagesRequest, CancelCustomerPackagesResponse>();
            mockedCancelCustomerPackagesMS.Process(Arg.Any<CancelCustomerPackagesRequest>(), null)
               .Returns(new CancelCustomerPackagesResponse()
               {
                   ResultType = ResultTypes.Ok,

               });
            var mockedGetCustomerChargesByCustomerIdMS = MockMicroService<GetCustomerChargesByCustomerIdRequest, GetCustomerChargesByCustomerIdResponse>();
            var customerCharge = CreateDefaultObject.Create<CustomerCharge>();
            customerCharge.Schedule.ChargeDefinition = charge4;
            mockedGetCustomerChargesByCustomerIdMS.Process(Arg.Any<GetCustomerChargesByCustomerIdRequest>(), null)
               .Returns(new GetCustomerChargesByCustomerIdResponse()
               {
                   ResultType = ResultTypes.Ok,
                   CustomerCharges = new List<CustomerCharge>() { customerCharge },
               });
            var mockedGetCustomerChargeScheduleByCustomerMS = MockMicroService<GetCustomerChargeScheduleByCustomerRequest, GetCustomerChargeScheduleByCustomerResponse>();
            var customerChargeSchedule = CreateDefaultObject.Create<CustomerChargeSchedule>();
            customerChargeSchedule.ChargeDefinition = charge2;
            mockedGetCustomerChargeScheduleByCustomerMS.Process(Arg.Any<GetCustomerChargeScheduleByCustomerRequest>(), null)
               .Returns(new GetCustomerChargeScheduleByCustomerResponse()
               {
                   ResultType = ResultTypes.Ok,
                   CustomerChargeSchedule = new List<CustomerChargeSchedule>() { customerChargeSchedule },
               });
            var mockedCancelCustomerChargeScheduleMs = MockMicroService<CancelCustomerChargeScheduleRequest, CancelCustomerChargeScheduleResponse>();
            mockedCancelCustomerChargeScheduleMs.Process(Arg.Any<CancelCustomerChargeScheduleRequest>(), null)
               .Returns(new CancelCustomerChargeScheduleResponse()
               {
                   ResultType = ResultTypes.Ok,

               });
            var cancelCustomerProductAssignmentMs = MockMicroService<CancelCustomerProductAssignmentRequest, CancelCustomerProductAssignmentResponse>();
            cancelCustomerProductAssignmentMs.Process(Arg.Any<CancelCustomerProductAssignmentRequest>(), null)
               .Returns(new CancelCustomerProductAssignmentResponse()
               {
                   ResultType = ResultTypes.Ok,

               });

            var getActiveCustomerAccountAssociationByDate = MockMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();
            getActiveCustomerAccountAssociationByDate.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(), null)
               .Returns(new GetActiveCustomerAccountAssociationByDateResponse()
               {
                   CustomerAccountAssociation = CreateDefaultObject.Create<CustomerAccountAssociation>(),
                   ResultType = ResultTypes.Ok,

               });


            var calculateNextBillRunDateForBillCycleMs = MockMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            calculateNextBillRunDateForBillCycleMs.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(), null)
               .Returns(new CalculateNextBillRunDateForBillCycleResponse()
               {
                   NextBillRun = nextBillRunDate,
                   ResultType = ResultTypes.Ok,

               });

            #region Create Mock Repository

            var actualProductOffering = CreateDefaultObject.Create<ProductOffering>();

            _mockProductOfferingRepo = MockRepositoryManager.GetMockedRepository<IProductOfferingRepository<ProductOffering>>();
            _mockProductOfferingRepo.GetByGroupId(Arg.Any<int>())
                .Returns(new List<ProductOffering>() { actualProductOffering });



            #endregion


            MockAbstractSinglePhaseOrderProcessor(request);

            var actualCancelCustomerProductResponseDTO = CallBizOp(request);

            var expectedCancelCustomerProductResponseDTO = new CancelCustomerProductResponseDTO
            {
                Customer = new CustomerDTO { CustomerId = customerid},
                Subscription = new SubscriptionDTO { CustomerId = customerid},
                ProductCatalog = new ProductCatalogDTO { Id = productid}                
            };

            Assert.IsTrue(actualCancelCustomerProductResponseDTO.resultType == ResultTypes.Ok);
            Assert.IsTrue(actualCancelCustomerProductResponseDTO.Customer.CustomerId == expectedCancelCustomerProductResponseDTO.Customer.CustomerId);
            Assert.IsTrue(actualCancelCustomerProductResponseDTO.Subscription.CustomerId == expectedCancelCustomerProductResponseDTO.Subscription.CustomerId);
            Assert.IsTrue(actualCancelCustomerProductResponseDTO.ProductCatalog.Id == expectedCancelCustomerProductResponseDTO.ProductCatalog.Id);
        }

        [Test()]
        public void CancelCustomerProductBizOp_CancelExpiredRecurringProudct_ShouldNotOK()
        {
            var request = new CancelCustomerProductRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerProductAssignmentId = 1,
                CancelDate = DateTime.Now.AddSeconds(1),
                CurrentTime = DateTime.Now,
                UseNextBillCycleEndDateInRecurring = false
            };

            #region
            var customerProductAssignment = new CustomerProductAssignment()
            {
                StartDate = startDate,
                EndDate = DateTime.Now.AddDays(-1)
            };
            customerProductAssignment.ProductOffering = CreateDefaultObject.Create<ProductOffering>();
            customerProductAssignment.ProductOffering.Id = productid;
            customerProductAssignment.ProductOffering.Names = CreateDefaultObject.Create<MultiLingualDescription>();
            customerProductAssignment.ProductOffering.Description = CreateDefaultObject.Create<MultiLingualDescription>();
            customerProductAssignment.ProductOffering.ChargingOptions = new List<ProductChargeOption>();
            customerProductAssignment.ProductOffering.Options = new List<ProductOfferingOption>();

            customerProductAssignment.PurchasedProduct = CreateDefaultObject.Create<Product>();
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan.PromotionPlanId = 1;
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan.StartDate = startDateClosest;
            customerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan.EndDate = endDate;
            customerProductAssignment.PurchasedProduct.AssociatedPackage = CreateDefaultObject.Create<PackageInfo>();

            customerProductAssignment.PurchasedProduct.AssociatedBundle = CreateDefaultObject.Create<BundleInfo>();

            customerProductAssignment.PurchasingCustomer = CreateDefaultObject.Create<CustomerInfo>();
            var servicesInfo1 = CreateDefaultObject.Create<ServicesInfo>();
            servicesInfo1.StartDate = startDate;
            servicesInfo1.EndDate = endDate;
            servicesInfo1.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();
            var servicesInfo2 = CreateDefaultObject.Create<ServicesInfo>();
            servicesInfo2.StartDate = startDate;
            servicesInfo2.EndDate = null;
            servicesInfo2.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();

            customerProductAssignment.PurchasingCustomer.ServicesInfo = new List<ServicesInfo>() { servicesInfo1, servicesInfo2 };
            var promotion1 = new CrmCustomersPromotionInfo();
            promotion1.Active = false;
            promotion1.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion1.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 3;
            promotion1.StartDate = startDate;
            promotion1.EndDate = endDate;
            var promotion2 = new CrmCustomersPromotionInfo();
            promotion2.Active = true;
            promotion2.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion2.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 2;
            promotion2.StartDate = startDate;
            promotion2.EndDate = endDate;
            var promotion3 = new CrmCustomersPromotionInfo();
            promotion3.Active = true;
            promotion3.PromotionDetail.RmPromotionPlanInfo = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotion3.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 1;
            promotion3.StartDate = startDate;
            promotion3.EndDate = null;


            customerProductAssignment.PurchasingCustomer.Promotions = new List<CrmCustomersPromotionInfo>() { promotion1, promotion2, promotion3 };

            customerProductAssignment.ProductChargePurchased = new ProductChargeOption();
            var charge1 = CreateDefaultObject.Create<ChargeNonRecurring>();
            var charge2 = CreateDefaultObject.Create<ChargeRecurring>();
            var charge3 = CreateDefaultObject.Create<ChargeNonRecurring>();
            var charge4 = CreateDefaultObject.Create<ChargeRecurring>();
            customerProductAssignment.ProductChargePurchased.Charges = new List<Charge>() { charge1, charge2, charge3, charge4 };
            #endregion
            //Mocked getCustomerProductAssignmentByIdMS
            var mockedGetCustomerProductAssignmentByIdMS = MockMicroService<GetCustomerProductAssignmentByIdRequest, GetCustomerProductAssignmentByIdResponse>();
            mockedGetCustomerProductAssignmentByIdMS.Process(Arg.Any<GetCustomerProductAssignmentByIdRequest>(), null)
               .Returns(new GetCustomerProductAssignmentByIdResponse()
               {
                   CustomerProductAssignment = customerProductAssignment,
                   ResultType = ResultTypes.Ok,

               });

            //Mocked getActiveCusotmerAccountMs
            var mockedGetActiveCusotmerAccountMs = MockMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();
            mockedGetActiveCusotmerAccountMs.Process(Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(), null)
               .Returns(new GetActiveCustomerAccountAssociationByDateResponse()
               {
                   CustomerAccountAssociation = CreateDefaultObject.Create<CustomerAccountAssociation>(),
                   ResultType = ResultTypes.Ok,

               });
            //Mocked GetCustomerChargesByCustomerIdMS
            var calculateNextBillRunDateForBillCycleMs = MockMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            calculateNextBillRunDateForBillCycleMs.Process(Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(), null)
               .Returns(new CalculateNextBillRunDateForBillCycleResponse()
               {
                   NextBillRun = nextBillRunDate,
                   ResultType = ResultTypes.Ok,

               });

            #region Create Mock Repository

            var actualProductOffering = CreateDefaultObject.Create<ProductOffering>();

            _mockProductOfferingRepo = MockRepositoryManager.GetMockedRepository<IProductOfferingRepository<ProductOffering>>();
            _mockProductOfferingRepo.GetByGroupId(Arg.Any<int>())
                .Returns(new List<ProductOffering>() { actualProductOffering });



            #endregion


            MockAbstractSinglePhaseOrderProcessor(request);

            var response = CallBizOp(request);

            Assert.IsTrue(response.errorCode == BizOpsErrors.ProductHasBeenExpired);
        }
    }
}
