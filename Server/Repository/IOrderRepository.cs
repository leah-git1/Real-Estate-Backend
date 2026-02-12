using Entities;

namespace Repository
{
    public interface IOrderRepository
    {
        Task<Order> AddOrder(Order order);
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int ind);
        Task<List<Order>> GetOrdersByUserId(int ind);
        Task<bool> OrderDelivered(int orderId);
        Task<Order> UpdateOrderStatus(int orderId, string status);
    }
}