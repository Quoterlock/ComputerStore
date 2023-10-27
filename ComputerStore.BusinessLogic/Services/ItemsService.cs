using ComputerStore.BusinessLogic.Domains;
using ComputerStore.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerStore.DataAccess.Interfaces;
using ComputerStore.DataAccess.Entities;

namespace ComputerStore.BusinessLogic.Services
{
    public class ItemsService : IItemsService
    {
        private IUnitOfWork _unitOfWork;
        public ItemsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ItemModel>> GetAll()
        {
            var entities = await _unitOfWork.Items.GetAsync();
            var items = new List<ItemModel>();
            if(entities != null)
                items = EntityesToModels(entities);
            return items;
        }

        public async Task<List<ItemModel>> GetFromCategory(string categoryId)
        {
            var items = new List<ItemModel>();   
            if(!string.IsNullOrEmpty(categoryId))
            {
                var entities = await _unitOfWork.Items.GetAsync(item => item.Category.Id == categoryId);
                if (entities != null)
                    items = EntityesToModels(entities);
            }    
            return items;
        }

        public Task<List<ItemModel>> Search(string value)
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

        private List<ItemModel> EntityesToModels(IEnumerable<Item> entities)
        {
            var items = new List<ItemModel>();
            foreach (var entity in entities)
            {
                items.Add(EntityToDomain(entity));
            }
            return items;
        }

        private ItemModel EntityToDomain(Item entity)
        {
            return new ItemModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Image = new ImageModel
                {
                    Alt = entity.Image.Alt,
                    Id = entity.Image.Id,
                    Bytes = entity.Image.Bytes
                },
                Price = entity.Price,
                Category = new CategoryModel
                {
                    Id = entity.Category.Id,
                    Name = entity.Category.Name,
                }
            };
        }
    }
}
