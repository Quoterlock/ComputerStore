using ComputerStore.DataAccess.Interfaces;

namespace ComputerStore.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        public IUserCartRepository UserCart { get; set; }
        public IItemsRepository Items { get; set; }
        public ICategoriesRepository Categories { get;  set; }
        public IOrdersRepository Orders { get; set; }

        public UnitOfWork(
            ApplicationDbContext context, 
            IOrdersRepository orders,
            ICategoriesRepository categories,
            IItemsRepository items,
            IUserCartRepository userCart)
        { 

            _context = context;
            UserCart = userCart;
            Items = items;
            Categories = categories;
            Orders = orders;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task RollbackAsync()
        {
            //RollBack Here
        }
    }
}
