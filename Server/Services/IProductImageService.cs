using DTOs;

namespace Services
{
    public interface IProductImageService
    {
        Task<ProductImageDTO> AddProductImage(ProductImageCreateDTO createImage);
        Task<bool> DeleteImage(int id);
        Task<ProductImageDTO> getProductImageById(int id);
        Task<List<ProductImageDTO>> GetProductImagesByProductId(int productId);
        Task<ProductImageDTO> UpdateProductImage(int imageId, ProductImageUpdateDTO updateImage);
    }
}