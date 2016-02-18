using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using com.etak.core.operation.contract;

namespace com.etak.core.operation.dtoConverters.product
{
    /// <summary>
    /// Convert from ProductOfferingSpecificationOption to ProductOfferingSpecificationDto
    /// </summary>
    public class ProductOfferingSpecificationOptionDtoConverter : ITypeConverter<ProductOfferingSpecificationOption, ProductOfferingSpecificationDTO>
    {
        static ProductOfferingSpecificationOptionDtoConverter()
        {
            AutoMapper.Mapper.CreateMap<ProductOfferingSpecificationOption, ProductOfferingSpecificationDTO>()
                .ForMember(dst => dst.SpecifiedProductOffering, map => map.MapFrom(src => src.SpecifiedProductOffering.ToDto()))
                .ForMember(dst => dst.Strategy, map => map.MapFrom(src => src.ConflictResolutionStrategy))
                .ForMember(dst => dst.OptionType, map => map.MapFrom(src => src.ProductOptionType));
        }
        /// <summary>
        /// Convert from ProductOfferingSpecificationOption to ProductOfferingSpecificationDTO
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public ProductOfferingSpecificationDTO Convert(ProductOfferingSpecificationOption source)
        {
            if (source != null && source.SpecifiedProductOffering == null)
                return null;

            return AutoMapper.Mapper.Map<ProductOfferingSpecificationDTO>(source);

        }
    }
}
