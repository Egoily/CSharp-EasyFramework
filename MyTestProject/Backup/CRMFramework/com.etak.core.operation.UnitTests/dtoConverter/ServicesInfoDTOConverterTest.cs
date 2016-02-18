using com.etak.core.model;
using com.etak.core.model.subscription;
using com.etak.core.operation.dtoConverters;
using com.etak.core.test.utilities;
using NUnit.Framework;

namespace com.etak.core.operation.UnitTests.dtoConverter
{
    [TestFixture]
    public class ServicesInfoDTOConverterTest
    {

        [Test]
        public static void ToDto_CorrectServicesInfo_ShouldReturnServicesInfoDTO()
        {
            ServicesInfo addInfo = CreateDefaultObject.Create<ServicesInfo>();
            var addDto = addInfo.ToDto();
            CheckAreEquals(addInfo, addDto);
        }

        private static void CheckAreEquals(ServicesInfo addInfo, ServicesInfoDTO addDto)
        {
            Assert.AreEqual(addDto.BilledBalance, addInfo.BilledBalance);
            Assert.AreEqual(addDto.BundleDefinitionId, addInfo.BundleDefinition.BundleID);
            Assert.AreEqual(addDto.CREDITLIMITBASEBUNDLEID, addInfo.CREDITLIMITBASEBUNDLEID);
            Assert.AreEqual(addDto.CreateDate, addInfo.CreateDate);
            Assert.AreEqual(addDto.CreditLimit, addInfo.CreditLimit);
            Assert.AreEqual(addDto.CustomerId, addInfo.CustomerInfo.CustomerID);
            Assert.AreEqual(addDto.DeleteFlag, addInfo.DeleteFlag);
            Assert.AreEqual(addDto.DepositAmount, addInfo.DepositAmount);
            Assert.AreEqual(addDto.EndDate, addInfo.EndDate);
            Assert.AreEqual(addDto.Id, addInfo.ServiceID);
            Assert.AreEqual(addDto.InvoiceTemplateId, addInfo.InvoiceTemplateID);
            Assert.AreEqual(addDto.PointToBaseBundle, addInfo.PointToBaseBundle);
            Assert.AreEqual(addDto.ProductInfoId, addInfo.ProductInfo.ProductID);
            Assert.AreEqual(addDto.StartDate, addInfo.StartDate);
            Assert.AreEqual(addDto.Status, addInfo.Status);
            Assert.AreEqual(addDto.UnBilledBalance, addInfo.UnBilledBalance);
            Assert.AreEqual(addDto.UserID, addInfo.UserID);
           
        }
    }
}
