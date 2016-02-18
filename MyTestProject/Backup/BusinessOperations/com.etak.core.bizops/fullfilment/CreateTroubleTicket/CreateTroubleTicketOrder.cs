using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation;

namespace com.etak.core.bizops.fullfilment.CreateTroubleTicket
{
    /// <summary>
    /// Order of CreateTroubleTicketBizop
    /// </summary>
    public class CreateTroubleTicketOrder : Order
    {
        /// <summary>
        /// Order discriminator for CreateTroubleTicket
        /// </summary>
        public override string Discriminator
        {
            get { return OrderDiscriminators.CreateTroubleTicket; }
        }
    }
}
