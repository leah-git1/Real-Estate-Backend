using AutoMapper;
using DTOs;
using Entities;
using Repository;

namespace Services
{
    public class AdminService : IAdminService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IUsersServices _usersServices;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public AdminService(IUsersRepository usersRepository, IUsersServices usersServices, IProductRepository productRepository, IOrderRepository orderRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _usersServices = usersServices;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<UserProfileDTO>> GetAllUsers()
        {
            List<User> users = await _usersRepository.GetAllUsers();
            return _mapper.Map<List<User>, List<UserProfileDTO>>(users);
        }

        public async Task<List<ProductDetailsDTO>> GetAllProducts()
        {
            List<Product> products = await _productRepository.GetAllProductsForAdmin();
            return _mapper.Map<List<Product>, List<ProductDetailsDTO>>(products);
        }

        public async Task<List<OrderAdminDTO>> GetAllOrders()
        {
            List<Order> orders = await _orderRepository.GetAllOrders();
            return orders.Select(o => new OrderAdminDTO(
                o.OrderId,
                o.UserId,
                o.User?.FullName ?? "לא ידוע",
                o.OrderDate,
                o.TotalAmount,
                o.Status ?? "לא ידוע"
            )).ToList();
        }

        public async Task<bool> DeleteUser(int id)
        {
            User user = await _usersRepository.GetUserById(id);
            if (user == null)
                return false;
            await _usersServices.DeleteUser(id);
            return true;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            return await _productRepository.DeleteProduct(id);
        }

        public async Task<bool> DeleteOrder(int id)
        {
            Order order = await _orderRepository.GetOrderById(id);
            if (order == null)
                return false;
            await _orderRepository.DeleteOrder(id);
            return true;
        }

        public async Task<AdminStatisticsDTO> GetStatistics()
        {
            List<User> users = await _usersRepository.GetAllUsers();
            List<Product> products = await _productRepository.GetAllProductsForAdmin();
            List<Order> orders = await _orderRepository.GetAllOrders();

            return new AdminStatisticsDTO(users.Count, products.Count, orders.Count);
        }
    }
}
