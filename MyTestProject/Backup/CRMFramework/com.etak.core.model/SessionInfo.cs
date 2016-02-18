using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class SessionInfo
    {
        virtual public LoginInfo LoginInfo { get; set; }

        virtual public String SessionID  { get; set; }

        virtual public int IdleTimoutMinutes  { get; set; }

        virtual public DateTime IdleTimeoutDate  { get; set; }

    }
}
