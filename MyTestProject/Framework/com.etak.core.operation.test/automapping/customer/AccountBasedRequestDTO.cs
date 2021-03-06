﻿using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.test.automapping.customer
{
    public class AccountBasedRequestDTO : RequestBaseDTO, IAccountIdBasedDTORequest
    {
        public long AccountId { get; set; }
    }
}
