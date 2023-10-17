using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace ComputerStore.Areas.Customer.Controllers
{
    public class ItemsController : Controller
    {
        public IActionResult Index(string categoryId)
        {
            if(!string.IsNullOrEmpty(categoryId))
            {
                // get items from category
            }
            else
            {
                // get all items
            }
            return View(); // return items list view model
        }
        public IActionResult Details(string itemId) 
        {
            // get item and return full details
            return View();
        }

        public IActionResult Search(string value)
        {
            // find by value and return
            return View();
        }
    }
}
