using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2010-7-8 11:56:50
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2010-7-8 11:56:50
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2010-7-8 11:56:50
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2010-7-8 11:56:50
   /// </summary>
   [DataContract]
    [Serializable]
    public class CrmCustomersTopupBonusInfo
   {
      #region 构造函数
      public CrmCustomersTopupBonusInfo()
      {}

      public CrmCustomersTopupBonusInfo(int TOPUPBONUSID, long TOPUPID, int DEALERID, int BONUSID, int CUSTOMERID, string RESOURCE, decimal BONUSAMOUNT, decimal BONUSAMOUNTWITHTAX, bool ACTIVE, DateTime APPLYDATE, long BONUSTOPUPID)
      {
         this.TOPUPBONUSID=TOPUPBONUSID;
         this.TOPUPID=TOPUPID;
         this.DEALERID=DEALERID;
         this.BONUSID=BONUSID;
         this.CUSTOMERID=CUSTOMERID;
         this.RESOURCE=RESOURCE;
         this.BONUSAMOUNT=BONUSAMOUNT;
         this.BONUSAMOUNTWITHTAX=BONUSAMOUNTWITHTAX;
         this.ACTIVE=ACTIVE;
         this.APPLYDATE=APPLYDATE;
         this.BONUSTOPUPID=BONUSTOPUPID;
      }
      #endregion

      #region 成员
      private int TOPUPBONUSID;
      private long TOPUPID;
      private int DEALERID;
      private int BONUSID;
      private int CUSTOMERID;
      private string RESOURCE;
      private decimal BONUSAMOUNT;
      private decimal BONUSAMOUNTWITHTAX;
      private bool ACTIVE;
      private DateTime APPLYDATE;
      private long BONUSTOPUPID;
      #endregion


      #region 属性
      public  int TopupBonusId
      {
         get {  return TOPUPBONUSID; }
         set {  TOPUPBONUSID = value; }
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

      public  decimal BonusAmount
      {
         get {  return BONUSAMOUNT; }
         set {  BONUSAMOUNT = value; }
      }

      public  decimal BonusAmountWithTax
      {
         get {  return BONUSAMOUNTWITHTAX; }
         set {  BONUSAMOUNTWITHTAX = value; }
      }

      public  bool Active
      {
         get {  return ACTIVE; }
         set {  ACTIVE = value; }
      }

      public  DateTime ApplyDate
      {
         get {  return APPLYDATE; }
         set {  APPLYDATE = value; }
      }

      public  long BonusTopupId
      {
         get {  return BONUSTOPUPID; }
         set {  BONUSTOPUPID = value; }
      }

      #endregion

   }
}
