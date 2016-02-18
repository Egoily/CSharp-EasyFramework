using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2010-7-7 11:24:50
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2010-7-7 11:24:50
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2010-7-7 11:24:50
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2010-7-7 11:24:50
   /// </summary>
    [DataContract]
    [Serializable]
    public class CrmMvnoTopupBonusInfo
   {
      #region 构造函数
      public CrmMvnoTopupBonusInfo()
      {}

      public CrmMvnoTopupBonusInfo(int BONUSID, int DEALERID, string NAME, string CHANNEL, string DAYINYEAR, bool DAYINYEARENABLED, string DAYINMONTH, bool DAYINMONTHENABLED,
          string DAYINWEEK, bool DAYINWEEKENABLED, bool DETAILENABLED, bool PROMOTIONENABLED, bool ENABLED, DateTime STARTDATE, DateTime ENDDATE, int PRIORITY, DateTime CREATEDATE,
          bool CUMULATIVE, int RecurrenceNumber, int PROMOTIONRECURRENCENUMBER, bool PROMOTIONBONUSNEEDFEE)
      {
         this.BONUSID=BONUSID;
         this.DEALERID=DEALERID;
         this.NAME=NAME;
         this.CHANNEL=CHANNEL;
         this.DAYINYEAR=DAYINYEAR;
         this.DAYINYEARENABLED=DAYINYEARENABLED;
         this.DAYINMONTH=DAYINMONTH;
         this.DAYINMONTHENABLED=DAYINMONTHENABLED;
         this.DAYINWEEK=DAYINWEEK;
         this.DAYINWEEKENABLED=DAYINWEEKENABLED;
         this.DETAILENABLED=DETAILENABLED;
         this.PROMOTIONENABLED=PROMOTIONENABLED;
         this.ENABLED=ENABLED;
         this.STARTDATE=STARTDATE;
         this.ENDDATE=ENDDATE;
         this.PRIORITY=PRIORITY;
         this.CREATEDATE=CREATEDATE;
         this.CUMULATIVE=CUMULATIVE;
         this.RECURRENCENUMBER = RecurrenceNumber;
         this.PROMOTIONRECURRENCENUMBER = PROMOTIONRECURRENCENUMBER;
         this.PROMOTIONBONUSNEEDFEE = PROMOTIONBONUSNEEDFEE;
      }
      #endregion

      #region 成员
      private int BONUSID;
      private int DEALERID;
      private string NAME;
      private string CHANNEL;
      private string DAYINYEAR;
      private bool DAYINYEARENABLED;
      private string DAYINMONTH;
      private bool DAYINMONTHENABLED;
      private string DAYINWEEK;
      private bool DAYINWEEKENABLED;
      private bool DETAILENABLED;
      private bool PROMOTIONENABLED;
      private bool ENABLED;
      private DateTime STARTDATE;
      private DateTime? ENDDATE;
      private int PRIORITY;
      private DateTime CREATEDATE;
      private bool CUMULATIVE;
      private int RECURRENCENUMBER;
      private int PROMOTIONRECURRENCENUMBER;
      private bool PROMOTIONBONUSNEEDFEE;
      #endregion


      #region 属性
      public int BonusId
      {
         get {  return BONUSID; }
         set {  BONUSID = value; }
      }

      public int DealerId
      {
         get {  return DEALERID; }
         set {  DEALERID = value; }
      }

      public string Name
      {
         get {  return NAME; }
         set {  NAME = value; }
      }

      public string Channel
      {
         get {  return CHANNEL; }
         set {  CHANNEL = value; }
      }

      public string DayInYear
      {
         get {  return DAYINYEAR; }
         set {  DAYINYEAR = value; }
      }

      public bool DayInYearEnabled
      {
         get {  return DAYINYEARENABLED; }
         set {  DAYINYEARENABLED = value; }
      }

      public string DayInMonth
      {
         get {  return DAYINMONTH; }
         set {  DAYINMONTH = value; }
      }

      public bool DayInMonthEnabled
      {
         get {  return DAYINMONTHENABLED; }
         set {  DAYINMONTHENABLED = value; }
      }

      public string DayInWeek
      {
         get {  return DAYINWEEK; }
         set {  DAYINWEEK = value; }
      }

      public bool DayInWeekEnabled
      {
         get {  return DAYINWEEKENABLED; }
         set {  DAYINWEEKENABLED = value; }
      }

      public bool DetailEnabled
      {
         get {  return DETAILENABLED; }
         set {  DETAILENABLED = value; }
      }

      public bool PromotionEnabled
      {
         get {  return PROMOTIONENABLED; }
         set {  PROMOTIONENABLED = value; }
      }

      public bool Enabled
      {
         get {  return ENABLED; }
         set {  ENABLED = value; }
      }

      public DateTime StartDate
      {
         get {  return STARTDATE; }
         set {  STARTDATE = value; }
      }

      public DateTime? Enddate
      {
         get {  return ENDDATE; }
         set {  ENDDATE = value; }
      }

      public int Priority
      {
         get {  return PRIORITY; }
         set {  PRIORITY = value; }
      }

      public DateTime CreateDate
      {
         get {  return CREATEDATE; }
         set {  CREATEDATE = value; }
      }

      public bool Cumulative
      {
         get {  return CUMULATIVE; }
         set {  CUMULATIVE = value; }
      }

      public int RecurrenceNumber
      {
         get {  return RECURRENCENUMBER; }
          set { RECURRENCENUMBER = value; }
      }
      public int PromotionRecurrenceNumber
      {
          get { return PROMOTIONRECURRENCENUMBER; }
          set { PROMOTIONRECURRENCENUMBER = value; }
      }
      public bool PromotionBonusNeedFee
      {
          get { return PROMOTIONBONUSNEEDFEE; }
          set { PROMOTIONBONUSNEEDFEE = value; }
      }
      public virtual bool CheckCustomersEligibility { get; set; }
      #endregion

   }
}
