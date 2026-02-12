using DTOs;

namespace Services
{
    public interface IProductService
    {
        Task<ProductDetailsDTO> addProduct(ProductCreateDTO productCreateDto);
        Task<bool> deleteProduct(int id);
        Task<ProductDetailsDTO> getProductById(int id);
        Task<IEnumerable<ProductSummaryDTO>> getProducts(int?[] categoryIds, string? city, decimal? minPrice, decimal? maxPrice, int? rooms, int? beds);
        Task<IEnumerable<ProductSummaryDTO>> getProductsByOwnerId(int ownerId);
        Task<ProductDetailsDTO> updateProduct(int id, ProductUpdateDTO productUpdateDto);
    }
}