﻿using AutoMapper;
using ProductShop.App.DTOs;
using ProductShop.App.ExportDtos;
using ProductShop.Models;

namespace ProductShop.App
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<ProductDto, Product>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, ExportCategoryDto>().ReverseMap();
        }
    }
}