
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository 
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAysnc();
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAysnc();
        Task<IReadOnlyList<ProductType>> GetProductTypesAysnc();

    }
}