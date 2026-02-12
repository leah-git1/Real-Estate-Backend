using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {
        ShopContext _ShopContext;
        public OrderRepository(ShopContext ShopContext)
        {
            this._ShopContext = ShopContext;
        }

        public async Task<Order> GetOrderById(int ind)
        {
            return await _ShopContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(x => x.OrderId == ind);
        }
        public async Task<List<Order>> GetOrdersByUserId(int ind)
        {
            return await _ShopContext.Orders
                .Where(o => o.UserId == ind)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _ShopContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi=>oi.Product)
                .ToListAsync();
        }

        public async Task<Order> AddOrder(Order order)
        {
            await _ShopContext.Orders.AddAsync(order);
            await _ShopContext.SaveChangesAsync();
            return order;
        }


        public async Task<Order> UpdateOrderStatus(int orderId, string status)
        {
            Order order = await _ShopContext.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null)
                return null;

            order.Status = status;
            await _ShopContext.SaveChangesAsync();
            return order;
        }

        public async Task<bool> OrderDelivered(int orderId)
        {
            Order order = await _ShopContext.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null)
                return false;

            order.Status = "delivered";
            await _ShopContext.SaveChangesAsync();
            return true;
        }
    }
}
