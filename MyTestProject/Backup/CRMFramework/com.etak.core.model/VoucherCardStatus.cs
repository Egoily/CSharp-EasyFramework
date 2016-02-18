using System;

namespace com.etak.core.model
{

    /// <summary>
    /// the voucher status value
    /// </summary>
    [Serializable]
    public enum VoucherStatus : int
    {
        /// <summary>
        /// it is created,the value is 0.
        /// </summary>
        Created = 0,
        /// <summary>
        /// it is active,the value is 1.
        /// </summary>
        Active = 1,
        /// <summary>
        /// it has been used,the value is 2.
        /// </summary>
        Used = 2,
        /// <summary>
        /// it has expired,the value is 3.
        /// </summary>
        Expired = 3,
        /// <summary>
        /// it is using,the value is 4.
        /// </summary>
        Using = 4,
        /// <summary>
        /// it is recharging,the value is 5.
        /// </summary>
        Recharging = 5,
        /// <summary>
        /// it is annulled,the value is 6.
        /// </summary>
        Annulled = 6,
        /// <summary>
        /// it is fraudulent,the value is 7.
        /// </summary>
        Fraudulent = 7,
        /// <summary>
        /// it is initialized,the value is 8.
        /// </summary>
        Init = 8,
    }
}
