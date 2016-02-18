using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class HolidayInfo
    {

        private int inDate;
        private byte isHoliday;
        private string holidayDescription;

        public HolidayInfo()
        {

        }

        public HolidayInfo(int inDate, byte isHoliday, string description)
        {
            this.inDate = inDate;
            this.isHoliday = isHoliday;
            this.holidayDescription = description;
        }


        public virtual int InDate
        {
            get { return inDate; }
            set { inDate = value; }
        }

        public virtual byte IsHoliday
        {
            get { return isHoliday; }
            set { isHoliday = value; }
        }

        public virtual string HolidayDescription
        {
            get { return holidayDescription; }
            set { holidayDescription = value; }
        }
    }
}
