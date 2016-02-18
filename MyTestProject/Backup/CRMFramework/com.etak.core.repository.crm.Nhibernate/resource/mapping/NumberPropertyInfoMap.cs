using com.etak.core.model;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.resource.mapping
{
    class NumberPropertyInfoMap : ClassMap<NumberPropertyInfo>
    {
        public NumberPropertyInfoMap()
        {
            Schema("dbo");
            Table("SYS_NPM_PROPERTY");
            DynamicUpdate();
            DynamicInsert();

            Id(x => x.Resource).
                Column("Resource").CustomType("AnsiString").Length(25).
                GeneratedBy.Foreign("NumberInfo").UnsavedValue(null);

            HasOne(x => x.NumberInfo).Constrained();

            Map(x => x.HideCLI, "HideCLI");
            Map(x => x.StatusID,"StatusID" );
            Map(x => x.CoolDownPeriod,"CoolDownPeriod" );
            Map(x => x.CoolDownDurDate,"CoolDownDurDate");
            Map(x => x.LockedBy,"LockedBy" );
            Map(x => x.LockedDate,"LockedDate");
            Map(x => x.TariffCode, "TariffCode").Length(15);
            Map(x => x.ChangeStatusDate,"ChangeStatusDate");
            Map(x => x.CreateUserID,"CreateUserID" );
            Map(x => x.CreateDate,"CreateDate");
            Map(x => x.UpdateUserID, "UpdateUserID");
            Map(x => x.UpdateDate,"UpdateDate");
            Map(x => x.DataStatus,"DataStatus" );
            Map(x => x.locationid, "locationid").Length(25);
            Map(x => x.numbertypeid, "NUMBERTYPEID").Length(4);
        }
    }
}
