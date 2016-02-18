using com.etak.core.model;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.resource.mapping
{
    /// <summary>
    /// Fluent Nhibernate mapping for SimCardInfo
    /// </summary>
    public class SIMCardInfoClassMap : ClassMap<SIMCardInfo>
    {
        /// <summary>
        /// Default constructor so fluent nhibernate creates the xml mapping on runtime. 
        /// </summary>
        public SIMCardInfoClassMap()
        {
            Table("CRM_MOBILE_SIMCARDS");
            DynamicInsert();
            DynamicUpdate();
            LazyLoad();

            Id(x => x.ICCID, "ICCID").Length(20).CustomType("AnsiString").GeneratedBy.Assigned();
            References(x => x.Dealer, "DEALERID");
            Map(x => x.IMSI1, "IMSI1");
            Map(x => x.IMSI2, "IMSI2");
            Map(x => x.IMSI3, "IMSI3");
            Map(x => x.IMSI4, "IMSI4");
            Map(x => x.IMSI5, "IMSI5");
            Map(x => x.IMSI6, "IMSI6");
            Map(x => x.IMSI7, "IMSI7");
            Map(x => x.IMSI8, "IMSI8");
            Map(x => x.IMSI9, "IMSI9");
            Map(x => x.IMSI10, "IMSI10");
            Map(x => x.IMSI11, "IMSI11");
            Map(x => x.IMSI12, "IMSI12");
            Map(x => x.IMSI13, "IMSI13");
            Map(x => x.IMSI14, "IMSI14");
            Map(x => x.IMSI15, "IMSI15");
            Map(x => x.MSISDN, "MSISDN");
            Map(x => x.PIN1, "PIN1");
            Map(x => x.PIN2, "PIN2");
            Map(x => x.PUK1, "PUK1");
            Map(x => x.PUK2, "PUK2");
            Map(x => x.KI, "KI");
            Map(x => x.OPC, "OPC");
            Map(x => x.KIC_0F, "KIC_0F");
            Map(x => x.KID_0F, "KID_0F");
            Map(x => x.KIK_0F, "KIK_0F");
            Map(x => x.ADM1, "ADM1");
            Map(x => x.ADM2, "ADM2");
            Map(x => x.ACC, "ACC");
            Map(x => x.Status, "STATUS");
            Map(x => x.AlgorithmName, "ALGORITHMNAME");
            Map(x => x.ManufacturerID, "MANUFACTURERID");
            Map(x => x.SIMType, "SIMTYPE");
            Map(x => x.ActivateType, "ACTIVATETYPE");
            Map(x => x.ManufactureDate, "MANUFACTUREDATE");
            Map(x => x.ChangeStatusDate, "CHANGESTATUSDATE");
            Map(x => x.AssignStatusId, "ASSIGNSTATUSID");
            Map(x => x.KIC2, "KIC2");
            Map(x => x.KID2, "KID2");
            Map(x => x.KIK2, "KIK2");
            Map(x => x.AlgoID, "AlgoID");
            Map(x => x.TEMPORARY_IMSI, "TEMPORARY_IMSI");
            Map(x => x.TEMPORARY_MSISDN, "TEMPORARY_MSISDN");
            Map(x => x.ManufacturerEncryptionType, "MANUFACTURERENCRYPTIONTYPE");
        }
    }
}
