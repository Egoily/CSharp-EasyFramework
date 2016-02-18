using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.fullfilment.QueryAvailableMsisdn;
using com.etak.core.dealer.messages.GetDealerInfosByFiscalUnitId;
using com.etak.core.dealer.messages.GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItem;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.resource;
using com.etak.core.operation;
using com.etak.core.operation.dtoConverters;
using com.etak.core.resource.msisdn.message.GetNumberByCategoryAndVmoAndStatusIdIn;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;
using List = NHibernate.Mapping.List;

namespace com.etak.core.bizops.UnitTests.fullfilment.QueryAvailableMsisdn
{
    [TestFixture()]
    public class QueryAvailableMsisdnBizOpUnitTests : AbstractBusinessOperationTest<QueryAvailableMsisdnBizOp, QueryAvailableMsisdnRequestDTO, QueryAvailableMsisdnResponseDTO, QueryAvailableMsisdnRequestInternal, QueryAvailableMsisdnResponseInternal>
    {
        [TestFixtureSetUp]
        public static void Initialize()
        {
            FakeSessionFactorySingleton.Init();
        }

        [Test()]
        public void QueryAvailableMsisdnBizOp_CorrectRequestGiven_ShouldReturnCorrectNumber()
        {
            int fetchNumber = 2;
            //Mock GetDealerInfosByFiscalUnitIdMS
            var actualgetDealerInfosByFiscalUnitIdReq = Arg.Is<GetDealerInfosByFiscalUnitIdRequest>(x => x.FiscalUnitId == 1);
            var actualgetDealerInfosByFiscalUnitIdRes = new GetDealerInfosByFiscalUnitIdResponse()
            {
                DealerInfos = new List<DealerInfo>()
                {
                    CreateDefaultObject.Create<DealerInfo>()
                }
            };

            var getDealerInfosByFiscalUnitIdMSMock = MockMicroService<GetDealerInfosByFiscalUnitIdRequest, GetDealerInfosByFiscalUnitIdResponse>();
            getDealerInfosByFiscalUnitIdMSMock.Process(actualgetDealerInfosByFiscalUnitIdReq,
                Arg.Any<RequestInvokationEnvironment>()).Returns(actualgetDealerInfosByFiscalUnitIdRes);

            //Mock getNumberByCategoryAndVmoAndStatusIdInMS
            var statusList = new List<int> { (int)ResourceStatus.Init };
            var dealerIdList = actualgetDealerInfosByFiscalUnitIdRes.DealerInfos.Select(x => x.DealerID.Value);

            var actualGetNumberByCategoryAndVmoAndStatusIdInReq = Arg.Is<GetNumberByCategoryAndVmoAndStatusIdInRequest>(x => x.CategoryId == 1 && x.MaxElements == fetchNumber && x.StatusId.SequenceEqual(statusList) && x.Vmo.SequenceEqual(dealerIdList));

            var getNumberByCategoryAndVmoAndStatusIdInMSMock =
                MockMicroService
                    <GetNumberByCategoryAndVmoAndStatusIdInRequest, GetNumberByCategoryAndVmoAndStatusIdInResponse>();
            var actualGetNumberByCategoryAndVmoAndStatusIdInRes = new GetNumberByCategoryAndVmoAndStatusIdInResponse()
            {
                NumberInfo = new List<NumberInfo>
                {
                    CreateDefaultObject.Create<NumberInfo>(),
                    CreateDefaultObject.Create<NumberInfo>(),               
                }
            };


            getNumberByCategoryAndVmoAndStatusIdInMSMock.Process(actualGetNumberByCategoryAndVmoAndStatusIdInReq,
                Arg.Any<RequestInvokationEnvironment>()).Returns(actualGetNumberByCategoryAndVmoAndStatusIdInRes);


            var getMvnoConfigActionMSMock = MockMicroService<GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest,
                        GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResponse>();


            var getMvnoConfigActionReqest = new GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest()
            {
                CategoryId =0,
                MvnoId = 190000,
                Item = "Filter_Msisdns_Range",
                StatusId = 1
            };
            var getMvnoConfigActionResponse = new GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResponse()
            {
                ResultType = ResultTypes.Ok,
                MvnoConfigActionInfos = new List<MVNOConfigActionInfo>()
                {
                    new MVNOConfigActionInfo(){Value = "34603051220-34603051222;34603051275-34603051275"}
                }
            };
            getMvnoConfigActionMSMock.Process(getMvnoConfigActionReqest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(getMvnoConfigActionResponse);

            var getNumberByCategoryAndVmoAndStatusIdInMsMock =
                  MockMicroService<GetNumberByCategoryAndVmoAndStatusIdInRequest, GetNumberByCategoryAndVmoAndStatusIdInResponse>();
            var getNumberByCategoryAndVmoAndStatusIdInRequest = new GetNumberByCategoryAndVmoAndStatusIdInRequest()
            {

            };
            getNumberByCategoryAndVmoAndStatusIdInMsMock.Process(getNumberByCategoryAndVmoAndStatusIdInRequest, Arg.Any<RequestInvokationEnvironment>())
                .ReturnsForAnyArgs(new GetNumberByCategoryAndVmoAndStatusIdInResponse()
                {
                    NumberInfo = new List<NumberInfo>()
                    {
                       CreateDefaultObject.Create<NumberInfo>(),
                       CreateDefaultObject.Create<NumberInfo>(),
                    }
                });
        
            var queryAvailableMsisdnRequestDTO = new QueryAvailableMsisdnRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CategoryId = 1,
                Quantity = fetchNumber,
            };
            var expectedAvailableMsisdns = new List<MSISDNResourceDTO>()
            {
                CreateDefaultObject.Create<NumberInfo>().ToDto(),
                CreateDefaultObject.Create<NumberInfo>().ToDto(),
            };

            var expectedqueryAvailableMsisdnResponseDTO = new QueryAvailableMsisdnResponseDTO
            {
                AvailableMsisdns = expectedAvailableMsisdns,
                resultType = ResultTypes.Ok,
                errorCode = 0,
                messages = new string[] { "Query Success" },
            };

            MockAbsctractBusinessOperation(queryAvailableMsisdnRequestDTO);

            var queryAvailableMsisdnResponseDTO = CallBizOp(queryAvailableMsisdnRequestDTO);

            AssertExt.ObjectPropertiesAreEqual(expectedqueryAvailableMsisdnResponseDTO, queryAvailableMsisdnResponseDTO);
        }

        [Test()]
        public void QueryAvailableMsisdnBizOp_DealerInfoNotFound_ShouldThrowDataValidationException()
        {
            var actualgetDealerInfosByFiscalUnitIdReq = Arg.Is<GetDealerInfosByFiscalUnitIdRequest>(x => x.FiscalUnitId == 1);
            var getDealerInfosByFiscalUnitIdMSMock = MockMicroService<GetDealerInfosByFiscalUnitIdRequest, GetDealerInfosByFiscalUnitIdResponse>();
            getDealerInfosByFiscalUnitIdMSMock.Process(actualgetDealerInfosByFiscalUnitIdReq,
                Arg.Any<RequestInvokationEnvironment>()).Returns((GetDealerInfosByFiscalUnitIdResponse)null);

            var queryAvailableMsisdnRequestDTO = new QueryAvailableMsisdnRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CategoryId = 1,
                Quantity = 10,
            };

            MockAbsctractBusinessOperation(queryAvailableMsisdnRequestDTO);

            var queryAvailableMsisdnResponseDTO = CallBizOp(queryAvailableMsisdnRequestDTO);
            Assert.AreEqual(ResultTypes.DataValidationError, queryAvailableMsisdnResponseDTO.resultType);
        }

        [Test()]
        public void QueryAvailableMsisdnBizOp_NoAvailableNumber_ShouldReturnEmpty()
        {
            //Mock GetDealerInfosByFiscalUnitIdMS
            var actualgetDealerInfosByFiscalUnitIdReq = Arg.Is<GetDealerInfosByFiscalUnitIdRequest>(x => x.FiscalUnitId == 1);
            var actualgetDealerInfosByFiscalUnitIdRes = new GetDealerInfosByFiscalUnitIdResponse()
            {
                DealerInfos = new List<DealerInfo>()
                {
                    CreateDefaultObject.Create<DealerInfo>()
                }
            };
            var getDealerInfosByFiscalUnitIdMSMock = MockMicroService<GetDealerInfosByFiscalUnitIdRequest, GetDealerInfosByFiscalUnitIdResponse>();
            getDealerInfosByFiscalUnitIdMSMock.Process(actualgetDealerInfosByFiscalUnitIdReq,
                Arg.Any<RequestInvokationEnvironment>()).Returns(actualgetDealerInfosByFiscalUnitIdRes);

            //Mock getNumberByCategoryAndVmoAndStatusIdInMS
            var statusList = new List<int> { (int)ResourceStatus.Init };
            var dealerIdList = actualgetDealerInfosByFiscalUnitIdRes.DealerInfos.Select(x => x.DealerID.Value);

            var actualGetNumberByCategoryAndVmoAndStatusIdInReq = Arg.Is<GetNumberByCategoryAndVmoAndStatusIdInRequest>(x => x.CategoryId == 1 && x.MaxElements == 10 && x.StatusId.SequenceEqual(statusList) && x.Vmo.SequenceEqual(dealerIdList));

            var getNumberByCategoryAndVmoAndStatusIdInMSMock =
                MockMicroService
                    <GetNumberByCategoryAndVmoAndStatusIdInRequest, GetNumberByCategoryAndVmoAndStatusIdInResponse>();
            var actualGetNumberByCategoryAndVmoAndStatusIdInRes = new GetNumberByCategoryAndVmoAndStatusIdInResponse()
            {
                NumberInfo = new List<NumberInfo> { }
            };
            getNumberByCategoryAndVmoAndStatusIdInMSMock.Process(actualGetNumberByCategoryAndVmoAndStatusIdInReq,
                Arg.Any<RequestInvokationEnvironment>()).Returns(actualGetNumberByCategoryAndVmoAndStatusIdInRes);

            var queryAvailableMsisdnRequestDTO = new QueryAvailableMsisdnRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CategoryId = 1,
                Quantity = 10,
            };

            MockAbsctractBusinessOperation(queryAvailableMsisdnRequestDTO);

            var queryAvailableMsisdnResponseDTO = CallBizOp(queryAvailableMsisdnRequestDTO);

            AssertExt.IsEmpty(queryAvailableMsisdnResponseDTO.AvailableMsisdns);
        }

        [Test()]
        public void QueryAvailableMsisdnBizOp_ShouldNotReturnTestResource()
        {
            //Mock GetDealerInfosByFiscalUnitIdMS
            var actualgetDealerInfosByFiscalUnitIdReq = Arg.Is<GetDealerInfosByFiscalUnitIdRequest>(x => x.FiscalUnitId == 1);
            var actualgetDealerInfosByFiscalUnitIdRes = new GetDealerInfosByFiscalUnitIdResponse()
            {
                DealerInfos = new List<DealerInfo>()
                {
                    CreateDefaultObject.Create<DealerInfo>()
                }
            };

            var getDealerInfosByFiscalUnitIdMSMock = MockMicroService<GetDealerInfosByFiscalUnitIdRequest, GetDealerInfosByFiscalUnitIdResponse>();
            getDealerInfosByFiscalUnitIdMSMock.Process(actualgetDealerInfosByFiscalUnitIdReq,
                Arg.Any<RequestInvokationEnvironment>()).Returns(actualgetDealerInfosByFiscalUnitIdRes);

            //Mock getNumberByCategoryAndVmoAndStatusIdInMS
            var statusList = new List<int> { (int)ResourceStatus.Init };
            var dealerIdList = actualgetDealerInfosByFiscalUnitIdRes.DealerInfos.Select(x => x.DealerID.Value);

            var actualGetNumberByCategoryAndVmoAndStatusIdInReq = Arg.Is<GetNumberByCategoryAndVmoAndStatusIdInRequest>(x => x.CategoryId == 1 && x.MaxElements == 10 && x.StatusId.SequenceEqual(statusList) && x.Vmo.SequenceEqual(dealerIdList));

            var getNumberByCategoryAndVmoAndStatusIdInMSMock =
                MockMicroService
                    <GetNumberByCategoryAndVmoAndStatusIdInRequest, GetNumberByCategoryAndVmoAndStatusIdInResponse>();
            var actualGetNumberByCategoryAndVmoAndStatusIdInRes = new GetNumberByCategoryAndVmoAndStatusIdInResponse()
            {
                NumberInfo = new List<NumberInfo> { CreateDefaultObject.Create<NumberInfo>() }
            };
            getNumberByCategoryAndVmoAndStatusIdInMSMock.Process(actualGetNumberByCategoryAndVmoAndStatusIdInReq,
                Arg.Any<RequestInvokationEnvironment>()).Returns(actualGetNumberByCategoryAndVmoAndStatusIdInRes);


            var getMvnoConfigActionMSMock = MockMicroService<GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest,
                        GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResponse>();


            var getMvnoConfigActionReqest = new GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest()
            {
                CategoryId = 0,
                MvnoId = 190000,
                Item = "Filter_Msisdns_Range",
                StatusId = 1
            };
            var getMvnoConfigActionResponse = new GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResponse()
            {
                ResultType = ResultTypes.Ok,
                MvnoConfigActionInfos = new List<MVNOConfigActionInfo>()
                {
                    new MVNOConfigActionInfo(){Value = "34603051220-34603051222;34603051275;"}
                }
            };
            getMvnoConfigActionMSMock.Process(getMvnoConfigActionReqest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(getMvnoConfigActionResponse);

            var getNumberByCategoryAndVmoAndStatusIdInMsMock =
                  MockMicroService<GetNumberByCategoryAndVmoAndStatusIdInRequest, GetNumberByCategoryAndVmoAndStatusIdInResponse>();
            var getNumberByCategoryAndVmoAndStatusIdInRequest = new GetNumberByCategoryAndVmoAndStatusIdInRequest()
            {

            };
            getNumberByCategoryAndVmoAndStatusIdInMsMock.Process(getNumberByCategoryAndVmoAndStatusIdInRequest, Arg.Any<RequestInvokationEnvironment>())
                .ReturnsForAnyArgs(new GetNumberByCategoryAndVmoAndStatusIdInResponse()
                {
                    NumberInfo = new List<NumberInfo>()
                    {
                        new NumberInfo(){Resource = "34603051218"},
                        new NumberInfo(){Resource = "34603051219"},
                        new NumberInfo(){Resource = "34603051220"},
                        new NumberInfo(){Resource = "34603051221"},
                        new NumberInfo(){Resource = "34603051222"},
                        new NumberInfo(){Resource = "34603051270"},
                        new NumberInfo(){Resource = "34603051271"},
                        new NumberInfo(){Resource = "34603051272"},
                        new NumberInfo(){Resource = "34603051273"},
                        new NumberInfo(){Resource = "34603051274"},
                        new NumberInfo(){Resource = "34603051275"},
                        new NumberInfo(){Resource = "34603051276"},
                        new NumberInfo(){Resource = "34603051277"},
                        new NumberInfo(){Resource = "34603051278"},
                    }
                });
            int fetchNumber = 10;
            var queryAvailableMsisdnRequestDTO = new QueryAvailableMsisdnRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CategoryId = 1,
                Quantity = fetchNumber,
            };
            var expectedAvailableMsisdns = new List<MSISDNResourceDTO>()
            {
              new  MSISDNResourceDTO(){MSISDN="34603051218",Category=null},
              new  MSISDNResourceDTO(){MSISDN="34603051219",Category=null},
              new  MSISDNResourceDTO(){MSISDN="34603051270",Category=null},
              new  MSISDNResourceDTO(){MSISDN="34603051271",Category=null},
              new  MSISDNResourceDTO(){MSISDN="34603051272",Category=null},
              new  MSISDNResourceDTO(){MSISDN="34603051273",Category=null},
              new  MSISDNResourceDTO(){MSISDN="34603051274",Category=null},
              new  MSISDNResourceDTO(){MSISDN="34603051276",Category=null},
              new  MSISDNResourceDTO(){MSISDN="34603051277",Category=null},
              new  MSISDNResourceDTO(){MSISDN="34603051278",Category=null},
            };

            var expectedqueryAvailableMsisdnResponseDTO = new QueryAvailableMsisdnResponseDTO
            {
                AvailableMsisdns = expectedAvailableMsisdns,
                resultType = ResultTypes.Ok,
                errorCode = 0,
                messages = new string[] { "Query Success" },
            };

            MockAbsctractBusinessOperation(queryAvailableMsisdnRequestDTO);

            var queryAvailableMsisdnResponseDTO = CallBizOp(queryAvailableMsisdnRequestDTO);

            AssertExt.ObjectPropertiesAreEqual(expectedqueryAvailableMsisdnResponseDTO, queryAvailableMsisdnResponseDTO);
        }
    }
}