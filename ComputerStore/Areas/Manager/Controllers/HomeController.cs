using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Areas.Manager.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
