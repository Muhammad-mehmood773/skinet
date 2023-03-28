using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithTypeAndBrandSpecification : BaseSpecification<Product>
    {
        public ProductWithTypeAndBrandSpecification(ProductSpecParams ProductParams)
            :base(x =>
                (string.IsNullOrEmpty(ProductParams.Search) || x.Name.ToLower()
                .Contains(ProductParams.Search)) &&
                (!ProductParams.BrandId.HasValue || x.ProductBrandId == ProductParams.BrandId) &&
                (!ProductParams.TypeId.HasValue || x.ProductTypeId == ProductParams.TypeId)
                 )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            // ApplyPaging((ProductParams.PageSize) * (ProductParams.PageIndex - 1),
            //     ProductParams.PageSize);
                

            if(!string.IsNullOrEmpty(ProductParams.Sort))
            {
                switch (ProductParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

        public ProductWithTypeAndBrandSpecification(int id) 
            : base(x => x.Id == id)
        {
             AddInclude(x => x.ProductType);
             AddInclude(x => x.ProductBrand);
        }
    }
}