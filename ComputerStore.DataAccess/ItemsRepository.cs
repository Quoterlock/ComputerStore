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

        public async Task<Item> GetById(string id)
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

        public async Task<List<Item>> Get(Func<Item, bool> predicate)
        {
            var items = _context.Items
                .Include(item => item.Category)
                .Include(item => item.Image)
                .Where(predicate).ToList();
            if (items == null) items = new List<Item>();
            return items;
        }

        public async Task<List<Item>> Get()
        {
            var items = await _context.Items
                .Include(item => item.Category)
                .Include(item => item.Image)
                .ToListAsync();
            if (items == null) items = new List<Item>();
            return items;
        }

        public async Task Add(Item item)
        {
            if (item != null && item.Name != null)
            {
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

        public async Task Update(Item item)
        {
            if (item != null && IsValid(item))
                _context.Update(item);
            else throw new Exception("Item entity is not valid");
        }

        public async Task Delete(string id)
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
    }
}
