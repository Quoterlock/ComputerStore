using ComputerStore.Data;
using ComputerStore.Models.Domains;
using ComputerStore.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Models
{
    public class ItemsRepository : IRepository<Item>
    {
        private ApplicationDbContext _context;
        public ItemsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Item item)
        {
            if (item != null && item.Name != null)
            {
                if (item.Category == null)
                {
                    var defaultCategory = await _context.Categories.Where(category => category.Name == "Other").FirstOrDefaultAsync();
                    if (defaultCategory != null)
                    {
                        item.Category = defaultCategory;
                    }
                    else throw new Exception("Cannot find default category");
                }
                _context.Add(item);
                await _context.SaveChangesAsync();
            }
            else throw new Exception("Item is not valid!!!");
        }

        public async Task Delete(string id)
        {
            if (id != null && id != string.Empty)
            {
                var item = await _context.Items.Where(item => item.Id == id).FirstOrDefaultAsync();
                if (item != null)
                {
                    _context.Items.Remove(item);
                    await _context.SaveChangesAsync();
                    return;
                }
            }
            throw new Exception("Item doesn't exist => id : " + id);
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<List<Item>> Get(Func<Item, bool> predicate)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var items = _context.Items
                .Include(item => item.Category)
                .Include(item => item.Image)
                .Where(predicate).ToList();
            if (items == null) items = new List<Item>();
            return items;
        }

        public async Task<List<Item>> GetAll()
        {
            var items = await _context.Items
                .Include(item => item.Category)
                .Include(item => item.Image)
                .ToListAsync();
            if (items == null) items = new List<Item>();
            return items;
        }

        public async Task<Item> GetById(string id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Item result = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (id != null && id != string.Empty)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                result = await _context.Items
                    .Include(item => item.Category)
                    .Include(item => item.Image)
                    .Where(item => item.Id == id)
                    .FirstOrDefaultAsync();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            }
            if (result == null) throw new Exception("Item not found");
            return result;
        }

        public bool IsValid(Item item)
        {
            if (item.Id != null && item.Id != string.Empty && item.Name != null && item.Description != null)
                return true;
            else
                return false;
        }

        public async Task Update(Item item)
        {
            if (item != null && IsValid(item))
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Item>> FindAll(string value)
        {
            List<Item> items = new List<Item>();
            if (value != null && value != string.Empty)
            {
                items.AddRange(await _context.Items
                    .Include(item => item.Category)
                    .Include(item => item.Image)
                    .Where(item => item.Name != null && item.Name.Contains(value))
                    .ToListAsync());
                items.AddRange(await _context.Items
                    .Include(item => item.Category)
                    .Include(item => item.Image)
                    .Where(item => item.Category.Name != null && item.Category.Name.Contains(value))
                    .ToArrayAsync());
                items.AddRange(await _context.Items
                    .Include(item => item.Category)
                    .Include(item => item.Image)
                    .Where(item => item.Description != null && item.Description.Contains(value))
                    .ToListAsync());
            }
            return items;
        }
    }
}
