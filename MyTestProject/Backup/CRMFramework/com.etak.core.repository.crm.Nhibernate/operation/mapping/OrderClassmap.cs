using com.etak.core.model.operation;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.operation.mapping
{
    /// <summary>
    /// Fluent nhibernate class map for Order
    /// </summary>
    public class OrderClassmap : ClassMap<Order>
    {
        /// <summary>
        /// Public constructor for fluent Nhibernate 
        /// </summary>
        public OrderClassmap()
        {
            Table("CRM_ORDERS");
            DiscriminateSubClassesOnColumn("ORDER_DISCRIMINATOR").Length(6);

            Id(x => x.Id, "ORDERID").GeneratedBy.Custom<MiddlefixIdGenerator>();
            Map(x => x.CompletitionDate, "COMPLETIONDATE");
            Map(x => x.CreationDate, "CREATEDATE");
            Map(x => x.ExternalId, "EXTERNALID").CustomType("AnsiString");
            Map(x => x.LastUpdateDate, "LAST_UPDATEDATE");
            Map(x => x.Status, "STATUS").CustomType("AnsiString").Length(4);
            Map(x => x.ThirdPartyId, "THIRD_PARTYID").CustomType("AnsiString");
            Map(x => x.ExternalThirdPartyId, "EXTERNAL_THIRD_PARTYID").CustomType("AnsiString");

            HasMany(x => x.OperationsForOrder)
             .KeyColumn("ORDERID")
             .ForeignKeyConstraintName("FK_OPERATION_ORDER_ID")
             .Inverse();

            References(x => x.Dealer, "FISCALUNITID").ForeignKey("FK_ORDER_DEALERID");
            References(x => x.ParentOrders, "PARENT_ORDERID").ForeignKey("FK_ORDER_PARENT_ORDERID");
            References(x => x.StartingOperation, "STARTING_OPERATIONEXECUTIONID").ForeignKey("FK_ORDER_OPERATION_ID");
            
            HasMany(x => x.SubOrders)
                .KeyColumn("PARENT_ORDERID")
                .ForeignKeyConstraintName("FK_PARENT_ORDER_ID")
                .Inverse();
        }
    }
}
