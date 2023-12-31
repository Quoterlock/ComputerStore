﻿using ComputerStore.Areas.Customer.ViewModels;
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
        private readonly IItemsService _itemsService;
        private readonly ICategoriesService _categoriesService;

        public ItemsController(IItemsService itemsService, ICategoriesService categoriesService)
        {
            _itemsService = itemsService;
            _categoriesService = categoriesService;
        }

        public async Task<IActionResult> Index(string categoryId, string sortBy)
        {
            List<ItemModel>? items;
            var model = new ItemsListViewModel();
            var category = new CategoryModel();

            if (!string.IsNullOrEmpty(categoryId))
            {
                items = await _itemsService.GetFromCategoryAsync(categoryId);
                category = await _categoriesService.GetAsync(categoryId);
            }
            else
                items = await _itemsService.GetAllAsync();

            model.Items = _itemsService.Sort(items, sortBy);
            model.SortBy = sortBy ?? SortMode.ItemId.ToString();

            if (category != null)
            {
                model.Title = category.Name;
                model.CategoryID = categoryId;
            }
            else
                model.Title = "All";

            return View(model);
        }

        public async Task<IActionResult> Details(string itemId)
        {
            if (string.IsNullOrEmpty(itemId)) 
                return NotFound();
            
            try
            {
                var item = await _itemsService.GetByIdAsync(itemId);
                return View(item);
            }
            catch(Exception ex)
            {
                return ErrorMessage(ex);
            }
        }

        public async Task<IActionResult> Search(string value, string sortBy)
        {
            if (!string.IsNullOrEmpty(value))
            {
                ViewData["SearchValue"] = value;

                var model = new ItemsListViewModel();
                model.Items = await _itemsService.SearchAsync(value) ?? new List<ItemModel>();
                model.Title = "Search";
                model.SortBy = sortBy;
                model.Items = _itemsService.Sort(model.Items, sortBy);

                return View(model);
            }
            else 
                return RedirectToAction(nameof(Index));
        }

        private IActionResult ErrorMessage(Exception ex)
        {
            return View("Error", ex.Message);
        }
    }
}
