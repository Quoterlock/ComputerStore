using ComputerStore.DataAccess.Entities;
using ComputerStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.DataAccess
{

    public class ItemsRepository : IItemsRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Item> GetAsync(string id)
        {
            if (id != null && id != string.Empty)
            {
                var result = await _context.Items
                    .Where(item => item.Id == id)
                    .FirstOrDefaultAsync();
                return result ?? throw new Exception("Item not found");
            }
            else throw new Exception("Repo: Item id is null!");
        }

        public async Task<List<Item>> GetAsync(Func<Item, bool> predicate)
        {
            var items = _context.Items.Where(predicate).ToList();
            return items ?? new List<Item>();
        }

        public async Task<List<Item>> GetAsync()
        {
            var items = await _context.Items.ToListAsync();
            return items ?? new List<Item>();
        }

        public async Task AddAsync(Item item)
        {
            if (IsValid(item))
            {
                if(await _context.Categories.AnyAsync(i=>i.Id == item.CategoryID))
                    _context.Add(item);
                else 
                    throw new Exception("Category is not found with id: " + item.CategoryID);
            }
            else throw new Exception("Item is not valid!");
        }

        public async Task UpdateAsync(Item item)
        {
            if (IsValid(item) && !string.IsNullOrEmpty(item.Id))
                _context.Update(item);
            else 
                throw new Exception("Item entity is not valid");
        }

        public async Task DeleteAsync(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var item = await _context.Items
                    .Where(item => item.Id == id)
                    .FirstOrDefaultAsync();

                if (item != null)
                    _context.Items.Remove(item);
                else 
                    throw new Exception("Item doesn't exist => id : " + id);
            }
            else throw new Exception("Item id is null!");
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _context.Items.AnyAsync(item => item.Id == id);
        }

        private static bool IsValid(Item item)
        {
            return item != null
                && !string.IsNullOrEmpty(item.Name)
                && !string.IsNullOrEmpty(item.CategoryID);
        }
    }
}
