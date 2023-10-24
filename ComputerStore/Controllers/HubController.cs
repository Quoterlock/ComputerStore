using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers
{
    public class HubController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
