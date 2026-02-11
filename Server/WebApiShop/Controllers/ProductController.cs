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
        public async Task<IEnumerable<ProductSummaryDTO>> GetProducts([FromQuery] int?[] categoryIds, string? city, decimal? minPrice, decimal? maxPrice, int? rooms, int? beds)
        {
            IEnumerable<ProductSummaryDTO> products = await _iProductService.getProducts(categoryIds, city, minPrice, maxPrice, rooms, beds);
            return products;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailsDTO>> GetById(int id)
        {
            ProductDetailsDTO product = await _iProductService.getProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDetailsDTO>> Post([FromBody] ProductCreateDTO productCreateDto)
        {
            ProductDetailsDTO newProduct = await _iProductService.addProduct(productCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = newProduct.ProductID }, newProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductUpdateDTO productUpdateDto)
        {
            ProductDetailsDTO updatedProduct = await _iProductService.updateProduct(id, productUpdateDto);
            if (updatedProduct == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool isDeleted = await _iProductService.deleteProduct(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("owner/{ownerId}")]
        public async Task<IEnumerable<ProductSummaryDTO>> GetByOwnerId(int ownerId)
        {
            IEnumerable<ProductSummaryDTO> ownerProducts = await _iProductService.getProductsByOwnerId(ownerId);
            return ownerProducts;
        }
    }
}