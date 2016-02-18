using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2010-10-26 15:15:07
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2010-10-26 15:15:07
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2010-10-26 15:15:07
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2010-10-26 15:15:07
   /// </summary>
   [DataContract]
    [Serializable]
    public class ExternalResourceMBInfo
   {
      #region 构造函数
      public ExternalResourceMBInfo()
      {}
      public virtual ExternalResourceMBInfo Clone()
      {

          return (ExternalResourceMBInfo)this.MemberwiseClone();

      }
      public ExternalResourceMBInfo(int RESOURCEID, int DEALERID, string RESOURCE, int STATUSID, DateTime CREATEDATE, string REMARK)
      {
         this._RESOURCEID=RESOURCEID;
         this._DEALERID=DEALERID;
         this._RESOURCE=RESOURCE;
         this._STATUSID=STATUSID;
         this._CREATEDATE=CREATEDATE;
         this._REMARK=REMARK;
      }
      #endregion

      #region 成员
      private int _RESOURCEID;
      private int _DEALERID;
      private string _RESOURCE;
      private int _STATUSID;
      private DateTime _CREATEDATE;
      private string _REMARK;
      #endregion


      #region 属性
      public  virtual int RESOURCEID
      {
         get {  return _RESOURCEID; }
         set {  _RESOURCEID = value; }
      }

      public  virtual int DEALERID
      {
         get {  return _DEALERID; }
         set {  _DEALERID = value; }
      }

      public  virtual string RESOURCE
      {
         get {  return _RESOURCE; }
         set {  _RESOURCE = value; }
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
