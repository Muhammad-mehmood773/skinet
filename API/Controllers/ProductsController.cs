using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using API.Errors;
using API.Helpers;

namespace API.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> ProductRepo;
        private readonly IGenericRepository<ProductBrand> ProductBrandRepo;
        private readonly IGenericRepository<ProductType> ProductTypeRepo;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> ProductRepo,
         IGenericRepository<ProductBrand> ProductBrandRepo,
         IGenericRepository<ProductType> ProductTypeRepo, IMapper  mapper)
        {
            this.ProductTypeRepo = ProductTypeRepo;
            this.mapper = mapper;
            this.ProductBrandRepo = ProductBrandRepo;
            this.ProductRepo = ProductRepo;
 
        }

        [HttpGet]
        public async Task <ActionResult<Paginations<ProductToReturbDto>>> GetProducts(
            [FromQuery]ProductSpecParams ProductParams)
        {
            var spec = new ProductWithTypeAndBrandSpecification(ProductParams);

            var CountSpec = new ProductWithFiltersForCountSpecification(ProductParams);

            var totalItems = await ProductRepo.CountAsync(CountSpec);
            
            var products = await ProductRepo.ListAsync(spec);

            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturbDto>>(products);

            return Ok(new Paginations<ProductToReturbDto>(ProductParams.PageIndex, ProductParams.PageSize, totalItems, data));
            
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturbDto>> GetProduct(int id)
        {
             var spec = new ProductWithTypeAndBrandSpecification(id);

             var product = await ProductRepo.GetEntityWithSpec(spec);

             if(product == null) return NotFound(new ApiResponse(404));

             return mapper.Map<Product, ProductToReturbDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await ProductBrandRepo.ListAllAsync());
        }
        
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await ProductTypeRepo.ListAllAsync());
        }

    }
}