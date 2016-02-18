using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2009/9/15 AM 10:54:21
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2009/9/15 AM 10:54:21
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2009/9/15 AM 10:54:21
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2009/9/15 AM 10:54:21
   /// </summary>
    [DataContract]
    [Serializable]
    public class MNPNpdbEsvfInfo
   {
      #region 构造函数
      public MNPNpdbEsvfInfo()
      {}

      public MNPNpdbEsvfInfo(string OPERATORCODE, string NRN, string MSISDN, DateTime EFFECTDATE, DateTime CREATEDATE)
      {
         this._OPERATORCODE=OPERATORCODE;
         this._NRN=NRN;
         this._MSISDN=MSISDN;
         this._EFFECTDATE=EFFECTDATE;
         this._CREATEDATE=CREATEDATE;
      }
      #endregion

      #region 成员

      private long _SeqID;
      private string _OPERATORCODE;
      private string _NRN;
      private string _MSISDN;
      private DateTime? _EFFECTDATE;
      private DateTime? _CREATEDATE;
      private string _OwnerOperator;

      public virtual string OwnerOperator
      {
          get { return _OwnerOperator; }
          set { _OwnerOperator = value; }
      }
      #endregion


      #region 属性
      public virtual long SeqID
      {
          get { return _SeqID; }
          set { _SeqID = value; }
      }
      public  virtual string OperatorCode
      {
         get {  return _OPERATORCODE; }
         set {  _OPERATORCODE = value; }
      }

      public  virtual string NRN
      {
         get {  return _NRN; }
         set {  _NRN = value; }
      }

      public  virtual string Msisdn
      {
         get {  return _MSISDN; }
         set {  _MSISDN = value; }
      }

      public virtual DateTime? EffectDate
      {
         get {  return _EFFECTDATE; }
         set {  _EFFECTDATE = value; }
      }

      public virtual DateTime? CreateDate
      {
         get {  return _CREATEDATE; }
         set {  _CREATEDATE = value; }
      }

      #endregion

   }
}
