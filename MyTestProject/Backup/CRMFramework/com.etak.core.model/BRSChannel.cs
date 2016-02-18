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
    public class BRSChannel
   {
      #region 构造函数
      public BRSChannel()
      {}

      public BRSChannel(int CHANNELCODE, string CHANNELNAME, string DESCRIPTION)
      {
         this._CHANNELCODE=CHANNELCODE;
         this._CHANNELNAME=CHANNELNAME;
         this._DESCRIPTION=DESCRIPTION;
      }
      #endregion

      #region 成员
      private int _CHANNELCODE;
      private string _CHANNELNAME;
      private string _DESCRIPTION;
      #endregion


      #region 属性
      public  virtual int ChannelCode
      {
         get {  return _CHANNELCODE; }
         set {  _CHANNELCODE = value; }
      }

      public  virtual string ChannelName
      {
         get {  return _CHANNELNAME; }
         set {  _CHANNELNAME = value; }
      }

      public  virtual string Description
      {
         get {  return _DESCRIPTION; }
         set {  _DESCRIPTION = value; }
      }

      #endregion

   }
}
