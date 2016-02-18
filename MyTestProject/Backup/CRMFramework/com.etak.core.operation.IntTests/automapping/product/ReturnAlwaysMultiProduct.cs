using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using com.etak.core.operation.implementation;
using com.etak.core.test.utilities;

namespace com.etak.core.operation.IntTests.automapping.product
{
    public class ReturnAlwaysMultiProduct : AbstractBusinessOperation<RequestBaseDTO, MultiProductOfferingDTOBasedResponse, DummyRequest, MultiProductOfferingBasedResponse>
    {

        public override string OperationCode
        {
            get { return "RAM"; }
        }

        public override string OperationDiscriminator
        {
            get { return "TRAM"; }
        }

        protected override void MapNotAutomappedOutboundProperties(MultiProductOfferingBasedResponse coreOutput, ref MultiProductOfferingDTOBasedResponse dtoOutput)
        {
        }

        protected override MultiProductOfferingBasedResponse ProcessBusinessLogic(DummyRequest request, model.operation.BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            #region Set all the properties of the product
            var proInfo = CreateDefaultObject.Create<ProductOffering>();

            proInfo.Names = new MultiLingualDescription()
            {
                Texts = new List<LanguageSpecificText>()
                {
                    CreateDefaultObject.Create<LanguageSpecificText>()
                }
            };
            proInfo.Description = new MultiLingualDescription()
            {
                Texts = new List<LanguageSpecificText>()
                {
                    CreateDefaultObject.Create<LanguageSpecificText>()
                }
            };

            var chargingOption = new ProductChargeOption()
            {
                CreateDate = CreateDefaultObject.Create<DateTime>(),
                EndDate = CreateDefaultObject.Create<DateTime>(),
                Id = CreateDefaultObject.Create<int>(),
                IsDefaultOption = DefaultOptions.Y,
                ProductOffering = CreateDefaultObject.Create<ProductOffering>(),
                StartDate = CreateDefaultObject.Create<DateTime>(),
                Name = new MultiLingualDescription(),
            };
            chargingOption.Name = new MultiLingualDescription()
            {
                Texts = new List<LanguageSpecificText>()
                {
                    CreateDefaultObject.Create<LanguageSpecificText>()
                }
            };
            chargingOption.Description = new MultiLingualDescription()
            {
                Texts = new List<LanguageSpecificText>()
                {
                    CreateDefaultObject.Create<LanguageSpecificText>()
                }
            };
            var chargeInfo = CreateDefaultObject.Create<ChargeRecurring>();
            chargeInfo.Name = new MultiLingualDescription()
            {
                Texts = new List<LanguageSpecificText>()
                {
                    new LanguageSpecificText()
                    {
                        Text = "text1",
                        Language = ISO639LanguageCodes.eng,
                    }
                }
            };
            chargeInfo.Description = new MultiLingualDescription()
            {
                Texts = new List<LanguageSpecificText>()
                {
                    new LanguageSpecificText()
                    {
                        Text = "text1",
                        Language = ISO639LanguageCodes.eng,
                    }   
                }
            };
            chargingOption.Charges = new List<Charge>() { chargeInfo };
            proInfo.ChargingOptions = new List<ProductChargeOption>() { chargingOption };
            #endregion
            return new MultiProductOfferingBasedResponse()
            {
                ProductOfferings = new List<ProductOffering>() { proInfo },
                ErrorCode = 0,
                Message = string.Empty,
                ResultType = ResultTypes.Ok,
            };
        }

        protected override void MapNotAutomappedInboundProperties(RequestBaseDTO dtoRequest, ref DummyRequest coreInput)
        {
        }
    }
}
