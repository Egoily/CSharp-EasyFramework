using System;

namespace com.etak.core.queue.Serialization
{
    public class XMLSerializer<T> : ISerializer<T>
    {
        #region ISerializer<T> Members


        

        public T Deserialize(System.IO.Stream stream)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ISerializer<T> Members

        public System.IO.MemoryStream Serialize(T Message)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
