using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class LanguageSpecificText
    {
        /// <summary>
        /// Unique Id 
        /// </summary>
        [DataMember] public virtual MultiLingualDescription Description { get; set; }
        
        /// <summary>
        /// Language in which text is expressed
        /// </summary>
        [DataMember] public virtual ISO639LanguageCodes Language { get; set; }

        /// <summary>
        /// The actual text in the Language specified
        /// </summary>
        [DataMember] public virtual String Text { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return (true);

            LanguageSpecificText otherObj = obj as LanguageSpecificText;

            if (otherObj == null)
                return (false);

            return (this.Language.Equals(otherObj.Language) && this.Description.Equals(otherObj.Description));
        }

        public override int GetHashCode()
        {
            return (this.Language.GetHashCode() + this.Description.GetHashCode());
        }
    }
}