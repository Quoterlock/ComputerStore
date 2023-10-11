using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComputerStore.Data;
using ComputerStore.Models.Domains;
using ComputerStore.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.CompilerServices;
using System.Net;

namespace ComputerStore.Controllers
{
    //[Authorize(Roles = RolesContainer.MANAGER)]
    public class ItemsController : Controller
    {
        private readonly IRepository<Item> _repository;
        private readonly IRepository<Category> _categoriesRepo;

        public ItemsController(IRepository<Item> repository, IRepository<Category> categoriesRepo)
        {
            _repository = repository;
            _categoriesRepo = categoriesRepo;
        }

        // GET: Items/?categoryId
        public async Task<IActionResult> Index(string categoryId)
        {
            List<Item> items;
            if (categoryId != null)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                items = await _repository.Get(item => item.Category.Id == categoryId);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                if (items != null && items.Count > 0) 
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    ViewData["CategoryName"] = items[0].Category.Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
            else
            {
                items = await _repository.GetAll();
                ViewData["CategoryName"] = "All";
            }

            if(items == null)
                items = new List<Item>();

            return View(items);

        }

        public async Task<IActionResult> Search(string value)
        {
            var items = await _repository.FindAll(value);
            ViewData["SearchValue"] = value;
            return View(items);
        }

        // Get: Items/List
        public async Task<IActionResult> List()
        {
            return View(await _repository.GetAll());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var item = await _repository.GetById(id);
            if(item == null) 
                return NotFound();
            else 
                return View(item);
        }

        // GET: Items/Create
        public async Task<IActionResult> Create()
        {
            List<Category> categoriesList = await _categoriesRepo.GetAll();
            ItemFormModel model = new ItemFormModel {
                Categories = categoriesList
            };
            return View(model);
        }

        // POST: Items/Create
        [HttpPost]
        public async Task<IActionResult> Create(ItemFormModel model)
        {
            var item = model.Item;
            if(item != null && item.Name != null)
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
            return RedirectToAction(nameof(Create));
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
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
            return NotFound();
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ItemFormModel model)
        {
            var item = model.Item;
            if(item != null && item.Name != null)
            {
                if (model.SelectedCategoryId != null)
                    item.Category = await _categoriesRepo.GetById(model.SelectedCategoryId);
                await _repository.Update(item);
                return RedirectToAction(nameof(List));
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if(id != null && id != string.Empty)
            {
                var item = await _repository.GetById(id);
                if (item != null) return View(item);
            }
            return NotFound();
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id != null && id != string.Empty)
            {
                await _repository.Delete(id);
                return RedirectToAction(nameof(List));
            }
            return NotFound();
        }
    }
}
