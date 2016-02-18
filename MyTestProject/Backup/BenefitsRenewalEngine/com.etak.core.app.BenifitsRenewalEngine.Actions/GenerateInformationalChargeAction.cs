using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository;
using com.etak.core.repository.crm;
using com.etak.core.repository.crm.revenueManagement;
using log4net;

namespace com.etak.core.app.BenifitsRenewalEngine.Actions
{
    public enum ChargeTypes
    {
        Recurring,
        NextAccumulated,
        CurrentAccumulated,
        CurrentRecurring,
        CurrentNonRecurringPaid,
        CurrentNonRecurringFree,
        NextRecurring,
        NextNonRecurringPaid,
        NextNRecurringFree,
    }

    public class GenerateInformationalChargeAction : AbstractPromotionRenewAction
    {
        /*
         INVOICE_OBJECT_ID	/CHARGEID/    variable                          description
         23	                /60/          Recurring                         DESCRIPTION CHARGE AGGREGATE Total Data Limit Recurring Products
         76	                /61/          NextAccumulated                   DESCRIPTION CHARGE AGGREGATE Accumulated Data, Previous Month
         27	                /62/          CurrentAccumulated                DESCRIPTION CHARGE AGGREGATE Accumulated Data, Current Month
         86	                /  /          CurrentRecurring                  Accumulated Data Recurring, Current Month
         87	                /  /          CurrentNonRecurringPaid           Accumulated Data Non-Recurring PAID, Current Month
         88	                /  /          CurrentNonRecurringFree           Accumulated Data Non-Recurring FREE, Current Month
         89	                /  /          NextRecurring                     Accumulated Data Recurring, Previous Month
         90	                /  /          NextNonRecurringPaid              Accumulated Data Non-Recurring PAID, Previous Month
         91	                /  /          NextNRecurringFree                Accumulated Data Non-Recurring FREE, Previous Month
       */
        private static readonly string ClassName = MethodBase.GetCurrentMethod().DeclaringType.ToString();
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private int accPromotionPlanId;
        private List<int> chargeIds;
        private List<Charge> charges = new List<Charge>();
        private List<MVNOConfigActionInfo> configurations = new List<MVNOConfigActionInfo>();
        private int mvnoId;

        public GenerateInformationalChargeAction()
        {
        }

        public GenerateInformationalChargeAction(RmPromotionPlanDetailInfo preReferredPromotion, RmPromotionPlanDetailInfo referredPromotion, string actionType, int priority)
        {
            base.PreRefferredPromotion = preReferredPromotion;
            base.RefferredPromotion = referredPromotion;
            base.ActionType = actionType;
            base.Priority = priority;
        }

        public override void Renew(CustomerInfo customerInfo, BillRun currentBillRun, BillRun nextBillRun)
        {
            try
            {
                Validate(customerInfo, currentBillRun, nextBillRun);
                Initialize();
                InsertInformationalCharge(customerInfo, currentBillRun, nextBillRun);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Error at {0}", ClassName), ex);
                throw;
            }
        }

        private static IEnumerable<MVNOConfigActionInfo> GetChargeIdConfiguration(int mvnoId)
        {
            const int categoryId = 1001;
            try
            {
                var repo = RepositoryManager.GetRepository<IMVNOConfigActionInfoRepository<MVNOConfigActionInfo>>();
                var configs = repo.GetMVNOConfigsByMvnoIdAndCategoryId(mvnoId, categoryId, 1);

                return configs;
            }
            catch (Exception ex)
            {
                Logger.Error("Error on GetChargeIdConfiguration:", ex);
                throw;
            }
        }

        private static IEnumerable<Charge> GetCharges(IEnumerable<string> chargeIds)
        {
            try
            {
                var chargeRepo = RepositoryManager.GetRepository<IChargeRepository<Charge>>();
                var result = chargeIds.Select(chargeId => chargeRepo.GetById(int.Parse(chargeId))).Where(charge => charge != null).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error on GetCharges:", ex);
                throw;
            }
        }

        private Charge GetCharge(ChargeTypes type)
        {
            try
            {
                if (!configurations.Any() || !charges.Any()) return null;
                var config = configurations.FirstOrDefault(x => x.Item == type.ToString());
                return config != null ? charges.FirstOrDefault(x => x.Id.ToString(CultureInfo.InvariantCulture) == config.Value) : null;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Error on GetChargeIdConfiguration:ChargeType={0}", type.ToString()), ex);
                throw;
            }
        }

        private void Initialize()
        {
            //get renewing promotionPlanId
            accPromotionPlanId = RefferredPromotion != null ? RefferredPromotion.RmPromotionPlanInfo.PromotionPlanId : PreRefferredPromotion.RmPromotionPlanInfo.PromotionPlanId;

            //get current mvnoId
            mvnoId = RefferredPromotion != null ? RefferredPromotion.RmPromotionPlanInfo.DealerId : PreRefferredPromotion.RmPromotionPlanInfo.DealerId;
            // get configuration of chargeId
            //TODO: CONFFIGACTION should be cached
            configurations = GetChargeIdConfiguration(mvnoId).ToList();
            // get charges based on chargeId configured.
            //TODO: CHARGES should be cached
            charges = GetCharges(configurations.Select(x => x.Value)).ToList();
        }

        private void InsertInformationalCharge(CustomerInfo customerInfo, BillRun currentBillRun, BillRun nextBillRun)
        {
            if (Logger.IsDebugEnabled)
                Logger.Debug(string.Format("GenerateInformationalChargeAction Begin:CustomerId={0},accPromotionPlanId={1},currentBillRun[{2}-{3}],nextBillRun[{4}-{5}]",
                     customerInfo.CustomerID, accPromotionPlanId, currentBillRun.StarteDate, currentBillRun.EndDate, nextBillRun.StarteDate, nextBillRun.EndDate));

            var customerChargeRepo = RepositoryManager.GetRepository<ICustomerChargeRepository<CustomerCharge>>();
            var invoiceRepo = RepositoryManager.GetRepository<IInvoiceRepository<Invoice>>();

            //get non-accumulated members
            var nonAccumulatedMembers = Utilities.QueryNonAccumulatedMembers(mvnoId, accPromotionPlanId);

            //get last 2 invoices of current customer
            var invoices = invoiceRepo.GetLastNInvoices(customerInfo.CustomerID ?? 0, 2).ToList();
            var currentInvoice = invoices.FirstOrDefault(x => x.StartDate >= currentBillRun.StarteDate && x.EndDate <= currentBillRun.EndDate.AddSeconds(1));
            var nextInvoice = invoices.FirstOrDefault(x => x.StartDate >= nextBillRun.StarteDate && x.EndDate <= nextBillRun.EndDate.AddSeconds(1));
            //get Account for customer
            var account = Utilities.GetCustomerCurrentAccount(customerInfo);
            //Generate Paid Of Customer Purchased Product:true=paid,false=Free
            var paidDictionary = Utilities.GeneratePaidOfCustomerPurchasedProduct(customerInfo);

            //cumulate limit for recurring promotion
            var totalLimit4CurrentRecurringPromotion = Utilities.Total4RecurringPromotion(customerInfo, currentBillRun, nonAccumulatedMembers);
            //cumulate limit for recurring promotion
            var totalLimit4NextRecurringPromotion = Utilities.Total4RecurringPromotion(customerInfo, nextBillRun, nonAccumulatedMembers, true);
            //cumulate limit for Accumulated PAID promotion
            var totalLimit4AccumulatedPaidPromotion = Utilities.Total4AccumulatedPaidPromotion(customerInfo, currentBillRun, nonAccumulatedMembers, paidDictionary);
            //cumulate limit for Accumulated FREE Promotion
            var totalLimit4AccumulatedFreePromotion = Utilities.Total4AccumulatedFreePromotion(customerInfo, currentBillRun, nonAccumulatedMembers, paidDictionary);

            #region generate charges

            //if (nextInvoice != null && nextInvoice.ChargingAccount != null)
            {
                //recurringInformationalCharge
                Utilities.CreateCustomerCharge(GetCharge(ChargeTypes.Recurring), nextInvoice, account, totalLimit4NextRecurringPromotion,
                    nextBillRun.StarteDate, customerInfo, customerChargeRepo);

                //NextAccumulatedInformationalCharge
                Utilities.CreateCustomerCharge(GetCharge(ChargeTypes.NextAccumulated), nextInvoice, account, totalLimit4CurrentRecurringPromotion + totalLimit4AccumulatedPaidPromotion + totalLimit4AccumulatedFreePromotion,
                    nextBillRun.StarteDate, customerInfo, customerChargeRepo);

                //NextPaidNonRecurringInformationalCharge
                Utilities.CreateCustomerCharge(GetCharge(ChargeTypes.NextNonRecurringPaid), nextInvoice, account, totalLimit4AccumulatedPaidPromotion,
                    nextBillRun.StarteDate, customerInfo, customerChargeRepo);

                //NextNonRecurringFreeInformationalCharge
                Utilities.CreateCustomerCharge(GetCharge(ChargeTypes.NextNRecurringFree), nextInvoice, account, totalLimit4AccumulatedFreePromotion,
                    nextBillRun.StarteDate, customerInfo, customerChargeRepo);

                //NextRecurringChargeInformationalCharge
                Utilities.CreateCustomerCharge(GetCharge(ChargeTypes.NextRecurring), nextInvoice, account, totalLimit4CurrentRecurringPromotion,
                    nextBillRun.StarteDate, customerInfo, customerChargeRepo);
            }
            //if (currentInvoice != null && currentInvoice.ChargingAccount != null)
            {
                // CurrentAccumulatedInformationalCharge
                Utilities.CreateCustomerCharge(GetCharge(ChargeTypes.CurrentAccumulated), currentInvoice, account, totalLimit4CurrentRecurringPromotion + totalLimit4AccumulatedPaidPromotion + totalLimit4AccumulatedFreePromotion,
                  currentBillRun.EndDate, customerInfo, customerChargeRepo);

                //CurrentNonRecurringPaidInformationalCharge
                Utilities.CreateCustomerCharge(GetCharge(ChargeTypes.CurrentNonRecurringPaid), currentInvoice, account, totalLimit4AccumulatedPaidPromotion,
                  currentBillRun.EndDate, customerInfo, customerChargeRepo);

                //CurrentNonRecurringFreeInformationalCharge
                Utilities.CreateCustomerCharge(GetCharge(ChargeTypes.CurrentNonRecurringFree), currentInvoice, account, totalLimit4AccumulatedFreePromotion,
                  currentBillRun.EndDate, customerInfo, customerChargeRepo);

                //CurrentRecurringInformationalCharge
                Utilities.CreateCustomerCharge(GetCharge(ChargeTypes.CurrentRecurring), currentInvoice, account, totalLimit4CurrentRecurringPromotion,
                  currentBillRun.EndDate, customerInfo, customerChargeRepo);
            }

            #endregion generate charges

            if (Logger.IsDebugEnabled)
                Logger.Debug(string.Format("GenerateInformationalChargeAction End:CustomerId={0},promotionPlanId={1},totalLimit4CurrentRecurringPromotion={2},totalLimit4NextRecurringPromotion={3},totalLimit4AccumulatedPaidPromotion={4},totalLimit4AccumulatedFreePromotion={5}",
                       customerInfo.CustomerID, accPromotionPlanId, totalLimit4CurrentRecurringPromotion, totalLimit4NextRecurringPromotion, totalLimit4AccumulatedPaidPromotion, totalLimit4AccumulatedFreePromotion));
        }

        private void Validate(CustomerInfo customerInfo, BillRun currentBillRun, BillRun nextBillRun)
        {
            if (RefferredPromotion == null && PreRefferredPromotion == null)
            {
                throw new Exception("ReferredPromotion and PreRefferredPromotion are null.");
            }
            if (RefferredPromotion != null && PreRefferredPromotion != null)
            {
                throw new Exception("ReferredPromotion and PreRefferredPromotion are NOT null at the same time.");
            }
            if ((RefferredPromotion != null && !RefferredPromotion.RmPromotionPlanInfo.Accumulative) || (PreRefferredPromotion != null && !PreRefferredPromotion.RmPromotionPlanInfo.Accumulative))
            {
                throw new Exception("ReferredPromotion or PreRefferredPromotion is not a accumulative promotion.");
            }

            if (customerInfo == null || currentBillRun == null || nextBillRun == null)
            {
                throw new Exception("Error input.");
            }
            if (customerInfo.Promotions == null || !customerInfo.Promotions.Any())
            {
                throw new Exception("The customer inputted has no promotions.");
            }
        }
    }
}