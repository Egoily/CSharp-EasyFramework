using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2009-7-9 11:10:19
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2009-7-9 11:10:19
   ///
   ///Function Description :    
   ///Developer                :   Darren 
   ///Builded Date:    2009-7-9 11:10:19
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2009-7-11 14:22:19
   /// </summary>
    [DataContract]
    [Serializable]
   public class NumberInfo
   {
      #region 成员
      private string _Resource;
      private int? _TrafficTypeID;
      private int? _CategoryID;
      private int? _UpdateUserID;
      private DateTime? _UpdateDate;
      private int? _CreateUserID;
      private DateTime? _CreateDate;
      private int? _DataStatus;
       private NumberPropertyInfo _numberProperty;
       private IList<DealerNumberInfo> _numberDealerSharing;
      #endregion


      #region 属性

      public virtual string Resource
      {
          get { return _Resource; }
          set { _Resource = value; }
      }

      public virtual int? TrafficTypeID
      {
          get { return _TrafficTypeID; }
          set { _TrafficTypeID = value; }
      }
        /// <summary>
        /// Category Id:
        ///    Gold = 1,
        ///    Silver = 2,
        ///    Bronze = 3,
        ///    Normal = 4
        /// </summary>
      public virtual int? CategoryID
      {
          get { return _CategoryID; }
          set { _CategoryID = value; }
      }

      public virtual int? UpdateUserID
      {
          get { return _UpdateUserID; }
          set { _UpdateUserID = value; }
      }

      public virtual DateTime? UpdateDate
      {
          get { return _UpdateDate; }
          set { _UpdateDate = value; }
      }

      public virtual int? CreateUserID
      {
          get { return _CreateUserID; }
          set { _CreateUserID = value; }
      }

      public virtual DateTime? CreateDate
      {
          get { return _CreateDate; }
          set { _CreateDate = value; }
      }

      public virtual int? DataStatus
      {
          get { return _DataStatus; }
          set { _DataStatus = value; }
      }

       public virtual NumberPropertyInfo NumberProperty
       {
           get { return _numberProperty; }
           set { _numberProperty = value; }
       }

       public virtual IList<DealerNumberInfo> NumberDealerSharing
       {
           get { return _numberDealerSharing; }
           set { _numberDealerSharing = value; }
       }

       #endregion


   }
}
