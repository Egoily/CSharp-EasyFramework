using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2010-7-7 11:24:49
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2010-7-7 11:24:49
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2010-7-7 11:24:49
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2010-7-7 11:24:49
   /// </summary>
   [DataContract]
    [Serializable]
    public class CrmCustomersTopupPromotionInfo
   {
      #region 构造函数
      public CrmCustomersTopupPromotionInfo()
      {}

      public CrmCustomersTopupPromotionInfo(int PROCESSID,int TOPUPPROMOTIONID,long TOPUPID,int DEALERID,int BONUSID,int BONUSPROMOTIONID,int CUSTOMERID,string RESOURCE,int PROMOTIONPLANID,bool ACTIVE,DateTime STARTDATE,DateTime? ENDDATE,DateTime APPLYDATE)
      {
         this.PROCESSID=PROCESSID;
         this.TOPUPPROMOTIONID=TOPUPPROMOTIONID;
         this.TOPUPID=TOPUPID;
         this.DEALERID=DEALERID;
         this.BONUSID=BONUSID;
         this.BONUSPROMOTIONID=BONUSPROMOTIONID;
         this.CUSTOMERID=CUSTOMERID;
         this.RESOURCE=RESOURCE;
         this.PROMOTIONPLANID=PROMOTIONPLANID;
         this.ACTIVE=ACTIVE;
         this.STARTDATE=STARTDATE;
         this.ENDDATE=ENDDATE;
         this.APPLYDATE=APPLYDATE;
      }
      #endregion

      #region 成员
      private int PROCESSID;
      private int TOPUPPROMOTIONID;
      private long TOPUPID;
      private int DEALERID;
      private int BONUSID;
      private int BONUSPROMOTIONID;
      private int CUSTOMERID;
      private string RESOURCE;
      private int PROMOTIONPLANID;
      private bool ACTIVE;
      private DateTime STARTDATE;
      private DateTime? ENDDATE;
      private DateTime APPLYDATE;
      #endregion


      #region 属性
      public  int ProcessId
      {
         get {  return PROCESSID; }
         set {  PROCESSID = value; }
      }

      public  int TopupPromotionId
      {
         get {  return TOPUPPROMOTIONID; }
         set {  TOPUPPROMOTIONID = value; }
      }

      public  long TopupId
      {
         get {  return TOPUPID; }
         set {  TOPUPID = value; }
      }

      public  int DealerId
      {
         get {  return DEALERID; }
         set {  DEALERID = value; }
      }

      public  int BonusId
      {
         get {  return BONUSID; }
         set {  BONUSID = value; }
      }

      public  int BonusPromotionId
      {
         get {  return BONUSPROMOTIONID; }
         set {  BONUSPROMOTIONID = value; }
      }

      public  int CustomerId
      {
         get {  return CUSTOMERID; }
         set {  CUSTOMERID = value; }
      }

      public  string Resource
      {
         get {  return RESOURCE; }
         set {  RESOURCE = value; }
      }

      public  int PromotionPlanId
      {
         get {  return PROMOTIONPLANID; }
         set {  PROMOTIONPLANID = value; }
      }

      public  bool Active
      {
         get {  return ACTIVE; }
         set {  ACTIVE = value; }
      }

      public  DateTime StartDate
      {
         get {  return STARTDATE; }
         set {  STARTDATE = value; }
      }

      public  DateTime? EndDate
      {
         get {  return ENDDATE; }
         set {  ENDDATE = value; }
      }

      public  DateTime ApplyDate
      {
         get {  return APPLYDATE; }
         set {  APPLYDATE = value; }
      }

      #endregion

   }
}
