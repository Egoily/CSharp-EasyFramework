using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2010/10/15 11:02:02
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2010/10/15 11:02:02
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2010/10/15 11:02:02
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2010/10/15 11:02:02
   /// </summary>

   [DataContract]
    [Serializable]
    public class RelateInfo
   {
      #region 构造函数
      public RelateInfo()
      {}

      public RelateInfo(int ID, int CARRIERID, int DEALERID, int CUSTOMERID, string TELEPHONENUMBER, string TARIFFCODE, DateTime? STARTPERIOD, DateTime? ENDPERIOD, int STATUSID)
      {
         this._ID=ID;
         this._CARRIERID=CARRIERID;
         this._DEALERID=DEALERID;
         this._CUSTOMERID=CUSTOMERID;
         this._TELEPHONENUMBER=TELEPHONENUMBER;
         this._TARIFFCODE=TARIFFCODE;
         this._STARTPERIOD=STARTPERIOD;
         this._ENDPERIOD=ENDPERIOD;
         this._STATUSID=STATUSID;
      }
      #endregion

      #region 成员
      private int _ID;
      private int _CARRIERID;
      private int _DEALERID;
      private int _CUSTOMERID;
      private string _TELEPHONENUMBER;
      private string _TARIFFCODE;
      private DateTime? _STARTPERIOD;
      private DateTime? _ENDPERIOD;
      private int _STATUSID;
      #endregion


      #region 属性
      public  int Id
      {
         get {  return _ID; }
         set {  _ID = value; }
      }

      public  int CarrierId
      {
         get {  return _CARRIERID; }
         set {  _CARRIERID = value; }
      }

      public  int DealerId
      {
         get {  return _DEALERID; }
         set {  _DEALERID = value; }
      }

      public  int CustomerId
      {
         get {  return _CUSTOMERID; }
         set {  _CUSTOMERID = value; }
      }

      public  string TelephoneNumber
      {
         get {  return _TELEPHONENUMBER; }
         set {  _TELEPHONENUMBER = value; }
      }

      public  string TariffCode
      {
         get {  return _TARIFFCODE; }
         set {  _TARIFFCODE = value; }
      }

      public  DateTime? StartPeriod
      {
         get {  return _STARTPERIOD; }
         set {  _STARTPERIOD = value; }
      }

      public  DateTime? EndPeriod
      {
         get {  return _ENDPERIOD; }
         set {  _ENDPERIOD = value; }
      }

      public  int StatusId
      {
         get {  return _STATUSID; }
         set {  _STATUSID = value; }
      }

      #region 用于在UI上的DataGriew显示
      private string _Status;
      public string Status
      {
          get { return _Status; }
          set { _Status = value; }
      }

      public string _CarrierIdStr;
      public string CarrierIdStr
      {
          get 
          {
              if (_CARRIERID == -1)
                  return "";
              return _CARRIERID.ToString(); 
          }
          set { CarrierIdStr = value; }
      }

      public string _DealerIdStr;
      public string DealerIdStr
      {
          get 
          {
              if (_DEALERID == -1)
                  return "";
              return _DEALERID.ToString(); 
          }
          set { _DealerIdStr = value; }
      }

      public string _CustomerIdStr;
      public string CustomerIdStr
      {
          get 
          {
              if (_CUSTOMERID == -1)
                  return "";
              return _CUSTOMERID.ToString(); 
          }
          set { _CustomerIdStr = value; }
      }
      #endregion

      #endregion

   }
}
