using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.dto;
using com.etak.core.model.usage;

namespace com.etak.core.operation.dtoConverters
{
    /// <summary>
    /// Class which contains all the dictionaries related with DTO Objects
    /// </summary>
    public static class DtoDictionaries
    {
        /// <summary>
        /// Dictionary to convert from DocumentTypes Enum to integer
        /// </summary>
        public static readonly IDictionary<DocumentTypes, Int32> DocEnumToIntMapper = new System.Collections.Generic.Dictionary<DocumentTypes, int>
        {
            {DocumentTypes.Passport, 1},
            {DocumentTypes.DrivingLicence, 2},
            {DocumentTypes.DNI, 3},
            {DocumentTypes.NIF, 4},
            {DocumentTypes.NIE, 5},
            {DocumentTypes.CIF, 6},
        };

        /// <summary>
        /// Dictionary to convert from integer to DocumentTypes Enum.
        /// </summary>
        public static readonly IDictionary<Int32, DocumentTypes> IntToDocEnumTo = new System.Collections.Generic.Dictionary<Int32, DocumentTypes>
        {
            {1, DocumentTypes.CIF},
            {2, DocumentTypes.DrivingLicence},
            {3, DocumentTypes.DNI},
            {4, DocumentTypes.NIF},
            {5, DocumentTypes.NIE},
            {6, DocumentTypes.CIF},

        };

        /// <summary>
        /// Dictionary to convert from integer to CustomerStatus Enum
        /// </summary>
        public static readonly IDictionary<Int32, CustomerStatus> IntToCustStatusEnum = new System.Collections.Generic.
            Dictionary<Int32, CustomerStatus>
        {
            {1, CustomerStatus.Pending},
            {2, CustomerStatus.Active},
            {3, CustomerStatus.Terminated},
            {4, CustomerStatus.Rejected},
            {5, CustomerStatus.PreActive},
            {6, CustomerStatus.Regulatory},
            {20, CustomerStatus.Deleted},
        };

        /// <summary>
        /// Dictionary to convert from PendingStatus Enum to Integer
        /// </summary>
        public static readonly IDictionary<PendingStatus, Int32> PendingStatusEnumToInt = new System.Collections.Generic.
            Dictionary<PendingStatus, Int32>
        {
            {PendingStatus.Pending, 1},
            {PendingStatus.Active, 2},
            {PendingStatus.Terminated, 3},
            {PendingStatus.Rejected, 4},
            {PendingStatus.PreActive, 5},
            {PendingStatus.Frozen, 6},
        }; 
        /// <summary>
        /// Dictionary to convert from UsagesSubTypes Enumerable to Int32
        /// </summary>
        public static readonly IDictionary<UsagesSubTypes, Int32> UsagesSubTypesEnumToInt = new Dictionary<UsagesSubTypes, Int32>
        {
            {UsagesSubTypes.Voice,      3001},
            {UsagesSubTypes.SMS,        3002},
            {UsagesSubTypes.Data,       3003},
            {UsagesSubTypes.MMS,        3004},
            {UsagesSubTypes.VideoCall,  3005},
        };

        /// <summary>
        /// Dictionary to convert from Int32 to UsagesSubTypes Enumerable
        /// </summary>
        public static readonly IDictionary<Int32, UsagesSubTypes> IntoToUsagesSubTypesEnum = new Dictionary<Int32, UsagesSubTypes>
        {
            {3001, UsagesSubTypes.Voice},
            {3002,UsagesSubTypes.SMS},
            {3003,UsagesSubTypes.Data},
            {3004,UsagesSubTypes.MMS},
            {3005,UsagesSubTypes.VideoCall},
        };
    }
}
