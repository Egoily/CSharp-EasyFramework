using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
        /// <summary>
        /// Entity to hold represent a discount of a charge
        /// </summary>
        [DataContract]
        public abstract class DiscountDTO
        {
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
        }


        /// <summary>
        /// Entity to hold represent a discount of a charge
        /// </summary>
        [DataContract]
        public class FixedAmountDiscountDTO : DiscountDTO
        {
            /// <summary>
            /// The Amount to be discounted
            /// </summary>
            [DataMember]
            public Decimal AmountDiscounted;
        }



        /// <summary>
        /// Entity to hold represent a discount of a charge
        /// </summary>
        [DataContract]
        public class PercentualDiscountDTO : DiscountDTO
        {
            /// <summary>
            /// Percentage to be discounted
            /// </summary>
            [DataMember]
            public Decimal PercentageDiscounted;
        }
}
