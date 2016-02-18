using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using com.etak.core.operation.contract.exceptions;

namespace com.etak.core.operation.dtoConverters.product
{
    /// <summary>
    /// Dto converter for PurchaseProductOfferingSelection and PurchaseProductOfferingSelectionDTO
    /// </summary>
    public class PurchaseProductOfferingSelectionDtoConverter : contract.ITypeConverter<PurchaseProductOfferingSelection, PurchaseProductOfferingSelectionDTO>, contract.ITypeConverter<PurchaseProductOfferingSelectionDTO, PurchaseProductOfferingSelection>
    {
        /// <summary>
        /// Convert from PurchaseProductOfferingSelectionDTO to PurchaseProductOfferingSelection
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public PurchaseProductOfferingSelection Convert(PurchaseProductOfferingSelectionDTO source)
        {
            var productOfferingSelection = new PurchaseProductOfferingSelection()
            {
                PurchaseOptions = new List<PurchaseProductOfferingSelection>()
            };

            productOfferingSelection.PurchasedProductOffering = new ProductOffering()
            {
               Id = source.ProductOfferingId,
               ChargingOptions = new List<ProductChargeOption>()
               {
                   new ProductChargeOption()
                   {
                       Id = source.ProductOfferingChargeOptionId
                   }
               }
            };

            productOfferingSelection.PurchaseOptions = GetOptions(source.Options);

            return productOfferingSelection;
        }

        /// <summary>
        /// Recursive function to return all the options inside the Offering Product
        /// </summary>
        /// <param name="option">A list of PurchaseProductOffering options</param>
        /// <returns></returns>
        private static List<PurchaseProductOfferingSelection> GetOptions(IEnumerable<PurchaseProductOfferingSelectionDTO> option)
        {
            var list = new List<PurchaseProductOfferingSelection>();

            if (option == null)
                return list;

            foreach (var subOption in option)
            {
                var offering = new PurchaseProductOfferingSelection()
                {
                    PurchasedProductOffering = new ProductOffering() {Id = subOption.ProductOfferingId},
                    PurchaseOptions = GetOptions(subOption.Options)
                };

                list.Add(offering);
                
            }

            return list;
        }

        /// <summary>
        /// Convertor 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public PurchaseProductOfferingSelectionDTO Convert(PurchaseProductOfferingSelection source)
        {
            throw new BusinessLogicErrorException("This mapping doesn't make sesnse", OperationErrorCodes.MappingNotAllowed);
        }
    }
}
