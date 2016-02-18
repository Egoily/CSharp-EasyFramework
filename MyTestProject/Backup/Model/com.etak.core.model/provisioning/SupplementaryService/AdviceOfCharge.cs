using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    
    public enum AdviceOfChargeTypes
    {
        /// <summary>
        /// This service permits MS to display an accurate estimate the size of the bill which will eventually be levied in the Home PLMN.
        /// </summary>
        [EnumMember]
        AdviceOfChargeInformation_AoCI,

        /// <summary>
        /// This service provides the means by which the MS may indicate the charge that will be made for the use of telecommunication services. 
        /// It is intended for applications where the user is generally not the subscriber but is known to the subscriber, and where the 
        /// user pays the subscriber, rather than the TypeOfBarring Provider.
        /// The charge information is based as closely as possible on the charge that will be levied on 
        /// the subscriber's bill in the Home PLMN. Where this charge cannot be stored in the MS, 
        /// use of the telecommunications service shall be prevented as described below.
        /// </summary>
        [EnumMember]
        AdviceOfChargeCharging_AoCC,
    }

    [Serializable]
    public class AdviceOfCharge : SuplementaryService
    {
        [DataMember]
        public AdviceOfChargeTypes Type { get; set; }
    }
}
