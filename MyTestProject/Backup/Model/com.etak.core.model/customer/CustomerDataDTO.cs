using System;

namespace com.etak.core.model.dto
{
    //TODO: Get the possible valies from CRM code.
    /// <summary>
    /// Types of document IDs available in Spain
    /// </summary>
    public enum DocumentTypes
    {
        /// <summary>
        /// Passport document
        /// </summary>
        Passport = 1,

        /// <summary>
        /// Driving Licence document
        /// </summary>
        DrivingLicence = 2,

        /// <summary>
        /// National identification card
        /// </summary>
        DNI = 3,

        /// <summary>
        /// National fiscal identification card
        /// </summary>
        NIF = 4,

        /// <summary>
        /// Foreigns identification card
        /// </summary>
        NIE = 5,

        /// <summary>
        /// Fiscal Id for companies
        /// </summary>
        CIF = 6,
    }

    //TODO: Get the possible valies from CRM code.
    /// <summary>
    /// Possible titles of a of customer
    /// </summary>
    public enum PersonTitles
    {
        /// <summary>
        /// Mr title
        /// </summary>
        Mr = 1,
        /// <summary>
        /// Miss title
        /// </summary>
        Miss = 0,
    }

    /// <summary>
    /// Genders of the customer
    /// </summary>
    public enum Genders
    {
        /// <summary>
        /// Male Gender
        /// </summary>
        Male,
        /// <summary>
        /// Female Gender
        /// </summary>
        Female,
    }

    /// <summary>
    /// Personal information of the customer
    /// </summary>

    public class CustomerDataDTO
    {
        /// <summary>
        /// The title of the Customer
        /// </summary>
        public virtual PersonTitles? Title { get; set; }
        /// <summary>
        /// Gender of the Customer
        /// </summary>
        public virtual Genders? Gender { get; set; }
        /// <summary>
        /// Initials of the Customer
        /// </summary>
        public virtual string Initials { get; set; }

        /// <summary>
        /// FirstName of the Customer
        /// </summary>
        public virtual string FirstName { get; set; }
        /// <summary>
        /// LastName of the Customer
        /// </summary>
        public virtual string LastName { get; set; }
        /// <summary>
        /// LastName2 of the Customer
        /// </summary>
        public virtual string LastName2 { get; set; }
        /// <summary>
        /// MiddleName of the Customer
        /// </summary>
        public virtual string MiddleName { get; set; }
        /// <summary>
        /// Document Type of the Customer
        /// </summary>
        public virtual DocumentTypes DocumentType { get; set; }

        /// <summary>
        /// Document Number of the Customer
        /// </summary>
        public virtual String DocumentNumber { get; set; }
        /// <summary>
        /// Company where the Customer works
        /// </summary>
        public virtual string Company { get; set; }
        /// <summary>
        /// Customer's telephone
        /// </summary>
        public virtual string Telephone { get; set; }
        /// <summary>
        /// Customer's telefax
        /// </summary>
        public virtual string Telefax { get; set; }
        /// <summary>
        /// Customer's mobile
        /// </summary>
        public virtual string Mobile { get; set; }
        /// <summary>
        /// Customer's email
        /// </summary>
        public virtual string Email { get; set; }
        /// <summary>
        /// Customer's birthday
        /// </summary>
        public virtual DateTime? BirthDay { get; set; }
        /// <summary>
        /// DTO object corresponding with the fiscal address
        /// </summary>
        public AddressDTO FiscalAddress { get; set; }
        /// <summary>
        /// DTO object corresponding with the delivery address
        /// </summary>
        public AddressDTO DeliveryAddress { get; set; }
        /// <summary>
        /// DTO object corresponding with the Customer Address
        /// </summary>
        public AddressDTO CustomerAddress { get; set; }
        /// <summary>
        /// DTO object corresponding with
        /// </summary>
        public BankInformationDTO BankInformation { get; set; }
        /// <summary>
        /// Pending Status of the Customer
        /// </summary>
        public PendingStatus ? PendingStatus { get; set; }
        /// <summary>
        /// Nationality
        /// </summary>
        public int? Nationality { get; set; }

    }
}
