using AutoMapper;
using com.etak.core.model;
using com.etak.core.model.resource;

namespace com.etak.core.operation.dtoConverters.resource
{
    /// <summary>
    /// Conversor for NumberInfo to MSISDNResourceDTOConverter
    /// </summary>
    public class MSISDNResourceDTOConverter : contract.ITypeConverter<NumberInfo, MSISDNResourceDTO>, contract.ITypeConverter<MSISDNResourceDTO, NumberInfo>
    {

        /// <summary>
        /// Constructor to set up the mappings
        /// </summary>
        static MSISDNResourceDTOConverter()
        {
            Mapper.CreateMap<MSISDNResourceDTO, NumberInfo>()
                .ForMember(dst => dst.Resource, map => map.MapFrom(src => src.MSISDN))
                .ForMember(dst => dst.CategoryID, map => map.MapFrom(src => src.Category));

            Mapper.CreateMap<NumberInfo, MSISDNResourceDTO>()
                .ForMember(dst => dst.MSISDN, map => map.MapFrom(src => src.Resource))
                .ForMember(dst => dst.Category, map => map.MapFrom(src => src.CategoryID));
        }

        /// <summary>
        /// Convert from MSISDNResourceDTO object to NumberInfo
        /// </summary>
        /// <param name="source">Object to be converted</param>
        /// <returns>Destination object</returns>
        public NumberInfo Convert(MSISDNResourceDTO source)
        {
            return Mapper.Map<NumberInfo>(source);
        }

        /// <summary>
        /// Convert from NumberInfo object to MSISDNResourceDTO
        /// </summary>
        /// <param name="source">Object to be converted</param>
        /// <returns>Destination object</returns>
        public MSISDNResourceDTO Convert(NumberInfo source)
        {
            return Mapper.Map<MSISDNResourceDTO>(source);
        }
    }
}
