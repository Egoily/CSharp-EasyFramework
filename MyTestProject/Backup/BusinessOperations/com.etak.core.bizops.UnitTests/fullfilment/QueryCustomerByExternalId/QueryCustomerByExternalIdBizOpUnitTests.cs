using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.fullfilment.QueryCustomerByExternalId;
using com.etak.core.model;
using com.etak.core.model.dto;
using com.etak.core.model.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.util;
using com.etak.core.repository.crm;
using com.etak.core.repository.crm.customer;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.fullfilment.QueryCustomerByExternalId
{
    [TestFixture]
    public class QueryCustomerByExternalIdBizOpUnitTests : AbstractBusinessOperationTest<QueryCustomerByExternalIdBizOp, QueryCustomerByExternalIdRequestDTO, QueryCustomerByExternalIdResponseDTO, QueryCustomerByExternalIdRequestInternal, QueryCustomerByExternalIdResponseInternal>
    {
        [TestFixtureSetUp]
        public static void Initialize()
        {
            FakeSessionFactorySingleton.Init();
        }


        public CustomerInfo StandardCustomerInfo()
        {
            var expectedCustomer = CreateDefaultObject.Create<CustomerInfo>();
            expectedCustomer.DealerID = 1;
            expectedCustomer.CustomerID = 9;
            expectedCustomer.ResourceMBInfo.FirstOrDefault(x => x.StatusID == (int)ResourceStatus.Active).CustomerInfo
                = expectedCustomer;
            return expectedCustomer;
        }

        public void StandardMockCustomerInfo(CustomerInfo expectedCustomerInfo)
        {
            var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            List<CustomerInfo> customerInfos = new List<CustomerInfo>();
            customerInfos.Add(expectedCustomerInfo);
            mockedRepoCustomerInfo.GetByExternalId("1").Returns(customerInfos);
            mockedRepoCustomerInfo.GetByExternalId("0").Returns((new List<CustomerInfo>()));
        }

        [Test()]
        public void QueryCustomerByExternalIdBizOp_CorrectRequestGiven_ShouldReturnCorrectCustomer()
        {
            var expectedCustomer = StandardCustomerInfo();
            var expectedCustomerDto = expectedCustomer.ToDto();
            var expectedCustomerDtos = new List<CustomerDTO> {expectedCustomerDto};
            var expectedQueryCustomerByExternalIdBizOpResponseDTO = new QueryCustomerByExternalIdResponseDTO()
            {
                CustomerDTOs = expectedCustomerDtos,
                Customer = expectedCustomerDto,
                Subscription = expectedCustomer.ResourceMBInfo.FirstOrDefault().ToDto()
            };
            var queryCustomerByExternalIdBizOpRequestDTO = new QueryCustomerByExternalIdRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                ExternalCustomerId = "1"
            };

            MockAbsctractBusinessOperation(queryCustomerByExternalIdBizOpRequestDTO);
            StandardMockCustomerInfo(expectedCustomer);

            var actualQueryCustomerByExternalIdBizOpResponseDTO = CallBizOp(queryCustomerByExternalIdBizOpRequestDTO);
            AssertExt.ObjectPropertiesAreEqual(expectedQueryCustomerByExternalIdBizOpResponseDTO.CustomerDTOs, actualQueryCustomerByExternalIdBizOpResponseDTO.CustomerDTOs);
            AssertExt.ObjectPropertiesAreEqual(expectedQueryCustomerByExternalIdBizOpResponseDTO.Customer, actualQueryCustomerByExternalIdBizOpResponseDTO.Customer);
            AssertExt.ObjectPropertiesAreEqual(expectedQueryCustomerByExternalIdBizOpResponseDTO.Subscription, actualQueryCustomerByExternalIdBizOpResponseDTO.Subscription);
        }

        [Test()]
        public void QueryCustomerByExternalIdBizOp_CorrectRequestGivenButNoCustomer_ShouldReturnNullCustomer()
        {
            var queryCustomerByExternalIdBizOpRequestDTO = new QueryCustomerByExternalIdRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                ExternalCustomerId = "0"
            };

            MockAbsctractBusinessOperation(queryCustomerByExternalIdBizOpRequestDTO);
            StandardMockCustomerInfo(StandardCustomerInfo()); 

            var actualQueryCustomerByExternalIdBizOpResponseDTO = CallBizOp(queryCustomerByExternalIdBizOpRequestDTO);
            AssertExt.IsEmpty(actualQueryCustomerByExternalIdBizOpResponseDTO.CustomerDTOs);
            Assert.IsNull(actualQueryCustomerByExternalIdBizOpResponseDTO.Customer);
            Assert.IsNull(actualQueryCustomerByExternalIdBizOpResponseDTO.Subscription);
        }
    }
}