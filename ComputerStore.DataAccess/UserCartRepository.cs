using ComputerStore.DataAccess.Entities;
using ComputerStore.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.DataAccess
{
    public class UserCartRepository : IUserCartRepository
    {
        private ApplicationDbContext _context;
        public UserCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task AddAsync(UserCart item)
        {
            throw new NotImplementedException();
        }

        public Task AddItem(string userId, string itemId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItem(string userId, string itemId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserCart>> GetAsync(Func<UserCart, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserCart>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserCart> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UserCart> GetUserCart(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExists(string id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveItems(string userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UserCart item)
        {
            throw new NotImplementedException();
        }
    }
}
