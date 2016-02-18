using System;
using com.etak.core.model.utils;

namespace com.etak.core.model.dto
{
    /// <summary>
    /// DTO Object corresponding with the BankInfo entity
    /// </summary>
    public class BankInformationDTO
    {
        /// <summary>
        /// BankCode of the Bank
        /// </summary>
        public virtual string BankCode { get; set; }
        /// <summary>
        /// The name of the Bank
        /// </summary>
        public virtual string BankName { get; set; }
        /// <summary>
        /// The Number of the Bank
        /// </summary>
        public virtual string BankNumber { get; set; }
        /// <summary>
        /// The owner
        /// </summary>
        public virtual string Owner { get; set; }
        /// <summary>
        /// City
        /// </summary>
        public virtual string City { get; set; }
        /// <summary>
        /// Country where the Bank is.
        /// </summary>
        public virtual int? CountryID { get; set; }
        /// <summary>
        /// Iban corresponding with the account
        /// </summary>
        public virtual string IBAN { get; set; }
        /// <summary>
        /// Swift corresponding with the account
        /// </summary>
        public virtual string Swift { get; set; }
        /// <summary>
        /// CVC corresponding with the account
        /// </summary>
        public virtual string CVC { get; set; }
        /// <summary>
        /// ABI corresponding with the account
        /// </summary>
        public virtual string ABI { get; set; }
        /// <summary>
        /// CAB corresponding with the account
        /// </summary>
        public virtual string CAB { get; set; }
        /// <summary>
        /// Valid Date
        /// </summary>
        public virtual string ValidDate { get; set; }
        /// <summary>
        /// When the account has been created
        /// </summary>
        public virtual DateTime? CreateDate { get; set; }
        /// <summary>
        /// Account Code
        /// </summary>
        public virtual string AccountCode { get; set; }
        /// <summary>
        /// Start Date of the account
        /// </summary>
        public virtual DateTime? StartDate { get; set; }
        /// <summary>
        /// End Date of the account 
        /// </summary>
        public virtual DateTime? EndDate { get; set; }


        /// <summary>
        /// Equals function to determine whether two objects are equal or not
        /// </summary>
        /// <param name="obj">The BankInformationDTO to be compared</param>
        /// <returns>True if are equal, false otherwise</returns>
        public override bool Equals(object obj)
        {
            BankInformationDTO compareObj = obj as BankInformationDTO;
            if (compareObj == null)
                return false;
            if (Comparators.SafeStringComparer(BankCode, compareObj.BankCode) &&
                Comparators.SafeStringComparer(BankName, compareObj.BankName) &&
                Comparators.SafeStringComparer(BankNumber, compareObj.BankNumber) &&
                Comparators.SafeStringComparer(Owner, compareObj.Owner) &&
                Comparators.SafeStringComparer(City, compareObj.City) && 
                CountryID == compareObj.CountryID &&
                Comparators.SafeStringComparer(IBAN, compareObj.IBAN) &&
                Comparators.SafeStringComparer(Swift, compareObj.Swift) &&
                Comparators.SafeStringComparer(CVC, compareObj.CVC) &&
                Comparators.SafeStringComparer(ABI, compareObj.ABI) &&
                Comparators.SafeStringComparer(CAB, compareObj.CAB) &&
                Comparators.SafeStringComparer(ValidDate, compareObj.ValidDate) &&
                CreateDate == compareObj.CreateDate &&
                Comparators.SafeStringComparer(AccountCode, compareObj.AccountCode) &&
                StartDate == compareObj.StartDate &&
                EndDate == compareObj.EndDate)
                return true;

            return false;
        }

       

        public override int GetHashCode()
        {
            return
                Comparators.SafeHashCode(BankCode) &
                Comparators.SafeHashCode(BankName) &
                Comparators.SafeHashCode(BankNumber) &
                Comparators.SafeHashCode(Owner) &
                Comparators.SafeHashCode(City) &
                Comparators.SafeHashCode(CountryID) &
                Comparators.SafeHashCode(IBAN) &
                Comparators.SafeHashCode(Swift) &
                Comparators.SafeHashCode(CVC) &
                Comparators.SafeHashCode(ABI) &
                Comparators.SafeHashCode(CAB) &
                Comparators.SafeHashCode(ValidDate) &
                Comparators.SafeHashCode(CreateDate) &
                Comparators.SafeHashCode(AccountCode) &
                Comparators.SafeHashCode(StartDate) &
                Comparators.SafeHashCode(EndDate);
        }

       

    }
}
