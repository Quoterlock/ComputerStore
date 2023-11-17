using ComputerStore.DataAccess.Entities;
using ComputerStore.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreTests.ServicesTests.FakeModules
{
    public class FakeCategoriesRepo : ICategoriesRepository
    {
        public FakeCategoriesRepo() { }
        public List<Category> Data { get; set; } = new List<Category>() {
            new Category { Id = "1", Image = null, Name = "FakeCategory"}
        };

        public Task AddAsync(Category item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetAsync(Func<Category, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Category>> GetAsync()
        {
            return Data;
        }

        public Task<bool> IsExists(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Category item)
        {
            throw new NotImplementedException();
        }
    }
}
