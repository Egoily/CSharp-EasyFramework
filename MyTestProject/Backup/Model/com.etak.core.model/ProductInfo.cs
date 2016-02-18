using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class ProductInfo
    {          
        public virtual int? ProductID { get; set; }
        public virtual int? ServiceTypeID { get; set; }
        public virtual decimal? CreditLimit { get; set; }
        public virtual decimal? SpecialCreditLimit { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual int? UserID { get; set; }
        public virtual decimal? ExactcreditLimit { get; set; }
        public virtual IList<ServicesInfo> ServiceInfo { get; set; }
        public virtual PackageInfo PackageDefinition { get; set; }
        public virtual CustomerInfo CustomerInfo { get; set; }      
  

        public virtual ProductInfo Clone()
        {
            return this.MemberwiseClone() as ProductInfo;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            ProductInfo cProd = obj as ProductInfo;
            if (cProd == null)
                return false;

            return (cProd.ProductID == this.ProductID);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
