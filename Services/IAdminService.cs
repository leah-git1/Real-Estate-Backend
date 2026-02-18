using DTOs;

namespace Services
{
    public interface IAdminService
    {
        Task<List<UserProfileDTO>> GetAllUsers();
        Task<List<ProductDetailsDTO>> GetAllProducts();
        Task<List<OrderAdminDTO>> GetAllOrders();
        Task<bool> DeleteUser(int id);
        Task<bool> DeleteProduct(int id);
        Task<bool> DeleteOrder(int id);
        Task<AdminStatisticsDTO> GetStatistics();
    }
}
