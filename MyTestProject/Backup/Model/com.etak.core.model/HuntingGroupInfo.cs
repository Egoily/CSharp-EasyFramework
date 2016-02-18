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
    public class HuntingGroupInfo
   {
      #region 构造函数
      public HuntingGroupInfo()
      {}

      public virtual HuntingGroupInfo Clone()
      {

          return (HuntingGroupInfo)this.MemberwiseClone();

      } 

      public HuntingGroupInfo(int GROUPID, int RESOURCEID, int NOREPLYTIMER1, int NOREPLYTIMER2, int MAXJUMPS, DateTime CREATEDATE, string REMARK)
      {
         this._GROUPID=GROUPID;
         this._RESOURCEID=RESOURCEID;
         this._NOREPLYTIMER1=NOREPLYTIMER1;
         this._NOREPLYTIMER2=NOREPLYTIMER2;
         this._MAXJUMPS=MAXJUMPS;
         this._CREATEDATE=CREATEDATE;
         this._REMARK=REMARK;
      }
      #endregion

      #region 成员
      private int _GROUPID;
      private int _RESOURCEID;
      private int _NOREPLYTIMER1;
      private int _NOREPLYTIMER2;
      private int _MAXJUMPS;
      private DateTime _CREATEDATE;
      private string _REMARK;
      #endregion


      #region 属性
      public  virtual int GROUPID
      {
         get {  return _GROUPID; }
         set {  _GROUPID = value; }
      }

      public  virtual int RESOURCEID
      {
         get {  return _RESOURCEID; }
         set {  _RESOURCEID = value; }
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
