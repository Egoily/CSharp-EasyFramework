using System;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2009-11-9 18:28:43
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2009-11-9 18:28:43
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2009-11-9 18:28:43
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2009-11-9 18:28:43
   /// </summary>
    [Serializable]
    public class RmFixNumberRangeInfo
   {
      #region 构造函数
      public RmFixNumberRangeInfo()
      {}

      public RmFixNumberRangeInfo(int DEALERID,string RANGE1,string RANGE2)
      {
         this._DEALERID=DEALERID;
         this._RANGE1=RANGE1;
         this._RANGE2=RANGE2;
      }
      #endregion

      #region 成员
      private int _DEALERID;
      private string _RANGE1;
      private string _RANGE2;
      #endregion


      #region 属性
      public  int DealerId
      {
         get {  return _DEALERID; }
         set {  _DEALERID = value; }
      }

      public  string Range1
      {
         get {  return _RANGE1; }
         set {  _RANGE1 = value; }
      }

      public  string Range2
      {
         get {  return _RANGE2; }
         set {  _RANGE2 = value; }
      }

      #endregion

   }
}
