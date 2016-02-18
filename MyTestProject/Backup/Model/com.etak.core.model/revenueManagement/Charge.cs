using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    /// <summary>
    /// Enum to indicate if the charge is informational only
    /// </summary>
     [Serializable]
    public enum InformationalTypes
    {
         /// <summary>
         /// The charge is not informational
         /// </summary>
        N = 0,
         /// <summary>
         /// The charge is informational only
         /// </summary>
        Y = 1,
    }

    /// <summary>
    /// This Charge Catalog (RM_CHARGES) handles, the characteristic of the charge
    /// </summary>
    [DataContract]
    [Serializable]
    public abstract class Charge
    {
        public Charge()
        {
            Prices = new List<ChargePrice>();
            ReferencingOptions = new List<ProductChargeOption>();
        }

        #region Basic charge information
        /// <summary>
        /// unique Id of the charge 
        /// </summary>
        public virtual Int32 Id { get; set; }

        /// <summary>
        /// Facility to organize charges by category
        /// </summary>
        public virtual Int32 ? Category { get; set; }     

       /// <summary>
       /// General Ledger account for the charge
       /// </summary>
       public virtual String  GeneralLedgerAccount { get; set; }    

        /// <summary>
        /// Internal name to identify the charge
        /// </summary>
        public virtual MultiLingualDescription Name { get; set; }

        /// <summary>
        /// Textual description of the charge
        /// </summary>
        public virtual MultiLingualDescription Description { get; set; }

        /// <summary>
        /// Date in which the charge was created
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

        /// <summary>
        /// Indicates if this charge is informational only
        /// </summary>
        public virtual InformationalTypes InformationalOnly { get; set; }

        /// <summary>
        /// Wether the charge is in Advance or in Arrear
        /// </summary>
        public virtual TimesOfCharge TypeOfTimeOfCharge { get; set; }

        /// <summary>
        /// List of prices for the charge, for the different time ranges
        /// </summary>
        public virtual IList<ChargePrice> Prices { get; set; }

        /// <summary>
        /// List of ProductChargeOptions that could trigger this charge
        /// </summary>
        public virtual IList<ProductChargeOption> ReferencingOptions { get; set; }

        /// <summary>
        /// An implementation of the Charge computer that will be used to calculate the amount
        /// </summary>
        public virtual IChargeComputer AmountComputer { get; set; }

        /// <summary>
        /// the status of the charge, used to determine if the charge is operative
        /// </summary>
        public virtual ChargeStatus Status { get; set; }

        /// <summary>
        /// Charges referenced by this charge
        /// </summary>
        public virtual IList<ChargeTarget> ReferencedCharges { get; set; }

        /// <summary>
        /// Charge referencing this charge
        /// </summary>
        public virtual IList<ChargeTarget> ReferencingCharges { get; set; }

        #endregion

        #region Proration charge information
        /// <summary>
        /// the unit of time the proration is based upon
        /// </summary>
        public virtual TimeUnits? ProrateUnit { get; set; }

        /// <summary>
        /// the number of prorateunit the proration is based upon.
        /// </summary>
        public virtual Decimal ? ProrateQty { get; set; }
        #endregion

    }
}