
using com.etak.core.model;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.resource.mapping
{
    class DealerNumberInfoMap : ClassMap<DealerNumberInfo>
    {
        private DealerNumberInfoMap()
        {
            Schema("dbo");
            Table("SYS_NPM_DEALER");
            DynamicUpdate();

            Id(x => x.ID, "ID").GeneratedBy.Custom<PrefixIdGenerator>();
            References(x => x.Resource).Column("Resource");
            Map(x => x.DealerID).Column("DealerID");
            Map(x => x.ShareType).Column("ShareType");
           
        }
    }
}
