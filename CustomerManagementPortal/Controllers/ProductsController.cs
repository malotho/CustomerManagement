using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models;
using LogicLayer.Models;
using Microsoft.AspNetCore.Authorization;

namespace CustomerManagementPortal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return _productService.GetProducts();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ID)
            {
                return BadRequest();
            }
            var updated = await _productService.UpdateProduct(id, product);
            if (updated)
            {
                return Ok(product.ID);
            }
            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductModel product)
        {
            var added = await _productService.Add(product);
            return CreatedAtAction("GetProduct", added.Product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _productService.Remove(id);
            return deleted ? Ok() : NoContent();
        }
    }
}
