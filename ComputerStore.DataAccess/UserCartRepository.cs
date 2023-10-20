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
        public Task Add(UserCart item)
        {
            throw new NotImplementedException();
        }

        public Task AddItem(string userId, string itemId)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItem(string userId, string itemId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserCart>> Get(Func<UserCart, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserCart>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<UserCart> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UserCart> GetUserCart(string userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveItems(string userId)
        {
            throw new NotImplementedException();
        }

        public Task Update(UserCart item)
        {
            throw new NotImplementedException();
        }
    }
}
