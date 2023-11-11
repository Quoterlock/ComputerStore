using ComputerStore.Areas.Customer.ViewModels;
using ComputerStore.BusinessLogic.Domains;
using ComputerStore.BusinessLogic.Interfaces;
using ComputerStore.DataAccess.Entities;
using ComputerStore.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Identity.Client;

namespace ComputerStore.Controllers
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

        public async Task<IActionResult> Index(string categoryId, string sortBy)
        {
            string listTitle = "All";
            List<ItemModel>? items;
            var model = new ItemsListViewModel();

            if (!string.IsNullOrEmpty(categoryId))
            {
                items = await _itemsService.GetFromCategoryAsync(categoryId);
                var category = await _categoriesService.GetAsync(categoryId);
                model.Category = category.Name; 
                model.CategoryID = categoryId;
                listTitle = model.Category ?? "All";
            }
            else
                items = await _itemsService.GetAllAsync();

            var sortMode = GetSortEnum(sortBy);
            if (sortMode!=SortMode.ItemId)
                items = _itemsService.Sort(items, GetSortEnum(sortBy));

            model.Items = items ?? new List<ItemModel>();
            model.Count = model.Items.Count;
            model.Title = listTitle;
            model.SortBy = sortMode;

            return View(model);
        }

        public async Task<IActionResult> Details(string itemId)
        {
            if(!string.IsNullOrEmpty(itemId))
            {
                try
                {
                    var item = await _itemsService.GetByIdAsync(itemId);
                    return View(item);
                }
                catch(Exception ex)
                {
                    return NotFound();
                }
            }
            
            return NotFound();
        }

        public async Task<IActionResult> Search(string value, string sortBy)
        {
            if (!string.IsNullOrEmpty(value))
            {
                ViewData["SearchValue"] = value;

                var model = new ItemsListViewModel();

                model.Items = await _itemsService.SearchAsync(value) ?? new List<ItemModel>();
                model.Count = model.Items.Count;
                model.Title = "Search";

                var sortMode = GetSortEnum(sortBy);
                if (sortMode != SortMode.ItemId)
                    model.Items = _itemsService.Sort(model.Items, GetSortEnum(sortBy));

                model.SortBy = sortMode;

                return View(model);
            }
            else return RedirectToAction(nameof(Index));
        }

        private SortMode GetSortEnum(string sortBy)
        {
            if (sortBy == null) return SortMode.ItemId;
            if (sortBy.Equals("costUp")) return SortMode.CostUp;
            if (sortBy.Equals("costDown")) return SortMode.CostDown;
            else return SortMode.ItemId;
        }
    }
}
