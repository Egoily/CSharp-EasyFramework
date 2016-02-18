using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using com.etak.core.model;
using com.etak.core.model.inventory;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using com.etak.core.repository;
using com.etak.core.repository.crm.subscription.catalog;

namespace com.etak.core.operation.dtoConverters.product
{
    /// <summary>
    /// Class to convert a ProductOffering Object to a ProductCatalogDTO
    /// </summary>
    public class ProductCatalogDtoConverter : contract.ITypeConverter<ProductOffering, ProductCatalogDTO>, contract.ITypeConverter<ProductCatalogDTO, ProductOffering>
    {
        /// <summary>
        /// Converts a ProductOffering to a ProductCatalogDTO
        /// </summary>
        /// <param name="productOffering">the source product to convert</param>
        /// <returns>the converterd ProductCatalogDTO</returns>
        public ProductCatalogDTO Convert(ProductOffering productOffering)
        {
            ProductCatalogDTO productCatalogDto = new ProductCatalogDTO();

            #region Id, Names and Descriptions
            productCatalogDto.Id = productOffering.Id;
            productCatalogDto.Names = productOffering.Names != null ? productOffering.Names.ToDto() : null;
            productCatalogDto.Descriptions = productOffering.Description != null ? productOffering.Description.ToDto() : null;
            #endregion

            #region Product
            productCatalogDto.ProductDto = productOffering.OfferedProduct.ToDto();
            #endregion

            #region PurchaseOptions
            productCatalogDto.PurchaseOptions = productOffering.ChargingOptions.Select(x => new ProductPurchaseChargingOptionDtoConverter().Convert(x)).ToList();
            #endregion

            #region Options
            productCatalogDto.Options = new List<ProductOfferingOptionDTO>();

            if (productOffering.Options == null)
                return productCatalogDto;

            //Get all the groups
            var groups = productOffering.Options.Where(x => x is ProductOfferingGroupOption);

            foreach (var item in groups)
            {
                var group = item as ProductOfferingGroupOption;
                var groupDto = group.ToDto();
                productCatalogDto.Options.Add(groupDto);
            }

            //Get all the related specification
            var specificProductOffering = productOffering.Options.Where(x => x is ProductOfferingSpecificationOption);

            foreach (var subProductItem in specificProductOffering)
            {
                var subProduct = subProductItem as ProductOfferingSpecificationOption;
                var specifiedProductDto = subProduct.ToDto();
                productCatalogDto.Options.Add(specifiedProductDto);
            }
            

            #endregion
            
            return productCatalogDto;
        }



        /// <summary>
        /// Convert a ProductCatalogDTO into a Product
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public ProductOffering Convert(ProductCatalogDTO source)
        {
            var productOffering = new ProductOffering {Id = (Int32) source.Id};

            productOffering.OfferedProduct = source.ProductDto.ToCore();

            #region Names
            var names = source.Names == null ? null : source.Names.FirstOrDefault();
            if (names != null)
                productOffering.Names = new MultiLingualDescription()
                {
                    Texts =
                        new List<LanguageSpecificText>()
                        {
                            new LanguageSpecificText() {Text = names.Text, Language = names.LanguageCode}
                        }
                }; 
            #endregion

            #region Description
            var description = source.Descriptions == null ? null : source.Descriptions.FirstOrDefault();
            if (description != null)
                productOffering.Description = new MultiLingualDescription()
                {
                    Texts =
                        new List<LanguageSpecificText>()
                        {
                            new LanguageSpecificText() {Text = description.Text, Language = description.LanguageCode}
                        }
                }; 
            #endregion

            #region ChargeOptions
            productOffering.ChargingOptions = source.PurchaseOptions.Select(x => x.ToCore()).ToList();
            #endregion

            return productOffering;
        }
    }
}
