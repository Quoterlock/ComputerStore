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
        private ApplicationDbContext _context;
        public UserCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserCart item)
        {
            if (item != null)
            {
                await _context.AddAsync(item);
            }
            else throw new ArgumentNullException("User card");
        }

        public async Task AddItem(string userId, string itemId)
        {
            if(string.IsNullOrEmpty(userId)) throw new ArgumentNullException("userId");
            if(string.IsNullOrEmpty(itemId)) throw new ArgumentNullException("itemId");

            var cart = await GetUserCart(userId);
            
            if (cart == null)
            {
                await AddAsync(new UserCart() { UserId = userId });
                cart = await GetUserCart(userId);
            }
            
            var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == itemId);
            if (item != null)
            {
                cart.Items.Add(item);
                _context.Update(cart);
            }
            else throw new Exception("item not found id:" + itemId);
        }

        public async Task DeleteAsync(string id)
        {
            if (id == null) throw new ArgumentNullException("user cart id");
            var cart = await _context.UserCarts.FirstOrDefaultAsync(c => c.Id == id);
            if (cart == null) throw new Exception("Cart not found id: " + id);
            _context.UserCarts.Remove(cart);
        }

        public async Task DeleteItem(string userId, string itemId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException("user id");
            if (string.IsNullOrEmpty(itemId)) throw new ArgumentNullException("item id");

            var cart = await _context.UserCarts.FirstOrDefaultAsync(i => i.UserId == userId);
            if (cart == null) throw new Exception("cart is empty");
            
            var item = cart.Items.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                cart.Items.Remove(item);
                _context.Update(cart);
            }
            else throw new Exception("can't find item in a cart");
        }

        public Task<List<UserCart>> GetAsync(Func<UserCart, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserCart>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<UserCart> GetAsync(string id)
        {
            if(string.IsNullOrEmpty(id)) throw new ArgumentNullException("cart id");

            var cart = await _context.UserCarts
                .FirstOrDefaultAsync(i => i.Id == id);
            if (cart == null)
                throw new Exception("User cart not found id: " + id);
            else return cart;
        }

        public async Task<UserCart> GetUserCart(string userId)
        {
            var cart = await _context.UserCarts
                .FirstOrDefaultAsync(i => i.UserId == userId);
            if (cart == null) 
                throw new Exception("User cart not found userId: " + userId);
            else return cart;
        }

        public Task<bool> IsExists(string id)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveItems(string userId)
        {
            if(string.IsNullOrEmpty(userId)) throw new ArgumentNullException("userId");
            var cart = await GetUserCart(userId);
            cart.Items.Clear();
            _context.Update(cart);
        }

        public async Task UpdateAsync(UserCart item)
        {
            if (item != null && await _context.UserCarts.AnyAsync(i => i.Id == item.Id))
                _context.Update(item);
            else throw new ArgumentNullException("item is null or doesn't exist");
        }
    }
}
