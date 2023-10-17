using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Areas.Customer.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MakeOrder()
        {
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Add(string itemId)
        {
            // redirect to previous view
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Remove(string itemId) 
        {
            // redirect to previous view
            return RedirectToAction(nameof(Index));
        }
    }
}
