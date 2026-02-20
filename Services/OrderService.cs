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
        private readonly IOrderRepository _iOrderRepository;
        private readonly IProductRepository _iProductRepository;
        private readonly IProductService _iProductService;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository iOrderRepository, IProductRepository iProductRepository, IProductService iProductService, IMapper mapper)
        {
            this._iOrderRepository = iOrderRepository;
            this._iProductRepository = iProductRepository;
            this._iProductService = iProductService;
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
            foreach (OrderItemDTO item in createOrder.OrderItems)
            {
                if (!item.StartDate.HasValue || !item.EndDate.HasValue)
                    continue;
                bool isAvailable = await _iProductService
                    .CheckAvailability(item.ProductId, item.StartDate, item.EndDate);

                if (!isAvailable)
                    throw new Exception("ProductUnavailable");
            }

            Order order = _mapper.Map<Order>(createOrder);

            decimal total = 0;

            foreach (OrderItem orderItem in order.OrderItems)
            {
                total += orderItem.PriceAtPurchase;

                Product p = await _iProductRepository
                    .GetProductById(orderItem.ProductId);


                if (p.TransactionType == "Sale")
                {
                    p.IsAvailable = false;
                    await _iProductRepository.UpdateProduct(p.ProductId, p);
                }
            }

            order.TotalAmount = total;
            order.OrderDate = DateTime.UtcNow;

            Order orderTbl = await _iOrderRepository.AddOrder(order);

            return _mapper.Map<OrderDTO>(orderTbl);
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

        public async Task<OccupiedDatesResponseDTO> GetOccupiedDatesForProduct(int productId, int month, int year)
        {
            var ranges = await _iOrderRepository.GetProductOccupiedRanges(productId, month, year);

            var occupiedDatesSet = new HashSet<DateTime>();

            foreach (var range in ranges)
            {
                for (var date = range.Start; date <= range.End; date = date.AddDays(1))
                {
                    if (date.Month == month && date.Year == year)
                    {
                        occupiedDatesSet.Add(date.Date);
                    }
                }
            }

            var formattedDates = occupiedDatesSet
                .OrderBy(d => d)
                .Select(d => d.ToString("yyyy-MM-dd"))
                .ToList();

            return new OccupiedDatesResponseDTO(
                ProductId: productId,
                Month: month,
                Year: year,
                OccupiedDates: formattedDates
            );
        }
    }
}
