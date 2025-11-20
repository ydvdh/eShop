using Catalog.API.Entities;

namespace Catalog.API.Repositories.Interface;

public interface IBrandRepository
{
    Task<IEnumerable<ProductBrand>> GetAllBrandsAsync();
    Task<ProductBrand> GetBrandByIdAsync(string id);
}

