using Microsoft.AspNetCore.Mvc;
using Services;
using DTOs;
namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : Controller
    {
        private readonly IProductImageService _iProductImageService;
        private readonly ILogger<ProductImageController> _logger;

        public ProductImageController(IProductImageService iProductImageService, ILogger<ProductImageController> logger)
        {
            _iProductImageService = iProductImageService;
            _logger = logger;
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductImageDTO>> GetProductImageById(int id)
        {
            return await _iProductImageService.getProductImageById(id);
        }

        [HttpGet("productImage/{productId}")]
        public async Task<ActionResult<List<ProductImageDTO>>> GetProductImagesByProductId(int productId)
        {
            return await _iProductImageService.GetProductImagesByProductId(productId);
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<ActionResult<ProductImageDTO>> AddProductImage(ProductImageCreateDTO productImage)
        {
            ProductImageDTO image = await _iProductImageService.AddProductImage(productImage);
            if (image == null)
            {
                _logger.LogWarning("Failed to add image for product {productId}", productImage.ProductId);
                return BadRequest();
            }
            _logger.LogInformation("Image {imageId} added successfully to product {productId}", image.ImageId, image.ProductId);
            return CreatedAtAction(nameof(GetProductImageById), new { id = image.ImageId }, image);
            //return await _iProductImageService.Invite(order);
        }

        [HttpPut("{imageId}")]
        public async Task<ActionResult> UpdateProductImage(int imageId, ProductImageUpdateDTO imageUpdate)
        {
            ProductImageDTO image = await _iProductImageService.UpdateProductImage(imageId, imageUpdate);
            if (image == null)
            {
                _logger.LogWarning("Update failed: Image {imageId} not found", imageId);
                return BadRequest();
            }

            _logger.LogInformation("Image {imageId} was updated successfully", imageId);
            return Ok(image);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {

            var success = await _iProductImageService.DeleteImage(id);
            if (!success)
            {
                _logger.LogWarning("Delete failed: Attempted to delete non-existent image {id}", id);
                return NotFound();
            }
            _logger.LogInformation("Image {id} was deleted successfully", id);
            return NoContent();
        }

    }
}
