using ComputerStore.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic.Services
{
    public class ItemsService : IItemsService
    {
        public Task Search(string value)
        {
            throw new NotImplementedException();
            /*
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
             */
        }
    }
}
