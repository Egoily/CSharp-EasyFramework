using System;
using System.Collections.Generic;
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
    public class Utilities
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// create a customer charge
        /// </summary>
        /// <param name="chargeId"></param>
        /// <param name="invoice"></param>
        /// <param name="account"></param>
        /// <param name="amount"></param>
        /// <param name="chargeDate"></param>
        /// <param name="customerInfo"></param>
        /// <param name="customerChargeRepo"></param>
        public static void CreateCustomerCharge(int chargeId, Invoice invoice, Account account, decimal amount, DateTime chargeDate, CustomerInfo customerInfo, IRepository<CustomerCharge, long> customerChargeRepo)
        {
            var chargeRepo = RepositoryManager.GetRepository<IChargeRepository<Charge>>();
            var charge = chargeRepo.GetById(chargeId);
            if (charge == null)
            {
                Logger.Error(string.Format("The charge not found.ChargeId={0}", chargeId));
                return;
            }

            //add by Oliver20150415 for CFP-115
            if (amount <= 0)
            {
                if (Logger.IsDebugEnabled)
                    Logger.Debug(string.Format("it is no need to create the charge with is InformationalAmount<=0."));
                return;
            }

            var informationalCharge = new CustomerCharge()
            {
                Customer = customerInfo,
                ChargingAccount = invoice != null ? invoice.ChargingAccount : account,
                ChargeDefinition = charge,
                Invoice = invoice,
                ChargingDate = chargeDate,
                InformationalAmount = amount,
                //Currency = ISO4217CurrencyCodes.EUR,
            };
            //Create charge
            customerChargeRepo.Create(informationalCharge);
            if (Logger.IsDebugEnabled)
                Logger.Debug(string.Format("Inserted InformationalCharge:ChargeID={0},CUSTOMER_CHARGEID={1}", chargeId, informationalCharge.Id));
        }

        public static void CreateCustomerCharge(Charge charge, Invoice invoice, Account account, decimal amount, DateTime chargeDate, CustomerInfo customerInfo, IRepository<CustomerCharge, long> customerChargeRepo)
        {
            if (charge == null)
            {
                Logger.Error(string.Format("Cannot create null charge."));
                return;
            }

            //add by Oliver20150415 for CFP-115
            if (amount <= 0)
            {
                if (Logger.IsDebugEnabled)
                    Logger.Debug(string.Format("it is no need to create the charge ({0}) for customer({1}) because InformationalAmount<=0.", charge.Id, customerInfo.CustomerID));
                return;
            }

            var informationalCharge = new CustomerCharge()
            {
                Customer = customerInfo,
                ChargingAccount = invoice != null ? invoice.ChargingAccount : account,
                ChargeDefinition = charge,
                Invoice = invoice,
                ChargingDate = chargeDate,
                InformationalAmount = amount,
                //Currency = ISO4217CurrencyCodes.EUR,
            };
            //Create charge
            customerChargeRepo.Create(informationalCharge);
            if (Logger.IsDebugEnabled)
                Logger.Debug(string.Format("Inserted InformationalCharge:ChargeID={0},CUSTOMER_CHARGEID={1}", charge.Id, informationalCharge.Id));
        }

        /// <summary>
        /// get customer current Account
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <returns></returns>
        public static Account GetCustomerCurrentAccount(CustomerInfo customerInfo)
        {
            var customerAccountRepo = RepositoryManager.GetRepository<ICustomerAccountAssociationRepository<CustomerAccountAssociation>>();
            var accounts = customerAccountRepo.AllAsociationsForCustomer(customerInfo).ToList();
            var currentAccount = accounts.FirstOrDefault(x =>
                x.StartDate <= DateTime.Now
                && (!x.EndTime.HasValue || x.EndTime > DateTime.Now));

            return currentAccount != null ? currentAccount.Account : null;
        }

        /// <summary>
        /// generate paid Dictionary,key=PromotionPlanId value=(true:PAID,false:FREE)
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <returns></returns>
        public static Dictionary<int, bool> GeneratePaidOfCustomerPurchasedProduct(CustomerInfo customerInfo)
        {
            var repo = RepositoryManager.GetRepository<ICustomerProductAssignmentRepository<CustomerProductAssignment>>();
            var customerProducts = repo.GetByCustomerId(customerInfo.CustomerID ?? 0).ToList();
            if (!customerProducts.Any())
                return null;

            var lstCustomerProducts = customerProducts.Where(
                    x => x.PurchasedProduct.AssociatedPrmotionPlan != null
                        && x.StartDate <= DateTime.Now
                        && (!x.EndDate.HasValue || x.EndDate > DateTime.Now));

            var result = new Dictionary<int, bool>();
            foreach(var item in lstCustomerProducts)
            {
                if (result.ContainsKey(item.PurchasedProduct.AssociatedPrmotionPlan.PromotionPlanId))
                    result[item.PurchasedProduct.AssociatedPrmotionPlan.PromotionPlanId] = IsPaidProduct(item.PurchasedProduct);
                else
                    result.Add(item.PurchasedProduct.AssociatedPrmotionPlan.PromotionPlanId, IsPaidProduct(item.PurchasedProduct));
            }
            return result;
        }

        /// <summary>
        /// Product is PAID or FREE
        /// </summary>
        /// <param name="product"></param>
        /// <returns>true:PAID,false:FREE</returns>
        public static bool IsPaidProduct(Product product)
        {
            return IsPaidProduct(product.ChargingOptions);
        }

        /// <summary>
        ///  Product is PAID or FREE
        /// </summary>
        /// <param name="productChargeOptions"></param>
        /// <returns>true:PAID,false:FREE</returns>
        public static bool IsPaidProduct(IEnumerable<ProductChargeOption> productChargeOptions)
        {
            var totalPrice = productChargeOptions.ToList().Sum(chargingOption => chargingOption.Charges.Sum(charge => charge.Prices.ToList().Sum(price => price.Amount)));

            return totalPrice > 0;
        }

        /// <summary>
        /// query group member except accPromotionPlanId
        /// </summary>
        /// <param name="mvnoId"></param>
        /// <param name="accPromotionPlanId"></param>
        /// <returns></returns>
        public static IList<RmPromotionPlanDetailInfo> QueryNonAccumulatedMembers(int mvnoId, int accPromotionPlanId)
        {
            var rmPromotionGroupMemberRepo = RepositoryManager.GetRepository<IRmPromotionGroupMemberRepository<RmPromotionGroupMember>>();
            //get all group members of current mvno
            var allGroupMembers = rmPromotionGroupMemberRepo.GetAll().Where(x => x.PromotionGroup.MvnoID == mvnoId).ToList();

            var nonAccumulatedMembers = new List<RmPromotionPlanDetailInfo>();

            var member = allGroupMembers.FirstOrDefault(x => x.PromotionPlan.PromotionPlanId == accPromotionPlanId && x.PromotionGroup.GroupType == 1);
            if (member != null)
            {
                nonAccumulatedMembers =
                    member.PromotionGroup.Members.Where(
                        x => x.PromotionPlan.PromotionPlanId != member.PromotionPlan.PromotionPlanId)
                        .Select(x => x.PromotionPlan)
                        .SelectMany(x => x.RmPromotionPlanDetailInfoList)
                        .ToList();
            }
            return nonAccumulatedMembers;
        }

        /// <summary>
        /// Total limit for Accumulated FREE Promotions
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <param name="billRun"></param>
        /// <param name="nonAccumulatedMembers"></param>
        /// <param name="paidDictionary"></param>
        /// <returns></returns>
        public static decimal Total4AccumulatedFreePromotion(CustomerInfo customerInfo, BillRun billRun, ICollection<RmPromotionPlanDetailInfo> nonAccumulatedMembers, IDictionary<int, bool> paidDictionary)
        {
            //query accumulated promotions which are in the same group as the accPromotion's.
            var accumulatedPromotions = customerInfo.Promotions.Where(x =>
                         x.StartDate >= billRun.StarteDate
                         && x.PromotionDetail.Periodicity <= 0
                         && x.EndDate.HasValue
                         && x.StartDate < x.EndDate
                         && x.EndDate >= billRun.EndDate && x.EndDate <= billRun.EndDate.AddSeconds(1)
                         && nonAccumulatedMembers.Contains(x.PromotionDetail)
                         && !paidDictionary[x.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId]
                         ).ToList();

            var result = accumulatedPromotions.Sum(x => x.CurrentLimit);

            return result;
        }

        /// <summary>
        /// Total limit for Accumulated PAID Promotions
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <param name="billRun"></param>
        /// <param name="nonAccumulatedMembers"></param>
        /// <param name="paidDictionary"></param>
        /// <returns></returns>
        public static decimal Total4AccumulatedPaidPromotion(CustomerInfo customerInfo, BillRun billRun, ICollection<RmPromotionPlanDetailInfo> nonAccumulatedMembers, IDictionary<int, bool> paidDictionary)
        {
            //query accumulated promotions which are in the same group as the accPromotion's.
            var accumulatedPromotions = customerInfo.Promotions.Where(x =>
                         x.StartDate >= billRun.StarteDate
                         && x.EndDate.HasValue 
                         && x.StartDate < x.EndDate
                         && x.EndDate >= billRun.EndDate && x.EndDate<=billRun.EndDate.AddSeconds(1)
                         && x.PromotionDetail.Periodicity <= 0
                         && nonAccumulatedMembers.Contains(x.PromotionDetail)
                         && paidDictionary[x.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId]
                         ).ToList();
            //cumulate limit for accumulated promotion
            var result = accumulatedPromotions.Sum(x => x.CurrentLimit);

            return result;
        }

        /// <summary>
        /// Total limit for Recurring Promotions
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <param name="billRun"></param>
        /// <param name="nonAccumulatedMembers"></param>
        /// <returns></returns>
        public static decimal Total4RecurringPromotion(CustomerInfo customerInfo, BillRun billRun, ICollection<RmPromotionPlanDetailInfo> nonAccumulatedMembers, bool isNextBillRun = false)
        {
            //query recurring promotions which are in the same group as the accPromotion's.
            var periodicPromotions = customerInfo.Promotions.Where(x =>
                (x.PromotionDetail.Periodicity > 0)//is periodic promotion
                && x.StartDate >= billRun.StarteDate
                && x.EndDate.HasValue
                && x.StartDate < x.EndDate
                && x.EndDate >= billRun.EndDate
                && x.EndDate <= billRun.EndDate.AddSeconds(1)
                && nonAccumulatedMembers.Contains(x.PromotionDetail)
                ).ToList();
            //cumulate limit for recurring promotion
            decimal result = 0;
            if (!isNextBillRun)
            {
                result = periodicPromotions.Sum(x => x.CurrentLimit);
            }
            else
            {
                result = periodicPromotions.Sum(x => x.PromotionDetail.Limit);
            }

            return result;
        }
    }
}