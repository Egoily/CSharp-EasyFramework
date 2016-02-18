using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2010-10-12 14:58:11
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2010-10-12 14:58:11
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2010-10-12 14:58:11
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2010-10-12 14:58:11
   /// </summary>
    [DataContract]
    [Serializable]
    public class MVNOConfigActionInfo
   {
        /// <summary>
        /// ID of config action
        /// </summary>
       public virtual int ID { get; set; }
        /// <summary>
        /// CategoryId
        /// </summary>
       public virtual int CategoryID { get; set; }
        /// <summary>
        /// Category Name
        /// </summary>
       public virtual string CategoryName { get; set; }
        /// <summary>
        /// Key of this config action
        /// </summary>
       public virtual string Item { get; set; }
        /// <summary>
        /// Name
        /// </summary>
       public virtual string Name { get; set; }

        /// <summary>
        /// Value of this config action
        /// </summary>
       public virtual string Value { get; set; }
        /// <summary>
        /// Description
        /// </summary>
       public virtual string Description { get; set; }
        /// <summary>
        /// StatusId, 0 mean active, 1. inactive, actually it is not used.
        /// </summary>
       public virtual int StatusID { get; set; }
       public virtual DateTime? StartDate { get; set; }
       public virtual DateTime? EndDate { get; set; }
       public virtual DateTime? CreateDate { get; set; }
       public virtual string BAK1 { get; set; }
       public virtual string BAK2 { get; set; }
       public virtual string BAK3 { get; set; }
       public virtual string BAK4 { get; set; }
       public virtual string BAK5 { get; set; }
       public virtual DealerInfo DealerInfo { get; set; }

      public virtual MVNOConfigActionInfo Clone()
      {
          return this.MemberwiseClone() as MVNOConfigActionInfo;
      }
   }
}
