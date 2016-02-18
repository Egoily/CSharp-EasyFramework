using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using com.etak.core.model;
using com.etak.core.model.subscription;

namespace com.etak.core.operation.dtoConverters.customer
{
    /// <summary>
    /// Class converter between CrmCustomersPromotionInfo and CrmCustomersPromotionInfoDTO
    /// </summary>
    public class CrmCustomersPromotionDtoConverter : contract.ITypeConverter<CrmCustomersPromotionInfo, CrmCustomersPromotionInfoDTO>,
                                                     contract.ITypeConverter<CrmCustomersPromotionInfoDTO, CrmCustomersPromotionInfo>
    {
        /// <summary>
        /// Static constructor to initialize mappings
        /// </summary>
        static CrmCustomersPromotionDtoConverter()
        {
            Mapper.CreateMap<CrmCustomersPromotionInfo, CrmCustomersPromotionInfoDTO>();
            Mapper.CreateMap<CrmCustomersPromotionInfoDTO,CrmCustomersPromotionInfo>();
        }
        /// <summary>
        /// Convert from CrmCustomersPromotionInfo to CrmCustomersPromotionInfoDTO using basic Automapper conversor
        /// </summary>
        /// <param name="source">The Info object to be converted</param>
        /// <returns>The object converted</returns>
        public CrmCustomersPromotionInfoDTO Convert(CrmCustomersPromotionInfo source)
        {
            return Mapper.Map<CrmCustomersPromotionInfoDTO>(source);
        }

        /// <summary>
        /// Convert from CrmCustomersPromotionInfoDTO to CrmCustomersPromotionInfo using basic Automapper conversor
        /// </summary>
        /// <param name="source">The object to be converted</param>
        /// <returns>The object converted</returns>
        public CrmCustomersPromotionInfo Convert(CrmCustomersPromotionInfoDTO source)
        {
            return Mapper.Map<CrmCustomersPromotionInfo>(source);
        }
    }
}
