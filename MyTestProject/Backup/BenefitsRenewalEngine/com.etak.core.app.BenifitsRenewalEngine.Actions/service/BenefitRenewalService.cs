using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using com.etak.core.app.BenefitsRenewalEngine.contract;
using com.etak.core.app.BenifitsRenewalEngine.Actions.Utilty;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository;
using com.etak.core.repository.crm.promotion;
using log4net;

namespace com.etak.core.app.BenifitsRenewalEngine.Actions.service
{
    public class BenefitRenewalService : IBenefitsRenewalService
    {
        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private void RunPreRenewActions(CrmCustomersPromotionInfo promotionInfo, BillRun oldBillRun, BillRun newBillRun)
        {
            //TODO: promotionInfo.PromotionDetail should use cached one.
            var actions = promotionInfo.PromotionDetail.PreRenewActions.OrderBy(e => e.Priority);

            actions.ForEach((ee) =>
            {
                ee.Renew(promotionInfo.Customer, oldBillRun, newBillRun);
            });
        }

        private void RunRenewActions(CrmCustomersPromotionInfo promotionInfo, BillRun oldBillRun, BillRun newBillRun)
        {
            var actions = promotionInfo.PromotionDetail.RenewActions.OrderBy(e => e.Priority);

            actions.ForEach((ee) =>
            {
                ee.Renew(promotionInfo.Customer, oldBillRun, newBillRun);
            });
        }

        public void RenewCustomersBenefits(List<int> customerIdsToRenew)
        {

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var promotionRepo = RepositoryManager.GetRepository<ICrmCustomersPromotionInfoRepository<CrmCustomersPromotionInfo>>();

                foreach (var customerid in customerIdsToRenew)
                {
                    if (logger.IsDebugEnabled)
                        logger.DebugFormat("begin renewing customer: {0}", customerid);

                    
                    using (var trx = conn.BeginTransaction())
                    {
                        try
                        {
                            var listToRenew = promotionRepo.GetCustomerPromotionPlanById(customerid, null).Where(ee => ee.NextRenewDate <= System.DateTime.Now && !ee.ActionsExecuted).ToList();

                            listToRenew.ForEach((promotionInfo) =>
                            {
                                DateTime oldBillRunStartDate = UtiltyHelper.GetOldBillRunStartDate(promotionInfo.PromotionDetail, promotionInfo.NextRenewDate.Value);
                                DateTime newBillRunEndDate = UtiltyHelper.GetNewBillRunEndDate(promotionInfo.PromotionDetail, promotionInfo.NextRenewDate.Value);

                                var oldBillRun = new BillRun
                                {
                                    StarteDate = oldBillRunStartDate,
                                    EndDate = promotionInfo.NextRenewDate.Value.AddSeconds(-1)
                                };

                                var newBillRun = new BillRun
                                {
                                    StarteDate = promotionInfo.NextRenewDate.Value,
                                    EndDate = newBillRunEndDate
                                };

                                if (!promotionInfo.PreActionsExecuted)
                                {
                                    if (logger.IsDebugEnabled)
                                        logger.DebugFormat("begin pre-renewal actions, customer: {0}", customerid);

                                    RunPreRenewActions(promotionInfo, oldBillRun, newBillRun);
                                    promotionInfo.PreActionsExecuted = true;
                                }

                                if (logger.IsDebugEnabled)
                                    logger.DebugFormat("begin renewal actions, customer: {0}", customerid);

                                RunRenewActions(promotionInfo, oldBillRun, newBillRun);
                                promotionInfo.ActionsExecuted = true;
                                promotionInfo.Active = false;

                                if (logger.IsDebugEnabled)
                                    logger.DebugFormat("set the ActionsExecuted to true, customer: {0}", customerid);

                                promotionRepo.Update(promotionInfo);
                            });

                            trx.Commit();
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Error Renewing customer:" + customerid, ex);
                            trx.Rollback();
                        }
                    }
                }
            }
        }



        public void PreRenewCustomersBenefits(List<int> customerIdsToRenew)
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var promotionRepo = RepositoryManager.GetRepository<ICrmCustomersPromotionInfoRepository<CrmCustomersPromotionInfo>>();

                foreach (var customerid in customerIdsToRenew)
                {
                    if(logger.IsDebugEnabled)
                        logger.DebugFormat("begin process customer: {0}", customerid);

                    using (var trx = conn.BeginTransaction())
                    {
                        try
                        {
                            var listToPreRenew = promotionRepo.GetCustomerPromotionPlanById(customerid, null).Where(ee => ee.PreRenewalActionsDate <= System.DateTime.Now && !ee.PreActionsExecuted).ToList();

                            listToPreRenew.ForEach((promotionInfo) =>
                            {
                                DateTime oldBillRunStartDate = UtiltyHelper.GetOldBillRunStartDate(promotionInfo.PromotionDetail, promotionInfo.NextRenewDate.Value);
                                DateTime newBillRunEndDate = UtiltyHelper.GetNewBillRunEndDate(promotionInfo.PromotionDetail, promotionInfo.NextRenewDate.Value);

                                var oldBillRun = new BillRun
                                {
                                    StarteDate = oldBillRunStartDate,
                                    EndDate = promotionInfo.NextRenewDate.Value.AddSeconds(-1)
                                };

                                var newBillRun = new BillRun
                                {
                                    StarteDate = promotionInfo.NextRenewDate.Value,
                                    EndDate = newBillRunEndDate
                                };

                                if (logger.IsDebugEnabled)
                                    logger.DebugFormat("begin pre-renewal actions, customer: {0}", customerid);

                                RunPreRenewActions(promotionInfo, oldBillRun, newBillRun);
                                promotionInfo.PreActionsExecuted = true;
                                promotionRepo.Update(promotionInfo);

                                if (logger.IsDebugEnabled)
                                    logger.DebugFormat("complete pre-renewal actions: {0}", customerid);

                            });
                            trx.Commit();
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Error Executing prerenew for customer:" + customerid, ex);
                            trx.Rollback();
                        }
                    }

                    if (logger.IsDebugEnabled)
                        logger.DebugFormat("end process customer: {0}", customerid);
                   
                }
            }
        }
    }
}


