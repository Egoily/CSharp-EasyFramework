using System;
using AutoMapper;
using com.etak.core.model;
using com.etak.core.model.dto;

namespace com.etak.core.operation.dtoConverters.customer
{
    /// <summary>
    /// Conversor for BankInfor entity to DTO
    /// </summary>
    public class BankDtoConverter : contract.ITypeConverter<BankInfo, BankInformationDTO>, contract.ITypeConverter<BankInformationDTO, BankInfo>
    {
        /// <summary>
        /// Initialize the Object and the mappings.
        /// </summary>
        static BankDtoConverter()
        {
            Mapper.CreateMap<BankInfo, BankInformationDTO>();
            Mapper.CreateMap<BankInformationDTO, BankInfo>()
                .ForMember(x => x.CreateDate, opt => opt.MapFrom(y => y.CreateDate ?? DateTime.Now));
        }

        /// <summary>
        /// Conversor from BankInfo to BankInformationDTO
        /// </summary>
        /// <param name="source">BankInfo object</param>
        /// <returns>BankInformationDTO object</returns>
        public BankInformationDTO Convert(BankInfo source)
        {
            return Mapper.Map<BankInformationDTO>(source);
        }

        /// <summary>
        /// Conversor from BankInformationDTO to BankInfo
        /// </summary>
        /// <param name="source">BankInformationDTO source</param>
        /// <returns>BankInfo object</returns>
        public BankInfo Convert(BankInformationDTO source)
        {
            return Mapper.Map<BankInfo>(source);
        }
    }
}
