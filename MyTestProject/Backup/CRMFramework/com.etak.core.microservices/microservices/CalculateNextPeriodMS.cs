using System;
using com.etak.core.microservices.messages.CalculateNextPeriod;
using com.etak.core.model;
using com.etak.core.operation.contract;


namespace com.etak.core.microservices.microservices
{
    /// <summary>
    /// Caculates the next NextPeriodNumber, CurrentCycleNumber and Next date for a standart ET scheduling
    /// </summary>
    public class CalculateNextPeriodMS : IMicroService<CalculateNextPeriodRequest, CalculateNextPeriodResponse>
    {
        /// <summary>
        /// Caculates the next NextPeriodNumber, CurrentCycleNumber and Next date for a standart ET scheduling
        /// </summary>
        /// <param name="request">the input with the catalog definition of the periodicuty and the instance to calculate</param>
        /// <param name="invoker">the common environment</param>
        /// <returns>NextPeriodNumber, CurrentCycleNumber and Next date for a standart ET scheduling</returns>
        public CalculateNextPeriodResponse Process(CalculateNextPeriodRequest request, operation.RequestInvokationEnvironment invoker)
        {
            CalculateNextPeriodResponse resp = new CalculateNextPeriodResponse
            {
                PeriodMatched = false,
                NextDate = request.NextDate,
                NextPeriodNumber = request.NextPeriodNumber,
                CurrentCycleNumber = request.CurrentCycleNumber,
            };
            // CycleRepeatCount = -1 means no limit.
            if (request.CurrentCycleNumber > request.CycleRepeatCount && request.CycleRepeatCount != -1)
                return resp;

            DateTime nextDate = request.NextDate;
            switch (request.PeriodUnit)
            {
                case TimeUnits.Year: nextDate = nextDate.AddYears(1); break;
                case TimeUnits.Month: nextDate = nextDate.AddMonths(1); break;
                case TimeUnits.Week: nextDate = nextDate.AddDays(7); break;
                case TimeUnits.Day: nextDate = nextDate.AddDays(1); break;
                case TimeUnits.Hour: nextDate = nextDate.AddHours(1); break;
                case TimeUnits.Minute: nextDate = nextDate.AddMinutes(1); break;
                default: throw new ArgumentException("Unknown time unit:" + request.PeriodUnit);
            }

            resp.NextDate = nextDate;

            if (request.NextPeriodNumber == request.Periodicity)
            {
                resp.NextPeriodNumber = 1;
                resp.CurrentCycleNumber = request.CurrentCycleNumber + 1;
            }
            else
            {
                resp.NextPeriodNumber = request.NextPeriodNumber + 1;
            }

            if (resp.NextPeriodNumber >= request.StartPeriodNumber &&
                resp.NextPeriodNumber < request.EndPeriodNumber &&
                request.NextPeriodNumber % request.PeriodCount == 0)
                resp.PeriodMatched = true;

            return resp;
        }
    }
}
