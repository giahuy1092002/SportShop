using AutoMapper;
using Data.Entities;
using Data.Entities.OrderAggregate;
using Data.Model;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dst => dst.Skus, opt => opt.MapFrom(src => src.Skus));
            CreateMap<CreateProductModel, Product>();
            CreateMap<ProductSKU, ProductSKUDto>()
                .ForMember(dst=>dst.Color,opt=>opt.MapFrom(src=>src.Color.Value))
                .ForMember(dst => dst.Size, opt => opt.MapFrom(src => src.Size.Value))
                .ForMember(dst => dst.SizeId, opt => opt.MapFrom(src => src.Size.Id))
                ;
            CreateMap<ProductSKU, ProductSKUOptionDto>()
                .ForMember(dst => dst.Size, opt => opt.MapFrom(src => src.Size.Value))
                ;
            CreateMap<CreateProductModel, Product>();
            CreateMap<Image, ImageDto>();
            CreateMap<Image, ImageColorDto>();
            CreateMap<CartItem, CartItemDto>()
               .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dst => dst.ProductSKUId, opt => opt.MapFrom(src => src.ProductSKU.Id))
               .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.ProductSKU.Product.Name))
               .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.ProductSKU.Price))
               .ForMember(dst => dst.PictureUrl, opt => opt
               .MapFrom(src => src.ProductSKU.Product.Images.FirstOrDefault(i => i.Type == "Anh chinh"&&i.ColorId==src.ProductSKU.ColorId).PictureUrl))
               .ForMember(dst => dst.Color, opt => opt.MapFrom(src => src.ProductSKU.Color.Name))
               .ForMember(dst => dst.ColorId, opt => opt.MapFrom(src => src.ProductSKU.ColorId))
               .ForMember(dst => dst.Size, opt => opt.MapFrom(src => src.ProductSKU.Size.Value))
               ;
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.ProductSKU.Product.Name))
                .ForMember(dst => dst.PictureUrl, opt => opt
                .MapFrom(src => src.ProductSKU.Product.Images.FirstOrDefault(i => i.Type == "Anh chinh" && i.ColorId == src.ProductSKU.ColorId).PictureUrl))
                .ForMember(dst => dst.Color, opt => opt.MapFrom(src => src.ProductSKU.Color.Name))
                .ForMember(dst => dst.Size, opt => opt.MapFrom(src => src.ProductSKU.Size.Value))
                ;
            CreateMap<Order, OrderDto>()
                .ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.OrderStatus.ToString()));


            CreateMap<Cart, CartDto>();
            CreateMap<CreateAddressModel, UserAddress>();
            CreateMap<UserAddress,UserAddressDto>();
        }
    }
}
