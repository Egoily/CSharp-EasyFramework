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
    public class CrmMvnoTopupBonusPromotionInfo
   {
      #region 构造函数
      public CrmMvnoTopupBonusPromotionInfo()
      {}

      public CrmMvnoTopupBonusPromotionInfo(int BONUSPROMOTIONID, int BONUSID, int PACKAGEID, int PROMOTIONID, decimal MINTOPUPAMOUNT, decimal MAXTOPUPAMOUNT, bool ENABLED, bool RECURRENT, int RECURRENCENUMBER, bool AMOUNT1INCLUDED, bool AMOUNT2INCLUDED)
      {
         this.BONUSPROMOTIONID=BONUSPROMOTIONID;
         this.BONUSID=BONUSID;
         this.PACKAGEID=PACKAGEID;
         this.PROMOTIONID=PROMOTIONID;
         this.MINTOPUPAMOUNT=MINTOPUPAMOUNT;
         this.MAXTOPUPAMOUNT = MAXTOPUPAMOUNT;
         this.ENABLED=ENABLED;
         this.RECURRENT=RECURRENT;
         this.RECURRENCENUMBER=RECURRENCENUMBER;
         this.AMOUNT1INCLUDED = AMOUNT1INCLUDED;
         this.AMOUNT2INCLUDED = AMOUNT2INCLUDED;
      }
      #endregion

      #region 成员
      private int BONUSPROMOTIONID;
      private int BONUSID;
      private int PACKAGEID;
      private int PROMOTIONID;
      private decimal MINTOPUPAMOUNT;
      private decimal MAXTOPUPAMOUNT;
      private bool ENABLED;
      private bool RECURRENT;
      private int RECURRENCENUMBER;
      private bool AMOUNT1INCLUDED;
      private bool AMOUNT2INCLUDED;
      private string PROMOTIONNAME;
      private decimal PROMOTIONLIMIT;

      #endregion


      #region 属性
      public  int BonusPromotionId
      {
         get {  return BONUSPROMOTIONID; }
         set {  BONUSPROMOTIONID = value; }
      }

      public  int BonusId
      {
         get {  return BONUSID; }
         set {  BONUSID = value; }
      }

      public  int PackageId
      {
         get {  return PACKAGEID; }
         set {  PACKAGEID = value; }
      }

      public  int PromotionId
      {
         get {  return PROMOTIONID; }
         set {  PROMOTIONID = value; }
      }

      public  decimal MinTopupAmount
      {
         get {  return MINTOPUPAMOUNT; }
         set {  MINTOPUPAMOUNT = value; }
      }

      public decimal MaxTopupAmount
      {
          get { return MAXTOPUPAMOUNT; }
          set { MAXTOPUPAMOUNT = value; }
      }

      public  bool Enabled
      {
         get {  return ENABLED; }
         set {  ENABLED = value; }
      }

      public  bool Recurrent
      {
         get {  return RECURRENT; }
         set {  RECURRENT = value; }
      }

      public  int RecurrenceNumber
      {
         get {  return RECURRENCENUMBER; }
         set {  RECURRENCENUMBER = value; }
      }
      public bool Amount1Included
      {
          get { return AMOUNT1INCLUDED; }
          set { AMOUNT1INCLUDED = value; }
      }
      public bool Amount2Included
      {
          get { return AMOUNT2INCLUDED; }
          set { AMOUNT2INCLUDED = value; }
      }

      public string PromotionName
      {
          get { return PROMOTIONNAME; }
          set { PROMOTIONNAME = value; }
      }

      public decimal PromotionLimit
      {
          get { return PROMOTIONLIMIT; }
          set { PROMOTIONLIMIT = value; }
      }
      #endregion

   }
}
