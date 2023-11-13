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
        public async Task<IActionResult> Edit(string orderId)
        {
            if (!string.IsNullOrEmpty(orderId))
            {
                var order = await _ordersService.GetById(orderId);
                return View(order);
            }
            else return NotFound(orderId);
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
            // (!!!!!!)
            // change only info-properties (like name, address)
            // nor items list.
            // (!!!!!!) 
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> SetStatus(string orderId, string newOrderStatus)
        {
            if (string.IsNullOrEmpty(orderId))
                return NotFound();
            if (!string.IsNullOrEmpty(newOrderStatus))
            {
                try
                {
                    await _ordersService.SetStatus(orderId, newOrderStatus);
                } 
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return RedirectToAction(nameof(Details), new { orderId });
        }

        [HttpGet]
        public async Task<IActionResult> RemoveItem(string itemId, string orderId)
        {
            if(!string.IsNullOrEmpty(itemId) && !string.IsNullOrEmpty(orderId))
            {
                await _ordersService.RemoveItem(itemId, orderId);
                return RedirectToAction(nameof(Edit), new { orderId });
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> AddItem(string itemId, string orderId)
        {
            if (!string.IsNullOrEmpty(itemId) && !string.IsNullOrEmpty(orderId))
            {
                await _ordersService.AddItem(itemId, orderId);
                return RedirectToAction(nameof(Edit), new { orderId });
            }
            return NotFound();
        }

    }
}
