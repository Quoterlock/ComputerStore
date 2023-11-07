using ComputerStore.DataAccess.Entities;
using ComputerStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public async Task AddAsync(Order item)
        {
            if(item != null)
            {
                await _context.AddAsync(item);
            }
            else throw new ArgumentNullException("order");
        }

        public async Task DeleteAsync(string id)
        {
            if(!string.IsNullOrEmpty(id))
            {
                var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
                if (order != null)
                {
                    _context.Orders.Remove(order);
                    return;
                }
                else throw new Exception("Order doesn't exist id:" + id);
            }
            else throw new ArgumentNullException("order id");
        }

        public async Task<List<Order>> GetAsync(Func<Order, bool> predicate)
        {
            return _context.Orders.Where(predicate).ToList()?? new List<Order>();
        }

        public async Task<List<Order>> GetAsync()
        {
            return await _context.Orders.ToListAsync()?? new List<Order>();
        }

        public async Task<Order?> GetAsync(string id)
        {
            if(!string.IsNullOrEmpty(id))
                return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            else 
                throw new ArgumentNullException("order id");
        }

        public async Task<bool> IsExists(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return await _context.Orders.AnyAsync(o => o.Id == id);
            } 
            return false;
        }

        public async Task UpdateAsync(Order item)
        {
            if (item != null && !string.IsNullOrEmpty(item.Id))
            {
                if(await _context.Orders.AnyAsync(o=>o.Id == item.Id))
                {
                    _context.Orders.Update(item);
                } else throw new ArgumentNullException("order.Id");
            }
            else throw new ArgumentNullException("order");
        }
    }
}
