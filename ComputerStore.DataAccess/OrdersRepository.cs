using ComputerStore.DataAccess.Entities;
using ComputerStore.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.DataAccess
{
    public class OrdersRepository : IOrdersRepository
    {
        private ApplicationDbContext _context;
        public OrdersRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task AddAsync(Order item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetAsync(Func<Order, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExists(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Order item)
        {
            throw new NotImplementedException();
        }
    }
}
