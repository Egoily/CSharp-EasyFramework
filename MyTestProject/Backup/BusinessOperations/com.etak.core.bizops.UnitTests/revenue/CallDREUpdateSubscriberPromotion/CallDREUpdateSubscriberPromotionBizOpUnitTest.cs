using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using com.etak.core.bizops.helper;
using com.etak.core.bizops.revenue.BenefitTransfer;
using com.etak.core.bizops.revenue.CallDREUpdateSubscriberPromotion;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;
using com.etak.core.repository;
using com.etak.core.repository.crm.configuration;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace com.etak.core.bizops.UnitTests.revenue.CallDREQuerySubscriberPromotion
{

    [TestFixture]
    public class CallDREUpdateSubscriberPromotionBizOpUnitTest : AbstractSinglePhaseOrderProcessorTest<CallDREUpdateSubscriberPromotionBizOp,
        CallDREUpdateSubscriberPromotionRequestDTO, CallDREUpdateSubscriberPromotionResponseDTO, CallDREUpdateSubscriberPromotionRequestInternal, CallDREUpdateSubscriberPromotionResponseInternal, CallDREUpdateSubscriberPromotionOrder>
    {

        private IHttpWebRequestHelper mockhttWebRequestHelper = null;


        public void StandardMSMocks()
        {

            #region Mock OperationConfig
            var mockedRepoConfig = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var operationConfiguration = CreateDefaultObject.Create<OperationConfiguration>();
            var benefitConfig = new BenefitTransferConfiguration()
            {
                BenefitSourceTransferProductId = 2,
                BenefitDestinationTransferProductId = 1,
                ValidSourcePromotions = new List<int> { 1, 2, 3, 4 },
                BenefitTransferSenderLimit = 12,
                MaxTransferDestinationLimit = 1,
                BenefitTransferReceiverLimit = 10,
                TotalBenefitLimit = 14
            };
            operationConfiguration.JSonConfig = JsonConvert.SerializeObject(benefitConfig);
            operationConfiguration.StarTime = DateTime.Now;
            operationConfiguration.EndDate = DateTime.Now.AddYears(1);
            mockedRepoConfig.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(new List<OperationConfiguration>() { operationConfiguration });

            #endregion


            #region Mock HttpWebRequest

            mockhttWebRequestHelper = NSubstitute.Substitute.For<IHttpWebRequestHelper>();

            #endregion
        }


        [TestFixtureSetUp]
        public static void Initialize()
        {
            FakeSessionFactorySingleton.Init();
        }

        [Test()]
        public void CallDREUpdateSubscriberPromotionBizOp_CorrectRequestGiven()
        {
            StandardMSMocks();

            #region Setup mocks

            mockhttWebRequestHelper.GetResponse(Arg.Any<string>(), Arg.Any<int?>(), Arg.Any<byte[]>())
                .Returns(Serialize<UpdateSubscriberPromotionRes>(new UpdateSubscriberPromotionRes()
                {
                    ErrorCode = 0,
                    PromotionId = "999"
                }));


            #endregion


            var updateSubscriberPromotionRequestDTO = new CallDREUpdateSubscriberPromotionRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                Msisdn = "34611470079",
                PromotionId = 1673000000000177225,
                ServerURL = "http://10.4.12.9:8080/",
                DecrementValue = 1,
                IncrementValue = 0,
            };

            MockAbstractSinglePhaseOrderProcessor(updateSubscriberPromotionRequestDTO);

            #region Customized CallBizOp

            var response = new CallDREUpdateSubscriberPromotionResponseDTO();
            using (var conn = RepositoryManager.GetNewConnection())
            {
                CallDREUpdateSubscriberPromotionBizOp bizop = new CallDREUpdateSubscriberPromotionBizOp();
                bizop.HttpWebRequestHelper = mockhttWebRequestHelper;

                response = bizop.ProcessFromCustomerModel(new NullValidator<CallDREUpdateSubscriberPromotionRequestDTO>(),
                    new SameTypeConverter<CallDREUpdateSubscriberPromotionRequestDTO>(),
                    new SameTypeConverter<CallDREUpdateSubscriberPromotionResponseDTO>(), updateSubscriberPromotionRequestDTO, FakeInvoker);

            }
            RepositoryManager.CloseConnection();

            // var updateSubscriberPromotionResponseDTO = CallBizOp(updateSubscriberPromotionRequestDTO);

            #endregion



            Assert.AreEqual(response.errorCode, 0);
        }



        public byte[] Serialize<T1>(T1 toSerialize)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T1));

            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializerNamespaces customNamespace = new XmlSerializerNamespaces();
                customNamespace.Add("", "");

                XmlWriterSettings setting = new XmlWriterSettings();
                setting.Encoding = new UTF8Encoding(false);
                setting.Indent = false;

                using (XmlWriter writer = XmlWriter.Create(stream, setting))
                {
                    serializer.Serialize(writer, toSerialize, customNamespace);
                }

                return stream.ToArray();
            }
        }

    }
}
