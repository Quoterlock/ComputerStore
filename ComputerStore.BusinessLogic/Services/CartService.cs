using ComputerStore.BusinessLogic.Adapters;
using ComputerStore.BusinessLogic.Domains;
using ComputerStore.BusinessLogic.Interfaces;
using ComputerStore.DataAccess.Entities;
using ComputerStore.DataAccess.Interfaces;

namespace ComputerStore.BusinessLogic.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrdersService _ordersService;
        private readonly IEntityToModelAdapter<Item, ItemModel> _itemAdapter;

        public CartService(IUnitOfWork unitOfWork, 
            IOrdersService ordersService,
            IEntityToModelAdapter<Item, ItemModel> itemAdapter)
        {
            _unitOfWork = unitOfWork;
            _ordersService = ordersService;
            _itemAdapter = itemAdapter;
        }

        public async Task AddItemAsync(string userId, string itemId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException("userId");
            if (string.IsNullOrEmpty(itemId))
                throw new ArgumentNullException("itemId");

            try
            {
                var cart = await _unitOfWork.UserCart.GetUserCartAsync(userId);
                if (await _unitOfWork.Items.IsExistsAsync(itemId))
                {
                    cart.ItemsIDs.Add(itemId);
                    await _unitOfWork.CommitAsync();
                }
                else throw new Exception("Item doesn't exist: " + itemId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Dictionary<ItemModel, int>> GetItemsAsync(string userId)
        {
            if(string.IsNullOrEmpty(userId))
            {
                try
                {
                    var cart = await _unitOfWork.UserCart.GetUserCartAsync(userId);
                    var items = new Dictionary<ItemModel, int>();

                    foreach (var itemID in cart.ItemsIDs)
                    {
                        var entity = await _unitOfWork.Items.GetAsync(itemID);
                        var itemModel = _itemAdapter.ToModel(entity);
                        
                        if (items.Keys.Any(k => k.Id == itemModel.Id))
                        {
                            var key = items.Keys.FirstOrDefault(k => k.Id == itemModel.Id);
                            if(key != null) 
                                items[key]++;
                        }
                        else 
                            items.Add(itemModel, 1);
                    }
                    return items;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else throw new ArgumentNullException("user Id");
        }

        public async Task<int> GetTotalCostAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                var cart = await _unitOfWork.UserCart.GetUserCartAsync(userId);
                int sum = 0;
                foreach (var itemId in cart.ItemsIDs)
                {
                    var item = await _unitOfWork.Items.GetAsync(itemId);
                    if (item != null)
                        sum += item.Price;
                }
                return sum;
            }
            else throw new ArgumentNullException("userId");
        }

        public async Task RemoveItemAsync(string userId, string itemId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException("userId");
            if (string.IsNullOrEmpty(itemId))
                throw new ArgumentNullException("itemId");

            var cart = await _unitOfWork.UserCart.GetUserCartAsync(userId);
            cart.ItemsIDs.Remove(itemId);
            await _unitOfWork.CommitAsync();
        }

        public async Task ClearAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException("userId");

            await _unitOfWork.UserCart.RemoveItems(userId);
            await _unitOfWork.CommitAsync();
        }

        public async Task MakeOrderAsync(OrderModel order, string userId)
        {
            if (!string.IsNullOrEmpty(userId) && order != null)
            {
                order.Items = await GetItemsAsync(userId);
                if (order.Items.Count <= 0)
                    throw new Exception("Items count is null");

                order.CreationDate = DateTime.Now.ToUniversalTime();
                order.LastUpdateTime = DateTime.Now.ToUniversalTime();

                try
                {
                    await _ordersService.AddAsync(order);
                    await ClearAsync(userId);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else throw new ArgumentNullException("order or userId is null");
        }

        public async Task RemoveAllByIdAsync(string userId, string itemId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException("userId");
            if (string.IsNullOrEmpty(itemId))
                throw new ArgumentNullException("itemId");

            var cart = await _unitOfWork.UserCart.GetUserCartAsync(userId);
            cart.ItemsIDs.RemoveAll(id => id == itemId);
            await _unitOfWork.CommitAsync();
        }
    }
}
