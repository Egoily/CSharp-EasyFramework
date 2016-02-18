using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.helper;
using com.etak.core.dealer.messages.GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItem;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.operation.manager;
using com.etak.core.product.message.GetProductByProductId;
using com.etak.core.product.message.GetProductOfferingByProductOfferingId;
using com.etak.core.test.utilities;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.helper
{
    [TestFixture()]
    public class PurchaseHelperTests
    {
        private DateTime startDate = new DateTime(2014, 5, 6);
        private DateTime startDateClosest = new DateTime(2014, 5, 5);

        private DateTime endDate = new DateTime(2016, 5, 6);
        private ProductOffering actualProductOffering = CreateDefaultObject.Create<ProductOffering>();
        private ProductOffering actualProductChildOffering = CreateDefaultObject.Create<ProductOffering>();
        private ProductChargeOption actualProductChargeOption = CreateDefaultObject.Create<ProductChargeOption>();



        private IMicroService<GetProductOfferingByProductOfferingIdRequest, GetProductOfferingByProductOfferingIdResponse> mockGetProductOfferingByProductOfferingId;





        #region 4G

        [Test()]
        public void GetCustomer4GProductsTest_OneProductFound()
        {

            #region mock Ms


            var mockgetMvnoConfigActionInfosByMvnoIdAndCategoryIdAndItemMs =
                MockMicroServiceManager.GetMockedMicroService<GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest,
                    GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResponse>();


            #endregion


            #region Setup MS

            mockgetMvnoConfigActionInfosByMvnoIdAndCategoryIdAndItemMs.Process(
                Arg.Any<GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest>()
                , Arg.Any<RequestInvokationEnvironment>())
                .Returns
                    (
                        new GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResponse()
                        {
                            MvnoConfigActionInfos = new List<MVNOConfigActionInfo>()
                            {
                                new MVNOConfigActionInfo() { Value = "33,35"}
                            }
                        }
                    );


            #endregion


            #region Assign Values
            var actuallistProducts = new List<Product>()
            {
                CreateDefaultObject.Create<Product>(),
                CreateDefaultObject.Create<Product>(),
                CreateDefaultObject.Create<Product>()
            };
            actuallistProducts[0].Id = 33;

            #endregion

            PurchaseHelper pHelper = new PurchaseHelper();

            List<Product> response = pHelper.GetCustomer4GProducts(actuallistProducts, 1).ToList();

            Assert.AreEqual(response.Count, 1);
            Assert.AreEqual(response[0].Id, 33);

        }

        [Test()]
        public void GetCustomer4GProductsTest_NoProductFound()
        {

            #region mock Ms


            var mockgetMvnoConfigActionInfosByMvnoIdAndCategoryIdAndItemMs =
                MockMicroServiceManager.GetMockedMicroService<GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest,
                    GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResponse>();


            #endregion


            #region Setup MS

            mockgetMvnoConfigActionInfosByMvnoIdAndCategoryIdAndItemMs.Process(
                Arg.Any<GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest>()
                , Arg.Any<RequestInvokationEnvironment>())
                .Returns
                    (
                        new GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResponse()
                        {
                            MvnoConfigActionInfos = new List<MVNOConfigActionInfo>()
                            {
                                new MVNOConfigActionInfo() { Value = "33,35"}
                            }
                        }
                    );


            #endregion


            #region Assign Values
            var actuallistProducts = new List<Product>()
            {
                CreateDefaultObject.Create<Product>(),
                CreateDefaultObject.Create<Product>(),
                CreateDefaultObject.Create<Product>()
            };
            actuallistProducts[0].Id = 330;

            #endregion

            PurchaseHelper pHelper = new PurchaseHelper();

            List<Product> response = pHelper.GetCustomer4GProducts(actuallistProducts, 1).ToList();

            Assert.AreEqual(response.Count, 0);


        }

        #region IsNeedtoDisable4GService UnitTests
        [Test()]
        public void IsNeedtoDisable4GService_No4GProductConfig_ShouldReturnFalse()
        {
            #region mock Ms
            var mockgetMvnoConfigActionInfosByMvnoIdAndCategoryIdAndItemMs =
                MockMicroServiceManager.GetMockedMicroService<GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest,
                    GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResponse>();
            #endregion

            #region Setup MS

            mockgetMvnoConfigActionInfosByMvnoIdAndCategoryIdAndItemMs.Process(
                Arg.Any<GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest>()
                , Arg.Any<RequestInvokationEnvironment>())
                .Returns
                    (
                        new GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResponse()
                        {
                            MvnoConfigActionInfos = new List<MVNOConfigActionInfo>()
                            {
                                new MVNOConfigActionInfo() {}
                            }
                        }
                    );


            #endregion

            #region Assign Values
            PurchaseHelper pHelper = new PurchaseHelper();
            var customerProductAssignment = new CustomerProductAssignment
            {
                PurchasedProduct = CreateDefaultObject.Create<Product>()
            };
            var customerProductAssignmentToCancel = new CustomerProductAssignment
            {
                PurchasedProduct = CreateDefaultObject.Create<Product>()
            };
            #endregion
            //False:mean not need to disable 4g
            Assert.IsFalse(pHelper.IsNeedtoDisable4GService(new List<CustomerProductAssignment>() { customerProductAssignment }, customerProductAssignmentToCancel, 1));
        }
        [Test()]
        public void IsNeedtoDisable4GService_GivenRecurringProduct_ShouldReturnFalse()
        {
            #region mock Ms
            var mockgetMvnoConfigActionInfosByMvnoIdAndCategoryIdAndItemMs =
                MockMicroServiceManager.GetMockedMicroService<GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest,
                    GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResponse>();
            #endregion
            #region Setup MS
            mockgetMvnoConfigActionInfosByMvnoIdAndCategoryIdAndItemMs.Process(
                Arg.Any<GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest>()
                , Arg.Any<RequestInvokationEnvironment>())
                .Returns
                    (
                        new GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResponse()
                        {
                            MvnoConfigActionInfos = new List<MVNOConfigActionInfo>()
                            {
                                new MVNOConfigActionInfo() { Value = "1,35"}
                            }
                        }
                    );
            #endregion
            #region Assign Values
            PurchaseHelper pHelper = new PurchaseHelper();

            var customerProductAssignment = new CustomerProductAssignment
            {
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(1),
                PurchasedProduct = CreateDefaultObject.Create<Product>()
            };

            ProductChargeOption productChargePurchased = CreateDefaultObject.Create<ProductChargeOption>();
            productChargePurchased.Charges = new List<Charge>()
            {
                new ChargeRecurring()
            };

            var customerProductAssignmentToCancel = new CustomerProductAssignment
            {
                PurchasedProduct = CreateDefaultObject.Create<Product>(),
                ProductChargePurchased = productChargePurchased

            };
            #endregion
            //False:mean not need to disable 4g
            Assert.IsFalse(pHelper.IsNeedtoDisable4GService(new List<CustomerProductAssignment>() { customerProductAssignment }, customerProductAssignmentToCancel, 1));


        }
        [Test()]
        public void IsNeedtoDisable4GService_GivenNonRecurringEffectProduct_ShouldReturnFalse()
        {
            #region mock Ms
            var mockgetMvnoConfigActionInfosByMvnoIdAndCategoryIdAndItemMs =
                MockMicroServiceManager.GetMockedMicroService<GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest,
                    GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResponse>();
            #endregion
            #region Setup MS
            mockgetMvnoConfigActionInfosByMvnoIdAndCategoryIdAndItemMs.Process(
                Arg.Any<GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest>()
                , Arg.Any<RequestInvokationEnvironment>())
                .Returns
                    (
                        new GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResponse()
                        {
                            MvnoConfigActionInfos = new List<MVNOConfigActionInfo>()
                            {
                                new MVNOConfigActionInfo() { Value = "1,35"}
                            }
                        }
                    );
            #endregion
            #region Assign Values
            PurchaseHelper pHelper = new PurchaseHelper();

            var customerProductAssignment = new CustomerProductAssignment
            {
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(1),
                PurchasedProduct = CreateDefaultObject.Create<Product>()
            };

            ProductChargeOption productChargePurchased = CreateDefaultObject.Create<ProductChargeOption>();
            productChargePurchased.Charges = new List<Charge>()
            {
                new ChargeNonRecurring()
            };

            var customerProductAssignmentToCancel = new CustomerProductAssignment
            {
                PurchasedProduct = CreateDefaultObject.Create<Product>(),
                ProductChargePurchased = productChargePurchased

            };
            #endregion
            //False:mean not need to disable 4g
            Assert.IsFalse(pHelper.IsNeedtoDisable4GService(new List<CustomerProductAssignment>() { customerProductAssignment }, customerProductAssignmentToCancel, 1));
        }
        [Test()]
        public void IsNeedtoDisable4GService_GivenNonRecurringExpiredProductAndMulti4GProuct_ShouldReturnFalse()
        {
            #region mock Ms
            var mockgetMvnoConfigActionInfosByMvnoIdAndCategoryIdAndItemMs =
                MockMicroServiceManager.GetMockedMicroService<GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest,
                    GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResponse>();
            #endregion
            #region Setup MS
            mockgetMvnoConfigActionInfosByMvnoIdAndCategoryIdAndItemMs.Process(
                Arg.Any<GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest>()
                , Arg.Any<RequestInvokationEnvironment>())
                .Returns
                    (
                        new GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResponse()
                        {
                            MvnoConfigActionInfos = new List<MVNOConfigActionInfo>()
                            {
                                new MVNOConfigActionInfo() { Value = "1,35"}
                            }
                        }
                    );
            #endregion
            #region Assign Values
            PurchaseHelper pHelper = new PurchaseHelper();

            var customerProductAssignment1 = new CustomerProductAssignment
            {
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now,
                PurchasedProduct = CreateDefaultObject.Create<Product>()
            };
            var customerProductAssignment2 = new CustomerProductAssignment
            {
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(1),
                PurchasedProduct = CreateDefaultObject.Create<Product>()
            };

            ProductChargeOption productChargePurchased = CreateDefaultObject.Create<ProductChargeOption>();
            productChargePurchased.Charges = new List<Charge>()
            {
                new ChargeNonRecurring()
            };

            var customerProductAssignmentToCancel = new CustomerProductAssignment
            {
                PurchasedProduct = CreateDefaultObject.Create<Product>(),
                ProductChargePurchased = productChargePurchased

            };
            #endregion
            //False:mean not need to disable 4g
            Assert.IsFalse(pHelper.IsNeedtoDisable4GService(new List<CustomerProductAssignment>() { customerProductAssignment1, customerProductAssignment2 }, customerProductAssignmentToCancel, 1));
        }
        [Test()]
        public void IsNeedtoDisable4GService_GivenNonRecurringExpiredProduct_ShouldReturnTrue()
        {
            #region mock Ms
            var mockgetMvnoConfigActionInfosByMvnoIdAndCategoryIdAndItemMs =
                MockMicroServiceManager.GetMockedMicroService<GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest,
                    GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResponse>();
            #endregion
            #region Setup MS
            mockgetMvnoConfigActionInfosByMvnoIdAndCategoryIdAndItemMs.Process(
                Arg.Any<GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest>()
                , Arg.Any<RequestInvokationEnvironment>())
                .Returns
                    (
                        new GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResponse()
                        {
                            MvnoConfigActionInfos = new List<MVNOConfigActionInfo>()
                            {
                                new MVNOConfigActionInfo() { Value = "1,35"}
                            }
                        }
                    );
            #endregion
            #region Assign Values
            PurchaseHelper pHelper = new PurchaseHelper();

            var customerProductAssignment = new CustomerProductAssignment
            {
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now,
                PurchasedProduct = CreateDefaultObject.Create<Product>()
            };

            ProductChargeOption productChargePurchased = CreateDefaultObject.Create<ProductChargeOption>();
            productChargePurchased.Charges = new List<Charge>()
            {
                new ChargeNonRecurring()
            };
            var customerProductAssignmentToCancel = new CustomerProductAssignment
            {
                PurchasedProduct = CreateDefaultObject.Create<Product>(),
                ProductChargePurchased = productChargePurchased

            };
            #endregion
            //True:mean need to disable 4g
            Assert.IsTrue(pHelper.IsNeedtoDisable4GService(new List<CustomerProductAssignment>() { customerProductAssignment }, customerProductAssignmentToCancel, 1));
        }
        #endregion

        #endregion


        [Test]
        public void AddProductAndChargeTest()
        {



            ProductOffering productOffering = new ProductOffering()
            {
                Id = 1003000007,
                ChargingOptions = new ProductChargeOption[]
                {
                    new ProductChargeOption()
                    {
                        Id = 1006000007,
                    }
                },
                OfferedProduct = new Product(),
                Options =  new List<ProductOfferingOption>()
            };

            ProductOffering productOfferingChild = new ProductOffering()
            {
                Id = 1003000070,
                ChargingOptions = new ProductChargeOption[]
                {
                    new ProductChargeOption()
                    {
                        Id = 1006000070,
                    }
                },
                OfferedProduct = new Product(),
            };

            ProductOfferingSpecificationOption option = CreateDefaultObject.Create<ProductOfferingSpecificationOption>(1);
            option.RelatedProductOffering = productOffering;
            option.MaxOccurs = 1;
            option.MinOccurs = 1;
            option.SpecifiedProductOffering = productOfferingChild;

            productOffering.Options.Add(option);

            List<ProductCatalogDTO> listProd = new List<ProductCatalogDTO>()
            {
                new ProductCatalogDTO()
                {
                    Id = 1003000007,
                    PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>() { new ProductPurchaseChargingOptionDTO() { Id = 1006000007 } }
                }
            };

            actualProductOffering.ChargingOptions = new List<ProductChargeOption>() { new ProductChargeOption() };
            actualProductOffering.Options = new List<ProductOfferingOption>() { new ProductOfferingSpecificationOption() };
            actualProductChildOffering.ChargingOptions = new List<ProductChargeOption>() { new ProductChargeOption() };
            actualProductChildOffering.Options = new List<ProductOfferingOption>() { new ProductOfferingGroupOption() };

            actualProductOffering.ChargingOptions[0].Id = 1006000007;
            actualProductOffering.Options[0].MinOccurs = 1;
            actualProductChildOffering.ChargingOptions[0].Id = 1006000007; ;
            actualProductOffering.Options.First().RelatedProductOffering = actualProductOffering;
            (actualProductOffering.Options.First() as ProductOfferingSpecificationOption).SpecifiedProductOffering = actualProductChildOffering;
            actualProductChildOffering.Id = 1006000007;


            mockGetProductOfferingByProductOfferingId =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <GetProductOfferingByProductOfferingIdRequest, GetProductOfferingByProductOfferingIdResponse>();

            mockGetProductOfferingByProductOfferingId.Process(Arg.Any<GetProductOfferingByProductOfferingIdRequest>()
                , Arg.Any<RequestInvokationEnvironment>())
                .Returns(new GetProductOfferingByProductOfferingIdResponse()
                {
                    ResultType = ResultTypes.Ok,
                    ProductOffering = actualProductOffering
                });


            

            var mockedGetProduct = MockMicroServiceManager.GetMockedMicroService<GetProductOfferingByProductOfferingIdRequest, GetProductOfferingByProductOfferingIdResponse>();
            var getProductReq = Arg.Is<GetProductOfferingByProductOfferingIdRequest>(x => x.ProductOfferingId == 1003000007);
            var getProductResp = new GetProductOfferingByProductOfferingIdResponse()
            {
                ResultType = ResultTypes.Ok,
                ProductOffering = productOffering,
                
            };
            mockedGetProduct.Process(getProductReq, Arg.Any<RequestInvokationEnvironment>()).Returns(getProductResp);



            PurchaseHelper purchHelper = new PurchaseHelper();

            var dict = purchHelper.GetProductsAndChargesOptions(listProd);



            Assert.IsTrue(dict.Count == 2);

        }
    }
}
