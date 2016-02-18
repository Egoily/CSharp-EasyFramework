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
    public class MNPPortabilityCustomerInfo
   {
      #region 构造函数
      public MNPPortabilityCustomerInfo()
      {}

      public MNPPortabilityCustomerInfo(int CUSTOMERID, string REFERENCECODE, DateTime CREATEDATE)
      {
         this._CUSTOMERID=CUSTOMERID;
         this._REFERENCECODE=REFERENCECODE;
         this._CREATEDATE=CREATEDATE;
      }
      #endregion

      #region 成员
      private int? _CUSTOMERID;
      private string _REFERENCECODE;
      private DateTime? _CREATEDATE;
      #endregion


      #region 属性
      public virtual int? CustomerID
      {
         get {  return _CUSTOMERID; }
         set {  _CUSTOMERID = value; }
      }

      public  virtual string ReferenceCode
      {
         get {  return _REFERENCECODE; }
         set {  _REFERENCECODE = value; }
      }

      public virtual DateTime? CreateDate
      {
         get {  return _CREATEDATE; }
         set {  _CREATEDATE = value; }
      }

      #endregion

   }
}
