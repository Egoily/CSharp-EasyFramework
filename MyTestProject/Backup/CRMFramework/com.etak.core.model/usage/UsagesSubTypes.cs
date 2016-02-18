using System.Runtime.Serialization;

namespace com.etak.core.model.usage
{
    /// <summary>
    /// Posibles types of sub types of a call
    /// </summary>
    [DataContract]
    public enum UsagesSubTypes
    {
        [EnumMember]
        Voice = 3001,       //MobileVoice = 3001,       
        [EnumMember]
        SMS = 3002,         //MobileSms = 3002,
        [EnumMember]
        Data = 3003,        //MobileData = 3003,
        [EnumMember]
        MMS = 3004,         //MobileMms = 3004,        
        [EnumMember]
        VideoCall = 3005,   //MobileVideo = 3005,
    }
}