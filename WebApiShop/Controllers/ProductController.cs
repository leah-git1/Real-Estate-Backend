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
        IProductService _iProductService;

        public ProductController(IProductService iProductService)
        {
            _iProductService = iProductService;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<PageResponseDTO<ProductSummaryDTO>>> GetProducts([FromQuery] int?[] categoryIds, string? city, decimal? minPrice, decimal? maxPrice, int? rooms, int? beds, int position, int skip)
        {
            return  await _iProductService.GetProducts(categoryIds, city, minPrice, maxPrice, rooms, beds, position, skip);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailsDTO>> GetProductById(int id)
        {
            ProductDetailsDTO product = await _iProductService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDetailsDTO>> AddProduct(ProductCreateDTO productCreateDto)
        {
            ProductDetailsDTO newProduct = await _iProductService.AddProduct(productCreateDto);
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.ProductId }, newProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, ProductUpdateDTO productUpdateDto)
        {
            ProductDetailsDTO updatedProduct = await _iProductService.UpdateProduct(id, productUpdateDto);
            if (updatedProduct == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            bool isDeleted = await _iProductService.DeleteProduct(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("owner/{ownerId}")]
        public async Task<ActionResult<List<ProductSummaryDTO>>> GetProductsByOwnerId(int ownerId)
        {
            List<ProductSummaryDTO> ownerProducts = await _iProductService.GetProductsByOwnerId(ownerId);
            return ownerProducts;
        }
    }
}