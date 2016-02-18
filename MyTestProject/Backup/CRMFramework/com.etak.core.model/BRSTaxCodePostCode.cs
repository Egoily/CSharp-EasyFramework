using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2009/8/20 PM 3:07:35
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2009/8/20 PM 3:07:35
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2009/8/20 PM 3:07:35
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2009/8/20 PM 3:07:35
   /// </summary>
   [DataContract]
    [Serializable]
    public class BRSTaxCodePostCode
   {
     
      public BRSTaxCodePostCode()
      {}

      public BRSTaxCodePostCode(string POSTCODE, int TAXCODE)
      {
          this.PostCode = POSTCODE;
          this.TaxCode = TAXCODE;
      }
     
      public virtual int SeqID { get; set; }
      public virtual string PostCode { get; set; }
      public virtual int TaxCode { get; set; }
      public virtual BRSTaxCode BRSTaxCodeForPostCode { get; set; }

   }
}
