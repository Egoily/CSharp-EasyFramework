using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    /// <summary>
    /// DTO Object corresponding to the Textual Description
    /// </summary>
    [DataContract]
    public class TextualDescription
    {
        /// <summary>
        /// ISO Code corresponding to the Language
        /// </summary>
        [DataMember]
        public ISO639LanguageCodes LanguageCode;

        /// <summary>
        /// Text of the Message
        /// </summary>
        [DataMember]
        public String Text;
    }
}
