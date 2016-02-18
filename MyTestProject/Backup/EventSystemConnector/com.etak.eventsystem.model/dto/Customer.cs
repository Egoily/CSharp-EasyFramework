using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace com.etak.eventsystem.model.dto
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]    
    public enum CustomerStatus
    {
        [EnumMember]
        NotSet = 0,
        [EnumMember]
        Pending = 1,
        [EnumMember]
        Active = 2,
        [EnumMember]
        Terminated = 3,
        [EnumMember]
        Rejected = 4,
        [EnumMember]
        PreActive = 5,
        [EnumMember]
        Regulatory = 6
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class Customer : LoadeableEntity
    {
        [DataMember()]
        public virtual Int32 CustomerID { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual Int32? DealerID { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual String Company { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual String Contact { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual Int32? TitleID { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual Int32? GenderID { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string Address { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string HouseNO { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string Zipcode { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string City { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string STATE { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string SUBURB { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual int? CountryID { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string Telephone { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string Telefax { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string Mobile { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string Email { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string Choc { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string VAT { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual DateTime? CreateDate { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual int? BillingDate { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual DateTime? ActivedDate { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string Initials { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string MiddleName { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string LastName { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string HouseExtention { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual byte [] UpdateStamp { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual int Status { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual int? ParentControl { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string CreateUserName { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string Area { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual int? PoBox { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual DateTime? DateOfBirth { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string ArName { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string FirstName { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string LastName2 { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string SalesShopID { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string SalesSellerID { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual string Neighborhood { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual IList<CustomerProperty> CustomerPropertyList { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual IList<MobileLineService> ResourcesList { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual IList<Product> Products { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public virtual IList<Service> Services { get; set; }     
       
    }
}