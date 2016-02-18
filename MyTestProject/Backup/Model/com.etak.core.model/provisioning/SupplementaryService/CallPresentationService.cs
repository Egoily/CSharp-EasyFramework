using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    
    public enum CallPresentationServices
    {
        /// <summary>
        /// The CLIP Supplementary TypeOfBarring provides the called party with the possibility to receive the line identity of the calling party
        /// </summary>
        [EnumMember]
        CallingLineIdentificationPresentation_CLIP,

        /// <summary>
        /// The CLIR Supplementary TypeOfBarring enables the calling party to prevent presentation of its line identity to the called party.
        /// </summary>
        [EnumMember]
        CallingLineIdentificationRestriction_CLIR,
        
        /// <summary>
        /// The Connected Line Identification Presentation (COLP) Supplementary TypeOfBarring provides the calling party with the possibility to receive the line identity of the connected party.
        /// </summary>
        [EnumMember]
        ConnectedLineIdentificationPresentation_COLP,

        /// <summary>
        /// The COLR Supplementary TypeOfBarring enables the connected party to prevent presentation of its line identity to the calling party.
        /// </summary>
        [EnumMember]
        ConnectedLineIdentificationRestriction_COLR,
    }

    /// <summary>
    /// The class is used to define the calling presentation behavior
    /// </summary>
    [Serializable]
    public class CallPresentationService : SuplementaryService
    {
        [DataMember]
        public CallPresentationServices Service { get; set; }
        [DataMember]
        public bool Override { get; set; }
    }
}
