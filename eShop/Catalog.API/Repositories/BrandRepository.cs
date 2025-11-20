using Catalog.API.Entities;
using Catalog.API.Repositories.Interface;
using Catalog.API.Settings;
using MongoDB.Driver;

namespace Catalog.API.Repositories;

public class BrandRepository : IBrandRepository
{
    public readonly IMongoCollection<ProductBrand> _brands;
    public BrandRepository(ICatalogDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);

        _brands = database.GetCollection<ProductBrand>(settings.BrandCollectionName);

    }
    public async Task<IEnumerable<ProductBrand>> GetAllBrandsAsync()
    {
        return await _brands.Find(_=>true).ToListAsync();
    }

    public async Task<ProductBrand> GetBrandByIdAsync(string id)
    {
        return await _brands.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
}

