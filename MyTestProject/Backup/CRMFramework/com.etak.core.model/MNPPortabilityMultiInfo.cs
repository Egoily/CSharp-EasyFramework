using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2011/3/7 AM 10:54:20
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
	///最后修改时间:    2011/3/7 AM 10:54:20
   ///
   ///Function Description :    
   ///Developer                :    
	///Builded Date:    2011/3/7 AM 10:54:20
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
	///Last Modify Date     :    2011/3/7 AM 10:54:20
   /// </summary>
    [DataContract]
    [Serializable]
    public class MNPPortabilityMultiInfo
   {
      #region 构造函数
      public MNPPortabilityMultiInfo()
      {}

	  public MNPPortabilityMultiInfo(string REFERENCECODE, string MSISDN, string ICCID)
      {
         this._REFERENCECODE=REFERENCECODE;
         this._MSISDN=MSISDN;
         this._ICCID=ICCID;
      }
      #endregion

      #region 成员
	  private long _SeqID;
	  private string _REFERENCECODE;
      private string _MSISDN;
	  private string _ICCID;
	  private int _STATUS;
	  #endregion


      #region 属性
	  public virtual long SeqID
	  {
		  get { return _SeqID; }
		  set { _SeqID = value; }
	  }

	  public virtual string ReferenceCode
      {
         get {  return _REFERENCECODE; }
         set {  _REFERENCECODE = value; }
      }

      public  virtual string Msisdn
      {
         get {  return _MSISDN; }
         set {  _MSISDN = value; }
      }

	  public virtual string ICCID
	  {
		  get { return _ICCID; }
		  set { _ICCID = value; }
	  }

	  public virtual int STATUS
	  {
		  get { return _STATUS; }
		  set { _STATUS = value; }
	  }

	  #endregion

   }
}
