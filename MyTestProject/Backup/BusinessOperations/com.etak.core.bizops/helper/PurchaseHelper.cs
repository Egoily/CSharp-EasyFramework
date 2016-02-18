using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.dealer.messages.GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItem;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.manager;
using com.etak.core.product.message.GetProductByProductId;
using com.etak.core.product.message.GetProductOfferingByProductOfferingId;

namespace com.etak.core.bizops.helper
{
    /// <summary>
    /// TypeOfPurchaseProduct
    /// </summary>
    public enum TypeOfPurchaseProduct
    {
        /// <summary>
        /// standard PurchaseProduct 
        /// </summary>
        PurchaseProduct = 0,
        /// <summary>
        /// when Register is called
        /// </summary>
        Register = 1,
        /// <summary>
        /// when Portability In is called
        /// </summary>
        PortIn = 2,
        /// <summary>
        /// when Benefit transfer is called
        /// </summary>
        BenefitTransfer = 3,
        /// <summary>
        /// when CheckPurchaseProduct is called
        /// </summary>
        CheckPurchaseProduct = 4
    }

    /// <summary>
    /// Class to contain all the operations related to Purchase Product bizOp
    /// 
    /// Business Operations affected:
    /// - Register
    /// - PurchaseProduct
    /// - Port In (to be done)
    /// </summary>
    public class PurchaseHelper : IPurchaseHelper
    {



        #region Ini vars
        /// <summary>
        /// Contains a list of all the products to purchase
        /// </summary>
        private List<Tuple<ProductOffering, ProductChargeOption>> listPurchaseProducts = new List<Tuple<ProductOffering, ProductChargeOption>>();

        #endregion

        #region Public Methods

        /// <summary>
        /// GetCustomer4GProducts
        /// </summary>
        /// <param name="productsToPurchase"></param>
        /// <param name="mvnoId"></param>
        /// <returns></returns>
        public virtual IEnumerable<Product> GetCustomer4GProducts(List<Product> productsToPurchase, int mvnoId)
        {
            if (!productsToPurchase.Any())
                return null;
            var productIds4G = Get4GProductIdsConfiguration(mvnoId);
            var result = productsToPurchase.Where(x => productIds4G.Contains(x.Id.ToString()));
            return result;
        }


        /// <summary>
        /// Main operation that will take all the Products form the list, and get the corresponding Charge Option.
        /// If the chargeoption is set, that will be the one putted in the dictionary. If not,
        /// the ChargeOption marked as Default will be the one putted in the dictionary.
        /// </summary>
        /// <param name="productsDto">A list of ProductCatalogDTO, with the list ChargingOptions containing the Charge Option to be used</param>
        /// <returns></returns>
        public virtual List<Tuple<ProductOffering, ProductChargeOption>> GetProductsAndChargesOptions(List<ProductCatalogDTO> productsDto)
        {
            var getProductMs = MicroServiceManager.GetMicroService<GetProductOfferingByProductOfferingIdRequest, GetProductOfferingByProductOfferingIdResponse>();
            var getProductReq = new GetProductOfferingByProductOfferingIdRequest();
            var getProductResp = new GetProductOfferingByProductOfferingIdResponse();

            foreach (var item in productsDto)
            {
                getProductReq.ProductOfferingId = (int)item.Id;
                getProductResp = getProductMs.Process(getProductReq, null);
                if (getProductResp.ResultType != ResultTypes.Ok || getProductResp.ProductOffering == null)
                    throw new BusinessLogicErrorException(String.Format("Unable to find information for the ProductOfferingID:{0}", item.Id), BizOpsErrors.ProductNotFound);

                var product = getProductResp.ProductOffering;

                AddProductAndCharge(product, item.PurchaseOptions.ToList());

            }

            return listPurchaseProducts;
        }

        /// <summary>
        /// Check if is Recurring Charge
        /// </summary>
        /// <param name="charges"></param>
        /// <returns></returns>
        public bool IsRecurringCharge(IEnumerable<Charge> charges)
        {
            return charges != null && charges.Any() && charges.Count(x => x is ChargeRecurring) > 0;

        }
        /// <summary>
        /// Check if is necessary to disable 4G service
        /// </summary>
        /// <param name="customerProductAssignments"></param>
        /// <param name="customerProductAssignmentToCancel"></param>
        /// <param name="mvnoId"></param>
        /// <returns></returns>
        public bool IsNeedtoDisable4GService(IEnumerable<CustomerProductAssignment> customerProductAssignments, CustomerProductAssignment customerProductAssignmentToCancel, int mvnoId)
        {
            if (customerProductAssignmentToCancel == null || customerProductAssignmentToCancel.ProductChargePurchased == null)
            {
                return false;
            }
            if (customerProductAssignmentToCancel.ProductChargePurchased != null
                && customerProductAssignmentToCancel.ProductChargePurchased.Charges != null
                && IsRecurringCharge(customerProductAssignmentToCancel.ProductChargePurchased.Charges))
            {
                return false;
            }
            var now = DateTime.Now;
            var toCancelPurchasedProductId = customerProductAssignmentToCancel.PurchasedProduct.Id;
            var productIds4G = Get4GProductIdsConfiguration(mvnoId).ToList();
            if (!productIds4G.Any() || !productIds4G.Contains(toCancelPurchasedProductId.ToString()))
            {
                return false;
            }
            var availableCustomer4GProductAssignments = customerProductAssignments
                .Where(x => x.StartDate <= now
                        && (!x.EndDate.HasValue || x.EndDate > now)
                        && productIds4G.Contains(x.PurchasedProduct.Id.ToString())).ToList();

            return (!availableCustomer4GProductAssignments.Any());

        }

        /// <summary>
        /// check if the actual bizop is called from another bizop (Regriter or portin) or directly by PurchaseProduct API
        /// </summary>
        /// <param name="typeOfPurchaseProductOperation"></param>
        /// <returns></returns>
        public static bool IsNotCallingPurchaseProductType(TypeOfPurchaseProduct typeOfPurchaseProductOperation)
        {
            return typeOfPurchaseProductOperation != TypeOfPurchaseProduct.PurchaseProduct;
        }

        /// <summary>
        /// Obtain if a ProductChargeOption have recurrent charges
        /// </summary>
        /// <param name="productChargeOption"></param>
        /// <returns>true if exists a recurring Charge</returns>
        public static bool HasRecurringCharge(ProductChargeOption productChargeOption)
        {
            bool isRecurring = productChargeOption != null && productChargeOption.Charges != null &&
                               productChargeOption.Charges.Count(x => x is ChargeRecurring) > 0;

            return isRecurring;
        }

        /// <summary>
        /// get start date from specific ProductChargeOption and type of purchase product
        /// </summary>
        /// <param name="productChargeOption"></param>
        /// <param name="datetimePurchase"></param>
        /// <param name="nextBillRunDate"></param>
        /// <param name="typeOfPurchaseProductOperation"></param>
        /// <returns></returns>
        public static DateTime GetStartDateByProductChargeOptionAndTypeOfPurchaseProduct(ProductChargeOption productChargeOption,
            DateTime datetimePurchase,
            DateTime nextBillRunDate,
            TypeOfPurchaseProduct typeOfPurchaseProductOperation)
        {
            DateTime datetime;
            var isRecurring = HasRecurringCharge(productChargeOption);

            if (isRecurring)
            {
                datetime = IsNotCallingPurchaseProductType(typeOfPurchaseProductOperation) ? datetimePurchase : nextBillRunDate;
            }
            else
            {
                // non-recurring
                datetime = datetimePurchase;
            }

            return datetime;
        }

        #endregion

        #region Private methods
        
        /// <summary>
        /// Recursive function to get all the childs of the product and their respectives charge options
        /// </summary>
        /// <param name="product"></param>
        /// <param name="chargingOptionDtos"></param>
        private void AddProductAndCharge(ProductOffering product, List<ProductPurchaseChargingOptionDTO> chargingOptionDtos)
        {
            ProductChargeOption chargeOption;
            ProductPurchaseChargingOptionDTO chargeOptionDto = chargingOptionDtos != null ? chargingOptionDtos.FirstOrDefault() : null;
            if (chargeOptionDto != null)
            {

                chargeOption = product.ChargingOptions.FirstOrDefault(x => x.Id == chargeOptionDto.Id);
                if (chargeOption == null)
                    throw new BusinessLogicErrorException(String.Format("Must exist one PurchaeseOption for the ProductID:{0} with Charging Option Id:{1}", product.Id, chargeOptionDto.Id),
                                                                        BizOpsErrors.ProductNotFound);
            }
            else
            {
                chargeOption = product.ChargingOptions.FirstOrDefault(x => x.IsDefaultOption == DefaultOptions.Y);
                if (chargeOption == null)
                    throw new BusinessLogicErrorException(String.Format("Must exist one PurchaeseOption for the ProductID:{0} with a default option", product.Id), BizOpsErrors.ProductNotFound);
            }

            listPurchaseProducts.Add(Tuple.Create(product, chargeOption));

            //As we want to get all the Products "Mandatory" for the Product Offering, we are going to get the "Childs", that means,
            //all the product options that are ProductOfferingSpecificationOption with MinOccurs equal to 1.
            var childList = new List<ProductOffering>();
            if (product.Options != null)
                childList = product.Options.Where(x => x is ProductOfferingSpecificationOption && x.MinOccurs == 1).Select(x => (x as ProductOfferingSpecificationOption).SpecifiedProductOffering).ToList();

            if (!childList.Any()) return;
            foreach (var child in childList)
            {
                AddProductAndCharge(child, null);
            }
        }


        /// <summary>
        /// Get4GProductIdsConfiguration
        /// </summary>
        /// <param name="mvnoId"></param>
        /// <returns></returns>
        private IEnumerable<string> Get4GProductIdsConfiguration(int mvnoId)
        {

            const int categoryId = 1000;
            const string item = "4GProductIds";
            var separater = new char[]
			{
				',',
				';',
			};

            var getMvnoConfigActionInfosByMvnoIdAndCategoryIdAndItemMs = MicroServiceManager.GetMicroService<GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest, GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResponse>();

            var config =
                Enumerable.FirstOrDefault(getMvnoConfigActionInfosByMvnoIdAndCategoryIdAndItemMs.Process(
                        new GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest()
                        {
                            MvnoId = mvnoId,
                            CategoryId = categoryId,
                            Item = item,
                            StatusId = 1
                        }, null).MvnoConfigActionInfos);


            return config != null
                ? config.Value.Split(separater, StringSplitOptions.RemoveEmptyEntries)
                : new string[]
				{
				};
        }



        #endregion
    }
}
