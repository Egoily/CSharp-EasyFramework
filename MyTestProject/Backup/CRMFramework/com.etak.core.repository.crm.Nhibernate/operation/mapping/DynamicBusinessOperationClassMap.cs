using System;
using com.etak.core.model.operation;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.operation.mapping
{
    /// <summary>
    /// Maps any business operation
    /// </summary>
    /// <typeparam name="TBizOp"></typeparam>
    public class DynamicBusinessOperationClassMap<TBizOp> : SubclassMap<TBizOp> where TBizOp : BusinessOperation, new()
    {
        /// <summary>
        /// Public constructor for fluent Nhibernate 
        /// </summary>
        public DynamicBusinessOperationClassMap()
        {
            TBizOp instance = new TBizOp();
            Not.LazyLoad();

            if (instance.OperationDiscriminator.Length > 6)
            {
                throw new Exception(String.Format("Discriminator for type '{0}' is longer than 6 characters '{1}'", typeof(TBizOp), instance.OperationDiscriminator));
            }
            if (instance.GetType().ContainsGenericParameters)
            {
                throw new Exception(String.Format("BusinessOpertaion  of type '{0}'  ContainsGenericParameters which is not supported", typeof(TBizOp)));
            }
            DiscriminatorValue(instance.OperationDiscriminator);
            DuplicatedDiscriminatorChecker.AddBizOp<TBizOp>(instance.OperationDiscriminator);
        }
    }
}
