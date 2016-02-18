using System;
using com.etak.core.operation.UnitTests.operations.messages;
using com.etak.core.operation.manager;
using NUnit.Framework;

namespace com.etak.core.operation.UnitTests
{
    [TestFixture()]
    public class BusinessOperationManagerUnitTest
    {
         [Test()]
        public void GetBusinessOperationAndGetCoreBusinessOperationAndGetDTOBusinessOperation_CorrectFakeOrderRequestDTOAndFakeOrderResponseDTOAndFakeOrderRequestAndFakeOrderResponse_ShouldReturnBizOpAndBizOpCoreOpAndBizOpDtoOp()
         {
             Int32 dealerId = 1999;
             BusinessOperationManager.RebindTypesInAssemblyForDealer(typeof(BusinessOperationManagerUnitTest).Assembly, dealerId);
             BusinessOperationManager.RebindTypesInAssemblyForDealer(typeof(BusinessOperationManagerUnitTest).Assembly, dealerId);
             var bizOp = BusinessOperationManager.GetBusinessOperation<FakeOrderRequestDTO, FakeOrderResponseDTO, FakeOrderRequest, FakeOrderResponse>(dealerId);
             var bizCoreOp = BusinessOperationManager.GetCoreBusinessOperation<FakeOrderRequest, FakeOrderResponse>(dealerId);
             var bizDToOp = BusinessOperationManager.GetDTOBusinessOperation<FakeOrderRequestDTO, FakeOrderResponseDTO>(dealerId);
            
             Assert.AreNotEqual(bizOp, null);
             Assert.AreNotEqual(bizCoreOp, null);
             Assert.AreNotEqual(bizDToOp, null);
         }
    }
}
