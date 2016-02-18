using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.operation.contract;
using com.etak.core.operation.contract.exceptions;

namespace com.etak.core.operation.IntTests.common
{
    public class AlwaysErrorValidator<TAny> : IValidator<TAny>
    {
        public bool Validate(TAny objectToValidate)
        {
            throw new BusinessLogicErrorException("TEST ERROR VALIDATOR MESSAGE", 9999);
        }
    }
}
