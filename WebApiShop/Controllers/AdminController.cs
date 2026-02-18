using DTOs;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IAdminService adminService, ILogger<AdminController> logger)
        {
            _adminService = adminService;
            _logger = logger;
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<UserProfileDTO>>> GetAllUsers()
        {
            List<UserProfileDTO> users = await _adminService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("products")]
        public async Task<ActionResult<List<ProductDetailsDTO>>> GetAllProducts()
        {
            List<ProductDetailsDTO> products = await _adminService.GetAllProducts();
            return Ok(products);
        }

        [HttpDelete("user/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            bool isDeleted = await _adminService.DeleteUser(id);
            if (!isDeleted)
            {
                _logger.LogWarning("Delete failed: User with ID {id} not found", id);
                return NotFound();
            }
            _logger.LogInformation("User with ID {id} was deleted by admin", id);
            return NoContent();
        }

        [HttpDelete("product/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            bool isDeleted = await _adminService.DeleteProduct(id);
            if (!isDeleted)
            {
                _logger.LogWarning("Delete failed: Product with ID {id} not found", id);
                return NotFound();
            }
            _logger.LogInformation("Product with ID {id} was deleted by admin", id);
            return NoContent();
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<AdminStatisticsDTO>> GetStatistics()
        {
            AdminStatisticsDTO statistics = await _adminService.GetStatistics();
            return Ok(statistics);
        }

        [HttpGet("orders")]
        public async Task<ActionResult<List<OrderAdminDTO>>> GetAllOrders()
        {
            List<OrderAdminDTO> orders = await _adminService.GetAllOrders();
            return Ok(orders);
        }

        [HttpDelete("order/{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            bool isDeleted = await _adminService.DeleteOrder(id);
            if (!isDeleted)
            {
                _logger.LogWarning("Delete failed: Order with ID {id} not found", id);
                return NotFound();
            }
            _logger.LogInformation("Order with ID {id} was deleted by admin", id);
            return NoContent();
        }
    }
}
