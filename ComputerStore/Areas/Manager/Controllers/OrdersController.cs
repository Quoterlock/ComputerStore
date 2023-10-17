using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ComputerStore.Areas.Manager.Controllers
{
    public class OrdersController : Controller
    {
        public OrdersController(UserManager<IdentityUser> userManager)
        {

        }

        [HttpGet]
        public IActionResult Index()
        {
            // get list of orders
            return View();
        }

        [HttpGet]
        public IActionResult Detail(string id)
        {
            // get detailed info about order
            return View();
        }

        [HttpGet]
        public IActionResult Edit(string id) 
        {
            // edit order
            return View();
        }

        [HttpPost]
        public IActionResult Delete(string id) 
        {
            // delete order
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit() 
        {
            // post changes to db
            return RedirectToAction(nameof(Index));
        }

    }
}
