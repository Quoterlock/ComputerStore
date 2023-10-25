using ComputerStore.BusinessLogic.Domains;
using ComputerStore.BusinessLogic.Interfaces;
using ComputerStore.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic.Services
{
    public class ImagesService : IImagesService
    {
        public ImageModel ConvertEntityToModel(Image entity)
        {
            return new ImageModel()
            {
                Alt = entity.Alt,
                Bytes = entity.Bytes,
                Id = entity.Id
            };
        }

        public Image ConvertModelToEntity(ImageModel? model)
        {
            return new Image
            {
                Id = model.Id,
                Alt = model.Alt,
                Bytes = model.Bytes
            };
        }
    }
}
