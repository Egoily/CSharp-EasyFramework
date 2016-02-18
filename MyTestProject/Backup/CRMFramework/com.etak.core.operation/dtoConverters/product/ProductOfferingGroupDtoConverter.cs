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
    /// Convertor class from ProductOfferingGroupOption to DTO
    /// </summary>
    public class ProductOfferingGroupDtoConverter : ITypeConverter<ProductOfferingGroupOption, ProductOfferingGroupDTO>
    {

        /// <summary>
        /// Convert from ProductOfferingGroupOption to ProductOfferingGroupDTO
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public ProductOfferingGroupDTO Convert(ProductOfferingGroupOption source)
        {
            if (source != null && source.Group == null)
                return null;
            

            var groupDto = new ProductOfferingGroupDTO()
            {
                GroupId = source.Id,
                Names = source.Group.Names == null ? null : source.Group.Names.ToDto(),
                Descriptions = source.Group.Description == null? null : source.Group.Description.ToDto(),
                MaxOccurs = source.MaxOccurs,
                MinOccurs = source.MinOccurs,
                OptionType = source.ProductOptionType,
                Strategy = source.ConflictResolutionStrategy,
            };

            return groupDto;
        }
    }
}
