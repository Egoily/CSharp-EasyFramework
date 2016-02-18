using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.etak.core.model.provisioning
{
    [DataContract]
    [Serializable]
    public class Carrier
    {
        /// <summary>
        /// Unique Id of the Carrier
        /// </summary>
        public virtual Int32 Id { get; set; }
        /// <summary>
        /// The code that identifies the Carrier
        /// </summary>
        public virtual String Code { get; set; }
        /// <summary>
        /// Description field that describes the Carrier
        /// </summary>
        public virtual String Description { get; set; }
    }
}
