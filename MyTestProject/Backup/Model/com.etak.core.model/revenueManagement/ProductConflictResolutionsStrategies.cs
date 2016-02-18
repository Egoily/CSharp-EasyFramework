
namespace com.etak.core.model.revenueManagement
{
    /// <summary>
    /// Enumeration of possible action types.
    /// </summary>
    public enum ProductConflictResolutionsStrategies
    {
        /// <summary>
        /// De provision the products that generates the conflict and provision the requested
        /// </summary>
        Deprovision,

        /// <summary>
        /// Reject the customer product association.
        /// </summary>
        Reject,
    }
}
