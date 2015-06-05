using System;
using com.etak.core.microservices.messages.GetHolidayInfoByDate;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;
using com.etak.core.repository;
using com.etak.core.repository.crm;

namespace com.etak.core.microservices.microservices
{
    /// <summary>
    /// GetHolidayInfoByDate Microservice
    /// </summary>
    public class GetHolidayInfoByDateMS : IMicroService<GetHolidayInfoByDateRequest, GetHolidayInfoByDateResponse>
    {
        /// <summary>
        /// Main process for the microservice
        /// </summary>
        /// <param name="request">Request with a DateTime object to be used to get the holidayInfo</param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public GetHolidayInfoByDateResponse Process(GetHolidayInfoByDateRequest request, operation.RequestInvokationEnvironment invoker)
        {
            var holidayId = Int32.Parse(request.Date.ToString("yyyyMMdd"));

            var repoHoliday = RepositoryManager.GetRepository<IHolidayInfoRepository<HolidayInfo>>();
            var holidayInfo = repoHoliday.GetById(holidayId);

            var response = new GetHolidayInfoByDateResponse()
            {
                HolidayInfo = holidayInfo,
                ResultType = ResultTypes.Ok,
            };

            return response;
        }
    }
}
