using System;

namespace com.etak.core.model.revenueManagement
{
    [Serializable]
    public enum ChargeStatus : byte
    {
        /// <summary>
        /// Only defined for test purposes and should not visible/usable through normal processes/API.
        /// </summary>
         Test = 0,

        /// <summary>
        /// The charge is usable
        /// </summary>
        Active = 1, 
        
        /// <summary>
        /// The charge should not be presented as options in front end operations, but all the schedules referring to this charge should continue.
        /// </summary>
        EndOfLife = 2,

        /// <summary>
        /// The charge is completely disabled, can't be used in any charge option, there can't be any active scheduled charge to be able to set the charge to this state. 
        /// </summary>
        Disabled = 3
    }
}