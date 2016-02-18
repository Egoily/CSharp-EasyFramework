using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2010-10-18 15:25:09
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2010-10-18 15:25:09
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2010-10-18 15:25:09
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2010-10-18 15:25:09
   /// </summary>
   [DataContract]
    [Serializable]
    public class VPNGroupInfo
   {
      #region 构造函数
      public VPNGroupInfo()
      {}
      public virtual VPNGroupInfo Clone()
      {

          return (VPNGroupInfo)this.MemberwiseClone();

      } 
      public VPNGroupInfo(int GROUPID, int VPNID, string GROUPCODE, string GROUPNAME, int NUMBERCATEGORYID, int STATUSID, DateTime CREATEDATE, string REMARK)
      {
         this._GROUPID=GROUPID;
         this._VPNID=VPNID;
         this._GROUPCODE=GROUPCODE;
         this._GROUPNAME=GROUPNAME;
         this._NUMBERCATEGORYID=NUMBERCATEGORYID;
         this._STATUSID=STATUSID;
         this._CREATEDATE=CREATEDATE;
         this._REMARK=REMARK;
      }
      #endregion

      #region 成员
      private int _GROUPID;
      private int _VPNID;
      private string _GROUPCODE;
      private string _GROUPNAME;
      private int _NUMBERCATEGORYID;
      private int _STATUSID;
      private DateTime _CREATEDATE;
      private string _REMARK;
      #endregion


      #region 属性
      public  virtual int GROUPID
      {
         get {  return _GROUPID; }
         set {  _GROUPID = value; }
      }

      public  virtual int VPNID
      {
         get {  return _VPNID; }
         set {  _VPNID = value; }
      }

      public  virtual string GROUPCODE
      {
         get {  return _GROUPCODE; }
         set {  _GROUPCODE = value; }
      }

      public  virtual string GROUPNAME
      {
         get {  return _GROUPNAME; }
         set {  _GROUPNAME = value; }
      }

      public  virtual int NUMBERCATEGORYID
      {
         get {  return _NUMBERCATEGORYID; }
         set {  _NUMBERCATEGORYID = value; }
      }

      public  virtual int STATUSID
      {
         get {  return _STATUSID; }
         set {  _STATUSID = value; }
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
