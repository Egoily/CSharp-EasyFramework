using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class PackageInfo
    { 
        virtual public  int? PackageID {get; set;}
        virtual public int? DealerID { get; set; }
        virtual public string PackageName { get; set; }
        virtual public int? PaymentTypeID { get; set; }
        virtual public int? ServiceTypeID { get; set; }
        virtual public decimal? CreditLimit { get; set; }
        virtual public int? InvoiceTemplateID { get; set; }
        virtual public decimal? SpecialCreditLimit { get; set; }
        virtual public int? PromotionPackageID { get; set; }
        virtual public int? PackageLevel { get; set; }
        virtual public IList<BundlePackageInfo> BundlePackageInfo { get; set; }
        public virtual IList<PackageBussinessRules> Rules { get; set; }   

        public PackageInfo()
        {
            BundlePackageInfo = new List<BundlePackageInfo>();
            Rules = new List<PackageBussinessRules>();
        }

        public static Boolean operator ==(PackageInfo c1, PackageInfo c2)
        { 
            if (ReferenceEquals(null, c1))
            {
                return ReferenceEquals(null, c2);
            }
            return (c1.Equals(c2));
        }

        public static Boolean operator !=(PackageInfo c1, PackageInfo c2)
        {
            if (ReferenceEquals(null, c1))
            {
                return !ReferenceEquals(null, c2);
            }
            return (!c1.Equals(c2));
        }


        public virtual bool Equals(PackageInfo other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return this.PackageID.Equals(other.PackageID) && this.DealerID.Equals(other.DealerID);
        }


        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            PackageInfo objCasted = other as PackageInfo;
            return Equals(objCasted);
        }

        public override int GetHashCode()
        {
            return this.PackageID.GetHashCode() + this.DealerID.GetHashCode();
        }
    }
}
