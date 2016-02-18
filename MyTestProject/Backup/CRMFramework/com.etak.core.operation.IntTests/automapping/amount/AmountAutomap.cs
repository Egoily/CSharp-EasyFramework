using System;
using System.Collections.Generic;
using System.Reflection;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;
using com.etak.core.operation.IntTests.common;
using com.etak.core.operation.IntTests.operations.messages;
using com.etak.core.operation.util;
using com.etak.core.repository;
using com.etak.core.repository.crm;
using com.etak.core.test.utilities;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.operation.IntTests.automapping.amount
{
    [TestFixture()]
    public class AmountAutomap : RepositoryBasedUnitTest
    {
        [TestFixtureSetUp]
        public void Init()
        {
            MyClassInitialize();
            
        }

        [Test]
        public void AmountMappedRequest()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();

            FakeBizOpRequestDTO reqDto = GenerateRequest<FakeBizOpRequestDTO>();
            reqDto.amount = 3m;

            var op = new AlwaysOkBusinessOperation();

            //#region Mock AuthenticationHelper.Authenticate(request.user, request.password);
            //var mockedRepoLoginInfo = MockRepositoryManager.GetMockedRepository<ILoginInfoRepository<LoginInfo>>();
            //var actualLogInfos = new List<LoginInfo>();
            //var actualLoginInfo = CreateDefaultObject.Create<LoginInfo>();
            //actualLoginInfo.Password = MD5Utility.ComputeHash(reqDto.password);
            //actualLoginInfo.Status = 0;
            //actualLogInfos.Add(actualLoginInfo);
            //mockedRepoLoginInfo.GetByUserId(Int32.Parse(reqDto.user.Trim())).Returns(actualLogInfos);
            //#endregion

            //#region Mock repositories used in AbstractBusinessOperation.GetDealerInfoByVmoRepo()
            //var mockedRepoMVNOPropertiesInfo = MockRepositoryManager.GetMockedRepository<IMVNOPropertiesRepository<MVNOPropertiesInfo>>();
            //var actualMVNOPropertiesInfo = CreateDefaultObject.Create<MVNOPropertiesInfo>();
            //actualMVNOPropertiesInfo.DealerInfo = CreateDefaultObject.Create<DealerInfo>();
            //var actualMVNOPropertiesInfos = new List<MVNOPropertiesInfo>();
            //actualMVNOPropertiesInfos.Add(actualMVNOPropertiesInfo);
            //mockedRepoMVNOPropertiesInfo.GetByVMNOId(reqDto.vmno).Returns(actualMVNOPropertiesInfos);
            //#endregion

            //var resp = op.ProcessFromCustomerModel(new NullValidator<AmountBasedRequestDTO>(), new SameTypeConverter<AmountBasedRequestDTO>(), new SameTypeConverter<AmountDTOBasedResponse>(), reqDto, new RequestInvokationEnvironment { Invoker = minfo });

            var resp = op.ProcessFromCustomerModel(new NullValidator<FakeBizOpRequestDTO>(),
                       new SameTypeConverter<FakeBizOpRequestDTO>(),
                       new SameTypeConverter<FakeBizOpResponseDTO>(),
                       reqDto, new RequestInvokationEnvironment { Invoker = minfo });

            Assert.IsNotNull(resp.Amount);

        }

    }
}
