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
        private readonly ICartService _cartService;
        private readonly IOrdersService _ordersService;

        public CartController(ICartService cartService, IOrdersService ordersService)
        { 
            _cartService = cartService;
            _ordersService = ordersService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var model = new OrderFormViewModel() 
            {
                Items = await _cartService.GetItemsAsync(userId),
                TotalCost = await _cartService.GetTotalCostAsync(userId)
            };
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
                CustomerComment = model.Comment
            };

            try
            {
                await _cartService.MakeOrderAsync(order, userId);
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
            var userId = GetUserId();
            if (!string.IsNullOrEmpty(userId))
            {
                if (!string.IsNullOrEmpty(itemId))
                {
                    try
                    {
                        await _cartService.AddItemAsync(userId, itemId);
                    }
                    catch (Exception ex) 
                    { 
                        // TODO: notify user 
                    }
                    return RedirectToAction(nameof(Index));
                }
                else return NotFound(itemId);
            }
            else return RedirectToAction("Login", "Account", new { area = "Identity" });
        }

        [HttpGet]
        public async Task<IActionResult> Remove(string itemId)
        {
            if (!string.IsNullOrEmpty(itemId))
            {
                var userId = GetUserId();

                if(userId != null)
                    await _cartService.RemoveItemAsync(userId, itemId);

                return RedirectToAction(nameof(Index));
            }
            else return NotFound(itemId);         
        }

        [HttpGet]
        public async Task<IActionResult> RemoveAll(string itemId)
        {
            if (!string.IsNullOrEmpty(itemId))
            {
                var userId = GetUserId();
                if (userId != null)
                    await _cartService.RemoveAllByIdAsync(userId, itemId);
                return RedirectToAction(nameof(Index));
            }
            else return NotFound(itemId);
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
