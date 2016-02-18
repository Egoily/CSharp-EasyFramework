using System;
using com.etak.core.model.provisioning;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository Interface for <typeparamref name="THLRRequestErrors"/> entity
    /// </summary>
    /// <typeparam name="THLRRequestErrors">The type of the entity managed is or extends HLRRequestErrors</typeparam>
    public interface IHLRRequestErrorsRepository<THLRRequestErrors> : IRepository<THLRRequestErrors, Int64>
        where THLRRequestErrors : HLRRequestErrors
    {

    }
}
