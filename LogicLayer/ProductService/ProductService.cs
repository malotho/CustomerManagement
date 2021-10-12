using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogicLayer.Models
{
    public interface IProductService
    {
        IEnumerable<Product> Products { get; }
        IEnumerable<Product> GetProducts();
        Task<Product> Get(int id);
        Task<ProductCreateResult> Add(ProductModel product);
        Task<bool> UpdateProduct(int id, Product product);
        Task<bool> Remove(int id);
    }

    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> Products { get => _context.Products; }


        public string GenerateCode()
        {
            var seq = _context.GetMySequence();
            return $"{DateTime.Today:yyyyMM}-{seq.ToString().PadLeft(3, '0')}";
        }

        /// <summary>
        /// TODO: User AutoMapper here... 
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns></returns>
        public async Task<ProductCreateResult> Add(ProductModel productModel)
        {            

            var cat = _context.Categories.Single(p => p.ID == productModel.Category.Id);
            var product = new Product
            {
                Category = cat,
                ProductName = productModel.ProductName,
                Description = productModel.Description,
                ProductCode = GenerateCode(),
                Price = productModel.Price
            };

            _context.Products.Add(product);
            var result = await _context.SaveChangesAsync();
            if (result == 1)
            {
                return new ProductCreateResult
                {
                    Created = true,
                    Product = product
                };
            }
            return new ProductCreateResult
            {
                Created = false
            };
        }

        public async Task<Product> Get(int id)
        {
            var product = await _context.Products.Include(c => c.Category)
                .SingleAsync(p => p.ID == id);
            return product;
        }

        public IEnumerable<Product> GetProducts()
        {
            var x = _context.Products.Include(c => c.Category).ToList();
            return x;
        }

        public async Task<bool> Remove(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> UpdateProduct(int id, Product product)
        {
            var cat = _context.Categories.Single(p => p.ID == product.Category.ID);
            var productUpdate = new Product
            {
                ID = product.ID,
                Category = cat,
                ProductName = product.ProductName,
                Description = product.Description,
                ProductCode = product.ProductCode,
                Price = product.Price
            };

            _context.Entry(productUpdate).State = EntityState.Modified;

            try
            {
                return await _context.SaveChangesAsync() == 1;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Products.Any(p => p.ID == id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
