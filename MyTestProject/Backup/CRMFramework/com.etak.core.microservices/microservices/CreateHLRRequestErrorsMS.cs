using System;
using com.etak.core.microservices.messages.CreateHLRRequestErrors;
using com.etak.core.model.operation.messages;
using com.etak.core.model.provisioning;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.repository;
using com.etak.core.repository.crm;
using Network3GPPModel;

namespace com.etak.core.microservices.microservices
{
    /// <summary>
    /// Microservice to CreateHLRRequestErrors
    /// </summary>
    public class CreateHLRRequestErrorsMS : IMicroService<CreateHLRRequestErrorsRequest, CreateHLRRequestErrorsResponse>
    {
        /// <summary>
        /// Main process of CreateHLRRequestErrorsMS
        /// </summary>
        /// <param name="request"></param>
        /// <param name="invoker"></param>
        /// <returns>return HLRRequestErrors within CreateHLRRequestErrorsResponse </returns>
        public CreateHLRRequestErrorsResponse Process(CreateHLRRequestErrorsRequest request, RequestInvokationEnvironment invoker)
        {
            var repoHLRReqErrors = RepositoryManager.GetRepository<IHLRRequestErrorsRepository<HLRRequestErrors>>();

            var result = repoHLRReqErrors.Create(request.HLRRequestErrorsObj);
            var HLRErrorCode = Convert.ToInt32(result.ERRORCODE);

            return new CreateHLRRequestErrorsResponse()
            {
                HLRRequestErrorsObj = result,
                ResultType = HLRErrorCode.Equals((int)HLRErrorCodes.Ok) ? ResultTypes.Ok : ResultTypes.BussinessLogicError
            };
        }

        /// <summary>
        /// convert from array to string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns>string</returns>
        /// in the future this should be move to common module 
        private static string ArrayToString<T>(T[] array)
        {
            string str = "";
            if (!array.Equals(null))
            {
                foreach (var elmt in array)
                {
                    str += elmt.ToString();
                }
            }
            return str;
        }
    }
}
