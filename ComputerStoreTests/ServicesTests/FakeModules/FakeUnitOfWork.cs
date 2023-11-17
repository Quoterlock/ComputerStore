using ComputerStore.DataAccess.Interfaces;

namespace ComputerStoreTests.ServicesTests.FakeModules
{
    internal class FakeUnitOfWork : IUnitOfWork
    {
        public IUserCartRepository UserCart
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public IItemsRepository Items
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public ICategoriesRepository Categories
        {
            get => new FakeCategoriesRepo();
            set => throw new NotImplementedException();
        }
        public IOrdersRepository Orders
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public Task Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
