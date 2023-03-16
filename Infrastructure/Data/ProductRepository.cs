
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext context;
        public ProductRepository(StoreContext context)
        {
            this.context = context;

        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAysnc()
        {
           return await context.ProductBrands.ToListAsync();
        }

       public async Task<Product> GetProductByIdAsync(int id)
        {
            return await context.product
            .Include(p => p.ProductType)
            .Include(p => p.ProductType)
            .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IReadOnlyList<Product>> GetProductsAysnc()
        {
            return await context.product
            .Include(p => p.ProductType)
            .Include(p => p.ProductType)
            .ToListAsync();
        }


        public async Task<IReadOnlyList<ProductType>> GetProductTypesAysnc()
        {
            return await context.ProductTypes.ToListAsync();
        }
    }
}