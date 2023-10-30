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
            var items = await _itemsService.GetFromCategoryAsync(categoryId);
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
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();
            try
            {
                var item = await _itemsService.GetByIdAsync(id);
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

        // POST: Items/Create
        [HttpPost]
        public async Task<IActionResult> Create(ItemFormModel model)
        {
            
            var item = model.Item;
            if (item != null && item.Name != null && !string.IsNullOrEmpty(model.SelectedCategoryId))
            {
                item.Category = await _categoriesService.GetAsync(model.SelectedCategoryId); /* new CategoryModel() { Id = model.SelectedCategoryId }; */
                item.Image = new ImageModel();
                item.Image.Bytes = await FileToBytes(model.ImageFile);
                item.Image.Alt = model.Item.Name + "-thumbnail";
                await _itemsService.AddAsync(item);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Create));
        }

        private async Task<byte[]> FileToBytes(IFormFile imageFile)
        {
            using (var stream = new MemoryStream())
            {
                await imageFile.CopyToAsync(stream);
                return stream.ToArray();
            }
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) 
                return NotFound();
            
            var item = await _itemsService.GetByIdAsync(id);
            
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

        // POST: Items/Edit/5
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

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            return View(id);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
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
    }
}
