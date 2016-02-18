using System;
using com.etak.core.model.operation;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.operation.mapping
{
    class OrderDynamicMapper<TOrder> : SubclassMap<TOrder> where TOrder : Order, new()
    {
        public OrderDynamicMapper()
        {
            DynamicInsert();
            DynamicUpdate();
            LazyLoad();
            
            
            TOrder sampleOrder = new TOrder();
            if (String.IsNullOrWhiteSpace(sampleOrder.Discriminator))
            {
                throw new Exception(String.Format("Discriminator for type '{0}' is null or empty", typeof(TOrder)));
            }
            if (sampleOrder.Discriminator.Length > 6)
            {
                throw new Exception(String.Format("Discriminator for type '{0}' is longer than 6 characters '{1}'", typeof(TOrder), sampleOrder.Discriminator));
            }

            DiscriminatorValue(sampleOrder.Discriminator);
            DuplicatedDiscriminatorChecker.AddOrder<TOrder>(sampleOrder.Discriminator);
        }
    }
}
