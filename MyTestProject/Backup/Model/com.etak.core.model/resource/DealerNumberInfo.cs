using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2009-7-9 11:10:19
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2009-7-9 11:10:19
   ///
   ///Function Description :    
   ///Developer                : Darren   
   ///Builded Date:    2009-7-9 11:10:19
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2009-7-9 11:10:19
   /// </summary>
    [DataContract]
    [Serializable]
    public class DealerNumberInfo
   {
      #region 成员
      private int? _ID;
      private NumberInfo _Resource;
      private int? _DealerID;
      private int? _ShareType;
      #endregion


      #region 属性
        /// <summary>
        /// Unique ID
        /// </summary>
      public virtual int? ID
      {
         get {  return _ID; }
         set {  _ID = value; }
      }
    /// <summary>
    /// MSISDN
    /// </summary>
      public virtual NumberInfo Resource
      {
         get {  return _Resource; }
         set {  _Resource = value; }
      }

        /// <summary>
        /// Dealer Id of this Number
        /// </summary>
      public virtual int? DealerID
      {
         get {  return _DealerID; }
         set {  _DealerID = value; }
      }
    /// <summary>
    /// Share Type, 1 means it's able to be shared to other dealer under the same vmo
    /// </summary>
      public virtual int? ShareType
      {
         get {  return _ShareType; }
         set {  _ShareType = value; }
      }

      #endregion

   }
}
