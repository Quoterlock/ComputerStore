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
                try
                {
                    var entity = Convertor.ConvertModelToEntity(model);
                    await _unitOfWork.Items.AddAsync(entity);
                    await _unitOfWork.Commit();
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
                items = EntityesToModels(entities);
            return items;
        }

        public async Task<ItemModel> GetByIdAsync(string id)
        {
            try
            {
                var entity = await _unitOfWork.Items.GetAsync(id);
                if (entity != null)
                    return Convertor.EntityToModel(entity);
                throw new Exception("Cannot find the item id:" + id);
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
                var entities = await _unitOfWork.Items.GetAsync(item => item.Category.Id == categoryId);
                if (entities != null)
                    items = EntityesToModels(entities);
            }
            return items;
        }

        public async Task RemoveAsync(string id)
        {
            try
            {
                await _unitOfWork.Items.DeleteAsync(id);
                await _unitOfWork.Commit();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ItemModel>> SearchAsync(string value)
        {
            var entities = new List<Item>();
            var items = new List<ItemModel>();
            if(!string.IsNullOrEmpty(value))
            {
                entities.AddRange(await _unitOfWork.Items.GetAsync(i => i.Name.Contains(value)));
                entities.AddRange(await _unitOfWork.Items.GetAsync(i => i.Category.Name.Contains(value)));
                entities.AddRange(await _unitOfWork.Items.GetAsync(i => i.Description.Contains(value)));
            }
            foreach (var entity in entities)
                items.Add(Convertor.EntityToModel(entity));
            return items;
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

        public List<ItemModel> Sort(List<ItemModel> items, SortMode sort)
        {
            if (sort == SortMode.CostDown)
            {
                items = SortByCost(items);
                items.Reverse();
                return items;
            }
            if (sort == SortMode.CostUp)
            {
                return SortByCost(items);
            }
            if(sort == SortMode.Name)
            {
                return SortByName(items);
            }
            return items;
        }

        private List<ItemModel> SortByCost(List<ItemModel> items)
        {
            return QuickSort(items, "cost", 0);
        }
        private List<ItemModel> SortByName(List<ItemModel> items)
        {
            return QuickSort(items, "name", 1);
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

        /// <summary>
        /// Sort the list with objects
        /// </summary>
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
    }
}
