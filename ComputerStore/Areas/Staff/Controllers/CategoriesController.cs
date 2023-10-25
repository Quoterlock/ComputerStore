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

namespace ComputerStore.Areas.Staff.Controllers
{
    //[Authorize(Roles = RolesContainer.MANAGER)]
    [Area("Staff")]
    public class CategoriesController : Controller
    {
        private ICategoriesService _categoriesService;
        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        // GET: Categories (public for other visitors)
        public async Task<IActionResult> Index()
        {
            var categories = await _categoriesService.GetAll();
            return View(categories);
        }

        // GET: Categories/Create
        //[Authorize(Roles = RolesContainer.MANAGER + ", " + RolesContainer.ADMINISTRATOR)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        //[Authorize(Roles = RolesContainer.MANAGER + ", " + RolesContainer.ADMINISTRATOR)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryFormModel model)
        {
           
            if (model != null && model.Name != null && model.ThumbnailFile != null)
            {
                var category = new CategoryModel() { Name = model.Name };
                using (var stream = new MemoryStream())
                {
                    await model.ThumbnailFile.CopyToAsync(stream);
                    category.Thumbnail = new ImageModel();
                    category.Thumbnail.Bytes = stream.ToArray();
                    category.Thumbnail.Alt = model.Name + "- Category thumbnail";
                    await _categoriesService.Add(category);
                }
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Categories/Edit/5
        //[Authorize(Roles = RolesContainer.MANAGER + ", " + RolesContainer.ADMINISTRATOR)]
        public async Task<IActionResult> Edit(string id)
        {
            
            if (id == null) return NotFound();
            var category = await _categoriesService.Get(id);
            if (category == null) return NotFound();

            var model = new CategoryFormModel()
            {
                Id = category.Id,
                Name = category.Name,
            };
            return View(model);
        }

        // POST: Categories/Edit/5
        //[Authorize(Roles = RolesContainer.MANAGER + ", " + RolesContainer.ADMINISTRATOR)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, CategoryFormModel model)
        {
            var category = await _categoriesService.Get(model.Id);
            category.Name = model.Name;
            await _categoriesService.Update(category);

            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Delete/5
        //[Authorize(Roles = RolesContainer.MANAGER + ", " + RolesContainer.ADMINISTRATOR)]
        public async Task<IActionResult> Delete(string id)
        {
            /*
            if (id == null) return NotFound();

            var category = await _repository.GetById(id);

            if (category == null) return NotFound();

            return View(category);
            */
            return RedirectToAction(nameof(Index));
        }

        // POST: Categories/Delete/5
        //[Authorize(Roles = RolesContainer.MANAGER + ", " + RolesContainer.ADMINISTRATOR)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            /*
            if (id != null)
            {
                await _repository.Delete(id);
                return RedirectToAction(nameof(List));
            }
            else
            {
                return NotFound(id);
            }
            */
            return RedirectToAction(nameof(Index));
        }
    }
}
