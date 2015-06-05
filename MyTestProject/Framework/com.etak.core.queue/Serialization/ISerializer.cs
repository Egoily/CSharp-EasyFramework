namespace com.etak.core.queue.Serialization
{
    /// <summary>
    /// Interface for a serializer for type T
    /// </summary>
    /// <typeparam name="T">The entity that is being Serialized or Deserialized</typeparam>
    public interface  ISerializer <T>
    {
        System.IO.MemoryStream Serialize(T Message);
        T Deserialize(System.IO.Stream stream);
    }
}
