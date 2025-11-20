using Catalog.API.Entities;
using Catalog.API.Repositories.Interface;
using Catalog.API.Settings;
using MongoDB.Driver;

namespace Catalog.API.Repositories;

public class TypeRepository : ITypeRepository
{
    public readonly IMongoCollection<ProductType> _types;
    public TypeRepository(ICatalogDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);

        _types = database.GetCollection<ProductType>(settings.TypeCollectionName);

    }
    public async Task<IEnumerable<ProductType>> GetAllTypesAsync()
    {
        return await _types.Find(_ => true).ToListAsync();
    }

    public async Task<ProductType> GetTypeByIdAsync(string id)
    {
        return await _types.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
}

