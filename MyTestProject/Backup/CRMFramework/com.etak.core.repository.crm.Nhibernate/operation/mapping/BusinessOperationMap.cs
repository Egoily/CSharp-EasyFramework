using com.etak.core.model.operation;
using com.etak.core.repository.crm.Nhibernate.Factory;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.operation.mapping
{
    /// <summary>
    /// Fluent nhibernate class map for BusinessOperation
    /// </summary>
    public class BusinessOperationMap : ClassMap<BusinessOperation>
    {
        /// <summary>
        /// Public constructor for fluent Nhibernate 
        /// </summary>
        public BusinessOperationMap()
        {
            Table("CRM_OPERATIONS");
            DynamicInsert();
            DynamicUpdate();
            Cache.NonStrictReadWrite().Region(CacheRegions.Constants);
            
            DiscriminateSubClassesOnColumn("OPERATION_DISCRIMINATOR").Length(6);

            Id(x => x.Id, "OPERATIONID").GeneratedBy.Assigned();
            Map(x => x.OperationCode, "OPERATIONCODE").CustomType("AnsiString").Length(6);
            References(x => x.Descriptions, "DESCRIPTIONID").ForeignKey("FK_TEXT_LOOKUP");
        }
    }
}
