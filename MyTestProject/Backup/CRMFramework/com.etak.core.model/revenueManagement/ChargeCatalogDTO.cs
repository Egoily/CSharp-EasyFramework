using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    /// <summary>
    /// Definition of a charge in the catalog
    /// </summary>
    [DataContract]
    public class ChargeCatalogDTO
    {
        /// <summary>
        /// Unique Id of the charge
        /// </summary>
        [DataMember]
        public String Id;

        /// <summary>
        /// A facility to organize charges
        /// </summary>
        [DataMember]
        public String Category;

        /// <summary>
        /// Description of the charge in possibly multiple languages, to be presented to the customer
        /// </summary>
        [DataMember]
        public IList<TextualDescription> Description;

        /// <summary>
        /// Description of the charge in possibly multiple languages, to be presented to the GUI and applications
        /// </summary>
        [DataMember]
        public IList<TextualDescription> Name;

        /// <summary>
        /// the date the charge was created
        /// </summary>
        [DataMember]
        public DateTime CreateDate;

        /// <summary>
        /// whether the charge is due in advance or in arrear ; only applies to charges invoiced in postpaid mode
        /// </summary>
        [DataMember]
        public TimesOfCharge TimeOfCharge;

        /// <summary>
        /// Prorating information regarding the change, will be present only if the charge represented is a charge that shall be prorated
        /// </summary>
        [DataMember]
        public ProratingSchemaDTO ProratingInformation;

        /// <summary>
        /// Information about the discounts associated with this charge
        /// </summary>
        [DataMember]
        public IList<DiscountDTO> Discounts;

        /// <summary>
        /// Information about the prices of the charge in a time range
        /// </summary>
        [DataMember]
        public IList<ChargePriceCatalogDTO> Prices;

    }
}
