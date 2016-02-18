using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using com.etak.core.operation.dtoConverters;
using com.etak.core.repository.crm.subscription.catalog;
using com.etak.core.test.utilities;
using NUnit.Framework;

namespace com.etak.core.operation.UnitTests.dtoConverter
{
 
    [TestFixture]
    public class ProductCatalogDtoMappingTest
    {

        [TestFixtureSetUp]
        public void Initialise()
        {
            var productOfferingRepo = MockRepositoryManager.GetMockedRepository<IProductOfferingRepository<ProductOffering>>();
        }


        [Test()]
        public static void ToDto_CorrectProduct_ShouldReturnProductCatalogDTO()
        {
            #region Set all the properties of the product
            var proInfo = CreateDefaultObject.Create<ProductOffering>();


            proInfo.Names = new MultiLingualDescription()
            {
                Texts = new List<LanguageSpecificText>()
                {
                    CreateDefaultObject.Create<LanguageSpecificText>()
                }
            };
            proInfo.Description = new MultiLingualDescription()
            {
                Texts = new List<LanguageSpecificText>()
                {
                    CreateDefaultObject.Create<LanguageSpecificText>()
                }
            };

            var chargingOption = new ProductChargeOption()
            {
                CreateDate = CreateDefaultObject.Create<DateTime>(),
                EndDate = CreateDefaultObject.Create<DateTime>(),
                Id = CreateDefaultObject.Create<int>(),
                IsDefaultOption = DefaultOptions.Y,
                ProductOffering = CreateDefaultObject.Create<ProductOffering>(),
                StartDate = CreateDefaultObject.Create<DateTime>(),
                Name = new MultiLingualDescription(),
            };
            chargingOption.Name = new MultiLingualDescription()
            {
                Texts = new List<LanguageSpecificText>()
                {
                    CreateDefaultObject.Create<LanguageSpecificText>()
                }
            };
            chargingOption.Description = new MultiLingualDescription()
            {
                Texts = new List<LanguageSpecificText>()
                {
                    CreateDefaultObject.Create<LanguageSpecificText>()
                }
            };
            var chargeInfo = CreateDefaultObject.Create<ChargeRecurring>();
            chargeInfo.Name = new MultiLingualDescription()
            {
                Texts = new List<LanguageSpecificText>()
                {
                    new LanguageSpecificText()
                    {
                        Text = "text1",
                        Language = ISO639LanguageCodes.eng,
                    }
                }
            };
            chargeInfo.Description = new MultiLingualDescription()
            {
                Texts = new List<LanguageSpecificText>()
                {
                    new LanguageSpecificText()
                    {
                        Text = "text1",
                        Language = ISO639LanguageCodes.eng,
                    }   
                }
            };
            chargingOption.Charges = new List<Charge>() { chargeInfo };
            proInfo.ChargingOptions = new List<ProductChargeOption>() { chargingOption }; 
            proInfo.Options = new List<ProductOfferingOption>();

            #region Product
            var productOffered = CreateDefaultObject.Create<Product>();
            productOffered.Names = new MultiLingualDescription()
            {
                Texts = new List<LanguageSpecificText>()
                {
                    CreateDefaultObject.Create<LanguageSpecificText>()
                }
            };
            productOffered.Description = new MultiLingualDescription()
            {
                Texts = new List<LanguageSpecificText>()
                {
                    CreateDefaultObject.Create<LanguageSpecificText>()
                }
            };
            proInfo.OfferedProduct = productOffered; 
            #endregion

            #region Options

            var specificOption = CreateDefaultObject.Create<ProductOfferingSpecificationOption>();
            specificOption.SpecifiedProductOffering = CreateDefaultObject.Create<ProductOffering>();
            specificOption.RelatedProductOffering = proInfo;
            specificOption.Id = 100;

            var groupOption = CreateDefaultObject.Create<ProductOfferingGroupOption>();
            groupOption.Id = 200;
            groupOption.RelatedProductOffering = proInfo;

            proInfo.Options.Add(specificOption);
            proInfo.Options.Add(groupOption);

            #endregion

            #endregion

            var proDto = proInfo.ToDto();

            Assert.AreEqual(proInfo.Options.FirstOrDefault(x => x is ProductOfferingGroupOption).Id, 200);
            Assert.AreEqual(proInfo.Options.FirstOrDefault(x => x is ProductOfferingSpecificationOption).Id, 100);

            CheckAreEquals(proInfo, proDto, true);
        }

        [Test()]
        public static void ToCore_CorrectProductCatalogDTO_ShouldReturnProduct()
        {
            #region Set all the properties
            var proDto = CreateDefaultObject.Create<ProductCatalogDTO>();
            var pruchaseOption = CreateDefaultObject.Create<ProductPurchaseChargingOptionDTO>();
            pruchaseOption.Description = new List<TextualDescription>() { CreateDefaultObject.Create<TextualDescription>() };
            proDto.PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>(){pruchaseOption};
            proDto.Names = new List<TextualDescription>() {CreateDefaultObject.Create<TextualDescription>()};
            proDto.Descriptions = new List<TextualDescription>() { CreateDefaultObject.Create<TextualDescription>() };
            proDto.ProductDto = CreateDefaultObject.Create<ProductDto>();
            #endregion

            var proCore = proDto.ToCore();

            CheckAreEquals(proCore, proDto, false);
        }

        private static void CheckAreEquals(ProductOffering proInfo, ProductCatalogDTO proDto, Boolean checkCharges)
        {
            Assert.AreEqual(proDto.Id, proInfo.Id);
            Assert.AreEqual(proDto.Names.FirstOrDefault().Text, proInfo.Names.Texts.FirstOrDefault().Text);
            Assert.AreEqual(proDto.Names.FirstOrDefault().LanguageCode, proInfo.Names.Texts.FirstOrDefault().Language);
            Assert.AreEqual(proDto.Descriptions.FirstOrDefault().Text, proInfo.Description.Texts.FirstOrDefault().Text);
            Assert.AreEqual(proDto.Descriptions.FirstOrDefault().LanguageCode, proInfo.Description.Texts.FirstOrDefault().Language);

            CheckProductPurhcaseChargingOptionsAreEquals(proInfo.ChargingOptions.FirstOrDefault(), proDto.PurchaseOptions.FirstOrDefault(), checkCharges);
            CheckProductsAreEquals(proInfo.OfferedProduct, proDto.ProductDto);

        }

        private static void CheckProductPurhcaseChargingOptionsAreEquals(ProductChargeOption proInfo, ProductPurchaseChargingOptionDTO proDto, Boolean checkCharges)
        {

            Assert.AreEqual(proDto.Description.FirstOrDefault().Text, proInfo.Description.Texts.FirstOrDefault().Text);
            Assert.AreEqual(proDto.Description.FirstOrDefault().LanguageCode, proInfo.Description.Texts.FirstOrDefault().Language);
            Assert.AreEqual(proDto.EffectiveDate, proInfo.StartDate);
            Assert.AreEqual(proDto.ExpirationDate, proInfo.EndDate.Value);
            Assert.AreEqual(proDto.Id, proInfo.Id);

            if (checkCharges) CheckChargesAreEquals(proInfo.Charges.FirstOrDefault(), proDto.Charges.FirstOrDefault());
        }

        private static void CheckChargesAreEquals(Charge chargeInfo, ChargeCatalogDTO chargeDto)
        {
            Assert.AreEqual(chargeDto.Id, chargeInfo.Id.ToString());
            Assert.AreEqual(chargeDto.Category, chargeInfo.Category.ToString());
            Assert.AreEqual(chargeDto.CreateDate, chargeInfo.CreateTime);
            Assert.AreEqual(chargeDto.TimeOfCharge, chargeInfo.TypeOfTimeOfCharge);
            Assert.AreEqual(chargeDto.ProratingInformation.Quantity, chargeInfo.ProrateQty.Value);
            Assert.AreEqual(chargeDto.ProratingInformation.Unit, chargeInfo.ProrateUnit.Value);
            Assert.AreEqual(chargeDto.Name.FirstOrDefault().Text, chargeInfo.Name.Texts.FirstOrDefault().Text);
            Assert.AreEqual(chargeDto.Name.FirstOrDefault().LanguageCode, chargeInfo.Name.Texts.FirstOrDefault().Language);
            Assert.AreEqual(chargeDto.Description.FirstOrDefault().Text, chargeInfo.Description.Texts.FirstOrDefault().Text);
            Assert.AreEqual(chargeDto.Description.FirstOrDefault().LanguageCode, chargeInfo.Description.Texts.FirstOrDefault().Language);


        }

        private static void CheckProductsAreEquals(Product productInfo, ProductDto productDto)
        {
            Assert.AreEqual(productDto.CarrierId, productInfo.Carrier.Id);
            if (productDto.Descriptions.Any())
                Assert.AreEqual(productDto.Descriptions.FirstOrDefault().Text, productInfo.Description.Texts.FirstOrDefault().Text);
            Assert.AreEqual(productDto.ExternalReference, productInfo.ExternalReference);
            Assert.AreEqual(productDto.Id, productInfo.Id);
            if (productDto.Names.Any())
                Assert.AreEqual(productDto.Names.FirstOrDefault().Text, productInfo.Names.Texts.FirstOrDefault().Text);
            Assert.AreEqual(productDto.ProductType, productInfo.Type.Description);
        }
    }
}
