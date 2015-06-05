using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.dtoConverters;
using com.etak.core.test.utilities;
using NUnit.Framework;

namespace com.etak.core.operation.test.dtoConverter
{
 
    [TestFixture]
    public class ProductCatalogDtoMappingTest
    {
        [Test()]
        public static void ProductCatalogCoreToDto()
        {
            #region Set all the properties of the product
            var proInfo = CreateDefaultObject.Create<Product>();


            proInfo.ChildProducts = new List<Product>()
            {
                CreateDefaultObject.Create<Product>()
            };
            proInfo.ParentProducts = new List<Product>()
            {
                CreateDefaultObject.Create<Product>()
            };
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
                ProductOfOption = CreateDefaultObject.Create<Product>(),
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
            #endregion


            var proDto = proInfo.ToDto();

            CheckAreEquals(proInfo, proDto, true);
        }

        [Test()]
        public static void ProductCatalogDtoToCore()
        {
            #region Set all the properties
            var proDto = CreateDefaultObject.Create<ProductCatalogDTO>();
            var pruchaseOption = CreateDefaultObject.Create<ProductPurchaseChargingOptionDTO>();
            pruchaseOption.Description = new List<TextualDescription>() { CreateDefaultObject.Create<TextualDescription>() };
            proDto.PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>(){pruchaseOption};
            proDto.Names = new List<TextualDescription>() {CreateDefaultObject.Create<TextualDescription>()};
            proDto.Descriptions = new List<TextualDescription>() { CreateDefaultObject.Create<TextualDescription>() };

            proDto.ChildProducts = new List<long>() { 1, 2 };
            proDto.ParentProducts = new List<long>() { 1, 2 };
            
            #endregion


            var proCore = proDto.ToCore();

            CheckAreEquals(proCore, proDto, false);
        }

        private static void CheckAreEquals(Product proInfo, ProductCatalogDTO proDto, Boolean checkCharges)
        {
            Assert.AreEqual(proDto.ChildProducts.FirstOrDefault(), proInfo.ChildProducts.FirstOrDefault().Id);
            Assert.AreEqual(proDto.ParentProducts.FirstOrDefault(), proInfo.ParentProducts.FirstOrDefault().Id);
            Assert.AreEqual(proDto.Id, proInfo.Id);
            Assert.AreEqual(proDto.Names.FirstOrDefault().Text, proInfo.Names.Texts.FirstOrDefault().Text);
            Assert.AreEqual(proDto.Names.FirstOrDefault().LanguageCode, proInfo.Names.Texts.FirstOrDefault().Language);
            Assert.AreEqual(proDto.Descriptions.FirstOrDefault().Text, proInfo.Description.Texts.FirstOrDefault().Text);
            Assert.AreEqual(proDto.Descriptions.FirstOrDefault().LanguageCode, proInfo.Description.Texts.FirstOrDefault().Language);


            CheckProductPurhcaseChargingOptionsAreEquals(proInfo.ChargingOptions.FirstOrDefault(), proDto.PurchaseOptions.FirstOrDefault(), checkCharges);

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
    }
}
