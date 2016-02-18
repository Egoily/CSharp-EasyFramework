using System;

namespace com.etak.core.model.revenueManagement
{  

    /*
     *        CREATE TABLE RM_CHARGE_TARGETS
       ( 
             TARGETID               INT NOT NULL,
             CHARGEID               INT NOT NULL ,
             COMPUTATION_ORDER      INT NOT NULL ,
             TARGET_CHARGEID        INT NULL ,
             TARGET_CHARGE_CATEGORYID INT NULL 
       )*/

    /// <summary>
    /// Class to express that a charge should be applied over a charge
    /// </summary>
    public class ChargeTarget 
    {
        /// <summary>
        /// Unique Id of the entity
        /// </summary>
        public virtual Int32 Id { get; set; }

        /// <summary>
        /// The charge that has targets
        /// </summary>
        public virtual Charge OwnerCharge { get; set; }

        /// <summary>
        /// The target charge
        /// </summary>
        public virtual Charge TargetCharge { get; set; }

        /// <summary>
        /// The order in which the charges needs to be aggregated. 
        /// </summary>
        public virtual Int32 ComputationOrder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Int32? TargetCategory { get; set; }
    }
}
