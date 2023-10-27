using ComputerStore.DataAccess.Entities;
using ComputerStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.DataAccess
{
    public class ImagesRepository : IImagesRepository
    {
        private ApplicationDbContext _context;
        public ImagesRepository(ApplicationDbContext applicationDbContext) 
        {
            _context = applicationDbContext;
        }
        public Task AddAsync(Image item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Image>> GetAsync(Func<Image, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<Image>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Image> GetAsync(string id)
        {
            return await _context.Images.SingleOrDefaultAsync(i=>i.Id == id);   
        }

        public Task<bool> IsExists(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Image item)
        {
            throw new NotImplementedException();
        }
    }
}
