using System.Reflection;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;
using com.etak.core.operation.test.automapping.amount;
using com.etak.core.operation.test.automapping.subscription;
using com.etak.core.operation.test.common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.etak.core.operation.test.automapping.customer
{
    [TestClass]
    public class CustomerAutomap : RepositoryBasedUnitTest
    {
        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            MyClassInitialize(testContext);
        }

        [TestMethod]
        public void CustomerIdMappedRequest()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            CustomerIdBasedRequestDTO req = GenerateRequest<CustomerIdBasedRequestDTO>();
            req.CustomerId = 1673003372;
            var op = new AlwaysOkBusinessOperation<CustomerIdBasedRequestDTO, ResponseBaseDTO, CustomerBasedRequest, CustomerBasedResponse>();
            op.ProcessFromCustomerModel(new NullValidator<CustomerIdBasedRequestDTO>(),
                       new SameTypeConverter<CustomerIdBasedRequestDTO>(),
                       new SameTypeConverter<ResponseBaseDTO>(),
                       req, new RequestInvokationEnvironment { Invoker = minfo });

        }

        [TestMethod]
        public void ExternalCustomerIdMappedRequest()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            ExternalCustomerIdBasedRequestDTO req = GenerateRequest<ExternalCustomerIdBasedRequestDTO>();
            req.ExternalCustomerId = "9901011722";
            var op = new AlwaysOkBusinessOperation<ExternalCustomerIdBasedRequestDTO, ResponseBaseDTO, MultiCustomerBasedrequest, CustomerBasedResponse>();
            op.ProcessFromCustomerModel(new NullValidator<ExternalCustomerIdBasedRequestDTO>(),
                       new SameTypeConverter<ExternalCustomerIdBasedRequestDTO>(),
                       new SameTypeConverter<ResponseBaseDTO>(),
                       req, new RequestInvokationEnvironment { Invoker = minfo });

        }

        [TestMethod]
        public void DocumentNumberCustomerMappedRequest()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            DocumentIdCustomerBasedRequestDTO req = GenerateRequest<DocumentIdCustomerBasedRequestDTO>();
            req.DocumentNumber = "47796774Z";
            req.DocumentType = 3;
            var op = new AlwaysOkBusinessOperation<DocumentIdCustomerBasedRequestDTO, ResponseBaseDTO, CustomerBasedRequest, CustomerBasedResponse>();
            op.ProcessFromCustomerModel(new NullValidator<DocumentIdCustomerBasedRequestDTO>(),
                       new SameTypeConverter<DocumentIdCustomerBasedRequestDTO>(),
                       new SameTypeConverter<ResponseBaseDTO>(),
                       req, new RequestInvokationEnvironment { Invoker = minfo });

        }

        [TestMethod]
        public void CustomerMappedResponse()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            CustomerIdBasedRequestDTO req = GenerateRequest<CustomerIdBasedRequestDTO>();
            req.CustomerId = 1673003372;
            var op = new ReceiveCustomerOutputCustomer<CustomerIdBasedRequestDTO, CustomerBasedResponseDTO>();
            op.ProcessFromCustomerModel(new NullValidator<CustomerIdBasedRequestDTO>(),
                       new SameTypeConverter<CustomerIdBasedRequestDTO>(),
                       new SameTypeConverter<CustomerBasedResponseDTO>(),
                       req, new RequestInvokationEnvironment { Invoker = minfo });

        }

        [TestMethod]
        public void CustomerBasedRequestButNoCustomerIdField()
        {

            MethodBase minfo = MethodBase.GetCurrentMethod();
            MsisdnBasedRequestDTO req = GenerateRequest<MsisdnBasedRequestDTO>();
            req.MSISDN = "47796774Z";
            var op =
                new AlwaysOkBusinessOperation
                    <MsisdnBasedRequestDTO, ResponseBaseDTO, CustomerBasedRequest, CustomerBasedResponse>();
            op.ProcessFromCustomerModel(
                new NullValidator<MsisdnBasedRequestDTO>(),
                new SameTypeConverter<MsisdnBasedRequestDTO>(),
                new SameTypeConverter<ResponseBaseDTO>(),
                req, new RequestInvokationEnvironment { Invoker = minfo });
            Assert.Fail("CustomerBased core request, but dto request had not ID declared and did not throw an exception");
        }

        [TestMethod]
        public void JointCustomerIdBasedRequest()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            JointCustomerIdBasedRequestDTO req = GenerateRequest<JointCustomerIdBasedRequestDTO>();
            req.SourceCustomerId = 1673003372;
            req.DestinationCustomerId = 1673003372;

            var op = new AlwaysOkBusinessOperation<JointCustomerIdBasedRequestDTO, ResponseBaseDTO, JointCustomerBasedRequest, CustomerBasedResponse>();
            op.ProcessFromCustomerModel(
                new NullValidator<JointCustomerIdBasedRequestDTO>(),
                new SameTypeConverter<JointCustomerIdBasedRequestDTO>(),
                new SameTypeConverter<ResponseBaseDTO>(),
                req, new RequestInvokationEnvironment() {Invoker = minfo});

        }

        [TestMethod]
        public void JointSubscriptionBasedRequest()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            JointMsisdnBasedRequestDTO req = GenerateRequest<JointMsisdnBasedRequestDTO>();
            req.SourceMSISDN = "34602623025";
            req.DestinationMSISDN = "34602623121";

            var op = new AlwaysOkBusinessOperation<JointMsisdnBasedRequestDTO, ResponseBaseDTO, JointSubscriptionBasedRequest, CustomerBasedResponse>();
            op.ProcessFromCustomerModel(
                new NullValidator<JointMsisdnBasedRequestDTO>(),
                new SameTypeConverter<JointMsisdnBasedRequestDTO>(),
                new SameTypeConverter<ResponseBaseDTO>(),
                req, new RequestInvokationEnvironment() { Invoker = minfo });
        }

        [TestMethod]
        public void AmountBasedRequest()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            AmountBasedRequestDTO req = GenerateRequest<AmountBasedRequestDTO>();
            req.amount = 0.9m;

            var op = new AlwaysOkBusinessOperation<AmountBasedRequestDTO, ResponseBaseDTO, AmountBasedRequest, CustomerBasedResponse>();
            op.ProcessFromCustomerModel(
                new NullValidator<AmountBasedRequestDTO>(),
                new SameTypeConverter<AmountBasedRequestDTO>(),
                new SameTypeConverter<ResponseBaseDTO>(),
                req, new RequestInvokationEnvironment() { Invoker = minfo });
        }

        [TestMethod]
        public void AccountBasedRequest()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            AccountBasedRequestDTO req = GenerateRequest<AccountBasedRequestDTO>();
            req.AccountId = 1674000000000000059;

            var op = new AlwaysOkBusinessOperation<AccountBasedRequestDTO, ResponseBaseDTO, AccountBasedRequest, CustomerBasedResponse>();
            op.ProcessFromCustomerModel(
                new NullValidator<AccountBasedRequestDTO>(),
                new SameTypeConverter<AccountBasedRequestDTO>(),
                new SameTypeConverter<ResponseBaseDTO>(),
                req, new RequestInvokationEnvironment() { Invoker = minfo });
        }
    }
}
