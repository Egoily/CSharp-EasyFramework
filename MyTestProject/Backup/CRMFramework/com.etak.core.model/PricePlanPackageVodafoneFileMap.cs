using System;

namespace com.etak.core.model
{
    [Serializable]
    public class PricePlanPackageVodafoneFileMap
    {
        virtual public Int32 Id { get; set; }
        virtual public String VMNO { get; set; }
        virtual public Int32 PricePlan { get; set; }
        virtual public Int32 PackageId { get; set; }
        virtual public String UserCategory { get; set; }
    }
}
