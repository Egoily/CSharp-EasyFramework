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
    /// Class converter to convert RmPromotionPlanInfo to DTO
    /// </summary>
    public class RmPromotionPlanDtoConverter : contract.ITypeConverter<RmPromotionPlanInfo, RmPromotionPlanInfoDTO>, contract.ITypeConverter<RmPromotionPlanInfoDTO, RmPromotionPlanInfo>
    {
        /// <summary>
        /// Initialize mappings and objects
        /// </summary>
        static RmPromotionPlanDtoConverter()
        {
            Mapper.CreateMap<RmPromotionPlanInfo, RmPromotionPlanInfoDTO>();
            Mapper.CreateMap<RmPromotionPlanInfoDTO, RmPromotionPlanInfo>();
        }

        /// <summary>
        /// Return the DTO object corresponding to the given Info object using Automapper conversor
        /// </summary>
        /// <param name="source">The Core object ot be converted</param>
        /// <returns>The DTO object obtained</returns>
        public RmPromotionPlanInfoDTO Convert(RmPromotionPlanInfo source)
        {
            return Mapper.Map<RmPromotionPlanInfoDTO>(source);
        }
        /// <summary>
        /// Return the core object corresponding to the given DTO object using Automapper conversor
        /// </summary>
        /// <param name="source">The Dto object to be converted</param>
        /// <returns>The Core object obtained</returns>
        public RmPromotionPlanInfo Convert(RmPromotionPlanInfoDTO source)
        {
            return Mapper.Map<RmPromotionPlanInfo>(source);
        }
    }
}
