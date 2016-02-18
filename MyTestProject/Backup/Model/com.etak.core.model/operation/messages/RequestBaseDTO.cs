using System;

namespace com.etak.core.model.operation.messages
{
    /// <summary>
    /// THe base type for any Bussiness operation input parameter based in DTO model.
    /// </summary>
    public class RequestBaseDTO
    {
        /// <summary>
        /// Username to authenticate
        /// </summary>
        public String user;

        /// <summary>
        /// password of the user to authenticate
        /// </summary>
        public String password;
        
        /// <summary>
        /// VMNO that is sending the request
        /// </summary>
        public String vmno;

        /// <summary>
        /// sessionId that is sending the request
        /// </summary>
        public String sessionId;
    }
}
