using System;
using System.Collections.Generic;
using com.etak.core.bizops.opssupport.QueryProductCatalogById;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using com.etak.core.operation;
using com.etak.core.operation.dtoConverters;
using com.etak.core.product.message.GetProductByProductId;
using com.etak.core.product.message.GetProductOfferingByProductOfferingId;
using com.etak.core.repository.crm.subscription.catalog;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.opssupport.QueryProductCatalogById
{
    [TestFixture()]
    public class QueryProductCatalogByIdBizOpUnitTests :
        AbstractBusinessOperationTest
            <QueryProductCatalogByIdBizOp, QueryProductCatalogByIdRequestDTO, QueryProductCatalogByIdResponseDTO,
                QueryProductCatalogByIdRequestInternal, QueryProductCatalogByIdResponseInternal>
    {
        [TestFixtureSetUp]
        public static void Initialize()
        {
            FakeSessionFactorySingleton.Init();
        }

        [Test()]
        public void QueryProductCatalogByIdBizOp_ExistingProductId_ReturnCustomerProductCatalogDtoAndProductPurchaseChargingOptionDto
            ()
        {
            var getProductOfferingByIdMsMock =
                MockMicroServiceManager
                    .GetMockedMicroService<GetProductOfferingByProductOfferingIdRequest, GetProductOfferingByProductOfferingIdResponse>();

            var mockedProductOfferingRepo = MockRepositoryManager.GetMockedRepository<IProductOfferingRepository<ProductOffering>>();
            mockedProductOfferingRepo.GetByGroupId(Arg.Any<int>()).ReturnsForAnyArgs(new List<ProductOffering>());

            var getProductOfferingByProductOfferingIdRequest = Arg.Is<GetProductOfferingByProductOfferingIdRequest>(x => x.ProductOfferingId == 123);

            var date = new DateTime(2015, 4, 28);

            var productOffering = CreateDefaultObject.Create<ProductOffering>();
            productOffering.Options = new List<ProductOfferingOption>();

            var productChargeOption = new ProductChargeOption();
            productChargeOption.Id = 333;
            productChargeOption.Name = new MultiLingualDescription();
            productChargeOption.Name.Texts = new List<LanguageSpecificText>();
            productChargeOption.StartDate = date;
            productChargeOption.EndDate = date;

            productOffering.Names = CreateDefaultObject.Create<MultiLingualDescription>();
            productOffering.Names.Texts = new List<LanguageSpecificText>();
            productOffering.Description = CreateDefaultObject.Create<MultiLingualDescription>();
            productOffering.Description.Texts = new List<LanguageSpecificText>();

            var charge = CreateDefaultObject.Create<ChargeAggregate>();
            charge.Id = 222;
            charge.Category = 333;
            charge.CreateTime = date;
            charge.ProrateQty = 1;
            charge.ProrateUnit = new TimeUnits();
            charge.TypeOfTimeOfCharge = new TimesOfCharge();
            charge.ReferencedCharges = new List<ChargeTarget>();
            charge.ReferencingCharges = new List<ChargeTarget>();
            charge.ReferencingOptions = new List<ProductChargeOption>();

            var languageSpecificText = CreateDefaultObject.Create<LanguageSpecificText>();
            languageSpecificText.Language = CreateDefaultObject.Create<ISO639LanguageCodes>();
            languageSpecificText.Text = "2112";
            charge.Name = CreateDefaultObject.Create<MultiLingualDescription>();
            charge.Name.Texts = new List<LanguageSpecificText>() { languageSpecificText };
            charge.Description = CreateDefaultObject.Create<MultiLingualDescription>();
            charge.Description.Texts = new List<LanguageSpecificText>() { languageSpecificText };

            var chargePrice = new ChargePrice();
            chargePrice.Amount = 1;
            chargePrice.EndDate = date;
            chargePrice.Currency = new ISO4217CurrencyCodes();
            chargePrice.StartDate = date;

            charge.Prices = new List<ChargePrice>()
            {
                chargePrice
            };

            productChargeOption.Charges = new List<Charge>
            {
                charge
            };

            productChargeOption.ProductOffering = productOffering;

            productOffering.ChargingOptions = new List<ProductChargeOption>
            {
                productChargeOption
            };

            var getProductOfferingByProducOfferingtIdResponse = new GetProductOfferingByProductOfferingIdResponse()
            {
                ProductOffering = productOffering
            };

            getProductOfferingByProducOfferingtIdResponse.ProductOffering.Id = 123;

            getProductOfferingByIdMsMock.Process(getProductOfferingByProductOfferingIdRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(getProductOfferingByProducOfferingtIdResponse);

            var productPurchaseChargingOptionDto = new List<ProductPurchaseChargingOptionDTO> { productChargeOption.ToDto() };

            var expectedQueryProductCatalogByIdResponseDTO = new QueryProductCatalogByIdResponseDTO()
            {
                CustomerProductCatalogDto = productOffering.ToDto(),
                ProductPurchaseChargingOptionDto = productPurchaseChargingOptionDto
            };

            var queryProductCatalogByIdRequestDTO = new QueryProductCatalogByIdRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                ProductCatalogId = 123,
            };

            MockAbsctractBusinessOperation(queryProductCatalogByIdRequestDTO);

            var actualQueryProductCatalogByIdResponseDTO = CallBizOp(queryProductCatalogByIdRequestDTO);

            AssertExt.ObjectPropertiesAreEqual(expectedQueryProductCatalogByIdResponseDTO.CustomerProductCatalogDto, actualQueryProductCatalogByIdResponseDTO.CustomerProductCatalogDto);
            AssertExt.ObjectPropertiesAreEqual(expectedQueryProductCatalogByIdResponseDTO.ProductPurchaseChargingOptionDto, actualQueryProductCatalogByIdResponseDTO.ProductPurchaseChargingOptionDto);
        }

        [Test()]
        public void QueryProductCatalogByIdBizOp_MSThrowException_ShouldThrowException()
        {
            var getProductOfferingByProductOfferingIdRequest = Arg.Is<GetProductOfferingByProductOfferingIdRequest>(x => x.ProductOfferingId == 1);
            var getProductOfferingByIdMsMock = MockMicroServiceManager.GetMockedMicroService<GetProductOfferingByProductOfferingIdRequest, GetProductOfferingByProductOfferingIdResponse>();

            getProductOfferingByIdMsMock.Process(getProductOfferingByProductOfferingIdRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(x => { throw new Exception("Error"); });

            var queryProductCatalogByIdRequestDTO = new QueryProductCatalogByIdRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                ProductCatalogId = 1
            };

            MockAbsctractBusinessOperation(queryProductCatalogByIdRequestDTO);

            var actualQueryProductCatalogByIdResponseDTO = CallBizOp(queryProductCatalogByIdRequestDTO);
            Assert.AreEqual(ResultTypes.UnknownError, actualQueryProductCatalogByIdResponseDTO.resultType);
        }

        [Test()]
        public void QueryProductCatalogByIdBizOp_NullProduct_ShouldThrowException()
        {
            var getProductOfferingByProductOfferingIdRequest = Arg.Is<GetProductOfferingByProductOfferingIdRequest>(x => x.ProductOfferingId == 1);
            var getProductOfferingByIdMsMock = MockMicroServiceManager.GetMockedMicroService<GetProductOfferingByProductOfferingIdRequest, GetProductOfferingByProductOfferingIdResponse>();

            getProductOfferingByIdMsMock.Process(getProductOfferingByProductOfferingIdRequest, Arg.Any<RequestInvokationEnvironment>()).Returns((GetProductOfferingByProductOfferingIdResponse)null);

            var queryProductCatalogByIdRequestDTO = new QueryProductCatalogByIdRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                ProductCatalogId = 1
            };

            MockAbsctractBusinessOperation(queryProductCatalogByIdRequestDTO);

            var actualQueryProductCatalogByIdResponseDTO = CallBizOp(queryProductCatalogByIdRequestDTO);

            Assert.AreEqual(ResultTypes.UnknownError, actualQueryProductCatalogByIdResponseDTO.resultType);
        }
    }
}