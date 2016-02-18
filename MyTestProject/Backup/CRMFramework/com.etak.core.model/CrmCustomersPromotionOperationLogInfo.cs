using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2011-8-17 17:16:50
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2011-8-17 17:16:50
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2011-8-17 17:16:50
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2011-8-17 17:16:50
   /// </summary>
   [DataContract]
    [Serializable]
    public class CrmCustomersPromotionOperationLogInfo : PromotionModelBase
   {
      #region 构造函数
      public CrmCustomersPromotionOperationLogInfo()
      {}

      public CrmCustomersPromotionOperationLogInfo(long LOGID, long? PRELOGID, DateTime OPERATIONDATE, int OPCODE, int DEALERID, string MSISDN, int CUSTOMERID, int PROMOTIONPLANID, string STATUS, int APRIORITY, DateTime ASTARTDATE, DateTime AENDDATE, decimal AAMOUNT, DateTime ANEXTRENEWDATE, int BPRIORITY, DateTime BSTARTDATE, DateTime BENDDATE, decimal BAMOUNT, DateTime BNEXTRENEWDATE)
      {
         this._LOGID=LOGID;
         this._PRELOGID=PRELOGID;
         this._OPERATIONDATE=OPERATIONDATE;
         this._OPCODE=OPCODE;
         this._DEALERID=DEALERID;
         this._MSISDN=MSISDN;
         this._CUSTOMERID=CUSTOMERID;
         this._PROMOTIONPLANID=PROMOTIONPLANID;
         this._STATUS=STATUS;
         this._APRIORITY=APRIORITY;
         this._ASTARTDATE=ASTARTDATE;
         this._AENDDATE=AENDDATE;
         this._AAMOUNT=AAMOUNT;
         this._ANEXTRENEWDATE=ANEXTRENEWDATE;
         this._BPRIORITY=BPRIORITY;
         this._BSTARTDATE=BSTARTDATE;
         this._BENDDATE=BENDDATE;
         this._BAMOUNT=BAMOUNT;
         this._BNEXTRENEWDATE=BNEXTRENEWDATE;

      }
      #endregion

      #region 成员
      private long _LOGID;
      private long? _PRELOGID;
      private DateTime _OPERATIONDATE;
      private int _OPCODE;
      private int _DEALERID;
      private string _MSISDN;
      private int _CUSTOMERID;
      private decimal? _OPERATIONCOST;
      private int _PROMOTIONPLANID;
      private string _STATUS;
      private int? _APRIORITY;
      private DateTime? _ASTARTDATE;
      private DateTime? _AENDDATE;
      private decimal? _AAMOUNT;
      private DateTime? _ANEXTRENEWDATE;
      private int? _BPRIORITY;
      private DateTime? _BSTARTDATE;
      private DateTime? _BENDDATE;
      private decimal? _BAMOUNT;
      private DateTime? _BNEXTRENEWDATE;
      private int _PROMOTIONPLANDETAILID;
      #endregion


      #region 属性
      public  long LOGID
      {
         get {  return _LOGID; }
         set {  _LOGID = value; }
      }

      public  long? PRELOGID
      {
         get {  return _PRELOGID; }
         set {  _PRELOGID = value; }
      }

      public  DateTime OPERATIONDATE
      {
         get {  return _OPERATIONDATE; }
         set {  _OPERATIONDATE = value; }
      }

      public  int OPCODE
      {
         get {  return _OPCODE; }
         set {  _OPCODE = value; }
      }

      public  int DEALERID
      {
         get {  return _DEALERID; }
         set {  _DEALERID = value; }
      }

      public  string MSISDN
      {
         get {  return _MSISDN; }
         set {  _MSISDN = value; }
      }

      public int CUSTOMERID
      {
         get {  return _CUSTOMERID; }
         set {  _CUSTOMERID = value; }
      }

      public decimal? OPERATIONCOST
      {
          get { return _OPERATIONCOST; }
          set { _OPERATIONCOST = value; }
      }

      public  int PROMOTIONPLANID
      {
         get {  return _PROMOTIONPLANID; }
         set {  _PROMOTIONPLANID = value; }
      }

      public  string STATUS
      {
         get {  return _STATUS; }
         set {  _STATUS = value; }
      }

      public  int? APRIORITY
      {
         get {  return _APRIORITY; }
         set {  _APRIORITY = value; }
      }

      public  DateTime? ASTARTDATE
      {
         get {  return _ASTARTDATE; }
         set {  _ASTARTDATE = value; }
      }

      public  DateTime? AENDDATE
      {
         get {  return _AENDDATE; }
         set {  _AENDDATE = value; }
      }

      public  decimal? AAMOUNT
      {
         get {  return _AAMOUNT; }
         set {  _AAMOUNT = value; }
      }

      public  DateTime? ANEXTRENEWDATE
      {
         get {  return _ANEXTRENEWDATE; }
         set {  _ANEXTRENEWDATE = value; }
      }

      public  int? BPRIORITY
      {
         get {  return _BPRIORITY; }
         set {  _BPRIORITY = value; }
      }

      public  DateTime? BSTARTDATE
      {
         get {  return _BSTARTDATE; }
         set {  _BSTARTDATE = value; }
      }

      public  DateTime? BENDDATE
      {
         get {  return _BENDDATE; }
         set {  _BENDDATE = value; }
      }

      public  decimal? BAMOUNT
      {
         get {  return _BAMOUNT; }
         set {  _BAMOUNT = value; }
      }

      public  DateTime? BNEXTRENEWDATE
      {
         get {  return _BNEXTRENEWDATE; }
         set {  _BNEXTRENEWDATE = value; }
      }

      public string DESCRIPTION
      { get; set; }

      public int PROMOTIONPLANDETAILID
      {
          get { return _PROMOTIONPLANDETAILID; }
          set { _PROMOTIONPLANDETAILID = value; }
      }
      //added by neil at 2014/09/01
      public virtual int? PromotionGroupID { get; set; }

      //added by neil at 2014/09/01
      public virtual string OperationCode { get; set; }

      public CrmCustomersPromotionInfo Promotion { get; set; }
      public virtual decimal PricePerLimitUnit { get; set; }
	  #endregion

   }
}
