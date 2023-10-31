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

namespace ComputerStore.Areas.Staff.Controllers
{
    [Area("Staff")]
    //[Authorize(Roles = RolesContainer.MANAGER)]
    public class ItemsController : Controller
    {
        private IItemsService _itemsService;
        private ICategoriesService _categoriesService;
        public ItemsController(IItemsService itemsService, ICategoriesService categoriesService)
        {
            _itemsService = itemsService;
            _categoriesService = categoriesService;
        }

        // GET: Items/?categoryId
        public async Task<IActionResult> Index(string categoryId)
        {
            var items = new List<ItemModel>();
            if (!string.IsNullOrEmpty(categoryId))
                items = await _itemsService.GetFromCategoryAsync(categoryId);
            else 
                items = await _itemsService.GetAllAsync();

            return View(items);
        }


        public async Task<IActionResult> Search(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                var items = await _itemsService.SearchAsync(value);
                ViewData["SearchValue"] = value;
                return View(items);
            }
            return NotFound();
        }

        // GET: Items/Details/5
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
                return NotFound();
            }
        }

        // GET: Items/Create
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
                item.Image = new ImageModel();
                item.Image.Bytes = await FileToBytes(model.ImageFile);
                item.Image.Alt = model.Item.Name + "-thumbnail";
                await _itemsService.AddAsync(item);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Create));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string itemId)
        {
            if (string.IsNullOrEmpty(itemId)) 
                return NotFound();
            
            var item = await _itemsService.GetByIdAsync(itemId);
            
            if (item != null)
            {
                var categories = await _categoriesService.GetAllAsync();
                var model = new ItemFormModel
                {
                    Item = item,
                    Categories = categories,
                    SelectedCategoryId = item.Category.Id
                };
                return View(model);
            }
            else 
                return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ItemFormModel model)
        {
            var item = model.Item;

            if (item != null && string.IsNullOrEmpty(item.Id)) 
                return NotFound();

            if(!string.IsNullOrEmpty(item.Name) && !string.IsNullOrEmpty(item.Id))
            {
                if (!string.IsNullOrEmpty(model.SelectedCategoryId))
                    item.Category = await _categoriesService.GetAsync(model.SelectedCategoryId);
                
                await _itemsService.UpdateAsync(item);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Edit), item.Id);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string itemId)
        {
            if (!string.IsNullOrEmpty(itemId))
            {
                var item = await _itemsService.GetByIdAsync(itemId);   
                if(item != null) return View(item);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    return NotFound(ex.Message);
                }
            }
            return NotFound();
        }

        private async Task<byte[]> FileToBytes(IFormFile imageFile)
        {
            using (var stream = new MemoryStream())
            {
                await imageFile.CopyToAsync(stream);
                return stream.ToArray();
            }
        }
    }
}
