using com.etak.core.microservices.messages.GetSettingInfosByDealerId;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;
using com.etak.core.repository;
using com.etak.core.repository.crm;

namespace com.etak.core.microservices.microservices
{
    /// <summary>
    /// MS : Get SettingInfo by using DealerId
    /// </summary>
    public class GetSettingInfosByDealerIdMS : IMicroService<GetSettingInfosByDealerIdRequest,GetSettingInfosByDealerIdResponse>
    {
        /// <summary>
        /// Main process to get IEnumerable(SettingInfo)
        /// </summary>
        /// <param name="request">GetSettingInfosByDealerIdRequest with DealerId</param>
        /// <param name="invoker">RequestInvokationEnvironment</param>
        /// <returns>SettingInfos with certain DealerId</returns>
        public GetSettingInfosByDealerIdResponse Process(GetSettingInfosByDealerIdRequest request, core.operation.RequestInvokationEnvironment invoker)
        {
            var repoSettingInfo = RepositoryManager.GetRepository<ISettingInfoRepository<SettingInfo>>();

            var settingInfos = repoSettingInfo.GetSettingInfoWithDetailByDealerId(request.DealerId);

            return new GetSettingInfosByDealerIdResponse()
            {
                SettingInfos = settingInfos,
                ErrorCode = 0,
                Message = string.Empty,
                ResultType = ResultTypes.Ok
            };
        }
    }
}
