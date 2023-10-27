using ComputerStore.BusinessLogic.Domains;
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
        private IUnitOfWork _unitOfWork;
        private IImagesService _imageService;
        
        public CategoriesService(IUnitOfWork unitOfWork, IImagesService imageService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
        }
        
        public async Task<CategoryModel> GetAsync(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var entity = await _unitOfWork.Categories.GetAsync(id);
                if (entity != null)
                    return ConvertEntityToModel(entity);
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
                var entity = ConvertModelToEntity(model);
                await _unitOfWork.Categories.AddAsync(entity);
                await _unitOfWork.Commit();
            }
            else throw new Exception("Model in null!");
        }

        public async Task UpdateAsync(CategoryModel model)
        {
            if(model != null)
            {
                var entity = ConvertModelToEntity(model);
                await _unitOfWork.Categories.UpdateAsync(entity);
                await _unitOfWork.Commit();
            }
            else throw new Exception("Model in null!");
        }

        public async Task RemoveAsync(string id)
        {
            await _unitOfWork.Categories.DeleteAsync(id);
            await _unitOfWork.Commit();
        }

        private List<CategoryModel> ConvertEntitiesToModels(List<Category> entities)
        {
            var models = new List<CategoryModel>();
            foreach (var entity in entities)
            {
                var model = ConvertEntityToModel(entity);
                if(model != null)
                    models.Add(model);
            }
            return models;
        }

        public CategoryModel ConvertEntityToModel(Category entity)
        {
            return new CategoryModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Thumbnail = _imageService.ConvertEntityToModel(entity.Image)
            };        
        }
        
        public Category ConvertModelToEntity(CategoryModel model)
        {
            return new Category
            {
                Id = model.Id,
                Name = model.Name,
                Image = _imageService.ConvertModelToEntity(model.Thumbnail)
            };
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _unitOfWork.Categories.IsExists(id);
        }
    }
}
