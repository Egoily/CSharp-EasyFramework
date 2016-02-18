using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.etak.core.model.inventory
{
    [DataContract]
    [Serializable]
    public enum NetworkType
    {
        /// <summary>
        /// GSM Mobile Network
        /// </summary>
        GSM,
        /// <summary>
        /// CDMA Mobile Network
        /// </summary>
        CDMA
    }
}
