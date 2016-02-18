using System;

namespace com.etak.core.model
{
    [Serializable]
    public enum AdjustmentDirection
    {
        Positive = 1,
        Negative = 2
    }
    [Serializable]
    public enum RunTaskUnit
    {
        Unkown=0,
        PerDay = 1,
        PerWeek = 2,
        PerMonth = 3,
        PerMin = 4,
        PerHour = 5
    }

    [Serializable]
    public enum Week
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7
    }

    [Serializable]
    public enum DurationFullUnit
    {
        Days = 1,
        Weeks = 2,
        Months = 3,
        Years = 4
    }
    [Serializable]
    public enum DurationDayWeekMonthUnit
    {
        Days = 1,
        Weeks = 2,
        Months = 3
    }
    [Serializable]
    public enum DurationDayWeekUnit
    {
        Days = 1,
        Weeks = 2
    }
    [Serializable]
    public enum ExtendSettingCategory : int
    {
        LifecycleExtendMonth = 100,
        LifecycleReportEmail = 101,
		BonusExtendMonth = 200 ,
        NotifyInfo=300,
        LifecycleMaxMonth = 102,
        LifecycleOnlyFrozen = 103
    }
}
