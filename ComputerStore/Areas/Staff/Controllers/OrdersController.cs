using ComputerStore.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ComputerStore.Areas.Staff.Controllers
{
    [Area("Staff")]
    public class OrdersController : Controller
    {
        private IOrdersService _ordersService;
        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = await _ordersService.GetAll();
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string orderId)
        {
            if (!string.IsNullOrEmpty(orderId))
            {
                var order = await _ordersService.GetById(orderId);
                return View(order);
            }
            else return NotFound(orderId);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var order = await _ordersService.GetById(id);
                return View(order);
            }
            else return NotFound(id);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                await _ordersService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            else return NotFound(id);
        }

        [HttpPost]
        public IActionResult Edit()
        {
            // post changes to db
            return RedirectToAction(nameof(Index));
        }

    }
}
