using com.etak.core.operation.contract;

namespace com.etak.core.operation.implementation
{
    /// <summary>
    /// Implementation of validator that does not perform any validation
    /// </summary>
    /// <typeparam name="T">THe type of object</typeparam>
    public class NullValidator<T> : IValidator<T>
    {
        /// <summary>
        /// Implementation of validate that always return true
        /// </summary>
        /// <param name="objectToValidate">the object to be validated</param>
        /// <returns>always true</returns>
        public bool Validate(T objectToValidate)
        {
            return true;
        }
    }
}
