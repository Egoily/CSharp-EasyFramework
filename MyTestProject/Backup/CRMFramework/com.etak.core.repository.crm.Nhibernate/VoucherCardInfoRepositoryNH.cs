using com.etak.core.model;
using com.etak.core.repository.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository Implementation for VoucherCardInfo
    /// </summary>
    /// <typeparam name="TVoucherCardInfo"></typeparam>
    public class VoucherCardInfoRepositoryNH<TVoucherCardInfo>
                    : NHibernateRepository<TVoucherCardInfo, string>,                         //Extends and gets basic CRUD operations
                      IVoucherCardInfoRepository<TVoucherCardInfo> where TVoucherCardInfo : VoucherCardInfo //Implementes 
    {

        /// <summary>
        /// Get Voucher card by vmo and VCENCRYPT code.
        /// </summary>
        /// <param name="vcencrypt">vcencrypt code</param>
        /// <param name="mvnoId">mvnoId</param>
        /// <returns></returns>
        public IEnumerable<TVoucherCardInfo> GetByCodeAndMvnoId(string vcencrypt, int mvnoId)
        {
            return (GetQuery().
                        Where(x => x.DealerId == mvnoId).
                        And(x => x.VcEncrypt == vcencrypt).
                        Future());
        }
    }
}
