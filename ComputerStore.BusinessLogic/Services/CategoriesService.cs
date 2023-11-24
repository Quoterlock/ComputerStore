﻿using ComputerStore.BusinessLogic.Domains;
using ComputerStore.BusinessLogic.Interfaces;
using ComputerStore.DataAccess.Entities;
using ComputerStore.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public CategoriesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<CategoryModel> GetAsync(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var entity = await _unitOfWork.Categories.GetAsync(id);
                if (entity != null)
                    return Convertor.ConvertEntityToModel(entity);
                else throw new Exception("Category not found by id: " + id);
            }
            else throw new Exception("Category id is null or empty string");
        }

        public async Task<List<CategoryModel>> GetAllAsync()
        {
            var entities = await _unitOfWork.Categories.GetAsync();
            return ConvertEntitiesToModels(entities);
        }

        public async Task AddAsync(CategoryModel model)
        {
            if (model != null)
            {
                var entity = Convertor.ConvertModelToEntity(model);
                await _unitOfWork.Categories.AddAsync(entity);
                await _unitOfWork.CommitAsync();
            }
            else throw new Exception("Model in null!");
        }

        public async Task UpdateAsync(CategoryModel model)
        {
            if(model != null && !string.IsNullOrEmpty(model.Id))
            {
                var entity = await _unitOfWork.Categories.GetAsync(model.Id);
                if (entity != null)
                {
                    if (model.Thumbnail != null && model.Thumbnail.Bytes.Length != 0)
                        entity.ImageBytes = model.Thumbnail.Bytes;
                    entity.Name = model.Name;

                    await _unitOfWork.Categories.UpdateAsync(entity);
                    await _unitOfWork.CommitAsync();
                }
                else throw new Exception("Category is not found with id: " + model.Id);
            }
            else throw new Exception("Model in null!");
        }

        public async Task RemoveAsync(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                await _unitOfWork.Categories.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            }
            else throw new ArgumentNullException("category id");
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _unitOfWork.Categories.IsExists(id);
        }

        public async Task<List<CategoryModel>> Search(string value)
        {
            value = value.ToLower();
            var entities = new List<Category>();
            entities.AddRange(await _unitOfWork.Categories.GetAsync(c => c.Name.ToLower().Contains(value)));
            return ConvertEntitiesToModels(entities);
        }

        private static List<CategoryModel> ConvertEntitiesToModels(List<Category> entities)
        {
            var models = new List<CategoryModel>();
            foreach (var entity in entities)
            {
                var model = Convertor.ConvertEntityToModel(entity);
                if(model != null)
                    models.Add(model);
            }
            return models;
        }
    }
}
