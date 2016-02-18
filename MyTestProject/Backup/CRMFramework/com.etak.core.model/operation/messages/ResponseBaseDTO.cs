using System;

namespace com.etak.core.model.operation.messages
{
    /// <summary>
    /// The base type for all responses of any DTO operation (Bussiness Operations)
    /// </summary>
    public class ResponseBaseDTO
    {
        /// <summary>
        /// the type of the result of the operation
        /// </summary>
        public ResultTypes resultType;

        /// <summary>
        /// Error code of the operation
        /// </summary>
        public Int32 errorCode;
        
        /// <summary>
        /// Messages to inform about the result of the operation 
        /// </summary>
        public String[] messages;
    }
}
