using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.etak.core.model.inventory
{
    [DataContract]
    [Serializable]
    public class CDMAHandset : Handset
    {
        /// <summary>
        /// Electronic serial number  
        /// </summary>
        public virtual string ESN { get; set; }
        /// <summary>
        /// Mobile equipment identifier
        /// </summary>
        public virtual string MEID { get; set; }
    }
}
