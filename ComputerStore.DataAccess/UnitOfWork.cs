using ComputerStore.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        public IUserCartRepository UserCart { get; private set; }
        public IItemsRepository Items { get; private set; }
        public ICategoriesRepository Categories { get; private set; }
        public IOrdersRepository Orders { get; private set; }
        public UnitOfWork(
            ApplicationDbContext context, 
            IOrdersRepository orders,
            ICategoriesRepository categories,
            IItemsRepository items,
            IUserCartRepository userCart){ 

            _context = context;
            UserCart = userCart;
            Items = items;
            Categories = categories;
            Orders = orders;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Rollback()
        {
            //RollBack Here
        }
    }
}
