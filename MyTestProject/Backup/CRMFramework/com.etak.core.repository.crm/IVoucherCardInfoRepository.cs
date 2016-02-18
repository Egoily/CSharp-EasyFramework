using com.etak.core.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository interface for VoucherCardInfo
    /// </summary>
    /// <typeparam name="TVoucherCardInfo">The type of the entity managed by the repository, is or extends VoucherCardInfo</typeparam>
    public interface IVoucherCardInfoRepository<TVoucherCardInfo> :
        IRepository<TVoucherCardInfo, string> where TVoucherCardInfo : VoucherCardInfo
    {
        /// <summary>
        /// Get Voucher card by vmo and VCENCRYPT code.
        /// </summary>
        /// <param name="vcencrypt">vcencrypt code</param>
        /// <param name="mvnoId">mvnoId</param>
        /// <returns></returns>
        IEnumerable<TVoucherCardInfo> GetByCodeAndMvnoId(string vcencrypt, int mvnoId);
    }
}
