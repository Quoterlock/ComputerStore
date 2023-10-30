using ComputerStore.BusinessLogic.Domains;
using ComputerStore.BusinessLogic.Interfaces;
using ComputerStore.DataAccess.Entities;
using ComputerStore.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic.Services
{
    public class ImagesService : IImagesService
    {
        private IImagesRepository _imagesRepository;
        public ImagesService(IImagesRepository imagesRepository) 
        {
            _imagesRepository = imagesRepository;
        }

        public async Task<ImageModel> GetById(string? id)
        {
            try
            {
                var entity = await _imagesRepository.GetAsync(id);
                if (entity != null)
                    return Convertor.ConvertEntityToModel(entity);
                else 
                    throw new Exception("Cannot find image with id: " + id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
