using com.etak.core.microservices.messages.GetHolidayInfoById;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;
using com.etak.core.repository;
using com.etak.core.repository.crm;

namespace com.etak.core.microservices.microservices
{
    /// <summary>
    /// Microservice to get a HolidayInfo by Id
    /// </summary>
    public class GetHolidayInfoByIdMS : IMicroService<GetHolidayInfoByIdRequest, GetHolidayInfoByIdResponse>
    {
        /// <summary>
        /// Main process for the microservice
        /// </summary>
        /// <param name="request">Request with an integer value to be used as a ID</param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public GetHolidayInfoByIdResponse Process(GetHolidayInfoByIdRequest request, operation.RequestInvokationEnvironment invoker)
        {
            var repoHoliday = RepositoryManager.GetRepository<IHolidayInfoRepository<HolidayInfo>>();
            var holidayInfo = repoHoliday.GetById(request.HolidayId);

            var response = new GetHolidayInfoByIdResponse()
            {
                HolidayInfo = holidayInfo,
                ResultType = ResultTypes.Ok,
            };

            return response;
        }
    }
}
