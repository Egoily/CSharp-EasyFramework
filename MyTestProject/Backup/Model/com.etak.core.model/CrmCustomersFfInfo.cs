using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2009-9-16 17:53:03
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2009-9-16 17:53:03
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2009-9-16 17:53:03
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2009-9-16 17:53:03
   /// </summary>
    [DataContract]
    [Serializable]
    public class CrmCustomersFfInfo : PromotionModelBase
   {
      #region 构造函数
      public CrmCustomersFfInfo()
      {}

      public CrmCustomersFfInfo(int CUSTOMERID,int FFID,string RESOURCE,DateTime? STARTDATE,DateTime? ENDDATE,DateTime? CREATEDATE,int USERID)
      {
         this._CUSTOMERID=CUSTOMERID;
         this._FFID=FFID;
         this._RESOURCE=RESOURCE;
         this._STARTDATE=STARTDATE;
         this._ENDDATE=ENDDATE;
         this._CREATEDATE=CREATEDATE;
         this._USERID=USERID;
      }
      #endregion

      #region 成员
      private int _CUSTOMERID;
      private int _FFID;
      private string _RESOURCE;
      private DateTime? _STARTDATE=null;
      private DateTime? _ENDDATE = null;
      private DateTime? _CREATEDATE = null;
      private int _USERID;
      #endregion


      #region 属性
      public  int CustomerId
      {
         get {  return _CUSTOMERID; }
         set {  _CUSTOMERID = value; }
      }

      private int? _CustomerId2;
      public int? CustomerId2
      {
          get { return _CustomerId2; }
          set { _CustomerId2 = value; }
      }

      private string _MSISDN2;
      public string MSISDN2
      {
          get { return _MSISDN2; }
          set { _MSISDN2 = value; }
      }

      private int _PromotionPlanID;
      public int PromotionPlanID
      {
          get { return _PromotionPlanID; }
          set { _PromotionPlanID = value; }
      }

      public  int FFId
      {
         get {  return _FFID; }
         set {  _FFID = value; }
      }

      public  string Resource
      {
         get {  return _RESOURCE; }
         set {  _RESOURCE = value; }
      }

      public  DateTime? StartDate
      {
         get {  return _STARTDATE; }
         set {  _STARTDATE = value; }
      }

      public  DateTime? EndDate
      {
         get {  return _ENDDATE; }
         set {  _ENDDATE = value; }
      }

      public  DateTime? CreateDate
      {
         get {  return _CREATEDATE; }
         set {  _CREATEDATE = value; }
      }

      public  int UserId
      {
         get {  return _USERID; }
         set {  _USERID = value; }
      }

      #endregion

   }
}
