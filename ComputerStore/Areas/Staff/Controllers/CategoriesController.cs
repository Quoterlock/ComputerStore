using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using ComputerStore.Areas.Staff.ViewModels;
using ComputerStore.BusinessLogic.Domains;
using ComputerStore.BusinessLogic.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Drawing;
using System.Collections;
using ComputerStore.DataAccess.Entities;
using ComputerStore.Utilities;

namespace ComputerStore.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Authorize(Roles = RolesContainer.MANAGER + "," + RolesContainer.ADMINISTRATOR)]
    public class CategoriesController : Controller
    {
        private ICategoriesService _categoriesService;
        private IImagesService _imagesService;
        public CategoriesController(ICategoriesService categoriesService, IImagesService imagesService)
        {
            _categoriesService = categoriesService;
            _imagesService = imagesService;
        }

        // GET: Categories (public for other visitors)
        public async Task<IActionResult> Index()
        {
            var categories = await _categoriesService.GetAllAsync();
            return View(categories);
        }

        // GET: Categories/Create
        //[Authorize(Roles = RolesContainer.MANAGER + ", " + RolesContainer.ADMINISTRATOR)]
        [HttpGet]
        public IActionResult Create()
        {
            var model = new CategoryFormModel() { 
                Category = new CategoryModel() };
            return View(model);
        }

        // POST: Categories/Create
        //[Authorize(Roles = RolesContainer.MANAGER + ", " + RolesContainer.ADMINISTRATOR)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryFormModel model)
        {
            if (ModelIsValid(model))
            {
                var category = model.Category;
                if (!string.IsNullOrEmpty(category.Name) && model.ThumbnailFile != null)
                {
                    category.Thumbnail = new ImageModel
                    {
                        Bytes = ConvertFileToBytes(model.ThumbnailFile),
                        Alt = category.Name + "-category-icon"
                    };
                    await _categoriesService.AddAsync(category);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        // GET: Categories/Edit/5
        //[Authorize(Roles = RolesContainer.MANAGER + ", " + RolesContainer.ADMINISTRATOR)]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = new CategoryFormModel();
            try 
            {
                var category = await _categoriesService.GetAsync(id);
                model.Category = category;

            } catch (Exception ex) { return NotFound(ex.Message); }

            return View(model);
        }

        // POST: Categories/Edit/5
        //[Authorize(Roles = RolesContainer.MANAGER + ", " + RolesContainer.ADMINISTRATOR)]
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryFormModel model)
        {
            if (!ModelIsValid(model) && await _categoriesService.IsExistsAsync(model.Category.Id)) 
                return NotFound(model.Category.Id);

            try
            {
                var category = model.Category;
                if (model.ThumbnailFile != null)
                {
                    category.Thumbnail = new ImageModel();
                    category.Thumbnail.Bytes = ConvertFileToBytes(model.ThumbnailFile);
                    category.Thumbnail.Alt = category.Name + "-category-icon";
                }
                await _categoriesService.UpdateAsync(category);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Error), nameof(CategoriesController), ex.Message);
            }

            return RedirectToAction(nameof(Index));

        }

        // GET: Categories/Delete/5
        //[Authorize(Roles = RolesContainer.MANAGER + ", " + RolesContainer.ADMINISTRATOR)]
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var model = await _categoriesService.GetAsync(id);
            return View(model);
        }

        // POST: Categories/Delete/5
        //[Authorize(Roles = RolesContainer.MANAGER + ", " + RolesContainer.ADMINISTRATOR)]
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(CategoryModel model)
        {
            if(!string.IsNullOrEmpty(model.Id))
            {
                var category = await _categoriesService.GetAsync(model.Id);
                if (category != null)
                    await _categoriesService.RemoveAsync(category.Id);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string message)
        {
            return View(message);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string value)
        {
            if(!string.IsNullOrEmpty(value))
            {
                ViewData["SearchValue"] = value;
                var model = await _categoriesService.Search(value);
                return View(nameof(Index), model);
            }
            return RedirectToAction();
        }

        private byte[] ConvertFileToBytes(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }

        private bool ModelIsValid(CategoryFormModel model)
        {
            return model != null && model.Category != null && !string.IsNullOrEmpty(model.Category.Name);
        }
    }
}
