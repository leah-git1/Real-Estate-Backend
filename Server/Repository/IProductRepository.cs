using Entities;

namespace Repository
{
    public interface IProductRepository
    {
        Task<Product> addProduct(Product product);
        Task<bool> deleteProduct(int id);
        Task<Product> getProductById(int id);
        Task<IEnumerable<Product>> getProducts(int?[] categoryIds, string? city, decimal? minPrice, decimal? maxPrice, int? rooms, int? beds);
        Task<IEnumerable<Product>> getProductsByOwnerId(int ownerId);
        Task<Product> updateProduct(int id, Product productToUpdate);
    }
}