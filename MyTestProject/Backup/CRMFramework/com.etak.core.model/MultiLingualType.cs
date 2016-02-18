using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public enum MultiLingualType
    {
        /// <summary>
        /// Default one that no need to specify type, like Name Description.
        /// </summary>
        [EnumMember]
        Default = 0,
        /// <summary>
        /// Color
        /// </summary>
        [EnumMember]
        Color = 1001,
        /// <summary>
        /// Storage
        /// </summary>
        [EnumMember]
        Storage = 1002,
        /// <summary>
        /// Operation System
        /// </summary>
        [EnumMember]
        OperationSystem = 1003,
        /// <summary>
        /// Color
        /// </summary>
        [EnumMember]
        Brand = 1004,
        /// <summary>
        /// Color
        /// </summary>
        [EnumMember]
        FrontCamera = 1005,
        /// <summary>
        /// Color
        /// </summary>
        [EnumMember]
        BackCamera = 1006,

    }
}
