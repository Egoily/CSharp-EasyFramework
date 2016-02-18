using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.app.BenifitsRenewalEngine.Actions.Utilty
{
    public class UtiltyHelper
    {
        public static DateTime GetOldBillRunStartDate(RmPromotionPlanDetailInfo rmPpdInfo, DateTime renewDate)
        {
            switch (rmPpdInfo.PeriodUnit)
            {
                case TimeUnits.Day:
                    return renewDate.AddDays(0 - rmPpdInfo.PeriodCount);
                case TimeUnits.Month:
                    return renewDate.AddMonths(0 - rmPpdInfo.PeriodCount);
                case TimeUnits.Week:
                    return renewDate.AddDays(7*(0 - rmPpdInfo.PeriodCount));
                case TimeUnits.Year:
                    return renewDate.AddYears(0 - rmPpdInfo.PeriodCount);
                default:
                    return renewDate;
            }
        }

        public static DateTime GetNewBillRunEndDate(RmPromotionPlanDetailInfo rmPpdInfo, DateTime renewDate)
        {
            switch (rmPpdInfo.PeriodUnit)
            {
                case TimeUnits.Day:
                    return renewDate.AddDays(rmPpdInfo.PeriodCount).AddSeconds(-1);
                case TimeUnits.Month:
                    return renewDate.AddMonths(rmPpdInfo.PeriodCount).AddSeconds(-1);
                case TimeUnits.Week:
                    return renewDate.AddDays(7 * rmPpdInfo.PeriodCount).AddSeconds(-1);
                case TimeUnits.Year:
                    return renewDate.AddYears(rmPpdInfo.PeriodCount).AddSeconds(-1);
                default:
                    return renewDate;
            }
        }

        public static List<int> GetCycleNumberAndNextPeriodNumber(int currentCycleNumber, int currentPeriodNumber, RmPromotionPlanDetailInfo rmPpdInfo)
        {
            int iCycleNumber = 0;
            int iNextPeriodNumber = 0;

            int incCycle = (currentPeriodNumber + 1) / rmPpdInfo.Periodicity;
            iNextPeriodNumber = (currentPeriodNumber + 1) % rmPpdInfo.Periodicity;
            iCycleNumber = incCycle + currentCycleNumber;

            if (iCycleNumber > rmPpdInfo.CycleRepeatCount)
                return null;

            return new List<int>() { iCycleNumber, iNextPeriodNumber };
        }
    }
}
