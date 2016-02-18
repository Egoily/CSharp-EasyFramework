using com.etak.core.model.operation;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace com.etak.core.repository.crm.Nhibernate.operation.mapping
{
    /// <summary>
    /// Fluent nhibernate class map for OrderTransition
    /// </summary>
    public class OrderTransitionMap : ClassMap<OrderTransition>
    {
        /// <summary>
        /// Public constructor for fluent Nhibernate 
        /// </summary>
        public OrderTransitionMap()
        {
            Table("CRM_ORDER_STATUS_HISTORY");
            DynamicUpdate();
            DynamicInsert();

            Id(x => x.Id, "ORDER_STATUS_HISTORYID").GeneratedBy.Custom<MiddlefixIdGenerator>();
            Map(x => x.Date, "STATUSCHANGEDATE").CustomType("DateTime2").Precision(7);
            Map(x => x.DestinationState, "NEW_STATUS").CustomType("AnsiString").Length(4);
            Map(x => x.SourceState, "OLD_STATUS").CustomType("AnsiString").Length(4);
            Map(x => x.TransitionCode, "TRANSITION_CODE");
            References(x => x.Order, "ORDERID").ForeignKey("FK_ORDER_TRANSITION_ORDERID");
            References(x => x.TriggeringOperation, "OPERATION_EXECUTIONID").ForeignKey("FK_TRANSITION_OPERATION_EXECUTIONID");
        }
    }
}
