
namespace com.etak.core.model.operation.messages
{
    /// <summary>
    /// Enumerated of all the error types handled by the platform
    /// </summary>
    public enum ResultTypes
    {
        /// <summary>
        /// The operation was successfull
        /// </summary>
        Ok = 0,
        /// <summary>
        /// The validation of the request failed
        /// </summary>
        DataValidationError = 1,

        /// <summary>
        /// The request has been queued
        /// </summary>
        Queued = 2,

        /// <summary>
        /// The request could not be processed due to bussiness or logic errors
        /// </summary>
        BussinessLogicError = 3,

        /// <summary>
        /// An unhandled error happened
        /// </summary>
        UnknownError = 4,

        /// <summary>
        /// Could not authenticate the username/password combination
        /// </summary>
        AuthenticationError = 5,

        /// <summary>
        /// The user given did not had permissions to perform the operation
        /// </summary>
        AuthorizationError = 6,

        /// <summary>
        /// The reference provided was not unique
        /// </summary>
        DuplicatedReference = 7 ,
    }
}
