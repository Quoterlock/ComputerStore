using ComputerStore.Data;
using ComputerStore.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System;

namespace ComputerStore.Models
{
    public class CategoriesRepository : IRepository<Category>
    {
        private ApplicationDbContext _context;
        public CategoriesRepository(ApplicationDbContext context) 
        { 
            _context = context;
        }
        public async Task Add(Category item)
        {
            if(item.Name != null && item.ThumbnailImageUri != null)
            {
                await _context.Categories.AddAsync(item);
                await _context.SaveChangesAsync();
            }
            else throw new Exception("Invalid category model");
        }

        public async Task Delete(string id)
        {
            if (id != null && id != string.Empty)
            {
                var item = await _context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
                if (item != null)
                {
                    _context.Categories.Remove(item);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                throw new Exception("Invalid category model");
            }
        }

        public async Task<List<Category>> Get(Func<Category, bool> predicate)
        {
            List<Category> result = _context.Categories.Where(predicate).ToList();
            if(result == null) 
                result = new List<Category>();
            return result;
        }

        public async Task<List<Category>> GetAll()
        {
            List<Category> result = await _context.Categories.ToListAsync();
            if (result == null)
                result = new List<Category>();
            return result;
        }

        public async Task<Category> GetById(string id)
        {
            var item = await _context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
            return item;
        }

        public bool IsValid(Category item)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Category item)
        {
            if(item != null && item.Id != null)
            {
                _context.Categories.Update(item);
                await _context.SaveChangesAsync();
            }
            throw new Exception("Invalid category item");
        }

        public async Task<List<Category>> FindAll(string value)
        {
            if(value != null && value != string.Empty)
                return await Get(c => c.Name.Contains(value));
            return new List<Category>();
        }
    }
}
