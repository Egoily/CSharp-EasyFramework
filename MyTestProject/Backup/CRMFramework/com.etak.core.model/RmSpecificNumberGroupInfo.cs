using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2009-9-16 16:54:48
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2009-9-16 16:54:48
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2009-9-16 16:54:48
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2009-9-16 16:54:48
    /// </summary>
    [DataContract]
    [Serializable]
    public class RmSpecificNumberGroupInfo : PromotionModelBase
    {
        public virtual int GroupId { get; set; }

        public virtual  string GroupName { get; set; }
        public virtual  RmPromotionPlanInfo RmPromotionPlanInfo { get; set; }
        public virtual  int SpecificNumberCategoryId { get; set; }
        public virtual  int TrafficTypeId { get; set; }
        public virtual  int ServiceTypeId { get; set; }

        public virtual  int SubServiceTypeId { get; set; }
        public virtual  int DealerId { get; set; }
        public virtual  decimal Setup { get; set; }
        public virtual  decimal Tariff1 { get; set; }
        public virtual  decimal Tariff2 { get; set; }
        public virtual  int DiscountMethodId { get; set; }
        public virtual  int MaxNumberCount { get; set; }
        public virtual  DateTime? StartDate { get; set; }
        public virtual  DateTime? EndDate { get; set; }

      

      

    }
}
