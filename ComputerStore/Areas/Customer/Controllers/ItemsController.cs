using ComputerStore.Areas.Customer.ViewModels;
using ComputerStore.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace ComputerStore.Areas.Customer.Controllers
{
    public class ItemsController : Controller
    {
        private IItemsService _itemsService;
        private ICategoriesService _categoriesService;
        public ItemsController(IItemsService itemsService, ICategoriesService categoriesService) 
        {
            _itemsService = itemsService;
            _categoriesService = categoriesService;
        }

        public async Task<IActionResult> Index(string categoryId)
        {
            var model = new ItemsListViewModel();
            if(!string.IsNullOrEmpty(categoryId))
            {
                var category = await _categoriesService.Get(categoryId);
                model.Items = await _itemsService.GetFromCategory(categoryId);
                model.Count = model.Items.Count;
                model.Tag = category.Name;
            }
            else
            {
                model.Items = await _itemsService.GetAll();
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
