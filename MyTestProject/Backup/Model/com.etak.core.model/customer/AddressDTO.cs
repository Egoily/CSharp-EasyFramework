using com.etak.core.model.utils;

namespace com.etak.core.model.dto
{
    /// <summary>
    /// Address details of a customer
    /// </summary>

    public class AddressDTO
    {
        /// <summary>
        /// Street
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// Block of the Address
        /// </summary>
        public virtual string Block { get; set; }
        /// <summary>
        /// Building Door of the Address
        /// </summary>
        public virtual string BuildingDoor { get; set; }
        /// <summary>
        /// Portal of the Address
        /// </summary>
        public virtual string Portal { get; set; }

        /// <summary>
        /// Stair of the Address
        /// </summary>
        public virtual string Stair { get; set; }

        /// <summary>
        /// Door of the Address
        /// </summary>
        public virtual string Door { get; set; }

        /// <summary>
        /// House Number of the Address
        /// </summary>
        public virtual string HouseNo { get; set; }

        /// <summary>
        /// House extension if it's needed more information
        /// </summary>
        public virtual string HouseExtension { get; set; }

        /// <summary>
        /// Area corresponding to the address
        /// </summary>
        public virtual string Area { get; set; }

        /// <summary>
        /// Neighborhood of the Address
        /// </summary>
        public virtual string Neighborhood { get; set; }

        /// <summary>
        /// Suburd
        /// </summary>
        public virtual string Suburb { get; set; }

        /// <summary>
        /// The city of the Address
        /// </summary>
        public virtual string City { get; set; }

        /// <summary>
        /// Zipcode of the Address
        /// </summary>
        public virtual string ZipCode { get; set; }

        /// <summary>
        /// PoBox if it's needed
        /// </summary>
        public virtual int PoBox { get; set; }

        /// <summary>
        /// The State
        /// </summary>
        public virtual string State { get; set; }

        /// <summary>
        /// Country Id corresponding to the address's couyntry
        /// </summary>
        public virtual int CountryId { get; set; }

        /// <summary>
        /// The Status of this Address
        /// </summary>
        public virtual int Status { get; set; }

        /// <summary>
        /// Equals Function to check if two Address are equal
        /// </summary>
        /// <param name="obj">An AddressDTO to be checked</param>
        /// <returns>True if equal, false otherwise</returns>
        public override bool Equals(object obj)
        {
            AddressDTO compareObj = obj as AddressDTO;
            if (compareObj == null)
                return false;

            if (Comparators.SafeStringComparer(Address, compareObj.Address) &&
                Comparators.SafeStringComparer(Area ,compareObj.Area) &&
                Comparators.SafeStringComparer(Block,compareObj.Block) &&
                Comparators.SafeStringComparer(BuildingDoor,compareObj.BuildingDoor) &&
                Comparators.SafeStringComparer(City, compareObj.City) && 
                CountryId == compareObj.CountryId &&
                Comparators.SafeStringComparer(Door,compareObj.Door) &&
                Comparators.SafeStringComparer(HouseExtension,compareObj.HouseExtension) &&
                Comparators.SafeStringComparer(HouseNo, compareObj.HouseNo) &&
                PoBox == compareObj.PoBox &&
                Comparators.SafeStringComparer(Portal,compareObj.Portal) &&
                Comparators.SafeStringComparer(Stair, compareObj.Stair) &&
                Comparators.SafeStringComparer(State,compareObj.State) &&
                Comparators.SafeStringComparer(Suburb,compareObj.Suburb) &&
                Comparators.SafeStringComparer(ZipCode,compareObj.ZipCode)
               )
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return
                Comparators.SafeHashCode(Address) &
                Comparators.SafeHashCode(Area) &
                Comparators.SafeHashCode(Block) &
                Comparators.SafeHashCode(BuildingDoor) &
                Comparators.SafeHashCode(City) &
                Comparators.SafeHashCode(CountryId) &
                Comparators.SafeHashCode(Door) &
                Comparators.SafeHashCode(HouseExtension) &
                Comparators.SafeHashCode(HouseNo) &
                Comparators.SafeHashCode(PoBox) &
                Comparators.SafeHashCode(Portal) &
                Comparators.SafeHashCode(Stair) &
                Comparators.SafeHashCode(State) &
                Comparators.SafeHashCode(Suburb) &
                Comparators.SafeHashCode(ZipCode);
        }
    }
}
