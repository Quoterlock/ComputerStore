using ComputerStore.BusinessLogic.Domains;
using ComputerStore.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic.Adapters
{
    public class ItemAdapter : IEntityToModelAdapter<Item, ItemModel>
    {
        public Item ToEntity(ItemModel model)
        {
            return new Item()
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Brief = model.Brief,
                ImageBytes = model.Image.Bytes,
                CategoryID = model.Category.Id
            };
        }

        public ItemModel ToModel(Item entity)
        {
            return new ItemModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Brief = entity.Brief,
                Image = new ImageModel
                {
                    Bytes = entity.ImageBytes,
                    Alt = entity.Name
                },
                Price = entity.Price,
                Category = new CategoryModel
                {
                    Id = entity.CategoryID,
                },
            };
        }
    }
}
