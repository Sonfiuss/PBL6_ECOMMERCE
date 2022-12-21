using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.ModelDtos;

namespace Website_Ecommerce.API.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // source -> target
            CreateMap<ProductDetailDto, ProductDetail>();
            CreateMap<ProductDetail, ProductDetailDto>();

            CreateMap<ProductImageDto, ProductImage>();
            CreateMap<ProductImage, ProductImageDto>();
            CreateMap<CartDto, Cart>();
            CreateMap<VoucherOrderDto, VoucherOrder>();
            CreateMap<VoucherShopDto, VoucherProduct>();
            CreateMap<VoucherProduct, VoucherShopDto>();
            CreateMap<User, ShipperDto>();

            CreateMap<User, ProfileDto>();
            CreateMap<ProfileDto, User>();

            CreateMap<ShopDto, Shop>();

            


        }
    }
}