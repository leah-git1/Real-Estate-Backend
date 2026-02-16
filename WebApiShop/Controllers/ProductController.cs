using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _iProductService;
        private readonly Logger<ProductController> _logger;

        public ProductController(IProductService iProductService, Logger<ProductController> logger)
        {
            _iProductService = iProductService;
            _logger = logger;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<PageResponseDTO<ProductSummaryDTO>>> GetProducts([FromQuery] int?[] categoryIds, string? city, decimal? minPrice, decimal? maxPrice, int? rooms, int? beds, int position, int skip)
        {
            return await _iProductService.GetProducts(categoryIds, city, minPrice, maxPrice, rooms, beds, position, skip);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailsDTO>> GetProductById(int id)
        {
            ProductDetailsDTO product = await _iProductService.GetProductById(id);
            if (product == null)
            {
                _logger.LogWarning("Product with ID {Id} was not found", id);
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDetailsDTO>> AddProduct(ProductCreateDTO productCreateDto)
        {
            ProductDetailsDTO newProduct = await _iProductService.AddProduct(productCreateDto);
            if(newProduct==null)
            {
                _logger.LogWarning("Failed to add product with name {Name}", productCreateDto.Title);
                return BadRequest();
            }
            _logger.LogInformation("Product added successfully with ID: {Id}", newProduct.ProductId);
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.ProductId }, newProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, ProductUpdateDTO productUpdateDto)
        {
            ProductDetailsDTO updatedProduct = await _iProductService.UpdateProduct(id, productUpdateDto);
            if (updatedProduct == null)
            {
                _logger.LogWarning("Update failed: Product with ID {Id} not found", id);
                return NotFound();
            }
            _logger.LogInformation("Product with ID {Id} updated successfully", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            bool isDeleted = await _iProductService.DeleteProduct(id);
            if (!isDeleted)
            {
                _logger.LogWarning("Delete failed: Product with ID {Id} not found", id);
                return NotFound();
            }
            _logger.LogInformation("Product with ID {Id} was deleted successfully", id);
            return NoContent();
        }

        [HttpGet("owner/{ownerId}")]
        public async Task<ActionResult<List<ProductSummaryDTO>>> GetProductsByOwnerId(int ownerId)
        {
            List<ProductSummaryDTO> ownerProducts = await _iProductService.GetProductsByOwnerId(ownerId);
            return ownerProducts;
        }

        [HttpGet("check-availability")]
        public async Task<ActionResult<bool>> GetAvailability([FromQuery] int productId, [FromQuery] DateTime? start, [FromQuery] DateTime? end)
        {
            bool isAvailable = await _iProductService.CheckAvailability(productId, start, end);
            return Ok(isAvailable);
        }
    }
}