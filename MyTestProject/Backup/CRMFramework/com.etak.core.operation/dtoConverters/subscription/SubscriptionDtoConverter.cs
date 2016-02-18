using AutoMapper;
using com.etak.core.model;
using com.etak.core.model.subscription;

namespace com.etak.core.operation.dtoConverters.subscription
{
    /// <summary>
    /// Converter for ResourceMB to SubscriptionDTO
    /// </summary>
    public class SubscriptionDtoConverter : contract.ITypeConverter<ResourceMBInfo, SubscriptionDTO>, contract.ITypeConverter<SubscriptionDTO, ResourceMBInfo>
    {
        /// <summary>
        /// Constructor where the mappings are set up.
        /// </summary>
        static SubscriptionDtoConverter()
        {
            #region ResourceMBInfo -> SubscriptionDTO
            Mapper.CreateMap<ResourceMBInfo, SubscriptionDTO>()
                    .ForMember(dst => dst.Id, map => map.MapFrom(src => src.ResourceID))
                    .ForMember(dst => dst.MSISDN, map => map.MapFrom(src => src.Resource))
                    .ForMember(dst => dst.CustomerId, map => map.MapFrom(src => src.CustomerInfo != null ? src.CustomerInfo.CustomerID : 0))
                    .ForMember(dst => dst.OperatorInfo, map => map.MapFrom(src => src.OperatorInfo != null ? src.OperatorInfo.FiscalUnitID : 0))
                    .ForMember(dst => dst.ICCId, map => map.MapFrom(src => src.ICC))
                    .ForMember(dst => dst.Status, map => map.MapFrom(src => src.StatusID)); 
            #endregion

            #region SubscriptionDTO

            Mapper.CreateMap<SubscriptionDTO, ResourceMBInfo>()
                .ForMember(dst => dst.ResourceID, map => map.MapFrom(src => src.Id))
                .ForMember(dst => dst.Resource, map => map.MapFrom(src => src.MSISDN))
                .ForMember(dst => dst.StatusID, map => map.MapFrom(src => src.Status))
                .ForMember(dst => dst.ICC, map => map.MapFrom(src => src.ICCId))
                .ForMember(dst => dst.OperatorInfo, map => map.UseValue(null));

            #endregion
        }

        /// <summary>
        /// Convert from ResourceMBInfo to SubscriptionDTO
        /// </summary>
        /// <param name="source">ResrouceMBInfo to be converted</param>
        /// <returns>SubscriptionDTO converted</returns>
        public SubscriptionDTO Convert(ResourceMBInfo source)
        {
            return Mapper.Map<SubscriptionDTO>(source);
        }

        /// <summary>
        /// Convert from SubscriptionDTO to ResourceMBInfo
        /// </summary>
        /// <param name="source">ResrouceMBInfo to be converted</param>
        /// <returns>SubscriptionDTO converted</returns>
        public ResourceMBInfo Convert(SubscriptionDTO source)
        {
            return Mapper.Map<ResourceMBInfo>(source);
        }
    }
}
