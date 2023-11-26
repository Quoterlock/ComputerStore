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
using ComputerStore.Utilities;
using ComputerStore.BusinessLogic.Adapters;

namespace ComputerStore.BusinessLogic.Services
{
    public class ItemsService : IItemsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityToModelAdapter<Item, ItemModel> _itemAdapter;
        private readonly IEntityToModelAdapter<Category, CategoryModel> _categoryAdapter;
        public ItemsService(IUnitOfWork unitOfWork, 
            IEntityToModelAdapter<Item, ItemModel> itemAdapter, 
            IEntityToModelAdapter<Category, CategoryModel> categoryAdapter)
        {
            _unitOfWork = unitOfWork;
            _itemAdapter = itemAdapter;
            _categoryAdapter = categoryAdapter;
        }

        public async Task AddAsync(ItemModel model)
        {
            if (model != null)
            {
                try
                {
                    var entity = _itemAdapter.ToEntity(model);
                    await _unitOfWork.Items.AddAsync(entity);
                    await _unitOfWork.CommitAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else throw new Exception("Model is null!");
        }

        public async Task<List<ItemModel>> GetAllAsync()
        {
            var entities = await _unitOfWork.Items.GetAsync();
            var items = new List<ItemModel>();

            if(entities != null)
                items = await ConvertEntitiesToModels(entities);

            return items;
        }

        public async Task<ItemModel> GetByIdAsync(string id)
        {
            try
            {
                var entity = await _unitOfWork.Items.GetAsync(id);
                if (entity != null)
                {
                    var model = _itemAdapter.ToModel(entity);
                    var categoryEntity = await _unitOfWork.Categories.GetAsync(entity.CategoryID);
                    if (categoryEntity != null)
                    {
                        model.Category = _categoryAdapter.ToModel(categoryEntity);
                        return model;
                    } 
                    else throw new Exception("Cannot find item category with id:" + entity.CategoryID);
                }
                else throw new Exception("Cannot find the item id:" + id);
            }
            catch (Exception ex) 
            { 
                throw new Exception(ex.Message); 
            }
        }

        public async Task<List<ItemModel>> GetFromCategoryAsync(string categoryId)
        {
            var items = new List<ItemModel>();   
            if(!string.IsNullOrEmpty(categoryId))
            {
                var entities = await _unitOfWork.Items.GetAsync(item => item.CategoryID == categoryId);
                if (entities != null)
                    items = await ConvertEntitiesToModels(entities);
            }
            return items;
        }

        public async Task RemoveAsync(string id)
        {
            if(!string.IsNullOrEmpty(id))
            {
                try
                {
                    await _unitOfWork.Items.DeleteAsync(id);
                    await _unitOfWork.CommitAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            } else throw new ArgumentNullException("item id");
        }

        public async Task UpdateAsync(ItemModel model)
        {
            if (model != null && !string.IsNullOrEmpty(model.Id))
            {
                try
                {
                    var entity = await _unitOfWork.Items.GetAsync(model.Id);
                    entity.Price = model.Price;
                    entity.Description = model.Description;
                    entity.Brief = model.Brief;
                    entity.Name = model.Name;
                    if (entity.CategoryID != model.Category.Id && !string.IsNullOrEmpty(model.Category.Id))
                        entity.CategoryID = model.Category.Id;
                    if (model.Image != null && model.Image.Bytes.Length != 0)
                        entity.ImageBytes = model.Image.Bytes;

                    await _unitOfWork.CommitAsync();
                } 
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else throw new Exception("Model is null!");
        }

        public async Task<List<ItemModel>> SearchAsync(string value)
        {
            var entities = new List<Item>();
            if (!string.IsNullOrEmpty(value))
            {
                value = value.ToLower();

                var categories = await _unitOfWork.Categories.GetAsync(c => c.Name.ToLower().Contains(value));

                entities.AddRange(await _unitOfWork.Items.GetAsync(i => i.Name.ToLower().Contains(value)));
                entities.AddRange(await _unitOfWork.Items.GetAsync(i => i.Description.ToLower().Contains(value)));
                foreach (var category in categories)
                    entities.AddRange(await _unitOfWork.Items.GetAsync(i => i.CategoryID.Equals(category.Id)));

                entities = RemoveDuplications(entities);
                return await ConvertEntitiesToModels(entities);
            }
            else
            {
                entities = await _unitOfWork.Items.GetAsync();
            }
            return await ConvertEntitiesToModels(entities);
        }

        public List<ItemModel> Sort(List<ItemModel> items, string sortMode)
        {
            var sort = GetSortEnum(sortMode);

            if (sort == SortMode.CostDown)
            {
                items = SortByCost(items);
                return items;
            }
            else if (sort == SortMode.CostUp)
            {
                items = SortByCost(items);
                items.Reverse();
                return items;
            }
            else if (sort == SortMode.Name)
            {
                return SortByName(items);
            }
            else
            {
                return items;
            }
        }
        
        private async Task<List<ItemModel>> ConvertEntitiesToModels(IEnumerable<Item> entities)
        {
            var items = new List<ItemModel>();
            foreach (var entity in entities)
            {
                var item = _itemAdapter.ToModel(entity);
                item.Category = _categoryAdapter.ToModel(await _unitOfWork.Categories.GetAsync(entity.CategoryID));
                items.Add(item);
            }
            return items;
        }

        private static SortMode GetSortEnum(string sortBy)
        {
            if (sortBy == null) return SortMode.ItemId;
            if (sortBy.Equals("costUp")) return SortMode.CostUp;
            if (sortBy.Equals("costDown")) return SortMode.CostDown;
            else return SortMode.ItemId;
        }

        private static List<ItemModel> SortByCost(List<ItemModel> items)
        {
            return QuickSort(items, "cost", 0);
        }

        private static List<ItemModel> SortByName(List<ItemModel> items)
        {
            return QuickSort(items, "name", 1);
        }

        private static int Partition(List<ItemModel> list, string key, int minIndex, int maxIndex, int type)
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
                if (CompareStrings(list[maxIndex][key].ToString(), list[i][key].ToString(), type))
                {
                    pivot++;
                    Swap(list, pivot, i);
                }

            pivot++;
            Swap(list, pivot, maxIndex);
            return pivot;
        }

        private static void QuickSort(List<ItemModel> list, string key, int minIndex, int maxIndex, int type)
        {
            if (minIndex >= maxIndex) return;

            var pivotIndex = Partition(list, key, minIndex, maxIndex, type);
            QuickSort(list, key, minIndex, pivotIndex - 1, type);
            QuickSort(list, key, pivotIndex + 1, maxIndex, type);
        }

        /// <param name="list"> It's a list with objects</param>
        /// <param name="key">Key field on which the sort will work</param>
        /// <param name="type">if 1 reverse sort(Я-А) else ordinary sort(А-Я)</param>
        public static List<ItemModel> QuickSort(List<ItemModel> list, string key, int type = 0)
        {
            QuickSort(list, key, 0, list.Count - 1, type);
            return list;
        }

        private static void Swap(List<ItemModel> list, int i, int j)
        {
            (list[i], list[j]) = (list[j], list[i]);
        }

        private static bool CompareStrings(string firstString, string secondString, int type = 0)
        {
            if (type == 1) return string.CompareOrdinal(firstString, secondString) < 0;
            return string.CompareOrdinal(firstString, secondString) > 0;
        }

        private static List<Item> RemoveDuplications(List<Item> entities)
        {
            var list = new List<Item>();
            foreach (var entity in entities)
                if (!list.Any(i => i.Id.Equals(entity.Id)))
                    list.Add(entity);
            return list;
        }
    }
}
