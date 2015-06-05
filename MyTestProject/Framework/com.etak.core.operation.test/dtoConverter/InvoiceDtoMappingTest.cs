using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.dtoConverters;
using com.etak.core.test.utilities;
using NUnit.Framework;

namespace com.etak.core.operation.test.dtoConverter
{
    [TestFixture]
    public class InvoiceDtoMappingTest
    {
        [Test()]
        public void InvoiceCoreToDto()
        {
            var invoiceCore = CreateDefaultObject.Create<Invoice>();
            invoiceCore.GeneratingBillRun = new BillRun()
            {
                BillingCycle = CreateDefaultObject.Create<BillCycle>(),
            };
            var customerCharge = CreateDefaultObject.Create<CustomerCharge>();
            customerCharge.ChargeDefinition = new ChargeAggregate()
            {
                Id = 1,
            };

            invoiceCore.Charges = new List<CustomerCharge>() { customerCharge };
            
            var invoiceDto = invoiceCore.ToDto(1);

            CheckAreEquals(invoiceCore, invoiceDto);

        }

        private void CheckAreEquals(Invoice invoiceCore, InvoiceDTO invoiceDto)
        {
            Assert.AreEqual(invoiceDto.InvoiceId, invoiceCore.Id);
            Assert.AreEqual(invoiceDto.BillingCycle, invoiceCore.GeneratingBillRun.BillingCycle.Id);
            Assert.AreEqual(invoiceDto.Amount, invoiceCore.Charges.Sum(x => x.Amount + x.TaxAmount));
        }
    }
}
