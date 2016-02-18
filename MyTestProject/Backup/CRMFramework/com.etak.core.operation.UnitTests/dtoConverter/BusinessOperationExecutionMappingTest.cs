using com.etak.core.model.operation;
using com.etak.core.operation.dtoConverters;
using com.etak.core.test.utilities;
using NUnit.Framework;

namespace com.etak.core.operation.UnitTests.dtoConverter
{
    public class TestOrder : Order
    {

        public override string Discriminator
        {
            get { return "NEVER"; }
        }
    }
    [TestFixture]
    public class BusinessOperationExecutionMappingTest
    {

        [Test]
        public static void ToDto_CorrectBusinessOperationExecution_ShouldReturnBusinessOperationExecutionCoreToDto()
        {
            var bizOpTraze = CreateDefaultObject.Create<BusinessOperationExecution>();
            bizOpTraze.OrderManaged = new TestOrder(); // Order is abstract can't be created by default
            var bizOptrazeDto = bizOpTraze.ToDto();
            CheckAreEquals(bizOpTraze, bizOptrazeDto);
        }

        private static void CheckAreEquals(BusinessOperationExecution bizOpTraze, BusinessOperationExecutionDTO bizOpTraceDTO)
        {
            Assert.AreEqual(bizOpTraceDTO.AccountId, bizOpTraceDTO.AccountId);
            Assert.AreEqual(bizOpTraceDTO.Amount, bizOpTraze.Amount);
            Assert.AreEqual(bizOpTraceDTO.Channel, bizOpTraze.Channel);
            Assert.AreEqual(bizOpTraceDTO.CustomerDestinationId, bizOpTraze.CustomerDestination.CustomerID);
            Assert.AreEqual(bizOpTraceDTO.CustomerId, bizOpTraze.Customer.CustomerID);
            Assert.AreEqual(bizOpTraceDTO.EndDate, bizOpTraze.EndDate);
            Assert.AreEqual(bizOpTraceDTO.ErrorCode, bizOpTraze.ErrorCode);
            Assert.AreEqual(bizOpTraceDTO.ICCId, bizOpTraze.SimCard.ICCID);
            Assert.AreEqual(bizOpTraceDTO.Id, bizOpTraze.Id);
            Assert.AreEqual(bizOpTraceDTO.MSISDN, bizOpTraze.MSISDN.Resource);
            Assert.AreEqual(bizOpTraceDTO.MVNOId, bizOpTraze.MVNO.DealerID);
            //Assert.AreEqual(bizOpTraceDTO.OperationCode, bizOpTraze.OperationCode);
            Assert.AreEqual(bizOpTraceDTO.OrderManagedId, bizOpTraze.OrderManaged.Id);
            Assert.AreEqual(bizOpTraceDTO.ParentBusinessOperationId, bizOpTraze.ParentBusinessOperation.Id);
           // Assert.AreEqual(bizOpTraceDTO.ProcessorDiscriminator, bizOpTraze.ProcessorDiscriminator);
            Assert.AreEqual(bizOpTraceDTO.ProductAssignmentId, bizOpTraze.ProductAssignment.Id);
            Assert.AreEqual(bizOpTraceDTO.ProductOfferingId, bizOpTraze.ProductOffering.Id);
            Assert.AreEqual(bizOpTraceDTO.ResultType, bizOpTraze.ResultType);
            Assert.AreEqual(bizOpTraceDTO.RootBusinessOperationId, bizOpTraze.RootBusinessOperation.Id);
            Assert.AreEqual(bizOpTraceDTO.StartTime, bizOpTraze.StartTime);
            Assert.AreEqual(bizOpTraceDTO.SubscriptionDestinationId, bizOpTraze.SubscriptionDestination.ResourceID);
            Assert.AreEqual(bizOpTraceDTO.SubscriptionId, bizOpTraze.Subscription.ResourceID);
            Assert.AreEqual(bizOpTraceDTO.SystemMessages, bizOpTraze.SystemMessages);
            Assert.AreEqual(bizOpTraceDTO.UserId, bizOpTraze.User.UserID);
        }
    }
}
