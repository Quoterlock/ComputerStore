using ComputerStore.DataAccess.Entities;
using ComputerStore.DataAccess;
using ComputerStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace ComputerStore.DataAccess
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private ApplicationDbContext _context;
        public CategoriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Category item)
        {
            if (item.Name != null && item.Image != null)
                await _context.Categories.AddAsync(item);
            else throw new Exception("Invalid category model");
        }

        public async Task Delete(string id)
        {
            if (id != null && id != string.Empty)
            {
                var item = await _context.Categories
                    .Where(c => c.Id == id)
                    .FirstOrDefaultAsync();
                if (item != null)
                    _context.Categories.Remove(item);
            }
            else
                throw new Exception("Invalid category model");
        }

        public async Task<List<Category>> Get(Func<Category, bool> predicate)
        {
            List<Category> result = _context.Categories
                .Include(c => c.Image)
                .Where(predicate).ToList();
            if (result == null)
                result = new List<Category>();
            return result;
        }

        public async Task<List<Category>> Get()
        {
            List<Category> result = await _context.Categories
                .Include(c => c.Image)
                .ToListAsync();
            if (result == null)
                result = new List<Category>();
            return result;
        }

        public async Task<Category> GetById(string id)
        {
            var item = await _context.Categories
                .Include(c => c.Image)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            if (item == null)
                throw new Exception("Category not found: id:" + id);

            return item;
        }

        public async Task Update(Category item)
        {
            if (item != null && item.Id != null)
                _context.Categories.Update(item);
            else throw new Exception("Invalid category item");
        }
    }
}
