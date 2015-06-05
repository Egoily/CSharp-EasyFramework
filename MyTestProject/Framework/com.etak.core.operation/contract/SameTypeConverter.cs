namespace com.etak.core.operation.conversion
{
    public class SameTypeConverter<T1> : ITypeConverter<T1,T1>
    {
        public T1 Convert(T1 source)
        {
            return (source);
        }
    }
}
