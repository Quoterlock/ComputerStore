﻿using ComputerStore.BusinessLogic.Domains;
using ComputerStore.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic.Interfaces
{
    public interface IImagesService
    {
        ImageModel ConvertEntityToModel(Image entity);
        Image ConvertModelToEntity(ImageModel? thumbnail);
        Task<ImageModel> GetById(string? id);
    }
}
