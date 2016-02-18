using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.operation.dtoConverters.product
{
    /// <summary>
    /// Dto Converter for PaymentInfo and PaymentDataDTO
    /// </summary>
    public class PaymentInfoDtoConverter : contract.ITypeConverter<PaymentInfo, PaymentDataDTO>, contract.ITypeConverter<PaymentDataDTO, PaymentInfo>
    {
        /// <summary>
        /// Static constructor to create the mappings between the two objects
        /// </summary>
        static PaymentInfoDtoConverter()
        {
            Mapper.CreateMap<PaymentDataDTO, PaymentInfo>()
                .ForMember(dst => dst.ExternalPayment, map => map.MapFrom(src => src.ExternalPaymentId))
                .ForMember(dst => dst.PaymentInfoText, map => map.MapFrom(src => src.PaymentInfo))
                .ForMember(dst => dst.PaymentMethod, map => map.MapFrom(src => src.PaymentMethodId));

            Mapper.CreateMap<PaymentInfo, PaymentDataDTO>()
                .ForMember(dst => dst.ExternalPaymentId, map => map.MapFrom(src => src.ExternalPayment))
                .ForMember(dst => dst.PaymentMethodId, map => map.MapFrom(src => src.PaymentMethod))
                .ForMember(dst => dst.PaymentInfo, map => map.MapFrom(src => src.PaymentInfoText));

        }

        /// <summary>
        /// Convert from PaymentInfo entity to PaymentDataDTO
        /// </summary>
        /// <param name="source">The entity to be converted</param>
        /// <returns>A Dto object that represents the given entity</returns>
        public PaymentDataDTO Convert(PaymentInfo source)
        {
            return Mapper.Map<PaymentDataDTO>(source);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public PaymentInfo Convert(PaymentDataDTO source)
        {
            return Mapper.Map<PaymentInfo>(source);
        }
    }
}
