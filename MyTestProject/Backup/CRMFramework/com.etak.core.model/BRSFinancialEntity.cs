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
    public class BRSFinancialEntity
   {
      #region 构造函数
      public BRSFinancialEntity()
      {}

      public BRSFinancialEntity(int ENTITYCODE, string ENTITYNAME, string DESCRIPTION)
      {
         this._ENTITYCODE=ENTITYCODE;
         this._ENTITYNAME=ENTITYNAME;
         this._DESCRIPTION=DESCRIPTION;
      }
      #endregion

      #region 成员
      private int _ENTITYCODE;
      private string _ENTITYNAME;
      private string _DESCRIPTION;
      #endregion


      #region 属性
      public  virtual int EntityCode
      {
         get {  return _ENTITYCODE; }
         set {  _ENTITYCODE = value; }
      }

      public  virtual string EntityName
      {
         get {  return _ENTITYNAME; }
         set {  _ENTITYNAME = value; }
      }

      public  virtual string Description
      {
         get {  return _DESCRIPTION; }
         set {  _DESCRIPTION = value; }
      }

      #endregion

   }
}
