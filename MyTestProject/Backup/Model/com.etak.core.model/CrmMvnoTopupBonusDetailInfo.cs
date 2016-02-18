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
    public class CrmMvnoTopupBonusDetailInfo
   {
      #region 构造函数
      public CrmMvnoTopupBonusDetailInfo()
      {}

      public CrmMvnoTopupBonusDetailInfo(int BONUSDETAILID,int BONUSID,decimal AMOUNT1,bool AMOUNT1INCLUDED,decimal AMOUNT2,bool AMOUNT2INCLUDED,bool FIXED,decimal BONUSAMOUNT,bool BONUSDETAILENABLED,int PackageID,int BonusAccount)
      {
         this.BONUSDETAILID=BONUSDETAILID;
         this.BONUSID=BONUSID;
         this.AMOUNT1=AMOUNT1;
         this.AMOUNT1INCLUDED=AMOUNT1INCLUDED;
         this.AMOUNT2=AMOUNT2;
         this.AMOUNT2INCLUDED=AMOUNT2INCLUDED;
         this.FIXED=FIXED;
         this.BONUSAMOUNT=BONUSAMOUNT;
         this.BONUSDETAILENABLED = BONUSDETAILENABLED;
         this.PACKAGEID = PackageID;
         this.BONUSACCOUNT = BonusAccount;

      }
      #endregion

      #region 成员
      private int BONUSDETAILID;
      private int BONUSID;
      private decimal AMOUNT1;
      private bool AMOUNT1INCLUDED;
      private bool BONUSDETAILENABLED;
      private decimal AMOUNT2;
      private bool AMOUNT2INCLUDED;
      private bool FIXED;
      private decimal BONUSAMOUNT;
      private int PACKAGEID;
      private int BONUSACCOUNT;
      #endregion


      #region 属性
      public  int BonusDetailid
      {
         get {  return BONUSDETAILID; }
         set {  BONUSDETAILID = value; }
      }

      public  int BonusId
      {
         get {  return BONUSID; }
         set {  BONUSID = value; }
      }

      public  decimal Amount1
      {
         get {  return AMOUNT1; }
         set {  AMOUNT1 = value; }
      }

      public  bool Amount1Included
      {
         get {  return AMOUNT1INCLUDED; }
         set {  AMOUNT1INCLUDED = value; }
      }

      public  decimal Amount2
      {
         get {  return AMOUNT2; }
         set {  AMOUNT2 = value; }
      }

      public  bool Amount2Included
      {
         get {  return AMOUNT2INCLUDED; }
         set {  AMOUNT2INCLUDED = value; }
      }

      public bool Fixed
      {
          get { return FIXED; }
          set { FIXED = value; }
      }

      public  bool BonusDetailEnabled
      {
          get { return BONUSDETAILENABLED; }
          set { BONUSDETAILENABLED = value; }
      }

      public  decimal BonusAmount
      {
         get {  return BONUSAMOUNT; }
         set {  BONUSAMOUNT = value; }
      }

      public int PackageId
      {
          get { return PACKAGEID; }
          set { PACKAGEID = value; }
      }

      public int BonusAccount
      {
          get { return BONUSACCOUNT; }
          set { BONUSACCOUNT = value; }
      }
      #endregion

      

   }
}
