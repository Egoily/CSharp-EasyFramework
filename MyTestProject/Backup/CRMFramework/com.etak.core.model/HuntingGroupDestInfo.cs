using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2010-11-2 17:11:05
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2010-11-2 17:11:05
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2010-11-2 17:11:05
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2010-11-2 17:11:05
   /// </summary>
   [DataContract]
    [Serializable]
    public class HuntingGroupDestInfo
   {
      #region 构造函数
      public HuntingGroupDestInfo()
      {}
      public virtual HuntingGroupDestInfo Clone()
      {

          return (HuntingGroupDestInfo)this.MemberwiseClone();

      } 
      public HuntingGroupDestInfo(int DESTINATIONID, int GROUPID, string DESTINATION, int PRIORITY, DateTime CREATEDATE, string REMARK)
      {
         this._DESTINATIONID=DESTINATIONID;
         this._GROUPID=GROUPID;
         this._DESTINATION=DESTINATION;
         this._PRIORITY=PRIORITY;
         this._CREATEDATE=CREATEDATE;
         this._REMARK=REMARK;
      }
      #endregion

      #region 成员
      private int _DESTINATIONID;
      private int _GROUPID;
      private string _DESTINATION;
      private int _PRIORITY;
      private DateTime _CREATEDATE;
      private string _REMARK;
      #endregion


      #region 属性
      public  virtual int DESTINATIONID
      {
         get {  return _DESTINATIONID; }
         set {  _DESTINATIONID = value; }
      }

      public  virtual int GROUPID
      {
         get {  return _GROUPID; }
         set {  _GROUPID = value; }
      }

      public  virtual string DESTINATION
      {
         get {  return _DESTINATION; }
         set {  _DESTINATION = value; }
      }

      public  virtual int PRIORITY
      {
         get {  return _PRIORITY; }
         set {  _PRIORITY = value; }
      }

      public  virtual DateTime CREATEDATE
      {
         get {  return _CREATEDATE; }
         set {  _CREATEDATE = value; }
      }

      public  virtual string REMARK
      {
         get {  return _REMARK; }
         set {  _REMARK = value; }
      }

      #endregion

   }
}
