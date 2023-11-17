using ComputerStore.DataAccess.Entities;
using ComputerStore.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreTests.ServicesTests.FakeModules
{
    internal class FakeOrdersRepo : IOrdersRepository
    {
        private List<Order> orders;
        public FakeOrdersRepo() {
            orders = new List<Order>()
            {
                new Order
                {
                    Id = "1",
                    ItemsID = new List<string> { "1", "2", "3" },
                    Status = "Pending",
                },
                new Order
                {
                    Id = "2",
                    ItemsID = new List<string> { "1", "2", "3" },
                    Status = "Pending",
                },
                new Order
                {
                    Id = "3",
                    ItemsID = new List<string> { "1", "2", "3" },
                    Status = "Pending",
                },
            };
        }
        public Task AddAsync(Order item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetAsync(string id)
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

        public Task<bool> IsExists(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Order item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateInfoAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
