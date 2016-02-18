using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2010-7-8 11:56:50
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2010-7-8 11:56:50
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2010-7-8 11:56:50
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2010-7-8 11:56:50
   /// </summary>
   [DataContract]
    [Serializable]
    public class CrmCustomersTopupBonusApplyCountInfo
   {
      #region 成员
      private int CUSTOMERID;
      private int COUNT;
      private string RESOURCE;
      private int BONUSID;
      private string YEAR;
      private string MONTH;

      private int ID;       

      #endregion


      #region 属性
      public virtual string Resource
      {
          get { return RESOURCE; }
          set { RESOURCE = value; }
      }

      public virtual int BonusId
      {
          get { return BONUSID; }
          set { BONUSID = value; }
      }

      public virtual string Year
      {
          get { return YEAR; }
          set { YEAR = value; }
      }

      public virtual string Month
      {
          get { return MONTH; }
          set { MONTH = value; }
      }

      public  int CustomerId
      {
         get {  return CUSTOMERID; }
         set {  CUSTOMERID = value; }
      }

      public  int Count
      {
         get {  return COUNT; }
         set {  COUNT = value; }
      }

      public int Id
      {
          get { return ID; }
          set { ID = value; }
      }
      #endregion

   }

}
