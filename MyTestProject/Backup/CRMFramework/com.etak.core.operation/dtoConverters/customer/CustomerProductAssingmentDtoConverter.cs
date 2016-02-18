using AutoMapper;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.operation.dtoConverters.customer
{
    /// <summary>
    /// Converter for the entity CustomerProductAssingment
    /// </summary>
    public class CustomerProductAssingmentDtoConverter : contract.ITypeConverter<CustomerProductAssignment, CustomerProductAssignmentDTO>
    {

        static CustomerProductAssingmentDtoConverter()
        {
            #region CustomerProductAssignment -> CustomerProductAssignmentDTO

            Mapper.CreateMap<CustomerProductAssignment, CustomerProductAssignmentDTO>()

                .ForMember(dst => dst.CreateDate, map => map.MapFrom(src => src.CreateDate))
                .ForMember(dst => dst.CreatingOrderId, map => map.MapFrom(src => src.CreatingOrder == null ? 0 : src.CreatingOrder.Id))
                .ForMember(dst => dst.EndDate, map => map.MapFrom(src => src.EndDate))
                .ForMember(dst => dst.Id, map => map.MapFrom(src => src.Id))
                .ForMember(dst => dst.ProductChargePurchasedId, map => map.MapFrom(src => src.ProductChargePurchased.Id))
                .ForMember(dst => dst.PurchasedProductId, map => map.MapFrom(src => src.PurchasedProduct.Id))
                .ForMember(dst => dst.PurchasingCustomerId, map => map.MapFrom(src => src.PurchasingCustomer.CustomerID ?? 0))
                .ForMember(dst => dst.StartDate, map => map.MapFrom(src => src.StartDate))
                .ForMember(dst => dst.PurchasedProductOfferingId, map => map.MapFrom(src => src.ProductOffering.Id));

            #endregion
        }

        /// <summary>
        /// Converts the object in CustomerProductAssignment to CustomerProductAssignmentDTO
        /// </summary>
        /// <param name="source">the object to be converted</param>
        /// <returns>the object in the CustomerProductAssignmentDTO</returns>
        public CustomerProductAssignmentDTO Convert(CustomerProductAssignment source)
        {
            return Mapper.Map<CustomerProductAssignmentDTO>(source);
        }
    }
}
