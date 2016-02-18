using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.revenue.ChargeService;
using com.etak.core.bizops.revenue.ChargeService.Proxy;
using com.etak.core.customer.message.GetAccountById;
using com.etak.core.customer.message.GetCustomerChargesScheduleById;
using com.etak.core.customer.message.GetCustomerInfoById;
using com.etak.core.customer.message.GetInvoiceById;
using com.etak.core.GSMSubscription.messages.GetCustomerProductAssignmentById;
using com.etak.core.microservices.messages.GetTaxDefinitionById;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.operation.implementation;
using com.etak.core.product.message.GetChargeById;
using com.etak.core.repository;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.revenue.ChargeService
{
    [TestFixture()]
    public class ChargeServiceBizOpUnitTests : AbstractBusinessOperationTest<ChargeServiceBizOp, ChargeServiceRequestDTO, ChargeServiceResponseDTO, ChargeServiceRequestInternal, ChargeServiceResponseInternal>
    {
        #region Create Mock MicroServices & related stuff

        private IMicroService<GetChargeByIdRequest, GetChargeByIdResponse> mockgetChargeByIdMS;
        private IMicroService<GetAccountByIdRequest, GetAccountByIdResponse> mockgetAccountByIdMs;
        private IMicroService<GetCustomerInfoByIdRequest, GetCustomerInfoByIdResponse> mockgetCustomerInfoByIdMS;
        private IMicroService<GetCustomerProductAssignmentByIdRequest, GetCustomerProductAssignmentByIdResponse> mockgetCustomerProductByIdMS;
        private IMicroService<GetCustomerChargesScheduleByIdRequest, GetCustomerChargesScheduleByIdResponse> mockgetCustomerChargeScheduleByIdMS;
        private IMicroService<GetInvoiceByIdRequest, GetInvoiceByIdResponse> mockedGetInvoiceById;

        private IApplyRecurringChargeInterface mockChargeService;

        // TODO: awaiting verison framework
        private IMicroService<GetTaxDefinitionByIdRequest, GetTaxDefinitionByIdResponse> mockgetTaxDefinitionByIdMS;

        #endregion

        [TestFixtureSetUp()]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();

            #region create mock MS

            mockgetChargeByIdMS = MockMicroService<GetChargeByIdRequest, GetChargeByIdResponse>();
            mockgetAccountByIdMs = MockMicroService<GetAccountByIdRequest, GetAccountByIdResponse>();
            mockgetCustomerInfoByIdMS = MockMicroService<GetCustomerInfoByIdRequest, GetCustomerInfoByIdResponse>();
            mockgetCustomerProductByIdMS = MockMicroService<GetCustomerProductAssignmentByIdRequest, GetCustomerProductAssignmentByIdResponse>();
            mockgetCustomerChargeScheduleByIdMS = MockMicroService<GetCustomerChargesScheduleByIdRequest, GetCustomerChargesScheduleByIdResponse>();
            mockedGetInvoiceById = MockMicroService<GetInvoiceByIdRequest, GetInvoiceByIdResponse>();

            // TODO: awaiting verison framework
            mockgetTaxDefinitionByIdMS = MockMicroService<GetTaxDefinitionByIdRequest, GetTaxDefinitionByIdResponse>();

            #endregion

            #region Create mock third party

            mockChargeService = Substitute.For<IApplyRecurringChargeInterface>();

            #endregion
        }

        [Test()]
        public void ChargeServiceBizOp_CorrectCustomerProductAssignmentGiven_ShouldReturnOK()
        {
            // setup
            var actualCharge = CreateDefaultObject.Create<ChargeNonRecurring>(1);
            var actualAccount = CreateDefaultObject.Create<Account>(1);
            var actualCusotmerInfo = CreateDefaultObject.Create<CustomerInfo>(1);
            actualCusotmerInfo.ResourceMBInfo = new List<ResourceMBInfo>(){CreateDefaultObject.Create<ResourceMBInfo>()};
            var cusotmerProductAssigs = new List<CustomerProductAssignment>()
            {
                CreateDefaultObject.Create<CustomerProductAssignment>(1),
                CreateDefaultObject.Create<CustomerProductAssignment>(1)
            };
            var actualCustomerCahrgeSchedule = CreateDefaultObject.Create<CustomerChargeSchedule>(1);
            actualCustomerCahrgeSchedule.Customer = CreateDefaultObject.Create<CustomerInfo>();

            actualCustomerCahrgeSchedule.Customer.ResourceMBInfo.Add(new ResourceMBInfo());
            var nextChargeSchedule = CreateDefaultObject.Create<nextChargeScheduleInfo>(1);
            var customerChargesRetuned = new List<customerChargeInfo>() { CreateDefaultObject.Create<customerChargeInfo>(1) };
            customerChargesRetuned[0].iso4217CurrencyCode = ISO4217CurrencyCodes.AED.ToString();
            var actualTaxDefinition = CreateDefaultObject.Create<TaxDefinition>(1);

            actualCustomerCahrgeSchedule.ChargeDefinition = actualCharge;
            actualCustomerCahrgeSchedule.ChargedAccount = actualAccount;
            actualCustomerCahrgeSchedule.Purchase = cusotmerProductAssigs[0];
            actualCustomerCahrgeSchedule.Purchase.PurchasingCustomer = actualCusotmerInfo;

            #region Setup mock External API to pass by DI

            // EXTERNAL API RESPONSE
            mockChargeService.firstPeriodChargingApply(Arg.Any<chargingRequestParameters>())
                .Returns
                (
                    new chargingApplyResponse()
                    {
                        code = 0,
                        flag = 0,
                        nextChargeScheduleInfo = nextChargeSchedule,
                        customerChargedResultList = customerChargesRetuned.ToArray(),
                        errorMsg = ""
                    }
                );

            #endregion

            #region Setup mock MS

            // Mock MS
            mockgetChargeByIdMS.Process(Arg.Any<GetChargeByIdRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetChargeByIdResponse()
                    {
                        Charge = actualCharge
                    }
                );

            mockgetAccountByIdMs.Process(Arg.Any<GetAccountByIdRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetAccountByIdResponse()
                    {
                        Account = actualAccount
                    }
                );

            mockgetCustomerInfoByIdMS.Process(Arg.Any<GetCustomerInfoByIdRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetCustomerInfoByIdResponse()
                    {
                        CustomerInfo = actualCusotmerInfo
                    }
                );

            mockgetCustomerProductByIdMS.Process(Arg.Any<GetCustomerProductAssignmentByIdRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetCustomerProductAssignmentByIdResponse()
                    {
                        CustomerProductAssignment = cusotmerProductAssigs.FirstOrDefault()
                    }
                );

            mockgetCustomerChargeScheduleByIdMS.Process(Arg.Any<GetCustomerChargesScheduleByIdRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetCustomerChargesScheduleByIdResponse()
                    {
                        CustomerChargeSchedule = actualCustomerCahrgeSchedule
                    }
                );

            mockedGetInvoiceById.Process(Arg.Any<GetInvoiceByIdRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetInvoiceByIdResponse()
                    {
                        Invoice = CreateDefaultObject.Create<Invoice>()
                    }
                );

            mockgetTaxDefinitionByIdMS.Process(Arg.Any<GetTaxDefinitionByIdRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetTaxDefinitionByIdResponse()
                    {
                        TaxDefinition = actualTaxDefinition
                    }
                );

            #endregion

            var requestDto = new ChargeServiceRequestDTO()
            {
                CustomerChargeScheduleId = 1,
                datetimePriceEffective = DateTime.Now,
                datetimePurchase = DateTime.Now,
                Invoice = new InvoiceDTO()
                {
                    InvoiceId = 99
                },

                user = "1644000204",
                password = "123456",
                vmno = "970100"
            };

            MockAbsctractBusinessOperation(requestDto);

            #region Clone Cusotmized CallBizOP

            ChargeServiceResponseDTO response = new ChargeServiceResponseDTO();
            using (var conn = RepositoryManager.GetNewConnection())
            {
                ChargeServiceBizOp bizop = new ChargeServiceBizOp(mockChargeService);

                response = bizop.ProcessFromCustomerModel(new NullValidator<ChargeServiceRequestDTO>(), new SameTypeConverter<ChargeServiceRequestDTO>(),
                    new SameTypeConverter<ChargeServiceResponseDTO>(), requestDto, FakeInvoker);
            }
            RepositoryManager.CloseConnection();

            #endregion

            //ChargeServiceResponseDTO res = CallBizOp(requestDto);

            Assert.AreEqual(response.resultType, ResultTypes.Ok);
            Assert.IsNotNull(response.Subscription);
        }

        [Test()]
        public void ChargeServiceBizOp_IncorrectCustomerProductAssignmentGiven_ShouldThrowException()
        {
            // setup
            var actualCharge = CreateDefaultObject.Create<ChargeNonRecurring>(1);
            var actualAccount = CreateDefaultObject.Create<Account>(1);
            var actualCusotmerInfo = CreateDefaultObject.Create<CustomerInfo>(1);
            var cusotmerProductAssigs = new List<CustomerProductAssignment>()
            {
                CreateDefaultObject.Create<CustomerProductAssignment>(1),
                CreateDefaultObject.Create<CustomerProductAssignment>(1)
            };
            var actualCustomerCahrgeSchedule = CreateDefaultObject.Create<CustomerChargeSchedule>(1);
            var nextChargeSchedule = CreateDefaultObject.Create<nextChargeScheduleInfo>(1);
            var customerChargesRetuned = new List<customerChargeInfo>() { CreateDefaultObject.Create<customerChargeInfo>(1) };
            customerChargesRetuned[0].iso4217CurrencyCode = ISO4217CurrencyCodes.AED.ToString();
            var actualTaxDefinition = CreateDefaultObject.Create<TaxDefinition>(1);

            actualCustomerCahrgeSchedule.ChargeDefinition = actualCharge;
            actualCustomerCahrgeSchedule.ChargedAccount = actualAccount;
            actualCustomerCahrgeSchedule.Purchase = cusotmerProductAssigs[0];
            actualCustomerCahrgeSchedule.Purchase.PurchasingCustomer = actualCusotmerInfo;

            #region Setup mock External API to pass by DI

            // EXTERNAL API RESPONSE
            mockChargeService.firstPeriodChargingApply(Arg.Any<chargingRequestParameters>())
                .Returns
                (
                    new chargingApplyResponse()
                    {
                        code = 1,
                        flag = 1,
                        nextChargeScheduleInfo = nextChargeSchedule,
                        customerChargedResultList = customerChargesRetuned.ToArray(),
                        errorMsg = ""
                    }
                );

            #endregion

            #region Setup mock MS

            // Mock MS
            mockgetChargeByIdMS.Process(Arg.Any<GetChargeByIdRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetChargeByIdResponse()
                    {
                        Charge = actualCharge
                    }
                );

            mockgetAccountByIdMs.Process(Arg.Any<GetAccountByIdRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetAccountByIdResponse()
                    {
                        Account = actualAccount
                    }
                );

            mockgetCustomerInfoByIdMS.Process(Arg.Any<GetCustomerInfoByIdRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetCustomerInfoByIdResponse()
                    {
                        CustomerInfo = actualCusotmerInfo
                    }
                );

            mockgetCustomerProductByIdMS.Process(Arg.Any<GetCustomerProductAssignmentByIdRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetCustomerProductAssignmentByIdResponse()
                    {
                        CustomerProductAssignment = cusotmerProductAssigs.FirstOrDefault()
                    }
                );

            mockgetCustomerChargeScheduleByIdMS.Process(Arg.Any<GetCustomerChargesScheduleByIdRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetCustomerChargesScheduleByIdResponse()
                    {
                        CustomerChargeSchedule = actualCustomerCahrgeSchedule
                    }
                );

            mockedGetInvoiceById.Process(Arg.Any<GetInvoiceByIdRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetInvoiceByIdResponse()
                    {
                        Invoice = CreateDefaultObject.Create<Invoice>()
                    }
                );

            mockgetTaxDefinitionByIdMS.Process(Arg.Any<GetTaxDefinitionByIdRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetTaxDefinitionByIdResponse()
                    {
                        TaxDefinition = actualTaxDefinition
                    }
                );

            #endregion

            var requestDto = new ChargeServiceRequestDTO()
            {
                CustomerChargeScheduleId = 1,
                datetimePriceEffective = DateTime.Now,
                datetimePurchase = DateTime.Now,
                Invoice = new InvoiceDTO()
                {
                    InvoiceId = 99
                },

                user = "1644000204",
                password = "123456",
                vmno = "970100"
            };

            MockAbsctractBusinessOperation(requestDto);

            #region Clone Cusotmized CallBizOP

            ChargeServiceResponseDTO response = new ChargeServiceResponseDTO();
            using (var conn = RepositoryManager.GetNewConnection())
            {
                ChargeServiceBizOp bizop = new ChargeServiceBizOp(mockChargeService);

                response = bizop.ProcessFromCustomerModel(new NullValidator<ChargeServiceRequestDTO>(), new SameTypeConverter<ChargeServiceRequestDTO>(),
                    new SameTypeConverter<ChargeServiceResponseDTO>(), requestDto, FakeInvoker);
            }
            RepositoryManager.CloseConnection();

            #endregion

            //ChargeServiceResponseDTO res = CallBizOp(requestDto);

            Assert.AreEqual(response.resultType, ResultTypes.UnknownError);
        }

        [Test()]
        public void ChargeServiceBizOp_CustomerChargeScheduleIsNull_ShouldThrowDataValidationException()
        {
            var chargeServiceRequestDto = new ChargeServiceRequestDTO()
            {
                CustomerChargeScheduleId = 1,
                datetimePurchase = DateTime.Now,
                Invoice = CreateDefaultObject.Create<InvoiceDTO>(),
                user = "1644000204",
                password = "123456",
                vmno = "970100"
            };

            mockgetCustomerChargeScheduleByIdMS.Process(Arg.Any<GetCustomerChargesScheduleByIdRequest>(),
                Arg.Any<RequestInvokationEnvironment>())
                .Returns
                (
                    new GetCustomerChargesScheduleByIdResponse()
                    {
                        CustomerChargeSchedule = null
                    }
                );

            mockedGetInvoiceById.Process(Arg.Any<GetInvoiceByIdRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns(new GetInvoiceByIdResponse()
                {
                    Invoice = new Invoice(),
                    ResultType = ResultTypes.Ok
                });

            MockAbsctractBusinessOperation(chargeServiceRequestDto);

            #region Clone Cusotmized CallBizOP

            ChargeServiceResponseDTO response = new ChargeServiceResponseDTO();
            using (var conn = RepositoryManager.GetNewConnection())
            {
                ChargeServiceBizOp bizop = new ChargeServiceBizOp(mockChargeService);

                response = bizop.ProcessFromCustomerModel(new NullValidator<ChargeServiceRequestDTO>(), new SameTypeConverter<ChargeServiceRequestDTO>(),
                    new SameTypeConverter<ChargeServiceResponseDTO>(), chargeServiceRequestDto, FakeInvoker);
            }
            RepositoryManager.CloseConnection();

            #endregion

            Assert.AreEqual(ResultTypes.DataValidationError, response.resultType);
        }
    }
}