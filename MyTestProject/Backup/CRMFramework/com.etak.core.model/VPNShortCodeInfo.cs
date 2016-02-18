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
    public class VPNShortCodeInfo
   {
      #region 构造函数
      public VPNShortCodeInfo()
      {}
      public virtual VPNShortCodeInfo Clone()
      {

          return (VPNShortCodeInfo)this.MemberwiseClone();

      } 
      public VPNShortCodeInfo(int SHORTCODEID, int GROUPID, string SHORTCODE, int STATUSID, DateTime CREATEDATE, string REMARK)
      {
         this._SHORTCODEID=SHORTCODEID;
         this._GROUPID=GROUPID;
         this._SHORTCODE=SHORTCODE;
         this._STATUSID=STATUSID;
         this._CREATEDATE=CREATEDATE;
         this._REMARK=REMARK;
      }
      #endregion

      #region 成员
      private int _SHORTCODEID;
      private int _GROUPID;
      private string _SHORTCODE;
      private int _STATUSID;
      private DateTime _CREATEDATE;
      private string _REMARK;
      #endregion


      #region 属性
      public  virtual int SHORTCODEID
      {
         get {  return _SHORTCODEID; }
         set {  _SHORTCODEID = value; }
      }

      public  virtual int GROUPID
      {
         get {  return _GROUPID; }
         set {  _GROUPID = value; }
      }

      public  virtual string SHORTCODE
      {
         get {  return _SHORTCODE; }
         set {  _SHORTCODE = value; }
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
