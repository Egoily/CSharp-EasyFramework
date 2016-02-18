using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class NumberHistoryInfo
   {
      #region 构造函数
      public NumberHistoryInfo()
      {}

	  public NumberHistoryInfo(string Resource, int? TrafficTypeID, int? CategoryID, int? UpdateUserID, DateTime? UpdateDate, int? CreateUserID, DateTime? CreateDate, int? dataStatus, DateTime? DeleteDate)
      {
          this._Resource = Resource;
          this._TrafficTypeID = TrafficTypeID;
          this._CategoryID = CategoryID; 
          this._UpdateUserID = UpdateUserID;
          this._UpdateDate = UpdateDate;
          this._CreateUserID = CreateUserID;
          this._CreateDate = CreateDate;
		  this._DataStatus = dataStatus;
		  this._DataStatus = dataStatus;
		  this._DeleteDate = DeleteDate;
	  }
      #endregion

      #region 成员
      private string _Resource;
      private int? _TrafficTypeID;
      private int? _CategoryID; 
      private int? _UpdateUserID;
      private DateTime? _UpdateDate;
      private int? _CreateUserID;
      private DateTime? _CreateDate;
      private int? _DataStatus;
	  private DateTime? _DeleteDate;
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

	  public virtual DateTime? DeleteDate
	  {
		  get { return _DeleteDate; }
		  set { _DeleteDate = value; }
	  }

	  #endregion


   }
}
