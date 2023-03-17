using API.DTOs;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturbDto>()
             .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
             .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
             .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductResolver>());
        }
    }
}