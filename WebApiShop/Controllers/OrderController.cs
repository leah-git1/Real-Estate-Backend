using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderService _iOrderService;
        public OrderController(IOrderService iOrderService)
        {
            _iOrderService = iOrderService;
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrderById(int id)
        {
            return await _iOrderService.GetOrderById(id);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<OrderHistoryDTO>>> GetOrdersByUserId(int userId)
        {
            return await _iOrderService.GetOrdersByUserId(userId);
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderHistoryAdminDTO>>> GetAllOrders()
        {
            return await _iOrderService.GetAllOrders();
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> AddOrder(OrderCreateDTO order)
        {
            OrderDTO postOrder = await _iOrderService.AddOrder(order);
            if (postOrder == null)
                return BadRequest();

            return CreatedAtAction(nameof(GetOrderById), new { id = postOrder.OrderId }, postOrder);
            //return await _iOrderService.Invite(order);
        }

        [HttpPut("{orderId}/status")]
        public async Task<ActionResult> UpdateOrderStatus(int orderId, OrderStatusUpdateDTO statusDto)
        {
            OrderDTO updatedOrder = await _iOrderService.UpdateOrderStatus(orderId, statusDto);
            if (updatedOrder == null)
                return BadRequest();

            return Ok(updatedOrder);
        }

        
        [HttpPut("{orderId}/delivered")]
        public async Task<ActionResult> OrderDelivered(int orderId)
        {
            bool result = await _iOrderService.OrderDelivered(orderId);
            if (!result)
                return BadRequest();

            return NoContent();
        }
    }
}
