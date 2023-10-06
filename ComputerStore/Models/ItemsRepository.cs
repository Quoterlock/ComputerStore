using ComputerStore.Models.Domains;

namespace ComputerStore.Models
{
    public class ItemsRepository : IRepository<Item>
    {
        public Task Add(Item item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Item>> Get(Func<Item, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<Item>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public bool IsValid(Item item)
        {
            throw new NotImplementedException();
        }

        public Task Update(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
