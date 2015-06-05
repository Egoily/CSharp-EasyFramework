using System;

namespace com.etak.core.operation
{
    /// <summary>
    /// Contains all constants used by operation framework
    /// </summary>
    internal class Definitions
    {
        public const Int32 ErrorBase = 100;
    }

    /// <summary>
    /// Enumeration of all possible errors throw by the framework.
    /// </summary>
    public class OperationErrorCodes
    {
        /// <summary>
        /// A null request received while updating and order
        /// </summary>
        public const Int32 NullRequestInOrderModification = Definitions.ErrorBase + 1;

        /// <summary>
        /// The order provided to modify was null
        /// </summary>
        public const Int32 NullOrderInOrderModification = Definitions.ErrorBase + 2;

        /// <summary>
        /// The type of order recoverd from the DB by the ID did not match 
        /// </summary>
        public const Int32 OrderTypeMissMatch = Definitions.ErrorBase + 3;

        /// <summary>
        /// Could not recover the dealar asociated to the MVNO provided
        /// </summary>
        public const Int32 MVNODoesNotExsist = Definitions.ErrorBase + 4;

        /// <summary>
        /// THe username was not provided in the request
        /// </summary>
        public const Int32 UserNameNotProvided = Definitions.ErrorBase + 5;

        /// <summary>
        /// Password was not provided in the request
        /// </summary>
        public const Int32 PasswordNotProvided = Definitions.ErrorBase + 6;

        /// <summary>
        /// The external reference provided already exists, can't create a new order
        /// </summary>
        public const Int32 DuplicatedReferenceWhileCreatingOrder = Definitions.ErrorBase + 7;

        /// <summary>
        /// There's no configuration for this business operation and vmno
        /// </summary>
        public static int MissingConfiguration = Definitions.ErrorBase + 8;

        /// <summary>
        /// There's no active configuration for this business operation and vmno
        /// </summary>
        public static int MissingActiveConfiguration = Definitions.ErrorBase + 9;


        /// <summary>
        /// There's more than one active configurtion for this business operation and vmno
        /// </summary>
        public static int MultipleActiveConfiguration = Definitions.ErrorBase + 10;

        /// <summary>
        /// Operation configuration had not a valid text configuration. (JSON config was null empty or whitespace)
        /// </summary>
        public static int NoConfigurationTextInOperationConfiguration = Definitions.ErrorBase + 10;

        /// <summary>
        /// The operation configuration text could not be de serialized as the expected type
        /// </summary>
        public static int ErrorDesrializingOperationConfiguration { get; set; }
    }
}
