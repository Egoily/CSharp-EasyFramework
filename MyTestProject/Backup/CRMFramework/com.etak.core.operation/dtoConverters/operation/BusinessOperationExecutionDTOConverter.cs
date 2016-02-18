using System;
using com.etak.core.model.operation;
using com.etak.core.operation.contract;

namespace com.etak.core.operation.dtoConverters.operation
{
    class BusinessOperationExecutionDTOConverter : ITypeConverter<BusinessOperationExecution, BusinessOperationExecutionDTO>
    {
        static BusinessOperationExecutionDTOConverter()
        {
            AutoMapper.Mapper.CreateMap<BusinessOperationExecution, BusinessOperationExecutionDTO>()
                .ForMember(x => x.CustomerDestinationId, s => s.MapFrom(y => y.CustomerDestination == null? 0 : y.CustomerDestination.CustomerID))
                .ForMember(x => x.CustomerId, s => s.MapFrom(y => y.Customer == null?0:y.Customer.CustomerID))
                .ForMember(x => x.ICCId, s => s.MapFrom(y => y.SimCard == null ? String.Empty : y.SimCard.ICCID))
                .ForMember(x => x.MVNOId, s => s.MapFrom(y =>y.MVNO == null ? 0: y.MVNO.DealerID))
                .ForMember(x => x.OrderManagedId, s => s.MapFrom(y => y.OrderManaged ==null? 0 : y.OrderManaged.Id))
                .ForMember(x => x.SubscriptionDestinationId, s => s.MapFrom(y => y.SubscriptionDestination == null ? 0 : y.SubscriptionDestination.ResourceID))
                .ForMember(x => x.SubscriptionId, s => s.MapFrom(y => y.Subscription == null ? 0 : y.Subscription.ResourceID))
                .ForMember(x => x.UserId, s => s.MapFrom(y => y.User == null ? 0 : y.User.UserID))
                .ForMember(x => x.MSISDN, s => s.MapFrom(y =>y.MSISDN == null ? String.Empty : y.MSISDN.Resource))
                .ForMember(x => x.ProductOfferingId, map => map.MapFrom(src => src.ProductOffering == null ? 0 : src.ProductOffering.Id))
                .ForMember(x => x.ProductId, map => map.MapFrom(src => src.Product == null ? 0 : src.Product.Id));
        }

        public BusinessOperationExecutionDTO Convert(BusinessOperationExecution source)
        {
            return AutoMapper.Mapper.Map<BusinessOperationExecutionDTO>(source);
        }
    }
}
