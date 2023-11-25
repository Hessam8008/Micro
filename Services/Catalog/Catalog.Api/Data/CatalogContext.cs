using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data;

public class CatalogContext : ICatalogContext
{
    public CatalogContext(IConfiguration Configuration)
    {
        var connectionString = Configuration.GetValue<string>("DatabaseSettings:ConnectionString");
        var databaseName = Configuration.GetValue<string>("DatabaseSettings:DatabaseName");
        var collectionName = Configuration.GetValue<string>("DatabaseSettings:CollectionName");
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        Products = database.GetCollection<Product>(collectionName);
    }

    public IMongoCollection<Product> Products { get; }
}