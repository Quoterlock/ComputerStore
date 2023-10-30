using ComputerStore.BusinessLogic.Domains;
using ComputerStore.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerStore.DataAccess.Interfaces;
using ComputerStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ComputerStore.BusinessLogic.Services
{
    public class ItemsService : IItemsService
    {
        private IUnitOfWork _unitOfWork;
        public ItemsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(ItemModel model)
        {
            if (model != null)
            {
                var entity = Convertor.ConvertModelToEntity(model);
                await _unitOfWork.Items.AddAsync(entity);
                await _unitOfWork.Commit();
            }
            else throw new Exception("Model is null!");
        }

        public async Task<List<ItemModel>> GetAllAsync()
        {
            var entities = await _unitOfWork.Items.GetAsync();
            var items = new List<ItemModel>();
            if(entities != null)
                items = EntityesToModels(entities);
            return items;
        }

        public async Task<ItemModel> GetByIdAsync(string id)
        {
            var entity = await _unitOfWork.Items.GetAsync(id);
            if(entity != null)
                return Convertor.EntityToModel(entity);
            throw new Exception("Cannot find the item id:" + id);
        }

        public async Task<List<ItemModel>> GetFromCategoryAsync(string categoryId)
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

        public async Task RemoveAsync(string id)
        {
            await _unitOfWork.Categories.DeleteAsync(id);
            await _unitOfWork.Commit();
        }

        public Task<List<ItemModel>> SearchAsync(string value)
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

        public async Task UpdateAsync(ItemModel model)
        {
            if (model != null)
            {
                var entity = Convertor.ConvertModelToEntity(model);
                await _unitOfWork.Items.UpdateAsync(entity);
                await _unitOfWork.Commit();
            }
            else throw new Exception("Model is null!");
        }

        private List<ItemModel> EntityesToModels(IEnumerable<Item> entities)
        {
            var items = new List<ItemModel>();
            foreach (var entity in entities)
            {
                items.Add(Convertor.EntityToModel(entity));
            }
            return items;
        }

    }
}
