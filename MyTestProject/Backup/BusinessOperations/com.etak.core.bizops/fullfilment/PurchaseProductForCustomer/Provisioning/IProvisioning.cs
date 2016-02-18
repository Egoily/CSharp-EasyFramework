//using System.Collections.Generic;
//using com.etak.core.model;
//using com.etak.core.model.provisioning;
//using System;

//namespace ETalk.CRM.Provisioning.Interface
//{

//    /// <summary>
//    /// the interface for provision business object.
//    /// </summary>
//    public interface IProvisioning
//    {
//        /// <summary>
//        /// Method to add a SIM Entry to HLR's SIM module(AuC)
//        /// </summary>
//        /// <param name="simCardInfo"></param>
//        /// <param name="SimcardProvisiongTemplate"></param>
//        /// <returns></returns>
//        bool AddSimCard(SIMCardInfo simCardInfo, string SimcardProvisiongTemplate);

//        /// <summary>
//        /// Gets the provision by id.
//        /// </summary>
//        /// <param name="provisionId">The provision id.</param>
//        /// <returns></returns>
//        CrmDefaultProvisionInfo GetProvisioningTemplateById(int provisionId);



//        /// <summary>
//        /// GetProvisioningTemplateByName
//        /// </summary>
//        /// <param name="templateName"></param>
//        /// <param name="mvno"></param>
//        /// <returns></returns>
//        CrmDefaultProvisionInfo GetProvisioningTemplateByName(string templateName, string mvno);


//        /// <summary>
//        /// Get CrmCustomersResourceMbInfo object by resource id.
//        /// This is a database opearion.
//        /// </summary>
//        /// <param name="resourceId">The resource id.</param>
//        /// <returns></returns>
//        CrmCustomersResourceMbInfo GetResourceById(int resourceId);
//        /// <summary>
//        /// Add Provison Template For Dealer.
//        /// This is a database operation.
//        /// </summary>
//        /// <param name="provisionSetting">The provision setting data model.</param>
//        /// <returns></returns>
//        int AddProvisionForDealer(CrmDefaultProvisionInfo provisionSetting);

//        /// <summary>
//        /// update provision template for dealer.
//        /// This is a database operation.
//        /// </summary>
//        /// <param name="provisionSetting">The provision setting data model.</param>
//        /// <returns></returns>
//        int UpdateProvisionForDealer(CrmDefaultProvisionInfo provisionSetting);
        
//        /// <summary>
//        /// Gets the provision by dealer.
//        /// </summary>
//        /// <param name="dealerId">The dealer id.</param>
//        /// <returns></returns>
//        CrmDefaultProvisionInfo GetDefaultProvisionTemplateOfDealer(int dealerId);

//        /// <summary>
//        /// Gets the provision by dealer and package id.
//        /// </summary>
//        /// <param name="dealerId">The dealer id.</param>
//        /// <param name="packageID"></param>
//        /// <returns></returns>
//        CrmDefaultProvisionInfo GetProvisionByDealer(int dealerId, int packageID);


//        /// <summary>
//        /// Provision the Customer to HLR.
//        /// CustomerProvisionData - the CrmCustomerResourceMbInfo object.
//        /// Return 0 for success.
//        /// </summary>
//        /// <param name="crmSubscriber"></param>
//        /// <returns></returns>
//        int AddProvisionForCustomer(CrmCustomersResourceMbInfo crmSubscriber);

                
//        /// <summary>
//        /// AddProvisionForCustomer special for Register
//        /// </summary>
//        /// <param name="crmSubscriber"></param>
//        /// <param name="simcardInfo"></param>
//        /// <returns></returns>
//        int AddProvisionForCustomer_Register(CrmCustomersResourceMbInfo crmSubscriber, SIMCardInfo simcardInfo);

//        /// <summary>
//        /// Update the provision setting For customer.
//        /// This is a legacy method for backward compatiblity.
//        /// This is a database operation.
//        /// </summary>
//        /// <returns></returns>
//        string UpdateProvisionForCustomer(CrmCustomersResourceMbInfo crmSubscriber);

//        /// <summary>
//        /// This is the API to update subscriber services to HLR.
//        /// Return empty message if update successfully.
//        /// Return detail error message if update failed.
//        /// </summary>
//        /// <param name="crmSubscriber"></param>
//        /// <param name="partialUpdateServiceTypes"></param>
//        /// <returns>empty means successful, otherwise errro message</returns>
//        string UpdatePartialServiceForCustomer(CrmCustomersResourceMbInfo crmSubscriber, SuplementaryServiceType[] partialUpdateServiceTypes);


//        /// <summary>
//        /// ResourcesMB's status ID changed.
//        /// For example, when change from active(1) to delete(20) , will delete MSISDN from HLR.
//        /// 1. resoruceMbInfo = the new ResourceMBInfo in CRM_CUSTOMERS_RESOURCEMB. Make sure the Resource and StatusID and IMSI is up-to-date.
//        /// 2. Old statusID = the old statusID
//        /// </summary>
//        /// <returns></returns>
//        int ResourceMBStatusChange(CrmCustomersResourceMbInfo newResoruceMbInfo,int oldStatusId, LoginInfo loginInfo);

//        /// <summary>
//        /// Request SIM Swap to HPS.
//        /// 1. newResoruceMbInfo = the new ResourceMBInfo in CRM_CUSTOMERS_RESOURCEMB, make sure the IMSI and MSISDN are up-to-date.
//        /// 2. oldResoruceMbInfo = the old ResourceMBInfo, make sure the IMSI, MSISDN and ICCID is the old value.
//        /// </summary>
//        int SwapSIMCard(CrmCustomersResourceMbInfo newResoruceMbInfo, CrmCustomersResourceMbInfo oldResoruceMbInfo, LoginInfo loginInfo);

//        /// <summary>
//        /// Request MSISDN Swap to HPS.
//        /// 1. newResoruceMbInfo = the new ResourceMBInfo in CRM_CUSTOMERS_RESOURCEMB, make sure the IMSI and MSISDN are up-to-date.
//        /// 2. oldResoruceMbInfo = the old ResourceMBInfo, make sure the IMSI, MSISDN, ICCID is the old value.
//        /// </summary>
//        int SwapMSISDN(CrmCustomersResourceMbInfo newResoruceMbInfo, CrmCustomersResourceMbInfo oldResoruceMbInfo, LoginInfo loginInfo);

//        /// <summary>
//        /// Change the MSISDN to a new Provision Template.
//        /// </summary>
//        /// <param name="msIsdn"></param>
//        /// <param name="templateId"></param>
//        /// <param name="loginInfo"></param>
//        /// <returns></returns>
//        bool ChangeProvisioningTemplate(string msIsdn, int templateId, LoginInfo loginInfo);

//        /// <summary>
//        /// Check if MSISDN exists in HLR.
//        /// </summary>
//        /// <returns></returns>
//        bool IsImsiActived(string imsi);

//        /// <summary>
//        /// Gets the CrmCustomerResourceMBInfo by customer id.
//        /// This is a database operation.
//        /// </summary>
//        /// <param name="customerId">The customer id.</param>
//        /// <returns></returns>
//        CrmCustomersResourceMbInfo GetResourceMBByCustomerID(int customerId);

//        /// <summary>
//        /// Gets the provision data by MSISDN.
//        /// This is a database+HLR operation.
//        /// It firstly read CRM_Customer_ResourceMB table, then use the IMSI as key to query HLR.
//        /// </summary>
//        /// <param name="msisdn"></param>
//        /// <returns></returns>
//        CrmCustomersResourceMbInfo GetResourceMbByMsisdn(string msisdn);

//        /// <summary>
//        /// Delete the provision template by provision template ID from database.
//        /// </summary>
//        /// <param name="ProvisionTemplateID"></param>
//        /// <returns></returns>
//        bool DeleteProvisionTemplate(int ProvisionTemplateID);
//        /// <summary>
//        ///Delete provision from Hlr server.
//        /// </summary>
//        /// <param name="imsi">The imsi.</param>
//        /// <returns></returns>
//        bool DeleteProvisioningSubscriber(string imsi);


//        /// <summary>
//        /// Get the CrmCustomersResourceMbInfo by imsi, this is a database + HLR operation.
//        /// It firstly read CRM_Customers_resourcemb by IMSI, then use the IMSI to query HLR.
//        /// </summary>
//        /// <param name="imsi"></param>
//        /// <returns></returns>
//        CrmCustomersResourceMbInfo GetResourceByImsi(string imsi);


//        /// <summary>
//        /// Legacy method. IHPSProvision support this method only for backward compatible.
//        /// newPackageID = 
//        /// </summary>
//        /// <param name="newProduct"></param>
//        /// <param name="loginInfo"></param>
//        /// <returns></returns>
//        bool UpdateCustomersProducts(ProductInfo newProduct, LoginInfo loginInfo);

//        /// <summary>
//        /// Deactivate a subscriber in HLR
//        /// </summary>
//        /// <param name="MSISDN"></param>
//        /// <param name="IMSI"></param>
//        /// <returns></returns>
//        bool DeactivateSubscriber(string MSISDN,string IMSI);

//        /// <summary>
//        /// Activate a subscriber in HLR
//        /// </summary>
//        /// <param name="MSISDN"></param>
//        /// <param name="IMSI"></param>
//        /// <returns></returns>
//        bool ActivateSubscriber(string MSISDN, string IMSI);

//        /// <summary>
//        /// Gets the default provision.
//        /// </summary>
//        /// <param name="dealerId">The dealer id.</param>
//        /// <param name="icc">The icc.</param>
//        /// <returns></returns>
//        CrmCustomersResourceMbInfo BuildDefaultCrmSubscriber(int dealerId, string icc);

//        /// <summary>
//        /// Gets the default provision.
//        /// </summary>
//        CrmCustomersResourceMbInfo BuildDefaultCrmSubscriber(int dealerId, string icc, string resource, int packageId);
        
//        /// <summary>
//        /// Query provision template by dealer ID.
//        /// </summary>
//        /// <param name="dealerId"></param>
//        /// <returns></returns>
//        IList<CrmDefaultProvisionInfo> GetAllCrmProvisioningTemplatesOfDealer(int dealerId);

//        /// <summary>
//        /// BuildDefaultCrmSubscriberFromTemplate
//        /// </summary>
//        /// <param name="templateId"></param>
//        /// <param name="resource"></param>
//        /// <param name="icc"></param>
//        /// <returns></returns>
//        CrmCustomersResourceMbInfo BuildDefaultCrmSubscriberFromTemplate(int templateId, string resource, string icc);

//        /// <summary>
//        /// Get all QoSTemplates from HLR.
//        /// </summary>
//        /// <returns></returns>
//        IList<ProvisioningApnTemplate> GetApnQosTemplates();

//        /// <summary>
//        /// Get the QoSTemplate from HLR where name matches to parameter.
//        /// </summary>
//        /// <param name="templateId"></param>
//        /// <returns></returns>
//        ProvisioningApnTemplate GetApnQosTemplate(string templateId);

//        /// <summary>
//        /// DeleteSimcardFromAUC
//        /// </summary>
//        /// <param name="IMSI"></param>
//        /// <returns></returns>
//        bool DeleteSimcardFromAUC(string IMSI);

//        /// <summary>
//        /// Verify new simcard for provisioning.
//        /// </summary>
//        /// <param name="simcardToBeVerified"></param>
//        /// <returns>empty means ok, otherwise returns error message</returns>
//        string VerifyNewProvisioningSimcard(SIMCardInfo simcardToBeVerified);

//        /// <summary>
//        /// Verify new msisdn for provisioning
//        /// </summary>
//        /// <param name="msisdn"></param>
//        /// <returns>empty means ok, otherwise returns error message</returns>
//        string VerifyNewProvisioningMSISDN(string msisdn);

//        /// <summary>
//        /// ActivateProvisioningSubscriber
//        /// </summary>
//        /// <param name="crmSubscriber"></param>
//        /// <param name="provisioningTarget"></param>
//        /// <returns></returns>
//        bool ActivateProvisioningSubscriber(CrmCustomersResourceMbInfo crmSubscriber, EProvisioningSystem provisioningTarget);

//        /// <summary>
//        /// DeactivateProvisioningSubscriber
//        /// </summary>
//        /// <param name="crmSubscriber"></param>
//        /// <param name="provisioningTarget"></param>
//        /// <returns></returns>
//        bool DeactivateProvisioningSubscriber(CrmCustomersResourceMbInfo crmSubscriber, EProvisioningSystem provisioningTarget);
//    }


//}