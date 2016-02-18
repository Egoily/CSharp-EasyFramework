using System;


namespace com.etak.core.microservices
{
    /// <summary>
    /// Holds all the errors produced in the micro services
    /// </summary>
    public class CoreMicroServicesErrorCodes
    {
        /// <summary>
        /// The starting number for all the errors in micro services
        /// </summary>
        public const Int32 ErrorBase = 100000;


        /// <summary>
        /// The Customer as input paramter was null
        /// </summary>
        public const Int32 CustomerNotProvided = ErrorBase + 1;
    }
}
