using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.fullfilment.AssignProductToCustomer;
using com.etak.core.customer.message.GetCustomerInfoById;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.GSMSubscription.messages.CreateCustomerProductAssignment;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;
using com.etak.core.model.subscription.catalog;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.product.message.GetProductByProductId;
using com.etak.core.product.message.GetProductChargeOptionByProductChargeOptionId;
using com.etak.core.product.message.GetProductOfferingByProductOfferingId;
using com.etak.core.repository;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.subscription.catalog;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.fullfilment.AssignProductToCustomer
{
    [TestFixture()]
    public class AssignProductToCustomerBizOpTests : AbstractSinglePhaseOrderProcessorTest<AssignProductOfferingToCustomerBizOp,
        AssignProductOfferingToCustomerRequestDTO,AssignProductOfferingToCustomerResponseDTO,AssignProductOfferingToCustomerRequestInternal,AssignProductOfferingToCustomerResponseInternal,
        AssignProductOfferingToCustomerOrder>
    {

        #region Ini Var

        private int DealerId = 1;
        private DealerInfo actualDealerInfo;
        private ProductOffering actualProductOffering;

        #endregion

        #region Define Mock MicroServices



        private IMicroService<CreateCustomerProductAssignmentRequest, CreateCustomerProductAssignmentResponse> _mockcreateassignProductForCustomerMs;
        private IMicroService<GetProductByProductIdRequest, GetProductByProductIdResponse> _mockgetProductByProductIdMs;
        private IMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse> _mockCheckAuthorizationMs;
        private IMicroService<GetCustomerInfoByIdRequest, GetCustomerInfoByIdResponse> _mockgetCustomerInfoMs;
        private IMicroService<GetProductChargeOptionByProductChargeOptionIdRequest, GetProductChargeOptionByProductChargeOptionIdResponse> _mockgetChargeOptionByChargeOptionId;
        private IMicroService<GetProductOfferingByProductOfferingIdRequest, GetProductOfferingByProductOfferingIdResponse> _mockgetProductOfferingByProductOfferingIdMs;


        #endregion

        #region Define Mock Repo

        private IProductOfferingRepository<ProductOffering> _mockProductOfferingRepo;

        #endregion



        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();

            actualDealerInfo = CreateDefaultObject.Create<DealerInfo>();
            actualDealerInfo.DealerID = DealerId;

            actualProductOffering = CreateDefaultObject.Create<ProductOffering>();
        }


        private void CommomInis()
        {

            #region Create Mock MS


            _mockcreateassignProductForCustomerMs = MockMicroService<CreateCustomerProductAssignmentRequest, CreateCustomerProductAssignmentResponse>();
            _mockCheckAuthorizationMs = MockMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
            _mockgetProductByProductIdMs = MockMicroService<GetProductByProductIdRequest, GetProductByProductIdResponse>();
            _mockgetCustomerInfoMs = MockMicroService<GetCustomerInfoByIdRequest, GetCustomerInfoByIdResponse>();
            _mockgetChargeOptionByChargeOptionId = MockMicroService<GetProductChargeOptionByProductChargeOptionIdRequest, GetProductChargeOptionByProductChargeOptionIdResponse>();
            _mockgetProductOfferingByProductOfferingIdMs =MockMicroService<GetProductOfferingByProductOfferingIdRequest, GetProductOfferingByProductOfferingIdResponse>();



            #endregion

            #region Create Mock BizOp


            #endregion

            #region Bind to BizOpKernel




            #endregion

            #region Create Mock Third party



            #endregion

            #region create Mock OperationConfiguration




            #endregion

            #region Create Mock Repository

            _mockProductOfferingRepo = MockRepositoryManager.GetMockedRepository<IProductOfferingRepository<ProductOffering>>();


            #endregion

        }


        [Test()]
        public void AssignProductToCusotmer_OK()
        {
            #region RequestDTO

            int IdCmd = 777;
            var product = CreateDefaultObject.Create<ProductCatalogDTO>();
            product.Id = IdCmd;

            AssignProductOfferingToCustomerRequestDTO actualAssignProductToCustomerRequestDTO = new AssignProductOfferingToCustomerRequestDTO
            {
                CreateDate = DateTime.Now,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                ProductOfferingId = IdCmd,
                user = "1644000204",
                password = "123456",
                vmno = "970100"
            };

            #endregion

            #region Commons

            MockAbstractSinglePhaseOrderProcessor(actualAssignProductToCustomerRequestDTO);


            CommomInis();

            #endregion

            #region Assign values


            var actualProduct = CreateDefaultObject.Create<Product>();
            actualProduct.Id = IdCmd;

            // TODO: Bug in conversor, crashes if no Names.Texts or Descriptions.Texts
            actualProduct.Names.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = ""}}};
            actualProduct.Description.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            //actualProduct.ChargingOptions = new List<ProductChargeOption>();

            var mockCustomerRepo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            var mockedActualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            mockedActualCustomerInfo.PropertyInfo = new List<PropertyInfo>()
            {
                CreateDefaultObject.Create<PropertyInfo>()
            };
            mockedActualCustomerInfo.PropertyInfo.First().PendingStatus = (int)PendingStatus.Active;
            mockedActualCustomerInfo.CustomerID = actualAssignProductToCustomerRequestDTO.CustomerId;
            mockedActualCustomerInfo.ResourceMBInfo.Clear();
            mockedActualCustomerInfo.ResourceMBInfo.Add(CreateDefaultObject.Create<ResourceMBInfo>());
            mockedActualCustomerInfo.ResourceMBInfo.First().CustomerInfo.CustomerID = actualAssignProductToCustomerRequestDTO.CustomerId;
            mockCustomerRepo.GetById(mockedActualCustomerInfo.CustomerID.Value).Returns(mockedActualCustomerInfo);


            var actualProductChargeOption = CreateDefaultObject.Create<ProductChargeOption>();
            actualProductOffering = CreateDefaultObject.Create<ProductOffering>();
            actualProductOffering.OfferedProduct = actualProduct;
            actualProductOffering.ChargingOptions = new List<ProductChargeOption>();
            actualProductOffering.Options = new List<ProductOfferingOption>();


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


            // Return the same parameter
            _mockcreateassignProductForCustomerMs.Process(Arg.Any<CreateCustomerProductAssignmentRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    x => new CreateCustomerProductAssignmentResponse()
                    {
                        CustomerProductAssignment = ((CreateCustomerProductAssignmentRequest)x[0]).CustomerProductAssignment
                    });

            _mockgetProductByProductIdMs.Process(Arg.Any<GetProductByProductIdRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetProductByProductIdResponse()
                    {
                        ResultType = ResultTypes.Ok,
                        Product = actualProduct
                    }
                );
            _mockgetCustomerInfoMs.Process(Arg.Any<GetCustomerInfoByIdRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetCustomerInfoByIdResponse()
                    {
                        ResultType = ResultTypes.Ok,
                        CustomerInfo = mockedActualCustomerInfo
                    }
                );

            _mockgetChargeOptionByChargeOptionId.Process(Arg.Any<GetProductChargeOptionByProductChargeOptionIdRequest>()
                , Arg.Any<RequestInvokationEnvironment>())
                .Returns(new GetProductChargeOptionByProductChargeOptionIdResponse()
                {
                    ResultType = ResultTypes.Ok,
                    ProductChargeOption = actualProductChargeOption
                });



            _mockgetProductOfferingByProductOfferingIdMs.Process(Arg.Any<GetProductOfferingByProductOfferingIdRequest>()
                , Arg.Any<RequestInvokationEnvironment>())
                .Returns(new GetProductOfferingByProductOfferingIdResponse()
                {
                    ResultType = ResultTypes.Ok,
                    ProductOffering = actualProductOffering
                });
            
                


            #endregion

            #region Setup mocks Repository

            _mockProductOfferingRepo.GetByGroupId(Arg.Any<int>())
                .Returns(new List<ProductOffering>() { actualProductOffering });

            #endregion

            #region CallBizOP

            var actualAssignProductToCustomerResponseDTO = CallBizOp(actualAssignProductToCustomerRequestDTO);
            var expectedAssignProductToCustomerResponseDTO = new AssignProductOfferingToCustomerResponseDTO
            {
                Subscription = new SubscriptionDTO { CustomerId = actualAssignProductToCustomerRequestDTO.CustomerId}
            };


            #endregion

            #region Asserts

            Assert.AreEqual(actualAssignProductToCustomerResponseDTO.resultType, ResultTypes.Ok);
            Assert.AreEqual(actualAssignProductToCustomerResponseDTO.productPurchased.PurchasedProductId, actualAssignProductToCustomerRequestDTO.ProductOfferingId);
            Assert.IsTrue(actualAssignProductToCustomerResponseDTO.Subscription.CustomerId == expectedAssignProductToCustomerResponseDTO.Subscription.CustomerId);
            #endregion
        }



        [Test()]
        public void AssignProductToCusotmer_CustomerProductAssnDifferent_NOK()
        {
            #region RequestDTO

            int IdCmd = 777;
            var product = CreateDefaultObject.Create<ProductCatalogDTO>();
            product.Id = IdCmd;


            AssignProductOfferingToCustomerRequestDTO requestDto = new AssignProductOfferingToCustomerRequestDTO
            {
                CreateDate = DateTime.Now,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                ProductOfferingId = IdCmd,
                user = "1644000204",
                password = "123456",
                vmno = "970100"
            };

            #endregion

            #region Commons

            MockAbstractSinglePhaseOrderProcessor(requestDto);


            CommomInis();

            #endregion

            #region Find out BizOP OperationID

            //var bizopID = new PurchaseProductForCustomerBizOp();
            //var a = bizopID.Id;

            //int operationId = bizopID.OperationCode.GetHashCode();

            #endregion

            #region Assign values

            var actualProduct = CreateDefaultObject.Create<Product>();

            // TODO: Bug in conversor, crashes if no Names.Texts or Descriptions.Texts
            actualProduct.Names.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            actualProduct.Description.Texts = new List<LanguageSpecificText>() { new LanguageSpecificText() { Text = "", Description = new MultiLingualDescription() { DefaultMessage = "" } } };
            


            var actualCusotmerProductAssignment = CreateDefaultObject.Create<CustomerProductAssignment>();
            actualCusotmerProductAssignment.PurchasedProduct.Id = 999;

            var actualProductChargeOption = CreateDefaultObject.Create<ProductChargeOption>();
            actualProductOffering = CreateDefaultObject.Create<ProductOffering>();
            actualProductOffering.OfferedProduct = actualProduct;
            actualProductOffering.ChargingOptions = new List<ProductChargeOption>();
            actualProductOffering.Options = new List<ProductOfferingOption>();

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


            // Return the same parameter
            _mockcreateassignProductForCustomerMs.Process(Arg.Any<CreateCustomerProductAssignmentRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    x => new CreateCustomerProductAssignmentResponse()
                    {
                        // cheat with wrong value, Assert will fail
                        CustomerProductAssignment = actualCusotmerProductAssignment
                    });


            _mockgetProductByProductIdMs.Process(Arg.Any<GetProductByProductIdRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetProductByProductIdResponse()
                    {
                        ResultType = ResultTypes.Ok,
                        Product = actualProduct
                    }
                );


            _mockgetChargeOptionByChargeOptionId.Process(Arg.Any<GetProductChargeOptionByProductChargeOptionIdRequest>()
                , Arg.Any<RequestInvokationEnvironment>())
                .Returns(new GetProductChargeOptionByProductChargeOptionIdResponse()
                {
                    ResultType = ResultTypes.Ok,
                    ProductChargeOption = actualProductChargeOption
                });



            _mockgetProductOfferingByProductOfferingIdMs.Process(Arg.Any<GetProductOfferingByProductOfferingIdRequest>()
                , Arg.Any<RequestInvokationEnvironment>())
                .Returns(new GetProductOfferingByProductOfferingIdResponse()
                {
                    ResultType = ResultTypes.Ok,
                    ProductOffering = actualProductOffering
                });
            


            #endregion

            #region Setup mocks Repository

            _mockProductOfferingRepo.GetByGroupId(Arg.Any<int>())
                .Returns(new List<ProductOffering>() { actualProductOffering });

            #endregion

            #region Setup Mock BizOp


            #endregion

            #region Setup Mock Third party



            #endregion

            #region Customized CallBizOP


            var response = new AssignProductOfferingToCustomerResponseDTO();
            response = CallBizOp(requestDto);


            
            //using (var conn = RepositoryManager.GetNewConnection())
            //{
            //    AssignProductOfferingToCustomerBizOp bizop = new AssignProductOfferingToCustomerBizOp();




            //    //PurchaseProductForCustomerResponseInternal responseInternal =bizop.ProcessRequest(null, new PurchaseProductForCustomerRequestInternal()
            //    //{
            //    //    AccountDefinition = actualAccount,
            //    //    Customer = actualCusotmerInfo,
            //    //    DatetimePurchase = DateTime.Now,
            //    //    Invoice = actualInvoice,
            //    //    ForceCreditLimit = null,
            //    //    ProductsList = productListOk,
            //    //    TypeOfPurchaseProductOperation = PurchaseProductForCustomerBizOp.TypeOfPurchaseProduct.PurchaseProduct,
            //    //    MVNO = actualDealerInfo,
            //    //    User = CreateDefaultObject.Create<LoginInfo>()
            //    //});



            //    response = bizop.ProcessFromCustomerModel(new NullValidator<AssignProductOfferingToCustomerRequestDTO>(), new SameTypeConverter<AssignProductOfferingToCustomerRequestDTO>(),
            //        new SameTypeConverter<AssignProductOfferingToCustomerResponseDTO>(), requestDto, FakeInvoker);

            //}
            //RepositoryManager.CloseConnection();


            // PurchaseProductForCustomerResponseDTO response = new PurchaseProductForCustomerResponseDTO();
            


            #endregion

            #region Asserts

            Assert.AreEqual(response.resultType, ResultTypes.Ok);
            Assert.AreNotEqual(response.productPurchased.PurchasedProductId, requestDto.ProductOfferingId);

            #endregion
        }

    }
}
