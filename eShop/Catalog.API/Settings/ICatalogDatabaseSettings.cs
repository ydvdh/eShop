namespace Catalog.API.Settings
{
    public interface ICatalogDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ProductCollectionName { get; set; }
        string BrandCollectionName { get; set; }
        string TypeCollectionName { get; set; }
    }
}
