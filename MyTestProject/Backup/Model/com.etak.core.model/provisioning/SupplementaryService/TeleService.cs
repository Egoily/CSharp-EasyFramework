using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    
    public enum TeleserviceTypes
    {
        /// <summary>
        /// Telephony
        /// </summary>
        [EnumMember]
        TS11,

        /// <summary>
        /// Emergency Calls
        /// </summary>
        [EnumMember]
        TS12,

        /// <summary>
        /// Short message MT/PP 
        /// </summary>
        [EnumMember]
        TS21,

        /// <summary>
        /// Short message MO/PP 
        /// </summary>
        [EnumMember]
        TS22,

        /// <summary>
        /// Cell Broadcast TypeOfBarring
        /// </summary>
        [EnumMember]
        TS23,

        /// <summary>
        /// Alternate speech and facsimile group 3
        /// </summary>
        [EnumMember]
        TS61,

        /// <summary>
        /// Automatic Facsimile group 3
        /// </summary>
        [EnumMember]
        TS62,

        /// <summary>
        /// Automatic Facsimile group 4
        /// </summary>
        [EnumMember]
        TS63,

        /// <summary>
        /// Voice Group Call TypeOfBarring
        /// </summary>
        [EnumMember]
        TS91,

        /// <summary>
        /// Voice Broadcast TypeOfBarring
        /// </summary>
        [EnumMember]
        TS92,

        /// <summary>
        /// PLMN Suplementary Service 1
        /// </summary>
        [EnumMember]
        TSD1,

        /// <summary>
        /// PLMN Suplementary Service 2
        /// </summary>
        [EnumMember]
        TSD2,

        /// <summary>
        /// PLMN Suplementary Service 3
        /// </summary>
        [EnumMember]
        TSD3,
        /// <summary>
        /// PLMN Suplementary Service 4
        /// </summary>
        [EnumMember]
        TSD4,
        /// <summary>
        /// PLMN Suplementary Service 5
        /// </summary>
        [EnumMember]
        TSD5,
        /// <summary>
        /// PLMN Suplementary Service 6
        /// </summary>
        [EnumMember]
        TSD6,
        /// <summary>
        /// PLMN Suplementary Service 7
        /// </summary>
        [EnumMember]
        TSD7,
        /// <summary>
        /// PLMN Suplementary Service 8
        /// </summary>
        [EnumMember]
        TSD8,
        /// <summary>
        /// PLMN Suplementary Service 9
        /// </summary>
        [EnumMember]
        TSD9,
        /// <summary>
        /// PLMN Suplementary Service 10
        /// </summary>
        [EnumMember]
        TSDA,
        /// <summary>
        /// PLMN Suplementary Service 11
        /// </summary>
        [EnumMember]
        TSDB,
        /// <summary>
        /// PLMN Suplementary Service 12
        /// </summary>
        [EnumMember]
        TSDC,

        /// <summary>
        /// PLMN Suplementary Service 13
        /// </summary>
        [EnumMember]
        TSDD,

        /// <summary>
        /// PLMN Suplementary Service 14
        /// </summary>
        [EnumMember]
        TSDE,

        /// <summary>
        /// PLMN Suplementary Service 15
        /// </summary>
        [EnumMember]
        TSDF,
    }

    /// <summary>
    /// Specifies the tele services of MS.
    /// </summary>
    [Serializable]
    public class TeleService : SuplementaryService
    {
        [DataMember]
        public TeleserviceTypes Service { get; set; }        
    }
}
