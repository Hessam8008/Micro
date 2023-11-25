using Catalog.Api.Data;
using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ICatalogContext _context;

    public ProductRepository(ICatalogContext catalogContext)
    {
        _context = catalogContext;
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        var cursor = await _context.Products.FindAsync(p => true).ConfigureAwait(false);
        return cursor.ToList();
    }

    public async Task<Product> GetProductById(string id)
    {
        var cursor = await
            _context.Products.FindAsync(p => p.Id == id).ConfigureAwait(false);
        return cursor.FirstOrDefault();
    }

    public async Task<Product> GetProductByName(string name)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Name, name);
        var cursor = await
            _context.Products.FindAsync(filter).ConfigureAwait(false);
        return cursor.FirstOrDefault();
    }

    public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Category, category);
        var cursor = await
            _context.Products.FindAsync(filter).ConfigureAwait(false);
        return cursor.ToList();
    }

    public Task CreateProduct(Product product)
    {
        return _context.Products.InsertOneAsync(product);
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
        var result = await _context.Products.ReplaceOneAsync(filter, product);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
        var result = await _context.Products.DeleteOneAsync(filter);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}