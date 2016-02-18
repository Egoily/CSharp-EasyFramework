using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2009/8/20 PM 3:07:34
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2009/8/20 PM 3:07:34
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2009/8/20 PM 3:07:34
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2009/8/20 PM 3:07:34
   /// </summary>
    [DataContract]
    [Serializable]
    public class BRSCurrency
   {
      #region 构造函数
      public BRSCurrency()
      {}

      public BRSCurrency(int CURRENCYID, int CODENUMBER, string CODESTRING, string DESCRIPTION)
      {
         this._CURRENCYID=CURRENCYID;
         this._CODENUMBER=CODENUMBER;
         this._CODESTRING=CODESTRING;
         this._DESCRIPTION=DESCRIPTION;
      }
      #endregion

      #region 成员
      private int _CURRENCYID;
      private int _CODENUMBER;
      private string _CODESTRING;
      private string _DESCRIPTION;
      #endregion


      #region 属性
      public  virtual int CurrencyID
      {
         get {  return _CURRENCYID; }
         set {  _CURRENCYID = value; }
      }

      public  virtual int CodeNumber
      {
         get {  return _CODENUMBER; }
         set {  _CODENUMBER = value; }
      }

      public  virtual string CodeString
      {
         get {  return _CODESTRING; }
         set {  _CODESTRING = value; }
      }

      public  virtual string Description
      {
         get {  return _DESCRIPTION; }
         set {  _DESCRIPTION = value; }
      }

      #endregion

   }
}
