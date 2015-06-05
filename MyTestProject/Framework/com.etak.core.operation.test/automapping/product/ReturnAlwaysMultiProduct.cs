using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.implementation;
using com.etak.core.test.utilities;

namespace com.etak.core.operation.test.automapping.product
{
    public class ReturnAlwaysMultiProduct<TRequest> : AbstractBusinessOperation<RequestBaseDTO, MultiProductDTOBasedResponse, TRequest, MultiProductBasedResponse>
        where TRequest : RequestBase, new ()
    {

        public override string OperationCode
        {
            get { return "RAM"; }
        }

        public override string OperationDiscriminator
        {
            get { return "TRAM"; }
        }

        protected override void MapNotAutomappedInboundProperties(RequestBaseDTO dtoRequest, ref TRequest coreInput)
        {
        }

        protected override void MapNotAutomappedOutboundProperties(MultiProductBasedResponse coreOutput, ref MultiProductDTOBasedResponse dtoOutput)
        {
        }

        protected override MultiProductBasedResponse ProcessBusinessLogic(TRequest request, model.operation.BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            #region Set all the properties of the product
            var proInfo = CreateDefaultObject.Create<Product>();


            proInfo.ChildProducts = new List<Product>()
            {
                CreateDefaultObject.Create<Product>()
            };
            proInfo.ParentProducts = new List<Product>()
            {
                CreateDefaultObject.Create<Product>()
            };
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
                ProductOfOption = CreateDefaultObject.Create<Product>(),
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
            return new MultiProductBasedResponse()
            {
                Products = new List<Product>() {proInfo},
                ErrorCode = 0,
                Message = string.Empty,
                ResultType = ResultTypes.Ok,
            };
        }
    }
}
