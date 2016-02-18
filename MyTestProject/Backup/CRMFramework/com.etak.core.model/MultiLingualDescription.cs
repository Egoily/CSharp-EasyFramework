using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class MultiLingualDescription
    {
        /// <summary>
        /// Unique Id 
        /// </summary>
        [DataMember] public virtual Int32 Id { get; set; }
        
        /// <summary>
        /// Language in which text is expressed
        /// </summary>
        [DataMember] public virtual IList<LanguageSpecificText> Texts { get; set; }

        /// <summary>
        /// The actual text in the Language specified
        /// </summary>
        [DataMember] public virtual String DefaultMessage { get; set; }

        /// <summary>
        /// The multiLingal type
        /// </summary>
        [DataMember]
        public virtual MultiLingualType Type { get; set; }
        /// <summary>
        /// The display Sequence
        /// </summary>
        [DataMember]
        public virtual int DisplaySequence { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return (true);

            MultiLingualDescription otherObj = obj as MultiLingualDescription;

            if (otherObj == null)
                return (false);

            return (this.Id.Equals(otherObj.Id));
        }

        public override int GetHashCode()
        {
            return (this.Id.GetHashCode());
        }
    }
}