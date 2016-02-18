using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using com.etak.core.customer.message.CreateCustomerCharge;
using com.etak.core.customer.message.CreateCustomerChargeSchedule;
using com.etak.core.customer.message.CreateInvoice;
using com.etak.core.customer.message.GetInvoicesByCustomerIdAndLegalInvoiceNumber;
using com.etak.core.dealer.messages.GetDealerInfoMVNOByDealerId;
using com.etak.core.dealer.messages.GetMVNOConfigActionInfosByIdAndName;
using com.etak.core.dealer.messages.GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItem;
using com.etak.core.microservices.messages.GetTaxAuthority;
using com.etak.core.model;
using com.etak.core.model.provisioning;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.product.message.GetProductChargeOptionByProductChargeOptionId;
using com.etak.core.product.message.GetProductChargeOptionsByProductId;
using com.etak.core.promotion.messages.CancelCustomersPromotion;
using com.etak.core.promotion.messages.GetCustomersPromotionInfosByCustomerIDAndPromotionPlanIds;
using com.etak.core.repository;
using com.etak.core.repository.crm.promotion;
using com.etak.core.service.messages.AddUnbilledBalance;
using com.etak.core.service.messages.GetServicesInfosByCustomerID;
using com.etak.core.service.messages.UpdateServicesInfoWithCustomCreditLimit;

namespace com.etak.core.bizops.fullfilment.PurchaseProductForCustomer
{
    /// <summary>
    /// Partial Implementation BizOp PurchaseProductForCustomer
    /// </summary>
    public partial class PurchaseProductForCustomerBizOp
    {

        #region Parameters Objects

        /// <summary>
        /// GetNextBillRunDate
        /// </summary>
        public class GetNextBillRunDateRequestObject
        {
            /// <summary>
            /// BillCycle definition
            /// </summary>
            public BillCycle BillCycleDefinition { get; set; }

            /// <summary>
            /// First Day Of Week. By default, it is set to Sunday.
            /// </summary>
            public DayOfWeek FirstDayOfWeek { get; set; }

            /// <summary>
            /// Date of purchase to calculate the next Bill Run Date in reference with this one
            /// </summary>
            public DateTime PurchaseTime { get; set; }
        }


        /// <summary>
        /// GetNextBillRunDateResponse
        /// </summary>
        public class GetNextBillRunDateResponseObject
        {
            /// <summary>
            /// Datetime of the next bill run
            /// </summary>
            public DateTime NextBillRun { get; set; }
        }



        /// <summary>
        /// CreateCustomerChargeResponse is used in CreateCustomerChargeMS
        /// </summary>
        public class CreateCustomerChargeResponseObject
        {
            /// <summary>
            ///  ChargeRecurring
            /// </summary>
            public CustomerChargeSchedule ChargeRecurring { get; set; }

            /// <summary>
            /// ChargeNonRecurring
            /// </summary>
            public CustomerCharge ChargeNonRecurring { get; set; }

            /// <summary>
            /// return if the customer has credit for purchase
            /// </summary>
            public bool HasCreditForPurchase { get; set; }


            /// <summary>
            /// CustomerCharge created
            /// </summary>
            public CustomerCharge CustomerCharge { get; set; }

        }


        /// <summary>
        /// CreateCustomerChargeRequest is used in CreateCustomerChargeMS
        /// </summary>
        public class CreateCustomerChargeRequestObject
        {

            /// <summary>
            /// CustomerCharge you wish to create
            /// </summary>
            public CustomerCharge CustomerCharge { get; set; }

            /// <summary>
            /// InsertLikeNonRecurring
            /// </summary>
            public bool InsertLikeNonRecurring { get; set; }

            /// <summary>
            /// StartDate
            /// </summary>
            public DateTime StartDate { get; set; }
        }


        #endregion

        #region Private Methods

        /// <summary>
        /// CreateInvoice
        /// </summary>
        /// <param name="nonRecurringChargeList"></param>
        /// <param name="customerInfo"></param>
        /// <param name="accountCustomer"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>

        private Invoice AccountCreateInvoice(List<CustomerCharge> nonRecurringChargeList,
            CustomerInfo customerInfo,
            Account accountCustomer,
            DateTime startDate,
            DateTime endDate)
        {


            CreateInvoiceRequest createAccountInvoiceRequest;
            CreateInvoiceResponse createAccountInvoiceResponse;

            createAccountInvoiceRequest = new CreateInvoiceRequest()
            {
                Invoice = new Invoice()
                {
                    ChargedCustomer = customerInfo,
                    Charges = nonRecurringChargeList,
                    ChargingAccount = accountCustomer,
                    StartDate = startDate,
                    EndDate = endDate
                }
            };

            createAccountInvoiceResponse = _createAccountInvoice.Process(createAccountInvoiceRequest, null);
            return createAccountInvoiceResponse.Invoice;
        }


        /// <summary>
        /// updateBundleWithCustomCreditLimit
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <param name="serviceInfoList"></param>
        /// <param name="forceCreditLimit"></param>
        /// <param name="startDate"></param>
        private void UpdateBundleWithCustomCreditLimit(CustomerInfo customerInfo,
            List<ServicesInfo> serviceInfoList,
            Decimal forceCreditLimit,
            DateTime startDate)
        {

         
            List<ServicesInfo> bundleList = null;

            if (serviceInfoList == null || serviceInfoList.Count == 0)
            {
                var serviceInfo =
                    Enumerable.ToList<ServicesInfo>(_getServiceByCusotmerId.Process(
                            new GetServicesInfosByCustomerIDRequest() {CustomerID = customerInfo.CustomerID.Value}, null).ServicesInfos);

                // repoServicesInfo.GetByCustomerId(customerInfo.CustomerID.Value).ToList();
                if (serviceInfo != null)
                    bundleList = serviceInfo;
            }
            else
            {
                bundleList = serviceInfoList;
            }

            if (bundleList != null && bundleList.Count > 0)
            {
                //Let's check if we have more than one Master Bundle active
                if (2 <=
                    bundleList.Count(
                        x =>
                            x.BundleDefinition.BundleID == x.CREDITLIMITBASEBUNDLEID &&
                            ((x.EndDate == null && startDate >= x.StartDate) ||
                             (startDate >= x.StartDate && startDate <= x.EndDate))))
                {
                    Log.Debug(
                        String.Format(
                            "ERROR IN BUNDLELIST!! Customer {0} have more than one masterBundle active for date {1}.",
                            customerInfo.CustomerID.Value,
                            startDate));
                }

                //Get the master bundle. If we have more than one, we are going to take the one with the most recent CreateDate
                var varServiceInfo =
                    bundleList.OrderByDescending(x => x.CreateDate).
                        FirstOrDefault(
                            x =>
                                x.BundleDefinition.BundleID == x.CREDITLIMITBASEBUNDLEID &&
                                ((x.EndDate == null && startDate >= x.StartDate) ||
                                 (startDate >= x.StartDate && startDate <= x.EndDate)));
                if (varServiceInfo == null)
                    throw new BusinessLogicErrorException(
                        String.Format(
                            "Unable to find a BaseBundlePriority in the list of Bundles for the CustomerID:{0}",
                            customerInfo.CustomerID.Value), BizOpsErrors.BaseBundlePriorityNotFound);

                varServiceInfo.CreditLimit = forceCreditLimit;
                _updateServicesInfoWithCustomCreditLimit.Process(new UpdateServicesInfoWithCustomCreditLimitRequest() { ServicesInfo = varServiceInfo, NewCreditLimit = forceCreditLimit },
                    null);
                // repoServicesInfo.Update(varServiceInfo);
            }
        }


        /// <summary>
        /// GetNextBillRunDate
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private GetNextBillRunDateResponseObject GetNextBillRunDate(GetNextBillRunDateRequestObject request)
        {
            GetNextBillRunDateResponseObject res = new GetNextBillRunDateResponseObject();



            DateTime now = request.PurchaseTime;
            DateTime nextBillRun;

            switch (request.BillCycleDefinition.PeriodUnit)
            {
                case TimeUnits.Hour:
                    now = now.AddHours(1);
                    nextBillRun = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
                    break;
                case TimeUnits.Day:
                    now = now.AddDays(1);
                    nextBillRun = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                    break;
                case TimeUnits.Week:
                    now = now.AddDays((7 - (int) now.DayOfWeek) + (int) request.FirstDayOfWeek);
                    nextBillRun = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                    break;
                case TimeUnits.Month:
                    now = now.AddMonths(1);
                    nextBillRun = new DateTime(now.Year, now.Month, 1, 0, 0, 0);
                    break;
                case TimeUnits.Year:
                    now = now.AddYears(1);
                    nextBillRun = new DateTime(now.Year, now.Month, 1, 0, 0, 0);
                    break;
                default:
                    now = now.AddMonths(1);
                    nextBillRun = new DateTime(now.Year, now.Month, 1, 0, 0, 0);
                    break;
            }

            res.NextBillRun = nextBillRun;
            return res;
        }


        /// <summary>
        /// Check compatibility of products in requested list and search any incompatibility between them
        /// </summary>
        /// <param name="productsToPurchase"></param>
        /// <returns>True: All products compatibles. False: Any incompatibility</returns>
        private bool CheckCompatibilityOfProductsInRequestedList(List<Product> productsToPurchase)
        {

            return true;

            //CheckListProductDependenciesResponseInternal response;
            //CheckListProductDependenciesRequestInternal request = new CheckListProductDependenciesRequestInternal()
            //{
            //    Products = productsToPurchase
            //};

            //response =_checklistProductDependenciesBizOP.ProcessBusinessLogic(request, null, null);


            //CheckCompatibilityProductsResponse response;
            //CheckCompatibilityProductsRequest request = new CheckCompatibilityProductsRequest()
            //{

            //    Products = productsToPurchase,
            //    CheckCompatibilityWithItself = true, //Ignore compatibility with itself.
            //};

            //response = _getCheckCompatibilityProductMs.Process(request,null);
            // return (response.ProductIncompatibleList != null && response.ProductIncompatibleList.Count == 0);
        }


        /// <summary>
        /// Check compatibility of products in requested list with the current products assigned to
        /// the customer
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <param name="productsToPurchase"></param>
        /// <returns>True: All products compatibles. False: Any incompatibility</returns>
        private Dictionary<Product, List<Product>> CheckCompatibilityWithCurrentProducts(CustomerInfo customerInfo,
            List<Product> productsToPurchase)
        {
            Dictionary<Product, List<Product>> ProductIncompatibleList = null;


            // TODO: Implement once BizOP ready

            //CheckListProductDependenciesForCustomerResponseInternal response;
            //CheckListProductDependenciesForCustomerRequestInternal request = new CheckListProductDependenciesRequestInternal()
            //{
                //Products = productsToPurchase
            //};

            //response = _checkListProductDependenciesForCustomerBizOp.ProcessBusinessLogic(request, null, null);



            //CheckCompatibilityCustomerProductsResponse response;
            //CheckCompatibilityCustomerProductsRequest request = new CheckCompatibilityCustomerProductsRequest()
            //{
            //    CustomerInfo = customerInfo,
            //    Products = productsToPurchase
            //};

            //response = _getCheckCompatibilityCustomerProductMS.Process(request,null);

            //if (response.ProductIncompatibleList != null && response.ProductIncompatibleList.Count > 0)
                //ProductIncompatibleList = response.ProductIncompatibleList.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return ProductIncompatibleList;
        }





        /// <summary>
        /// Remove incompatible products that are already assigned to the customer.
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <param name="pair_NewProduct_ListProductIncompatible"></param>
        /// <param name="accountCustomer"></param>
        /// <param name="purchaseDate"></param>
        /// <param name="nextBillRunDate"></param>
        /// <param name="newProductsWithCanceledRecurringProducts"></param>
        /// <param name="promotionsCanceledByIncompatibleProduct"></param>
        private void CancelAssignedProductsIncompatible(CustomerInfo customerInfo,
            Dictionary<Product, List<Product>> pair_NewProduct_ListProductIncompatible,
            Account accountCustomer,
            DateTime purchaseDate,
            DateTime nextBillRunDate,
            out List<Product> newProductsWithCanceledRecurringProducts,
            out List<CrmCustomersPromotionInfo> promotionsCanceledByIncompatibleProduct)
        {
            newProductsWithCanceledRecurringProducts = null;
            promotionsCanceledByIncompatibleProduct = null;
        }

        //    List<Product> productsIncompatible = new List<Product>();
        //    newProductsWithCanceledRecurringProducts = new List<Product>();
        //    promotionsCanceledByIncompatibleProduct = new List<CrmCustomersPromotionInfo>();

        //    #region Get incompatible list

        //    foreach (List<Product> productsIncompatibles in pair_NewProduct_ListProductIncompatible.Values)
        //    {
        //        foreach (Product incompatible in productsIncompatibles)
        //        {
        //            if (!productsIncompatible.Contains(incompatible))
        //                productsIncompatible.Add(incompatible);
        //        }
        //    }

        //    #endregion Get incompatible list

        //    #region Cancel Promotions

        //    CancelCustomerPromotionRequest cancelCustomerPromotionRequest;
        //    CancelCustomerPromotionResponse cancelCustomerPromotionResponse;

        //    foreach (Product product in productsIncompatible.Where(x => x.AssociatedPrmotionPlan != null))
        //    {
        //        List<long> listPromotionPlanId = new List<long>();
        //        listPromotionPlanId.Add(product.AssociatedPrmotionPlan.PromotionPlanId);

        //        ICrmCustomersPromotionInfoRepository<CrmCustomersPromotionInfo> repoCustomerPromotion =
        //            RepositoryManager.GetRepository<ICrmCustomersPromotionInfoRepository<CrmCustomersPromotionInfo>>();
        //        var customerPromotions = repoCustomerPromotion.GetCustomerPromotionPlanById(customerInfo.CustomerID.Value, listPromotionPlanId.ToArray());

        //        if (customerPromotions != null)
        //        {
        //            bool hasRecurringCharges = isProductHasRecurringCharges(product.Id);

        //            foreach (CrmCustomersPromotionInfo cp in customerPromotions)
        //            {
        //                DateTime newEndDate = hasRecurringCharges ? nextBillRunDate : purchaseDate;
        //                // When the product is recurring but incompatible, it must be canceled in
        //                // the next bill cycle.,
        //                //if (cp.EndDate == null || (newEndDate >= cp.StartDate && newEndDate <= cp.EndDate))
        //                //{
        //                //    cancelCustomerPromotionRequest = new CancelCustomerPromotionRequest()
        //                //    {
        //                //        PromotionId = cp.PromotionId,
        //                //        EndDate = newEndDate,

        //                //        CreateLogPromotion = new CreateLogPromotionInfo()
        //                //        {
        //                //            Description = string.Empty,
        //                //            Msisdn = string.Empty,
        //                //            OperationCode = 4,
        //                //            Status = "CO"
        //                //        },
        //                //        user = _user,
        //                //        vMNO = _vMNO
        //                //    };

        //                //    cancelCustomerPromotionResponse = _coreCancelCustomerPromotion.Process(cancelCustomerPromotionRequest, oplog, invoker);
        //                //    promotionsCanceledByIncompatibleProduct.Add(cancelCustomerPromotionResponse.promotion);
        //                //    _log.Info(string.Format("CancelCustomerPromotion. CustomerID:{0}, PromotionID: {1}", customerInfo.CustomerID, cancelCustomerPromotionRequest.PromotionId));
        //                //}

        //                //Modidied by neil at 2015-04-20, bugfix for jira:CSP-821, we should always cancel the custommerpromotions to avoid the promotion is renewed if the promotion is recurring product. 
        //                if (cp.EndDate.HasValue && cp.EndDate < newEndDate)
        //                {
        //                    newEndDate = cp.EndDate.Value;
        //                }
        //                cancelCustomerPromotionRequest = new CancelCustomerPromotionRequest()
        //                {
        //                    PromotionId = cp.PromotionId,
        //                    EndDate = newEndDate,

        //                    CreateLogPromotion = new CreateLogPromotionInfo()
        //                    {
        //                        Description = string.Empty,
        //                        Msisdn = string.Empty,
        //                        OperationCode = 4,
        //                        Status = "CO"
        //                    },
        //                    user = _user,
        //                    vMNO = _vMNO
        //                };
        //                cancelCustomerPromotionResponse = _coreCancelCustomerPromotion.Process(cancelCustomerPromotionRequest, oplog, invoker);
        //                promotionsCanceledByIncompatibleProduct.Add(cancelCustomerPromotionResponse.promotion);
        //                _log.Info(string.Format("CancelCustomerPromotion. CustomerID:{0}, PromotionID: {1}", customerInfo.CustomerID, cancelCustomerPromotionRequest.PromotionId));
        //            }
        //        }
        //    }

        //    #endregion Cancel Promotions

        //    #region Cancel Bundles (ServicesInfo)

        //    CancelBundleRequest cancelBundleRequest;
        //    CancelBundleResponse cancelBundleResponse;

        //    foreach (Product product in productsIncompatible.Where(x => x.AssociatedBundle != null))
        //    {
        //        cancelBundleRequest = new CancelBundleRequest()
        //        {
        //            CustomerDefinition = customerInfo,
        //            BundleDefinition = product.AssociatedBundle,
        //            EndDate = purchaseDate,
        //            user = _user,
        //            vMNO = _vMNO
        //        };

        //        cancelBundleResponse = _coreCancelBundleOperation.Process(cancelBundleRequest, oplog, invoker);
        //        _log.Info(String.Format("CancelBundleOperation. CustomerID:{0}, BundleID: {1}",
        //            customerInfo.CustomerID,
        //            cancelBundleRequest.BundleDefinition.BundleID));
        //    }

        //    #endregion Cancel Bundles (ServicesInfo)

        //    #region Cancel Packages

        //    CancelPackageForCustomerRequest cancelPackageForCustomerRequest;
        //    CancelPackageForCustomerResponse cancelPackageForCustomerResponse;

        //    foreach (Product product in productsIncompatible.Where(x => x.AssociatedPackage != null))
        //    {
        //        cancelPackageForCustomerRequest = new CancelPackageForCustomerRequest()
        //        {
        //            CustomerDefinition = customerInfo,
        //            PackageDefinition = product.AssociatedPackage,
        //            EndDate = purchaseDate,
        //            user = _user,
        //            vMNO = _vMNO
        //        };

        //        cancelPackageForCustomerResponse = _coreCancelPackageForCustomerOperation.Process(cancelPackageForCustomerRequest, oplog, invoker);
        //        _log.Info(String.Format("CancelPackageForCustomerOperation. CustomerID:{0}, PackageID: {1}",
        //            customerInfo.CustomerID,
        //            cancelPackageForCustomerRequest.PackageDefinition.PackageID));
        //    }

        //    #endregion Cancel Packages


        //    #region Cancel Product

        //    CancelCustomerProductRequest cancelCustomerProductRequest;
        //    CancelCustomerProductResponse cancelCustomerProductResponse;

        //    foreach (Product product in productsIncompatible)
        //    {
        //        bool hasRecurringCharges = isProductHasRecurringCharges(product.Id);
        //        ;

        //        if (hasRecurringCharges && !newProductsWithCanceledRecurringProducts.Contains(product))
        //            newProductsWithCanceledRecurringProducts.Add(product);

        //        cancelCustomerProductRequest = new CancelCustomerProductRequest()
        //        {
        //            CustomerDefinition = customerInfo,
        //            ProductDefinition = product,
        //            EndDate = hasRecurringCharges ? nextBillRunDate : purchaseDate,
        //            // When the product is recurring but incompatible, it must be canceled in the
        //            // next bill cycle.
        //            user = _user,
        //            vMNO = _vMNO
        //        };

        //        cancelCustomerProductResponse = _coreCancelCustomerProductOperation.Process(cancelCustomerProductRequest, oplog, invoker);
        //        _log.Info(String.Format("CancelCustomerProductOperation. CustomerID:{0}, ProductID: {1}",
        //            customerInfo.CustomerID,
        //            cancelCustomerProductRequest.ProductDefinition.Id));
        //    }

        //    #endregion Cancel Product

        //    #region Cancel recurring charges

        //    // TODO: Use the new method GetByCustomerID in ICustomerChargeScheduleRepository to
        //    // build this code!!!!!.... Not implemented yet.

        //    ////CustomerChargeSchedule chargeSchedule = item.Schedule;
        //    ////chargeSchedule.NextChargeDate = null;
        //    ////chargeSchedule.Status = ScheduleChargeStatus.Disabled;
        //    ////chargeSchedule.UpdateDate = purchaseDate;
        //    ////repoCustomerChargeSchedule.Update(chargeSchedule);

        //    #endregion Cancel recurring charges

        //}



        /// <summary>
        /// getStartDateForOperation
        /// </summary>
        /// <param name="productChargeOption"></param>
        /// <param name="datetimePurchase"></param>
        /// <param name="nextBillRunDate"></param>
        /// <param name="isExternalCalling"></param>
        /// <returns></returns>
        private DateTime getStartDateForOperation(
            PurchaseProductForCustomer.PurchaseProductForCustomerBizOp.Product_ChargeOption
                productChargeOption,
            DateTime datetimePurchase,
            DateTime nextBillRunDate,
            bool isExternalCalling)
        {
            DateTime datetime;
            bool isRecurring = false;

            isRecurring = isRecurringCharge(productChargeOption);

            if (isRecurring)
            {
                datetime = isExternalCalling ? datetimePurchase : nextBillRunDate;
            }
            else
            {
                // non-recurring
                datetime = datetimePurchase;
            }

            return datetime;
        }


        /// <summary>
        /// getInvoiceByLegalInvoiceNumbe
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <param name="IsInvoiceMandatory"></param>
        /// <returns></returns>
        private Invoice getInvoiceByLegalInvoiceNumbe(CustomerInfo customerInfo, bool IsInvoiceMandatory)
        {
            
            Invoice invoice = null;
            string legalNumber = null; 
            DateTime now = DateTime.Now;

            var varInvoice =
                _getInvoiceByLegalInvoiceNumber.Process(
                    new GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest()
                    {
                        CustomerId = customerInfo.CustomerID.Value,
                        LegalInvoiceNumber = legalNumber
                    }, null).Invoices;
            
            if (varInvoice != null)
                invoice =
                    Enumerable.ToList<Invoice>(varInvoice)
                        .FirstOrDefault(
                            x => (x.EndDate == null && now >= x.StartDate) || (now >= x.StartDate && now < x.EndDate));
            if (IsInvoiceMandatory && invoice == null)
                throw new BusinessLogicErrorException(
                    String.Format("Unable to find an Invoice by LegalInvoiceNumber for the CustomerID:{0}",
                        customerInfo.CustomerID), BizOpsErrors.InvoiceNotFound);

            return invoice;
        }


        /// <summary>
        /// getBundleMaster
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private ServicesInfo getBundleMaster(CreateCustomerChargeRequestObject request)
        {
            List<ServicesInfo> bundleList = null;
            ServicesInfo bundleMaster = null;


            if (request.CustomerCharge.Customer.ServicesInfo == null || request.CustomerCharge.Customer.ServicesInfo.Count == 0)
            {
                var serviceInfo =
                    _getServiceByCusotmerId.Process(
                        new GetServicesInfosByCustomerIDRequest() {CustomerID = request.CustomerCharge.Customer.CustomerID.Value}, null).ServicesInfos;

                if (serviceInfo != null)
                    bundleList = Enumerable.ToList<ServicesInfo>(serviceInfo);
            }
            else
            {
                bundleList = request.CustomerCharge.Customer.ServicesInfo.ToList();
            }

            if (bundleList != null && bundleList.Count > 0)
            {
                //Let's check if we have more than one Master Bundle active
                if (2 <= bundleList.Count(x => x.BundleDefinition.BundleID == x.CREDITLIMITBASEBUNDLEID
                                               && x.StartDate <= request.StartDate
                                               && (x.EndDate == null || x.EndDate > request.StartDate)))
                {
                    Log.Debug(
                        string.Format(
                            "ERROR IN BUNDLELIST!! Customer {0} have more than one masterBundle active for date {1}.",
                            request.CustomerCharge.Customer.CustomerID.Value, request.StartDate));
                }

                //Get the master bundle. If we have more than one, we are going to take the one with the most recent CreateDate
                var varServiceInfo = bundleList
                    .OrderByDescending(x => x.CreateDate)
                    .FirstOrDefault(x => x.BundleDefinition.BundleID == x.CREDITLIMITBASEBUNDLEID
                                         && x.StartDate <= request.StartDate
                                         && (x.EndDate == null || x.EndDate > request.StartDate));
                if (varServiceInfo != null)
                    bundleMaster = varServiceInfo;
            }

            if (bundleMaster == null)
                throw new BusinessLogicErrorException(
                    string.Format("Unable to find a BaseBundlePriority in the list of Bundles for the CustomerID:{0}",
                        request.CustomerCharge.Customer.CustomerID.Value), BizOpsErrors.BaseBundlePriorityNotFound);

            return bundleMaster;
        }


        /// <summary>
        /// CreateRecurringAndNoRecurringCharges
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private CreateCustomerChargeResponseObject CreateRecurringAndNoRecurringCharges(CreateCustomerChargeRequestObject request)
        {
            #region Ini var

            CreateCustomerChargeResponseObject response = new CreateCustomerChargeResponseObject();
            CustomerChargeSchedule chargeRecurring = null;
            CustomerCharge chargeNonRecurring = null;
            ServicesInfo bundleMaster = null;
            bool hasCreditForPurchase = false;
            decimal newAmount = 0;

            #endregion


            #region Create Charge/ ChargeSchedule

            if ((request.InsertLikeNonRecurring) ||
                request.CustomerCharge.ChargeDefinition is ChargeNonRecurring)
            {
                #region Non recurring charge

                ChargePrice chargePrice = getPrice(request.CustomerCharge.ChargeDefinition, request.StartDate);
                // SpanishTaxAuthority spanishTaxAuthority = new SpanishTaxAuthority();
                TaxDefinition taxDefinition = _getTaxAuthorityMS.Process(new GetTaxAuthorityRequest() { CustomerId =(int) request.CustomerCharge.Id }, null).TaxDefinition;
                    // spanishTaxAuthority.GetTaxDefinitionForSpain(request.CustomerCharge);
                decimal taxPercentage = getTaxRate(taxDefinition, request.StartDate).Percentage;

                if (request.CustomerCharge.Amount == 0)
                    newAmount = chargePrice.Amount;
                else
                    newAmount = request.CustomerCharge.Amount;

                CreateCustomerChargeRequest customerCharge = new CreateCustomerChargeRequest()
                {
                    CustomerCharge = new CustomerCharge()
                    {
                        Amount = newAmount,
                        ChargeDefinition = request.CustomerCharge.ChargeDefinition,
                        ChargingAccount = request.CustomerCharge.ChargingAccount,
                        ChargingDate = request.StartDate,
                        Currency = chargePrice.Currency,
                        Customer = request.CustomerCharge.Customer,
                        CycleNumber = request.CustomerCharge.CycleNumber,
                        InformationalAmount = newAmount + (newAmount*(taxPercentage/100)),
                        Invoice = request.CustomerCharge.Invoice,
                        PeriodNumber = request.CustomerCharge.PeriodNumber,
                        Product = request.CustomerCharge.Product,
                        Schedule = request.CustomerCharge.Schedule,
                        Tax = taxDefinition,
                        TaxAmount = newAmount*(taxPercentage/100)
                    }
                };

                chargeNonRecurring = _createCustomerChargeResponseMS.Process(customerCharge, null).CustomerCharge;

                #endregion
            }
            else
            {
                #region Recurring charge

                CreateCustomerChargeScheduleRequest customerChargeSchedule = new CreateCustomerChargeScheduleRequest()
                {
                    CustomerChargeSchedule = new CustomerChargeSchedule()
                    {
                        ChargedAccount = request.CustomerCharge.ChargingAccount,
                        ChargeDefinition = request.CustomerCharge.ChargeDefinition,
                        Charges = null,
                        CreateDate = DateTime.Now,
                        CurrentCyclenumber = 0,
                        Customer = request.CustomerCharge.Customer,
                        NextChargeDate = request.StartDate,
                        NextPeriodNumber = 1,
                        PriceEffectiveDate = null,
                        Purchase = request.CustomerCharge.Product,
                        Status = ScheduleChargeStatus.InProcess,
                        UpdateDate = null
                    }
                };

                chargeRecurring = _createCustomerChargeSchedule.Process(customerChargeSchedule, null).CustomerChargeSchedule;

                #endregion
            }

            #endregion

            #region Update Bundle Master

            bundleMaster = getBundleMaster(request);
            hasCreditForPurchase = (bundleMaster.UnBilledBalance + newAmount <= bundleMaster.CreditLimit);

            if (hasCreditForPurchase && newAmount > 0)
            {
                //Created by David 2014-12-19: Raise event
                SendChargeAppliedEvent(request, bundleMaster.UnBilledBalance, bundleMaster.UnBilledBalance + newAmount);

                bundleMaster.UnBilledBalance += newAmount;
                _addUnbilledBalanceMS.Process(
                    new AddUnbilledBalanceRequest() {ServicesInfo = bundleMaster, Amount = (decimal) bundleMaster.UnBilledBalance},
                    null);
                // _repoServicesInfo.Update(bundleMaster);
            }

            #endregion

            response.HasCreditForPurchase = hasCreditForPurchase;
            response.ChargeRecurring = chargeRecurring;
            response.ChargeNonRecurring = chargeNonRecurring;

            return response;
        }


        /// <summary>
        /// reviewCreditLimitForEachPromotion
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <param name="customerPromotionListPurchased"></param>
        /// <param name="promotionsCanceledByIncompatibleProduct"></param>
        /// <param name="datetimePurchase"></param>
        /// <param name="nextBillRunDate"></param>
        private void reviewCreditLimitForEachPromotion(CustomerInfo customerInfo,
            List<CrmCustomersPromotionInfo> customerPromotionListPurchased,
            List<CrmCustomersPromotionInfo> promotionsCanceledByIncompatibleProduct, DateTime datetimePurchase,
            DateTime nextBillRunDate)
        {

            // Get the max limit configured for the pair SubServiceTypeId/WhiteList

            DealerInfo dealerInfo =
                _getDealerInfoMVNOByDealerIdMS.Process(
                    new GetDealerInfoMVNOByDealerIdRequest() {DealerId = customerInfo.DealerID.Value}, null).DealerInfo;
            //repoDealer.GetMVNOByDealerId(customerInfo.DealerID.Value);
            if (dealerInfo == null)
                return;
            List<MVNOConfigActionInfo> configurationList =
                Enumerable.ToList<MVNOConfigActionInfo>(_getMVNOConfigActionInfosByIdAndNameMS.Process(new GetMVNOConfigActionInfosByIdAndNameRequest()
                {
                    MvnoId = dealerInfo.FiscalUnitID.Value,
                    CategoryName = "PURCHASE_PRODUCT_LIMIT_PROMOTION"
                }, null).MvnoConfigActionInfos);

            

            foreach (CrmCustomersPromotionInfo customersPromotionInfo in customerPromotionListPurchased)
            {
                RmPromotionPlanDetailInfo promotionDetail = customersPromotionInfo.PromotionDetail;

                // get the max limit configured
                decimal configLimit = 0;
                bool hasConfig = false;
                foreach (MVNOConfigActionInfo config in configurationList)
                {
                    int configSubServiceTypeId = 0;
                    string configWhiteList = String.Empty;

                    string[] pair = config.BAK1.Split('|');

                    if (pair.Length > 0)
                        configSubServiceTypeId = Int32.Parse(pair[0]);

                    if (pair.Length > 1)
                        configWhiteList = pair[1];

                    if (promotionDetail != null && promotionDetail.SubServiceTypeId == configSubServiceTypeId &&
                        promotionDetail.WhiteList == configWhiteList)
                    {
                        configLimit = Decimal.Parse(config.Value);
                        hasConfig = true;
                        break;
                    }
                }

                if (hasConfig)
                {
                    decimal limit = 0;
                    limit = getCurrentLimit(customerInfo, customersPromotionInfo,
                        promotionsCanceledByIncompatibleProduct, datetimePurchase, nextBillRunDate);
                    if (limit > configLimit)
                        throw new BusinessLogicErrorException(
                            String.Format("Not enough credit to perform this operation. SubServiceTypeId:{0}",
                                promotionDetail.SubServiceTypeId), BizOpsErrors.CreditNotEnough);
                }
            }
        }


        /// <summary>
        /// getCurrentLimit
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <param name="customersPromotionInfo"></param>
        /// <param name="promotionsCanceledByIncompatibleProduct"></param>
        /// <param name="datetimePurchase"></param>
        /// <param name="nextBillRunDate"></param>
        /// <returns></returns>
        private decimal getCurrentLimit(CustomerInfo customerInfo, CrmCustomersPromotionInfo customersPromotionInfo,
            List<CrmCustomersPromotionInfo> promotionsCanceledByIncompatibleProduct,
            DateTime datetimePurchase, DateTime nextBillRunDate)
        {
            decimal promotionLimit = 0;
            decimal currentCustomerLimit = 0;
            RmPromotionPlanDetailInfo promotionDetail = customersPromotionInfo.PromotionDetail;

            promotionLimit = promotionDetail.Limit; // Limit of the new promotion 

            if (customersPromotionInfo.StartDate == nextBillRunDate)
            {
                // Get the total of future Limit for the customer
                foreach (
                    CrmCustomersPromotionInfo cp in
                        customerInfo.Promotions.Where(
                            x => x.PromotionDetail.SubServiceTypeId == promotionDetail.SubServiceTypeId
                                 && x.WhiteList == promotionDetail.WhiteList
                                 && x.Active == true
                                 && x.StartDate >= nextBillRunDate))
                {
                    // Not considering promotions canceled. Ignore new promotion added to the current CustomerPromotions
                    if (promotionsCanceledByIncompatibleProduct.Count(x => x.PromotionId == cp.PromotionId) == 0 &&
                        cp.PromotionId != customersPromotionInfo.PromotionId)
                        currentCustomerLimit += cp.PromotionDetail.Limit;
                }
            }
            else
            {
                // Get the total of current Limit for the customer
                foreach (
                    CrmCustomersPromotionInfo cp in
                        customerInfo.Promotions.Where(
                            x => x.PromotionDetail.SubServiceTypeId == promotionDetail.SubServiceTypeId
                                 && x.WhiteList == promotionDetail.WhiteList
                                 && x.Active == true
                                 && ((x.EndDate == null && datetimePurchase >= x.StartDate)
                                     || (datetimePurchase >= x.StartDate && datetimePurchase < x.EndDate))))
                {
                    // Not considering promotions canceled. Ignore new promotion added to the current CustomerPromotions
                    if (promotionsCanceledByIncompatibleProduct.Count(x => x.PromotionId == cp.PromotionId) == 0 &&
                        cp.PromotionId != customersPromotionInfo.PromotionId)
                        currentCustomerLimit += cp.PromotionDetail.Limit;
                }
            }

            return promotionLimit + currentCustomerLimit;
        }



        #region 4G Section

        /// <summary>
        /// Apply4GServiceForCustomer
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <param name="mvnoId"></param>
        /// <param name="productsToPurchase"></param>
        private void Apply4GServiceForCustomer(CustomerInfo customerInfo, int mvnoId, List<Product> productsToPurchase)
        {
            try
            {
                var product4G = GetCustomer4GProducts(productsToPurchase, mvnoId).ToList();
                if (!product4G.Any())
                    return;

                Log.Debug(
                    String.Format(
                        " Customer[Id={0}] has purchase 4G product[Id={1}], so need to enable 4G service for the customer.",
                        customerInfo.CustomerID,
                        String.Join(",", product4G.Select(x => x.Id).ToList())));

                // TODO: ProvisioningService is still in legacy code
                var provisioning = _provisioningService;

                

                var provisionResourceMb = provisioning.GetResourceMBByCustomerID(customerInfo.CustomerID ?? 0);
                provisioning.ActivateProvisioningSubscriber(provisionResourceMb, EProvisioningSystem.HSS);
                Log.Debug(String.Format(" -Enable4GService for Customer[Id={0}]", customerInfo.CustomerID ?? 0));
            }
            catch (Exception ex)
            {
                Log.Error("Error on Apply4GServiceForCustomer:", ex);
                throw;
            }
        }

        /// <summary>
        /// GetCustomer4GProducts
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <param name="mvnoId"></param>
        /// <returns></returns>
        private IEnumerable<Product> GetCustomer4GProducts(CustomerInfo customerInfo, int mvnoId)
        {
            var productIds4G = Get4GProductIdsConfiguration(mvnoId);
            var nowTime = DateTime.Now;
            var result =
                customerInfo.RevenueProductsInfo.Where(
                    x =>
                        x.StartDate <= nowTime && (!x.EndDate.HasValue || x.EndDate > nowTime) &&
                        productIds4G.Contains(x.PurchasedProduct.Id.ToString(CultureInfo.InvariantCulture)))
                    .Select(x => x.PurchasedProduct)
                    .ToList();
            return result;
        }

        /// <summary>
        /// GetCustomer4GProducts
        /// </summary>
        /// <param name="productsToPurchase"></param>
        /// <param name="mvnoId"></param>
        /// <returns></returns>
        private IEnumerable<Product> GetCustomer4GProducts(List<Product> productsToPurchase, int mvnoId)
        {
            if (!productsToPurchase.Any())
                return null;
            var productIds4G = Get4GProductIdsConfiguration(mvnoId);
            var result = productsToPurchase.Where(x => productIds4G.Contains(x.Id.ToString()));
            return result;
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

            
            var config =
                Enumerable.FirstOrDefault<MVNOConfigActionInfo>(_getMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemMS.Process(
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

        /// <summary>
        /// Check if a productID, using its chargeOption by default, has recurring charges.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        private bool isProductHasRecurringCharges(int productId)
        {
            ProductChargeOption productChargeOption = getProductChargeOptionByPurchaseItem(productId, null); //Use default chargeOption

            return (productChargeOption != null && productChargeOption.Charges.FirstOrDefault(x => x is ChargeRecurring) != null);
        }



        /// <summary>
        /// getProductChargeOptionByPurchaseItem
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productPurchaseOptionId"></param>
        /// <returns></returns>
        private ProductChargeOption getProductChargeOptionByPurchaseItem(int productId, int? productPurchaseOptionId)
        {
            
            ProductChargeOption productChargeOption = null;

            if (productPurchaseOptionId == null)
            {
                var chargesOption =
                    _getProductChargeOptionsByProductIdMS.Process(new GetProductChargeOptionsByProductIdRequest() { ProductId =productId }, null).ProductChargeOptions;
                    //repoProductChargeOption.GetByProductId(productId);
                if (chargesOption != null)
                    productChargeOption = Enumerable.SingleOrDefault<ProductChargeOption>(chargesOption, x => x.IsDefaultOption == DefaultOptions.Y);

                //if (productChargeOption == null)
                //    throw new BusinessLogicErrorException(string.Format("Unable to find a ProductChargeOption by default for the productID:{0}", productId));
            }
            else
            {
                productChargeOption =
                    _getProductChargeOptionByProductChargeOptionIdMS.Process(
                        new GetProductChargeOptionByProductChargeOptionIdRequest() { ProductChargeOptionId =productPurchaseOptionId.Value }, null).ProductChargeOption;
                    //repoProductChargeOption.GetById(productPurchaseOptionId.Value);
                if (productChargeOption == null)
                    throw new BusinessLogicErrorException(String.Format("Unable to find information for the productChargeOptionID:{0}",
                        productPurchaseOptionId.Value),BizOpsErrors.ProductChargeOptionNotFound);
                if (productChargeOption.ProductOfOption.Id != productId)
                    throw new BusinessLogicErrorException(String.Format("The ProductChargeOption {0} is a Charge Option of product {1} instead of {2}.",
                        productChargeOption.Id,
                        productChargeOption.ProductOfOption.Id,
                        productId),BizOpsErrors.ProductChargeOptionOfAnotherProduct);
            }

            return productChargeOption;
        }


        /// <summary>
        /// Obtain if a ProductChargeOption have recurrent charges
        /// </summary>
        /// <param name="productChargeOption"></param>
        /// <returns>true if exists a recurring Charge</returns>
        private bool isRecurringCharge(PurchaseProductForCustomer.PurchaseProductForCustomerBizOp.Product_ChargeOption productChargeOption)
        {
            bool isRecurring = productChargeOption.ProductChargeOption != null && productChargeOption.ProductChargeOption.Charges != null &&
                               productChargeOption.ProductChargeOption.Charges.Count(x => x is ChargeRecurring) > 0;

            return isRecurring;
        }




        #endregion

        #endregion

    }
}


