using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using AutoMapper;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.operation.dtoConverters.product
{
    /// <summary>
    /// Converter class for types ProductChargeOption to ProductPurchaseChargingOptionDTO
    /// </summary>
    public class ProductPurchaseChargingOptionDtoConverter : contract.ITypeConverter<ProductChargeOption, ProductPurchaseChargingOptionDTO>, contract.ITypeConverter<ProductPurchaseChargingOptionDTO, ProductChargeOption>
    {
        /// <summary>
        /// Static constructor of the class
        /// </summary>
        static ProductPurchaseChargingOptionDtoConverter()
        {
            Mapper.CreateMap<ProductPurchaseChargingOptionDTO, ProductChargeOption>()
                .ForMember(src => src.StartDate, map => map.MapFrom(dst => dst.EffectiveDate))
                .ForMember(src => src.EndDate, map => map.MapFrom(dst => dst.ExpirationDate))
                .ForMember(src => src.Description, map => map.Ignore());
        }

        /// <summary>
        /// Converts a ProductChargeOption to a ProductPurchaseChargingOptionDTO
        /// </summary>
        /// <param name="source">the source ProductChargeOption to convert</param>
        /// <returns>the return ProductPurchaseChargingOptionDTO</returns>
        public ProductPurchaseChargingOptionDTO Convert(ProductChargeOption source)
        {
            ProductPurchaseChargingOptionDTO productPurchaseChargeOpDTO = new ProductPurchaseChargingOptionDTO()
            {
                Id = source.Id,
                Description = (from e in source.Name.Texts
                               select new TextualDescription()
                               {
                                   LanguageCode = e.Language,
                                   Text = e.Text,
                               }).ToList(),
                EffectiveDate = source.StartDate,
                ExpirationDate = source.EndDate.Value,
            };
            productPurchaseChargeOpDTO.Charges = source.Charges.Select(x => new ChargeCatalogDtoConverter().Convert(x)).ToList();

            return productPurchaseChargeOpDTO;
        }

        /// <summary>
        /// Converts a ProductPurchaseChargingOptionDTO into a ProductChargeOption
        /// </summary>
        /// <param name="source">the source object to be converted</param>
        /// <returns>the converted object</returns>
        public ProductChargeOption Convert(ProductPurchaseChargingOptionDTO source)
        {

            ProductChargeOption productChargeOption = Mapper.Map<ProductChargeOption>(source);

            TextualDescription description = source.Description == null ? null : source.Description.FirstOrDefault();
            if (description != null)
            {
                productChargeOption.Description = new MultiLingualDescription()
                {
                    Texts = new List<LanguageSpecificText>()
                    {
                        new LanguageSpecificText()
                        {
                            Language = description.LanguageCode,
                            Text = description.Text,
                        }
                    }
                };
            }

            return productChargeOption;
        }
    }
}
