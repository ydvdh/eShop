using Catalog.API.Data.Interface;
using Catalog.API.Entities;
using Catalog.API.Settings;
using MongoDB.Driver;

namespace Catalog.API.Data;

public class CatalogContext : ICatalogContext
{
    public CatalogContext(ICatalogDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);

        Products = database.GetCollection<Product>(settings.ProductCollectionName);
        Brands = database.GetCollection<ProductBrand>(settings.BrandCollectionName);
        Types = database.GetCollection<ProductType>(settings.TypeCollectionName);
    }
    public IMongoCollection<Product> Products { get; }
    public IMongoCollection<ProductBrand> Brands { get; }
    public IMongoCollection<ProductType> Types { get; }
}

