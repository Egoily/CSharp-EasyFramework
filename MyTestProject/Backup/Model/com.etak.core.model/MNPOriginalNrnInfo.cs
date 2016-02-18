using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2009/9/15 AM 10:54:20
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2009/9/15 AM 10:54:20
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2009/9/15 AM 10:54:20
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2009/9/15 AM 10:54:20
   /// </summary>
    [DataContract]
    [Serializable]
    public class MNPOriginalNrnInfo
   {
      #region 构造函数
      public MNPOriginalNrnInfo()
      {}

      public MNPOriginalNrnInfo(string RANGE1, string RANGE2, string OPERATORCODE, string NRN, string OPERATORNAME, DateTime STARTDATE, DateTime ENDDATE, DateTime CREATEDATE)
      {
         this._RANGE1=RANGE1;
         this._RANGE2=RANGE2;
         this._OPERATORCODE=OPERATORCODE;
         this._NRN=NRN;
         this._OPERATORNAME=OPERATORNAME;
         this._STARTDATE=STARTDATE;
         this._ENDDATE=ENDDATE;
         this._CREATEDATE=CREATEDATE;
      }
      #endregion

      #region 成员

      private long _SeqID;
      private string _RANGE1;
      private string _RANGE2;
      private string _OPERATORCODE;
      private string _NRN;
      private string _OPERATORNAME;
      private DateTime? _STARTDATE;
      private DateTime? _ENDDATE;
      private DateTime? _CREATEDATE;
      #endregion


      #region 属性

      public virtual long SeqID
      {
          get { return _SeqID; }
          set { _SeqID = value; }
      }
      public virtual string Range1
      {
         get {  return _RANGE1; }
         set {  _RANGE1 = value; }
      }

      public virtual string Range2
      {
         get {  return _RANGE2; }
         set {  _RANGE2 = value; }
      }

      public virtual string OperatorCode
      {
         get {  return _OPERATORCODE; }
         set {  _OPERATORCODE = value; }
      }

      public  virtual string NRN
      {
         get {  return _NRN; }
         set {  _NRN = value; }
      }

      public virtual string OperatorName
      {
         get {  return _OPERATORNAME; }
         set {  _OPERATORNAME = value; }
      }

      public virtual DateTime? StartDate
      {
         get {  return _STARTDATE; }
         set {  _STARTDATE = value; }
      }

      public virtual DateTime? EndDate
      {
         get {  return _ENDDATE; }
         set {  _ENDDATE = value; }
      }

      public virtual DateTime? CreateDate
      {
         get {  return _CREATEDATE; }
         set {  _CREATEDATE = value; }
      }

      #endregion

   }
}
