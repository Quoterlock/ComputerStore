using ComputerStore.BusinessLogic.Domains;
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
        public Task Add(Order item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> Get(Func<Order, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Order item)
        {
            throw new NotImplementedException();
        }
    }
}
