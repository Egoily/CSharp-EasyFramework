using AutoMapper;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.operation.dtoConverters.customer
{
    /// <summary>
    /// Converter between CustomerCharge Object and CustomerChargeDTO
    /// </summary>
    public class CustomerChargeDtoConverter : contract.ITypeConverter<CustomerCharge, CustomerChargeDTO>
    {
        /// <summary>
        /// Constructor with the initialization of the mappings
        /// </summary>
        static CustomerChargeDtoConverter()
        {
            #region CustomerCharge -> CustomerChargeDTO
            Mapper.CreateMap<CustomerCharge, CustomerChargeDTO>()
                    .ForMember(dst => dst.ChargeId, map => map.MapFrom(src => (src.ChargeDefinition != null) ? (long)src.ChargeDefinition.Id : 0))
                    .ForMember(dst => dst.CreateTime, map => map.MapFrom(src => src.ChargingDate))
                    .ForMember(dst => dst.CustomerId, map => map.MapFrom(src => (long?)src.Customer.CustomerID ?? 0))
                    .ForMember(dst => dst.InvoiceId, map => map.MapFrom(src => (src.Invoice != null) ? src.Invoice.Id : 0))
                    .ForMember(dst => dst.ProductPurchaseId, map => map.MapFrom(src => (src.Product != null) ? (src.Product.PurchasedProduct != null) ? (long?)src.Product.PurchasedProduct.Id : 0 : 0))
                    .ForMember(dst => dst.ReferenceCode, map => map.MapFrom(src => src.ExternalReferenceCode));
            #endregion
        }

        /// <summary>
        /// Convert an object of type CustomerCharge to type CustomerChargeDTO
        /// </summary>
        /// <param name="source">Source Object to be converted</param>
        /// <returns>Destination object</returns>
        public CustomerChargeDTO Convert(CustomerCharge source)
        {
            return Mapper.Map<CustomerChargeDTO>(source);
        }
    }
}
