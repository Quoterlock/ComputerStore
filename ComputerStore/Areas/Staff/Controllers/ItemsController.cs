using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.CompilerServices;
using System.Net;
using ComputerStore.ViewModels;
using ComputerStore.BusinessLogic.Interfaces;
using ComputerStore.BusinessLogic.Domains;
using ComputerStore.Utilities;

namespace ComputerStore.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Authorize(Roles = RolesContainer.MANAGER + "," + RolesContainer.ADMINISTRATOR)]
    public class ItemsController : Controller
    {
        private readonly IItemsService _itemsService;
        private readonly ICategoriesService _categoriesService;
        public ItemsController(IItemsService itemsService, ICategoriesService categoriesService)
        {
            _itemsService = itemsService;
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string categoryId)
        {
            List<ItemModel> items;
            if (!string.IsNullOrEmpty(categoryId))
                items = await _itemsService.GetFromCategoryAsync(categoryId);
            else 
                items = await _itemsService.GetAllAsync();

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string itemId)
        {
            if (string.IsNullOrEmpty(itemId)) 
                return NotFound();
            try
            {
                var item = await _itemsService.GetByIdAsync(itemId);
                return View(item);
            }
            catch (Exception ex)
            {
                return ErrorMessage(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create(string categoryId)
        {
            var model = new ItemFormModel();
            model.Item = new ItemModel();
            model.Categories = await _categoriesService.GetAllAsync();
            if (!string.IsNullOrEmpty(categoryId) && model.Categories.FirstOrDefault(c =>c.Id == categoryId) != null)
                model.SelectedCategoryId = categoryId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemFormModel model)
        {
            var item = model.Item;
            if (item != null && item.Name != null && !string.IsNullOrEmpty(model.SelectedCategoryId))
            {
                item.Category = new CategoryModel() { Id = model.SelectedCategoryId };
                if(model.ImageFile != null)
                {
                    item.Image = new ImageModel();
                    item.Image.Bytes = await FileToBytes(model.ImageFile);
                    item.Image.Alt = item.Name + "-thumbnail";
                } 
                else return View(item);

                await _itemsService.AddAsync(item);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Create));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string itemId)
        {
            if (!string.IsNullOrEmpty(itemId))
            {
                var item = await _itemsService.GetByIdAsync(itemId);
                if (item != null)
                {
                    var categories = await _categoriesService.GetAllAsync();
                    var model = new ItemFormModel
                    {
                        Item = item,
                        Categories = categories,
                        SelectedCategoryId = item.Category != null ?
                            item.Category.Id : string.Empty
                    };
                    return View(model);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ItemFormModel model)
        {
            var item = model.Item;

            if (item != null && !string.IsNullOrEmpty(item.Id))
            {
                if (!string.IsNullOrEmpty(item.Name) && !string.IsNullOrEmpty(item.Id))
                {
                    if (!string.IsNullOrEmpty(model.SelectedCategoryId))
                        item.Category = await _categoriesService.GetAsync(model.SelectedCategoryId);
                    await _itemsService.UpdateAsync(item);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Edit), "Items", new { itemId = item.Id });
            }
            else return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string itemId)
        {
            if (!string.IsNullOrEmpty(itemId))
            {
                var item = await _itemsService.GetByIdAsync(itemId);   
                if(item != null) 
                    return View(item);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    await _itemsService.RemoveAsync(id);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return ErrorMessage(ex);
                }
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Search(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                ViewData["SearchValue"] = value;
                return View(nameof(Index), await _itemsService.SearchAsync(value));
            }
            return RedirectToAction(nameof(Index));
        }

        private static async Task<byte[]> FileToBytes(IFormFile imageFile)
        {
            var stream = new MemoryStream();
            await imageFile.CopyToAsync(stream);
            return stream.ToArray();
        }

        private IActionResult ErrorMessage(Exception ex)
        {
            return View("Error", ex.Message);
        }
    }
}
