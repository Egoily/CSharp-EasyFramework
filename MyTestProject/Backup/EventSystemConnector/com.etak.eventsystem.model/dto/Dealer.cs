using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.eventsystem.model.dto
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public enum MVNOBusinessType
    {
       
        /// <summary>
        ///  Mobile MVNO
        /// </summary>
        [EnumMember]
        Unknown = 0,

        
        /// <summary>
        ///  Mobile MVNO
        /// </summary>
        [EnumMember]
        Mobile = 1,

       
        /// <summary>
        /// WIFI MVNO
        /// </summary>
        [EnumMember]
        WIFI = 2,
        
       
        /// <summary>
        /// CPS MVNO
        /// </summary>
        [EnumMember]
        CPS = 3,

       
        /// <summary>
        /// PRS MVNO
        /// </summary>
        [EnumMember]
        PRS = 4,
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class Dealer : LoadeableEntity
    {
        [DataMember(EmitDefaultValue = false)]
		public virtual Int32 DealerID { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? ParentID { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual string DealerNode { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? DealerTypeID { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? FiscalUnitID { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? ResellerID { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? AgentID { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual int? SubagentID { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual string Company { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual string Contact { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? TitleID { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? GenderID { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual string Address { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual string HouseNO { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual string Zipcode { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual string City { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? CountryID { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual string Telephone { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual string Telefax { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual string Email { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual string CHOC { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual string VAT { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual DateTime? CreateDate { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? UserID { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? CreateUser { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? UpdateUser { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual DateTime? UpdateDate { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? State { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? Hide { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? MvnotypeID { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual IList<DealerProperty> DealerProperties { get; set; }
    }
}