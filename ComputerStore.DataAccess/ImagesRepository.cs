using ComputerStore.DataAccess.Entities;
using ComputerStore.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.DataAccess
{
    public class ImagesRepository : IImagesRepository
    {
        public Task Add(Image item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Image>> Get(Func<Image, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<Image>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Image> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Image item)
        {
            throw new NotImplementedException();
        }
    }
}
