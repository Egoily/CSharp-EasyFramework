using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.dtoConverters;
using com.etak.core.test.utilities;
using NUnit.Framework;

namespace com.etak.core.operation.UnitTests.dtoConverter
{
    [TestFixture]
    public class ChargeCatalogDtoMappingTest
    {
        [Test]
        public static void ToDto_CorrectChargeRecurring_ShouldReturnChargeCatalogDTO()
        {

            #region Create Object
            var chargeInfo = CreateDefaultObject.Create<ChargeRecurring>();
            chargeInfo.Prices.First().EndDate = null;
            AddNameAndDescription(chargeInfo);
            #endregion

            var chargeDto = chargeInfo.ToDto();

            CheckAreEquals(chargeInfo, chargeDto);
        }

        [Test]
        public static void ToDto_CorrectChargeNonRecurring_ShouldReturnChargeCatalogDTO()
        {

            #region Create Object
            var chargeInfo = CreateDefaultObject.Create<ChargeNonRecurring>();
            chargeInfo.Prices.First().EndDate = null;
            AddNameAndDescription(chargeInfo);
            #endregion

            var chargeDto = chargeInfo.ToDto();

            CheckAreEquals(chargeInfo, chargeDto);
        }

        private static void CheckAreEquals(Charge chargeInfo, ChargeCatalogDTO chargeDto)
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

        private static void AddNameAndDescription(Charge chargeInfo)
        {
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
        }
    }
}
