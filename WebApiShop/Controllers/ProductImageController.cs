using Microsoft.AspNetCore.Mvc;
using Services;
using DTOs;
namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : Controller
    {
            IProductImageService _iProductImageService;
            public ProductImageController(IProductImageService iProductImageService)
            {
                _iProductImageService = iProductImageService;
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
                    return BadRequest();

                return CreatedAtAction(nameof(GetProductImageById), new { id = image.ImageId }, image);
                //return await _iProductImageService.Invite(order);
            }

            [HttpPut("{imageId}")]
            public async Task<ActionResult> UpdateProductImage(int imageId, ProductImageUpdateDTO imageUpdate)
            {
                ProductImageDTO image = await _iProductImageService.UpdateProductImage(imageId, imageUpdate);
                if (image == null)
                    return BadRequest();

                return Ok(image);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteImage(int id)
            {
                
                var success = await _iProductImageService.DeleteImage(id);
                if (!success)
                {
                    return NotFound();
                }
                return NoContent();
            }
        
    }
}
