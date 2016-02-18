using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2011/4/8 10:17:19
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2011/4/8 10:17:19
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2011/4/8 10:17:19
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2011/4/8 10:17:19
    /// </summary>
    [DataContract]
    [Serializable]
    public class RoamingSettingInfo : ModelBase
    {
        virtual public int SettingID { get; set; }
        virtual public bool EnableBaocPrepaid { get; set; }
        virtual public bool EnableBaocPostpaid { get; set; }
        virtual public bool ReceiveSmsPostpaid { get; set; }
        virtual public bool ReceiveSmsPrepaid { get; set; }
        virtual public bool ReceiveSmsNoneCamel { get; set; }
        virtual public bool ReceiveSmsCamel { get; set; }
        virtual public bool Activated { get; set; }
        virtual public string FtpServer { get; set; }
        virtual public int FtpPort { get; set; }
        virtual public string Folder { get; set; }
        virtual public string UserName { get; set; }
        virtual public string Password { get; set; }
        virtual public int ImportWay { get; set; }
        virtual public DateTime? CreateDate { get; set; }
        virtual public DealerInfo DealerInfo { get; set; }

    }
}
