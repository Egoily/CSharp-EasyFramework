namespace com.etak.core.operation.contract
{
    /// <summary>
    /// Interface to validate objects
    /// </summary>
    /// <typeparam name="TValidated">the type of object to be validated</typeparam>
    public interface IValidator<TValidated>
    {
        /// <summary>
        /// Checks the validity of an object
        /// </summary>
        /// <param name="objectToValidate">the object that will be validated</param>
        /// <returns>a Boolean indicating if the object is valid or not</returns>
        bool Validate(TValidated objectToValidate);
    }
}
