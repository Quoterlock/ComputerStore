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
        public IUserCartRepository UserCart { get; set; }
        public IItemsRepository Items { get; set; }
        public ICategoriesRepository Categories { get;  set; }
        public IOrdersRepository Orders { get; set; }
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

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Rollback()
        {
            //RollBack Here
        }
    }
}
