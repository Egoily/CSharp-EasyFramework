using System;

namespace com.etak.core.app.BenifitsRenewalEngine.DTO
{
    public class RenewalPosition
    {
        public long PromotionId { get; set; }
        public int TryTimes { get; set; }
        public DateTime? preResetDateTime { get; set; }
        //public DateTime DateToReset { get; set; }
    }
}
