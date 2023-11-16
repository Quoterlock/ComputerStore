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
            if (id != null && id != string.Empty)
            {
                var result = await _context.Items
                    .Include(item => item.Category)
                    .Include(item => item.Image)
                    .Where(item => item.Id == id)
                    .FirstOrDefaultAsync();

                if (result == null) 
                    throw new Exception("Item not found");

                return result;
            }
            else 
                throw new Exception("Repo: Item id is null!");
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
            if (IsValid(item))
            {
                if (item.Category != null && !string.IsNullOrEmpty(item.Category.Id))
                {
                    var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == item.Category.Id);

                    if (category != null) 
                        item.Category = category;
                    else 
                        throw new Exception("Cannot find category id: " + item.Category.Id);
                }
                else 
                    throw new Exception("Category is null!");
                /*
                if (item.Image != null && !string.IsNullOrEmpty(item.Image.Id))
                {
                    var image = await _context.Images.FirstOrDefaultAsync(i=>i.Id == item.Image.Id);
                    if (image != null) 
                        item.Image = image; 
                    else 
                        throw new Exception("Cannot find image id:" + item.Image.Id);
                }
                */
                _context.Add(item);
            }
            else throw new Exception("Item is not valid!!!");
        }

        public async Task UpdateAsync(Item item)
        {
            if (IsValid(item) && !string.IsNullOrEmpty(item.Id))
                _context.Update(item);
            else throw new Exception("Item entity is not valid");
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
                else throw new Exception("Item doesn't exist => id : " + id);
            }
            else throw new Exception("Item id is null!");
        }

        private static bool IsValid(Item item)
        {
            return item != null && !string.IsNullOrEmpty(item.Name);
        }

        public async Task<bool> IsExists(string id)
        {
            return await _context.Items.AnyAsync(item => item.Id == id);
        }
    }
}
