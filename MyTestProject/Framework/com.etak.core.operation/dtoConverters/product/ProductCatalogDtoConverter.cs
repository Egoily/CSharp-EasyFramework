using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.operation.dtoConverters.product
{
    /// <summary>
    /// Class to convert a Product Object to a ProductCatalogDTO
    /// </summary>
    public class ProductCatalogDtoConverter : contract.ITypeConverter<Product, ProductCatalogDTO>, contract.ITypeConverter<ProductCatalogDTO, Product>
    {
        /// <summary>
        /// Converts a Product to a ProductCatalogDTO
        /// </summary>
        /// <param name="product">the source product to convert</param>
        /// <returns>the converterd ProductCatalogDTO</returns>
        public ProductCatalogDTO Convert(Product product)
        {
            ProductCatalogDTO productCatalogDto = new ProductCatalogDTO();

            productCatalogDto.Id = product.Id;
            productCatalogDto.Names = (from e in product.Names.Texts
                                       select new TextualDescription()
                                       {
                                           LanguageCode = e.Language,
                                           Text = e.Text,
                                       }).ToList();

            productCatalogDto.Descriptions = (from e in product.Description.Texts
                                              select new TextualDescription()
                                              {
                                                  LanguageCode = e.Language,
                                                  Text = e.Text,
                                              }).ToList();
            productCatalogDto.ChildProducts = product.ChildProducts.Select(x => (Int64)x.Id).ToList();
            productCatalogDto.ParentProducts = product.ParentProducts.Select(x => (Int64)x.Id).ToList();
            productCatalogDto.PurchaseOptions = product.ChargingOptions.Select(x => new ProductPurchaseChargingOptionDtoConverter().Convert(x)).ToList();

            return productCatalogDto;
        }

        /// <summary>
        /// Convert a ProductCatalogDTO into a Product
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public Product Convert(ProductCatalogDTO source)
        {
            Product product = new Product();
            product.Id = (Int32)source.Id;

            #region Names
            var names = source.Names == null ? null : source.Names.FirstOrDefault();
            if (names != null)
                product.Names = new MultiLingualDescription()
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
                product.Description = new MultiLingualDescription()
                {
                    Texts =
                        new List<LanguageSpecificText>()
                        {
                            new LanguageSpecificText() {Text = description.Text, Language = description.LanguageCode}
                        }
                }; 
            #endregion
            
            product.ChargingOptions = source.PurchaseOptions.Select(x => x.ToCore()).ToList();
            product.ChildProducts = source.ChildProducts.Select(x => new Product() {Id = (Int32) x}).ToList();
            product.ParentProducts = source.ParentProducts.Select(x => new Product() {Id = (Int32) x}).ToList();
            return product;
        }
    }
}
