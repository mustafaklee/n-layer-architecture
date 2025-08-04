﻿using App.Repositories.Products;
using AutoMapper;

namespace App.Services.Products.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDto,Product>();
            CreateMap<Product, ProductDto>();
        }
    }
}
