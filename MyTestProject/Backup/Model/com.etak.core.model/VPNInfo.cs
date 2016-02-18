using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2010-10-18 15:25:08
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2010-10-18 15:25:08
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2010-10-18 15:25:08
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2010-10-18 15:25:08
   /// </summary>
    [DataContract]
    [Serializable]
    public class VPNInfo
   {
      #region 构造函数
      public VPNInfo()
      {}

      public virtual VPNInfo Clone()
      {

          return (VPNInfo)this.MemberwiseClone();

      } 
      public VPNInfo(int VPNID, int DEALERID, string VPNNAME, int GROUPCODELENGTH, int SHORTCODELENGTH, DateTime CREATEDATE, string REMARK)
      {
         this._VPNID=VPNID;
         this._DEALERID=DEALERID;
         this._VPNNAME=VPNNAME;
         this._GROUPCODELENGTH=GROUPCODELENGTH;
         this._SHORTCODELENGTH=SHORTCODELENGTH;
         this._CREATEDATE=CREATEDATE;
         this._REMARK=REMARK;
      }
      #endregion

      #region 成员
      private int _VPNID;
      private int _DEALERID;
      private string _VPNNAME;
      private int _GROUPCODELENGTH;
      private int _SHORTCODELENGTH;
      private DateTime _CREATEDATE;
      private string _REMARK;
      #endregion


      #region 属性
      public  virtual int VPNID
      {
         get {  return _VPNID; }
         set {  _VPNID = value; }
      }

      public  virtual int DEALERID
      {
         get {  return _DEALERID; }
         set {  _DEALERID = value; }
      }

      public  virtual string VPNNAME
      {
         get {  return _VPNNAME; }
         set {  _VPNNAME = value; }
      }

      public  virtual int GROUPCODELENGTH
      {
         get {  return _GROUPCODELENGTH; }
         set {  _GROUPCODELENGTH = value; }
      }

      public  virtual int SHORTCODELENGTH
      {
         get {  return _SHORTCODELENGTH; }
         set {  _SHORTCODELENGTH = value; }
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
