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
    public class BRSTaxCode
   {
      #region 构造函数
      public BRSTaxCode()
      {}

      public BRSTaxCode(int TAXCODE, double TAXVALUE, string DESCRIPTION)
      {
         this._TAXCODE=TAXCODE;
         this._TAXVALUE=TAXVALUE;
         this._DESCRIPTION=DESCRIPTION;
      }
      #endregion

      #region 成员
      private int _TAXCODE;
      private double _TAXVALUE;
      private string _DESCRIPTION;
      #endregion


      #region 属性
      public  virtual int TaxCode
      {
         get {  return _TAXCODE; }
         set {  _TAXCODE = value; }
      }

      public  virtual double TaxValue
      {
         get {  return _TAXVALUE; }
         set {  _TAXVALUE = value; }
      }

      public  virtual string Description
      {
         get {  return _DESCRIPTION; }
         set {  _DESCRIPTION = value; }
      }
      //==modify by John 2012-09-24 start
      public virtual decimal GetDefualtTaxRate()
      {
          decimal taxValue = string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["DefaultTaxRate"]) ? 0 : Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["DefaultTaxRate"]);
          return taxValue;
      }
      public virtual float GetDefualtTaxRateF()
      {
          float taxValue = string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["DefaultTaxRate"]) ? 0 : Convert.ToSingle(System.Configuration.ConfigurationManager.AppSettings["DefaultTaxRate"]);
          return taxValue;
      }
      public virtual int GetDefualtTaxCode()
      {
          int taxCode = string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["DefaultTaxCode"]) ? 3 : Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultTaxCode"]);
          return taxCode;
      }
       //==modify by John 2012-09-24 end
      #endregion

   }
}
