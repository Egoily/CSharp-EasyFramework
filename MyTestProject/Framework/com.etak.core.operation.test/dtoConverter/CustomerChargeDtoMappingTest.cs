using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.dtoConverters;
using com.etak.core.test.utilities;
using NUnit.Framework;

namespace com.etak.core.operation.test.dtoConverter
{
    [TestFixture]
    public class CustomerChargeDtoMappingTest
    {
        [Test()]
        public static void CustomerChargeCoreToDto()
        {
            var chargeInfo = CreateDefaultObject.Create<CustomerCharge>();
            var customerInfo = CreateDefaultObject.Create<CustomerInfo>();
            var productInfo = CreateDefaultObject.Create<CustomerProductAssignment>();
            productInfo.PurchasedProduct = CreateDefaultObject.Create<Product>();
            var invoiceInfo = CreateDefaultObject.Create<Invoice>();
            var charge = CreateDefaultObject.Create<ChargeNonRecurring>();
            
            chargeInfo.ChargeDefinition = charge;
            chargeInfo.Customer = customerInfo;
            chargeInfo.Product = productInfo;
            
            chargeInfo.Invoice = invoiceInfo;

            var chargeDto = chargeInfo.ToDto();

            CheckAreEquals(chargeInfo, chargeDto);

        }

        private static void CheckAreEquals(CustomerCharge chargeInfo, CustomerChargeDTO chargeDto)
        {
            Assert.AreEqual(chargeDto.ChargeId, chargeInfo.ChargeDefinition.Id);
            Assert.AreEqual(chargeDto.Amount, chargeInfo.Amount);
            Assert.AreEqual(chargeDto.Currency, chargeInfo.Currency);
            Assert.AreEqual(chargeDto.CreateTime, chargeInfo.ChargingDate);
            Assert.AreEqual(chargeDto.CustomerId, chargeInfo.Customer.CustomerID);
            Assert.AreEqual(chargeDto.InvoiceId, chargeInfo.Invoice.Id);
            Assert.AreEqual(chargeDto.ProductPurchaseId, chargeInfo.Product.PurchasedProduct.Id);
            Assert.AreEqual(chargeDto.ReferenceCode, chargeInfo.ExternalReferenceCode);
        }

    }
}
