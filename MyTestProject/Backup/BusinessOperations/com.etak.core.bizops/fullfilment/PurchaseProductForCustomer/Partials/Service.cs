using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.microservices.messages.GetSystemConfigDataInfoById;
using com.etak.core.model;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.service.messages.CreateServicesInfo;
using com.etak.core.service.messages.GetBundleInfoByID;

namespace com.etak.core.bizops.fullfilment.PurchaseProductForCustomer
{
    /// <summary>
    /// Partial Implementation BizOp PurchaseProductForCustomer
    /// </summary>
    public partial class PurchaseProductForCustomerBizOp
    {

        #region Public Methods

        /// <summary>
        /// AssignBundleForCustomerRequestObject USed internally private methods
        /// </summary>
        public class AssignBundleForCustomerRequestObject
        {
            /// <summary>
            /// CustomerInfo
            /// </summary>
            public CustomerInfo CustomerDefinition { get; set; }

            /// <summary>
            /// BundleInfo list 
            /// </summary>
            public IList<BundleInfo> BundleDefinitionList { get; set; }

            /// <summary>
            /// Start date for each Bundle
            /// </summary>
            public DateTime? StartDate { get; set; }

            /// <summary>
            /// Logininfo
            /// </summary>
            public LoginInfo LoginInfo { get; set; }
        }

        /// <summary>
        /// Used internally private methods
        /// </summary>
        public class AssignBundleForCustomerResponseObject
        {
            /// <summary>
            /// ServicesList
            /// </summary>
            public List<ServicesInfo> ServicesList { get; set; }
        }


        /// <summary>
        /// AddServiceToCustomerList
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AssignBundleForCustomerResponseObject AddServiceToCustomerList(AssignBundleForCustomerRequestObject request)
        {

            AssignBundleForCustomerResponseObject response = new AssignBundleForCustomerResponseObject();


            
            List<ServicesInfo> subServiceList = null;
            BundleInfo bundlePriority = null;

            if (request.BundleDefinitionList != null)
            {
                subServiceList = new List<ServicesInfo>();

                bundlePriority = getBundlePriority(request.BundleDefinitionList.ToList());

                foreach (BundleInfo bundleInfo in request.BundleDefinitionList)
                {
                    ServicesInfo subService = new ServicesInfo()
                    {
                        BundleDefinition = bundleInfo,
                        CustomerInfo = request.CustomerDefinition,
                        StartDate = request.StartDate,  
                        UserID = request.LoginInfo.UserID,
                        UnBilledBalance = 0.00M,
                        BilledBalance = 0,
                        InvoiceTemplateID = 1,  // Value forLegacy code 
                        CreateDate = DateTime.Now
                    };

                    if (bundlePriority.BundleID == bundleInfo.BundleID)
                    {
                        subService.CreditLimit = bundleInfo.CreditLimit;
                        subService.CREDITLIMITBASEBUNDLEID = bundleInfo.BundleID.Value;
                    }
                    else
                    {
                        subService.CreditLimit = 0;
                        subService.CREDITLIMITBASEBUNDLEID = bundlePriority.BundleID.Value;
                    }

                    subService = _createServiceMS.Process(new CreateServicesInfoRequest() { subService = subService }, null).ServicesInfos;
                    subServiceList.Add(subService);
                }
            }

            response.ServicesList = subServiceList;

            return response;
        } 
        #endregion

        #region Private Methods

        /// <summary>
        /// getBundlePriority
        /// </summary>
        /// <param name="bundleDefinitionList"></param>
        /// <returns></returns>
        private BundleInfo getBundlePriority(List<BundleInfo> bundleDefinitionList)
        {

            //ISystemConfigDataInfoRepository<SystemConfigDataInfo> repo = RepositoryManager.GetRepository<ISystemConfigDataInfoRepository<SystemConfigDataInfo>>();
            //IBundleInfoRepository<BundleInfo> repoBundleInfo = RepositoryManager.GetRepository<IBundleInfoRepository<BundleInfo>>();
            SystemConfigDataInfo systemConfigDataInfo =
                _getSystemConfigDataInfoByIdMS.Process(new GetSystemConfigDataInfoByIdRequest() { SystemConfigDataId = "BaseBundlePriority" }, null).SystemConfigData;
                //repo.GetById("BaseBundlePriority");
            string[] bundlePriorityIDs = systemConfigDataInfo.Value.Split(',');

            for (int i = 0; i < bundlePriorityIDs.Length; ++i)
            {
                var item =
                    bundleDefinitionList.OrderBy(x => x.SubserviceTypeID)
                        .FirstOrDefault(x => _getBundleInfoByIDMS.Process(new GetBundleInfoByIDRequest()
                        {
                            BundleId = x.BundleID.Value
                        }, null).BundleInfo.SubserviceTypeID == int.Parse(bundlePriorityIDs[i]));
                    
                if (item != null)
                    return _getBundleInfoByIDMS.Process(new GetBundleInfoByIDRequest() { BundleId = item.BundleID.Value }, null).BundleInfo;
            }

            throw new BusinessLogicErrorException(string.Format("Unable to find a BaseBundlePriority necessary for create a customer service"), BizOpsErrors.UnableFindBaseBundlePriorityForCreateCustomerService);
        } 
        #endregion

    }
}


