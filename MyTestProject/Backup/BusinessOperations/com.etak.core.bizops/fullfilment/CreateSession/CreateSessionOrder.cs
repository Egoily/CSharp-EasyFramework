using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation;

namespace com.etak.core.bizops.fullfilment.CreateSession
{
    /// <summary>
    /// Order produced in the create session operation
    /// </summary>
    public class CreateSessionOrder : Order
    {
        /// <summary>
        /// Discriminator
        /// </summary>
        public override string Discriminator
        {
            get { return OrderDiscriminators.CreateSession; }
        }
    }
}
