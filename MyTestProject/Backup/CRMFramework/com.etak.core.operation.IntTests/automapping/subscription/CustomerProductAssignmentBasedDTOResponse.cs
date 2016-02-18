using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.operation.IntTests.automapping.subscription
{

    public class CustomerProductAssignmentBasedDTOResponse : ResponseBaseDTO, ICustomerProductAssignmentBasedDTOResponse
    {
        public CustomerProductAssignmentDTO CustomerProductAssignment { get; set; }
    }
}
