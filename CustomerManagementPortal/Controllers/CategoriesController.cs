using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Models;
using LogicLayer.Models;
using Microsoft.AspNetCore.Authorization;

namespace CustomerManagementPortal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _categoryService.GetCategories();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _categoryService.Get(id);
            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        //PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryModel category)
        {
            var updated = await _categoryService.UpdateCategory(id, category);
            if (!updated)
            {
                return BadRequest();
            }
            return Accepted();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CategoryModel category)
        {
            var result = await _categoryService.Add(category);
            if (result.Created)
            {
                return CreatedAtAction("GetCategory", result.Category);
            }
            return BadRequest();
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deleted = await _categoryService.Remove(id);
            if (!deleted)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
