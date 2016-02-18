using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2010-10-18 15:25:09
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2010-10-18 15:25:09
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2010-10-18 15:25:09
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2010-10-18 15:25:09
   /// </summary>
    [DataContract]
    [Serializable]
    public class VPNSubscriberInfo
   {
      #region 构造函数
      public VPNSubscriberInfo()
      {}
      public virtual VPNSubscriberInfo Clone()
      {

          return (VPNSubscriberInfo)this.MemberwiseClone();

      } 
      public VPNSubscriberInfo(int SUBSCRIBERID, int GROUPID, int RESOURCEID, int RESOURCETYPEID, int SHORTCODEID, int STATUSID, DateTime ENDDATE, DateTime STARTDATE, DateTime CREATEDATE, string REMARK)
      {
         this._SUBSCRIBERID=SUBSCRIBERID;
         this._GROUPID=GROUPID;
         this._RESOURCEID=RESOURCEID;
         this._RESOURCETYPEID=RESOURCETYPEID;
         this._SHORTCODEID=SHORTCODEID;
         this._STATUSID=STATUSID;
         this._ENDDATE=ENDDATE;
         this._STARTDATE=STARTDATE;
         this._CREATEDATE=CREATEDATE;
         this._REMARK=REMARK;
      }
      #endregion

      #region 成员
      private int _SUBSCRIBERID;
      private int _GROUPID;
      private int _RESOURCEID;
      private int _RESOURCETYPEID;
      private int _SHORTCODEID;
      private int _STATUSID;
      private DateTime? _ENDDATE;
      private DateTime _STARTDATE;
      private DateTime _CREATEDATE;
      private string _REMARK;
      #endregion


      #region 属性
      public  virtual int SUBSCRIBERID
      {
         get {  return _SUBSCRIBERID; }
         set {  _SUBSCRIBERID = value; }
      }

      public  virtual int GROUPID
      {
         get {  return _GROUPID; }
         set {  _GROUPID = value; }
      }

      public  virtual int RESOURCEID
      {
         get {  return _RESOURCEID; }
         set {  _RESOURCEID = value; }
      }

      public  virtual int RESOURCETYPEID
      {
         get {  return _RESOURCETYPEID; }
         set {  _RESOURCETYPEID = value; }
      }

      public  virtual int SHORTCODEID
      {
         get {  return _SHORTCODEID; }
         set {  _SHORTCODEID = value; }
      }

      public  virtual int STATUSID
      {
         get {  return _STATUSID; }
         set {  _STATUSID = value; }
      }

      public  virtual DateTime? ENDDATE
      {
         get {  return _ENDDATE; }
         set {  _ENDDATE = value; }
      }

      public  virtual DateTime STARTDATE
      {
         get {  return _STARTDATE; }
         set {  _STARTDATE = value; }
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
