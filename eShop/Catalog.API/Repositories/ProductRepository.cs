using Catalog.API.Entities;
using Catalog.API.Repositories.Interface;
using Catalog.API.Settings;
using Catalog.API.Specification;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.API.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _products;
    private readonly IMongoCollection<ProductBrand> _brands;
    private readonly IMongoCollection<ProductType> _types;

    public ProductRepository(ICatalogDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);

        _products = database.GetCollection<Product>(settings.ProductCollectionName);
        _brands = database.GetCollection<ProductBrand>(settings.BrandCollectionName);
        _types = database.GetCollection<ProductType>(settings.TypeCollectionName);
    }
    public async Task<Pagination<Product>> GetProducts(CatalogSpecParams specParams)
    {
        var builder = Builders<Product>.Filter;
        var filter = builder.Empty;
        if (!string.IsNullOrEmpty(specParams.Search))
        {
            filter &= builder.Where(p => p.Name.ToLower().Contains(specParams.Search.ToLower()));
        }
        if (!string.IsNullOrEmpty(specParams.BrandId))
        {
            filter &= builder.Eq(p => p.Brand.Id, specParams.BrandId);
        }
        if (!string.IsNullOrEmpty(specParams.TypeId))
        {
            filter &= builder.Eq(p => p.Type.Id, specParams.TypeId);
        }

        var totalItems = await _products.CountDocumentsAsync(filter);
        var data = await ApplyDataFilters(specParams, filter);

        return new Pagination<Product>(
            specParams.PageIndex,
            specParams.PageSize,
            (int)totalItems,
            data
        );
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _products.Find(prop => true).ToListAsync();
    }

    public async Task<Product> GetProductAsync(string id)
    {
        return await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<ProductBrand> GetBrandByIdAsync(string brandId)
    {
        return await _brands.Find(b => b.Id == brandId).FirstOrDefaultAsync();
    }

    public async Task<ProductType> GetTypeByIdAsync(string typeId)
    {
        return await _types.Find(t => t.Id == typeId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByBrand(string name)
    {
        return await _products.Find(p => p.Brand.Name.ToLower() == name.ToLower()).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
    {
        var filter = Builders<Product>.Filter.Regex(p => p.Name, new BsonRegularExpression($".*{name}.*", "i"));
        return await _products.Find(filter).ToListAsync();
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        await _products.InsertOneAsync(product);
        return product;
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        var updatedProduct = await _products.ReplaceOneAsync(p => p.Id == product.Id, product);
        return updatedProduct.IsAcknowledged && updatedProduct.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProductAsync(string id)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
        DeleteResult deleteResult = await _products.DeleteOneAsync(filter);

        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }

    private async Task<IReadOnlyCollection<Product>> ApplyDataFilters(CatalogSpecParams specParams, FilterDefinition<Product> filter)
    {
        var sortDefn = Builders<Product>.Sort.Ascending("Name");
        if (!string.IsNullOrEmpty(specParams.Sort))
        {
            sortDefn = specParams.Sort switch
            {
                "priceAsc" => Builders<Product>.Sort.Ascending(p => p.Price),
                "priceDesc" => Builders<Product>.Sort.Descending(p => p.Price),
                _ => Builders<Product>.Sort.Ascending(p => p.Name)
            };
        }
        return await _products.Find(filter).Sort(sortDefn).Skip(specParams.PageSize * (specParams.PageIndex - 1))
            .Limit(specParams.PageSize).ToListAsync();
    }
}

