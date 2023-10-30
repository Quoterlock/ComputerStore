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
                Image = model.Thumbnail != null? ConvertModelToEntity(model.Thumbnail) : null
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
                Category = ConvertEntityToModel(entity.Category)
            };
        }

        internal static Item ConvertModelToEntity(ItemModel model)
        {
            var entity = new Item();
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Category = ConvertModelToEntity(model.Category);
            entity.Image = ConvertModelToEntity(model.Image);
            return entity;
        }
    }
}
