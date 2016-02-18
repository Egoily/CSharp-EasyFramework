using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.bizops.fullfilment.CancelCustomerAndSubscriptions;
using com.etak.core.bizops.fullfilment.RegisterCustomer;
using com.etak.core.bizops.helper;
using com.etak.core.customer.message.GetActiveCustomerAccountAssociationByDate;
using com.etak.core.customer.message.GetPropertyInfosByDocumentId;
using com.etak.core.GSMSubscription.message.GetCustomerDataRoamingLimitNotificationByCustomerId;
using com.etak.core.GSMSubscription.messages.GetCustomerDataRoamingLimitsByCustomerID;
using com.etak.core.GSMSubscription.messages.GetLastSubscriptionByMsisdn;
using com.etak.core.GSMSubscription.messages.GetProvisioningTemplateById;
using com.etak.core.GSMSubscription.messages.GetRoamingBlackListByCustomerID;
using com.etak.core.model;
using com.etak.core.model.dto;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.product.message.CalculateNextBillRunDateForBillCycle;
using com.etak.core.product.message.GetProductByProductId;
using com.etak.core.product.message.GetProductOfferingByProductOfferingId;
using com.etak.core.product.message.GetRmPromotionGroupMemberAll;
using com.etak.core.promotion.messages.CreateCustomerPromotionLogInfo;
using com.etak.core.promotion.messages.CreateLogPromotion;
using com.etak.core.promotion.messages.GetCustomerPromotionOperationLogByCustomerIDAndPromotion;
using com.etak.core.promotion.messages.UpdateCustomersPromotion;
using com.etak.core.repository;
using com.etak.core.repository.crm.configuration;
using com.etak.core.repository.crm.operation;
using com.etak.core.repository.crm.subscription.catalog;
using com.etak.core.resource.msisdn.message.GetNumberByResource;
using com.etak.core.resource.simCard.message.GetSimCardByICCId;
using com.etak.core.resource.simCard.message.InitSimCard;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using com.etak.vfes.implementation.bizOps.ChangeOfHolderBizOp;
using FluentNHibernate.Testing.Values;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace com.etak.core.bizops.UnitTests.fullfilment.ChangeOfHolder
{
    [TestFixture]
    public class ChangeOfHolderBizopUnitTests : AbstractSinglePhaseOrderProcessorTest<ChangeOfHolderBizOp, ChangeOfHolderRequestDTO, ChangeOfHolderResponseDTO,
        ChangeOfHolderRequestInternal, ChangeOfHolderResponseInternal,ChangeOfHolderOrder>
    {

        private const string MSISDN = "34611470006";
        private const string ICC = "879000034634390024";
        private const string IMSI = "214034634390024";
        private const int DealerIdToMock = 1;
        private ResourceMBInfo resourceMBInfo;
        private CustomerInfo customerInfo;
        private CustomerInfo getCustomerInfoByDocumentID;
        private RmPromotionPlanInfo promotionPlan;
        private PropertyInfo customerProperty;
        private CrmCustomersPromotionInfo customerPromotion;
        private decimal newCustomerAccumulatedProtionCreditlimit = 5.0M;
        private const decimal configuredMaxTransfer = 20.0M;
        private ChangeOfHolderRequestDTO requestDto;

        private List<ProductOffering> productListOk = new List<ProductOffering>();
        private ProductOffering actualProductOffering;
        private ProductChargeOption actualProductChargeOption = CreateDefaultObject.Create<ProductChargeOption>();

        #region mocked MS and BizOp
        private ICoreBusinessOperation<CancelCustomerAndSubscriptionsRequestInternal, CancelCustomerAndSubscriptionsResponseInternal>
            mockCancleCustomerBizop;

        private ICoreBusinessOperation<RegisterCustomerRequestInternal, RegisterCustomerResponseInternal>
            mockRegisterCustomerBizop;
        private IOperationConfigurationRepository<OperationConfiguration> mockConfigurationRepository;
        private IMicroService<InitSimCardRequest, InitSimCardResponse> mockInitSimcardMS;
        private IMicroService<GetLastSubscriptionByMsisdnRequest, GetLastSubscriptionByMsisdnResponse> mockGetLastSubscriptionByMsisdnMS;
        private IMicroService<GetNumberByResourceRequest, GetNumberByResourceResponse> mockGetNumberByResourceMS;
        private IMicroService<GetSimCardByICCIdRequest, GetSimCardByICCIdResponse> mockGetSimCardByICCIdMS;
        private IMicroService<GetProvisioningTemplateByIdRequest, GetProvisioningTemplateByIdResponse> mockGetProvisioningTemplateByIdMS;

        private IMicroService<GetCustomerDataRoamingLimitsByCustomerIDRequest, GetCustomerDataRoamingLimitsByCustomerIDResponse>
            mockGetCustomerDataRoamingLimitsByCustomerIDMS;

        private IMicroService<GetCustomerDataRoamingLimitNotificationByCustomerIdRequest,
                        GetCustomerDataRoamingLimitNotificationByCustomerIdResponse>
            mockGetCustomerDataRoamingLimitNotificationByCustomerIdMS;

        private IMicroService<GetRoamingBlackListByCustomerIDRequest, GetRoamingBlackListByCustomerIDResponse>
           mockGetRoamingBlackListByCustomerIDMS;

        private IMicroService
                <GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>
            mockGetActiveCustomerAccountAssociationByDateMS;

        private IMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>
            mockCalculateNextBillRunDateForBillCycleMS;

        private IMicroService<GetRmPromotionGroupMemberAllRequest, GetRmPromotionGroupMemberAllResponse>
          mockGetRmPromotionGroupMemberMS;

        private IMicroService<UpdateCustomersPromotionRequest, UpdateCustomersPromotionResponse>
           mockUpdateCustomersPromotionMS;

        private IMicroService<GetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest, GetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse> 
            mockGetCustomerPromotionOperationLogByCustomerIDAndPromotionMS;
        private IMicroService<CreateCustomerPromotionLogInfoRequest, CreateCustomerPromotionLogInfoResponse>
          mockCreateCustomerPromotionLogInfoMS;

        private IMicroService<GetPropertyInfosByDocumentIdRequest, GetPropertyInfosByDocumentIdResponse>
            mockGetPropertyInfosByDocumentIdMS;

        private IMicroService<GetProductByProductIdRequest, GetProductByProductIdResponse>
            mockGetProductByProductIdMS;


        private
            IMicroService<GetProductOfferingByProductOfferingIdRequest, GetProductOfferingByProductOfferingIdResponse>
            mockGetProductOfferingByProductOfferingId;
        #endregion



        #region Define Mock Repo

        private IProductOfferingRepository<ProductOffering> mockProductOfferingRepo;

        #endregion

        #region Dependency Injection Mocks

        private IPurchaseHelper mockPurchaseHelper;

        #endregion


        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            mockConfigurationRepository = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            mockCancleCustomerBizop = Substitute.For<ICoreBusinessOperation<CancelCustomerAndSubscriptionsRequestInternal, CancelCustomerAndSubscriptionsResponseInternal>>();
            mockRegisterCustomerBizop = Substitute.For<ICoreBusinessOperation<RegisterCustomerRequestInternal, RegisterCustomerResponseInternal>>();
            BusinessOperationManager.RebindCoreInterfaceToConstant(DealerIdToMock, mockCancleCustomerBizop);
            BusinessOperationManager.RebindCoreInterfaceToConstant(DealerIdToMock, mockRegisterCustomerBizop);

           mockGetLastSubscriptionByMsisdnMS = MockMicroServiceManager.GetMockedMicroService<GetLastSubscriptionByMsisdnRequest, GetLastSubscriptionByMsisdnResponse>();
           mockGetNumberByResourceMS = MockMicroServiceManager.GetMockedMicroService<GetNumberByResourceRequest, GetNumberByResourceResponse>();
           mockGetSimCardByICCIdMS = MockMicroServiceManager.GetMockedMicroService<GetSimCardByICCIdRequest, GetSimCardByICCIdResponse>();
           mockGetProvisioningTemplateByIdMS = MockMicroServiceManager.GetMockedMicroService<GetProvisioningTemplateByIdRequest, GetProvisioningTemplateByIdResponse>();
           mockGetCustomerDataRoamingLimitsByCustomerIDMS =MockMicroServiceManager.GetMockedMicroService
                    <GetCustomerDataRoamingLimitsByCustomerIDRequest, GetCustomerDataRoamingLimitsByCustomerIDResponse>();
            mockGetCustomerDataRoamingLimitNotificationByCustomerIdMS = MockMicroServiceManager.GetMockedMicroService
                <GetCustomerDataRoamingLimitNotificationByCustomerIdRequest,
                    GetCustomerDataRoamingLimitNotificationByCustomerIdResponse>();
            mockGetRoamingBlackListByCustomerIDMS = MockMicroServiceManager.GetMockedMicroService
                 <GetRoamingBlackListByCustomerIDRequest, GetRoamingBlackListByCustomerIDResponse>();
            mockGetActiveCustomerAccountAssociationByDateMS =
                MockMicroServiceManager
                    .GetMockedMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();
            mockCalculateNextBillRunDateForBillCycleMS = MockMicroServiceManager.GetMockedMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();

            mockGetRmPromotionGroupMemberMS = MockMicroServiceManager.GetMockedMicroService<GetRmPromotionGroupMemberAllRequest, GetRmPromotionGroupMemberAllResponse>();
            
             mockInitSimcardMS = MockMicroServiceManager.GetMockedMicroService<InitSimCardRequest, InitSimCardResponse>();
            mockUpdateCustomersPromotionMS = MockMicroServiceManager.GetMockedMicroService<UpdateCustomersPromotionRequest, UpdateCustomersPromotionResponse>();

             mockGetCustomerPromotionOperationLogByCustomerIDAndPromotionMS =
                 MockMicroServiceManager.GetMockedMicroService<GetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest, GetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse>();
             mockCreateCustomerPromotionLogInfoMS = MockMicroServiceManager.GetMockedMicroService<CreateCustomerPromotionLogInfoRequest, CreateCustomerPromotionLogInfoResponse>();

             mockGetPropertyInfosByDocumentIdMS = MockMicroServiceManager.GetMockedMicroService<GetPropertyInfosByDocumentIdRequest, GetPropertyInfosByDocumentIdResponse>();

             mockGetProductByProductIdMS = MockMicroServiceManager.GetMockedMicroService<GetProductByProductIdRequest, GetProductByProductIdResponse>();


            mockGetProductOfferingByProductOfferingId =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <GetProductOfferingByProductOfferingIdRequest, GetProductOfferingByProductOfferingIdResponse>();



            #region Create & Setup Mock Repository

            var actualProductOffering = CreateDefaultObject.Create<ProductOffering>();

            mockProductOfferingRepo = MockRepositoryManager.GetMockedRepository<IProductOfferingRepository<ProductOffering>>();
            mockProductOfferingRepo.GetByGroupId(Arg.Any<int>())
                 .Returns(productListOk);

            #endregion

            #region Create & Setup mock Dependency Injection

            mockPurchaseHelper = Substitute.For<IPurchaseHelper>();
            mockPurchaseHelper.GetProductsAndChargesOptions(Arg.Any<List<ProductCatalogDTO>>()).
                Returns(new List<Tuple<ProductOffering, ProductChargeOption>>()
                {
                    new Tuple<ProductOffering, ProductChargeOption>(actualProductOffering,actualProductChargeOption) 
                });

            #endregion

        }

        [Test()]
        public void ChangeOfHolderBizop_ReturnOK()
        {


            prepareData();

            mockProcees(resourceMBInfo, customerInfo, promotionPlan, configuredMaxTransfer);

            MockAbstractSinglePhaseOrderProcessor(requestDto);
            ChangeOfHolderResponseDTO response = null;
            using (var conn = RepositoryManager.GetNewConnection())
            {
                ChangeOfHolderBizOp changeHolderBizop = new ChangeOfHolderBizOp();
                changeHolderBizop.PurchaseHelper = mockPurchaseHelper;
                
                response = changeHolderBizop.ProcessFromCustomerModel(new NullValidator<ChangeOfHolderRequestDTO>(),
                    new SameTypeConverter<ChangeOfHolderRequestDTO>(), new SameTypeConverter<ChangeOfHolderResponseDTO>(),
                    requestDto, FakeInvoker);

                

            }
            RepositoryManager.CloseConnection();

            Assert.AreEqual(response.resultType, ResultTypes.Ok);
            
        }

        [Test()]
        public void ChangeOfHolderBizop_ReturnError_WhileTheMsisdnDoesnotBelongCurrentMVNO()
        {
             prepareData();

            //set condition for case
            customerInfo.ResourceMBInfo.First().OperatorInfo.DealerID = 0;

            mockProcees(resourceMBInfo, customerInfo, promotionPlan, configuredMaxTransfer);

            MockAbstractSinglePhaseOrderProcessor(requestDto);
            ChangeOfHolderResponseDTO response = null;
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var changeHolderBizop = new ChangeOfHolderBizOp();
                response = changeHolderBizop.ProcessFromCustomerModel(new NullValidator<ChangeOfHolderRequestDTO>(),
                    new SameTypeConverter<ChangeOfHolderRequestDTO>(), new SameTypeConverter<ChangeOfHolderResponseDTO>(),
                    requestDto, FakeInvoker);


            }
            RepositoryManager.CloseConnection();

            Assert.AreEqual(response.resultType, ResultTypes.BussinessLogicError);
            Assert.AreEqual(response.errorCode, BizOpsErrors.MVNODontHavePermision);
        }

        [Test()]
        public void ChangeOfHolderBizop_ReturnError_WhileFirstHolderIsNotActived()
        {
            prepareData();

            //set condition  for case
            customerInfo.ResourceMBInfo.First().StatusID = (int) ResourceStatus.Deleted;

            mockProcees(resourceMBInfo, customerInfo, promotionPlan, configuredMaxTransfer);

            MockAbstractSinglePhaseOrderProcessor(requestDto);
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var changeHolderBizop = new ChangeOfHolderBizOp();
                var response = changeHolderBizop.ProcessFromCustomerModel(new NullValidator<ChangeOfHolderRequestDTO>(),
                    new SameTypeConverter<ChangeOfHolderRequestDTO>(), new SameTypeConverter<ChangeOfHolderResponseDTO>(),
                    requestDto, FakeInvoker);

                Assert.AreEqual(response.resultType, ResultTypes.BussinessLogicError);
                Assert.AreEqual(response.errorCode, BizOpsErrors.CustomerDoesNotHaveActiveResource);

            }
            RepositoryManager.CloseConnection();
        }
         [Test()]
        public void ChangeOfHolderBizop_ReturnError_WhileSecondHolderDocumentIDHasAnyfronzeMsisdnButIsSameMVNO()
        {
            prepareData();

            //set condition  for case
            getCustomerInfoByDocumentID.ResourceMBInfo = new ResourceMBInfo[]
            {
                new ResourceMBInfo() {StartDate = DateTime.Now.AddDays(-1), StatusID = (int) ResourceStatus.Frozen},
            };

            mockProcees(resourceMBInfo, customerInfo, promotionPlan, configuredMaxTransfer);

            MockAbstractSinglePhaseOrderProcessor(requestDto);
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var changeHolderBizop = new ChangeOfHolderBizOp();
                var response = changeHolderBizop.ProcessFromCustomerModel(new NullValidator<ChangeOfHolderRequestDTO>(),
                    new SameTypeConverter<ChangeOfHolderRequestDTO>(), new SameTypeConverter<ChangeOfHolderResponseDTO>(),
                    requestDto, FakeInvoker);

                Assert.AreEqual(response.resultType, ResultTypes.BussinessLogicError);
                Assert.AreEqual(response.errorCode, BizOpsErrors.ResourceStatusIsFrozen);

            }
            RepositoryManager.CloseConnection();
        }
         [Test()]
         public void ChangeOfHolderBizop_ReturnOK_WhileSecondHolderDocumentIDHasAnyfronzeMsisdnButIsAnotherMVNO()
         {
             prepareData();

             //set condition  for case
             getCustomerInfoByDocumentID.DealerID = 0;
             getCustomerInfoByDocumentID.ResourceMBInfo = new ResourceMBInfo[]
            {
                new ResourceMBInfo() {StartDate = DateTime.Now.AddDays(-1), StatusID = (int) ResourceStatus.Frozen},
            };

             mockProcees(resourceMBInfo, customerInfo, promotionPlan, configuredMaxTransfer);

             MockAbstractSinglePhaseOrderProcessor(requestDto);
             ChangeOfHolderResponseDTO response = null;
             using (var conn = RepositoryManager.GetNewConnection())
             {
                 ChangeOfHolderBizOp changeHolderBizop = new ChangeOfHolderBizOp();
                 changeHolderBizop.PurchaseHelper = mockPurchaseHelper;
                 response = changeHolderBizop.ProcessFromCustomerModel(new NullValidator<ChangeOfHolderRequestDTO>(),
                     new SameTypeConverter<ChangeOfHolderRequestDTO>(), new SameTypeConverter<ChangeOfHolderResponseDTO>(),
                     requestDto, FakeInvoker);
             }
             RepositoryManager.CloseConnection();


             Assert.AreEqual(response.resultType, ResultTypes.Ok);
         }

         /// <summary>
         /// test case that the secondHolderHasBenefit is less than the max config value,
         ///  and first holder has more benefit than  max configured value
         /// </summary>
         /// <param name="firstHolderHasBenefit">total benefit that first holder left</param>
         /// <param name="secondHolderHasBenefit">total benefit  that second holder has own</param>
         /// <param name="expected">total expected transfer benefit from first holder</param>
        [TestCase(15.0005, 8.0, 12.0)]
         public void ChangeOfHolderBizop_TransferBenefit_Case1(decimal firstHolderHasBenefit, decimal secondHolderHasBenefit, decimal expected)
        {
            ChangeOfHolderBizop_TransferBenefit_ReturnAsExpectedValue(firstHolderHasBenefit, secondHolderHasBenefit, expected, configuredMaxTransfer);
        }
        /// <summary>
        /// test case that the secondHolderHasBenefit is less than the max config value, 
        /// and first holder has less benefits than  max configured value
        /// </summary>
        /// <param name="firstHolderHasBenefit">total benefit that first holder left</param>
        /// <param name="secondHolderHasBenefit">total benefit  that second holder has own</param>
        /// <param name="expected">total expected transfer benefit from first holder</param>
        [TestCase(8.5005, 1.0, 8.5005)]
        public void ChangeOfHolderBizop_TransferBenefit_Case2(decimal firstHolderHasBenefit, decimal secondHolderHasBenefit, decimal expected)
        {
            ChangeOfHolderBizop_TransferBenefit_ReturnAsExpectedValue(firstHolderHasBenefit, secondHolderHasBenefit, expected, configuredMaxTransfer);
        }

        /// <summary>
        /// test case that the secondHolderHasBenefit is greater or equal than the max config value
        /// </summary>
        /// <param name="firstHolderHasBenefit">total benefit that first holder left</param>
        /// <param name="secondHolderHasBenefit">total benefit  that second holder has own</param>
        /// <param name="expected">total expected transfer benefit from first holder</param>
        [TestCase(8.0, 21.0, 0.0)]
        public void ChangeOfHolderBizop_TransferBenefit_Case3(decimal firstHolderHasBenefit, decimal secondHolderHasBenefit, decimal expected)
        {
            ChangeOfHolderBizop_TransferBenefit_ReturnAsExpectedValue(firstHolderHasBenefit, secondHolderHasBenefit, expected, configuredMaxTransfer);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstHolderHasBenefit">total benefit that first holder left</param>
        /// <param name="secondHolderHasBenefit">total benefit  that second holder has own</param>
        /// <param name="expected">total expected transfer benefit from first holder</param>
        /// <param name="maxTransfer">the max allowed value that can be transfered from first holder to second holder</param>
        private void ChangeOfHolderBizop_TransferBenefit_ReturnAsExpectedValue(decimal firstHolderHasBenefit, decimal secondHolderHasBenefit, decimal expected, decimal maxTransfer = configuredMaxTransfer)
        {
            prepareData();

            //set condition  for case
            customerPromotion.CurrentLimit = firstHolderHasBenefit;
            newCustomerAccumulatedProtionCreditlimit = secondHolderHasBenefit;

            mockProcees(resourceMBInfo, customerInfo, promotionPlan,maxTransfer);

            MockAbstractSinglePhaseOrderProcessor(requestDto);
            ChangeOfHolderResponseDTO response = null;
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var changeHolderBizop = new ChangeOfHolderBizOp();
                changeHolderBizop.PurchaseHelper = mockPurchaseHelper;
                response = changeHolderBizop.ProcessFromCustomerModel(new NullValidator<ChangeOfHolderRequestDTO>(),
                    new SameTypeConverter<ChangeOfHolderRequestDTO>(), new SameTypeConverter<ChangeOfHolderResponseDTO>(),
                    requestDto, FakeInvoker);

            }
            RepositoryManager.CloseConnection();

            Assert.AreEqual(response.resultType, ResultTypes.Ok);

            Assert.AreEqual(response.BenefitTransfered, expected);
        }

        private void prepareData()
        {
            resourceMBInfo = CreateDefaultObject.Create<ResourceMBInfo>();
            resourceMBInfo.Resource = MSISDN;
            resourceMBInfo.ICC = ICC;
            resourceMBInfo.IMSI = IMSI;
            resourceMBInfo.StatusID = 1;

            customerInfo = CreateDefaultObject.Create<CustomerInfo>();
            resourceMBInfo.CustomerInfo = customerInfo;
            customerInfo.ResourceMBInfo = new List<ResourceMBInfo>() { resourceMBInfo };

            customerProperty = CreateDefaultObject.Create<PropertyInfo>();
            customerProperty.CustomerInfo = customerInfo;
            customerProperty.IDType = 3;
            customerProperty.IDNumber = "123456";
            customerInfo.PropertyInfo = new PropertyInfo[] { customerProperty };

            promotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotionPlan.Accumulative = true;

            customerPromotion = CreateDefaultObject.Create<CrmCustomersPromotionInfo>();
            customerPromotion.StartDate = DateTime.Now.AddDays(-1);
            customerPromotion.EndDate = null;
            customerPromotion.CurrentLimit = 5;
            customerPromotion.Active = true;
            customerPromotion.PromotionDetail = CreateDefaultObject.Create<RmPromotionPlanDetailInfo>();
            customerPromotion.PromotionDetail.RmPromotionPlanInfo = promotionPlan;
            customerInfo.Promotions = new CrmCustomersPromotionInfo[]
            {
                customerPromotion
            };

            getCustomerInfoByDocumentID = CreateDefaultObject.Create<CustomerInfo>();

            CustomerDTO customerData = CreateDefaultObject.Create<CustomerDTO>();
            customerData.CustomerData = CreateDefaultObject.Create<CustomerDataDTO>();
            customerData.CustomerData.BankInformation = CreateDefaultObject.Create<BankInformationDTO>();
            customerData.CustomerData.DeliveryAddress = CreateDefaultObject.Create<AddressDTO>();
            customerData.CustomerData.DocumentType = DocumentTypes.DNI;
            customerData.CustomerData.DocumentNumber = "12345";

            List<ProductCatalogDTO> productsToPurchase = new List<ProductCatalogDTO>();
            ProductCatalogDTO productDto = CreateDefaultObject.Create<ProductCatalogDTO>();
            productDto.PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>() { CreateDefaultObject.Create<ProductPurchaseChargingOptionDTO>() };
            productsToPurchase.Add(productDto);

            requestDto = new ChangeOfHolderRequestDTO()
            {
                MSISDN = MSISDN,
                channel = "channel",
                DocumentType = DocumentTypes.DNI,
                DocumentNumber = "123456",
                CustomerData = customerData,
                PurchasedProducts = productsToPurchase,
                user = "1644000204",
                password = "admin",
                vmno = "970100"
            };
        }

        private void mockProcees(ResourceMBInfo resourceMBInfo, CustomerInfo customerInfo, RmPromotionPlanInfo promotionPlan, decimal configuredMaxTransfer)
        {
            #region Mock OperationConfig
            var mockedRepoConfig = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var operationConfiguration = CreateDefaultObject.Create<OperationConfiguration>();
            var changeOfHolderCf = new ChangeOfHolderConfiguration()
            {
                MaxAmountTransfer = configuredMaxTransfer,
                PromotionGroupID = 1008000001
            };
            operationConfiguration.JSonConfig = JsonConvert.SerializeObject(changeOfHolderCf);
            operationConfiguration.StarTime = DateTime.Now;
            operationConfiguration.EndDate = DateTime.Now.AddYears(1);
            mockedRepoConfig.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).
                Returns(new List<OperationConfiguration>() { operationConfiguration });

            #endregion

            #region Mock PurchaseHelper
            var productDict = CreateDefaultObject.Create<ProductOffering>();
            
            var chargeOptionDict = new ProductChargeOption()
            {
                CreateDate = DateTime.Now,
                Charges = new List<Charge>() { CreateDefaultObject.Create<ChargeNonRecurring>() },
                Id = 1,
                ProductOffering = productDict,
                StartDate = DateTime.Now,
                IsDefaultOption = DefaultOptions.Y,
                Status = ProductPurchaseStatus.Default
            };
            productDict.ChargingOptions.Add(chargeOptionDict);
            mockGetProductByProductIdMS.Process(new GetProductByProductIdRequest(),
                Arg.Any<RequestInvokationEnvironment>()).
                ReturnsForAnyArgs(new GetProductByProductIdResponse()
                {
                    Product = productDict.OfferedProduct,
                    ResultType = ResultTypes.Ok
                });
          
            #endregion

            mockGetLastSubscriptionByMsisdnMS.Process(new GetLastSubscriptionByMsisdnRequest()
            {
                Msisdn = MSISDN
            }, Arg.Any<RequestInvokationEnvironment>())
                .ReturnsForAnyArgs(new GetLastSubscriptionByMsisdnResponse()
                {
                    ResourceMBInfo = new List<ResourceMBInfo> { resourceMBInfo },
                    ResultType = ResultTypes.Ok
                });

            mockGetNumberByResourceMS.Process(new GetNumberByResourceRequest()
            {
                Resource = MSISDN
            }, Arg.Any<RequestInvokationEnvironment>())
                .ReturnsForAnyArgs(new GetNumberByResourceResponse()
                {
                    NumberInfo = new NumberInfo() { Resource = MSISDN },
                    ResultType = ResultTypes.Ok
                });

            mockGetSimCardByICCIdMS.Process(new GetSimCardByICCIdRequest()
            {
                IccId = ICC
            },
                Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(
                new GetSimCardByICCIdResponse()
                {
                    SimCardInfo = new SIMCardInfo() { ICCID = ICC, IMSI1 = IMSI },
                    ResultType = ResultTypes.Ok
                });

            mockGetPropertyInfosByDocumentIdMS.Process(new GetPropertyInfosByDocumentIdRequest(),
                Arg.Any<RequestInvokationEnvironment>())
                .ReturnsForAnyArgs(new GetPropertyInfosByDocumentIdResponse()
                {
                    PropertyInfos = new List<PropertyInfo> { new PropertyInfo()
                    {
                        CustomerInfo = getCustomerInfoByDocumentID
                    }},
                    ResultType = ResultTypes.Ok
                });

            mockGetProvisioningTemplateByIdMS.Process(new GetProvisioningTemplateByIdRequest(),
                Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(new GetProvisioningTemplateByIdResponse
                {
                    CrmDefaultProvision = new CrmDefaultProvisionInfo(),
                    ResultType = ResultTypes.Ok
                });

            mockGetCustomerDataRoamingLimitsByCustomerIDMS.Process(
                new GetCustomerDataRoamingLimitsByCustomerIDRequest()
                {
                    CustomerID = customerInfo.CustomerID.Value
                },
                Arg.Any<RequestInvokationEnvironment>())
                .ReturnsForAnyArgs(new GetCustomerDataRoamingLimitsByCustomerIDResponse() { ResultType = ResultTypes.Ok }
                );

            mockGetCustomerDataRoamingLimitNotificationByCustomerIdMS.Process(
                new GetCustomerDataRoamingLimitNotificationByCustomerIdRequest()
                {
                    CustomerId = customerInfo.CustomerID.Value
                },
                Arg.Any<RequestInvokationEnvironment>())
                .ReturnsForAnyArgs(new GetCustomerDataRoamingLimitNotificationByCustomerIdResponse() { ResultType = ResultTypes.Ok }
                );

            mockGetRoamingBlackListByCustomerIDMS.Process(new GetRoamingBlackListByCustomerIDRequest()
            {
                CustomerId = customerInfo.CustomerID.Value
            },
                Arg.Any<RequestInvokationEnvironment>())
                .ReturnsForAnyArgs(new GetRoamingBlackListByCustomerIDResponse() { ResultType = ResultTypes.Ok }
                );

            mockGetActiveCustomerAccountAssociationByDateMS.Process(
                new GetActiveCustomerAccountAssociationByDateRequest()
                {
                    CustomerInfo = customerInfo
                },
                Arg.Any<RequestInvokationEnvironment>())
                .ReturnsForAnyArgs(new GetActiveCustomerAccountAssociationByDateResponse()
                {
                    CustomerAccountAssociation = new CustomerAccountAssociation()
                    {
                        Customer = customerInfo,
                        Account = CreateDefaultObject.Create<Account>(),
                        StartDate = DateTime.Now
                    },
                    ResultType = ResultTypes.Ok
                });

            mockCalculateNextBillRunDateForBillCycleMS.Process(new CalculateNextBillRunDateForBillCycleRequest()
            {
                BillCycleDefinition = CreateDefaultObject.Create<BillCycle>(),
            },
                Arg.Any<RequestInvokationEnvironment>())
                .ReturnsForAnyArgs(new CalculateNextBillRunDateForBillCycleResponse() { ResultType = ResultTypes.Ok });

            mockGetRmPromotionGroupMemberMS.Process(new GetRmPromotionGroupMemberAllRequest(),
                Arg.Any<RequestInvokationEnvironment>())
                .ReturnsForAnyArgs(new GetRmPromotionGroupMemberAllResponse()
                {
                    RmPromotionGroupMembers = new List<RmPromotionGroupMember>()
                    {
                        new RmPromotionGroupMember()
                        {
                            PromotionGroup = new RmPromotionGroupInfo()
                            {
                                PromotionGroupID = 1008000001
                            },
                            PromotionPlan = promotionPlan
                        }
                    },
                    ResultType = ResultTypes.Ok
                });

            var updateCustomersPromotionRequest = CreateDefaultObject.Create<UpdateCustomersPromotionRequest>();
            mockUpdateCustomersPromotionMS.Process(updateCustomersPromotionRequest,
                Arg.Any<RequestInvokationEnvironment>()).
                ReturnsForAnyArgs(new UpdateCustomersPromotionResponse()
                {
                    ResultType = ResultTypes.Ok
                });

            mockGetCustomerPromotionOperationLogByCustomerIDAndPromotionMS.Process(
                new GetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest(),
                Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(new GetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse()
                {
                    ResultType = ResultTypes.Ok
                });
            mockCreateCustomerPromotionLogInfoMS.Process(new CreateCustomerPromotionLogInfoRequest(),
                Arg.Any<RequestInvokationEnvironment>()).
                ReturnsForAnyArgs(new CreateCustomerPromotionLogInfoResponse()
                {
                    ResultType = ResultTypes.Ok
                });
            mockInitSimcardMS.Process(new InitSimCardRequest(), Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(new InitSimCardResponse()
            {
                ResultType = ResultTypes.Ok
            });
            mockCancleCustomerBizop.Process(new CancelCustomerAndSubscriptionsRequestInternal()
            {
                CustomerInfo = customerInfo,
                CustomerAccountAssociation = null
            }, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(new CancelCustomerAndSubscriptionsResponseInternal()
            {
                ResultType = ResultTypes.Ok
            });

            var createdNewCustomer = CreateDefaultObject.Create<CustomerInfo>();
            createdNewCustomer.ResourceMBInfo = new List<ResourceMBInfo>()
            {
                new ResourceMBInfo()
                {
                    StartDate = DateTime.Now.AddDays(-1),
                    StatusID = 1
                }
            };
            createdNewCustomer.Promotions = new CrmCustomersPromotionInfo[]
            {
                new CrmCustomersPromotionInfo()
                {
                    CurrentLimit = newCustomerAccumulatedProtionCreditlimit,
                    StartDate = DateTime.Now.AddDays(-1),
                    PromotionDetail = new RmPromotionPlanDetailInfo()
                    {
                        RmPromotionPlanInfo = promotionPlan
                    }
                },
            };
            mockRegisterCustomerBizop.Process(new RegisterCustomerRequestInternal(), Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(new RegisterCustomerResponseInternal()
            {
                Customer = createdNewCustomer,
                ResultType = ResultTypes.Ok
            });

            
            mockGetProductOfferingByProductOfferingId.Process(Arg.Any<GetProductOfferingByProductOfferingIdRequest>()
                , Arg.Any<RequestInvokationEnvironment>())
                .Returns(new GetProductOfferingByProductOfferingIdResponse()
                {
                    ResultType = ResultTypes.Ok,
                    ProductOffering = actualProductOffering
                });


        }

    }
}
