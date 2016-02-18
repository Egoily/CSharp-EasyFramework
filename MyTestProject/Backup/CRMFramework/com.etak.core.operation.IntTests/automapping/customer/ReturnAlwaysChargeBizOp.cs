using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.implementation;
using com.etak.core.test.utilities;

namespace com.etak.core.operation.IntTests.automapping.customer
{
    public class ReturnAlwaysChargeBizOp : AbstractBusinessOperation<RequestBaseDTO, ChargeBasedResponseDTO, ChargeBasedRequest, ChargeBasedResponse>
    {
        public override string OperationCode
        {
            get { return "RACB"; }
        }

        public override string OperationDiscriminator
        {
            get { return "TRACB"; }
        }

        protected override void MapNotAutomappedInboundProperties(RequestBaseDTO dtoRequest, ref ChargeBasedRequest coreInput)
        {
            
        }

        protected override void MapNotAutomappedOutboundProperties(ChargeBasedResponse coreOutput, ref ChargeBasedResponseDTO dtoOutput)
        {
            
        }

        protected override ChargeBasedResponse ProcessBusinessLogic(ChargeBasedRequest request, model.operation.BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            #region Create Object
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
            #endregion
            return new ChargeBasedResponse()
            {
                Charge = chargeInfo,
                ResultType = ResultTypes.Ok,
                ErrorCode = 0,
                Message = string.Empty,
            };
        }
    }
}
