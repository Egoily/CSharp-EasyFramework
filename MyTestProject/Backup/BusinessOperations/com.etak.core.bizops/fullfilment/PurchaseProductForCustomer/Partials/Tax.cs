using System;
using System.Linq;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.contract.exceptions;

namespace com.etak.core.bizops.fullfilment.PurchaseProductForCustomer
{
    /// <summary>
    /// Partial Implementation BizOp PurchaseProductForCustomer
    /// </summary>
    public partial class PurchaseProductForCustomerBizOp
    {

        #region Private Methods

        /// <summary>
        /// getPrice
        /// </summary>
        /// <param name="charge"></param>
        /// <param name="datePurchase"></param>
        /// <returns></returns>
        private ChargePrice getPrice(Charge charge, DateTime datePurchase)
        {
            var chargePrice =
                charge.Prices.OrderByDescending(x => x.StartDate).
                    FirstOrDefault(x => (x.EndDate == null && datePurchase >= x.StartDate) || (datePurchase >= x.StartDate && datePurchase <= x.EndDate));

            if (chargePrice == null)
                throw new InternalErrorException(String.Format("There is not a price for ChargeID {0} and the date:{1}", charge.Id, datePurchase.ToString()), BizOpsErrors.NoPriceForCharge);

            return chargePrice;
        }   
     

        /// <summary>
        /// getTaxRate
        /// </summary>
        /// <param name="taxDefinition"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private TaxRates getTaxRate(TaxDefinition taxDefinition, DateTime dateTime)
        {
            var taxRates = taxDefinition.Rates.OrderByDescending(x => x.StartDate).FirstOrDefault(x => (x.EndDate == null && dateTime >= x.StartDate)
                                                                    || (dateTime >= x.StartDate && dateTime <= x.EndDate));

            if (taxRates == null)
                throw new InternalErrorException(string.Format("There is not a TaxRate for the TaxDefinitionID:{0}", taxDefinition.Id), BizOpsErrors.NoTaxRatesForTaxDefinitionId);

            return taxRates;
        }

        #endregion
    }
}
