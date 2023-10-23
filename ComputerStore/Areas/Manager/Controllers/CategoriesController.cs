using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using ComputerStore.ViewModels;
using ComputerStore.BusinessLogic.Domains;

namespace ComputerStore.Areas.Manager.Controllers
{
    //[Authorize(Roles = RolesContainer.MANAGER)]
    public class CategoriesController : Controller
    {
        public CategoriesController()
        {

        }

        // GET: Categories (public for other visitors)
        public async Task<IActionResult> Index()
        {
            
            return View();
        }

        // GET: Categories/Details/5
        //[Authorize(Roles = RolesContainer.MANAGER + ", " + RolesContainer.ADMINISTRATOR)]
        public async Task<IActionResult> Details(string id)
        {
            /*
            var category = await _repository.GetById(id);
            if (category == null) return NotFound(id);
            */
            return View();
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
            /*
            if (model != null && model.Name != null && model.ThumbnailFile != null)
            {
                var category = new Category() { Name = model.Name };
                using (var stream = new MemoryStream())
                {
                    await model.ThumbnailFile.CopyToAsync(stream);
                    category.Thumbnail = new Image();
                    category.Thumbnail.Bytes = stream.ToArray();
                    category.Thumbnail.Alt = model.Name + "- Category thumbnail";
                    await _repository.Add(category);
                }
                return RedirectToAction(nameof(Index));
            }

            return View(model);
            */
            return View();
        }

        // GET: Categories/Edit/5
        //[Authorize(Roles = RolesContainer.MANAGER + ", " + RolesContainer.ADMINISTRATOR)]
        public async Task<IActionResult> Edit(string id)
        {
            /*
            if (id == null) return NotFound();

            var category = await _repository.GetById(id);

            if (category == null) return NotFound();

            return View(category);
            */
            return View();
        }

        // POST: Categories/Edit/5
        //[Authorize(Roles = RolesContainer.MANAGER + ", " + RolesContainer.ADMINISTRATOR)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,ThumbnailImageUri")] CategoryModel category)
        {
            /*
            if (id != category.Id) return NotFound();
            try
            {
                await _repository.Update(category);
                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {
                return View(category);
                // message
            }
            */
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
