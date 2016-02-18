using AutoMapper;
using com.etak.core.model;
using com.etak.core.model.usage;

namespace com.etak.core.operation.dtoConverters.customer
{
    /// <summary>
    /// Converter class for UsageDetailRecord to UsageDetailDTO
    /// </summary>
    public class UsageDetailDtoConverter : contract.ITypeConverter<UsageDetailRecord, UsageDetailDTO>
    {
        /// <summary>
        /// Initializes the automap
        /// </summary>
        static UsageDetailDtoConverter()
        {
            #region UsageDetailRecord -> UsageDetailDTO
            Mapper.CreateMap<UsageDetailRecord, UsageDetailDTO>()
                    .ForMember(dst => dst.Amount, map => map.MapFrom(src => src.Amount1 + src.Amount2))
                    .ForMember(dst => dst.SubServiceTypeId, map => map.MapFrom(src => (UsagesSubTypes)src.Subservicetypeid));
            #endregion
        }

        /// <summary>
        /// Converts a UsageDetailRecord to a UsageDetailDTO
        /// </summary>
        /// <param name="source">the source object to convert</param>
        /// <returns>the converted object</returns>
        public UsageDetailDTO Convert(UsageDetailRecord source)
        {
            return Mapper.Map<UsageDetailDTO>(source);
        }
    }
}
