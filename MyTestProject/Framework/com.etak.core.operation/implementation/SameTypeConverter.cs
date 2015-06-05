using com.etak.core.operation.contract;

namespace com.etak.core.operation.implementation
{
    /// <summary>
    /// Dummy class that converts implements TypeConverter using same class as order and destination
    /// </summary>
    /// <typeparam name="TAny">The type of the input and the output</typeparam>
    public class SameTypeConverter<TAny> : ITypeConverter<TAny,TAny>
    {
        /// <summary>
        /// Does nothing, just returns source object provided
        /// </summary>
        /// <param name="source">The source object</param>
        /// <returns>The destination object that is the same as the source</returns>
        public TAny Convert(TAny source)
        {
            return (source);
        }
    }
}
