using System;
using AutoMapper;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.operation.dtoConverters.customer
{
    /// <summary>
    /// Converter for CustomerCharge to CustomerRecurringChargeDTO
    /// </summary>
    public class CustomerRecurringChargeDtoConverter : contract.ITypeConverter<CustomerCharge, CustomerRecurringChargeDTO>
    {
        static CustomerRecurringChargeDtoConverter()
        {
            #region CustomerCharge -> CustomerRecurringChargeDTO
            Mapper.CreateMap<CustomerCharge, CustomerRecurringChargeDTO>()
                .ForMember(dst => dst.ChargeId, map => map.MapFrom(src => (src.ChargeDefinition != null) ? (long)src.ChargeDefinition.Id : 0))
                    .ForMember(dst => dst.CreateTime, map => map.MapFrom(src => src.ChargingDate))
                    .ForMember(dst => dst.CustomerId, map => map.MapFrom(src => (long?)src.Customer.CustomerID ?? 0))
                    .ForMember(dst => dst.InvoiceId, map => map.MapFrom(src => (src.Invoice != null) ? src.Invoice.Id : 0))
                    .ForMember(dst => dst.ProductPurchaseId, map => map.MapFrom(src => (src.Product != null) ? (src.Product.PurchasedProduct != null) ? (long)src.Product.PurchasedProduct.Id : 0 : 0))
                    .ForMember(dst => dst.ReferenceCode, map => map.MapFrom(src => src.ExternalReferenceCode))
                    .ForMember(dst => dst.NextChargeDate, map => map.MapFrom(src => (src.Schedule != null) ? src.Schedule.NextChargeDate : (DateTime?)null))
                    .ForMember(dst => dst.NextPeriodNumber, map => map.MapFrom(src => (src.Schedule != null) ? src.Schedule.NextPeriodNumber : 0))
                    .ForMember(dst => dst.CurrentCycleNumber, map => map.MapFrom(src => (src.CycleNumber != null) ? src.CycleNumber.Value : 0));
            #endregion
        }

        /// <summary>
        /// Coverts a CustomerCharge to CustomerRecurringChargeDTO
        /// </summary>
        /// <param name="source">The CustomerCharge to convert</param>
        /// <returns>the CustomerRecurringChargeDTO converter</returns>
        public CustomerRecurringChargeDTO Convert(CustomerCharge source)
        {
            return Mapper.Map<CustomerRecurringChargeDTO>(source);
        }
    }
}
