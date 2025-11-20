using Catalog.API.Entities;

namespace Catalog.API.Repositories.Interface;

public interface ITypeRepository
{
    Task<IEnumerable<ProductType>> GetAllTypesAsync();
    Task<ProductType> GetTypeByIdAsync(string id);
}

