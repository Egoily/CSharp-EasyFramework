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
    public class ProductPurchaseChargingOptionDtoMappingTest
    {
        [Test()]
        public static void ProductPurchaseChargingOptionCoreToDto()
        {
            #region Set all the properties
            var proInfo = new ProductChargeOption()
                {
                    CreateDate = CreateDefaultObject.Create<DateTime>(),
                    EndDate = CreateDefaultObject.Create<DateTime>(),
                    Id = CreateDefaultObject.Create<int>(),
                    IsDefaultOption = DefaultOptions.Y,
                    ProductOfOption = CreateDefaultObject.Create<Product>(),
                    StartDate = CreateDefaultObject.Create<DateTime>(),
                    Name = new MultiLingualDescription(),
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

            proInfo.Charges = new List<Charge>() { chargeInfo };
            proInfo.Name.Texts = new List<LanguageSpecificText>()
            {
                new LanguageSpecificText()
                {
                    Text = "text1",
                    Language = ISO639LanguageCodes.eng,
                }
            }; 
            #endregion

            var proDto = proInfo.ToDto();

            CheckAreEquals(proInfo, proDto);
        }

        private static void CheckAreEquals(ProductChargeOption proInfo, ProductPurchaseChargingOptionDTO proDto)
        {

            Assert.AreEqual(proDto.Description.FirstOrDefault().Text, proInfo.Name.Texts.FirstOrDefault().Text);
            Assert.AreEqual(proDto.Description.FirstOrDefault().LanguageCode, proInfo.Name.Texts.FirstOrDefault().Language);
            Assert.AreEqual(proDto.EffectiveDate, proInfo.StartDate);
            Assert.AreEqual(proDto.ExpirationDate, proInfo.EndDate.Value);
            Assert.AreEqual(proDto.Id, proInfo.Id);

            CheckChargesAreEquals(proInfo.Charges.FirstOrDefault(), proDto.Charges.FirstOrDefault());
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
