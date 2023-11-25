using ComputerStore.DataAccess.Entities;
using ComputerStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.DataAccess
{
    public class UserCartRepository : IUserCartRepository
    {
        private readonly ApplicationDbContext _context;

        public UserCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserCart item)
        {
            if (item != null)
                await _context.AddAsync(item);
            else 
                throw new ArgumentNullException("User card");
        }

        public async Task DeleteAsync(string id)
        {
            if (id != null)
            {
                var cart = await _context.UserCarts.FirstOrDefaultAsync(c => c.Id == id);

                if (cart != null)
                    _context.UserCarts.Remove(cart);
                else
                    throw new Exception("Cart not found id: " + id);
            }
            else throw new ArgumentNullException("user cart id");
        }

        public async Task<UserCart> GetAsync(string id)
        {
            if(!string.IsNullOrEmpty(id))
            {
                var cart = await _context.UserCarts.FirstOrDefaultAsync(i => i.Id == id);

                if (cart == null)
                    throw new Exception("User cart not found id: " + id);
                else
                    return cart;
            }
            else throw new ArgumentNullException("cart id");
        }

        public async Task<UserCart> GetUserCartAsync(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                if (!await _context.UserCarts.AnyAsync(i=>i.UserId == userId))
                {
                    await AddAsync(new UserCart() { UserId = userId });
                    await _context.SaveChangesAsync();
                }

                var cart = await _context.UserCarts
                    .FirstOrDefaultAsync(i => i.UserId == userId);

                if (cart != null)
                    return cart;
                else 
                    throw new Exception("UserCart didn't created!");
            }
            else throw new ArgumentNullException("user ID");
        }

        public async Task RemoveItems(string userId)
        {
            if(!string.IsNullOrEmpty(userId))
            {
                var cart = await GetUserCartAsync(userId);
                cart.ItemsIDs.Clear();
                _context.Update(cart);
            }
            else throw new ArgumentNullException("userId");
        }

        public async Task UpdateAsync(UserCart item)
        {
            if (item != null && await _context.UserCarts.AnyAsync(i => i.Id == item.Id))
                _context.Update(item);
            else 
                throw new ArgumentNullException("item is null or doesn't exist");
        }

        public Task<bool> IsExistsAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserCart>> GetAsync(Func<UserCart, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserCart>> GetAsync()
        {
            throw new NotImplementedException();
        }
    }
}
