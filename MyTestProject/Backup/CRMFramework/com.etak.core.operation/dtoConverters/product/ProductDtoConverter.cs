using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.inventory;
using com.etak.core.model.provisioning;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.contract;

namespace com.etak.core.operation.dtoConverters.product
{
    /// <summary>
    /// Conversor class to convert all the objects related with Product entity
    /// </summary>
    public class ProductDtoConverter : 
        ITypeConverter<Product, ProductDto>, 
        ITypeConverter<ProductDto, Product>
        //ITypeConverter<PhysicalProduct, PhysicalProductSpecificationDto>
    {

        static ProductDtoConverter()
        {
            AutoMapper.Mapper.CreateMap<Product, ProductDto>()
                .ForMember(dst => dst.CarrierId, map => map.MapFrom(src => src.Carrier.Id))
                .ForMember(dst => dst.Descriptions, map => map.MapFrom(src => src.Description.ToDto()))
                .ForMember(dst => dst.Names, map => map.MapFrom(src => src.Names.ToDto()))
                .ForMember(dst => dst.ProductType, map => map.MapFrom(src => src.Type.Description))
                .Include<PhysicalProduct, PhysicalProductSpecificationDto>();

            
            AutoMapper.Mapper.CreateMap<PhysicalProduct, PhysicalProductSpecificationDto>()
                .ForMember(dst => dst.BackCamera, map => map.MapFrom(src => src.PhysicalResourceSpecification.BackCamera.ToDto()))
                .ForMember(dst => dst.Brands, map => map.MapFrom(src => src.PhysicalResourceSpecification.Brand.ToDto()))
                .ForMember(dst => dst.Colors, map => map.MapFrom(src => src.PhysicalResourceSpecification.Color.ToDto()))
                .ForMember(dst => dst.SpecificationDescriptions, map => map.MapFrom(src => src.PhysicalResourceSpecification.Description.ToDto()))
                .ForMember(dst => dst.FrontCamera, map => map.MapFrom(src => src.PhysicalResourceSpecification.FrontCamera.ToDto()))
                .ForMember(dst => dst.SpecificationId, map => map.MapFrom(src => src.PhysicalResourceSpecification.Id))
                .ForMember(dst => dst.ImageUrl, map => map.MapFrom(src => src.PhysicalResourceSpecification.ImageUrl))
                .ForMember(dst => dst.ModelNumber, map => map.MapFrom(src => src.PhysicalResourceSpecification.ModelNumber))
                .ForMember(dst => dst.SpecificationNames, map => map.MapFrom(src => src.PhysicalResourceSpecification.BackCamera.ToDto()))
                .ForMember(dst => dst.OperationSystems, map => map.MapFrom(src => src.PhysicalResourceSpecification.OperationSystem.ToDto()))
                .ForMember(dst => dst.SKU, map => map.MapFrom(src => src.PhysicalResourceSpecification.SKU))
                .ForMember(dst => dst.Storages, map => map.MapFrom(src => src.PhysicalResourceSpecification.Storage.ToDto()));
            
            AutoMapper.Mapper.CreateMap<ProductDto, Product>()
                .ForMember(dst => dst.Id, map => map.MapFrom(src => src.Id))
                .ForMember(dst => dst.Carrier, map => map.MapFrom(src => new Carrier() {Id = src.CarrierId.HasValue ? src.CarrierId.Value : 0}))
                .ForMember(dst => dst.Type, map => map.MapFrom(src => new ProductType(){Description = src.ProductType}))
                .ForMember(dst => dst.Names, map => map.MapFrom(src => !src.Names.Any() ? new MultiLingualDescription() : new MultiLingualDescription(){Texts = new List<LanguageSpecificText>(){new LanguageSpecificText(){Text = src.Names.FirstOrDefault().Text}}}))
                .ForMember(dst => dst.Description, map => map.MapFrom(src => !src.Descriptions.Any() ? new MultiLingualDescription() : new MultiLingualDescription(){Texts = new List<LanguageSpecificText>(){new LanguageSpecificText(){Text = src.Descriptions.FirstOrDefault().Text}}}));
        }

        //public PhysicalProductSpecificationDto Convert(PhysicalProduct source)
        //{
            
        //}
        /// <summary>
        /// Convert from a ProductDto to a Product entity with the most basic information
        /// </summary>
        /// <param name="source">ProductDto to be converted</param>
        /// <returns>Product entity with basic information filled</returns>
        public Product Convert(ProductDto source)
        {
            return AutoMapper.Mapper.Map<Product>(source);
        }

        /// <summary>
        /// Convert from Product to ProductDto object, including inherited type PhysicalProduct
        /// </summary>
        /// <param name="source">Product entity to be converted</param>
        /// <returns>ProductDto object filled</returns>
        public ProductDto Convert(Product source)
        {
            return AutoMapper.Mapper.Map<ProductDto>(source);
        }
    }
}
