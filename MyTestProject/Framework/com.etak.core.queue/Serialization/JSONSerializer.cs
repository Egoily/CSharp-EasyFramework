using System.IO;
using System.Runtime.Serialization.Json;

namespace com.etak.core.queue.Serialization
{
    /// <summary>
    /// A serializer based on JSON implemented using .Net DataContractJsonSerializer
    /// </summary>
    /// <typeparam name="T">the type of entity to be serialized</typeparam>
    public class JSONSerializer <T> : ISerializer <T>
    {
        private readonly DataContractJsonSerializer InnerSerializer = null;
      
    
        /// <summary>
        /// Default constructor
        /// </summary>
        public JSONSerializer()
        {
            InnerSerializer = new DataContractJsonSerializer(typeof(T));           
        }


        #region ISerializer<T> Members
        public System.IO.MemoryStream Serialize(T Message)
        {
            MemoryStream buffer = new MemoryStream(10 * 1024);
            InnerSerializer.WriteObject(buffer, Message);
            return (buffer);
        }

        public T Deserialize(System.IO.Stream stream)
        {
            T Message = (T)InnerSerializer.ReadObject(stream);
            return (Message);
        }
        #endregion
    }
}
