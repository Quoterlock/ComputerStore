using ComputerStore.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Areas.Customer.Controllers
{
    public class CategoriesController : Controller
    {
        private ICategoriesService _categoriesService;
        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoriesService.GetAll();
            return View(categories);
        }
    }
}
