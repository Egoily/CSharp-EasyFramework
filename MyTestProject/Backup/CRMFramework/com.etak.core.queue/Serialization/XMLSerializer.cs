using System;

namespace com.etak.core.queue.Serialization
{
    /// <summary>
    /// Serializes objects into XML of type T
    /// </summary>
    /// <typeparam name="T">The type of object serialized</typeparam>
    public class XMLSerializer<T> : ISerializer<T>
    {
      


        
        /// <summary>
        /// Reads an stream and outputs the Object that was serialized
        /// </summary>
        /// <param name="stream">the stream where the object needs to be read from</param>
        /// <returns>The object read</returns>
        public T Deserialize(System.IO.Stream stream)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Coverts a message of type T into a memory stream in it's serialized form
        /// </summary>
        /// <param name="message">the message to serialize</param>
        /// <returns>the memory stream with the object</returns>
        public System.IO.MemoryStream Serialize(T message)
        {
            throw new NotImplementedException();
        }

     
    }
}
