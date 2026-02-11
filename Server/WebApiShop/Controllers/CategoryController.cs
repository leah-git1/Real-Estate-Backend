using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoriesServies _iCategoryServices;
        public CategoryController(ICategoriesServies iCategoryServices)
        {
            _iCategoryServices = iCategoryServices;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<List<CategoryDTO>> GetAllCategories()
        {
            return await _iCategoryServices.GetAllCategories();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            return await _iCategoryServices.GetCategoryById(id);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryCreateDTO>> AddCategory(CategoryCreateDTO category)
        {
            CategoryDTO newCategory = await _iCategoryServices.AddCategory(category);
            if (newCategory == null)
                return BadRequest();
            return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.CategoryId }, newCategory);
            //return await _iOrderService.Invite(order);
        }

        // DELETE: api/<CategoryController>/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var success = await _iCategoryServices.DeleteCategory(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        // PUT: api/<CategoryController>/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDTO>> UpdateCategory(int id, CategoryUpdateDTO categoryUpdate)
        {
            CategoryDTO updatedCategory = await _iCategoryServices.UpdateCategory(id, categoryUpdate);
            if (updatedCategory == null)
            {
                return NotFound(); 
            }
            return Ok(updatedCategory); 
        }


    }
}
