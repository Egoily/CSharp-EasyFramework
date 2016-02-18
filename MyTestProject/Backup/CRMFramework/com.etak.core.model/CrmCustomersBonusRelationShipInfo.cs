using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2010-7-20 16:39:40
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2010-7-20 16:39:40
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2010-7-20 16:39:40
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2010-7-20 16:39:40
   /// </summary>
    [DataContract]
    [Serializable]
    public class CrmCustomersBonusRelationShipInfo
   {
      #region 构造函数
      public CrmCustomersBonusRelationShipInfo()
      {}

      public CrmCustomersBonusRelationShipInfo(int ID,int MVNOID,string Description,int RelationShipTypeID,int BeRelationShipedID,decimal BonusAmount,int BounsValidityType,int BonusValidityValue,DateTime? StartDate,DateTime? EndDate,DateTime? CreateDate,int CreateUserID,int Status,DateTime? UpdateDate,int UpdateUserID)
      {
         this._ID=ID;
         this._MVNOID=MVNOID;
         this._Description=Description;
         this._RelationShipTypeID=RelationShipTypeID;
         this._BeRelationShipedID=BeRelationShipedID;
         this._BonusAmount=BonusAmount;
         this._BounsValidityType=BounsValidityType;
         this._BonusValidityValie=BonusValidityValue;
         this._StartDate=StartDate;
         this._EndDate=EndDate;
         this._CreateDate=CreateDate;
         this._CreateUserID=CreateUserID;
         this._Status=Status;
         this._UpdateDate=UpdateDate;
         this._UpdateUserID=UpdateUserID;
      }
      #endregion

      #region 成员
      private int _ID;
      private int _MVNOID;
      private string _Description;
      private int _RelationShipTypeID;
      private int _BeRelationShipedID;
      private decimal _BonusAmount;
      private int _BounsValidityType;
      private int _BonusValidityValie;
      private DateTime? _StartDate;
      private DateTime? _EndDate;
      private DateTime? _CreateDate;
      private int _CreateUserID;
      private int _Status;
      private DateTime? _UpdateDate;
      private int _UpdateUserID;
      private int _BonusPromotionID;
      private string _PromotionName;
      private string _PackageName; 
      #endregion


      #region 属性
      virtual public  int ID
      {
         get {  return _ID; }
         set {  _ID = value; }
      }

      virtual public  int MVNOID
      {
         get {  return _MVNOID; }
         set {  _MVNOID = value; }
      }

      virtual public  string Description
      {
         get {  return _Description; }
         set {  _Description = value; }
      }

      virtual public  int RelationShipTypeID
      {
         get {  return _RelationShipTypeID; }
         set {  _RelationShipTypeID = value; }
      }

      virtual public  int BeRelationShipedID
      {
         get {  return _BeRelationShipedID; }
         set {  _BeRelationShipedID = value; }
      }

      virtual public  decimal BonusAmount
      {
         get {  return _BonusAmount; }
         set {  _BonusAmount = value; }
      }

      virtual public  int BounsValidityType
      {
         get {  return _BounsValidityType; }
         set {  _BounsValidityType = value; }
      }

      virtual public  int BonusValidityValue
      {
         get {  return _BonusValidityValie; }
         set {  _BonusValidityValie = value; }
      }

      virtual public int BonusPromotionID
      {
          get { return _BonusPromotionID; }
          set { _BonusPromotionID = value; }
      }
        
      virtual public  DateTime? StartDate
      {
         get {  return _StartDate; }
         set {  _StartDate = value; }
      }

      virtual public  DateTime? EndDate
      {
         get {  return _EndDate; }
         set {  _EndDate = value; }
      }

      virtual public  DateTime? CreateDate
      {
         get {  return _CreateDate; }
         set {  _CreateDate = value; }
      }

      /// <summary>
      /// Id of the user that created the entity
      /// </summary>
      virtual public  int CreateUserID
      {
         get {  return _CreateUserID; }
         set {  _CreateUserID = value; }
      }

      virtual public  int Status
      {
         get {  return _Status; }
         set {  _Status = value; }
      }

      virtual public  DateTime? UpdateDate
      {
         get {  return _UpdateDate; }
         set {  _UpdateDate = value; }
      }

      /// <summary>
      /// Id of the user that updated the entity
      /// </summary>
      virtual public  int UpdateUserID
      {
         get {  return _UpdateUserID; }
         set {  _UpdateUserID = value; }
      }

      virtual public string PromotionName
      {
          get { return _PromotionName; }
          set { _PromotionName = value; }
      }

      virtual public string PackageName
      {
          get { return _PackageName; }
          set { _PackageName = value; }
      }

      #endregion

   }
}
