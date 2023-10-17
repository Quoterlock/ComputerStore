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

namespace ComputerStore.Areas.Manager.Controllers
{
    //[Authorize(Roles = RolesContainer.MANAGER)]
    public class ItemsController : Controller
    {

        public ItemsController()
        {

        }

        // GET: Items/?categoryId
        public async Task<IActionResult> Index(string categoryId)
        {
            /*
            List<Item> items;
            if (categoryId != null)
            {
                items = await _repository.Get(item => item.Category.Id == categoryId);
                if (items != null && items.Count > 0)
                    ViewData["CategoryName"] = items[0].Category.Name;
            }
            else
            {
                items = await _repository.GetAll();
                ViewData["CategoryName"] = "All";
            }

            if (items == null)
                items = new List<Item>();

            return View(items);
            */
            return View();
        }

        
        public async Task<IActionResult> Search(string value)
        {
            /*
            var items = await _repository.FindAll(value);
            ViewData["SearchValue"] = value;
            return View(items);
            */
            return View();
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(string id)
        {
            /*
            var item = await _repository.GetById(id);
            if (item == null)
                return NotFound();
            else
                return View(item);
            */
            return View();
        }

        // GET: Items/Create
        public async Task<IActionResult> Create()
        {
            /*
            List<Category> categoriesList = await _categoriesRepo.GetAll();
            ItemFormModel model = new ItemFormModel
            {
                Categories = categoriesList
            };
            return View(model);
            */
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        public async Task<IActionResult> Create(ItemFormModel model)
        {
            /*
            var item = model.Item;
            if (item != null && item.Name != null)
            {
                item.Category = await _categoriesRepo.GetById(model.SelectedCategoryId);
                using (var stream = new MemoryStream())
                {
                    await model.ImageFile.CopyToAsync(stream);
                    item.Image = new Image();
                    item.Image.Bytes = stream.ToArray();
                    item.Image.Alt = model.Item.Name;
                    await _repository.Add(item);
                }
                return RedirectToAction(nameof(Index));
            }
            */
            return RedirectToAction(nameof(Create));
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            /*
            if (id != null)
            {
                var item = await _repository.GetById(id);
                var categoriesList = await _categoriesRepo.GetAll();
                var model = new ItemFormModel
                {
                    Item = item,
                    Categories = categoriesList,
                    SelectedCategoryId = item.Category.Id
                };
                if (item != null)
                    return View(model);
            }
            */
            return NotFound();
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ItemFormModel model)
        {
            /*
            var item = model.Item;
            if (item != null && item.Name != null)
            {
                if (model.SelectedCategoryId != null)
                    item.Category = await _categoriesRepo.GetById(model.SelectedCategoryId);
                await _repository.Update(item);
                return RedirectToAction(nameof(List));
            }
            return View(item);
            */
            return RedirectToAction(nameof(Index));
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            /*
            if (id != null && id != string.Empty)
            {
                var item = await _repository.GetById(id);
                if (item != null) return View(item);
            }
            */
            return NotFound();
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            /*
            if (id != null && id != string.Empty)
            {
                await _repository.Delete(id);
                return RedirectToAction(nameof(List));
            }
            */
            return NotFound();
        }
    }
}
