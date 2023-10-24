using ComputerStore.Areas.Customer.ViewModels;
using ComputerStore.BusinessLogic.Domains;
using ComputerStore.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Identity.Client;

namespace ComputerStore.Controllers
{
    [Area("Customer")]
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
            string listTitle = "All";
            List<ItemModel>? items;
            if (!string.IsNullOrEmpty(categoryId))
            {
                items = await _itemsService.GetFromCategory(categoryId);
                listTitle = (await _categoriesService.Get(categoryId)).Name ?? "All";
            }
            else
                items = await _itemsService.GetAll();

            var model = new ItemsListViewModel();
            model.Items = items ?? new List<ItemModel>();
            model.Count = model.Items.Count;
            model.Title = listTitle;

            return View(model);
        }
        public IActionResult Details(string itemId)
        {
            // get item and return full details
            return View();
        }

        public async Task<IActionResult> Search(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                ViewData["SearchValue"] = value;

                var model = new ItemsListViewModel();

                model.Items = await _itemsService.Search(value) ?? new List<ItemModel>();
                model.Count = model.Items.Count;
                model.Title = "Search";
                model.SortBy = Utilities.SortMode.ItemId;

                return View(model);

            }
            else return RedirectToAction(nameof(Index));
        }
    }
}
