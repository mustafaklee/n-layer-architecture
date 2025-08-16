using AutoMapper;
using App.Application.Features.Products.Dto;
using App.Domain.Entities;
namespace App.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
        }
    }
}
