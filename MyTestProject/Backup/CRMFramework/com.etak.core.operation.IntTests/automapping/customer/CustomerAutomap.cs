using System.Reflection;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;
using com.etak.core.operation.IntTests;
using com.etak.core.operation.IntTests.automapping.amount;
using com.etak.core.operation.IntTests.automapping.customer;
using com.etak.core.operation.IntTests.common;
//using com.etak.core.operation.IntTests.automapping.subscription;
using com.etak.core.operation.IntTests.operations.messages;
using NUnit.Framework;
using com.etak.core.operation.IntTests.automapping.subscription;


namespace com.etak.core.operation.UnitTests.automapping.customer
{
    [TestFixture]
    public class CustomerAutomap : RepositoryBasedUnitTest
    {
        [TestFixtureSetUp]
        public void Init()
        {
            MyClassInitialize();
        }

        [Test]
        public void CustomerIdMappedRequest()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            FakeBizOpRequestDTO req = GenerateRequest<FakeBizOpRequestDTO>();
            req.CustomerId = 1673003372;
            var op = new AlwaysOkBusinessOperation();
            op.ProcessFromCustomerModel(new NullValidator<FakeBizOpRequestDTO>(),
                       new SameTypeConverter<FakeBizOpRequestDTO>(),
                       new SameTypeConverter<FakeBizOpResponseDTO>(),
                       req, new RequestInvokationEnvironment { Invoker = minfo });
        }

        [Test]
        public void ExternalCustomerIdMappedRequest()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            FakeBizOpRequestDTO req = GenerateRequest<FakeBizOpRequestDTO>();
            req.ExternalCustomerId = "9901011722";
            var op = new AlwaysOkBusinessOperation();
            op.ProcessFromCustomerModel(new NullValidator<FakeBizOpRequestDTO>(),
                       new SameTypeConverter<FakeBizOpRequestDTO>(),
                       new SameTypeConverter<FakeBizOpResponseDTO>(),
                       req, new RequestInvokationEnvironment { Invoker = minfo });

        }

        [Test]
        public void DocumentNumberCustomerMappedRequest()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            FakeBizOpRequestDTO req = GenerateRequest<FakeBizOpRequestDTO>();
            req.DocumentNumber = "47796774Z";
            req.DocumentType = 3;
            var op = new AlwaysOkBusinessOperation();
            op.ProcessFromCustomerModel(new NullValidator<FakeBizOpRequestDTO>(),
                       new SameTypeConverter<FakeBizOpRequestDTO>(),
                       new SameTypeConverter<FakeBizOpResponseDTO>(),
                       req, new RequestInvokationEnvironment { Invoker = minfo });

        }

        [Test]
        public void CustomerMappedResponse()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            FakeBizOpRequestDTO req = GenerateRequest<FakeBizOpRequestDTO>();
            req.CustomerId = 1673003372;
            var op = new ReceiveCustomerOutputCustomer();
            op.ProcessFromCustomerModel(new NullValidator<FakeBizOpRequestDTO>(),
                       new SameTypeConverter<FakeBizOpRequestDTO>(),
                       new SameTypeConverter<FakeBizOpResponseDTO>(),
                       req, new RequestInvokationEnvironment { Invoker = minfo });

        }

        [Test]
        public void CustomerBasedRequestButNoCustomerIdField()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            MsisdnBasedRequestDTO req = GenerateRequest<MsisdnBasedRequestDTO>();
            req.MSISDN = "34611462095";
            var op =new ParameterErrorBusinessOperation();
            var res = op.ProcessFromCustomerModel(
                new NullValidator<MsisdnBasedRequestDTO>(),
                new SameTypeConverter<MsisdnBasedRequestDTO>(),
                new SameTypeConverter<ResponseBaseDTO>(),
                req, new RequestInvokationEnvironment { Invoker = minfo });
            Assert.AreEqual(ResultTypes.UnknownError,res.resultType);
        }

        [Test]
        public void JointCustomerIdBasedRequest()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            FakeBizOpRequestDTO req = GenerateRequest<FakeBizOpRequestDTO>();
            req.SourceCustomerId = 1673003372;
            req.DestinationCustomerId = 1673003372;

            var op = new AlwaysOkBusinessOperation();
            op.ProcessFromCustomerModel(
                new NullValidator<FakeBizOpRequestDTO>(),
                new SameTypeConverter<FakeBizOpRequestDTO>(),
                new SameTypeConverter<FakeBizOpResponseDTO>(),
                req, new RequestInvokationEnvironment() { Invoker = minfo });

        }

        [Test]
        public void JointSubscriptionBasedRequest()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            FakeBizOpRequestDTO req = GenerateRequest<FakeBizOpRequestDTO>();
            req.SourceMSISDN = "34602623025";
            req.DestinationMSISDN = "34602623121";

            var op = new AlwaysOkBusinessOperation();
            op.ProcessFromCustomerModel(
                new NullValidator<FakeBizOpRequestDTO>(),
                new SameTypeConverter<FakeBizOpRequestDTO>(),
                new SameTypeConverter<FakeBizOpResponseDTO>(),
                req, new RequestInvokationEnvironment() { Invoker = minfo });
        }

        [Test]
        public void AmountBasedRequest()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            FakeBizOpRequestDTO req = GenerateRequest<FakeBizOpRequestDTO>();
            req.amount = 0.9m;

            var op = new AlwaysOkBusinessOperation();
            op.ProcessFromCustomerModel(
                new NullValidator<FakeBizOpRequestDTO>(),
                new SameTypeConverter<FakeBizOpRequestDTO>(),
                new SameTypeConverter<FakeBizOpResponseDTO>(),
                req, new RequestInvokationEnvironment() { Invoker = minfo });
        }

        [Test]
        public void AccountBasedRequest()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            FakeBizOpRequestDTO req = GenerateRequest<FakeBizOpRequestDTO>();
            req.AccountId = 1674000000000000059;

            var op = new AlwaysOkBusinessOperation();
            op.ProcessFromCustomerModel(
                new NullValidator<FakeBizOpRequestDTO>(),
                new SameTypeConverter<FakeBizOpRequestDTO>(),
                new SameTypeConverter<FakeBizOpResponseDTO>(),
                req, new RequestInvokationEnvironment() { Invoker = minfo });
        }
    }
}
