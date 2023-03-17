using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class ProductResolver : IValueResolver<Product, ProductToReturbDto, string>
    {
        private readonly IConfiguration config;

        public ProductResolver(IConfiguration config)
        {
            this.config = config;
            //  _config = config;
        }

        public string Resolve(Product source, ProductToReturbDto destination, string destMember,
          ResolutionContext context)
        {
           if(!string.IsNullOrEmpty(source.PictureUrl))
           { 
             return  config["ApiUrl"] + source.PictureUrl;
           }

            return null;
        }
    }
}