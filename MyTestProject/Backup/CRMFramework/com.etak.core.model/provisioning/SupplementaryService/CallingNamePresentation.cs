using System;

namespace com.etak.core.model.provisioning
{
    /// <summary>
    /// This supplementary service provides for the ability to indicate the name information of the calling party to the called party at call set-up time for all incoming calls.
    /// The calling party takes no action to activate, initiate, or in any manner provide Calling Name Identification Presentation. 
    /// However, the delivery of the calling name to the called party may be affected by other services subscribed to by the calling party. 
    /// For example, if the calling party has subscribed to Calling Line Identification Restriction (CLIR), 
    /// then the calling line identity  as well as the calling name identity shall not be presented to the called party. 
    /// </summary>
    [Serializable]
    public class CallingNamePresentation : SuplementaryService
    {

    }
}
