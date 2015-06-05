using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.dtoConverters;
using com.etak.core.test.utilities;
using NUnit.Framework;

namespace com.etak.core.operation.test.dtoConverter
{
    [TestFixture]
    public class CustomerProductAssingmentDtoMappingTests
    {
        [Test()]
        public static void ProductPurchaseCoreToDto()
        {
            var proInfo = CreateDefaultObject.Create<CustomerProductAssignment>();
            proInfo.PurchasingCustomer = CreateDefaultObject.Create<CustomerInfo>();
            proInfo.PurchasedProduct = CreateDefaultObject.Create<Product>();
            #region Create ProductChargeOption
            var productChargeOption = new ProductChargeOption()
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

            productChargeOption.Charges = new List<Charge>() { chargeInfo };
            productChargeOption.Name.Texts = new List<LanguageSpecificText>()
            {
                new LanguageSpecificText()
                {
                    Text = "text1",
                    Language = ISO639LanguageCodes.eng,
                }
            }; 
            #endregion
            proInfo.ProductChargePurchased = productChargeOption;

            CustomerProductAssignmentDTO proDto = proInfo.ToDto();

            CheckAreEquals(proInfo, proDto);
        }

        private static void CheckAreEquals(CustomerProductAssignment proInfo, CustomerProductAssignmentDTO proDto)
        {
            Assert.AreEqual(proDto.PurchasingCustomerId, proInfo.PurchasingCustomer.CustomerID);
            Assert.AreEqual(proDto.EndDate, proInfo.EndDate);
            Assert.AreEqual(proDto.Id, proInfo.Id);
            Assert.AreEqual(proDto.ProductChargePurchasedId, proInfo.ProductChargePurchased.Id);
            Assert.AreEqual(proDto.PurchasedProductId, proInfo.PurchasedProduct.Id);
            Assert.AreEqual(proDto.StartDate, proInfo.StartDate);
        }
    }
}
