using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using com.etak.core.model;
using com.etak.core.model.subscription;

namespace com.etak.core.operation.dtoConverters.product
{
    /// <summary>
    /// Converter Class 
    /// </summary>
    public class RmPromotionPlanDetailDtoConverter : contract.ITypeConverter<RmPromotionPlanDetailInfo, RmPromotionPlanDetailInfoDTO>,
                                                  contract.ITypeConverter<RmPromotionPlanDetailInfoDTO, RmPromotionPlanDetailInfo>
    {
        /// <summary>
        /// Initialize all the mappings
        /// </summary>
        static RmPromotionPlanDetailDtoConverter()
        {
            Mapper.CreateMap<RmPromotionPlanDetailInfo, RmPromotionPlanDetailInfoDTO>();
            Mapper.CreateMap<RmPromotionPlanDetailInfoDTO, RmPromotionPlanDetailInfo>();
        }

        /// <summary>
        /// Convert from RmPromotionPlanDetailInfo to Dto using Automapper conversor
        /// </summary>
        /// <param name="source">The Core object to be converted</param>
        /// <returns>The Dto object to be converted</returns>
        public RmPromotionPlanDetailInfoDTO Convert(RmPromotionPlanDetailInfo source)
        {
            return Mapper.Map<RmPromotionPlanDetailInfoDTO>(source);
        }
        /// <summary>
        /// Convert from RmPromotionPlanDetailInfoDTO to Core using Automapper conversor
        /// </summary>
        /// <param name="source">The Dto object to be converted</param>
        /// <returns>The Core object obtained</returns>
        public RmPromotionPlanDetailInfo Convert(RmPromotionPlanDetailInfoDTO source)
        {
            return Mapper.Map<RmPromotionPlanDetailInfo>(source);
        }
    }
}
