﻿using com.etak.core.model.operation.contract.amount;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.test.automapping.amount
{
    public class AmountBasedRequestDTO : RequestBaseDTO, IAmountBasedDTORequest
    {
        public decimal amount { get; set; }
    }
}
