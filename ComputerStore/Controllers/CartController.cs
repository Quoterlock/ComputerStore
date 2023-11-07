﻿using ComputerStore.BusinessLogic.Domains;
using ComputerStore.BusinessLogic.Interfaces;
using ComputerStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ICartService _cartService;
        private IOrdersService _ordersService;
        public CartController(ICartService cartService, IOrdersService ordersService)
        { 
            _cartService = cartService;
            _ordersService = ordersService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var model = new OrderFormViewModel();
            model.Items = await _cartService.GetItems(userId);
            model.TotalCost = await _cartService.GetTotalCost(userId);
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> MakeOrder(OrderFormViewModel model)
        {
            var userId = GetUserId();

          
            var order = new OrderModel()
            {
                TotalCost = model.TotalCost,
                PostOfficeAddress = model.PostOfficeAddress,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Status = Utilities.OrderStatus.Pending,
                CreationDate = DateTime.Now.ToUniversalTime(),
                LastUpdateTime = DateTime.Now.ToUniversalTime(),
                Items = new List<ItemModel>()
            };

            var items = await _cartService.GetItems(userId);
            foreach(var item in items)
                for (int i = 0; i < item.Value; i++)
                    order.Items.Add(item.Key);

            if(order.Items.Count <= 0)
                return RedirectToAction(nameof(Index));

            try
            {
                await _ordersService.Add(order);
                await _cartService.Clear(userId);
                return RedirectToAction(nameof(Success));
            }
            catch(Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Add(string itemId)
        {
            var previousUrl = Request.Headers["Referer"].ToString();
            
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account", new { area = "Identity" });

            if (!string.IsNullOrEmpty(itemId))
                try
                {
                    await _cartService.AddItem(userId, itemId);
                }
                catch(Exception ex) 
                { 
                    return RedirectToAction(nameof(Index)); 
                }
            else 
                return NotFound(itemId);

            if (!string.IsNullOrEmpty(previousUrl) && Url.IsLocalUrl(previousUrl))
                return Redirect(previousUrl);
            else 
                return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Remove(string itemId)
        {
            // redirect to previous view
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }

        private string? GetUserId()
        {
            var id = User
                .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?
                .Value;
            return id;
        }

    }
}
