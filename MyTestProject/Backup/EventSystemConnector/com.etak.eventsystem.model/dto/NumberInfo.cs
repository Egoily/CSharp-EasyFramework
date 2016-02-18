using System;
using System.Runtime.Serialization;

namespace com.etak.eventsystem.model.dto
{

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class NumberInfo : LoadeableEntity
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
      #endregion


      #region 属性
      [DataMember()]
      public string Resource
      {
          get { return _Resource; }
          set { _Resource = value; }
      }

      [DataMember()]
      public int? TrafficTypeID
      {
          get { return _TrafficTypeID; }
          set { _TrafficTypeID = value; }
      }

      [DataMember()]
      public int? CategoryID
      {
          get { return _CategoryID; }
          set { _CategoryID = value; }
      }

      [DataMember()]
      public int? UpdateUserID
      {
          get { return _UpdateUserID; }
          set { _UpdateUserID = value; }
      }

      [DataMember()]
      public DateTime? UpdateDate
      {
          get { return _UpdateDate; }
          set { _UpdateDate = value; }
      }

      [DataMember()]
      public int? CreateUserID
      {
          get { return _CreateUserID; }
          set { _CreateUserID = value; }
      }

      [DataMember()]
      public DateTime? CreateDate
      {
          get { return _CreateDate; }
          set { _CreateDate = value; }
      }

      [DataMember()]
      public int? DataStatus
      {
          get { return _DataStatus; }
          set { _DataStatus = value; }
      }

      #endregion


   }
}
