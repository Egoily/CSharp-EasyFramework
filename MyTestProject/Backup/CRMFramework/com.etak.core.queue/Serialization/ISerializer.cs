using System.IO;

namespace com.etak.core.queue.Serialization
{
    /// <summary>
    /// Interface for a serializer for type T
    /// </summary>
    /// <typeparam name="T">The entity that is being Serialized or Deserialized</typeparam>
    public interface  ISerializer <T>
    {
        /// <summary>
        /// Coverts a message of type T into a memory stream in it's serialized form
        /// </summary>
        /// <param name="message">the message to serialize</param>
        /// <returns>the memory stream with the object</returns>
        MemoryStream Serialize(T message);

        /// <summary>
        /// Reads a serialized object from a memory streams and returns it
        /// </summary>
        /// <param name="stream">the source stream to read</param>
        /// <returns>The object deserialized</returns>
        T Deserialize(Stream stream);
    }
}
