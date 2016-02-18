using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class CustomerInfo
    {
        /// <summary>
        /// THe products of the customer
        /// </summary>
        public virtual IList<ProductInfo> ProductsInfo { get; set; }

        /// <summary>
        /// The customer properties
        /// </summary>
        public virtual IList<PropertyInfo> PropertyInfo { get; set; }

        /// <summary>
        /// GSM subscriptions associated to the customer
        /// </summary>
        public virtual IList<ResourceMBInfo> ResourceMBInfo { get; set; }
        /// <summary>
        /// Remarks for storing information of portability.
        /// </summary>
        public virtual IList<RemarksInfo> RemarksInfo { get; set; }

        /// <summary>
        ///  mvno related custoemr properties.
        /// </summary>
        public virtual IList<MVNOCustomerPropertyInfo> MVNOCustomerPropertyInfo { get; set; }
        /// <summary>
        /// Customer Bank Information
        /// </summary>
        public virtual IList<BankInfo> BankInfo { get; set; }
        
        /// <summary>
        /// Customer Dealer Mapping
        /// </summary>
        public virtual IList<MappingInfo> MappingInfo { get; set; }
        /// <summary>
        /// Customer Service Info, like Voice, Data, SMS etc..
        /// </summary>
        public virtual IList<ServicesInfo> ServicesInfo { get; set; }
        /// <summary>
        /// GSM subscriptions associated to the customer for provisioning.
        /// </summary>
        public virtual IList<CrmCustomersResourceMbInfo> ProvisionResourceMBInfo { get; set; }
        /// <summary>
        /// Customer Credit Cards
        /// </summary>
        public virtual IList<CustomerCreditCard> CustomerCreditCards { get; set; }
        /// <summary>
        /// Customer Promotions
        /// </summary>
        public virtual IList<CrmCustomersPromotionInfo> Promotions { get; set; }
        /// <summary>
        /// Customer Promotion Groups
        /// </summary>
        public virtual IList<CrmCustomersPromotionGroup> PromotionGroups { get; set; }
        /// <summary>
        /// Customer Address
        /// </summary>
        public virtual IList<CustomerAddress> Addresses { get; set; }
        /// <summary>
        /// Revenue Products
        /// </summary>
        public virtual IList<CustomerProductAssignment> RevenueProductsInfo { get; set; }
        public virtual IList<CustomerDataRoamingLimit> DataRoamingLimits { get; set; }
        public virtual IList<CustomerDataRoamingLimitNotification> DataRoamingLimitNotifications { get; set; }
 
        /// <summary>
        /// Unique id of the customer
        /// </summary>
        public virtual int? CustomerID { get; set; }

        /// <summary>
        /// DealerId of this customer
        /// </summary>
        public virtual int? DealerID { get; set; }
        /// <summary>
        /// Parent Customer Id, null means no parent.
        /// </summary>
        public virtual int? ParentID { get; set; }
        /// <summary>
        /// Company
        /// </summary>
        public virtual string Company { get; set; }
        /// <summary>
        /// Contact
        /// </summary>
        
        public virtual string Contact { get; set; }
        
        public virtual int? TitleID { get; set; }
        public virtual int? GenderID { get; set; }       
        /// <summary>
        /// Telephone contact information
        /// </summary>
        public virtual string Telephone { get; set; }
        
        /// <summary>
        /// Fax contact information
        /// </summary>
        public virtual string Telefax { get; set; }
        
        /// <summary>
        /// Mobile contact information
        /// </summary>
        public virtual string Mobile { get; set; }

        /// <summary>
        /// Email contact information
        /// </summary>
        public virtual string Email { get; set; }
        public virtual string Choc { get; set; }
        public virtual string VAT { get; set; }
        
        /// <summary>
        /// Date in which te customer was created
        /// </summary>
        public virtual DateTime? CreateDate { get; set; }

        /// <summary>
        /// Id of the user that created the customer
        /// </summary>
        public virtual int? UserID { get; set; }

        public virtual int? BillingDate { get; set; }
        
        public virtual DateTime? ActivedDate { get; set; }

        /// <summary>
        /// First name of the customer
        /// </summary>
        public virtual string FirstName { get; set; }

        

        /// <summary>
        /// Initials of the customer
        /// </summary>
        public virtual string Initials { get; set; }

        /// <summary>
        /// Middle name of the customer
        /// </summary>
        public virtual string MiddleName { get; set; }

        /// <summary>
        /// Last name of the customer
        /// </summary>
        public virtual string LastName { get; set; }

        /// <summary>
        /// Second last name of the customer
        /// </summary>
        public virtual string LastName2 { get; set; }

        public virtual byte[] UpdateStamp { get; set; }

        /// <summary>
        /// Status of the customer
        /// </summary>
        public virtual int? StatusID { get; set; }
        public virtual int? ParentControl { get; set; }
        public virtual string CreateUserName { get; set; }  
    
        /// <summary>
        /// Date of birth of the customer
        /// </summary>
        public virtual DateTime? DateOfBirth { get; set; }
       /// <summary>
       /// The sales shop that customer get registerd.
       /// </summary>
        public virtual string SalesShopID { get; set; }
        /// <summary>
        /// Sales seller
        /// </summary>
        public virtual string SalesSellerID { get; set; }     
        public virtual string Category { get; set; }
        /// <summary>
        /// Registration Type
        /// 1. Common  2. Internal Portability, 3 external Portability
        /// </summary>
        public virtual int RegistrationType { get; set; }
        public virtual string ArName { get; set; }
        /// <summary>
        /// Heleper property to get the FiscalAddress of the customer
        /// </summary>
        public virtual AddressInfo FiscalAddress
        {
            get
            {
                var fiscalAddress = this.Addresses.Where(x => x.UsageType == AddressUsages.FiscalAddress).Select(y => y.Address).FirstOrDefault();

                return fiscalAddress;
            }
        }
       
        public CustomerInfo()
        {
            ProductsInfo = new List<ProductInfo>();
            PropertyInfo = new List<PropertyInfo>();
            ResourceMBInfo = new List<ResourceMBInfo>();
            MVNOCustomerPropertyInfo = new List<MVNOCustomerPropertyInfo>();
            BankInfo = new List<BankInfo>();
            MappingInfo = new List<MappingInfo>();
            ServicesInfo = new List<ServicesInfo>();
            RevenueProductsInfo = new List<CustomerProductAssignment>();
            ProvisionResourceMBInfo = new List<CrmCustomersResourceMbInfo>();
            CustomerCreditCards = new List<CustomerCreditCard>();
            Promotions = new List<CrmCustomersPromotionInfo>();
            PromotionGroups = new List<CrmCustomersPromotionGroup>();
            Addresses = new List<CustomerAddress>();
            DataRoamingLimitNotifications = new List<CustomerDataRoamingLimitNotification>();
            DataRoamingLimits = new List<CustomerDataRoamingLimit>();
            //RemarksInfo = new List<RemarksInfo>();
        }

        public virtual CustomerInfo Clone()
        {
            CustomerInfo customerInfo = this.MemberwiseClone() as CustomerInfo;
            return customerInfo;
        }
    }
}
