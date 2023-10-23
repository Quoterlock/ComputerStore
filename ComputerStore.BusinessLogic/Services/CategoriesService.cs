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
        public async Task<CategoryModel> Get(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var entity = await _unitOfWork.Categories.GetById(id);
                if (entity != null)
                    return ConvertEntityToModel(entity);
                else throw new Exception("Category not found by id: " + id);
            }
            else throw new Exception("Category id is null or empty");
        }

        public async Task<List<CategoryModel>> GetAll()
        {
            var entities = await _unitOfWork.Categories.Get();
            return ConvertEntitiesToModels(entities);
        }

        private List<CategoryModel> ConvertEntitiesToModels(List<Category> entities) 
        {
            var models = new List<CategoryModel>();
            foreach (var entity in entities)
                models.Add(ConvertEntityToModel(entity));
            return models;
        }

        internal CategoryModel ConvertEntityToModel(Category entity)
        {
            return new CategoryModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Thumbnail = _imageService.ConvertEntityToModel(entity.Image)
            };        
        }
    }
}
