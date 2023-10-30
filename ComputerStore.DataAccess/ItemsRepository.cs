using ComputerStore.DataAccess.Entities;
using ComputerStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.DataAccess
{

    public class ItemsRepository : IItemsRepository
    {
        private ApplicationDbContext _context;
        public ItemsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Item> GetAsync(string id)
        {
            Item result = null;
            if (id != null && id != string.Empty)
            {
                result = await _context.Items
                    .Include(item => item.Category)
                    .Include(item => item.Image)
                    .Where(item => item.Id == id)
                    .FirstOrDefaultAsync();
            }
            if (result == null) throw new Exception("Item not found");
            return result;
        }

        public async Task<List<Item>> GetAsync(Func<Item, bool> predicate)
        {
            var items = _context.Items
                .Include(item => item.Category)
                .Include(item => item.Image)
                .Where(predicate).ToList();
            if (items == null) items = new List<Item>();
            return items;
        }

        public async Task<List<Item>> GetAsync()
        {
            var items = await _context.Items
                .Include(item => item.Category)
                .Include(item => item.Image)
                .ToListAsync();
            if (items == null) items = new List<Item>();
            return items;
        }

        public async Task AddAsync(Item item)
        {
            if (item != null && item.Name != null)
            {
                /*
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == item.Category.Id);
                if (category != null)
                    _context.Add(item);
                else throw new Exception("Cannot find default category");
                */
                
                if (item.Category == null)
                {
                    var defaultCategory = await _context.Categories.Where(category => category.Name == "Other").FirstOrDefaultAsync();
                    if (defaultCategory != null)
                        item.Category = defaultCategory;
                    else throw new Exception("Cannot find default category");
                }
                _context.Add(item);
                
            }
            else throw new Exception("Item is not valid!!!");
        }

        public async Task UpdateAsync(Item item)
        {
            if (item != null && IsValid(item))
                _context.Update(item);
            else throw new Exception("Item entity is not valid");
        }

        public async Task DeleteAsync(string id)
        {
            if (id != null && id != string.Empty)
            {
                var item = await _context.Items
                    .Where(item => item.Id == id)
                    .FirstOrDefaultAsync();
                if (item != null)
                {
                    _context.Items.Remove(item);
                }
            }
            else throw new Exception("Item doesn't exist => id : " + id);
        }

        private bool IsValid(Item item)
        {
            if (item.Id != null && item.Id != string.Empty && item.Name != null && item.Description != null)
                return true;
            else
                return false;
        }

        public Task<bool> IsExists(string id)
        {
            throw new NotImplementedException();
        }
    }
}
