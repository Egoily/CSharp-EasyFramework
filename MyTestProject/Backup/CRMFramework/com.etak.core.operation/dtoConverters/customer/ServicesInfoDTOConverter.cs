using com.etak.core.model;
using com.etak.core.model.subscription;
using com.etak.core.operation.contract;

namespace com.etak.core.operation.dtoConverters.customer
{
    /// <summary>
    /// Converter for entity ServicesInfo to ServicesInfoDTO
    /// </summary>
    class ServicesInfoDTOConverter : ITypeConverter<ServicesInfo, ServicesInfoDTO>
    {
        /// <summary>
        /// static constructor initializing the mappings
        /// </summary>
        static ServicesInfoDTOConverter()
        {
            AutoMapper.Mapper.CreateMap<ServicesInfo, ServicesInfoDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.ServiceID))
                .ForMember(x => x.ProductInfoId, opt => opt.MapFrom(x => x.ProductInfo == null ? 0 : x.ProductInfo.ProductID))
                .ForMember(x => x.BundleDefinitionId, opt => opt.MapFrom(x => x.BundleDefinition == null ? 0 : x.BundleDefinition.BundleID))
                .ForMember(x => x.CustomerId, opt => opt.MapFrom(x => x.CustomerInfo == null ? 0 : x.CustomerInfo.CustomerID))

                .ForMember(x => x.InvoiceTemplateId, opt => opt.MapFrom(x => x.InvoiceTemplateID));



        }

        /// <summary>
        /// Converts the ServicesInfo into a ServicesInfoDTO
        /// </summary>
        /// <param name="source">the ServicesInfo to convert</param>
        /// <returns>the object converted in ServicesInfoDTO form</returns>
        public ServicesInfoDTO Convert(ServicesInfo source)
        {
           return AutoMapper.Mapper.Map<ServicesInfoDTO>(source);
        }
    }
}
