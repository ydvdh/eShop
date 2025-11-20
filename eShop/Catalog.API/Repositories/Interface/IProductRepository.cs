using Catalog.API.Entities;
using Catalog.API.Specification;

namespace Catalog.API.Repositories.Interface;

public interface IProductRepository
{
    Task<Pagination<Product>> GetProducts(CatalogSpecParams specParams);
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product> GetProductAsync(string id);
    Task<IEnumerable<Product>> GetProductByNameAsync(string name);
    Task<IEnumerable<Product>> GetProductsByBrand(string name);

    Task<Product> CreateProductAsync(Product product);
    Task<bool> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(string id);

    Task<ProductBrand> GetBrandByIdAsync(string brandId);
    Task<ProductType> GetTypeByIdAsync(string typeId);
}

