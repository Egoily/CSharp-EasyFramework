using com.etak.core.model.operation;

namespace com.etak.core.bizops.assurance.ApplyChargeAndSchedule
{
    /// <summary>
    /// Order corresponding to ApplyChargeAndSchedule
    /// </summary>
    public class ApplyChargeAndScheduleOrder : Order
    {
        /// <summary>
        /// The discriminator for BizOp ApplyChargeAndSchedule
        /// </summary>
        public override string Discriminator {
            get { return OrderDiscriminators.ApplyChargeAndSchedule; }
        }


    }
}
