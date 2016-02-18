using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace com.etak.core.repository.crm.Nhibernate.operation.mapping
{
    /// <summary>
    /// Fluent nhibernate class map for BusinessOperationExecution
    /// </summary>
    public class BusinessOperationExecutionClassmap : ClassMap<BusinessOperationExecution>
    {
        /// <summary>
        /// Public constructor for fluent Nhibernate 
        /// </summary>
        public BusinessOperationExecutionClassmap()
        {
            Table("CRM_OPERATION_EXECUTIONS");
            DynamicInsert();
            DynamicUpdate();

            Id(x => x.Id, "OPERATION_EXECUTIONID").GeneratedBy.Custom<MiddlefixIdGenerator>();
            References(x => x.OperationDefintition, "OPERATIONID");
            References(x => x.Configuration, "OPERATION_CONFIGURATIONID");
            //Map(x => x.OperationCode, "OPERATION_CODE");
            Map(x => x.StartTime, "STARTDATE").CustomType<TimestampType>();
            Map(x => x.EndDate, "ENDDATE").CustomType<TimestampType>();
            Map(x => x.Channel, "EXTERNAL_CHANNEL").CustomType("AnsiString");
            Map(x => x.Amount, "AMOUNT").Precision(22).Scale(8);
            Map(x => x.ResultType, "RESULTTYPE").CustomType<ResultTypes>();
            Map(x => x.ErrorCode, "ERRORCODE");
            //Map(x => x.ProcessorDiscriminator, "OPERATION_DISCRIMINATOR");
            Map(x => x.SystemMessages, "ERRORMESSAGE").Length(1024);

            References(x => x.ParentBusinessOperation, "PARENT_OPERATION_EXECUTIONID")
                .Cascade.None()
                .ForeignKey("FK_PARENT_OPERATIONID");

            References(x => x.RootBusinessOperation, "ROOT_OPERATION_EXECUTIONID")
               .Cascade.None()
               .ForeignKey("FK_ROOT_OPEXECUTIONID");

            References(x=> x.User, "USERID").ForeignKey("FK_OPERAION_USERID");
            References(x => x.MVNO, "FISCALUNITID").ForeignKey("FK_OPERAION_DEALERID");
            References(x => x.Customer, "CUSTOMERID").ForeignKey("FK_OPERAION_CUSTOMERID").Index("IX_CUSTOMERID");
            References(x => x.CustomerDestination, "DESTINATION_CUSTOMERID").ForeignKey("FK_OPERAION_DESTINATION_CUSTOMER_ID").Index("IX_DEST_CUSTOMERID");
            References(x => x.Subscription, "RESOURCEID").ForeignKey("FK_OPERAION_RESOURCEMBID");
            References(x => x.SubscriptionDestination, "DESTINATION_RESOURCEID").ForeignKey("FK_OPERAION_DESTINATION_RESOURCEMBID");
            References(x => x.MSISDN, "MSISDN").ForeignKey("FK_OPERAION_SYS_NPMID");
            References(x => x.Account, "ACCOUNTID").ForeignKey("FK_OPERAION_ACCOUNTID");
            References(x => x.ProductOffering, "PRODUCT_OFFERINGID").ForeignKey("FK_OPERATION_PRODUCT_OFFERINGID");
            References(x => x.Product, "PRODUCTGK").ForeignKey("FK_OPERATION_PRODUCTGK");
            References(x => x.ProductAssignment, "CUSTOMERPRODUCTID").ForeignKey("FK_OPERAION_CUST_PROD_ASSNID");
            References(x => x.SimCard, "ICCID").ForeignKey("FK_OPERAION_SIMID");
            References(x => x.OrderManaged, "ORDERID").ForeignKey("FK_OPERAION_ORDERID");
        }
    }
}
