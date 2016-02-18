using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.operation.contract;
using com.etak.core.operation.contract.exceptions;
using NHibernate.Mapping;

namespace com.etak.core.operation.IntTests.common
{
    public class AlwaysErrorConverter<TAny> : ITypeConverter<TAny, TAny>
    {
        public TAny Convert(TAny source)
        {
            throw new BusinessLogicErrorException("TEST ERROR CONVERTER MESSAGE", 9999);
        }
    }
}
