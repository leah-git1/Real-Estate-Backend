using Entities;

namespace Repository
{
    public interface IProductRepository
    {
        Task<Product> AddProduct(Product product);
        Task<bool> DeleteProduct(int id);
        Task<Product> GetProductById(int id);
        Task<(List<Product>, int total)> GetProducts(int?[] categoryIds, string? city, decimal? minPrice, decimal? maxPrice, int? rooms, int? beds, int position = 1, int skip = 10);
        Task<List<Product>> GetProductsByOwnerId(int ownerId);
       
        Task<Product> UpdateProduct(int id, Product productToUpdate);
    }
}