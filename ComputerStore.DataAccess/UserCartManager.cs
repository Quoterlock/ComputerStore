using ComputerStore.Data;
using ComputerStore.Models.Domains;
using ComputerStore.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Models
{
    public class UserCartManager : IUserCartManager
    {
        private ApplicationDbContext _context;
        public UserCartManager(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task AddItem(string userId, string itemId)
        {
            var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == itemId);
            if(!string.IsNullOrEmpty(userId) && item != null && !string.IsNullOrEmpty(item.Id))
            {
                var cart = await _context.UserCarts.FirstOrDefaultAsync(cart => cart.UserId == userId);
                if (cart == null)
                {
                    cart = new UserCart
                    {
                        UserId = userId
                    };
                    _context.Add<UserCart>(cart);
                    await _context.SaveChangesAsync();
                }
                cart.Items.Add(item);
                _context.Update<UserCart>(cart);
                await _context.SaveChangesAsync();
                return;
            }
            throw new Exception("Add item failed (wrong user id or item)");
        }

        public Task Clear(string userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItem(string userId, string ItemId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserCart> Get(string userId)
        {
            var cart = await _context.UserCarts.Include(cart=>cart.Items).FirstOrDefaultAsync(cart => cart.UserId == userId);
            if(cart == null)
            {
                cart = new UserCart
                {
                    UserId = userId,
                    Items = new List<Item>()
                };
                _context.Add<UserCart>(cart);
                await _context.SaveChangesAsync();

            }
            return cart;
        }
    }
}
