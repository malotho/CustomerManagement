using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogicLayer.Models
{
    public interface ICategoryService
    {
        IEnumerable<Category> Categories { get; }
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> Get(int id);
        Task<CategoryCreateResult> Add(CategoryModel category);
        Task<bool> UpdateCategory(int id, CategoryModel product);
        Task<bool> Remove(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> Categories { get => _context.Categories; }

        public async Task<CategoryCreateResult> Add(CategoryModel categoryModel)
        {
            var category = new Category
            {
                CategoryCode = categoryModel.CategoryCode,
                CategoryName = categoryModel.CategoryName,
                IsActive = categoryModel.IsActive
            };
            _context.Categories.Add(category);
            var result = await _context.SaveChangesAsync();
            if (result == 1)
            {
                return new CategoryCreateResult
                {
                    Created = true,
                    Category = category
                };
            }
            return new CategoryCreateResult
            {
                Created = false,
            };
        }

        public async Task<Category> Get(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return category;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }


        public async Task<bool> Remove(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return false;
            }
            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> UpdateCategory(int id, CategoryModel categoryModel)
        {
            var category = new Category
            {
                ID = categoryModel.Id,
                CategoryCode = categoryModel.CategoryCode,
                CategoryName = categoryModel.CategoryName,
                IsActive = categoryModel.IsActive
            };
            _context.Entry(category).State = EntityState.Modified;

            try
            {
                return await _context.SaveChangesAsync() == 1;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Categories.Any(p => p.ID == id))
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
