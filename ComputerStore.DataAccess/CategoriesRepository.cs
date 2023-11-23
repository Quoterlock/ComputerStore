using ComputerStore.DataAccess.Entities;
using ComputerStore.DataAccess;
using ComputerStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using Npgsql.TypeMapping;
using AutoMapper;
using System.Runtime.Intrinsics.X86;

namespace ComputerStore.DataAccess
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private ApplicationDbContext _context;
        public CategoriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Category item)
        {
            await _context.Categories.AddAsync(item);
        }

        public async Task DeleteAsync(string id)
        {
            var item = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
            if (item != null)
                _context.Categories.Remove(item);
            else throw new Exception("Invalid category item");
        }
        public async Task UpdateAsync(Category item)
        {
            if(item != null && item.Id != null)
            {
                _context.Update(item);
            }
            else throw new Exception("Invalid category item");
        }

        public async Task<List<Category>> GetAsync(Func<Category, bool> predicate)
        {
            List<Category> result = _context.Categories.Where(predicate).ToList();
            result ??= new List<Category>();
            return result;
        }

        public async Task<List<Category>> GetAsync()
        {
            List<Category> result = await _context.Categories.ToListAsync();
            return result ?? new List<Category>();
        }

        public async Task<Category> GetAsync(string id)
        {
            var item = await _context.Categories
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            return item ?? throw new Exception("Category not found: id:" + id);
        }

        public async Task<bool> IsExists(string id)
        {
            bool isAny = false;
            if (!string.IsNullOrEmpty(id))
                isAny = !(await _context.Categories.AnyAsync(c => c.Id == id)).Equals(null);
            return isAny;
        }
    }
}
