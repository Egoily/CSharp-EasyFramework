using System;

namespace com.etak.core.model
{
    [Serializable]
    public class IDMappingInfo
    {
        virtual public long ID { get; set; }
        virtual public int MvnoId { get; set; }
        virtual public string ExternalID1 { get; set; }
        virtual public string ExternalID2 { get; set; }
        virtual public string ETID1 { get; set; }
        virtual public string ETID2 { get; set; }
        virtual public string ETID3 { get; set; }
        virtual public IDMappingType MappingType { get; set; }
    }

    [Serializable]
    public enum IDMappingType
    {
        PromotionPlan = 0,
        Currency = 1,
        CardGroup = 2,
        /// <summary>
        /// Resource status for Mexico
        /// </summary>
        ResourceStatus = 3 //Added by Ego.2013-12-11
    }
}
