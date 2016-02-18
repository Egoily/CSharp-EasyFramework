using System;

namespace com.etak.core.operation
{
    /// <summary>
    /// Enumeration of all possible errors throw by the framework.
    /// </summary>
    public class OperationErrorCodes
    {
        /// <summary>
        /// THe start number for all the errors produced by the operation framework
        /// </summary>
        public const Int32 ErrorBase = 100;

        /// <summary>
        /// A null request received while updating and order
        /// </summary>
        public const Int32 NullRequestInOrderModification = ErrorBase + 1;

        /// <summary>
        /// The order provided to modify was null
        /// </summary>
        public const Int32 NullOrderInOrderModification = ErrorBase + 2;

        /// <summary>
        /// The type of order recoverd from the DB by the ID did not match 
        /// </summary>
        public const Int32 OrderTypeMissMatch = ErrorBase + 3;

        /// <summary>
        /// Could not recover the dealar asociated to the MVNO provided
        /// </summary>
        public const Int32 MVNODoesNotExsist = ErrorBase + 4;

        /// <summary>
        /// THe username was not provided in the request
        /// </summary>
        public const Int32 UserNameNotProvided = ErrorBase + 5;

        /// <summary>
        /// Password was not provided in the request
        /// </summary>
        public const Int32 PasswordNotProvided = ErrorBase + 6;

        /// <summary>
        /// The external reference provided already exists, can't create a new order
        /// </summary>
        public const Int32 DuplicatedReferenceWhileCreatingOrder = ErrorBase + 7;

        /// <summary>
        /// There's no configuration for this business operation and vmno
        /// </summary>
        public static int MissingConfiguration = ErrorBase + 8;

        /// <summary>
        /// There's no active configuration for this business operation and vmno
        /// </summary>
        public static int MissingActiveConfiguration = ErrorBase + 9;


        /// <summary>
        /// There's more than one active configurtion for this business operation and vmno
        /// </summary>
        public static int MultipleActiveConfiguration = ErrorBase + 10;

        /// <summary>
        /// Operation configuration had not a valid text configuration. (JSON config was null empty or whitespace)
        /// </summary>
        public static int NoConfigurationTextInOperationConfiguration = ErrorBase + 11;

        /// <summary>
        /// THe given user does not have permissions to perform actions for the MVNO provided.
        /// </summary>
        public static int UserDoesNotHavePermissionsForDealer = ErrorBase + 12;

        /// <summary>
        /// The operation configuration text could not be de serialized as the expected type
        /// </summary>
        public static int ErrorDesrializingOperationConfiguration = ErrorBase + 13;

        /// <summary>
        /// The type of dealer is unknown so can't check the permissions.
        /// </summary>
        public static int UnknownDealerType = ErrorBase + 13;

        /// <summary>
        /// The sessionId was not provided in the request
        /// </summary>
        public const int SessionIdNotProvided = ErrorBase + 14;

        /// <summary>
        /// Could not recover the DealerInfo asociated to the dealerid provided
        /// </summary>
        public const int DealerIdNotExsist = ErrorBase + 15;

        /// <summary>
        /// Could not recover the VMNO asociated to the dealer
        /// </summary>
        public const int VMNONotExsist = ErrorBase + 16;

        /// <summary>
        /// The sessionId expired
        /// </summary>
        public const int SessionIdExpired = ErrorBase + 17;

        /// <summary>
        /// The mapping used is not allowed
        /// </summary>
        public const int MappingNotAllowed = ErrorBase + 18;

    }
}
