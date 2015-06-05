using System;
using System.Collections.Generic;
using com.etak.core.microservices.messages.GetSucessfulOperationExecutionForCustomer;
using com.etak.core.microservices.microservices;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.repository.crm.operation;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.microservices.unittest
{
    [TestFixture]
    public class GetSucessfulOperationExecutionForCustomerTests : AbstractMicroServiceTest<GetSucessfulOperationExecutionForCustomerMS, GetSucessfulOperationExecutionForCustomerRequest, GetSucessfulOperationExecutionForCustomerResponse>
    {
        private readonly CustomerInfo _fakeCustomer = CreateDefaultObject.Create<CustomerInfo>();
        private readonly String _fakeDiscriminator = "UNITTEST";
        private IBusinessOperationExecutionRepository<BusinessOperationExecution> _mockedRepo;

        private readonly IEnumerable<BusinessOperationExecution> _expectedResult = CreateDefaultObject.Create<List<BusinessOperationExecution>>();
            
         [TestFixtureSetUp()]
        public void InitializeSetup()
        {
            _mockedRepo = MockRepository<IBusinessOperationExecutionRepository<BusinessOperationExecution>>();
             _mockedRepo.GetCustomerOperationsBetweenDates(_fakeCustomer, Arg.Any<String>(), Arg.Any<DateTime>(),
                 Arg.Any<DateTime>(), Arg.Any<ResultTypes[]>())
                 .Returns(_expectedResult);
        }

        private GetSucessfulOperationExecutionForCustomerRequest CreateRequest()
        {
            return  new GetSucessfulOperationExecutionForCustomerRequest
            {
                Customer = _fakeCustomer,
                StartDate = DateTime.Now,
                OperationDiscriminator = _fakeDiscriminator,
            };
        }

        [Test]
        public void GetOk()
        {
            var resp = CallMicroservice(CreateRequest());
            Assert.AreEqual(resp.ResultType, ResultTypes.Ok);
            Assert.AreEqual(resp.ErrorCode, 0);
            Assert.AreEqual(resp.Message, String.Empty);
            Assert.AreEqual(resp.Operations, _expectedResult);
        }

        [Test]
        public void NoCustomerProvided()
        {
            var req = CreateRequest();
            req.Customer = null;
            Assert.Throws<BusinessLogicErrorException>(() => CallMicroservice(req));
        }

    }
}
