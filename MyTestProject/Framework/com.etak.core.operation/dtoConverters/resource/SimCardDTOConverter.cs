using AutoMapper;
using com.etak.core.model;
using com.etak.core.model.resource;

namespace com.etak.core.operation.dtoConverters.resource
{
    /// <summary>
    /// Converter for SimCardInfo object
    /// </summary>
    public class SimCardDtoConverter : contract.ITypeConverter<SIMCardInfo, SimCardDTO>, contract.ITypeConverter<SimCardDTO, SIMCardInfo>
    {

        /// <summary>
        /// Constructor with all the mappings defined
        /// </summary>
        static SimCardDtoConverter()
        {
            Mapper.CreateMap<SIMCardInfo, SimCardDTO>()
                .ForMember(dst => dst.DealerID, map => map.MapFrom(src => src.Dealer == null ? null : src.Dealer.DealerID));
            Mapper.CreateMap<SimCardDTO, SIMCardInfo>();
        }

        /// <summary>
        /// Convert from SimcardInfo to SimCardDto
        /// </summary>
        /// <param name="source">SimcardInfo object to be converted</param>
        /// <returns>SimcardDTO converted</returns>
        public SimCardDTO Convert(SIMCardInfo source)
        {
            return Mapper.Map<SimCardDTO>(source);
        }

        /// <summary>
        /// Convert from SimcardDto to SimcardInfo
        /// </summary>
        /// <param name="source">SimCardDto object to be converted</param>
        /// <returns>SimCardInfo converted</returns>
        public SIMCardInfo Convert(SimCardDTO source)
        {
            return Mapper.Map<SIMCardInfo>(source);
        }
    }
}
