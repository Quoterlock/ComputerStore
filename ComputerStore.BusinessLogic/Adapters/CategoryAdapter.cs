using ComputerStore.BusinessLogic.Domains;
using ComputerStore.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic.Adapters
{
    public class CategoryAdapter : IEntityToModelAdapter<Category, CategoryModel>
    {
        public Category ToEntity(CategoryModel model)
        {
            return new Category
            {
                Id = model.Id,
                Name = model.Name,
                ImageBytes = model.Thumbnail.Bytes
            };
        }

        public CategoryModel ToModel(Category entity)
        {
            return new CategoryModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Thumbnail = new ImageModel
                {
                    Bytes = entity.ImageBytes,
                    Alt = entity.Name
                }
            };
        }
    }
}
