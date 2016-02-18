using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2009-9-23 11:39:03
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2009-9-23 11:39:03
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2009-9-23 11:39:03
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2009-9-23 11:39:03
   /// </summary>
    [DataContract]
    [Serializable]
    public class DateCategoryInfo
   {

      #region 成员
      //private int _DATECATEGORYID;
      //private int _DATECATEGORYSEQ;
      private string _DATECATEGORYNAME;
      private int _DATECATEGORYTYPEID;
      private int _MONTH;
      private string _DAYOFMONTH;
      private string _DAYOFWEEK;
      private int _STARTTIME;
      private int _ENDTIME;
      private string _REMARK;
      #endregion

      #region 属性
      //public  int DateCategoryId
      //{
      //   get {  return _DATECATEGORYID; }
      //   set {  _DATECATEGORYID = value; }
      //}

      //public  int DateCategorySeq
      //{
      //   get {  return _DATECATEGORYSEQ; }
      //   set {  _DATECATEGORYSEQ = value; }
      //}

      public  string DateCategoryName
      {
         get {  return _DATECATEGORYNAME; }
         set {  _DATECATEGORYNAME = value; }
      }

      public  int DateCategoryTypeId
      {
         get {  return _DATECATEGORYTYPEID; }
         set {  _DATECATEGORYTYPEID = value; }
      }

      public  int Month
      {
         get {  return _MONTH; }
         set {  _MONTH = value; }
      }

      public string DayOfMonth
      {
         get {  return _DAYOFMONTH; }
         set {  _DAYOFMONTH = value; }
      }

      public  string DayOfWeek
      {
         get {  return _DAYOFWEEK; }
         set {  _DAYOFWEEK = value; }
      }

      public  int StartTime
      {
         get {  return _STARTTIME; }
         set {  _STARTTIME = value; }
      }

      public  int EndTime
      {
         get {  return _ENDTIME; }
         set {  _ENDTIME = value; }
      }

      public  string Remark
      {
         get {  return _REMARK; }
         set {  _REMARK = value; }
      }

      private DateCategoryPKInfo _PKInfo;

      public DateCategoryPKInfo PKInfo
      {
          get { return _PKInfo; }
          set { _PKInfo = value; }
      }

      #endregion

      public override bool Equals(object obj)
      {
          if (obj is DateCategoryInfo)
          {
              DateCategoryInfo second = obj as DateCategoryInfo;
              if (this.PKInfo.DateCategoryID == second.PKInfo.DateCategoryID
                   && this.PKInfo.DateCategorySeq == second.PKInfo.DateCategorySeq)
              {
                  return true;
              }
              return false;
          }
          return false;
      }

      public override int GetHashCode()
      {
          return base.GetHashCode();
      }

   }

    [DataContract]
    [Serializable]
    public class DateCategoryPKInfo
    {
        #region
        private int? _DateCategoryID = null;
        private int? _DateCategorySeq = 0;
        #endregion

        #region Attribute
        public int? DateCategoryID
        {
            get { return _DateCategoryID; }
            set { _DateCategoryID = value; }
        }

        public int? DateCategorySeq
        {
            get { return _DateCategorySeq; }
            set { _DateCategorySeq = value; }
        }
        #endregion

        /// <summary>
        /// Override Equals method needed by Nhibernate to map the entity 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;

            var other = obj as DateCategoryPKInfo;
            if (other == null) return false;

            if (DateCategoryID == other.DateCategoryID
                && DateCategorySeq == other.DateCategorySeq)
                return true;
            
            return false;
        }

        /// <summary>
        /// Override GetHashCode method needed by Nhibernate to map the entity
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = DateCategoryID.GetHashCode();
                result = 29 * result + DateCategorySeq.GetHashCode();

                return result;

            }
        }
    }
}
