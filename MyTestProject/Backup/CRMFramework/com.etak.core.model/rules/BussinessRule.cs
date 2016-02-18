using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public abstract class BussinessRule
    {
        public virtual Int32 Id { get; set; }

        /// <summary>
        /// Descriptive name
        /// </summary>
        public virtual String ClassType { get; set; }

        /// <summary>
        /// Descriptive name
        /// </summary>
        public virtual String Name { get; set; }

        /// <summary>
        /// Db field holing the JSON config for serializing and de serializing TConfig type
        /// </summary>
        public virtual String JSonConfig { get; set; }
    }    
}
