using AutoMapper;
using com.etak.core.model;
using com.etak.core.model.dto;

namespace com.etak.core.operation.dtoConverters.customer
{
    /// <summary>
    /// Class Converter Between AddressInfo and AddressDTO
    /// </summary>
    public class AddressDtoConverter : contract.ITypeConverter<AddressInfo, AddressDTO>, contract.ITypeConverter<AddressDTO, AddressInfo>
    {
        /// <summary>
        /// Initialize the object and mappings
        /// </summary>
        static AddressDtoConverter()
        {
            Mapper.CreateMap<AddressDTO, AddressInfo>()
                .ForMember(x => x.HouseExtention, opt => opt.MapFrom(y => y.HouseExtension));
            Mapper.CreateMap<AddressInfo, AddressDTO>()
                .ForMember(x => x.HouseExtension, opt => opt.MapFrom(y => y.HouseExtention));   
        }

        /// <summary>
        /// Convert from AddresInfo to AddressDTO
        /// </summary>
        /// <param name="source">AddresInfo to be converted</param>
        /// <returns>AddressDTO converted</returns>
        public AddressDTO Convert(AddressInfo source)
        {
            //return DtoMappings.Convert<AddressInfo, AddressDTO>(source);
            return Mapper.Map<AddressDTO>(source);
        }

        /// <summary>
        /// Convert from AddressDTO to AddressInfo
        /// </summary>
        /// <param name="source">AddressDTO to be converted</param>
        /// <returns>AddressInfo converted</returns>
        public AddressInfo Convert(AddressDTO source)
        {
            //return DtoMappings.Convert<AddressDTO, AddressInfo>(source);
            return Mapper.Map<AddressInfo>(source);
        }
    }
}
