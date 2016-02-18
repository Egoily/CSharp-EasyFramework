namespace com.etak.core.operation.contract
{
    /// <summary>
    /// Defines a contract to convert types between diferent types
    /// </summary>
    /// <typeparam name="Tsource">The source type that will be converted</typeparam>
    /// <typeparam name="TDestination">The destination type of the conversion</typeparam>
    public interface ITypeConverter<Tsource, TDestination>
    {
        /// <summary>
        /// Converts the object in TSource to TDestination
        /// </summary>
        /// <param name="source">the object to be converted</param>
        /// <returns>the object in the TDestination</returns>
        TDestination Convert(Tsource source);
    }
}
