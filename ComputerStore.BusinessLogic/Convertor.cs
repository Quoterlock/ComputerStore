using ComputerStore.BusinessLogic.Domains;
using ComputerStore.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic
{
    internal static class Convertor
    {
        internal static Image ConvertModelToEntity(ImageModel? model)
        {
            return new Image
            {
                Id = model.Id,
                Alt = model.Alt,
                Bytes = model.Bytes
            };
        }

        internal static ImageModel ConvertEntityToModel(Image entity)
        {
            return new ImageModel()
            {
                Alt = entity.Alt,
                Bytes = entity.Bytes,
                Id = entity.Id
            };
        }

        public static CategoryModel ConvertEntityToModel(Category entity)
        {
            return new CategoryModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Thumbnail = ConvertEntityToModel(entity.Image)
            };
        }

        public static Category ConvertModelToEntity(CategoryModel model)
        {
            return new Category
            {
                Id = model.Id,
                Name = model.Name,
            };
        }

        internal static ItemModel EntityToModel(Item entity)
        {
            return new ItemModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Image = ConvertEntityToModel(entity.Image),
                Price = entity.Price,
                Category = new CategoryModel
                {
                    Id = entity.Category.Id,
                    Name = entity.Category.Name
                },
            };
        }

        internal static Item ConvertModelToEntity(ItemModel model)
        {
            return new Item()
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Image = ConvertModelToEntity(model.Image),
                Category = new Category() { Id = model.Category.Id }
            };
        }
    }
}
