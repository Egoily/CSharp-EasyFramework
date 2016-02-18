using System;
using System.Collections.Generic;
using com.etak.core.app.BenifitsRenewalEngine.DTO;
using com.etak.core.repository;

namespace com.etak.core.app.BenifitsRenewalEngine.Repository
{
    interface IBenifitsRenewalRepository<T> : IRepository<T, Int64> where T : RenewalCandidates
    {
        IList<T> FetchRenewCandidates(int perCount, DateTime fetchDateTime, long promotionId);
        IList<T> FetchPreRenewCandidates(int perCount, DateTime fetchDateTime, long promotionId);
    }
}
