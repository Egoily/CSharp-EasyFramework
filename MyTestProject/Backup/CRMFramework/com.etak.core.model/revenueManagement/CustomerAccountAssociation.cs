using System;

namespace com.etak.core.model.revenueManagement
{
    /// <summary>
    /// Association between a customer and and an account in a time range
    /// </summary>
    [Serializable]
    public class CustomerAccountAssociation
    {
        /// <summary>
        /// Unique Id of the association
        /// </summary>
        virtual public Int32 Id { get; set; }

        /// <summary>
        /// Customer associated to the account
        /// </summary>
        virtual public CustomerInfo Customer { get; set; }
        
        /// <summary>
        /// Account associated to the customer
        /// </summary>
        virtual public Account Account { get; set; }

        /// <summary>
        /// The start date in which this association is valid
        /// </summary>
        virtual public DateTime StartDate { get; set; }

        /// <summary>
        /// Ent end date in which this association si valid
        /// </summary>
        virtual public DateTime? EndTime { get; set; }
    }
}
