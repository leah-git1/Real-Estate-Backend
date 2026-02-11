using DTOs;

namespace Services
{
    public interface IOrderService
    {
        Task<OrderDTO> AddOrder(OrderCreateDTO createOrder);
        Task<List<OrderDTO>> getAllOrders(int id);
        Task<OrderDTO> getOrderById(int id);
        Task<List<OrderDTO>> GetOrdersByUserId(int userId);
        Task<bool> OrderDelivered(int orderId);
        Task<OrderDTO> UpdateOrderStatus(int orderId, OrderStatusUpdateDTO dto);
    }
}