using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract, Serializable]
    public class AddressInfo
    {
        /// <summary>
        /// Unique Id of the AddressInfo
        /// </summary>
        public virtual Int64 Id { get; set; }

        public virtual string Address { get; set; }

        public virtual string Block { get; set; }

        public virtual string BuildingDoor { get; set; }

        public virtual string Portal { get; set; }

        public virtual string Stair { get; set; }

        public virtual string Door { get; set; }

        public virtual string HouseNo { get; set; }

        public virtual string HouseExtention { get; set; }

        public virtual int Province { get; set; }

        public virtual string Area { get; set; }

        public virtual string Neighborhood { get; set; }

        public virtual string Suburb { get; set; }

        public virtual string City { get; set; }

        public virtual string ZipCode { get; set; }

        public virtual int? PoBox { get; set; }

        public virtual string State { get; set; }

        public virtual int? CountryId { get; set; }

        public virtual int? Status { get; set; }

        public virtual DateTime? CreateDate { get; set; }

        public virtual int CreateUser { get; set; }

        public virtual string Comments { get; set; }

        public virtual IList<PersonInfo> Owners { get; set; }

        public virtual bool CompareInformation(AddressInfo compareObj)
        {
            return (string.IsNullOrEmpty(this.Address) ? "" : this.Address) == (string.IsNullOrEmpty(compareObj.Address) ? "" : compareObj.Address) &&
                   (string.IsNullOrEmpty(this.Area) ? "" : this.Area) == (string.IsNullOrEmpty(compareObj.Area) ? "" : compareObj.Area) &&
                   (string.IsNullOrEmpty(this.Block) ? "" : this.Block) == (string.IsNullOrEmpty(compareObj.Block) ? "" : compareObj.Block) &&
                   (string.IsNullOrEmpty(this.BuildingDoor) ? "" : this.BuildingDoor) ==
                   (string.IsNullOrEmpty(compareObj.BuildingDoor) ? "" : compareObj.BuildingDoor) &&
                   (string.IsNullOrEmpty(this.City) ? "" : this.City) == (string.IsNullOrEmpty(compareObj.City) ? "" : compareObj.City) &&
                   this.CountryId == compareObj.CountryId &&
                   (string.IsNullOrEmpty(this.Door) ? "" : this.Door) == (string.IsNullOrEmpty(compareObj.Door) ? "" : compareObj.Door) &&
                   (string.IsNullOrEmpty(this.HouseExtention) ? "" : this.HouseExtention) ==
                   (string.IsNullOrEmpty(compareObj.HouseExtention) ? "" : compareObj.HouseExtention) &&
                   (string.IsNullOrEmpty(this.HouseNo) ? "" : this.HouseNo) == (string.IsNullOrEmpty(compareObj.HouseNo) ? "" : compareObj.HouseNo) &&
                   this.PoBox == compareObj.PoBox &&
                   (string.IsNullOrEmpty(this.Portal) ? "" : this.Portal) == (string.IsNullOrEmpty(compareObj.Portal) ? "" : compareObj.Portal) &&
                   (string.IsNullOrEmpty(this.Stair) ? "" : this.Stair) == (string.IsNullOrEmpty(compareObj.Stair) ? "" : compareObj.Stair) &&
                   (string.IsNullOrEmpty(this.State) ? "" : this.State) == (string.IsNullOrEmpty(compareObj.State) ? "" : compareObj.State) &&
                   (string.IsNullOrEmpty(this.Suburb) ? "" : this.Suburb) == (string.IsNullOrEmpty(compareObj.Suburb) ? "" : compareObj.Suburb) &&
                   (string.IsNullOrEmpty(this.ZipCode) ? "" : this.ZipCode) == (string.IsNullOrEmpty(compareObj.ZipCode) ? "" : compareObj.ZipCode);
        }
    }
}