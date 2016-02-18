using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    /// <summary>
    /// This class is used to defined the camel service of subscriber.
    /// </summary>
    [Serializable]
    public class CamelService : SuplementaryService
    {
        [DataMember]
        public String TemplateIdForCSIGTs { get; set; }

        /// <summary>
        /// Indicates whether the HLR will request subscriber location information from the serving VLR and send it to the GMSC.
        /// </summary>
        [DataMember]
        public Boolean? RequestSendSubscriberLocation { get; set; }

        /// <summary>
        /// Specifies whether the current location should be requested in a CAMEL PSI message.
        /// </summary>
        [DataMember]
        public Boolean? RequestSendCurrentLocation { get; set; }

        /// <summary>
        /// Indicates whether the HLR will request subscriber state information from the serving VLR and send it to the GMSC.
        /// </summary>
        [DataMember]
        public Boolean? RequestSendSubscriberState { get; set; }

        [DataMember]
        public CamelPhase1Service Phase1Settings { get; set; }
        [DataMember]
        public CamelPhase2Service Phase2Settings { get; set; }
        [DataMember]
        public CamelPhase3Service Phase3Settings { get; set; }
    }

    [Serializable]
    public class CamelPhase1Service 
    {
        /// <summary>
        /// Originating CSI
        /// </summary>
        [DataMember]
        public Boolean? OCSI { get; set; }

        /// <summary>
        /// Terminating CSI
        /// </summary>
        [DataMember]
        public Boolean? TCSI { get; set; }

        public CamelPhase1Service()
        {
            OCSI = TCSI = false;
        }
    }

    [Serializable]
    public class CamelPhase2Service
    {
        /// <summary>
        /// Originating CSI
        /// </summary>
        [DataMember]
        public Boolean? OCSI { get; set; }

        /// <summary>
        /// Terminating CSI
        /// </summary>
        [DataMember]
        public Boolean? TCSI { get; set; }


        public CamelPhase2Service()
        {
            OCSI = TCSI = false;
        }
    }


    [Serializable]
    public class CamelPhase3Service
    {
        /// <summary>
        /// D-CSI	Dialled Services CAMEL Subscription Information 
        /// </summary>
        [DataMember]
        public Boolean? DCSI { get; set; }

        /// <summary>
        /// GPRS-CSI	GPRS CAMEL Subscription Information
        /// </summary>
        [DataMember]
        public Boolean? GPRSCSI { get; set; }

        /// <summary>
        /// M-CSI	Mobility Management event Notification CAMEL Subscription Information
        /// </summary>
        [DataMember]
        public Boolean? MCSI { get; set; }

        /// <summary>
        /// N CSI	Network CAMEL Service Information
        /// </summary>
        [DataMember]
        public Boolean? NCSI { get; set; }

        /// <summary>
        /// Originating CAMEL Subscription Information (O-CSI)
        /// </summary>
        [DataMember]
        public Boolean? OCSI { get; set; }

        /// <summary>
        /// SMS CSI	Short Message Service CAMEL Subscription Information
        /// </summary>
        [DataMember]
        public Boolean? SMSCSI { get; set; }

        /// <summary>
        /// Supplementary Service Notification CAMEL Subscription Information 
        /// </summary>
        [DataMember]
        public Boolean? SSCSI { get; set; }

        /// <summary>
        /// T-CSI	Terminating CAMEL Subscription Information (in the GMSC)
        /// </summary>
        [DataMember]
        public Boolean? TCSI { get; set; }

        /// <summary>
        /// TIF-CSI	Translation Information Flag
        /// </summary>
        [DataMember]
        public Boolean? TIFCSI { get; set; }

        /// <summary>
        /// U-CSI	USSD CAMEL Subscription Information
        /// </summary>
        [DataMember]
        public Boolean? UCSI { get; set; }

        /// <summary>
        /// UUG-CSI	USSD General CAMEL Service Information			
        /// </summary>
        [DataMember]
        public Boolean? UUGCSI { get; set; }

        /// <summary>
        /// VT-CSI	VMSC Terminating CAMEL Subscription Information
        /// </summary>
        [DataMember]
        public Boolean? VTCSI { get; set; }


        public CamelPhase3Service()
        {
            DCSI = DCSI = GPRSCSI = MCSI = NCSI = OCSI = SMSCSI = SSCSI = TCSI = TIFCSI = UCSI = UUGCSI = VTCSI = false;
        }
    }
}
