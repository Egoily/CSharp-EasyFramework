using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.customer.message.GetTTQuestionInfoByTypeSubTypeAndMvnoid;
using com.etak.core.customer.message.CreateTroubleTicketInfo;
using com.etak.core.repository;
using com.etak.core.repository.crm;

namespace com.etak.core.bizops.fullfilment.CreateTroubleTicket
{
    /// <summary>
    /// BizOp to create trouble ticket
    /// </summary>
    public class CreateTroubleTicketBizOp:AbstractSinglePhaseOrderProcessor<CreateTroubleTicketRequestDTO,CreateTroubleTicketResponseDTO,CreateTroubleTicketRequestInternal,CreateTroubleTicketResponseInternal,CreateTroubleTicketOrder>
    {
        /// <summary>
        /// Mapping not Automapped Inbound Properties
        /// </summary>
        /// <param name="request">CreateTroubleTicketRequestDTO</param>
        /// <param name="coreInput">CreateTroubleTicketRequestInternal</param>
        protected override void MapNotAutomappedOrderInboundProperties(CreateTroubleTicketRequestDTO request, ref CreateTroubleTicketRequestInternal coreInput)
        {
            if (coreInput.Subscription.CustomerInfo == null)
                throw new BusinessLogicErrorException("Customer is not exist in system", BizOpsErrors.CustomerIsNull);

            var getTTCodeMS = MicroServiceManager.GetMicroService<GetTTQuestionInfoByTypeSubTypeAndMvnoidRequest, GetTTQuestionInfoByTypeSubTypeAndMvnoidResponse>();
            var getTTCodeRequest = new GetTTQuestionInfoByTypeSubTypeAndMvnoidRequest()
            {
                Type = request.type,
                SubType = request.subtype
            };

            var getTTCodeResponse = getTTCodeMS.Process(getTTCodeRequest, null);

            if (getTTCodeResponse.TroubleTicketQuestionInfos.IsEmpty())
                throw new BusinessLogicErrorException("Can not get trouble ticket question info", BizOpsErrors.TroubleTicketQuestionInfoIsNull);

            var getTTNumberSequenceProvider = RepositoryManager.GetRepository<ISequenceProvider>();
            var getTTNumber = getTTNumberSequenceProvider.GetNextSequence("CRM_TROUBLE_TICKET.TICKETNUMBER").ToString();

            //TODO : if we have a TT DTO model then we need to update this part
            coreInput.TTInfo = new TroubleTicketInfo();
            coreInput.TTInfo.CALLTIME = DateTime.Now;
            coreInput.TTInfo.CLASSID = (int)eFuente.Individual;
            coreInput.TTInfo.CUSTOMERID = coreInput.Subscription.CustomerInfo.CustomerID.Value;
            coreInput.TTInfo.CustomerName = coreInput.Subscription.CustomerInfo.FirstName + " " + coreInput.Subscription.CustomerInfo.LastName;
            coreInput.TTInfo.CUSTTYPE = coreInput.Subscription.CustomerInfo.PropertyInfo.FirstOrDefault().PaymentMethodID;
            coreInput.TTInfo.CLIENTID = coreInput.Subscription.CustomerInfo.PropertyInfo.FirstOrDefault().IDNumber;
            coreInput.TTInfo.DESCRIPTION = request.troubleTicketDescription;
            coreInput.TTInfo.MSISDN = coreInput.Subscription.Resource;
            if (request.priority.HasValue) coreInput.TTInfo.PRIORITY = request.priority.Value;
            coreInput.TTInfo.OPERATORID = coreInput.MVNO.UserID;
            coreInput.TTInfo.REPORTTIME = DateTime.Now;
            coreInput.TTInfo.UPDATETIME = DateTime.Now;
            coreInput.TTInfo.EXTERNALCODE = coreInput.ExternalReference;
            coreInput.TTInfo.STATUS = (int)eAtosTTStates.CREADO;
            coreInput.TTInfo.CALLPLACE = string.Empty;
            coreInput.TTInfo.NETWORK = string.Empty;
            coreInput.TTInfo.DIALEDNO = string.Empty;
            coreInput.TTInfo.DEPTID = 0;
            coreInput.TTInfo.HANDLEBY = 0;

            var TTCode = getTTCodeResponse.TroubleTicketQuestionInfos.ToList().FirstOrDefault();
            coreInput.TTInfo.TTCODE = TTCode.TTSUBTYPE;
            coreInput.TTInfo.TICKETNUMBER = getTTNumber;
            

        }

        /// <summary>
        /// Mapping not Automapped Outbound Properties
        /// </summary>
        /// <param name="source">CreateTroubleTicketResponseInternal</param>
        /// <param name="DTOOutput">CreateTroubleTicketResponseDTO</param>
        protected override void MapNotAutomappedOrderOutboundProperties(CreateTroubleTicketResponseInternal source, ref CreateTroubleTicketResponseDTO DTOOutput)
        {
            if (source.TroubleTicket != null)
                DTOOutput.TroubleTicket = source.TroubleTicket;
            DTOOutput.resultType = source.ResultType;
        }

        /// <summary>
        /// Process logic of CreateTroubleTicketResponseInternal
        /// </summary>
        /// <param name="order"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public override CreateTroubleTicketResponseInternal ProcessRequest(CreateTroubleTicketOrder order, CreateTroubleTicketRequestInternal request)
        {
            var createTroubleTicketMS = MicroServiceManager.GetMicroService<CreateTroubleTicketInfoRequest, CreateTroubleTicketInfoResponse>();
            var createTroubleTicketInfoRequest = new CreateTroubleTicketInfoRequest()
            {
                TroubleTicketInfo = request.TTInfo
            };

            var createTroubleTicketInfoResponse = createTroubleTicketMS.Process(createTroubleTicketInfoRequest, null);

            if (createTroubleTicketInfoResponse.TroubleTicketInfo == null || createTroubleTicketInfoResponse.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException("Cannot create Trouble Ticket", BizOpsErrors.TroubleTicketInfoIsNull);

            var response = new CreateTroubleTicketResponseInternal()
            {
                TroubleTicket = createTroubleTicketInfoResponse.TroubleTicketInfo,
                ResultType = createTroubleTicketInfoResponse.ResultType
            };

            return response;

            
        }

        /// <summary>
        /// Discriminator Code for CreateTroubleTicketBizOp
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.CreateTroubleTicket; }
        }

        /// <summary>
        /// Operation Code for CreateTroubleTicketBizOp
        /// </summary>
        public override string OperationCode {
            get { return OperationCodes.CreateTroubleTicket; }
        }
    }
}
