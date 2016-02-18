using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2010-11-2 17:11:05
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2010-11-2 17:11:05
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2010-11-2 17:11:05
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2010-11-2 17:11:05
   /// </summary>
    [DataContract]
    [Serializable]
    public class HuntingGroupSettingInfo
   {
      #region 构造函数
      public HuntingGroupSettingInfo()
      {}
      public virtual HuntingGroupSettingInfo Clone()
      {

          return (HuntingGroupSettingInfo)this.MemberwiseClone();

      } 
      public HuntingGroupSettingInfo(int SETTINGID, int DEALERID, int NOREPLYTIMER1, int NOREPLYTIMER2, int MAXJUMPS, DateTime CREATEDATE, string REMARK)
      {
         this._SETTINGID=SETTINGID;
         this._DEALERID=DEALERID;
         this._NOREPLYTIMER1=NOREPLYTIMER1;
         this._NOREPLYTIMER2=NOREPLYTIMER2;
         this._MAXJUMPS=MAXJUMPS;
         this._CREATEDATE=CREATEDATE;
         this._REMARK=REMARK;
      }
      #endregion

      #region 成员
      private int _SETTINGID;
      private int _DEALERID;
      private int _NOREPLYTIMER1;
      private int _NOREPLYTIMER2;
      private int _MAXJUMPS;
      private DateTime _CREATEDATE;
      private string _REMARK;
      #endregion


      #region 属性
      public  virtual int SETTINGID
      {
         get {  return _SETTINGID; }
         set {  _SETTINGID = value; }
      }

      public  virtual int DEALERID
      {
         get {  return _DEALERID; }
         set {  _DEALERID = value; }
      }

      public  virtual int NOREPLYTIMER1
      {
         get {  return _NOREPLYTIMER1; }
         set {  _NOREPLYTIMER1 = value; }
      }

      public  virtual int NOREPLYTIMER2
      {
         get {  return _NOREPLYTIMER2; }
         set {  _NOREPLYTIMER2 = value; }
      }

      public  virtual int MAXJUMPS
      {
         get {  return _MAXJUMPS; }
         set {  _MAXJUMPS = value; }
      }

      public  virtual DateTime CREATEDATE
      {
         get {  return _CREATEDATE; }
         set {  _CREATEDATE = value; }
      }

      public  virtual string REMARK
      {
         get {  return _REMARK; }
         set {  _REMARK = value; }
      }

      #endregion

   }
}
