using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.dtoConverters;
using com.etak.core.test.utilities;
using NUnit.Framework;

namespace com.etak.core.operation.test.dtoConverter
{
    [TestFixture]
    public class CustomerRecurringChargeDtoMappingTest
    {
        [Test()]
        public static void CustomerRecurringChargeInfoToDto()
        {
            var chargeInfo = CreateDefaultObject.Create<CustomerCharge>();
            var customerInfo = CreateDefaultObject.Create<CustomerInfo>();
            var productInfo = CreateDefaultObject.Create<CustomerProductAssignment>();
            var invoiceInfo = CreateDefaultObject.Create<Invoice>();
            var charge = CreateDefaultObject.Create<ChargeNonRecurring>();
            var schedule = CreateDefaultObject.Create<CustomerChargeSchedule>();
            productInfo.PurchasedProduct = CreateDefaultObject.Create<Product>();

            chargeInfo.ChargeDefinition = charge;
            chargeInfo.Customer = customerInfo;
            chargeInfo.Product = productInfo;
            chargeInfo.Invoice = invoiceInfo;
            chargeInfo.Schedule = schedule;

            var chargeDto = chargeInfo.ToRecurringChargeDto();

            CheckAreEquals(chargeInfo, chargeDto);
        }

        private static void CheckAreEquals(CustomerCharge chargeInfo, CustomerRecurringChargeDTO chargeDto)
        {
            Assert.AreEqual(chargeDto.Amount, chargeInfo.Amount);
            Assert.AreEqual(chargeDto.ChargeId, chargeInfo.ChargeDefinition.Id);
            Assert.AreEqual(chargeDto.CreateTime, chargeInfo.ChargingDate);
            Assert.AreEqual(chargeDto.CustomerId, chargeInfo.Customer.CustomerID);
            Assert.AreEqual(chargeDto.InvoiceId, chargeInfo.Invoice.Id);
            Assert.AreEqual(chargeDto.ProductPurchaseId, chargeInfo.Product.PurchasedProduct.Id);
            Assert.AreEqual(chargeDto.ReferenceCode, chargeInfo.ExternalReferenceCode);
            Assert.AreEqual(chargeDto.NextChargeDate, chargeInfo.Schedule.NextChargeDate);
            Assert.AreEqual(chargeDto.NextPeriodNumber, chargeInfo.Schedule.NextPeriodNumber);
            Assert.AreEqual(chargeDto.CurrentCycleNumber, chargeInfo.Schedule.CurrentCyclenumber);
        }
    }
}
