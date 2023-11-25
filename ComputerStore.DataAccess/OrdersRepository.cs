using ComputerStore.DataAccess.Entities;
using ComputerStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.DataAccess
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly ApplicationDbContext _context;
        public OrdersRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Order item)
        {
            if(item != null)
                await _context.AddAsync(item);
            else throw new ArgumentNullException("order entity");
        }

        public async Task DeleteAsync(string id)
        {
            if(!string.IsNullOrEmpty(id))
            {
                var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
                
                if (order != null)
                    _context.Orders.Remove(order);
                else throw new Exception("Order doesn't exist id:" + id);
            }
            else throw new ArgumentNullException("order entity id");
        }

        public async Task<List<Order>> GetAsync(Func<Order, bool> predicate)
        {
            return _context.Orders.Where(predicate).ToList() ?? new List<Order>();
        }

        public async Task<List<Order>> GetAsync()
        {
            return await _context.Orders.ToListAsync()?? new List<Order>();
        }

        public async Task<Order?> GetAsync(string id)
        {
            if(!string.IsNullOrEmpty(id))
                return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            else throw new ArgumentNullException("order entity id");
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            if (!string.IsNullOrEmpty(id))
                return await _context.Orders.AnyAsync(o => o.Id == id);
            else 
                return false;
        }

        public async Task UpdateAsync(Order item)
        {
            if (item != null && !string.IsNullOrEmpty(item.Id))
            {
                if(await _context.Orders.AnyAsync(o=>o.Id == item.Id))
                    _context.Orders.Update(item);
                else throw new Exception("order entity not found");
            }
            else throw new ArgumentNullException("order entity");
        }

        public async Task UpdateInfoAsync(Order order)
        {
            if (order != null && !string.IsNullOrEmpty(order.Id))
            {
                var entity = await _context.Orders.FirstOrDefaultAsync(o => o.Id == order.Id);
                
                if (entity != null)
                {
                    // update all except items
                    entity.FirstName = order.FirstName ?? string.Empty;
                    entity.LastName = order.LastName ?? string.Empty;
                    entity.Email = order.Email ?? string.Empty;
                    entity.PostOfficeAddress = order.PostOfficeAddress ?? string.Empty;
                    entity.CustomerComment = order.CustomerComment ?? string.Empty;
                    entity.PhoneNumber = order.PhoneNumber ?? string.Empty;
                }
                else throw new Exception("order entity not found");
            }
            else throw new ArgumentNullException("order entity");
        }
    }
}
