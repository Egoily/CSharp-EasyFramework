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
        private readonly DataContractJsonSerializer _innerSerializer = null;
      
    
        /// <summary>
        /// Default constructor
        /// </summary>
        public JSONSerializer()
        {
            _innerSerializer = new DataContractJsonSerializer(typeof(T));           
        }


        #region ISerializer<T> Members

        /// <summary>
        /// Coverts a message of type T into a memory stream in it's serialized form (JSON)
        /// </summary>
        /// <param name="message">the message to serialize</param>
        /// <returns>the memory stream with the object</returns>
        public MemoryStream Serialize(T message)
        {
            MemoryStream buffer = new MemoryStream(10 * 1024);
            _innerSerializer.WriteObject(buffer, message);
            return (buffer);
        }

        /// <summary>
        /// Reads a serialized object from a memory streams (in JSON) and returns it
        /// </summary>
        /// <param name="stream">the source stream to read</param>
        /// <returns>The object deserialized</returns>
        public T Deserialize(System.IO.Stream stream)
        {
            T Message = (T)_innerSerializer.ReadObject(stream);
            return (Message);
        }
        #endregion
    }
}
