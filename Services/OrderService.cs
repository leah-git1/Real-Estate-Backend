using AutoMapper;
using DTOs;
using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        IOrderRepository _iOrderRepository;
        IMapper _mapper;
        public OrderService(IOrderRepository iOrderRepository, IMapper mapper)
        {
            this._iOrderRepository = iOrderRepository;
            this._mapper = mapper;
        }

        public async Task<OrderDTO> GetOrderById(int id)
        {
            Order order = await _iOrderRepository.GetOrderById(id);
            OrderDTO orderDTO = _mapper.Map<Order, OrderDTO>(order);
            return orderDTO;
        }

        public async Task<List<OrderHistoryDTO>> GetOrdersByUserId(int userId)
        {
            List<Order> orders = await _iOrderRepository.GetOrdersByUserId(userId);
            return _mapper.Map<List<Order>, List<OrderHistoryDTO>>(orders);
        }

        public async Task<List<OrderHistoryAdminDTO>> GetAllOrders()
        {
            List<Order> orders = await _iOrderRepository.GetAllOrders();
            return _mapper.Map<List<Order>, List<OrderHistoryAdminDTO>>(orders);
        }

        public async Task<OrderDTO> AddOrder(OrderCreateDTO createOrder)
        {
            Order order = _mapper.Map<OrderCreateDTO, Order>(createOrder);
            Order orderTbl = await _iOrderRepository.AddOrder(order);
            return _mapper.Map<Order, OrderDTO>(orderTbl);
        }

        public async Task<OrderDTO> UpdateOrderStatus(int orderId, OrderStatusUpdateDTO dto)
        {
            string status = dto.Status;
            Order order = await _iOrderRepository.UpdateOrderStatus(orderId, status);
            return _mapper.Map<Order, OrderDTO>(order);
        }

        public async Task<bool> OrderDelivered(int orderId)
        {
            return await _iOrderRepository.OrderDelivered(orderId);
        }
    }
}
