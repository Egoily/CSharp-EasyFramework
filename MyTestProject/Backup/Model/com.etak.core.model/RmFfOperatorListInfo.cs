using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2009-9-16 16:54:48
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2009-9-16 16:54:48
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2009-9-16 16:54:48
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2009-9-16 16:54:48
   /// </summary>
    [DataContract]
    [Serializable]
    public class RmFfOperatorListInfo : PromotionModelBase
   {
      #region 构造函数
      public RmFfOperatorListInfo()
      {}

      public RmFfOperatorListInfo(int DEALERID,string OPERATORCODE)
      {
         this._DEALERID=DEALERID;
         this._OPERATORCODE=OPERATORCODE;
      }
      #endregion

      #region 成员
      private int _DEALERID;
      private string _OPERATORCODE;
      #endregion


      #region 属性
      public  int DealerId
      {
         get {  return _DEALERID; }
         set {  _DEALERID = value; }
      }

      public  string OperatorCode
      {
         get {  return _OPERATORCODE; }
         set {  _OPERATORCODE = value; }
      }

      #endregion

   }
}
